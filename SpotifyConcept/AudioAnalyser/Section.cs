using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyConcept
{
    class Section
    {
        public double start { get; set; }
        public double duration { get; set; }
        public double loudness { get; set; }
        public double tempo { get; set; }
        public double key { get; set; }

        public Section()
        {
            loudness = -100;
        }
        public Section(string json)
        {
            JToken token = JObject.Parse(json);

            start = Double.Parse(token.SelectToken("start").ToString());
            duration = Double.Parse(token.SelectToken("duration").ToString());
            loudness = Double.Parse(token.SelectToken("loudness").ToString());
            tempo = Double.Parse(token.SelectToken("tempo").ToString());
            key = Double.Parse(token.SelectToken("key").ToString());
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
