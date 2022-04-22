using Discord;
using Discord.WebSocket;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Bot_Vasiliy
{
    class Program
    {
       

        DiscordSocketClient client;
        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();
        

        private async Task MainAsync()
        {

            client = new DiscordSocketClient();
            client.MessageReceived += CommandsHandler;
            client.Log += Log;

            var builder = new ConfigurationBuilder();
            string path = Directory.GetCurrentDirectory();
            builder.SetBasePath(path);
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string token = config.GetConnectionString("DefaultConnection");

            await client.LoginAsync(Discord.TokenType.Bot,token);
            await client.StartAsync();
            Console.ReadLine();
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private Task CommandsHandler(SocketMessage msg)
        {
            if (!msg.Author.IsBot)
            {
                switch (msg.Content)
                {
                    case "!info":
                        {
                            //msg.Channel.SendMessageAsync("Выводим информаию о командах");
                            msg.Channel.SendMessageAsync("Доступные команды:\n!hello\n!random\n!case");
                        }
                        break;
                    case "!hello":
                        {
                            msg.Channel.SendMessageAsync("Привет! "+msg.Author.Username);
                        }
                        break;
                    case "!random":
                        {
                            Random rnd = new Random();
                            msg.Channel.SendMessageAsync("Выпало: " + rnd.Next(10,1000));
                        }
                        break;
                    case "!case":
                        {
                            //var priceCase=ParserCase.GetNameCase("https://steamcommunity.com/market/listings/730/Chroma%202%20Case",
                            //    "//div[@class='market_commodity_orders_header_promote']"); ////span[contains(@class,'market_commodity_orders_header_promote')]

                            //msg.Channel.SendMessageAsync($"Цена на Охотничий оружейный кейс: {priceCase}");
                            msg.Channel.SendMessageAsync("Скоро будет реализация...");
                        }
                        break;
                }
            
                //msg.Channel.SendMessageAsync(msg.Content);

            }
            return Task.CompletedTask;
        }
    }

    public static class ParserCase
    {
        public static string GetNameCase(string link, string reversName)
        {
           var ws = new HtmlWeb();
           var  doc = ws.Load(link);
           string name="";
            foreach (HtmlNode l in doc.DocumentNode.SelectNodes(reversName))
            {
                name = l.InnerText;
            }
            return name;
        }
    }

}
