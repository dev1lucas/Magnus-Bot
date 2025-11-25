
# Magnus BOT 🤖🧙

Magnus BOT é um bot de Discord desenvolvido em C# que integra a API do **Google Gemini** para gerar respostas inteligentes e dinâmicas dentro do servidor. Ele responde quando mencionado diretamente ou quando utilizado o prefixo `!magnus`.

---

## 🚀 Funcionalidades

- Conexão com o Discord via **Discord.Net**
- Integração com o modelo **Gemini 2.5 Flash**
- Respostas inteligentes com base em prompts do usuário
- Suporte a menção direta ou prefixo de comando
- Exibição de digitação antes de responder
- Tratamento de erros e logs do Discord

---

## 🛠️ Tecnologias Utilizadas

- **C# .NET**
- **Discord.Net**
- **Google GenAI**
- **Newtonsoft.Json**
- **WebSocket / Gateway Intents**

---

## 📁 Estrutura do Projeto

```
/Magnus_BOT
 ├── Program.cs
 ├── appsettings.json
 ├── models/
 └── README.md
```

---

## ⚙️ Configuração do `appsettings.json`

O bot utiliza um arquivo `appsettings.json` para armazenar informações sensíveis como token do bot e chave da API.

Exemplo:

```json
{
  "token": "SEU_TOKEN_DO_DISCORD",
  "apiKey": "SUA_API_KEY_DO_GEMINI",
  "prompt": "Seu prompt base para o bot"
}
```

⚠️ **Nunca compartilhe esse arquivo publicamente.**

---

## ▶️ Como Executar

1. Instale as dependências pelo NuGet:
   - `Discord.Net.WebSocket`
   - `Google.GenAI`
   - `Newtonsoft.Json`

2. Crie o arquivo `appsettings.json` com os dados necessários.

3. Compile e execute o projeto:
   ```bash
   dotnet run
   ```

O bot permanecerá ativo aguardando mensagens no Discord.

---

## 💬 Como Usar no Discord

O bot responde de duas formas:

✅ **Menção direta:**
```
@Magnus Olá, tudo bem?
```

✅ **Prefixo de comando:**
```
!magnus Qual o sentido da vida?
```

Ele então enviará uma resposta gerada pela IA:

```
🧙 @Usuário, [resposta da IA]
```

---

## 🧠 Como a IA Funciona

O método `GetAIResponse()` envia o prompt do usuário para o modelo `gemini-2.5-flash` e retorna o texto gerado. Caso ocorra erro, o bot responde com uma mensagem amigável ao usuário.

---

## 🐞 Logs e Erros

O bot utiliza logs do Discord para monitoramento e possui tratamento de exceções tanto no acesso à IA quanto no processamento da mensagem.

---

## 📜 Licença

Este projeto é livre para estudo, modificação e uso pessoal.

---

## 🙌 Autor

Projeto criado para aprendizado e integração com IA e Discord, por [@DevLucaos☕](https://x.com/DevLucaos).
