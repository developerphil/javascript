using Acceptance.Tests.Template.Utils;
using TechTalk.SpecFlow;

namespace Acceptance.Tests.Template.Steps
{
    public class BaseSteps : TechTalk.SpecFlow.Steps
    {
        public CommonSteps CommonSteps
        {
            get { return ScenarioContext.Current.LazyLoad(() => new CommonSteps()); }
        }

        public static Browser TemplateBrowser
        {
            get { return FeatureContext.Current.LazyLoad(() => new Browser()); }
        }
    }
}
