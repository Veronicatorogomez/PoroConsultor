using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using PoroConsultor;
using PoroConsultor.Shared;
using PoroConsultor.Models;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components.Rendering;

namespace PoroConsultor.Pages
{
    public partial class Ranking
    {
        private List<string> opciones = new List<string>
        {
            "Todos",
            "Asesinos",
            "Luchadores",
            "Magos",
            "Tiradores",
            "Tanques"
        };
        private string opcionSeleccionada = string.Empty;
        List<string> stats = new List<string>
        {
            "Vida",
            "Daño",
            "Armadura",
            "Velocidad",
            "Alcance",
            "Velocidad de Ataque"
        };
        private string statSeleccionada = string.Empty;
        private List<Estadisticas> campeonesStats;
        private List<Campeones> campeonesInfo;
        private List<Union> campeones = new List<Union>();
        private List<List<int>> filtradosValor;
        private List<List<Union>> filtrados;
        private bool botonPulsado;
        private IJSObjectReference module;
        protected override async Task OnInitializedAsync()
        {
            campeonesStats = await Http.GetFromJsonAsync<List<Estadisticas>>("apis/estadisticas.json");
            campeonesInfo = await Http.GetFromJsonAsync<List<Campeones>>("apis/campeones.json");
            foreach (var item in campeonesInfo)
            {
                foreach (var item2 in campeonesStats)
                {
                    if (item.Nombre == item2.Nombre.TrimStart(' ').ToUpper())
                    {
                        campeones.Add(new Union(item, item2));
                    }
                }
            }
        }

        private void NavegaACampeon(string campeon)
        {
            NavigationManager.NavigateTo($"/campeon/{campeon}");
        }

        private async Task QuitarAlerta()
        {
            module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/Ranking.js");
            await module.InvokeVoidAsync("quitarAlerta");
        }

        private void Filtrar()
        {
            botonPulsado = true;
            filtrados = new List<List<Union>>();
            filtradosValor = new List<List<int>>();
            List<Union> campeonesFiltro = new List<Union>();
            switch (opcionSeleccionada)
            {
                case "Todos":
                    campeonesFiltro = campeones;
                    break;
                case "Asesinos":
                    campeonesFiltro = campeones.Where(champ => champ.Rol.Equals("ASESINO")).ToList();
                    break;
                case "Luchadores":
                    campeonesFiltro = campeones.Where(champ => champ.Rol == "LUCHADOR").ToList();
                    break;
                case "Magos":
                    campeonesFiltro = campeones.Where(champ => champ.Rol == "MAGO").ToList();
                    break;
                case "Tiradores":
                    campeonesFiltro = campeones.Where(champ => champ.Rol == "TIRADOR").ToList();
                    break;
                case "Tanques":
                    campeonesFiltro = campeones.Where(champ => champ.Rol == "TANQUE").ToList();
                    break;
            }

            switch (statSeleccionada)
            {
                case "Vida":
                    campeonesFiltro.Sort((a, b) => int.Parse(b.Vida).CompareTo(int.Parse(a.Vida)));
                    filtrados.Add(campeonesFiltro.Take(10).ToList());
                    filtradosValor.Add(campeonesFiltro.Take(10).Select(x => int.Parse(x.Vida)).ToList());
                    break;
                case "Daño":
                    campeonesFiltro.Sort((a, b) => int.Parse(b.Dano).CompareTo(int.Parse(a.Dano)));
                    filtrados.Add(campeonesFiltro.Take(10).ToList());
                    filtradosValor.Add(campeonesFiltro.Take(10).Select(x => int.Parse(x.Dano)).ToList());
                    break;
                case "Armadura":
                    campeonesFiltro.Sort((a, b) => int.Parse(b.Armadura).CompareTo(int.Parse(a.Armadura)));
                    filtrados.Add(campeonesFiltro.Take(10).ToList());
                    filtradosValor.Add(campeonesFiltro.Take(10).Select(x => int.Parse(x.Armadura)).ToList());
                    break;
                case "Velocidad":
                    campeonesFiltro.Sort((a, b) => int.Parse(b.Velocidad).CompareTo(int.Parse(a.Velocidad)));
                    filtrados.Add(campeonesFiltro.Take(10).ToList());
                    filtradosValor.Add(campeonesFiltro.Take(10).Select(x => int.Parse(x.Velocidad)).ToList());
                    break;
                case "Alcance":
                    campeonesFiltro.Sort((a, b) => int.Parse(b.Alcance).CompareTo(int.Parse(a.Alcance)));
                    filtrados.Add(campeonesFiltro.Take(10).ToList());
                    filtradosValor.Add(campeonesFiltro.Take(10).Select(x => int.Parse(x.Alcance)).ToList());
                    break;
                case "Velocidad de Ataque":
                    campeonesFiltro.Sort((a, b) => int.Parse(b.VelocidadDeAtaque).CompareTo(int.Parse(a.VelocidadDeAtaque)));
                    filtrados.Add(campeonesFiltro.Take(10).ToList());
                    filtradosValor.Add(campeonesFiltro.Take(10).Select(x => int.Parse(x.VelocidadDeAtaque)).ToList());
                    break;
            }
        }
    }
}