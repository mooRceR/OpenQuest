using api2017;
using api2018;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
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

        public static getcachedlogins CreateProfile()
        {
            string B = "SaveData\\MultiProfiles\\";
            long ID = new Random().Next();
            B += ID + "\\";
            Directory.CreateDirectory(B);
            string Username = CreateName();
            string DisplayName = Username;
            int Level = 1;
            using(StreamWriter SW = File.CreateText(B + "userid.txt"))
            {
                SW.Write(ID);
                SW.Close();
                SW.Dispose();
            }
            using (StreamWriter SW = File.CreateText(B + "username.txt"))
            {
                SW.Write(Username);
                SW.Close();
                SW.Dispose();
            }
            using (StreamWriter SW = File.CreateText(B + "DisplayName.txt"))
            {
                SW.Write(DisplayName);
                SW.Close();
                SW.Dispose();
            }
            using (StreamWriter SW = File.CreateText(B + "Level.txt"))
            {
                SW.Write(Level);
                SW.Close();
                SW.Dispose();
            }
            return GetProfile(ID);
        }

        public static getcachedlogins GetProfile(long Id)
        {
            string B = "SaveData\\MultiProfiles\\" + Id;
            long ID = 0;
            string Username = CreateName();
            string DisplayName = Username;
            int Level = 99;
            if (File.Exists(B + "\\" + "userid.txt"))
            {
                ID = long.Parse(File.ReadAllText(B + "\\" + "userid.txt"));
            }
            if (File.Exists(B + "\\" + "username.txt"))
            {
                Username = File.ReadAllText(B + "\\" + "username.txt");
                DisplayName = Username;
            }
            if (File.Exists(B + "\\" + "DisplayName.txt"))
            {
                DisplayName = File.ReadAllText(B + "\\" + "DisplayName.txt");
            }
            if (File.Exists(B + "\\" + "Level.txt"))
            {
                Level = int.Parse(File.ReadAllText(B + "\\" + "Level.txt"));
            }
            return new getcachedlogins()
            {
                RegistrationStatus = 2,
                AvoidJuniors = false,
                CanReceiveInvites = false,
                Developer = true,
                DisplayName = DisplayName,
                ForceJuniorImages = false,
                HasBirthday = true,
                Id = (ulong)ID,
                JuniorProfile = false,
                Level = Level,
                PendingJunior = false,
                PlatformIds = new List<mPlatformID>(),
                PlayerReputation = new mPlayerReputation()
                {
                    Noteriety = 0,
                    CheerCredit = 20,
                    CheerGeneral = 10,
                    CheerHelpful = 10,
                    CheerGreatHost = 10,
                    CheerSportsman = 10,
                    CheerCreative = 10,
                    SubscriberCount = 0,
                    SubscribedCount = 0,
                    SelectedCheer = 0
                },
                ProfileImageName = "OpenQuest",
                Username = Username,
                XP = 9999
            };
        }

        public static List<long> GetProfiles()
        {
            List<long> Profiles = new List<long>();
            string[] Folders = Directory.GetDirectories("SaveData\\MultiProfiles\\");
            foreach(var F in Folders)
            {
                if(File.Exists(F + "\\" + "userid.txt"))
                {
                    Profiles.Add(long.Parse(File.ReadAllText(F + "\\" + "userid.txt")));
                }
            }
            return Profiles;
        }
    }
}
