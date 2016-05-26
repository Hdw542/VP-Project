using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.IO;
namespace VP
{
    public partial class TY : Form
    {

        SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();

        SpeechSynthesizer spSynthesizer = new SpeechSynthesizer();

        public TY()
        {
            InitializeComponent();  
            Choices commands = new Choices();
            var lines = System.IO.File.ReadAllLines(@"D:\Hadi.txt");
            commands.Add(lines);
            GrammarBuilder gramBuilder = new GrammarBuilder();
            gramBuilder.Append(commands);
            
            Grammar grammar = new Grammar(gramBuilder);
            recEngine.LoadGrammarAsync(grammar);
            recEngine.SetInputToDefaultAudioDevice();
            recEngine.SpeechRecognized += recEngine_SpeechRecognized;
        }

        private void btnEnable_Click(object sender, EventArgs e)
        {
            recEngine.RecognizeAsync(RecognizeMode.Multiple);
            btnDisable.Enabled = true;
            btnEnable.Enabled = false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        public static string[] ReadFile()
        {
            string[] lines = File.ReadAllLines(@"D:\Hadi.txt", Encoding.UTF8);
            return lines;
        }

        void recEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string[] Data = ReadFile();
            string question = "";
            string answer = "";
            for (int i = 0; i < Data.Length; i++)
            {
                if (Data[i]==e.Result.Text)
                {
                    question = Data[i];
                    answer = Data[i + 1];
                    spSynthesizer.SpeakAsync(answer);
                }
            }
           
                    
              
        }

        private void btnDisable_Click(object sender, EventArgs e)
        {
            recEngine.RecognizeAsyncStop();
            btnDisable.Enabled = false;
            btnEnable.Enabled = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string[] lines = File.ReadAllLines(@"D:\Hadi.txt", Encoding.UTF8);
           
        }

        private void TY_Load(object sender, EventArgs e)
        {

        }

    }
}
