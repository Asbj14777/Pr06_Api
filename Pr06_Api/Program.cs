using Pr06_Api.Interface;
using Pr06_Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

builder.Services.AddHttpClient<IRickAndMortyHttpService, RickAndMortyHttpService>(client =>
{
    client.BaseAddress = new Uri("https://rickandmortyapi.com/api/");
});
builder.Services.AddHttpClient("NasaClient", client =>
{
    client.BaseAddress = new Uri("https://api.nasa.gov/planetary/");
});

builder.Services.AddScoped<INasaHttpService, NasaHttpService>();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

// MVC controllers
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=RickAndMorty}/{action=Index}/{id?}");

// Razor Pages
app.MapRazorPages()
   .WithStaticAssets();

app.Run();