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
		await page.GotoAsync("https://www.mobachampion.com/champion/");
        ILocator locator = page.Locator(".css-47sehv");//quita las cookies
		await locator.WaitForAsync(new() { Timeout = 3000 });
		await locator.ClickAsync();

        Thread.Sleep(1000);
        IReadOnlyList<IElementHandle> elementoPersonajesInicio = await page.QuerySelectorAllAsync(".text-xs.font-bold");
		List<Estadisticas> personajes = new List<Estadisticas>(elementoPersonajesInicio.Count);

		for (int i = 0; i < elementoPersonajesInicio.Count; i++)
		{
			await page.EvaluateAsync($"window.scrollTo(0, {i * 76 + 700})");
			Thread.Sleep(1000);
			IReadOnlyList<IElementHandle> elementoPersonajes = await page.QuerySelectorAllAsync(".text-xs.font-bold");
			await AnadirPersonaje(elementoPersonajes[i], page, personajes);
			if (i % 20 == 0)//esto es porque la pagina no esta bien optimizada y te llena la cache, entonces cerramos y volvemos a abrir
			{
				await page.CloseAsync();
				page = await browser.NewPageAsync();
				await page.GotoAsync("https://www.mobachampion.com/champion/");
				ILocator locator2 = page.Locator(".css-47sehv");//quita las cookies
				await locator2.WaitForAsync(new() { Timeout = 5000 });
				await locator2.ClickAsync();
			}
		}

		using FileStream fileStream = File.Open("../../../../../../BlazorWeb/PoroConsultor/wwwroot/apis/estadisticas.json", FileMode.Create);
		JsonSerializer.Serialize(fileStream, personajes);

	}
	public static async Task AnadirPersonaje(IElementHandle personaje, IPage page, List<Estadisticas> personajes)
	{
		Thread.Sleep(1500);
		await personaje.ClickAsync();
		Thread.Sleep(1500);

		IReadOnlyList<IElementHandle> elementoBotonStats = await page.QuerySelectorAllAsync(".text-xs.font-bold");//entrar en stats
		await elementoBotonStats[0].ClickAsync();
		Thread.Sleep(1500);

		IElementHandle elementoNombre = await page.QuerySelectorAsync("h2.tracking-tightish");//coge el nombre 
        string nombre = await elementoNombre.InnerTextAsync();

        IElementHandle elementoImagen = await page.QuerySelectorAsync(".h-20.rounded-lg");//coge el nombre
        string imagen = await elementoImagen.GetAttributeAsync("src");

        Random random = new Random();
		await page.EvaluateAsync($"window.scrollTo(0, 1500)");//todo esto es para simular que estoy en la pagina y no es un bot
		await page.EvaluateAsync($"window.scrollTo(0, 0)");
		await page.EvaluateAsync($"window.scrollTo(0, 1500)");
		Thread.Sleep(random.Next(2000, 6000));


		IElementHandle elementoAd = await page.QuerySelectorAsync(".inset-y-0.bg-pink-400");//coge px del ad
        string ad = await page.EvaluateAsync<string>("(div) => getComputedStyle(div).width", elementoAd);

		IElementHandle elementoAp = await page.QuerySelectorAsync(".inset-y-0.bg-indigo-500");//coge px del ap
        string ap = await page.EvaluateAsync<string>("(div) => getComputedStyle(div).width", elementoAp);

		IElementHandle elementoTd = await page.QuerySelectorAsync(".inset-y-0.bg-green-400");//coge px del td
        string td = await page.EvaluateAsync<string>("(div) => getComputedStyle(div).width", elementoTd);


		IReadOnlyList<IElementHandle> elementoVida = await page.QuerySelectorAllAsync(".flex-row + .flex-row .flex-col .font-head");//coge cada elemento
        string vida = await elementoVida[0].InnerTextAsync();
		string mana = await elementoVida[1].InnerTextAsync();
		string dano = await elementoVida[2].InnerTextAsync();
		string armadura = await elementoVida[3].InnerTextAsync();
		string resistenciaMagica = await elementoVida[4].InnerTextAsync();
		string tipoEnergia = await elementoVida[5].InnerTextAsync();
		string vidaRegen = await elementoVida[6].InnerTextAsync();
		string manaRegen = await elementoVida[7].InnerTextAsync();
		string alcance = await elementoVida[8].InnerTextAsync();
		string probCritico = await elementoVida[9].InnerTextAsync();
		string velocidad = await elementoVida[10].InnerTextAsync();
		string velocidadAtaque = await elementoVida[11].InnerTextAsync();

		double adF = double.Parse(ad.Replace("px", "").Replace(".", ","));//error
		double apF = double.Parse(ap.Replace("px", "").Replace(".", ","));
		double tdF = double.Parse(td.Replace("px", "").Replace(".", ","));
		int suma = (int)adF + (int)apF + (int)tdF;


		personajes.Add(new Estadisticas()
		{
			Nombre = nombre,
			Imagen = imagen,
			PorcentajeAD = Convert.ToString((adF / suma) * 100),//calcula el porcentaje en base a los demas
			PorcentajeAP = Convert.ToString((apF / suma) * 100),
			PorcentajeTD = Convert.ToString((tdF / suma) * 100),
			Vida = vida.Split(' ')[0],// coge solo el primer dato es decir el base
			Mana = mana.Split(' ')[0],
			Dano = dano.Split(' ')[0],
			Armadura = armadura.Split(' ')[0],
			ResistenciaMagica = resistenciaMagica.Split(' ')[0],
			TipoEnergia = tipoEnergia.Split(' ')[0],
			VidaRegen = vidaRegen.Split(' ')[0],
			ManaRegen = manaRegen.Split(' ')[0],
			Alcance = alcance.Split(' ')[0],
			ProbabilidadCritico = probCritico.Split(' ')[0],
			Velocidad = velocidad.Split(' ')[0],
			VelocidadDeAtaque = velocidadAtaque.Split(' ')[0]
		});


		IElementHandle elementoBotonCampeones = await page.QuerySelectorAsync(".nuxt-link-active.text-yellow-300");
		await elementoBotonCampeones.ClickAsync();
		Thread.Sleep(1000);
	}
}
