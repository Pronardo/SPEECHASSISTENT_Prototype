using System.Speech.Synthesis;
using System.Windows;

namespace AAA_Speech_Proto.Text2Speech
{
    class SpeechSynth
    {
        Point MouseCoordinates;
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
