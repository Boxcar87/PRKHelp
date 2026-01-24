
namespace PRKHelp.Components
{
    public class BlankComponent : Component
    {
        public BlankComponent()
        {
        }

        public override (int, string) ValidateParams(string[] _params)
        {
            return SpecificParamChecks(_params);
        }

        public override (int, string) SpecificParamChecks(string[] _params)
        {
            return (1, "true");
        }

        public override int Process(string[] _params)
        {
            return 1;
        }
    }
}
