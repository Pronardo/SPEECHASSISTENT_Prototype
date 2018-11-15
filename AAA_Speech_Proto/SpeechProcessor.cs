using System;

namespace AAA_Speech_Proto
{
    public abstract class SpeechProcessor
    {
        protected bool isInitialized = false;
        internal abstract void DoProcess(SpeechCommand command);

        public void Process(string input)
        {
            if (!isInitialized) throw new InvalidOperationException("Method Init(string) not called!");
            SpeechCommand command = ExtractCommand(input);
            DoProcess(command);
        }

        internal abstract SpeechCommand ExtractCommand(string input);

    }
}
