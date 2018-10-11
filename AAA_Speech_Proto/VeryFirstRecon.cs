using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;

namespace AAA_Speech_Proto
{
    class VeryFirstRecon : SpeechRecon
    {
        private EventHandler<MySpeechEventArgs> speechRecognized;
        public void StartRecognize(EventHandler<MySpeechEventArgs> speechRecognized)
        {
            this.speechRecognized = speechRecognized;
            using (var recognizer = new SpeechRecognitionEngine(new CultureInfo("en-US")))
            {
                recognizer.LoadGrammar(new DictationGrammar());
                recognizer.SpeechRecognized += Recognizer_SpeechRecognized; ;
                recognizer.SetInputToDefaultAudioDevice();
                recognizer.RecognizeAsync(RecognizeMode.Multiple);
            }
        }

        private void Recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
          string text=  e.Result.ToString();
            speechRecognized(this, new MySpeechEventArgs { Text = text });
        }
    }
}
