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
		#region Definiciones
		public string Ruta { get; set; }
		public string Archivo { get; set; }
		#endregion
		#region Metodos
		private void CargarInformacion()
		{
			FileInfo fi = new FileInfo(Path.Combine(Ruta, Archivo));
			try
			{
				if (fi.Exists)
				{
					using (StreamReader sr = File.OpenText(Path.Combine(Ruta, Archivo)))
					{
						Autorizacion m = JsonConvert.DeserializeObject<Autorizacion>(sr.ReadLine());

						string[] words = m.CodigoUnico.Split('-');


						if (words[0].Equals(Serial().ToUpper()))
						{
							label1.Text = Constantes.Day.activo.ToString();
							button1.Enabled = false;
						}
						else
						{
							label1.Text = Constantes.Day.duplicado.ToString();
						}
					}
				}
				else
				{
					label1.Text = Constantes.Day.prueba.ToString();
				}
			}
			catch (Exception Ex)
			{
				Console.WriteLine(Ex.ToString());
			}
		}
		public static string Serial()
		{
			try
			{
				string HDD = System.Environment.CurrentDirectory.Substring(0, 1);
				ManagementObject disk = new ManagementObject(""+Constantes.cadena0001+"" + HDD + ":\"");
				disk.Get();
				return disk[Constantes.cadena0002].ToString();
			}
			catch
			{
				return "NONE";
			}
		}
		#endregion
		#region Eventos
		private void Demo_Load(object sender, EventArgs e)
		{
			CargarInformacion();
		}


		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
				Autorizacion autorizacion = new Autorizacion();
				autorizacion.nombre = Constantes.cadena0004;
				autorizacion.CodigoUnico = Serial().ToString().ToUpper() + "-" + Guid.NewGuid().ToString().ToUpper();
				autorizacion.fecha = DateTime.Now;

				string json = JsonConvert.SerializeObject(autorizacion);

				// Create a new file   
				FileInfo fi = new FileInfo(Path.Combine(Ruta, Archivo));
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
		#endregion
		#region Constructor
		public Demo()
		{
			InitializeComponent();
			Archivo = Constantes.cadena0003;
			Ruta = Path.Combine(Constantes.cadena0006, Constantes.cadena0005);

			if (!Directory.Exists(Ruta))
			{
				Directory.CreateDirectory(Ruta);
			}
		}
		#endregion
	}
}
