using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CFG
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		string cfgPath = string.Empty;
		string opensslPath = string.Empty;

		private void Form1_Load(object sender, EventArgs e)
		{
			button3.Enabled = false;
			button1.Enabled = false;
			button4.Enabled = false;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			folderBrowserDialog1.ShowDialog();
			cfgPath = folderBrowserDialog1.SelectedPath.ToString();
			label9.Text = $"{cfgPath}\\sysBankers.cfg";

			button1.Enabled = true;
		}


		private void button3_Click(object sender, EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "|openssl.exe";
			dlg.FileName = "openssl.exe";

			dlg.ShowDialog();

			opensslPath = dlg.FileName;
			label11.Text = opensslPath;

			button4.Enabled = true;
		}

		private void button1_Click(object sender, EventArgs e)
		{

			if (string.IsNullOrEmpty(textBox1.Text) ||
				string.IsNullOrEmpty(textBox2.Text) ||
				string.IsNullOrEmpty(textBox3.Text) ||
				string.IsNullOrEmpty(textBox4.Text) ||
				string.IsNullOrEmpty(textBox5.Text) ||
				string.IsNullOrEmpty(textBox6.Text) ||
				string.IsNullOrEmpty(textBox7.Text)
				)
			{
				MessageBox.Show("همه ی فیلد ها اجباری میباشند");

				return;
			}

			System.IO.StreamWriter stream = null;

			try
			{
				Encoding utf8WithoutBom = new UTF8Encoding(false);

				string savePath = label9.Text;

				stream = new System.IO.StreamWriter(path: savePath, append: false, encoding: utf8WithoutBom);


				string t1 = textBox1.Text;
				string t2 = textBox2.Text;
				string t3 = textBox3.Text;
				string t4 = textBox4.Text;
				string t5 = textBox5.Text;
				string t6 = textBox6.Text;
				string t7 = textBox7.Text;


				StringBuilder sw = new StringBuilder();
				#region [ body ]
				sw.Append($"###############################################{Environment.NewLine}");
				sw.Append($"## CUSTOMERS SYSTEM CERTIFICATE SUBJECT INFO ##{Environment.NewLine}");
				sw.Append($"###############################################{Environment.NewLine}");
				sw.Append($"{Environment.NewLine}");
				sw.Append($"orgURL = {t1} # Domain's IP or URL{Environment.NewLine}");
				sw.Append($"orgName ={t2}{Environment.NewLine}");
				sw.Append($"orgShahab = {t3} # Organization SHAHAB ID – 16 digits{Environment.NewLine}");
				sw.Append($"{Environment.NewLine}");
				sw.Append($"personName = {t4} # System Admin. Name in Farsi{Environment.NewLine}");
				sw.Append($"personFamily = {t5} # System Admin. Family (surname) in Farsi{Environment.NewLine}");
				sw.Append($"personShahab = {t6} # System Admin. SHAHAB ID - 16 digits{Environment.NewLine}");
				sw.Append($"personEmail = {t7} # System Admin. Email Address{Environment.NewLine}");
				sw.Append($"{Environment.NewLine}");
				sw.Append($"{Environment.NewLine}");
				sw.Append($"###############################{Environment.NewLine}");
				sw.Append($"## DO NOT CHANGE LINES BELOW ##{Environment.NewLine}");
				sw.Append($"###############################{Environment.NewLine}");
				sw.Append($"[ req ]{Environment.NewLine}");
				sw.Append($"default_bits = 2048{Environment.NewLine}");
				sw.Append($"default_md = sha256{Environment.NewLine}");
				sw.Append($"prompt = no{Environment.NewLine}");
				sw.Append($"distinguished_name = req_distinguished_name{Environment.NewLine}");
				sw.Append($"req_extensions = req_ext{Environment.NewLine}");
				sw.Append($"{Environment.NewLine}");
				sw.Append($"[ req_distinguished_name ]{Environment.NewLine}");
				sw.Append($"countryName = \"ir\"{Environment.NewLine}");
				sw.Append($"organizationName = $ENV::orgName-$ENV::orgShahab{Environment.NewLine}");
				sw.Append($"commonName = $ENV::orgURL{Environment.NewLine}");
				sw.Append($"emailAddress = $ENV::personEmail{Environment.NewLine}");
				sw.Append($"givenName = $ENV::personName{Environment.NewLine}");
				sw.Append($"surname = $ENV::personFamily{Environment.NewLine}");
				sw.Append($"serialNumber = $ENV::personShahab{Environment.NewLine}");
				sw.Append($"{Environment.NewLine}");
				sw.Append($"[ req_ext ]{Environment.NewLine}");
				sw.Append($"subjectAltName = @alt_names{Environment.NewLine}");
				sw.Append($"{Environment.NewLine}");
				sw.Append($"[ alt_names ]{Environment.NewLine}");
				sw.Append($"DNS.1 = $ENV::orgURL");
				#endregion

				textBox9.Text = sw.ToString();

				stream.Write(sw.ToString());

				//stream.Write(Environment.NewLine);

				stream.Close();

				MessageBox.Show($"فایل با موفقیت ایجاد گردید ...{Environment.NewLine}{cfgPath}\\sysBankers.cfg");

			}
			catch (System.Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				if (stream != null)
				{
					stream.Dispose();
					stream = null;
				}
			}
			button3.Enabled = true;
		}


		static void LaunchCommandLineApp()
		{
			// For the example
			const string ex1 = "C:\\";
			const string ex2 = "C:\\Dir";

			// Use ProcessStartInfo class
			ProcessStartInfo startInfo = new ProcessStartInfo();
			startInfo.CreateNoWindow = false;
			startInfo.UseShellExecute = false;
			startInfo.FileName = "help.exe";
			startInfo.WindowStyle = ProcessWindowStyle.Hidden;
			startInfo.Arguments = "-f j -o \"" + ex1 + "\" -z 1.0 -s y " + ex2;

			try
			{
				// Start the process with the info we specified.
				// Call WaitForExit and then the using statement will close.
				using (Process exeProcess = Process.Start(startInfo))
				{
					exeProcess.WaitForExit();
				}
			}
			catch
			{
				// Log error.
			}
		}


		private void button4_Click(object sender, EventArgs e)
		{
			string opensslPathOnly = opensslPath.Remove(opensslPath.Length - 4);
			string opensslCommand = $"{opensslPathOnly} req -config {cfgPath}\\sysBankers.cfg -newkey rsa:2048 -keyout \"{cfgPath}\\keyPrivate.key\" -out \"{cfgPath}\\certRequest.csr\" -utf8";
			string cmdCommand = $"/k {opensslCommand}";

			Process myProc = Process.Start("CMD.exe", cmdCommand);
			//Thread.Sleep(5000);
			//myProc.CloseMainWindow();
			//this.Close();
		}

		private void button5_Click(object sender, EventArgs e)
		{
			ExtractResource("CFG." + "help1.exe");
		}


		public void ExtractResource(string resource)
		{
			string exePath = "help1.exe";

			Stream stream = GetType().Assembly.GetManifestResourceStream(resource);

			if (stream != null)
			{
				byte[] bytes = new byte[(int)stream.Length];
				stream.Read(bytes, 0, bytes.Length);
				File.WriteAllBytes(exePath, bytes);
				System.Diagnostics.Process.Start(exePath); 
			}
			
		}

	}
}
