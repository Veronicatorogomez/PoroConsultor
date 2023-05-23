using Microsoft.Playwright;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace CogerDatos;
internal class Program
{
    public static async Task Main()
    {
        //Microsoft.Playwright.Program.Main(new[] { "install" });

        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions() { Headless = false });
        var page = await browser.NewPageAsync();
        await page.GotoAsync("https://www.leagueoflegends.com/es-es/champions/");

        ILocator locator = page.Locator(".osano-cm-accept-all");//quita las cookies
        await locator.WaitForAsync(new() { Timeout = 3000 });
        await locator.ClickAsync();

        Thread.Sleep(1000);
        IReadOnlyList<IElementHandle> elementoPersonajesInicio = await page.QuerySelectorAllAsync(".ehjaZK");//el css selector de cada personaje
        List<Campeones> personajes = new List<Campeones>(elementoPersonajesInicio.Count);


		for (int i = 0; i < elementoPersonajesInicio.Count; i++)
        {
            Thread.Sleep(500);
            await page.EvaluateAsync($"window.scrollTo(0, {i * 60 + 200})");//hace scroll hasta el personaje mas o menos para que no de error por no cargar
            IReadOnlyList<IElementHandle> elementoPersonajes = await page.QuerySelectorAllAsync(".ehjaZK");
            await AnadirPersonaje(elementoPersonajes[i], page, personajes);//pasa datos al metodo
        }

        using FileStream fileStream = File.Open("../../../../../../BlazorWeb/PoroConsultor/wwwroot/apis/campeones.json", FileMode.Create);//crea un fichero donde guardar
        JsonSerializer.Serialize(fileStream, personajes);//serializa, escribe en json

    }
    public static async Task AnadirPersonaje(IElementHandle personaje, IPage page, List<Campeones> personajes)
    {
        Thread.Sleep(1000);
        await personaje.ClickAsync();//hace click en el personaje que se le pasa

        Thread.Sleep(2000);

        IReadOnlyList<IElementHandle> elementoNombre = await page.QuerySelectorAllAsync(".style__RevealWrapper-sc-14kr0ky-0 span");
        string nombre = await elementoNombre[1].InnerTextAsync();//coge su nombre oficial
        string apodo = await elementoNombre[0].InnerTextAsync();//coge su apodo, o como lo llaman

        IElementHandle elementoImagenPrincipal = await page.QuerySelectorAsync(".style__ForegroundAsset-sc-8gkpub-4 img");
        string imagenPrincipal = await elementoImagenPrincipal.GetAttributeAsync("src");//coge imagen principal

        IElementHandle elementoDescripcion = await page.QuerySelectorAsync(".style__Desc-sc-8gkpub-9");
        string descripcion = await elementoDescripcion.InnerTextAsync();//coge descripcion personahe

        IReadOnlyList<IElementHandle> elementoRolDificultad = await page.QuerySelectorAllAsync(".style__SpecsItemValue-sc-8gkpub-15");
        string rol = await elementoRolDificultad[0].InnerTextAsync();//esto coge rol
        string dificultad = await elementoRolDificultad[1].InnerTextAsync();//coge dificultad


        IReadOnlyList<IElementHandle> elementoIconHabilidades = await page.QuerySelectorAllAsync(".style__OptionIconContent-sc-1ac4kmt-6 img");
        List<string> iconHabilidades = new List<string>();//coge los iconos de las habilidades

        IReadOnlyList<IElementHandle> elementoTipoHabilidades = await page.QuerySelectorAllAsync(".style__AbilityInfoItemType-sc-1bu2ash-9");
        List<string> tipoHabilidades = new List<string>();//coge el tipo de habilidades (es decir pasiva, q ....)

        IReadOnlyList<IElementHandle> elementoNombreHabilidades = await page.QuerySelectorAllAsync(".style__AbilityInfoItemName-sc-1bu2ash-10");
        List<string> nombreHabilidades = new List<string>();//coge el nombre de las habilidades

        IReadOnlyList<IElementHandle> elementoDescripcionHabilidades = await page.QuerySelectorAllAsync(".style__AbilityInfoItemDesc-sc-1bu2ash-11");
        List<string> descripcionHabilidades = new List<string>();//coge la descripcion de las habilidades

        IReadOnlyList<IElementHandle> elementoVideoHabilidades = await page.QuerySelectorAllAsync(".style__Video-fydk49-2 source[type=\"video/mp4\"]");//esto coge los vieos
        if(elementoVideoHabilidades.Count == 1)//hay personajes que no tienen mp4, asi que coge el formato webm
		{
            elementoVideoHabilidades = await page.QuerySelectorAllAsync(".style__Video-fydk49-2 source[type=\"video/webm\"]");
        }
        List<string> videoHabilidades = new List<string>();

        for (int i = 0; i < 5; i++)//por cada habilidad coge los datos anteriores y los guarda en una lista
        {
            iconHabilidades.Add(await elementoIconHabilidades[i].GetAttributeAsync("src"));
            tipoHabilidades.Add(await elementoTipoHabilidades[i].InnerTextAsync());
            nombreHabilidades.Add(await elementoNombreHabilidades[i].InnerTextAsync());
            descripcionHabilidades.Add(await elementoDescripcionHabilidades[i].InnerTextAsync());
            videoHabilidades.Add(await elementoVideoHabilidades[i].GetAttributeAsync("src"));
        }


        IReadOnlyList<IElementHandle> elementoIconoSkins = await page.QuerySelectorAllAsync(".style__CarouselItemThumb-gky2mu-15 img");//coge los iconos de las skins
        List<string> iconoSkins = new List<string>();

        IReadOnlyList<IElementHandle> elementoNombreSkins = await page.QuerySelectorAllAsync(".style__CarouselItemText-gky2mu-16");//coge el nombre de las skins
        List<string> nombreSkins = new List<string>();

        IReadOnlyList<IElementHandle> elementoImagenSkins = await page.QuerySelectorAllAsync(".style__SlideshowItemImage-gky2mu-4 img");//coge las imagenes grandes de las skins
        List<string> imagenSkins = new List<string>();

        for (int i = 0; i < elementoIconoSkins.Count; i++)//por cada skin q existe añade los datos anteriores a una lista
        {
            iconoSkins.Add(await elementoIconoSkins[i].GetAttributeAsync("src"));
            nombreSkins.Add(await elementoNombreSkins[i].InnerTextAsync());
            imagenSkins.Add(await elementoImagenSkins[i].GetAttributeAsync("src"));
        }

        personajes.Add(new Campeones()
        {
            Nombre = nombre,
            Apodo = apodo,
            ImagenPrincipal = imagenPrincipal,
            Descripcion = descripcion,
            Rol = rol,
            Dificultad = dificultad,
            ImagenHabilidad = iconHabilidades,
            TipoHabilidad = tipoHabilidades,
            NombreHabilidad = nombreHabilidades,
            DescripcionHabilidad = descripcionHabilidades,
            VideoHabilidad = videoHabilidades,
            IconoSkin = iconoSkins,
            NombreSkin = nombreSkins,
            ImagenSkin = imagenSkins,
            
        });


        IReadOnlyList<IElementHandle> elementoBotonCampeones = await page.QuerySelectorAllAsync("._3ckGQp1O-rIdTLJTTVRFsB a");//vuelve a la pagina de los campeones
        await elementoBotonCampeones[1].ClickAsync();
    }
}