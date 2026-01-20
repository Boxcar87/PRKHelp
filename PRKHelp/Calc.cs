using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PRKHelp
{
    internal class Calc
    {
        public static string GetResult(string formula, bool windowResponseText = false)
        {
            //Style input string         
            string pattern = @"[+\-*/%()]";
            string styledFormula = "";
            string textColor = "#FFFFFF"; //uncomment to specify color of numbers in formula
            string operatorColor = "#00FFFF";
            string newLineText = "<br>               ";
            int newLineStartMarker = newLineText.Length - 4;
            int textRowSizeLimit = 35;
            int maxInputLength = 200; //Estimated limit to keep response under 1kb //This is really just to throttle the size of response from script, may not be an issue.

            if (windowResponseText)
                styledFormula += $"/text <a href=\"text://<br><br>Equation: <font color={textColor}>";

            else
                styledFormula += $"/text Equation: <font color={textColor}>";

            //Preliminary size check
            if (formula.Length > maxInputLength)
                return "/text Error: Input formula too large";

            int styledFormulaRowLength = 10;
            foreach (char character in formula)
            {
                string _tempString = character.ToString();
                if (Regex.Match(_tempString, pattern).Success)
                {
                    _tempString = $"<font color={operatorColor}>{_tempString}</font>";
                }
                styledFormula += _tempString;
                styledFormulaRowLength++;
                if (windowResponseText)
                {
                    //Only make new line if curent character is an operator and longer than desired length
                    if (_tempString[0].ToString() == "<" && styledFormulaRowLength > textRowSizeLimit)
                    {
                        styledFormula += newLineText;
                        styledFormulaRowLength = newLineStartMarker;
                    }
                }
            }
            styledFormula += "</font>";

            //!Calc command should already be trimmed from string
            //Ensure \n at the end of string has already been trimmed
            double? product = Evaluate(formula);
            string productColor = "#FFFF00";
            string responseText;
            if (product.HasValue)
            {

                if (windowResponseText)
                    responseText = $"{styledFormula}<br><br>Result: <font color={productColor}>{product.ToString()}</font>\">Equation Successful: Click for Results";
                else
                    responseText = $"{styledFormula}<font color={operatorColor}> = </font><font color={productColor}>{product.ToString()}</font>";
            }
            else
            {
                responseText = $"/text Error! Math is hard, check your inputs.";
            }
            return responseText;
        }

        public static double? Evaluate(string expression)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("expression", typeof(string), expression);
                DataRow row = table.NewRow();
                table.Rows.Add(row);
                return double.Parse((string)row["expression"]);
            }

            catch
            {
                return null;
            }
        }
    }
}
