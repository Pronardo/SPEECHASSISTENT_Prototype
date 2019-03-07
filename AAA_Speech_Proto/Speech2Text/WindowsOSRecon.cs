using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;

namespace AAA_Speech_Proto.Speech2Text
{
  class WindowsOSRecon : SpeechRecon
  {
    private EventHandler<MySpeechEventArgs> speechRecognized; //Response Delegate
    private bool isBusy;
    public void StartRecognize(EventHandler<MySpeechEventArgs> callback) //speechRecognized is callback handler
    {
      //if (isBusy) return;
      //isBusy = true;
      this.speechRecognized = callback; //Keep callback in mind
      try
      {
        //using (var recognizer = new SpeechRecognitionEngine(new CultureInfo("en-US")))
        var recognizer = new SpeechRecognitionEngine();
        {
          Console.WriteLine("StartRecognition");
          recognizer.LoadGrammar(new DictationGrammar());
          recognizer.SpeechRecognized += Recognizer_SpeechRecognized; ; //Call Recognizer_SpeechRecognized when finished
          recognizer.SetInputToDefaultAudioDevice();
          //recognizer.RecognizeAsync(RecognizeMode.Multiple);
          recognizer.RecognizeAsync();
          //RecognitionResult result = recognizer.Recognize();
          //speechRecognized(this, new MySpeechEventArgs { Text = result.Text });
        }
      }
      catch (InvalidOperationException exception)
      {
        string text = String.Format("Could not recognize input from default aduio device. Is a microphone or sound card available?\r\n{0} - {1}.", exception.Source, exception.Message);
        speechRecognized(this, new MySpeechEventArgs { Text = text });
      }
      finally
      {
        //recognizer.UnloadAllGrammars();
      }
    }

    private void Recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
    {
      string text = e.Result.Text; //Translation from Recognizer event to own event
      Console.WriteLine("************ Callback Caller with <{text}>");
      speechRecognized(this, new MySpeechEventArgs { Text = text });
    }
  }
}
