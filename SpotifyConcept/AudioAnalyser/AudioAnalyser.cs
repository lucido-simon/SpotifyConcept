using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyConcept
{
    class AudioAnalyser
    {
        public static async Task<List<Section>> analyseTrackSection(string id)
        {
            var a = await Network.SendGet("https://api.spotify.com/v1/audio-analysis/" + id);
            JToken token = JObject.Parse(a);

            List<Section> sections = new List<Section>();

            var sectionJson = token.SelectToken("sections").ToArray();
            foreach (var b in sectionJson)
            {
                sections.Add(new Section(b.ToString()));
            }

            return sections;
        }

        public static async Task<List<Section>> analyseTrackSection(Track track)
        {
            return await analyseTrackSection(track.id);
        }

        public static Section loudestSection(List<Section> sections)
        {
            Section loudest = new Section();
            if (sections.Count != 0)
                 loudest = sections[0];

            foreach (var item in sections)
            {
                if (item.loudness > loudest.loudness)
                    loudest = item;
            }

            return loudest;
        }

        public static async Task<List<Segment>> analyseTrackSegment(string id)
        {
            var a = await Network.SendGet("https://api.spotify.com/v1/audio-analysis/" + id);
            JToken token = JObject.Parse(a);

            List<Segment> segments = new List<Segment>();

            var segmentsJson = token.SelectToken("segments").ToArray();
            foreach (var b in segmentsJson)
            {
                segments.Add(new Segment(b.ToString()));
            }

            return segments;
        }

        public static async Task<List<Segment>> analyseTrackSegment(Track track)
        {
            return await analyseTrackSegment(track.id);
        }

        public static Segment loudestSegment(List<Segment> segments)
        {
            Segment loudest = new Segment();
            if (segments.Count != 0)
                loudest = segments[0];

            foreach (var item in segments)
            {
                if (item.loudness_max > loudest.loudness_max)
                    loudest = item;
            }

            return loudest;
        }
    }
}
