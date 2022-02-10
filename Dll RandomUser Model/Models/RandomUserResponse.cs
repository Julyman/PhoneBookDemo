/* Assembly     RandomUser.Model
 * Solution     Dme.PhoneBookDemo
 * Creator      P.Rykov(julyman@yandex.ru)
 */

using System;
using System.Collections.Generic;

namespace Dme.RandomUser.Model
{
    [Serializable]
    public class RandomUserResponse
    {
        public List<RandomUser> Results { get; set; }
        public Info Info { get; set; }
    }

    [Serializable]
    public class Info
    {
        public int Results { get; set; }
        public int Page { get; set; }
        public string Version { get; set; }
        public string Seed { get; set; }

    }

    [Serializable]
    public class Result
    {
        public RandomUser User { get; set; }
        public string Seed { get; set; }
    }
}
