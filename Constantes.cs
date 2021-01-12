using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licencia
{
	public class Constantes
	{
		public static string cadena0003 = "Autorizacion.json";
		public static string cadena0001 = "win32_logicaldisk.deviceid=\"";
		public static string cadena0002 = "VolumeSerialNumber";
		public static string cadena0004 = "Licencia";
		public static string cadena0005 = "Gema";
		public static string cadena0006 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData).ToString();
		public enum Day { activo='a' , duplicado='b', prueba='c'};
	}
}
