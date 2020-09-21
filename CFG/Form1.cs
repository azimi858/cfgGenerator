using System;
using System.Diagnostics;
using System.IO;
using System.Text;
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
			button6.Enabled = false;
			button4.Enabled = false;

		}

		private void button2_Click(object sender, EventArgs e)
		{
			try
			{
				folderBrowserDialog1.ShowDialog();
				cfgPath = folderBrowserDialog1.SelectedPath.ToString();
				label9.Text = $"{cfgPath}\\";

				button1.Enabled = true;
				button6.Enabled = true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
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
				//string savePath = "d:\\2\\sysCustomers.cfg";


				stream = new System.IO.StreamWriter(path: savePath + "sysCustomers.cfg", append: false, encoding: utf8WithoutBom);


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
				sw.Append($"orgURL \t \t =" + $" {t1}".PadRight(21) + $" # Domain's IP or URL{Environment.NewLine}");
				sw.Append($"orgName \t =" + $" {t2}".PadRight(21) + $" # Organization Name in Farsi{Environment.NewLine}");
				sw.Append($"orgShahab \t =" + $" {t3}".PadRight(21) + $" # Organization SHAHAB ID – 16 digits{Environment.NewLine}");
				sw.Append($"{Environment.NewLine}");
				sw.Append($"personName\t =" + $" {t4}".PadRight(21) + $" # System Admin. Name in Farsi{Environment.NewLine}");
				sw.Append($"personFamily =" + $" {t5}".PadRight(21) + $" # System Admin. Family (surname) in Farsi{Environment.NewLine}");
				sw.Append($"personShahab =" + $" {t6}".PadRight(21) + $" # System Admin. SHAHAB ID - 16 digits{Environment.NewLine}");
				sw.Append($"personEmail\t =" + $" {t7}".PadRight(21) + $" # System Admin. Email Address{Environment.NewLine}");
				sw.Append($"{Environment.NewLine}");
				sw.Append($"{Environment.NewLine}");
				sw.Append($"###############################{Environment.NewLine}");
				sw.Append($"## DO NOT CHANGE LINES BELOW ##{Environment.NewLine}");
				sw.Append($"###############################{Environment.NewLine}");
				sw.Append($"{Environment.NewLine}");
				sw.Append($"[ req ]{Environment.NewLine}");
				sw.Append($"default_bits ".PadRight(19) + $"\t= 2048{Environment.NewLine}");
				sw.Append($"default_md ".PadRight(19) + $"\t= sha256{Environment.NewLine}");
				sw.Append($"prompt ".PadRight(19) + $"\t= no{Environment.NewLine}");
				sw.Append($"encrypt_key ".PadRight(20) + $"= yes{Environment.NewLine}");
				sw.Append($"distinguished_name ".PadRight(19) + $"\t= req_distinguished_name{Environment.NewLine}");
				sw.Append($"req_extensions ".PadRight(19) + $"\t= req_ext{Environment.NewLine}");
				sw.Append($"{Environment.NewLine}");
				sw.Append($"[ req_distinguished_name ]{Environment.NewLine}");
				sw.Append($"countryName\t\t\t= \"ir\"{Environment.NewLine}");
				sw.Append($"organizationName\t= $ENV::orgName-$ENV::orgShahab{Environment.NewLine}");
				sw.Append($"commonName\t\t\t= $ENV::orgURL{Environment.NewLine}");
				sw.Append($"emailAddress\t\t= $ENV::personEmail{Environment.NewLine}");
				sw.Append($"givenName\t\t\t= $ENV::personName{Environment.NewLine}");
				sw.Append($"surname\t\t\t\t= $ENV::personFamily{Environment.NewLine}");
				sw.Append($"serialNumber\t\t= $ENV::personShahab{Environment.NewLine}");
				sw.Append($"{Environment.NewLine}");
				sw.Append($"[ req_ext ]{Environment.NewLine}");
				sw.Append($"subjectAltName \t\t= @alt_names{Environment.NewLine}");
				sw.Append($"{Environment.NewLine}");
				sw.Append($"[ alt_names ]{Environment.NewLine}");
				sw.Append($"DNS.1 = $ENV::orgURL");
				sw.Append($"{Environment.NewLine}");
				#endregion

				textBox9.Text = sw.ToString();

				stream.Write(sw.ToString());

				//stream.Write(Environment.NewLine);

				stream.Close();

				MessageBox.Show($"فایل با موفقیت ایجاد گردید ...{Environment.NewLine}{cfgPath}\\sysCustomers.cfg");

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
			string sysFile = string.Empty;
			if (radioButton1.Checked)
			{
				sysFile = "sysCustomers.cfg";
			}
			else
			{
				sysFile = "sysBankers.cfg";
			}
			if (File.Exists($"{cfgPath}\\{sysFile}"))
			{
				string opensslPathOnly = opensslPath.Remove(opensslPath.Length - 4);
				string opensslCommand = $"{opensslPathOnly} req -config {cfgPath}\\{sysFile} -newkey rsa:2048 -keyout \"{cfgPath}\\keyPrivate.key\" -out \"{cfgPath}\\certRequest.csr\" -utf8";
				string cmdCommand = $"/k {opensslCommand}";

				Process myProc = Process.Start("CMD.exe", cmdCommand);
			}
			else
			{
				MessageBox.Show($" یافت نشد " + $" {cfgPath}\\{sysFile} " + $" فایل ");
			}


		}



		//Thread.Sleep(5000);
		//myProc.CloseMainWindow();
		//this.Close();

		private void button5_Click(object sender, EventArgs e)
		{
			ExtractResource("PFX." + "help1.exe");
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

		private void button6_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(textBox8.Text) ||
				string.IsNullOrEmpty(textBox11.Text) ||
				string.IsNullOrEmpty(textBox10.Text) ||
				string.IsNullOrEmpty(textBox12.Text) ||
				string.IsNullOrEmpty(textBox13.Text) ||
				string.IsNullOrEmpty(textBox14.Text) ||
				string.IsNullOrEmpty(textBox15.Text)
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
				//string savePath = "d:\\2\\sysBankers.cfg";


				stream = new System.IO.StreamWriter(path: savePath + "sysBankers.cfg", append: false, encoding: utf8WithoutBom);


				string t21 = textBox8.Text;
				string t22 = textBox11.Text;
				string t23 = textBox10.Text;
				string t24 = textBox12.Text;
				string t25 = textBox13.Text;
				string t26 = textBox14.Text;
				string t27 = textBox15.Text;


				StringBuilder sw = new StringBuilder();
				#region [ body ]
				sw.Append($"#############################################{Environment.NewLine}");
				sw.Append($"## BANKERS SYSTEM CERTIFICATE SUBJECT INFO ##{Environment.NewLine}");
				sw.Append($"#############################################{Environment.NewLine}");
				sw.Append($"{Environment.NewLine}");
				sw.Append($"bankURL \t =" + $" {t21}".PadRight(21) + $" # Domain's IP or URL{Environment.NewLine}");
				sw.Append($"bankName \t =" + $" {t22}".PadRight(21) + $" # Bank Name in Farsi{Environment.NewLine}");
				sw.Append($"bankBIC \t =" + $" {t23}".PadRight(21) + $" # Bank BIC (Swift Code) - 11 characters{Environment.NewLine}");
				sw.Append($"{Environment.NewLine}");
				sw.Append($"personName\t =" + $" {t24}".PadRight(21) + $" # System Admin. Name in Farsi{Environment.NewLine}");
				sw.Append($"personFamily =" + $" {t25}".PadRight(21) + $" # System Admin. Family (surname) in Farsi{Environment.NewLine}");
				sw.Append($"personShahab =" + $" {t26}".PadRight(21) + $" # System Admin. SHAHAB ID - 16 digits{Environment.NewLine}");
				sw.Append($"personEmail\t =" + $" {t27}".PadRight(21) + $" # System Admin. Email Address{Environment.NewLine}");
				sw.Append($"{Environment.NewLine}");
				sw.Append($"{Environment.NewLine}");
				sw.Append($"###############################{Environment.NewLine}");
				sw.Append($"## DO NOT CHANGE LINES BELOW ##{Environment.NewLine}");
				sw.Append($"###############################{Environment.NewLine}");
				sw.Append($"{Environment.NewLine}");
				sw.Append($"[ req ]{Environment.NewLine}");
				sw.Append($"default_bits ".PadRight(19) + $"\t= 2048{Environment.NewLine}");
				sw.Append($"default_md ".PadRight(19) + $"\t= sha256{Environment.NewLine}");
				sw.Append($"prompt ".PadRight(19) + $"\t= no{Environment.NewLine}");
				sw.Append($"encrypt_key ".PadRight(20) + $"= yes{Environment.NewLine}");
				sw.Append($"distinguished_name ".PadRight(19) + $"\t= req_distinguished_name{Environment.NewLine}");
				sw.Append($"req_extensions ".PadRight(19) + $"\t= req_ext{Environment.NewLine}");
				sw.Append($"{Environment.NewLine}");
				sw.Append($"[ req_distinguished_name ]{Environment.NewLine}");
				sw.Append($"countryName\t\t\t= \"ir\"{Environment.NewLine}");
				sw.Append($"organizationName\t= $ENV::bankName-$ENV::bankBIC{Environment.NewLine}");
				sw.Append($"commonName\t\t\t= $ENV::bankURL{Environment.NewLine}");
				sw.Append($"emailAddress\t\t= $ENV::personEmail{Environment.NewLine}");
				sw.Append($"givenName\t\t\t= $ENV::personName{Environment.NewLine}");
				sw.Append($"surname\t\t\t\t= $ENV::personFamily{Environment.NewLine}");
				sw.Append($"serialNumber\t\t= $ENV::personShahab{Environment.NewLine}");
				sw.Append($"{Environment.NewLine}");
				sw.Append($"[ req_ext ]{Environment.NewLine}");
				sw.Append($"subjectAltName \t\t= @alt_names{Environment.NewLine}");
				sw.Append($"{Environment.NewLine}");
				sw.Append($"[ alt_names ]{Environment.NewLine}");
				sw.Append($"DNS.1 = $ENV::bankURL");
				sw.Append($"{Environment.NewLine}");
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

		private void Form1_FormClosed(object sender, FormClosedEventArgs e)
		{
			CFG.MainForm mainform = new MainForm();
			mainform.Show();
		}
	}
}