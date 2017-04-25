using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace Decrypt
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
			while (g.Length > 0)
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
			for (int o = 3; o >= 0; o--)
			{
				y += (c[o] - 48) * (int)Math.Pow(4, 3 - o);
			}
			return y;
		}
	}
}