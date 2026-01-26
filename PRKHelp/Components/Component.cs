namespace PRKHelp.Components
{
    public class Component
    {
        public List<Type> ParamTypes = [];
        public string ParamSyntax = "None";

        public string TextColor = "<font color=#FFFFFF>";
        public string ValueColor = "<font color=#00FFFF>";
        public string HighlightColor = "<font color=#FFFF00>";
        public string RedColor = "<font color=#FF0000>";
        public string EndColor = "</font>";
        public string Indent = "   ";

        public string ChannelOverride = null;

        public List<string> OutputStrings = [];

        public Component()
        {
            OutputStrings.Add("");
        }

        // Generic validation based on 2 definitions
        public virtual (int, string) ValidateParams(string[] _params)
        {
            if (_params.Length != ParamTypes.Count)
                return (-1, $"Error. Check your inputs, should match {ParamSyntax}");

            for(var i=0; i < _params.Length; i++)
            {
                // Inputs are basically either string or int
                if (ParamTypes[i] != typeof(string))
                {
                    if (!int.TryParse(_params[i], out int parseResult))
                        return (-1, $"Error. Check your inputs, should match {ParamSyntax}");
                    if (parseResult < 0)
                        return (-1, $"Error. Parameter must be greater that 0");
                }
            }            
            return SpecificParamChecks(_params);
        }

        public virtual (int, string) SpecificParamChecks(string[] _params)
        {
            return (1, "true");
        }

        public virtual int Process(string[] _params)
        {
            return 1;
        }

        public virtual (List<string>, string) GetResult()
        {
            List<string> outputStrings = [];
            foreach(string output in OutputStrings)
            {
                outputStrings.Add(output);
            }
            OutputStrings.Clear();
            OutputStrings.Add("");
            return (outputStrings, ChannelOverride);
        }

        public string BuildItemRef(int _minID, int _maxID, int _QL, string _name)
        {
            return $"<a href=\'itemref://{_minID}/{_maxID}/{_QL}\'>{_name}</a>";
        }
    }
}
