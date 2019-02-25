using System.Speech.Synthesis;
using System.Windows;

namespace WpfText2Speech1
{
    interface SpeechSynth
    {
        void SynthesizeInput(string input);
    }
}
