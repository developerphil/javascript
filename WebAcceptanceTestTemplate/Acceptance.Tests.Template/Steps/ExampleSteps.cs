using TechTalk.SpecFlow;

namespace Acceptance.Tests.Template.Steps
{
    [Binding]
    public class ExampleSteps : BaseSteps
    {
        [When(@"I search for specflow")]
        public void WhenSearchForSpecflow()
        {
            TemplateBrowser.SetElementText("gbqfq", "specflow");
        }

    }
}
