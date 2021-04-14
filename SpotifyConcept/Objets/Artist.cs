using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyConcept
{
    class Artist : Context, IRequestable
    {
        public string href { get; set; }
        public string id { get; set; }
        public string name { get; set; }


        public Artist(string json) : base(json)
        {
            JToken token = JObject.Parse(json);

            href = token.SelectToken("href").ToString();
            id = token.SelectToken("id").ToString();
            name = token.SelectToken("name").ToString();
            type = token.SelectToken("type").ToString();
            uri = token.SelectToken("uri").ToString();
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        public static List<Artist> GenerateList(string artists)
        {
            throw new NotImplementedException();
        }
    }
}
