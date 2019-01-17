using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AAA_Speech_Proto.Speech2Text
{
  public class WPFSpeechProcessor : SpeechProcessor
  {
    protected List<FrameworkElement> controls = new List<FrameworkElement>();

    public void Init(Window window)
    {
      isInitialized = true;
      var panel = window.Content as Panel;
      if (panel == null) return;
      FindControls(panel);
      controls.ForEach(x => Console.WriteLine($"  Found {x}"));
    }

    private void FindControls(Panel panel)
    {
      foreach (var control in panel.Children.OfType<FrameworkElement>())
      {
        Console.WriteLine($"   adding {control}");
        if (control is Panel) FindControls(control as Panel);
        else controls.Add(control);
      }
    }

    internal override void DoProcess(SpeechCommand command)
    {
      Console.WriteLine($"DoProcess {command}");
      var targets = controls.Where(x => x.Tag != null && x.Tag.ToString() == command.ControlName);
      Console.WriteLine($"   Found {targets.Count()} targets");
      if (!targets.Any()) return;
      var target = targets.First();
      PropertyInfo property = target.GetType().GetProperty(command.PropertyName);
      property.SetValue(target, command.Value);
      Console.WriteLine($"Changed {property} @ {target} to {property.GetValue(target)}");
    }

    internal override SpeechCommand ExtractCommand(string input)
    {
      Console.WriteLine($"---> ExtractCommand {input}");
      Match m = Regex.Match(input, @"(?<control>\w+)\s+(?<property>\w+)\s+(?<value>.+)");
      SpeechCommand command = new SpeechCommand
      {
        ControlName = m.Groups["control"].Value,
        PropertyName = m.Groups["property"].Value,
        Value = m.Groups["value"].Value
      };
      return command;
    }

  }
}
