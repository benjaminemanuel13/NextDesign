using Microsoft.CognitiveServices.Speech;
using System;
using System.ClientModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colors.AI.Services
{
    public class SpeechService
    {
        public static string Key { get; set; } = string.Empty;
        public static string Endpoint { get; set; } = string.Empty;

        public event EventHandler<EventArguments.RecognisedSpeechEventArgs> SpeechRecognised;

        private SpeechConfig _config;
        private SpeechRecognizer _recognizer;
        private SpeechSynthesizer _speech;

        public SpeechService()
        {
            Initialize();
        }

        private async void Initialize()
        {
            _config = SpeechConfig.FromEndpoint(new Uri(Endpoint), new ApiKeyCredential(Key));
            _recognizer = new SpeechRecognizer(_config);
            _recognizer.Recognized += Speech_Recognised;

            _speech = new SpeechSynthesizer(_config);
        }

        public async void Speak(string text)
        {
            var result = await _speech.SpeakTextAsync(text);
        }

        public async void StartRecognising()
        {
            await _recognizer.StartContinuousRecognitionAsync();
        }

        public async void StopRecognising()
        {
            await _recognizer.StopContinuousRecognitionAsync();
        }

        private void Speech_Recognised(object sender, SpeechRecognitionEventArgs e)
        {
            if (e.Result.Reason == ResultReason.RecognizedSpeech)
            {
                SpeechRecognised?.Invoke(this, new EventArguments.RecognisedSpeechEventArgs(e.Result.Text, true));
            }
            else if (e.Result.Reason == ResultReason.NoMatch)
            {
                SpeechRecognised?.Invoke(this, new EventArguments.RecognisedSpeechEventArgs("No speech could be recognized.", false));
            }
        }
    }
}
