using Colors.AI.Services;
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
    }
}
