using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenRec.Tools
{
    internal static class ProfileTool
    {
        static List<string> Adjectives = new List<string>()
        {
            "Silly",
            "Goofy",
            "Crazy",
            "Happy",
            "Careless",
            "Adamant",
            "Alert",
            "Aimless",
            "Caring",
            "Successful",
            "Splooty"
        };
        static List<string> Animals = new List<string>()
        {
            "Cat",
            "Fox",
            "Possum",
            "Kitten",
            "Bat",
            "Coyote",
            "Hyena"
        };
        public static string CreateName()
        {
            Random R = new Random();
            return Adjectives[R.Next(0, Adjectives.Count)] + Animals[R.Next(0, Animals.Count)] + new Random().Next(0, 9999);
        }

        public static void CreateProfile()
        {

        }

        public static List<long> GetProfiles()
        {
            List<long> Profiles;
            string[] Folders = Directory.GetDirectories("SaveData\\MultiProfiles\\");
            return new List<long>();
        }
    }
}
