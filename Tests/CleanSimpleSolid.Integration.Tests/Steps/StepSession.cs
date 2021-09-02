using Gauge.CSharp.Lib;
using Gauge.CSharp.Lib.Attribute;

namespace CleanSimpleSolid.Integration.Tests.Steps
{
    public class StepSession: StepBase
    {
        [Step("Init User")]
        public void Init()
        {
            var response = SetupRequest("/init");
            ValidateOk(response);
        }
    }
}