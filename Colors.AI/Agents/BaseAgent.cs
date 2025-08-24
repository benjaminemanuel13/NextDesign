using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Smile_7.Agents;
//using Smile_7.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smile_7.Plugins.MultiAgent
{
    public abstract class BaseAgent : IAgent
    {
        public static string OpenAIKey { get; set; } = string.Empty;
        public static string Model { get; set; } = "gpt-4";

        protected Kernel kernel;

        public BaseAgent(bool addPlugins = false)
        {
            kernel = GetKernel();

            if(addPlugins) 
                AddPlugins();
        }

        private Kernel GetKernel()
        {
            kernel = Kernel
                .CreateBuilder()
                .AddOpenAIChatCompletion(Model, OpenAIKey)
                .Build();

            return kernel;
        }

        public abstract Task<string> AskAsync(string question, Action<string> del = null, Dictionary<string, object> args = null);

        protected abstract void AddPlugins();
    }
}
