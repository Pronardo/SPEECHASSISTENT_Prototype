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
        protected List<UIElement> controls = new List<UIElement>();

        //public WPFSpeechProcessor(Window window) : base(window) { }
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
            foreach (var control in panel.Children.OfType<UIElement>())
            {
                Console.WriteLine($"adding {control}");
                if (control is Panel) FindControls(control as Panel);
                else controls.Add(control);
            }
        }

        internal override void DoProcess(SpeechCommand command) //public void process(string context, string input)
        {

            //Assembly assembly = Assembly.GetExecutingAssembly(); // Assembly mscorlib = typeof(string).Assembly;
            //Type[] types = assembly.GetTypes();
            //var windows = types.ToList()
            //    .Where(current => current.BaseType == typeof(Window));
            //var window = windows.First();
            Console.WriteLine("");

            //controls.Where(x.Tag => command.ControlName);
        }
    }
}
