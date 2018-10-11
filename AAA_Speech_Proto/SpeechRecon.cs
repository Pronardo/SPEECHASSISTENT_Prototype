using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech;
using System.Speech.Recognition;
using System.Globalization;

namespace AAA_Speech_Proto
{
    class SpeechRecon
    {
        public void StartRecognize(EventHandler<SpeechRecognizedEventArgs> speechRecognized)
        {

            using (var recognizer = new SpeechRecognitionEngine(new CultureInfo("en-US")))
            {
                recognizer.LoadGrammar(new DictationGrammar());
                recognizer.SpeechRecognized += speechRecognized;
                recognizer.SetInputToDefaultAudioDevice();
                recognizer.RecognizeAsync(RecognizeMode.Multiple);
            }
        }


    }
}
