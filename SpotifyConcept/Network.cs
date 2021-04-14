using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyConcept
{
    class Network
    {
        public static AuthenticationHeaderValue defaultAuthenticationHeaderValue;
        public static void initAHV()
        {
            defaultAuthenticationHeaderValue = new AuthenticationHeaderValue("Bearer", Program.token);
        }

        public static async Task<string> SendPost(string url, AuthenticationHeaderValue authenticationHeaderValue = null, string content = "", string mediatype = "")
        {
            HttpClient client = new HttpClient();
            if ( authenticationHeaderValue == null )       
                authenticationHeaderValue = defaultAuthenticationHeaderValue;
            client.DefaultRequestHeaders.Authorization = authenticationHeaderValue;

            HttpRequestMessage mess = new HttpRequestMessage(HttpMethod.Post, url);
            if (content != "")
            {
                if ( mediatype != "")       
                    mess.Content = new StringContent(content, Encoding.UTF8, mediatype);
                else mess.Content = new StringContent(content, Encoding.UTF8);
            }

            var response = await client.SendAsync(mess);
            return await response.Content.ReadAsStringAsync();
        }
      
    

        public static async Task<string> SendGet(string url, AuthenticationHeaderValue authenticationHeaderValue = null)
        {
            HttpClient client = new HttpClient();
            if (authenticationHeaderValue == null)
                authenticationHeaderValue = defaultAuthenticationHeaderValue;
            client.DefaultRequestHeaders.Authorization = authenticationHeaderValue;
            HttpRequestMessage mess = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await client.SendAsync(mess);
            return await response.Content.ReadAsStringAsync();
        }

        public static async Task<string> SendPut(string url, AuthenticationHeaderValue authenticationHeaderValue = null, string content = "", string mediatype = "")
        {
            HttpClient client = new HttpClient();
            if (authenticationHeaderValue == null)
                authenticationHeaderValue = defaultAuthenticationHeaderValue;
            client.DefaultRequestHeaders.Authorization = authenticationHeaderValue;
            HttpRequestMessage mess = new HttpRequestMessage(HttpMethod.Put, url);
            if (content != "")
            {
                if (mediatype != "")
                    mess.Content = new StringContent(content, Encoding.UTF8, mediatype);
                else mess.Content = new StringContent(content, Encoding.UTF8);
            }
            var response = await client.SendAsync(mess);
            var responseString = await response.Content.ReadAsStringAsync();
            Console.WriteLine("SendPut : " + responseString);
            return responseString;
        }
    }
}
