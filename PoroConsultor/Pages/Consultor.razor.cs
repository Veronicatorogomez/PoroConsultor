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
    public partial class Consultor
    {
        private bool botonPulsado;
        private List<Campeones> personajes;
        private List<Estadisticas> estadisticas;
        private List<Union> campeones = new List<Union>();
        private string nombre1 = string.Empty;
        private string nombre2 = string.Empty;
        private Union campeon1;
        private Union campeon2;
        private bool vs;
        private IJSObjectReference module;
        private string ganadorVida = string.Empty;
        private string ganadorMana = string.Empty;
        private string ganadorDano = string.Empty;
        private string ganadorArmadura = string.Empty;
        private string ganadorResistenciaMagica = string.Empty;
        private string ganadorAlcance = string.Empty;
        private string ganadorVelocidad = string.Empty;
        private string ganadorVelocidadDeAtaque = string.Empty;
        private ElementReference vida1;
        private ElementReference vida2;
        private ElementReference mana1;
        private ElementReference mana2;
        private ElementReference dano1;
        private ElementReference dano2;
        private ElementReference armadura1;
        private ElementReference armadura2;
        private ElementReference resistenciaMagica1;
        private ElementReference resistenciaMagica2;
        private ElementReference alcance1;
        private ElementReference alcance2;
        private ElementReference velocidad1;
        private ElementReference velocidad2;
        private ElementReference velocidadDeAtaque1;
        private ElementReference velocidadDeAtaque2;
        private int contador1 = 0;
        private int contador2 = 0;
        private Union ganador;
        protected override async Task OnInitializedAsync()
        {
            estadisticas = await Http.GetFromJsonAsync<List<Estadisticas>>("apis/estadisticas.json");
            personajes = await Http.GetFromJsonAsync<List<Campeones>>("apis/campeones.json");
            foreach (var item in personajes)
            {
                foreach (var item2 in estadisticas)
                {
                    if (item.Nombre == item2.Nombre.TrimStart(' ').ToUpper())
                    {
                        campeones.Add(new Union(item, item2));
                    }
                }
            }

            module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/Ranking.js");
        }

        private async Task QuitarAlerta()
        {
            await module.InvokeVoidAsync("quitarAlerta");
        }

        private async Task Remover1()
        {
            module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/Consultor.js");
            await module.InvokeVoidAsync("remover", vida1);
        }

        private async Task Remover2()
        {
            module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/Consultor.js");
            await module.InvokeVoidAsync("remover", vida2);
        }

        private async Task Remover3()
        {
            module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/Consultor.js");
            await module.InvokeVoidAsync("remover", mana1);
        }

        private async Task Remover4()
        {
            module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/Consultor.js");
            await module.InvokeVoidAsync("remover", mana2);
        }

        private async Task Remover5()
        {
            module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/Consultor.js");
            await module.InvokeVoidAsync("remover", dano1);
        }

        private async Task Remover6()
        {
            module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/Consultor.js");
            await module.InvokeVoidAsync("remover", dano2);
        }

        private async Task Remover7()
        {
            module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/Consultor.js");
            await module.InvokeVoidAsync("remover", armadura1);
        }

        private async Task Remover8()
        {
            module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/Consultor.js");
            await module.InvokeVoidAsync("remover", armadura2);
        }

        private async Task Remover9()
        {
            module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/Consultor.js");
            await module.InvokeVoidAsync("remover", resistenciaMagica1);
        }

        private async Task Remover10()
        {
            module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/Consultor.js");
            await module.InvokeVoidAsync("remover", resistenciaMagica2);
        }

        private async Task Remover11()
        {
            module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/Consultor.js");
            await module.InvokeVoidAsync("remover", alcance1);
        }

        private async Task Remover12()
        {
            module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/Consultor.js");
            await module.InvokeVoidAsync("remover", alcance2);
        }

        private async Task Remover13()
        {
            module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/Consultor.js");
            await module.InvokeVoidAsync("remover", velocidad1);
        }

        private async Task Remover14()
        {
            module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/Consultor.js");
            await module.InvokeVoidAsync("remover", velocidad2);
        }

        private async Task Remover15()
        {
            module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/Consultor.js");
            await module.InvokeVoidAsync("remover", velocidadDeAtaque1);
        }

        private async Task Remover16()
        {
            module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/Consultor.js");
            await module.InvokeVoidAsync("remover", velocidadDeAtaque2);
        }

        private async Task AnalizarDatos()
        {
            botonPulsado = true;
            vs = true;
            ganador = null;
            contador1 = 0;
            contador2 = 0;
            if (nombre1 != "" && nombre2 != "")
            {
                campeon1 = campeones.First(x => x.Nombre.Equals(nombre1));
                campeon2 = campeones.First(x => x.Nombre.Equals(nombre2));
                ganadorVida = ConvertirNum(campeon1.Vida, campeon2.Vida);
                ganadorMana = ConvertirNum(campeon1.Mana, campeon2.Mana);
                ganadorDano = ConvertirNum(campeon1.Dano, campeon2.Dano);
                ganadorArmadura = ConvertirNum(campeon1.Armadura, campeon2.Armadura);
                ganadorResistenciaMagica = ConvertirNum(campeon1.ResistenciaMagica, campeon2.ResistenciaMagica);
                ganadorAlcance = ConvertirNum(campeon1.Alcance, campeon2.Alcance);
                ganadorVelocidad = ConvertirNum(campeon1.Velocidad, campeon2.Velocidad);
                ganadorVelocidadDeAtaque = ConvertirNum(campeon1.VelocidadDeAtaque, campeon2.VelocidadDeAtaque);
                module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/Consultor.js");
                await module.InvokeVoidAsync("quitarClase");
                Comparar();
                Ganador();
            }
        }

        private void Comparar()
        {
            if (ganadorVida.Equals(campeon1.Vida))
            {
                Remover1();
                contador1++;
            }
            else if (ganadorVida.Equals(campeon2.Vida))
            {
                contador2++;
                Remover2();
            }

            if (ganadorMana.Equals(campeon1.Mana))
            {
                contador1++;
                Remover3();
            }
            else if (ganadorMana.Equals(campeon2.Mana))
            {
                contador2++;
                Remover4();
            }

            if (ganadorDano.Equals(campeon1.Dano))
            {
                contador1++;
                Remover5();
            }
            else if (ganadorDano.Equals(campeon2.Dano))
            {
                contador2++;
                Remover6();
            }

            if (ganadorArmadura.Equals(campeon1.Armadura))
            {
                contador1++;
                Remover7();
            }
            else if (ganadorArmadura.Equals(campeon2.Armadura))
            {
                contador2++;
                Remover8();
            }

            if (ganadorResistenciaMagica.Equals(campeon1.ResistenciaMagica))
            {
                contador1++;
                Remover9();
            }
            else if (ganadorResistenciaMagica.Equals(campeon2.ResistenciaMagica))
            {
                contador2++;
                Remover10();
            }

            if (ganadorAlcance.Equals(campeon1.Alcance))
            {
                contador1++;
                Remover11();
            }
            else if (ganadorAlcance.Equals(campeon2.Alcance))
            {
                contador2++;
                Remover12();
            }

            if (ganadorVelocidad.Equals(campeon1.Velocidad))
            {
                contador1++;
                Remover13();
            }
            else if (ganadorVelocidad.Equals(campeon2.Velocidad))
            {
                contador2++;
                Remover14();
            }

            if (ganadorVelocidadDeAtaque.Equals(campeon1.VelocidadDeAtaque))
            {
                contador1++;
                Remover15();
            }
            else if (ganadorVelocidadDeAtaque.Equals(campeon2.VelocidadDeAtaque))
            {
                contador2++;
                Remover16();
            }
        }

        private void Ganador()
        {
            if (contador1 > contador2)
            {
                Console.WriteLine("Caca1");
                ganador = campeon1;
            }
            else if (contador2 > contador1)
            {
                Console.WriteLine("Caca2");
                ganador = campeon2;
            }
        }

        private string ConvertirNum(string dato, string dato2)
        {
            int a = int.Parse(dato);
            int b = int.Parse(dato2);
            int c = -1; //ganador
            if (a > b)
            {
                c = a;
            }
            else if (b > a)
            {
                c = b;
            }

            return c.ToString();
        }

        private void Redirigir()
        {
            NavigationManager.NavigateTo($"/campeon/{ganador.Nombre}");
        }
    }
}