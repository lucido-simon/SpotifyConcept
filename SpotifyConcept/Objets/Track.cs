using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyConcept
{
    class Track : Context, IRequestable
    {
        public Album album { get; set; }
        public List<Artist> artists { get; set; }
        public int duration_ms { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public string name { get; set; }

        public Track(string json) : base(json)

        {
            JToken token = JObject.Parse(json);

            if (token.Value<string>("album") != null)
                album = new Album(token.SelectToken("album").ToString());

            var artistsToken = token.SelectToken("artists").ToArray();
            artists = new List<Artist>();
            foreach (var a in artistsToken)
            {
                artists.Add(new Artist(a.ToString()));
            }

            duration_ms = Int32.Parse((string)token.SelectToken("duration_ms"));
            href = token.SelectToken("href").ToString();
            id = token.SelectToken("id").ToString();
            name = token.SelectToken("name").ToString();
        }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
