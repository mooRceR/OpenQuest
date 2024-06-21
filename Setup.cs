using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using OpenRec.Tools;

namespace start
{
	class Setup
	{
		public static bool firsttime = false;
		public static void setup()
		{
			Console.OutputEncoding = Encoding.Unicode;
			// something something vault server uhh uhh ermm...
			Console.WriteLine("Getting Data... [This may take a while! lots of files 🙀]");
			Console.WriteLine();
            CreateFolders();
            if (!(File.Exists("SaveData\\App\\firsttime.txt")))
			{
				firsttime = true;
			}
            CreateLocalFileIfRequired("SaveData\\App\\firsttime.txt", "uhh just shows the app it's been ran before? 🤷");
			CreateFileIfRequired("SaveData\\avatar.txt", "Download/avatar.txt");
			if (File.ReadAllText("SaveData\\avatar.txt") == "")
            {
				File.WriteAllText("SaveData\\avatar.txt", GithubTool.GetString("Download/avatar.txt").Result);
			}
			CreateFileIfRequired("SaveData\\avataritems.txt", "Download/avataritems.txt");
			CreateFileIfRequired("SaveData\\avataritems2.txt", "Download/avataritems2.txt");
			CreateFileIfRequired("SaveData\\equipment.txt", "Download/equipment.txt");
			CreateFileIfRequired("SaveData\\consumables.txt", "Download/consumables.txt");
			CreateSaveDataIfRequired("consumables.txt");
            CreateSaveDataIfRequired("gameconfigs.txt");
            CreateSaveDataIfRequired("storefronts2.txt");
            CreateSaveDataIfRequired("baserooms.txt");

			CreateLocalFileIfRequired("SaveData\\Profile\\username.txt", ProfileTool.CreateName());
			CreateLocalFileIfRequired("SaveData\\Profile\\level.txt", "10");
			CreateLocalFileIfRequired("SaveData\\Profile\\userid.txt", new Random().Next().ToString());
			CreateLocalFileIfRequired("SaveData\\myrooms.txt", "[]");
			CreateLocalFileIfRequired("SaveData\\settings.txt", Newtonsoft.Json.JsonConvert.SerializeObject(api.Settings.CreateDefaultSettings()));

			if (!(File.Exists("SaveData\\profileimage.png")))
			{
				File.WriteAllBytes("SaveData\\profileimage.png", new WebClient().DownloadData("https://github.com/OpenRecRoom/OpenRec/raw/main/profileimage.png"));
			}
			if (!(File.Exists("SaveData\\App\\privaterooms.txt")))
			{
				File.WriteAllText("SaveData\\App\\privaterooms.txt", "Disabled");
			}
			if (!(File.Exists("SaveData\\App\\showopenrecinfo.txt")))
			{
				File.WriteAllText("SaveData\\App\\showopenrecinfo.txt", "Enabled");
			}
			if (!(File.Exists("SaveData\\App\\facefeaturesadd.txt")))
			{
				File.WriteAllText("SaveData\\App\\facefeaturesadd.txt", new WebClient().DownloadString("https://raw.githubusercontent.com/recroom2016/OpenRec/master/Download/facefeaturesadd.txt"));
			}
			if (!File.Exists("SaveData\\Rooms\\Downloaded\\roomname.txt"))
            {
				try
				{
					api.CustomRooms.RoomGet("gogo9");
				}
				catch
				{
					Console.WriteLine("Failure ?? press any key to retry (I'm not sure what this is for :scream_cat:)");
					Console.ReadKey();
					setup();
				}
				
			}
			Console.WriteLine("Done!");
			Console.Clear();
		}
        static void CreateSaveDataIfRequired(string Name)
        {
            try
            {
                if (!File.Exists("SaveData\\" + Name))
                {
                    using (StreamWriter SW = File.CreateText("SaveData\\" + Name))
                    {
                        SW.Write(GithubTool.GetString("Download/" + Name).Result);
                        SW.Close();
                        SW.Dispose();
                    }
                }
                Console.Write("█");
            }
            catch
            {
                Console.Write("⚠");
            }
        }
        static void CreateFileIfRequired(string Path, string GithubDataPath)
		{
            try
			{
                if (!File.Exists(Path))
                {
					string S = GithubTool.GetString(GithubDataPath).Result;
                    using (StreamWriter SW = File.CreateText(Path))
                    {
                        SW.Write(S);
                        SW.Close();
                        SW.Dispose();
                    }
                }
                Console.Write("█");
            }
            catch
            {
                Console.Write("⚠");
            }
        }
        static void CreateLocalFileIfRequired(string Path, string Content)
        {
            try
            {
                if (!File.Exists(Path))
                {
                    using (StreamWriter SW = File.CreateText(Path))
                    {
                        SW.Write(Content);
                        SW.Close();
                        SW.Dispose();
                    }
                }
                Console.Write("█");
            }
            catch
            {
                Console.Write("⚠");
            }
        }
        static void CreateFolders()
		{
            Directory.CreateDirectory("SaveData\\App\\");
            Console.Write("█");
            Directory.CreateDirectory("SaveData\\Profile\\");
            Console.Write("█");
            Directory.CreateDirectory("SaveData\\MultiProfiles\\");
            Console.Write("█");
            Directory.CreateDirectory("SaveData\\Images\\");
            Console.Write("█");
            Directory.CreateDirectory("SaveData\\Rooms\\");
            Console.Write("█");
            Directory.CreateDirectory("SaveData\\Images\\");
            Console.Write("█");
            Directory.CreateDirectory("SaveData\\Rooms\\Downloaded\\");
            Console.Write("█");
        }
	}
}
