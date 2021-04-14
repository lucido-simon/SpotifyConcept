using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyConcept
{
    class Segment
    {
        public double start { get; set; }
        public double duration { get; set; }
        public double loudness_max { get; set; }
        public double loundness_max_time { get; set; }


        public Segment()
        {
            loudness_max = -100;
        }
        public Segment(string json)
        {
            JToken token = JObject.Parse(json);

            start = Double.Parse((string)token.SelectToken("start"));
            duration = Double.Parse((string)token.SelectToken("duration"));
            loudness_max = Double.Parse((string)token.SelectToken("loudness_max"));
            loundness_max_time = Double.Parse((string)token.SelectToken("loudness_max_time"));     

        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
