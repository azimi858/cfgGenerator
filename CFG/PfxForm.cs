using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;


namespace CFG
{
	public partial class PfxForm : Form
	{
		public PfxForm()
		{
			InitializeComponent();
		}

		string pfxPath = string.Empty;

		string keyPath = string.Empty;
		string cerPath = string.Empty;
		string derPath = string.Empty;
		string trustchainPath = string.Empty;

		string keyName = string.Empty;
		string cerName = string.Empty;
		string trustchainName = string.Empty;

		string opensslPath = string.Empty;

		private void PfxForm_Load(object sender, EventArgs e)
		{
			button4.Enabled = false;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			try
			{
				folderBrowserDialog1.ShowDialog();
				pfxPath = folderBrowserDialog1.SelectedPath.ToString();
				label9.Text = $"{pfxPath}\\";

				button2.BackColor = System.Drawing.Color.LightGreen;
			}
			catch (Exception)
			{
				MessageBox.Show("خطای سیستمی");
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
				OpenFileDialog dlg = new OpenFileDialog();
				dlg.Filter = "|*.key";
				dlg.FileName = "*.key";
				dlg.ShowDialog();

				keyPath = dlg.FileName;

				button1.BackColor = System.Drawing.Color.LightGreen;
			}
			catch (Exception)
			{
				MessageBox.Show("خطای سیستمی");
			}
		}

		private void button6_Click(object sender, EventArgs e)
		{
			try
			{
				OpenFileDialog dlg = new OpenFileDialog();
				dlg.Filter = "|*.cer";
				dlg.FileName = "*.cer";
				dlg.ShowDialog();

				cerPath = dlg.FileName;
				string cerFileName = dlg.SafeFileName;
				string cerFileNameOnly = cerFileName.Remove(cerFileName.Length - 4);

				X509Certificate2 cert = new X509Certificate2(cerPath);

				string base64Result = ExportToBase642(cert);
				int base64ResultLen = (base64Result.Length)-2;
				int indexCount = base64ResultLen / 64;

				StringBuilder builder2 = new StringBuilder();
				builder2.AppendLine("-----BEGIN CERTIFICATE-----");

				for (int i = 0; i < indexCount; i++)
				{
					string a = string.Empty;

					a = base64Result.Substring(64 * i, 64);
					builder2.AppendLine(a);
				}

				int base64ResultLine = 64 * indexCount;

				int base64ResultRest = base64ResultLen - base64ResultLine;

				if (base64ResultRest > 0)
				{
					string a = string.Empty;

					a = base64Result.Substring(64 * indexCount, base64ResultRest);
					builder2.AppendLine(a);
				}
				builder2.AppendLine("-----END CERTIFICATE-----");

				File.WriteAllText($"{pfxPath}\\{cerFileNameOnly}_base64.cer", builder2.ToString());
				derPath = $"{pfxPath}\\{cerFileNameOnly}_base64.cer";

				button6.BackColor = System.Drawing.Color.LightGreen;
			}
			catch (Exception)
			{
				MessageBox.Show("خطای سیستمی");
			}
		}

		public static string ExportToBase642(X509Certificate cert)
		{
			StringBuilder builder = new StringBuilder();
			try
			{
				builder.AppendLine(Convert.ToBase64String(cert.Export(X509ContentType.Cert), Base64FormattingOptions.None));
			}
			catch (Exception)
			{
				MessageBox.Show("خطای سیستمی");
			}
			return builder.ToString();
		}

		private void button7_Click(object sender, EventArgs e)
		{
			try
			{
				OpenFileDialog dlg = new OpenFileDialog();
				dlg.Filter = "|*.cer";
				dlg.FileName = "*.cer";
				dlg.ShowDialog();

				trustchainPath = dlg.FileName;

				button7.BackColor = System.Drawing.Color.LightGreen;
			}
			catch (Exception)
			{
				MessageBox.Show("خطای سیستمی");
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			try
			{
				OpenFileDialog dlg = new OpenFileDialog();
				dlg.Filter = "|openssl.exe";
				dlg.FileName = "openssl.exe";
				dlg.ShowDialog();

				opensslPath = dlg.FileName;
				label11.Text = opensslPath;

				button3.BackColor = System.Drawing.Color.LightGreen;
				button4.Enabled = true;
			}
			catch (Exception)
			{
				MessageBox.Show("خطای سیستمی");
			}
		}

		private void button4_Click(object sender, EventArgs e)
		{
			try
			{
				string opensslPathOnly = opensslPath.Remove(opensslPath.Length - 4);
				string opensslCommand = $"{opensslPathOnly} pkcs12 -export -out {pfxPath}\\orgCertBundle.pfx -inkey {keyPath} -in {derPath} -certfile {trustchainPath}";

				Process myProc = Process.Start("CMD.exe", "/k" + opensslCommand);
			}
			catch (Exception)
			{
				MessageBox.Show("خطای سیستمی");
			}
		}

		private void PfxForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				CFG.MainForm mainform = new MainForm();
				mainform.Show();
			}
			catch (Exception)
			{
				MessageBox.Show("خطای سیستمی");
			}
		}
	}
}
