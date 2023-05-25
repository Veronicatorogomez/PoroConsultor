using static System.Net.Mime.MediaTypeNames;
using System.Text.Json;
using Microsoft.Playwright;
using System.ComponentModel.DataAnnotations.Schema;

namespace CogerDatos;
internal class Program
{
	static async Task Main(string[] args)
	{
        //Microsoft.Playwright.Program.Main(new[] { "install" });

        using var playwright = await Playwright.CreateAsync();
		await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions() { Headless = false });
		var page = await browser.NewPageAsync();
		await page.GotoAsync("https://leagueoflegends.fandom.com/wiki/List_of_champions/Base_statistics");
        ILocator locator = page.Locator(".NN0_TB_DIsNmMHgJWgT7U");//quita las cookies
		await locator.WaitForAsync(new() { Timeout = 3000 });
		await locator.ClickAsync();

        IReadOnlyList<IElementHandle> elementoDatos = await page.QuerySelectorAllAsync(".table-wide tr > td");
		List<Estadisticas> personajes = new List<Estadisticas>();

		for (int i = 0; i < elementoDatos.Count; i+=19)
		{
			string nombre = await elementoDatos[i].InnerTextAsync();
			string vida = await elementoDatos[i+1].InnerTextAsync();
			string mana = await elementoDatos[i+5].InnerTextAsync();
			string dano = await elementoDatos[i+9].InnerTextAsync();
			string velocidadDeAtaque = await elementoDatos[i+11].InnerTextAsync();
			string armadura = await elementoDatos[i+13].InnerTextAsync();
			string resistenciaMagica = await elementoDatos[i+15].InnerTextAsync();
			string velocidad = await elementoDatos[i+17].InnerTextAsync();
			string alcance = await elementoDatos[i+18].InnerTextAsync();
			personajes.Add(new Estadisticas()
			{
				Nombre = nombre,
				Vida = vida,
				Mana = mana,
				Dano = dano,
				Armadura = armadura,
				ResistenciaMagica = resistenciaMagica,
				Alcance = alcance,
				Velocidad = velocidad,
				VelocidadDeAtaque = double.Parse(velocidadDeAtaque).ToString().PadRight(3, '0'),
			});

		}

		using FileStream fileStream = File.Open("../../../../../../BlazorWeb/PoroConsultor/wwwroot/apis/estadisticas.json", FileMode.Create);
		JsonSerializer.Serialize(fileStream, personajes);

	}
}
