using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
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

namespace AAA_Speech_Proto
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SpeechRecon speechRecon = new SpeechRecon();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnRecognize_Click(object sender, RoutedEventArgs e)
        {
            speechRecon.StartRecognize(SpeechRecognized);
        }

        private void SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            lblText.Content = e.Result.Text;
        }
    }
}
