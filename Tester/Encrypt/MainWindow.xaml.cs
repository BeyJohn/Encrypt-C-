using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace Encrypt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private static List<string> files;
        private static bool done;

        public MainWindow()
        {
            InitializeComponent();
            done = false;
            files = new List<string>();
            GetFiles();
            foreach(string f in files)
                cbItems.Items.Add(f);
        }

        private void GetFiles()
        {
            if(File.GetAttributes(AppDomain.CurrentDomain.BaseDirectory).ToString() == "Directory")
            {
                string[] f = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory);
                foreach(string h in f)
                {
                    if(File.GetAttributes(h).ToString() != "Directory")
                    {
                        //tbPrompt.Text += "\n" + h.Substring(AppDomain.CurrentDomain.BaseDirectory.ToString().Length) + ":" + File.GetAttributes(h).ToString();
                        files.Add(h.Substring(AppDomain.CurrentDomain.BaseDirectory.ToString().Length));
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(tbNewFile.Text != "" && cbItems.Text != "" && !done)
            {
                Start(AppDomain.CurrentDomain.BaseDirectory + cbItems.Text, AppDomain.CurrentDomain.BaseDirectory + tbNewFile.Text);
                done = true;
            }
        }

        private void Start(string inFile, string outFile)
        {

            File.WriteAllLines(outFile, Enc(File.ReadAllLines(inFile)));
            tbPrompt.Text = File.ReadAllText(outFile);
        }

        private string[] Enc(string[] g)
        {
            for (int o = 0; o < g.Length; o++)
            {
                g[o] = Enc(g[o]);
            }
                
            return g;
        }

        private string Enc(string g)
        {
            string h = "";
            for(int o = 0; o < g.Length; o++)
            {
                char[] c = g.ToCharArray();
                h += DecimalToBaseFour(c[o]);
                h = h.Replace('0', 'G').Replace('1', 'C').Replace('2', 'A').Replace('3', 'T');
            }
            return h;
        }

        public string DecimalToBaseFour(int decimalNumber)
        {
            const int BitsInInt = 32;
            const string Digits = "0123";

            int radix = 4;

            if (radix < 2 || radix > Digits.Length)
                throw new ArgumentException("The radix must be >= 2 and <= " + Digits.Length.ToString());

            if (decimalNumber == 0)
                return "0";

            int index = BitsInInt - 1;
            long currentNumber = Math.Abs(decimalNumber);
            char[] charArray = new char[BitsInInt];

            while (currentNumber != 0)
            {
                int remainder = (int)(currentNumber % radix);
                charArray[index--] = Digits[remainder];
                currentNumber = currentNumber / radix;
            }

            string result = new String(charArray, index + 1, BitsInInt - index - 1);
            if (decimalNumber < 0)
            {
                result = "-" + result;
            }

            return result;
        }

    }
}
