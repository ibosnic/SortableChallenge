using System;
using System.Text;

namespace AuctionConsoleApp
{
    class Program
    {
        //hard coded path to the config file based off run command
        private const string CONFIG_PATH = "/auction/config.json";
        
        static void Main(string[] args)
        {
            var inputParameter = GetInputParameter();
            var service = new AuctionService();
            service.RunAuctionService(inputParameter, CONFIG_PATH);
        }

        private static string GetInputParameter()
        {
            var inputStringBuilder = new StringBuilder();
            var input = Console.In;

            while (input.Peek() != -1) 
            {
                inputStringBuilder.Append(input.ReadLine());
            };

            return inputStringBuilder.ToString();
        }
    }
}