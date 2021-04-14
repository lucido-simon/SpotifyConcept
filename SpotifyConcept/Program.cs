using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Web;
using Newtonsoft.Json.Linq;

namespace SpotifyConcept
{
    class Program
    {
        public static HttpListener listener;
        private static HttpClient client = new HttpClient();

        private static HttpListenerContext ctx;
        private static HttpListenerRequest req;
        private static HttpListenerResponse resp;

        public static string token;
        public static string url = "http://192.168.1.14:38000/";
        public static string redirectUri = "http://farlane.hd.free.fr:38000/";//"http://2.4.88.135:8054/"; //""http://86.210.102.116:8054/



        public static async Task sendHtmlResponse(HttpListenerResponse resp)
        {
           
            byte[] data = Encoding.UTF8.GetBytes("");
            await resp.OutputStream.WriteAsync(data, 0, data.Length);


            resp.Close();
        }
        
        public static async Task getToken()
        {
            Console.WriteLine("-- GetToken..");

            var tCtx = listener.GetContextAsync();
            System.Diagnostics.Process.Start("https://accounts.spotify.com/authorize?client_id=a701f69589c54ba4b1b26f658ec7cb6c&response_type=code&redirect_uri=" + redirectUri + "&scope=user-read-private%20user-read-email%20user-modify-playback-state%20user-read-playback-state&state=123");

            ctx = await tCtx;
            req = ctx.Request;
            resp = ctx.Response;

            string value = "grant_type=authorization_code&code=" + req.QueryString["code"] + "&redirect_uri=" + redirectUri;


            await sendHtmlResponse(resp);
            string rep = await Network.SendPost("https://accounts.spotify.com/api/token", new AuthenticationHeaderValue("Basic", "YTcwMWY2OTU4OWM1NGJhNGIxYjI2ZjY1OGVjN2NiNmM6YWM1M2QxYmRjMTQ1NDBmMDkyOWQxNDFmNWM4ZWVkNGY="), value, "application/x-www-form-urlencoded");
            Console.WriteLine("RESPONSE : " + rep);

            JToken a = JObject.Parse(rep);
            token = (string)a.SelectToken("access_token");
            Network.initAHV();
            Console.WriteLine("TOKEN : " + token);
        }

        public static async Task HandleIncomingConnections()
        {

            bool runServer = true;

            while (runServer)
            {

                 // await Player.next();
                 var a = await Player.currentTrack();
                 Console.WriteLine("--CurrentTrack : " + a);
                 var b = await AudioAnalyser.analyseTrackSection(a);
                 Console.WriteLine("--AnalyseTrack: ");
                 b.ForEach(Console.WriteLine);
                 var c = AudioAnalyser.loudestSection(b);
                 Console.WriteLine("--LoudestSection : " + c);
                 var d = await Player.setPosition(c);
                 Console.WriteLine("--SetPosition : " + d);
/*                Playlist p = new Playlist(await Network.SendGet("https://api.spotify.com/v1/albums/5VdyJkLe3yvOs0l4xXbWp0"));
                Console.WriteLine("Playlsit : " + p);
                Console.ReadLine();*/

            }
        }


        public static async Task start()
        {
        //    try
            {
                // Create a Http server and start listening for incoming connections
                listener = new HttpListener();
                listener.Prefixes.Add(url);
                listener.Start();

                Console.WriteLine("Listening for connections on {0}", url);

                await getToken();

                // Handle requests
                await HandleIncomingConnections();


                Console.WriteLine("HandleIncomingConnections returned, closing listener..");

                // Close the listener
                listener.Close();

                // Console.ReadKey();
            }

           // catch (Exception e)
            {
             //   Console.WriteLine(e.Message);
                Console.ReadLine();
            }

        }

        public static void Main(string[] args)
        {
            //You need to run in admin ( through the .exe it's easier )
            start().Wait();
            Console.WriteLine("apres start");
            Console.ReadLine();
        }
    }
}
