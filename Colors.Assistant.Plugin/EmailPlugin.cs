
using Microsoft.SemanticKernel;
using Smile_7.Common.Models;
using Smile_7.Plugins;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smile_7.Plugins.EmailPlugin
{
    public class EmailPlugin : IPlugin
    {
        [KernelFunction]
        [Description("Sends an Email using provided details using email provided by StaffLookupPlugin plugin")]
        public string SendEmail(EmailModel email)
        {
            string msg = $"** ACTION TAKEN ** Email sent to {email.To} from {email.From} body: {email.Body}";

            // Simulate sending an email
            Console.WriteLine(msg);
            return msg;
        }
    }
}
