using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Speech.Recognition;
using Microsoft.Speech.Synthesis;
using System.Globalization;

namespace AAA_Speech_Proto.Speech2Text
{
    class MicrosoftSDKRecon : SpeechRecon
    {
        public CultureInfo LangCulture { get; set; } = new CultureInfo("en-us");
        public Grammar[] Grammars { get; set; }



        private SpeechRecognitionEngine SRE;
        private EventHandler<MySpeechEventArgs> speechRecognized;

        public void StartRecognize(EventHandler<MySpeechEventArgs> callback)
        {
            if (Grammars == null) { Console.WriteLine("Microsoft's SDK Speech Engine needs grammars for operation"); return; }
            try
            {
                Console.WriteLine("StartRecognition");
                this.speechRecognized = callback; //Keep callback in mind
                InitRecongnition();
            }
            catch (InvalidOperationException exception)
            {
                string text = String.Format("Speech Recongnition Process Failed", exception.Source, exception.Message);
                speechRecognized(this, new MySpeechEventArgs { Text = text });
            }
        }

        private void InitRecongnition()
        {
            Console.WriteLine("InitRecognition");
            SRE = new SpeechRecognitionEngine(LangCulture);
            SRE.SetInputToDefaultAudioDevice();
            SRE.SpeechRecognized += Recognizer_SpeechRecognized;
            InitGrammars();
            SRE.RecognizeAsync(RecognizeMode.Multiple);
        }

        private void Recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string text = e.Result.Text;
            speechRecognized(this, new MySpeechEventArgs { Text = text });
        }

        public void InitGrammars()
        {
            //Choices->GrammarBuilder->Grammar->Load Grammar
            //Choices ch_StartStopCommands = new Choices();
            //ch_StartStopCommands.Add("speech on");
            //ch_StartStopCommands.Add("speech off");
            foreach (var grammar in Grammars)
            {
                Console.WriteLine($"{grammar.ToString()}");
                SRE.LoadGrammarAsync(grammar);
            }
        }
    }
}
