using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyConcept
{
    class Playlist :  Context, IRequestable
    {

        public string href { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public Paging tracks { get; set; }

        public Playlist(string json) : base(json)

        {
            JToken token = JObject.Parse(json);

            href = token.SelectToken("href").ToString();
            id = token.SelectToken("id").ToString();
            name = token.SelectToken("name").ToString();


            tracks = new Paging(token.SelectToken("tracks").ToString());

        }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
