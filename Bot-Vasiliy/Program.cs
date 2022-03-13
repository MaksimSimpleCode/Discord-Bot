using Discord;
using Discord.WebSocket;
using HtmlAgilityPack;
using System;
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

            var token = "OTUyMTA1MzQxMzQ3MzE1NzUy.YixLMg.DUo3B5G0wEE5B7Ggf7Es56OrrrM";

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
                            var priceCase=ParserCase.GetNameCase("https://steamcommunity.com/market/listings/730/Chroma%202%20Case",
                                ".//div[@class='market_commodity_orders_block']//span[contains(@class,'market_commodity_orders_header_promote')]");

                            msg.Channel.SendMessageAsync($"Цена на Охотничий оружейный кейс: {priceCase}");
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
