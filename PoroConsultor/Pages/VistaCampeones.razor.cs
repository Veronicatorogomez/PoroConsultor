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
    public partial class VistaCampeones
    {
        List<Campeones> campeonesPaginados = new List<Campeones>();
        List<Campeones> campeonesPaginadosAlreves = new List<Campeones>();
        int numeroPagina = 1;
        int elementosPorPagina = 50;
        private bool orden;
        private List<Campeones> campeonesAlreves = new List<Campeones>();
        private List<Campeones> campeones;
        private string terminoBusqueda = "";
        private bool terminoBusquedaBool;
        List<Campeones> resultadosBusqueda = new List<Campeones>();
        private IJSObjectReference jsModule;
        protected override async Task OnInitializedAsync()
        {
            campeones = await Http.GetFromJsonAsync<List<Campeones>>("apis/campeones.json");
            await PaginarCampeones();
            await CrearListaAlreves();
            await PaginarCampeonesAlreves();
        }

        private void NavegaACampeon(string campeon)
        {
            NavigationManager.NavigateTo($"./campeon/{campeon}");
        }

        private async Task ScrollToTop()
        {
            jsModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/VistaCampeones.js");
            await jsModule.InvokeVoidAsync("scrollToTop");
        }

        private async Task GoDown()
        {
            jsModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/VistaCampeones.js");
            await jsModule.InvokeVoidAsync("scrollToBottom");
        }

        private void resetPagina()
        {
            numeroPagina = 1;
            PaginarCampeones();
            PaginarCampeonesAlreves();
        }

        private Task Buscar(ChangeEventArgs e)
        {
            terminoBusqueda = e.Value.ToString();
            resultadosBusqueda = campeones.Where(campeon => campeon.Nombre.Contains(terminoBusqueda, StringComparison.OrdinalIgnoreCase)).ToList();
            if (terminoBusqueda != "")
            {
                terminoBusquedaBool = true;
            }
            else
            {
                terminoBusquedaBool = false;
            }

            return Task.CompletedTask;
        }

        private Task CrearListaAlreves()
        {
            for (int i = campeones.Count - 1; i >= 0; i--)
            {
                campeonesAlreves.Add(campeones[i]);
            }

            return Task.CompletedTask;
        }

        private Task PaginarCampeones()
        {
            campeonesPaginados = campeones.Skip((numeroPagina - 1) * elementosPorPagina).Take(elementosPorPagina).ToList();
            return Task.CompletedTask;
        }

        private Task PaginarCampeonesAlreves()
        {
            campeonesPaginadosAlreves = campeonesAlreves.Skip((numeroPagina - 1) * elementosPorPagina).Take(elementosPorPagina).ToList();
            return Task.CompletedTask;
        }

        private void IrPaginaAnterior()
        {
            if (numeroPagina != 1)
            {
                numeroPagina--;
                PaginarCampeones();
                PaginarCampeonesAlreves();
                ScrollToTop();
            }
        }

        private void IrPaginaSiguiente()
        {
            if (numeroPagina != 4)
            {
                numeroPagina++;
                PaginarCampeones();
                PaginarCampeonesAlreves();
                ScrollToTop();
            }
        }
    }
}