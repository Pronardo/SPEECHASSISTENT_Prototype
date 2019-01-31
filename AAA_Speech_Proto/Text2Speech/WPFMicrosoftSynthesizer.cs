﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;
using System.Speech.Synthesis;
using System.Threading;
using System.Windows;
using System.Windows.Input;

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
        public int DebounceDelay { get; set; } = 1000; //milliseconds
        private Dictionary<string, string> SpeechMappings = new Dictionary<string, string>();
        public WPFMicrosoftSynthesizer(UIElement element) => Init(element);

        #region ------------------------------------------------------ Initializers
        public void Init(UIElement element)
        {
            LogWithThread($"----- Initializing WPFMicrosoftSynthesizer with root {element.GetType().Name}");
            SeedMappings();
            InitSynthesizer();
            ObserveMouse(element);
        }

        private void InitSynthesizer()
        {
            LogWithThread($"----- Initializing SpeechSynthesizer");
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
        #endregion

        #region ------------------------------------------------------ Observable
        public void ObserveMouse(UIElement element)
        {
            LogWithThread($"----- ObserveMouse with delay {DebounceDelay}");
            var mouseMove = Observable
                .FromEventPattern<MouseEventArgs>(element, "MouseMove")
                .Select(x => x.EventArgs.Source as UIElement)
                .Where(x => x != null)
                .Sample(TimeSpan.FromMilliseconds(DebounceDelay))
                .Subscribe(this);
        }
        #endregion

        #region ------------------------------------------------------ Observer
        public void OnNext(UIElement element)
        {
            Type type = element.GetType();
            string eletype = type.Name;
            if (!SpeechMappings.ContainsKey(eletype))
            {
                LogWithThread($"-- Element {eletype} is not mapped with speech settings");
                return;
            }

            LogWithThread($"Try to speak contents of {eletype}");
            var prop = SpeechMappings[eletype];
            Application.Current.Dispatcher.Invoke(
                () => TalkInMainThread(element, type, prop)
                );
        }

        public void OnError(Exception error) => LogWithThread($"***** An error occured while processing {error.Message}");

        public void OnCompleted()
        {
            LogWithThread("==============================");
            LogWithThread("MouseMove-Observable completed");
            LogWithThread("==============================");
        }
        #endregion

        #region ------------------------------------------------------ Various
        private void TalkInMainThread(UIElement element, Type type, string prop)
        {
            var propertyInfo = type.GetProperty(prop);
            string text = propertyInfo.GetValue(element).ToString();
            LogWithThread($"   speak property {propertyInfo.Name} --> {text}");
            SynthesizeInput(text);
        }

        public void SynthesizeInput(string input)
        {
            if (String.IsNullOrEmpty(input)) { LogWithThread("Synthesize Failed input string null or empty"); return; }
            LogWithThread($"synthesize input <{input}>");
            synthesizer.Speak(input);
        }

        private void LogWithThread(string msg) => Console.WriteLine($"[Thread {Thread.CurrentThread.ManagedThreadId}]: {msg}");
        #endregion
    }
}
