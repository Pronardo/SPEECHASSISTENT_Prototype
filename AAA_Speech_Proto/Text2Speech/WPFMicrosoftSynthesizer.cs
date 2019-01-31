using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace AAA_Speech_Proto.Text2Speech
{
    class WPFMicrosoftSynthesizer : SpeechSynth, IObserver<UIElement>
    {
        private SpeechSynthesizer synthesizer;
        //Tasks:
        //Debounce to MouseMoveEvent
        //Compare e.Source to Mappings
        //Get Mapped Property of recieved element
        //Give String to TTS Engine
        //WPF Functionality must be outsourced to SynthProcessor resp. WPFSynthProcessor
        public int DebounceTimer { get; set; } = 1000; //milliseconds
        private Dictionary<string, string> SpeechMappings = new Dictionary<string, string>();
        public UIElement ObservedElement { get; set; }
        public WPFMicrosoftSynthesizer(UIElement element)
        {
            Init(element);
        }
        public void Init(UIElement element)
        {
            SeedMappings();
            InitSynthesizer();
            ObserveMouse(element);
            ObservedElement = element;
        }

        private void InitSynthesizer()
        {
            synthesizer = new SpeechSynthesizer
            {
                Volume = 100,
                Rate = -2
            };
            synthesizer.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult, 0, CultureInfo.GetCultureInfo("en-US"));
        }

        private void SeedMappings()
        {
            SpeechMappings.Add("Button", "Content");
            SpeechMappings.Add("Label", "Content");
            SpeechMappings.Add("TextBox", "Text");
        }
        public void SynthesizeInput(string input)
        {
            if (String.IsNullOrEmpty(input)) { Console.WriteLine("Synthesize Failed input string null or empty"); return; }
            Console.WriteLine($"synthesize input {input}");
            //using (var synthesizer = new SpeechSynthesizer())
            //{
            //    synthesizer.Volume = 100;
            //    synthesizer.Rate = -2;
            //synthesizer.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult, 0, CultureInfo.GetCultureInfo("en-US"));
            synthesizer.Speak(input);
            //}
        }

        public void ObserveMouse(UIElement element)
        {
            var mouseMove = Observable
                .FromEventPattern<MouseEventArgs>(element, "MouseMove")
                .Select(x => x.EventArgs.Source as UIElement)
                .Where(x => x != null)
                .Sample(TimeSpan.FromMilliseconds(DebounceTimer))
            //   .Subscribe(
            //        next => Process(next),
            //        error => LogError(error)
            //);
            .Subscribe(this);
        }

        public void OnNext(UIElement element)
        {
            Type type = element.GetType();
            string eletype = type.Name;
            //eletype = "Button";
            if (SpeechMappings.ContainsKey(eletype))
            {
                Console.WriteLine($"Evaluate speech output for {eletype}");
                var prop = SpeechMappings[eletype];


                Console.WriteLine($"Current Synth Thread : {Thread.CurrentThread.Name} associated with {Thread.CurrentThread.ManagedThreadId}");
                string propertyvalue = "";
                Application.Current.Dispatcher.Invoke(
                    new Action(() =>
                    {
                        PropertyInfo property = type.GetProperty(prop);
                        Console.WriteLine($"   reflection to property: {property}");
                        propertyvalue = property.GetValue(element).ToString();
                        Console.WriteLine($"   reflection to proeprty value: {propertyvalue}");
                    }));

                //PropertyInfo property = type.GetProperty(prop);
                //Console.WriteLine($"{property.GetValue(target).ToString()}");
                //string propertyvalue = property.GetValue(target).ToString();
                //Console.WriteLine($"propertyvalue outcome: {propertyvalue}");
                SynthesizeInput(propertyvalue);
            }
            else
            {
                Console.WriteLine($"-- Element {eletype} is not mapped with speech settings");
            }
        }

        public void OnError(Exception error)
        {
            Console.WriteLine("***** An error occured while processing", error.Message);
        }

        public void OnCompleted()
        {
            Console.WriteLine("==============================");
            Console.WriteLine("MouseMove-Observable completed");
            Console.WriteLine("==============================");
        }
    }
}
