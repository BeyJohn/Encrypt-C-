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

namespace Decrypt
{
    
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
            foreach (string f in files)
                cbItems.Items.Add(f);
        }

        private void GetFiles()
        {
            if (File.GetAttributes(AppDomain.CurrentDomain.BaseDirectory).ToString() == "Directory")
            {
                string[] f = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory);
                foreach (string h in f)
                {
                    if (File.GetAttributes(h).ToString() != "Directory")
                    {
                        //tbPrompt.Text += "\n" + h.Substring(AppDomain.CurrentDomain.BaseDirectory.ToString().Length) + ":" + File.GetAttributes(h).ToString();
                        files.Add(h.Substring(AppDomain.CurrentDomain.BaseDirectory.ToString().Length));
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (tbNewFile.Text != "" && cbItems.Text != "" && !done)
            {
                Start(AppDomain.CurrentDomain.BaseDirectory + cbItems.Text, AppDomain.CurrentDomain.BaseDirectory + tbNewFile.Text);
                done = true;
            }
        }

        private void Start(string inFile, string outFile)
        {

            File.WriteAllLines(outFile, Dec(File.ReadAllLines(inFile)));
            tbPrompt.Text = File.ReadAllText(outFile);
        }
        
        private string[] Dec(string[] g)
        {
            for (int o = 0; o < g.Length; o++)
            {
                g[o] = Dec(g[o]);
            }

            return g;
        }
        
        private string Dec(string g)
        {
            string h = "";
            g = g.Replace('G', '0').Replace('C', '1').Replace('A', '2').Replace('T', '3'); 
            while(g.Length > 0)
            {
                h += (char)BaseFourToDecimal(g.Substring(0, 4));
                g = g.Substring(4);
            }
            return h;
        }
        
        public int BaseFourToDecimal(string baseFour)
        {
            int y = 0;
            char[] c = baseFour.ToCharArray();
            for(int o = 3; o >= 0; o--)
            {
                y += (c[o] - 48) * (int)Math.Pow(4, 3 - o);
            }
            return y;
        }

    }

}
