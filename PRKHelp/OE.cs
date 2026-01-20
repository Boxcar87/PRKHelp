using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRKHelp
{
    internal class OE
    {
        public static string GetResult(string value)
        {
            bool valid = Int32.TryParse(value, out int numValue);
            if (!valid)
                return "/text Error! Check your input value";
            if (numValue < 0)
                return "/text Error! Value must be > 0";

            double oe100 = Math.Floor(numValue / 0.8);
            double oe75 = Math.Floor(numValue / 0.6);
            double oe50 = Math.Floor(numValue / 0.4);
            double oe25 = Math.Floor(numValue / 0.2);
            double oe100min = Math.Floor(numValue * 0.8);
            double oe75min = Math.Floor(numValue * 0.6);
            double oe50min = Math.Floor(numValue * 0.4);
            double oe25min = Math.Floor(numValue * 0.2);

            string textColor = "#FFFFFF";
            string valueColor = "#00FFFF";
            string highlightColor = "#FFFF00";
            string indentation = "  ";

            string responseText = $"/text <a href=\"text://<font color={textColor}>OE data for <font color={valueColor}>{value}</font> skill";
            responseText += $"<br><br>Item requires <font color={valueColor}>{value}</font> - mininum required:";
            responseText += $"<br>{indentation}<font color={highlightColor}>100%</font>: <font color={valueColor}>{oe100min}</font>";
            responseText += $"<br>{indentation}{indentation}<font color={highlightColor}> 75%</font>: <font color={valueColor}>{oe75min}</font>";
            responseText += $"<br>{indentation}{indentation}<font color={highlightColor}> 50%</font>: <font color={valueColor}>{oe50min}</font>";
            responseText += $"<br>{indentation}{indentation}<font color={highlightColor}> 25%</font>: <font color={valueColor}>{oe25min}</font>";
            responseText += $"<br><br>Your skill is <font color={valueColor}>{value}</font> - effectiveness at requirement:";
            responseText += $"<br>{indentation}<font color={highlightColor}>100%</font>: <font color={valueColor}>{oe100}</font>";
            responseText += $"<br>{indentation}{indentation}<font color={highlightColor}> 75%</font>: <font color={valueColor}>{oe75}</font>";
            responseText += $"<br>{indentation}{indentation}<font color={highlightColor}> 50%</font>: <font color={valueColor}>{oe50}</font>";
            responseText += $"<br>{indentation}{indentation}<font color={highlightColor}> 25%</font>: <font color={valueColor}>{oe25}</font>";
            responseText += $"</font>\">{oe100min} - {value} - {oe100}";

            return responseText;
        }
    }
}
