using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AAA_Speech_Proto.Text2Speech
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        SpeechSynthesizer speechSynthesizerObj;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            speechSynthesizerObj = new SpeechSynthesizer();
            btnResume.IsEnabled = false;
            btnPause.IsEnabled = false;
            btnStop.IsEnabled = false;
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            if (speechSynthesizerObj != null)
            {
                //Gets the current speaking state of the SpeechSynthesizer object.   
                if (speechSynthesizerObj.State == SynthesizerState.Speaking)
                {
                    //Pauses the SpeechSynthesizer object.   
                    speechSynthesizerObj.Pause();
                    btnPause.IsEnabled = false;
                    btnPause.IsEnabled = false;
                }
            }
        }

        private void btnResume_Click(object sender, RoutedEventArgs e)
        {
            if (speechSynthesizerObj != null)
            {
                if (speechSynthesizerObj.State == SynthesizerState.Paused)
                {
                    //Resumes the SpeechSynthesizer object after it has been paused.   
                    speechSynthesizerObj.Resume();
                    btnResume.IsEnabled = false;
                    btnSpeak.IsEnabled = true;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            speechSynthesizerObj.Dispose();
            if (txtbox.Text != "")
            {
                speechSynthesizerObj = new SpeechSynthesizer();
                //Asynchronously speaks the contents present in RichTextBox1   
                speechSynthesizerObj.SpeakAsync(txtbox.Text);
                btnPause.IsEnabled = true;
                btnStop.IsEnabled = true;
            }
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            if (speechSynthesizerObj != null)
            {
                //Disposes the SpeechSynthesizer object   
                speechSynthesizerObj.Dispose();
                btnSpeak.IsEnabled = true;
                btnResume.IsEnabled = false;
                btnPause.IsEnabled = false;
                btnStop.IsEnabled = false;
            }
        }
    }
}
