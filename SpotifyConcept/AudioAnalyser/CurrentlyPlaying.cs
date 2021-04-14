using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyConcept
{
    class CurrentlyPlaying
    {
        public Context context { get; set; }
        public UInt64 timestamp { get; set; }
        public UInt64 progress_ms { get; set; }
        public bool is_playing{ get; set; }
        public Track item { get; set; }


        public CurrentlyPlaying(string json)
        {
            JToken token = JObject.Parse(json);

            context = new Context( token.SelectToken("context").ToString() );
            timestamp = UInt64.Parse(token.SelectToken("timestamp").ToString());
            progress_ms = UInt64.Parse(token.SelectToken("progress_ms").ToString());
            is_playing = Boolean.Parse(token.SelectToken("is_playing").ToString());
            item = new Track((token.SelectToken("item").ToString()));
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}

