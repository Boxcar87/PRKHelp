
namespace PRKHelp.Components
{
    public class Template : Component
    {
        //string TextColor
        //string ValueColor
        //string HighlightColor
        //string RedColor
        //string EndColor
        //string Indent

        //List<Type> ParamTypes

        //List<string> OutputStrings; // Inherited object retrieved for response by Route()
        public Template(/*DB _db*/) // Pass DB reference in from route if needed
        {
            // Base class will perform basic validation on params
            ParamSyntax = "/template 123 string";
            ParamTypes.Add(typeof(int));
            ParamTypes.Add(typeof(string));
        }

        // If you have variable inputs you can override ValidateParams, otherwise this performs basic param validation
        public override (int, string) ValidateParams(string[] _params)
        {
            return SpecificParamChecks(_params);
        }

        // Use this function override to append additional param checks if needed
        public override (int, string) SpecificParamChecks(string[] _params)
        {
            int statusCode = -1; // -1 for error 1 for success
            string statusMessage = "invalid params";

            return (statusCode, statusMessage);
        }

        // Perform function logic here
        public override int Process(string[] _params)
        {
            int statusCode = 1; // -1 for error 1 for success

            OutputStrings[0] = $"Result of function";
            OutputStrings.Add($"Additional scripts can be generated as new lines"); // Useful when text output would exceed 4096 characters (/text limit)

            // Route() will return a generic failure if value here is -1.
            return statusCode;
        }
    }
}
