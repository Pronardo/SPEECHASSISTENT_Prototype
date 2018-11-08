using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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

        private SpeechCommand ExtractCommand(string input)
        {
            Match m = Regex.Match(input, @"(?<control>\w+)\s+(?<property>\w+)\s+(?<value>\w+)");
            SpeechCommand command=new SpeechCommand();
            command.ControlName = m.Groups["control"].Value;
            command.PropertyName = m.Groups["property"].Value;
            command.Value = m.Groups["value"].Value;
            return command;
        }

 
    }
}
