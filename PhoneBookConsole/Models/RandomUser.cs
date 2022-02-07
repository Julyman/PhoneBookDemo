/* Assembly     PhoneBookConsole (PhoneBookConsole app)
 * Solution     Dme.PhoneBookDemo
 * Creator      P.Rykov(julyman@yandex.ru)
 */

using Newtonsoft.Json;
using System;

namespace Dme.PhoneBookConsole.Models
{
    [Serializable]
    public class RandomUser
    {
        [JsonProperty("dob")]
        public Dob Dob { get; set; }

        [JsonProperty("name")]
        public Name Name { get; set; }

        [JsonProperty("picture")]
        public Picture Picture { get; set; }
    }

    [Serializable]
    public class Name
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("first")]
        public string First { get; set; }

        [JsonProperty("last")]
        public string Last { get; set; }
    }

    [Serializable]
    public class Dob
    {
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }
    }

    [Serializable]
    public class Picture
    {
        [JsonProperty("large")]
        public string Large { get; set; }

        [JsonProperty("medium")]
        public string Medium { get; set; }

        [JsonProperty("thumbnail")]
        public string Thumbnail { get; set; }
    }
}

