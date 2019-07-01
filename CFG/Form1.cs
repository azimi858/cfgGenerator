using System;
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


		private void button2_Click(object sender, EventArgs e)
		{
			folderBrowserDialog1.ShowDialog();
			string pathAddress = folderBrowserDialog1.SelectedPath.ToString();
			label9.Text = $"{pathAddress}\\sysBankers.cfg";
		}


		private void button3_Click(object sender, EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "|openssl.exe";
			dlg.FileName = "openssl.exe";



			dlg.ShowDialog();
			//openFileDialog1.ShowDialog();
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

				string pathAddress = label9.Text;

				stream = new System.IO.StreamWriter	(path: pathAddress, append: true, encoding: utf8WithoutBom);

				string t1 = textBox1.Text;
				string t2 = textBox2.Text;
				string t3 = textBox3.Text;
				string t4 = textBox4.Text;
				string t5 = textBox5.Text;
				string t6 = textBox6.Text;
				string t7 = textBox7.Text;


				StringBuilder sw = new StringBuilder();
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


				textBox9.Text = sw.ToString();
				stream.Write(sw.ToString());

				//stream.Write(Environment.NewLine);

				stream.Close();

				MessageBox.Show("فایل متنی با موفقیت ایجاد یا ذخیره گردید ...");

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
		}


	}
}
