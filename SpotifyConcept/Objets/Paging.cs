using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyConcept
{
    class Paging
    {
        public string href { get; set; }
        public List<Context> items { get; set; }
        public int limit { get; set; }
        public string next { get; set; }
        public int offset { get; set; }
        public string previous { get; set; }
        public int total { get; set; }

        public Paging(string json)
        {
            JToken token = JObject.Parse(json);

            href = token.SelectToken("href").ToString();

            items = new List<Context>();
            var itemsToken = token.SelectToken("items").ToArray();
            
            if ( itemsToken.Count() != 0 )
            {
                foreach (var a in itemsToken)
                {
                    if (a.Value<string>("type") == "playlist")
                    {
                        Console.WriteLine("TYPE PLAYLIST");
                        items.Add(new Playlist(a.ToString()));
                    }

                    else if (a.Value<string>("type") == "track")
                    {
                        Console.WriteLine("TYPE Track");
                        items.Add(new Track(a.ToString()));
                    }

                    else if (a.Value<string>("type") == null)
                    {
                        Console.WriteLine("TYPE PlaylistTrack");
                        items.Add(new Track(a.SelectToken("track").ToString()));
                    }

                    else Console.WriteLine("ERREUR Paging() : Type inconnu : " + token.Value<string>("type"));                 
                }
            }

          

            limit = Int32.Parse(token.SelectToken("limit").ToString());
            offset = Int32.Parse(token.SelectToken("offset").ToString());
            next = token.SelectToken("next").ToString();
            previous = token.SelectToken("previous").ToString();
            total = Int32.Parse(token.SelectToken("total").ToString());
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
