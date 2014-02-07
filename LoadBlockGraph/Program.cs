using Neo4jClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LoadBlockGraph
{
    class Program
    {
        static GraphClient neo4j;

        static async Task<string> FetchBlockJson(string blockId)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage msg = await client.GetAsync("http://blockchain.info/rawblock/" + blockId);

            return await msg.Content.ReadAsStringAsync();
        }

        static void DoBlock(string blockId)
        {
            Task<string> t = FetchBlockJson(blockId);
            JObject block = JObject.Parse(t.Result);

            
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            neo4j = new GraphClient(new Uri("http://localhost:7474/db/data"));
            neo4j.Connect();

            DoBlock("0000000000000000b6359c198b89747d41ebd833fc118e0d920a44ad63af0578");
        }
    }
}
