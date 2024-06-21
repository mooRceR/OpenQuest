using System;
using server;
using System.IO;
using ws;
using api;
using System.Net;
using System.Diagnostics;
using vaultgamesesh;
using System.Collections.Generic;
using Newtonsoft.Json;
using OpenRec.Tools;
using System.Threading;

namespace start
{
    class Program
    {
        static void Tutorial()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Setup.firsttime = false;
            Console.Title = "OpenQuest Intro";
            Console.WriteLine("Welcome to OpenQuest " + appversion + "!");
            Console.WriteLine("Is this your first time using OpenQuest or OpenRec?");
            Console.WriteLine("Yes or No (Y, N)");
            var A = InputTool.ReadInput();
            if (A.KeyChar == 'y' || A.KeyChar == 'Y')
            {
                Console.Clear();
                Console.Title = "OpenQuest Tutorial";
                Console.WriteLine("In that case, welcome to OpenQuest!");
                Console.WriteLine("OpenRec is server software that emulates the old servers of previous RecRoom versions.");
                Console.WriteLine("To use OpenRec, you'll need to have builds aswell!");
                Console.WriteLine("To download builds, either go to the builds channel or use the links below: (these links are also available from the #builds channel)" + Environment.NewLine);
                Console.WriteLine(GithubTool.GetString("Update/builds.txt").Result);
                Console.WriteLine("Download a build and press any key to continue:");
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("Now that you have a build, what you're going to do is as follows:" + Environment.NewLine);
                Console.WriteLine("1. Unzip the build");
                Console.WriteLine("2. Start the server by pressing 5 on the main menu and selecting your version as follows");
                Console.WriteLine("3. Run Recroom_Release.exe from the folder of the build you downloaded." + Environment.NewLine);
                Console.WriteLine("And that's it! Press any key to go to the main menu, where you will be able to start the server:");
                Console.ReadKey();
                Console.Clear();
                Main();
            }

            else
            {
                Console.Clear();
                Main();
            }
        }
        static void Main()
        {
            //startup for openrec

            GithubTool.Setup();
            Setup.setup();
            Console.CursorVisible = false;

            if (Setup.firsttime == true)
            {
                Tutorial();
            }

            Console.Title = "OpenQuest - Loading...";
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Loading...");
            try
            {
                float V = GithubTool.GetFloat("Download/version.txt").Result;
                Console.Clear();
                if (V < appversion)
                {
                    Console.WriteLine("This version of OpenQuest is outdated. We recommend you install the latest version, OpenQuest " + V);
                }
            }
            catch
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to check app version!");
                Console.ForegroundColor = ConsoleColor.Magenta;
            }
            Console.Title = "OpenQuest - Startup";
            Console.WriteLine("OpenQuest - a fork of OpenRec with some (hopefully) nice changes! :p (Version: " + appversion + ")");
            Console.WriteLine("Made and provided by RecRoom 2016 and GabeTheFirst.");
            Console.WriteLine("Download source code here: https://github.com/mooRceR/OpenQuest");
            Console.WriteLine("Discord: https://discord.gg/CmnX7KWSGb" + Environment.NewLine);
            
