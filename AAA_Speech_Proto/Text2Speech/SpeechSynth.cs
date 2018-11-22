using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace AAA_Speech_Proto.Text2Speech
{
    class SpeechSynth
    {
        static void ProGO()
        {
            using (var synthesizer = new SpeechSynthesizer())
            {
                synthesizer.Volume = 100;
                synthesizer.Rate = -2;
            }
        }
    }
}
