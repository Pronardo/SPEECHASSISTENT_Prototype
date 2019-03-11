using Microsoft.Speech.Recognition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AAA_Speech_Proto.Speech2Text
{
    class WPFGrammarSet
    {
        private Window currentWin;
        public Grammar Commands { get; set; }
        public void SetWindow(Window window)
        {
            currentWin = window;
            CreateWindowGrammar();
        }

        private void CreateWindowGrammar()
        {
            Choices ch_Tags = new Choices();//Read in runtime?
            Choices ch_Properties = new Choices(); //Fixed Ones?
            Choices ch_Values = new Choices();//Any?
            var controls=ControlDetector.FindControls(currentWin);
            foreach (var control in controls)
            {
                var identified=control as FrameworkElement;
                ch_Tags.Add($"{identified.Tag}");
                var properties = control.GetType().GetProperties();
                foreach (var property in properties)
                {
                    ch_Properties.Add($"{property.Name}");
                }
            }

 
            GrammarBuilder baseSets = new GrammarBuilder();
            baseSets.Append($"{ch_Tags} ");
            baseSets.Append($"{ch_Properties} ");
            baseSets.Append($"{ch_Values}");
            Commands = new Grammar(baseSets);
        }
    }
}
