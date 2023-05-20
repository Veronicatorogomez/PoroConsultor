using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CogerDatos;
public class Campeones
{
	public string Nombre { get; set; }
	public string Apodo { get; set; }
	public string ImagenPrincipal { get; set; }
	public string Descripcion { get; set; }
	public string Rol { get; set; }
	public string Dificultad { get; set; }
	public List<string> ImagenHabilidad { get; set; }
	public List<string> TipoHabilidad { get; set; }
	public List<string> NombreHabilidad { get; set; }
	public List<string> DescripcionHabilidad { get; set; }
	public List<string> VideoHabilidad { get; set; }
	public List<string> IconoSkin { get; set; }
	public List<string> NombreSkin { get; set; }
	public List<string> ImagenSkin { get; set; }
	public Campeones() { }
}
