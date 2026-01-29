
using System.Diagnostics;

namespace PRKHelper.Parser
{
    public class CombatParser
    {
        static string LogPath;
        static long LogSize;
        static int LastReadLine = 1;
        static bool TrackXPBool = false;
        static bool TrackDamageBool = false;
        static int LastTotalXP = 0;
        static int TotalXP = 0;
        static int TotalDamage = 0;
        long XPPausedTime = 0;
        long XPStartTime = 0;
        long XPEndTime;
        long LastPooledXP = 0;
        long DamagePausedTime = 0;
        long DamageStartTime = 0;
        long DamageEndTime;
        string PetName = String.Empty;
        RichTextBox DamageField;
        RichTextBox XPField;

        public CombatParser(RichTextBox _damageField, RichTextBox _xpField)
        {
            DamageField = _damageField;
            XPField = _xpField;
        }

        async public void Run()
        {
            if (TrackXPBool || TrackDamageBool)
            {
                if (TrackXPBool)
                {
                    if (TotalXP > 0)
                    {
                        XPEndTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                        long totalTime = XPEndTime - XPStartTime;
                        float timeInHours = totalTime / 1000f / 60f / 60f;
                        int xpPerHour = (int)(TotalXP / timeInHours);
                        if (LastTotalXP == 0 || TotalXP > LastTotalXP)
                        {
                            XPField.Text = FormXPString(xpPerHour);
                            LastTotalXP = TotalXP;
                        }
                        if (totalTime % 5000 < 500)
                            XPField.Text = FormXPString(xpPerHour);
                    }
                }
                if (TrackDamageBool)
                {
                    if (TotalDamage > 0)
                    {
                        DamageEndTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                        long totalTime = DamageEndTime - DamageStartTime;
                        float timeInMinutes = totalTime / 1000f / 60f;
                        int damagePerMinute = (int)(TotalDamage / timeInMinutes);
                        DamageField.Text = damagePerMinute.ToString();
                    }
                }

                await Parse();
            }
            await Task.Delay(500);
            Run();
        }

        async public Task Parse()
        {
            using (FileStream fileStream = new(LogPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                if (fileStream.Length == LogSize)
                    return;

                LogSize = fileStream.Length;
                using (StreamReader reader = new(fileStream))
                {
                    for(int i=1; i< LastReadLine; i++)
                    {
                        reader.ReadLine();
                    }
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string messageType = line[17].ToString()+line[18].ToString();
                        if(messageType == "05" || messageType == "08" || messageType == "09")
                        {
                            //bool crit = false;
                            string[] splitLine = line.Split("]");
                            string toParse = splitLine[1];

                            if (messageType == "09" && PetName != String.Empty)
                            {
                                int petNameIndex = PetName.Length;
                                if (toParse[..petNameIndex] != PetName)
                                    continue;
                            }

                            //if (toParse[^1] == '!')
                            //    crit = true;

                            int damageEndIndex = toParse.IndexOf("points of")-1;
                            string truncated = toParse[..damageEndIndex];
                            int damageStartIndex = truncated.LastIndexOf(" ")+1;
                            string damageString = truncated[damageStartIndex..];
                            bool isDamage = Int32.TryParse(damageString, out int damage);
                            if (isDamage)
                            {
                                if(TotalDamage < 1)
                                    DamageStartTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

                                TotalDamage += damage;
                            }
                        }
                        else if(messageType == "0b" || messageType == "01")
                        {
                            string[] splitLine = line.Split("]");
                            string toParse = splitLine[1];
                            if(messageType == "01") // System message check for XP pool
                            {
                                if (toParse[..7] == "This XP")
                                {
                                    int XPStringStartIndex = toParse.LastIndexOf(" ")+1;
                                    string XPString = toParse[XPStringStartIndex..];
                                    bool isXP = Int64.TryParse(XPString[..^1], out long NewPoolXP);
                                    if (isXP)
                                    {
                                        if (LastPooledXP == 0)
                                        {
                                            LastPooledXP = NewPoolXP;
                                            continue;
                                        }
                                        if(TotalXP < 1)
                                            XPStartTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

                                        TotalXP += (int)(NewPoolXP - LastPooledXP);
                                    }
                                }
                            }
                            else
                            {
                                int XPEndIndex = toParse.LastIndexOf(" ")-1;
                                string truncated = toParse[..XPEndIndex];
                                int XPStartingIndex = truncated.LastIndexOf(" ")+1;
                                string XPString = truncated[XPStartingIndex..];
                                bool isXP = Int32.TryParse(XPString, out int XPGained);
                                if (isXP)
                                {
                                    if(TotalXP == 0)
                                        XPStartTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                                    TotalXP += XPGained;
                                }
                            }
                        }
                        LastReadLine++;
                    }
                }
            }
        }

        public void TrackXP()
        {
            if (XPPausedTime > 0)
                XPStartTime += DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - XPPausedTime;
            TrackXPBool = true;
        }

        public void StopXP()
        {
            TrackXPBool = false;
            XPPausedTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }
        public void TrackDamage(string _petName)
        {
            if (DamagePausedTime > 0)
                DamageStartTime += DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - DamagePausedTime;
            PetName = _petName;
            TrackDamageBool = true;
        }

        public void StopDamage()
        {
            TrackDamageBool = false;
            DamagePausedTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }

        public void ResetXP()
        {
            TotalXP = 0;
            XPStartTime = 0;
            XPEndTime = 0;
            LastPooledXP = 0;
            LastTotalXP = 0;
            XPField.Text = "0";
            XPPausedTime = 0;
            TrackXPBool = false;
            LastReadLine = 1;
            using (FileStream fileStream = new(LogPath, FileMode.Truncate, FileAccess.ReadWrite, FileShare.ReadWrite)) { }
        }

        public void ResetDamage(string _petName)
        {
            TotalDamage = 0;
            DamageStartTime = 0;
            DamageEndTime = 0;
            DamageField.Text = "0";
            DamagePausedTime = 0;
            TrackXPBool = false;
            LastReadLine = 1;
            using (FileStream fileStream = new(LogPath, FileMode.Truncate, FileAccess.ReadWrite, FileShare.ReadWrite)) { }
        }

        public void UpdatePath(string _logPath)
        {
            LogPath = _logPath;
        }

        static string FormXPString(int _xp)
        {
            switch (_xp)
            {
                case > 1000000:
                    return ((float)_xp / 1000000f).ToString("N1")+"M";
                case > 50000:
                    return (_xp / 1000).ToString()+"K";
                case > 1000:
                    return ((float)_xp / 1000f).ToString("N1")+"K";
                default:
                    return _xp.ToString();
            }
        }
    }
}
