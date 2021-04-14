using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyConcept
{
    class Album : Context, IRequestable
    {
        public string album_type { get; set; }
        public List<Artist> artists { get; set; }
        public List<string> genres { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public Paging tracks { get; set; }

        public Album(string json) : base(json)
        {
            JToken token = JObject.Parse(json);

            album_type = token.SelectToken("album_type").ToString();
            artists = new List<Artist>();
            var artistsToken = token.SelectToken("artists").ToArray();
            foreach ( var a in artistsToken )
            {
                Artist temp = new Artist(a.ToString());
                artists.Add(temp);
            }
            if (token.Value<string>("tracks") != null)
                tracks = new Paging(token.SelectToken("tracks").ToString());

            if ( token.Value<string>("genres") != null )
            {
                var genresTokens = token.SelectToken("genres").ToArray();
                genres = new List<string>();
                foreach (var a in genresTokens)
                {
                    genres.Add(a.ToString());
                }
            }

        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
