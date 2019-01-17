using System.Speech.Synthesis;
using System.Windows;

namespace AAA_Speech_Proto.Text2Speech
{
    interface SpeechSynth
    {
        void SynthesizeInput(string input);
    }
}
