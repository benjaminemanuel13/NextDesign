using Colors.AI.Services;
using Smile_7.Plugins.MultiAgent;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Colors
{
    public partial class AssistantForm : Form
    {
        private SpeechService _speech = new SpeechService();
        private DynamicAgent _agent = new DynamicAgent();


        private bool speaking = true;

        public AssistantForm()
        {
            InitializeComponent();

            _speech.SpeechRecognised += SpeechService_SpeechRecognised;
        }

        private void SpeechService_SpeechRecognised(object? sender, AI.EventArguments.RecognisedSpeechEventArgs e)
        {
            if (e.Success)
            {

            }
            else
            {

            }
        }

        private async void speakTest_Click(object sender, EventArgs e)
        {
            await _agent.AskAsync("Send an email from John Doe to Jane Smith subject Hello body Hello", (s) =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    output.Text += s;
                });
            });

            input.Text = string.Empty;
        }
    }
}
