using Gauge.CSharp.Lib;
using Gauge.CSharp.Lib.Attribute;

namespace CleanSimpleSolid.Integration.Tests.Steps
{
    public class StepTasks: StepBase
    {

        [Step("Create Task <name>")]
        public void CreateTask(string name)
        {
            var response = SetupRequest("/task", new {Name = name});
            ValidateOk(response);
            GaugeMessages.WriteMessage(response.Content);
        }
    }
}