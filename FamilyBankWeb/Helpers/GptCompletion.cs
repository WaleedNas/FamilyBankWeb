using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;

namespace FamilyBankWeb.Helpers
{
    public class GptCompletion
    {
        private static Conversation chat;

        public static void Initialize(bool isLoggedIn)
        {
            Console.WriteLine("Initializing GPT-4...");
            var api = new OpenAIAPI(new APIAuthentication("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"/*Environment.GetEnvironmentVariable("OPENAI_API_KEY")*/, "org-tXuTEkbOQzlQXrZWDXedJRxN")); // shorthand
            
            chat = api.Chat.CreateConversation(new ChatRequest()
            {
                Model = Model.GPT4
            });

            chat.AppendSystemMessage("You are a helpful and professional chatbot at a family bank called FamBank. " +
                                     "Always answer briefly, don't write big paragraphs." +
                                     (isLoggedIn ? "If the user is logged in and asks about how much is safe to spend, " +
                                     "reply with '*' only, nothing else." : "Recommend that the user creates an account"));
            Console.WriteLine("Finished initialization");
        }

        public static async Task<string> AskChatbot(string message)
        {
            if(chat == null)
                Initialize(true);

            chat.AppendUserInput(message);

            return await chat.GetResponseFromChatbotAsync();
        }

        public static void AddExampleChatbotAnswer(string message)
        {
            chat.AppendExampleChatbotOutput(message);
        }
    }
}