            Console.WriteLine("(1) What's New\n" + 
                "(2) Change Settings\n" + 
                "(3) Modify Profile\n" +
                "(4) Build Download Links\n" +
                "(5) Start Server");
            var A = InputTool.ReadInput();
            if (A.KeyChar == '1')
            {
                Changelog();
            }
            else if (A.KeyChar == '2')
            {
                Settings();
            }
            else if (A.KeyChar == '3')
            {
                Console.Clear();
                goto Profile;

            Profile:
                Console.Title = "OpenRec Profile Menu";
                Console.WriteLine("(1) Change Username" + Environment.NewLine + "(2) Change Profile Image" + Environment.NewLine + "(3) Change Level" + Environment.NewLine + "(4) Profile Downloader" + Environment.NewLine + "(5) Go Back");
                string readline3 = Console.ReadLine();
                if (readline3 == "1")
                {
                    Console.WriteLine("Current Username: " + File.ReadAllText("SaveData\\Profile\\username.txt"));
                    Console.WriteLine("New Username: ");
                    string newusername = Console.ReadLine();
                    File.WriteAllText("SaveData\\Profile\\username.txt", newusername);
                    Console.Clear();
                    Console.WriteLine("Success!");
                    goto Profile;
                }
                else if (readline3 == "2")
                {
                    Console.Clear();
                    Console.WriteLine("1) Upload Media Link" + Environment.NewLine + "2) Drag Image onto this window" + Environment.NewLine + "3) Download Rec.Net Profile Image" + Environment.NewLine + "4) Go Back");
                    string readline4 = Console.ReadLine();
                    if (readline4 == "1")
                    {
                        Console.WriteLine("Paste Media Link: ");
                        string medialink = Console.ReadLine();
                        try
                        {
                            File.WriteAllBytes("SaveData\\profileimage.png", new WebClient().DownloadData(medialink));
                        }
                        catch
                        {
                            Console.Clear();
                            Console.WriteLine("Invalid Media Link");
                            goto Profile;
                        }
                        Console.Clear();
                        Console.WriteLine("Success!");
                        goto Profile;
                    }
                    else if (readline4 == "2")
                    {
                        Console.WriteLine("Drag any image onto this window and press enter: ");
                        string imagedir = Console.ReadLine();
                        try
                        {
                            byte[] imagefile = File.ReadAllBytes(imagedir);
                            File.Replace(imagedir, "SaveData\\profileimage.png", "backupfilename.png");
                            File.WriteAllBytes(imagedir, imagefile);
                            File.Delete("backupfilename.png");
                        }
                        catch (Exception ex4)
                        {
                            Console.Clear();
                            Console.WriteLine("Invalid Image (Make sure its on the same drive as OpenRec)");
                            goto Profile;
                        }
                        Console.Clear();
                        Console.WriteLine("Success!");
                        goto Profile;
                    }
                    else if (readline4 == "3")
                    {
                        Console.WriteLine("Type a RecRoom @ username and press enter: ");
                        string username = Console.ReadLine();
                        if (username.StartsWith("@"))
                        {
                            username = username.Remove(0, 1);
                        }
                        try
                        {
                            string data = "";
                            try
                            {
                                data = new WebClient().DownloadString("https://accounts.rec.net/account/search?name=" + username);
                            }
                            catch
                            {
                                Console.Clear();
                                Console.WriteLine("Failed to download profile...");
                                Main();
                            }
                        
                            List<ProfieStealer.Root> profile = JsonConvert.DeserializeObject<List<ProfieStealer.Root>>(data);
                            byte[] profileimage = new WebClient().DownloadData("https://img.rec.net/" + profile[0].profileImage + "?cropSquare=true&width=192&height=192");
                            File.WriteAllBytes("SaveData\\profileimage.png", profileimage);
                            
                        }
                        catch
                        {
                            Console.Clear();
                            Console.WriteLine("Unable to download image...");
                            goto Profile;
                        }
                        Console.Clear();
                        Console.WriteLine("Success!");
                        goto Profile;
                    }
                    else if (readline4 == "4")
                    {
                        Console.Clear();
                        Main();
                    }
                }
                else if (readline3 == "3")
                {
                    Console.WriteLine("Current Level: " + File.ReadAllText("SaveData\\Profile\\level.txt"));
                    Console.WriteLine("New Level: ");
                    string newlevel = Console.ReadLine();
                    File.WriteAllText("SaveData\\Profile\\level.txt", newlevel);
                    Console.Clear();
                    Console.WriteLine("Success!");
                    goto Profile;
                }
                else if (readline3 == "4")
                {
                    Console.Title = "OpenRec Profile Downloader";
                    Console.Clear();
                    Console.WriteLine("Profile Downloader: This tool takes the username and profile image of any username you type in and imports it to OpenRec.");
                    Console.WriteLine("Please type the @ username of the profile you would like:");
                    string readusername = Console.ReadLine();
                    if (readusername.StartsWith("@"))
                    {
                        readusername = readusername.Remove(0, 1);
                    }
                    string data2 = "";
                    try
                    {
                        data2 = new WebClient().DownloadString("https://accounts.rec.net/account/search?name=" + readusername);
                    }
                    catch
                    {
                        Console.Clear();
                        Console.WriteLine("Failed to download profile...");
                        Main();
                    }
                    
                    ProfieStealer.ProfileSteal(data2);
                    
                    Console.Clear();
                    Console.WriteLine("Success!");
                    Main();
                }
                else if (readline3 == "5")
                {
                    Console.Clear();
                    Main();
                }
            }
            else if (A.KeyChar == '4')
            {
                Console.Title = "OpenRec Build Downloads";
                Console.Clear();
                Console.WriteLine("To download builds, either go to the builds channel or use the links below: (these links are also available from the #builds channel)" + Environment.NewLine);
                Console.WriteLine(new WebClient().DownloadString("https://raw.githubusercontent.com/recroom2016/OpenRec/master/Update/builds.txt"));
                Console.WriteLine("Download a build and press any key to continue:");
                Console.ReadKey();
                Console.Clear();
                Main();
            }
            else if (A.KeyChar == '5')
            {
                VersionSelect();
            }
            else
            {
                Main();
            }
        }

        static void VersionSelect()
        {
            Console.Title = "OpenQuest - Version Select";
            Console.Clear();
            Console.WriteLine("Please select a game version for this server to host: \n" +
                "(1) 2016 \n" +
                "(2) 2017 \n" +
                "(3) 2018");
            string readline2 = InputTool.ReadInput().KeyChar.ToString();
            if (readline2 == "1")
            {
                Console.Title = "OpenQuest - 2016";
                version = "2016";
                Console.Clear();
                Console.WriteLine("Version Selected: December 25th, 2016.");
                new APIServer();
                new WebSocket();
            }
            else if (readline2 == "2")
            {
                Console.Title = "OpenQuest - 2017";
                version = "2017";
                Console.Clear();
                Console.WriteLine("Version Selected: October 19th, 2017.");
                new APIServer();
                new WebSocket();
            }
            else if (readline2 == "3")
            {
                MonthSelect();
            }
            Console.Clear();
            Console.WriteLine("Loading...");
            try
            {
                string S = GithubTool.GetString("Download/StartupMessage.txt").Result;
                Console.Clear();
                InputTool.SILLY();
                Console.WriteLine();
                Console.WriteLine(S);
                Console.WriteLine();
                Console.WriteLine("The server is now online...");
            }
            catch
            {
                Console.WriteLine("[FAILED TO GET WELCOME MESSAGE! :p]");
            }
            Console.ReadKey();
        }

        static void MonthSelect()
        {
            Console.Clear();
            Console.WriteLine("May, July or September 2018? (M, J, S)");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("September requires run as ADMIN");
            Console.ForegroundColor = ConsoleColor.Magenta;
            string readline3 = InputTool.ReadInput().KeyChar.ToString();
            if ((readline3 == "M") || (readline3 == "m"))
            {
                Console.Title = "OpenQuest - May 2018";
                version = "2018";
                Console.Clear();
                Console.WriteLine("Version Selected: May 30th, 2018.");
                new NameServer();
                new ImageServer();
                new APIServer();
                new WebSocket();
            }
            else if ((readline3 == "S") || (readline3 == "s"))
            {
                Console.Title = "OpenQuest - September 2018";
                version = "2018";
                Console.Clear();
                Console.WriteLine("Version Selected: September 27th, 2018.");
                Console.WriteLine("Starting...");
                new NameServer();
                new ImageServer();
                new APIServer();
                new Late2018WebSock();
                Thread.Sleep(750);
            }
            else if ((readline3 == "J") || (readline3 == "j"))
            {
                Console.Title = "OpenQuest - July 2018";
                version = "2018";
                Console.Clear();
                Console.WriteLine("Version Selected: July 20th, 2018");
                new NameServer();
                new ImageServer();
                new APIServer();
                new WebSocket();
            }
            else
            {
                MonthSelect();
            }
        }

        static void Settings()
        {
            Console.Clear();

            Console.Title = "OpenQuest - Settings";
            Console.WriteLine("OpenQuest settings:\n");
            Console.WriteLine("(1) Private Rooms: " + File.ReadAllText("SaveData\\App\\privaterooms.txt") + "\n" +
                "(2) Custom Room Downloader\n" +
                "(3) Reset SaveData\n" +
                "(4) Go Back\n");
            string readline4 = InputTool.ReadInput().KeyChar.ToString();
            if (readline4 == "1")
            {
                if (File.ReadAllText("SaveData\\App\\privaterooms.txt") == "Disabled")
                {
                    File.WriteAllText("SaveData\\App\\privaterooms.txt", "Enabled");
                }
                else
                {
                    File.WriteAllText("SaveData\\App\\privaterooms.txt", "Disabled");
                }
                Console.Clear();
                Console.WriteLine("Success!");
                Settings();
            }
            else if (readline4 == "2")
            {
                Console.Title = "OpenQuest - Room Download";
                Console.Clear();
                Console.WriteLine("Custom Room Downloader: This tool takes the room data of any room you type in and imports it into ^CustomRoom in September 27th 2018.");
                Console.WriteLine("Please type in the name of the room you would like to download: (Case sensitive)");
                string roomname = Console.ReadLine();
                string text = "";
                Console.Clear();
                Console.WriteLine("Loading...");
                try
                {
                    text = new WebClient().DownloadString("https://rooms.rec.net/rooms?name=" + roomname + "&include=297");
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("Failed to download room... [Press any key to continue! :p]");
                    Console.ReadKey();
                    Settings();
                }
                CustomRooms.RoomDecode(text);
                Console.Clear();
                Console.WriteLine("Success!");
                Settings();
            }
            else if (readline4 == "3")
            {
                File.Delete("SaveData\\avatar.txt");
                File.Delete("SaveData\\avataritems.txt");
                File.Delete("SaveData\\equipment.txt");
                File.Delete("SaveData\\consumables.txt");
                File.Delete("SaveData\\gameconfigs.txt");
                File.Delete("SaveData\\storefronts2.txt");
                File.Delete("SaveData\\Profile\\username.txt");
                File.Delete("SaveData\\Profile\\level.txt");
                File.Delete("SaveData\\Profile\\userid.txt");
                File.Delete("SaveData\\myrooms.txt");
                File.Delete("SaveData\\settings.txt");
                File.Delete("SaveData\\App\\privaterooms.txt");
                File.Delete("SaveData\\App\\facefeaturesadd.txt");
                File.Delete("SaveData\\profileimage.png");
                File.Delete("SaveData\\App\\firsttime.txt");
                File.Delete("SaveData\\avataritems2.txt");
                File.Delete("SaveData\\Rooms\\Downloaded\\roomname.txt");
                File.Delete("SaveData\\Rooms\\Downloaded\\roomid.txt");
                File.Delete("SaveData\\Rooms\\Downloaded\\datablob.txt");
                File.Delete("SaveData\\Rooms\\Downloaded\\roomsceneid.txt");
                File.Delete("SaveData\\Rooms\\Downloaded\\imagename.txt");
                File.Delete("SaveData\\Rooms\\Downloaded\\cheercount.txt");
                File.Delete("SaveData\\Rooms\\Downloaded\\favcount.txt");
                File.Delete("SaveData\\Rooms\\Downloaded\\visitcount.txt");
                Console.Clear();
                Setup.setup();
                Setup.firsttime = false;
                Settings();
            }
            else if (readline4 == "4")
            {
                Console.Clear();
                Main();
            }
        }

        static void Changelog()
        {
            Console.Title = "OpenQuest Changelog";
            Console.Clear();
            Console.WriteLine("Loading...");
            try
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                string C = GithubTool.GetString("Download/changelog.txt").Result;
                Console.Clear();
                Console.WriteLine(C);
            }
            catch
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to download changelog!");
            }
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Press any key to continue:");
            Console.ReadKey();
            Console.Clear();
            Main();
        }

        public static string msg = "//This is the server sending and recieving data from recroom." + Environment.NewLine + "//Ignore this if you don't know what this means." + Environment.NewLine + "//Please start up the build now.";
        public static string version = "";
        public static float appversion = 7.1f;
        public static bool bannedflag = false;
    }

}
