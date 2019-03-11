using Microsoft.Speech.Recognition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAA_Speech_Proto.Speech2Text
{
    class WPFGrammarSet
    {
        public WPFGrammarSet()
        {
            Choices ch_Tags = new Choices(); //Read in runtime?
            Choices ch_Properties = new Choices(); //Fixed Ones?
            Choices ch_Values = new Choices();//Any?
            GrammarBuilder baseSets = new GrammarBuilder();
            baseSets.Append($"{ch_Tags} ");
            baseSets.Append($"{ch_Properties} ");
            baseSets.Append($"{ch_Values}");
            Grammar commands = new Grammar(baseSets);
        }


    }
}
