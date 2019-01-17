using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace AAA_Speech_Proto.Text2Speech
{
    class MicrosoftSynthesizer : SpeechSynth
    {
        public void SynthesizeInput(string input)
        {
            using (var synthesizer = new SpeechSynthesizer())
            {
                synthesizer.Volume = 100;
                synthesizer.Rate = -2;
                synthesizer.Speak(input);
            }
        }

        public void ObserveMouse()
        {
        

        }
    }
}
