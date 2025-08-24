using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colors.AI.EventArguments
{
    public class RecognisedSpeechEventArgs : EventArgs
    {
        public string RecognisedText { get; set; } = string.Empty;
        public bool Success { get; set; } = true;

        public RecognisedSpeechEventArgs(string recognisedText, bool success)
        {
            RecognisedText = recognisedText;
            Success = success;
        }
    }
}
