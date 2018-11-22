using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;

namespace AAA_Speech_Proto.Speech2Text
{
    class VeryFirstRecon : SpeechRecon
    {
        private EventHandler<MySpeechEventArgs> speechRecognized; //Response Delegate
        private bool isBusy;
        public void StartRecognize(EventHandler<MySpeechEventArgs> callback) //speechRecognized is callback handler
        {
            if (isBusy) return;
            isBusy = true;
            this.speechRecognized = callback; //Keep callback in mind
            using (var recognizer = new SpeechRecognitionEngine(new CultureInfo("en-US")))
            {
                Console.WriteLine("StartRecognition");
                recognizer.LoadGrammar(new DictationGrammar());
                recognizer.SpeechRecognized += Recognizer_SpeechRecognized; ; //Call Recognizer_SpeechRecognized when finished
                recognizer.SetInputToDefaultAudioDevice();
                recognizer.RecognizeAsync(RecognizeMode.Multiple);
            }
        }

        private void Recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Console.WriteLine("Callback Caller");
          string text=  e.Result.ToString(); //Translation from Recognizer event to own event
            speechRecognized(this, new MySpeechEventArgs { Text = text });
        }
    }
}
