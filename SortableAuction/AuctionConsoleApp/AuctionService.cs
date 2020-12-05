using AuctionConsoleApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AuctionConsoleApp
{
    public class AuctionService
    {

        public void RunAuctionService(string inputJson, string configLocation)
        {
            var configJson = GetContentsFromTextFile(configLocation);
            var config = ParseJsonToConfig(configJson);
            var input = ParseJsonToInputList(inputJson);

            var auctionProcessor = new AuctionProcessor(config);
            var result = auctionProcessor.ProcessResult(input);
            
            Console.WriteLine(ParseResultToJson(result));
        }

        private string ParseResultToJson(IList<IList<Bid>> result)
        {
            try
            {
                return JsonConvert.SerializeObject(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Results could not be parsed to json.");
                Console.WriteLine(ex.Message);
            }

            return string.Empty;
        }

        private string GetContentsFromTextFile(string configLocation)
        {
            StringBuilder builder = new StringBuilder();
            try
            {
                using (var sr = new StreamReader(configLocation))
                {
                    while (sr.Peek() != -1)
                    {
                        builder.Append(sr.ReadLine());
                    }
                }
            }
            catch(IOException ex)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(ex.Message);
            }

            return builder.ToString();
        }

        private IList<SiteBid> ParseJsonToInputList(string inputJson)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<SiteBid>>(inputJson);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Input json could not be parsed to object.");
                Console.WriteLine(ex.Message);
            }

            return new List<SiteBid>();
        }

        private Config ParseJsonToConfig(string configJson)
        {
            try
            {
                return JsonConvert.DeserializeObject<Config>(configJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Config json could not be parsed to object.");
                Console.WriteLine(ex.Message);
            }

            return new Config();
        }
    }
}
