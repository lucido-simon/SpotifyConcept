using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyConcept
{
    class Context
    {
        public string type { get; set; }
        public string uri { get; set; }

        public Context(string json)
        {
            JToken token = JObject.Parse(json);
          
            type = token.SelectToken("type").ToString();
            uri = token.SelectToken("uri").ToString();
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
