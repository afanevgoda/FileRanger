using FileRanger.DAL;
using FileRanger.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
new StorageInitializer().Init(builder.Services);

builder.Services.AddControllersWithViews();
builder.Services.AddTransient<Scanner>();
builder.Services.AddTransient<FileBrowser>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
}

app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();