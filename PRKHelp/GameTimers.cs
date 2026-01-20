using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using System.Diagnostics;

namespace PRKHelp
{
    public class GameTimers
    {
        static Dictionary<string, (long, System.Timers.Timer)> CustomTimers = new Dictionary<string, (long, System.Timers.Timer)>();
        //static string TimerEndedWriteFile;

        public GameTimers(string _logFile)
        {
            //TimerEndedWriteFile = _logFile;
        }
        public string CreateTimer(string name, string duration)
        {
            long durationLimit = 2 * 3600000; // Do you want a limit?
            duration = duration.Trim(' ');
            long totalDuration = 0;
            List<string> uniqueDenotationsUsed = [];

            // Split the string into sections based on hour minute and second.
            string[] durationSections = Regex.Split(duration, @"(?<=[hm])");
            foreach (string section in durationSections)
            {
                if (section == string.Empty) // Clean up hanging empty elements in array
                    continue;

                string denotation = section[^1].ToString();
                string durationOnly = section[..^1];

                // Ensure the format doesnt have duplicates like 1h1h1s
                if (!uniqueDenotationsUsed.Contains(denotation))
                    uniqueDenotationsUsed.Add(denotation);
                else
                    return "/text Duplicate denotation used. Please used format 1h10m30s";

                bool validDuration = Int64.TryParse(durationOnly, out long durationTime);
                if (!validDuration)
                    return "/text Invalid duration. Please use format 1h10m30s";
                if (durationTime < 0)
                    return "/text Duration must be greater than 0. Please use format 1h10m30s";

                // multiply duration milliseconds appropriately
                switch (denotation.ToLower())
                {
                    case "s":
                        durationTime *= 1000;
                        break;
                    case "m":
                        durationTime *= 60000;
                        break;
                    case "h":
                        durationTime *= 3600000;
                        break;
                    default:
                        return "/text Invalid duration type. Please use format 1h10m30s";
                }
                totalDuration += durationTime;
            }

            // Keep if you want a limit
             if(totalDuration > durationLimit)
            {
                return "/text No No No, I want to be alive when this timer ends. 1 Hour maximum";
            }

            long timerEnd = DateTimeOffset.Now.ToUnixTimeMilliseconds() + totalDuration;

            // Create new timer and pass name into callback for proper termination once timer ends
            System.Timers.Timer newTimer = new(totalDuration);
            newTimer.Elapsed += (sender, e) => ExpireTimer(name);
            newTimer.AutoReset = false;
            newTimer.Start();

            CustomTimers.Add(name, (timerEnd, newTimer));
            return $"/text Created timer for {name} Successfully";
        }

        public string GetTimers()
        {
            string highlightColor = "#FFFF00";
            string alertColor = "#FF0000";
            string textColor = "#FFFFFF";
            string responseText = "/text <a href=\"text://Active Timers:<br><br>";
            foreach (KeyValuePair<string, (long, System.Timers.Timer)> timer in CustomTimers)
            {
                string secondsColor = textColor;
                long remaining = timer.Value.Item1 - DateTimeOffset.Now.ToUnixTimeMilliseconds();
                if (remaining <= 60000)
                    secondsColor = alertColor;

                responseText += $"    <font color={highlightColor}>{timer.Key}</font>: <font color={secondsColor}>{ParseTimeRemaining(timer.Value.Item1 - DateTimeOffset.Now.ToUnixTimeMilliseconds())}</font> remaining<br><br>";
            }
            responseText += "\">Current Timers</a>";
            return responseText;
        }
        static void ExpireTimer(string name)
        {
            CustomTimers[name].Item2.Dispose();
            //Console.WriteLine($"{name} timer has been disposed"); // Left in for testing functionality
            CustomTimers.Remove(name);
            //File.WriteAllText(TimerEndedWriteFile, $"!TimerEnd Timer: {name} has just expired!");
        }

        // Convert time into readable string
        static string ParseTimeRemaining(long remaining)
        {
            TimeSpan t = TimeSpan.FromMilliseconds(remaining);

            remaining /= 1000;
            switch (remaining)
            {
                case >= 3600: // Hours
                    return $"{(int)t.Hours}h {t.Minutes}m {t.Seconds}s";
                case >= 60: // Minutes
                    return $"{t.Minutes}m {t.Seconds}s";
                default:
                    return $"{t.Seconds}s";
            }
        }
    }
}
