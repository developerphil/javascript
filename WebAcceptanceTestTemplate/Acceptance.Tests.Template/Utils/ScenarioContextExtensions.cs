using System;
using TechTalk.SpecFlow;

namespace Acceptance.Tests.Template.Utils
{
    public static class ScenarioContextExtensions
    {
        public static T LazyLoad<T>(this SpecFlowContext specFlowContext, Func<T> creation) where T : class
        {
            T item;

            if (!specFlowContext.TryGetValue(out item))
            {
                item = creation();
                specFlowContext.Set(item);
            }

            return item;
        }

        public static T LazyLoad<T>(this SpecFlowContext specFlowContext, Func<T> creation, string key) where T : class
        {
            T item;

            if (!specFlowContext.TryGetValue(key, out item))
            {
                item = creation();
                specFlowContext.Set(item, key);
            }

            return item;
        }
    }
}
