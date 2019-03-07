using AAA_Speech_Proto.Speech2Text;
using AAA_Speech_Proto.Text2Speech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfText2Speech;

namespace AAA_Speech_Proto
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SpeechRecon speechRecon = new WindowsOSRecon();

        public double DebounceTimer { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnRecognize_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Button clicked");
            speechRecon.StartRecognize(SpeechResponse); //SpeechRecognized is the response method
        }

        private void SpeechResponse(object sender, MySpeechEventArgs e)
        {
            Console.WriteLine("Retrieved Response - interpreted data");
            lblText.Content = e.Text; //Write response data in label
            WPFSpeechProcessor processor = new WPFSpeechProcessor();
            processor.Init(this);
            processor.Process(e.Text); //-> DoProcess is internal & concrete
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine($"Current UI Thread : {Thread.CurrentThread.Name} associated with {Thread.CurrentThread.ManagedThreadId}");
            var synthesizer = WPFMicrosoftSynthesizer.OnlyOne;
            synthesizer.SetWindow(this);
            synthesizer.SetApplication(Application.Current);
            synthesizer.SynthesizeInput("Hello User");

        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {

            //string type = e.Source.GetType().Name;
            //string text = null;
            //if (type == "Label") text = (e.Source as Label).Content.ToString();
            //if (type == "Button") text = (e.Source as Button).Content.ToString();
            //if (type == "TextBox") text = (e.Source as TextBox).Text;
            //Console.WriteLine($"MouseMove in Thread {Thread.CurrentThread.ManagedThreadId}: {text}");
        }
    }
}
