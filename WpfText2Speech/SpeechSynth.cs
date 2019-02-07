using System.Speech.Synthesis;
using System.Windows;

namespace WpfText2Speech
{
    interface SpeechSynth
    {
        void SynthesizeInput(string input);
    }
}
