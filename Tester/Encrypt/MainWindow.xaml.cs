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
				char[] c = g.ToCharArray();
				h += DecimalToBaseFour(c[o]);
				h = h.Replace('0', 'G').Replace('1', 'C').Replace('2', 'A').Replace('3', 'T');
			}
			return h;
		}

		public string DecimalToBaseFour(char decimalNumber)
		{
			int index = 3;
			long currentNumber = Math.Abs(decimalNumber);
			char[] charArray = new char[4];

			while (index >= 0)
			{
				charArray[index--] = (char)(currentNumber % 4);
				currentNumber = currentNumber / 4;
			}

			return new String(charArray);
		}
	}
}