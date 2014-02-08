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

        private class Address
        {
            public String Address { get; set; }
        }

        private class Transaction
        {
            public String Hash { get; set; }
            public DateTime Time { get; set; }
        }

        private class Put
        {
            public Address Address { get; set; }
            public Transaction Transaction { get; set; }
            public long Value { get; set; }
        }

        private class Input : Put { }
        private class Output : Put { }

        private class Subgraph
        {
            public Subgraph()
            {
                AddressNodes = new HashSet<Address>();
                TxnNodes = new List<Transaction>();
                InputEdges = new List<Input>();
                OutputEdges = new List<Output>();
            }

            public HashSet<Address> AddressNodes { get; set; }
            public List<Transaction> TxnNodes { get; set; }
            public List<Input> InputEdges { get; set; }
            public List<Output> OutputEdges { get; set; }
        }

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
