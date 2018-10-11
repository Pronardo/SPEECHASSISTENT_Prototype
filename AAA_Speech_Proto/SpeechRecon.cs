using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech;
using System.Speech.Recognition;

namespace AAA_Speech_Proto
{
    interface SpeechRecon
    {
        void StartRecognize(EventHandler<MySpeechEventArgs> speechRecognized);


    }
}
