using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Licencia
{
	public partial class Demo : Form
	{
		public string Ruta { get; set; }
		public string Archivo { get; set; }
		public Demo()
		{
			InitializeComponent();
			Archivo = "Autorizacion.json";
			Ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData).ToString(),"Gema");

			if (!Directory.Exists(Ruta))
			{
				Directory.CreateDirectory(Ruta);
			}

		}
		private void CargarInformacion()
		{
			FileInfo fi = new FileInfo(Path.Combine(Ruta,Archivo));
			try
			{
				if (fi.Exists)
				{
					using (StreamReader sr = File.OpenText(Path.Combine(Ruta,Archivo)))
					{
						Autorizacion m = JsonConvert.DeserializeObject<Autorizacion>(sr.ReadLine());
						if (m.CodigoUnico.Equals(Serial()))
						{
							label1.Text = "SERIAL ACTIVO";
							button1.Enabled = false;
						}
						else
						{
							label1.Text = "SERIAL DUPLICADA";
						}
					}
				}
				else
				{
					label1.Text = "SERIAL PRUEBA";
				}
			}
			catch (Exception Ex)
			{
				Console.WriteLine(Ex.ToString());
			}
		}
		private void Demo_Load(object sender, EventArgs e)
		{
			CargarInformacion();
		}
			public static string Serial()
			{
			try
			{
				string HDD = System.Environment.CurrentDirectory.Substring(0, 1);
				ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"" + HDD + ":\"");
				disk.Get();
				return disk["VolumeSerialNumber"].ToString();
			}
			catch 
			{
				return "NONE";
			}
			}

		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
				Autorizacion autorizacion = new Autorizacion();
				autorizacion.nombre = "Licencia";
				autorizacion.CodigoUnico = Serial();
				autorizacion.fecha = DateTime.Now;

				string json = JsonConvert.SerializeObject(autorizacion);

				// Create a new file   
				FileInfo fi = new FileInfo(Path.Combine(Ruta,Archivo));
				using (StreamWriter sw = fi.CreateText())
				{
					sw.WriteLine(json);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString());
			}
		}
	}
}
