using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyConcept
{
    class Player
    {
        public static async Task<string> play(int position_ms = 0)
        {
            var rep = await Network.SendPut("https://api.spotify.com/v1/me/player/play");
            return rep += await setPosition(position_ms);
            
        }
        public static async Task<string> play(string ressource, string type = "", int position_ms = 0)
        {
            string url;
            if (type == "")
                url = "{\"context_uri\":\"" + ressource + "\",\"position_ms\":" + position_ms + "}";
            else
                url = "{\"context_uri\": \"spotify:" + type + ":" + ressource + "\",\"position_ms\":" + position_ms + "}";
            Console.WriteLine("URL" + url);
            return await Network.SendPut("https://api.spotify.com/v1/me/player/play", Network.defaultAuthenticationHeaderValue, url);

        }


        public static async Task<string> play(List<string> tracks, int position_ms = 0)
        {
            var json = JsonConvert.SerializeObject(tracks, Formatting.Indented);
            return await Network.SendPut("https://api.spotify.com/v1/me/player/play", Network.defaultAuthenticationHeaderValue, "{\"uris\":" + json  + ",\"position_ms\":" + position_ms + "}");
        }

        public static async Task<string> play(Context requestable, int position_ms = 0)
        {
            return await play(requestable.uri, "", position_ms);
        }

        public static async Task<string> play(List<Track> tracks, int position_ms = 0)
        {
            List<String> tracksString = new List<string>();
            foreach ( var t in tracks )
            {
                tracksString.Add(t.uri);
            }

            return await play(tracksString, position_ms);
        }

        public static async Task<string> play(Track track, int posisition_ms = 0)
        {
            var a = new List<string>();
            a.Add(track.uri);
            return await play(a, posisition_ms);
        }

        public static async Task<string> setPosition(int position_ms)
        {
            string url = "https://api.spotify.com/v1/me/player/seek?position_ms=" + position_ms;
            return await Network.SendPut(url);
        }

        public static async Task<string> setPosition(Section section)
        {
            return await setPosition((int)(section.start*1000));
        }

        public static async Task<string> setPosition(Segment segment)
        {
            return await setPosition((int)((segment.start + segment.loundness_max_time )* 1000));
        }

        public static async Task<string> pause()
        {
            return await Network.SendPut("https://api.spotify.com/v1/me/player/pause");
        }

        public static async Task<string> next()
        {
            return await Network.SendPost("https://api.spotify.com/v1/me/player/next");
        }

        public static async Task<string> previous()
        {
            return await Network.SendPost("https://api.spotify.com/v1/me/player/previous");
        }

        public static async Task<Track> currentTrack()
        {
            var a = await Network.SendGet("https://api.spotify.com/v1/me/player/currently-playing");
            Console.WriteLine("CURRENTTRACK = " + a);
            CurrentlyPlaying cp = new CurrentlyPlaying(a);
       
            
            return cp.item;
        }

    }
}
