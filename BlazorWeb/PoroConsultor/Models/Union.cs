namespace PoroConsultor.Models;
public class Union
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
    public string PorcentajeAD { get; set; }
    public string PorcentajeAP { get; set; }
    public string PorcentajeTD { get; set; }
    public string Vida { get; set; }
    public string Mana { get; set; }
    public string Dano { get; set; }
    public string Armadura { get; set; }
    public string ResistenciaMagica { get; set; }
    public string TipoEnergia { get; set; }
    public string VidaRegen { get; set; }
    public string ManaRegen { get; set; }
    public string Alcance { get; set; }
    public string ProbabilidadCritico { get; set; }
    public string Velocidad { get; set; }
    public string VelocidadDeAtaque { get; set; }

    public Union()
    {

    }
	public Union(Campeones campeon, Estadisticas stat)
	{
        Nombre = campeon.Nombre;
        Apodo = campeon.Apodo;
		ImagenPrincipal = campeon.ImagenPrincipal;
		Descripcion = campeon.Descripcion;
		Rol = campeon.Rol;
		Dificultad = campeon.Dificultad;
        ImagenHabilidad = campeon.ImagenHabilidad;
        TipoHabilidad = campeon.TipoHabilidad;
        NombreHabilidad = campeon.NombreHabilidad;
        DescripcionHabilidad = campeon.DescripcionHabilidad;
        VideoHabilidad = campeon.VideoHabilidad;
		IconoSkin = campeon.IconoSkin;
		NombreSkin = campeon.NombreSkin;
		ImagenSkin = campeon.ImagenSkin;
		PorcentajeAD = stat.PorcentajeAD;
        PorcentajeAP = stat.PorcentajeAP;
        PorcentajeTD = stat.PorcentajeTD;
        Vida = stat.Vida;
        Mana = stat.Mana;
        Dano = stat.Dano;
        Armadura = stat.Armadura;
		ResistenciaMagica = stat.ResistenciaMagica;
		TipoEnergia = stat.TipoEnergia;
		VidaRegen = stat.VidaRegen;
		ManaRegen = stat.ManaRegen;
		Alcance = stat.Alcance;
        ProbabilidadCritico = stat.ProbabilidadCritico;
        Velocidad = stat.Velocidad;
		VelocidadDeAtaque = stat.VelocidadDeAtaque;
	}
}
