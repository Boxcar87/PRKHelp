using PRKHelp.Components;

namespace PRKHelp
{
    public static class Router
    {
        static Dictionary<string, Component> Components = new();
        static DB DB;
        static Router()
        {
            DB = new DB();
            
            // Add new script components here
            MA maFist = new MA();
            GameTimers gameTimers = new();
            Components.Add("calc", new Calc());
            Components.Add("oe", new OE());
            Components.Add("mafist", maFist);
            Components.Add("ma", maFist);
            Components.Add("itemfind", new Item(DB));
            Components.Add("trickle", new Trickle(DB));
            Components.Add("symbiant", new Symbiant(DB));
            Components.Add("pocketboss", new PB(DB));
            Components.Add("timer", gameTimers);
            Components.Add("timers", gameTimers);
        }
        public static void Handle(string _input)
        {
            string[] sectioned = _input.Split(' ');
            string path = sectioned[0];
            string[] parameters = new string[sectioned.Length-1];
            if (parameters.Length > 0)
                parameters = sectioned[1..];

            List<string> outputStrings = [];
            string channelOverride = null;
            if (Components.ContainsKey(path))
            {
                (int validStatus, string validMessage) = Components[path].ValidateParams(parameters);

                if (validStatus < 0)
                    outputStrings.Add(validMessage);

                else
                {
                    int processStatus = Components[path].Process(parameters);

                    if (processStatus < 0)
                        outputStrings.Add($"Error processing inputs."); 
                    else
                        (outputStrings, channelOverride) = Components[path].GetResult();
                }
            }
            else
                outputStrings.Add($"{_input} is not a valid command.");
            if(channelOverride != null) 
                ScriptManager.WriteOutput(outputStrings, channelOverride);
            else
                ScriptManager.WriteOutput(outputStrings);
        }
    }
}
