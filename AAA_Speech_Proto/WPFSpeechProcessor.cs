using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AAA_Speech_Proto
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
            controls.ForEach(x => Console.WriteLine($"Found {x}"));
        }

        private void FindControls(Panel panel)
        {
            foreach (var control in panel.Children.OfType<FrameworkElement>())
            {
                Console.WriteLine($"adding {control}");
                if (control is Panel) FindControls(control as Panel);
                else controls.Add(control);
            }
        }

        internal override void DoProcess(SpeechCommand command)
        {
            FrameworkElement target=controls.Where(x => x.Tag.ToString()==command.ControlName).First();
            PropertyInfo property = target.GetType().GetProperty(command.PropertyName);
            property.SetValue(target, command.Value);
            Console.WriteLine($"Changed {property} @ {target} to {property.GetValue(target)}");
        }
    }
}
