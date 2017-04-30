using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace Encrypt
{
	public partial class MainWindow : Window
	{
		private static List<string> files;
		private string loc;

		public MainWindow()
		{
			InitializeComponent();

			loc = AppDomain.CurrentDomain.BaseDirectory;
			files = new List<string>();

			GetFiles();
			foreach (string f in files)
				cbItems.Items.Add(f);
		}

		private void GetFiles()
		{
			DirectoryInfo d = new DirectoryInfo(loc);
			foreach (FileInfo h in d.GetFiles())
			{
				if (File.GetAttributes(h.Name).ToString() != "Directory")
				{
					files.Add(h.Name);
				}
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (tbNewFile.Text != "" && cbItems.Text != "")
			{
				Start(loc + cbItems.Text, loc + tbNewFile.Text);
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
			for (int o = 0; o < g.Length; o++)
			{
				h += DecimalToBaseFour(g[o]);
			}
			
			return h.Replace('0', 'G').Replace('1', 'C').Replace('2', 'A').Replace('3', 'T');
		}

		public string DecimalToBaseFour(int decimalNumber)
		{
			string newNum = "";

			while (decimalNumber > 0)
			{
				newNum = (decimalNumber % 4) + newNum;
				decimalNumber = decimalNumber / 4;
			}
			while (newNum.Length < 4)
			{
				newNum = 0 + newNum;
			}

			return newNum;
		}
	}
}