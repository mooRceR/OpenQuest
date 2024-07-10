using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using OpenRec.Tools;

namespace api2018
{
	// Token: 0x02000078 RID: 120
	public class logincached
	{
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600035E RID: 862 RVA: 0x00003A6C File Offset: 0x00001C6C
		// (set) Token: 0x0600035F RID: 863 RVA: 0x00003A74 File Offset: 0x00001C74
		public string Error { get; set; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000360 RID: 864 RVA: 0x00003A7D File Offset: 0x00001C7D
		// (set) Token: 0x06000361 RID: 865 RVA: 0x00003A85 File Offset: 0x00001C85
		public getcachedlogins Player { get; set; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000362 RID: 866 RVA: 0x00003A8E File Offset: 0x00001C8E
		// (set) Token: 0x06000363 RID: 867 RVA: 0x00003A96 File Offset: 0x00001C96
		public string Token { get; set; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000364 RID: 868 RVA: 0x00003A9F File Offset: 0x00001C9F
		// (set) Token: 0x06000365 RID: 869 RVA: 0x00003AA7 File Offset: 0x00001CA7
		public bool FirstLoginOfTheDay { get; set; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000366 RID: 870 RVA: 0x00003AB0 File Offset: 0x00001CB0
		// (set) Token: 0x06000367 RID: 871 RVA: 0x00003AB8 File Offset: 0x00001CB8
		public ulong AnalyticsSessionId { get; set; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000368 RID: 872 RVA: 0x00003AC1 File Offset: 0x00001CC1
		// (set) Token: 0x06000369 RID: 873 RVA: 0x00003AC9 File Offset: 0x00001CC9
		public bool CanUseScreenMode { get; set; }

		// Token: 0x0600036A RID: 874 RVA: 0x00009044 File Offset: 0x00007244
		public static string loginCache(ulong userid, ulong platformid)
		{
            int level = int.Parse(File.ReadAllText("SaveData\\Profile\\level.txt"));
            string name = File.ReadAllText("SaveData\\Profile\\username.txt");
            return JsonConvert.SerializeObject(new logincached
            {
                Error = "",
                Player = ProfileTool.GetProfile((long)userid),
                Token = userid.ToString(), // yeah yeah I know, but it's localhost!! it doesn't make any difference
                FirstLoginOfTheDay = true,
                AnalyticsSessionId = 392394UL,
                CanUseScreenMode = true
            });
        }

        public static string Create(ulong platformid)
        {
            int level = int.Parse(File.ReadAllText("SaveData\\Profile\\level.txt"));
            string name = File.ReadAllText("SaveData\\Profile\\username.txt");
			getcachedlogins L = ProfileTool.CreateProfile();
            return JsonConvert.SerializeObject(new logincached
            {
                Error = "",
                Player = L,
                Token = L.Id.ToString(), // yeah yeah I know, but it's localhost!! it doesn't make any difference
                FirstLoginOfTheDay = true,
                AnalyticsSessionId = 392394UL,
                CanUseScreenMode = true
            });
        }
    }
}
