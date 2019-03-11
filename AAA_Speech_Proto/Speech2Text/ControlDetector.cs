using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AAA_Speech_Proto.Speech2Text
{
    static class ControlDetector
    {//List can be set, but does not affect operation
        public static List<UIElement> Controls { get; set; } = new List<UIElement>();
        public static List<UIElement> FindControls(Window window)
        {
            var panel = window.Content as Panel;
            Controls = new List<UIElement>();
            foreach (var control in panel.Children.OfType<FrameworkElement>())
            {
                Console.WriteLine($"   adding {control}");
                if (control is Panel) FindControls(control as Window);
                else Controls.Add(control);
            }
            return Controls;
        }
        public static void LogControls()
        {
            Console.WriteLine("ControlDector - current COntrols");
            Controls.ForEach(x => Console.WriteLine($"  Found {x}"));
        }
    }
}
