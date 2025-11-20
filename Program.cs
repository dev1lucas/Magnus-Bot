using Discord;
using Discord.WebSocket;
using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using Magnus_BOT.models;

namespace Magnus_BOT
{
    internal class Program
    {
        private readonly Bot bot;
        private readonly DiscordSocketClient client;

        public Program()
        {
            string jsonString = File.ReadAllText("appsettings.json");
            this.bot = JsonConvert.DeserializeObject<Bot>(jsonString);

            var config = new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
            };
            this.client = new DiscordSocketClient(config);

            this.client.MessageReceived += MessageHandler;
            this.client.Log += LogFuncAsync; 

            Task LogFuncAsync(LogMessage message) => Task.Run(() => Console.WriteLine(message.ToString()));
        }

        public async Task StartBotAsync()
        {
            await this.client.LoginAsync(TokenType.Bot, bot.token);
            await this.client.StartAsync();

            await Task.Delay(-1);
        }

        private async Task<string> GetAIResponse(string message)
        {
            try
            {
                var clientAI = new Google.GenAI.Client(apiKey: bot.apiKey);

                var response = await clientAI.Models.GenerateContentAsync(
                    model: "gemini-2.5-flash",
                    contents: (bot.prompt + message)
                );
                return response.Candidates.FirstOrDefault()?.Content.Parts.FirstOrDefault()?.Text ?? "Resposta vazia.";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao acessar a API do Gemini: {ex.Message}");
                return "Desculpe, não consegui entrar em contato com a minha inteligência artificial.";
            }
        }

        private async Task MessageHandler(SocketMessage message)
        {
            if (message.Author.IsBot) return;

            if (client.CurrentUser == null) return;

            string mentionPrefix = $"<@{client.CurrentUser.Id}>";
            string prefix = "!magnus ";
            string userPrompt = string.Empty;

            if (message.Content.StartsWith(mentionPrefix))
            {
                userPrompt = message.Content.Substring(mentionPrefix.Length).Trim();
            }
            else if (message.Content.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            {
                userPrompt = message.Content.Substring(prefix.Length).Trim();
            }
            else
            {
                return;
            }

            if (!string.IsNullOrWhiteSpace(userPrompt))
            {
                try
                {
                    await message.Channel.TriggerTypingAsync();
                    string aiResponse = await GetAIResponse(userPrompt);
                    await message.Channel.SendMessageAsync($"🧙 {message.Author.Mention}, {aiResponse}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro no processamento da IA: {ex.Message}");
                    await message.Channel.SendMessageAsync($"Desculpe {message.Author.Mention}, não consegui processar sua pergunta agora. 😥");
                }
            }
        }

        static async Task Main(string[] args)
        {
            var myBot = new Program();

            await myBot.StartBotAsync();
        }
    }
}