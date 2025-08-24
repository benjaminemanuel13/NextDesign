using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smile_7.Agents
{
    public interface IAgent
    {
        Task<string> AskAsync(string question, Action<string> del = null, Dictionary<string, object> args = null);
    }
}
