using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAA_Speech_Proto.Speech2Text
{
  internal class SpeechCommand
  {
    public string ControlName { get; set; }
    public string PropertyName { get; set; }
    public string Value { get; set; }

    public override string ToString() => $"SpeechCommand {ControlName}.{PropertyName} --> {Value}";
  }
}
