using Taller2_TP10.Repositorios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//para cookies y sesion
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(120);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


//Inyecciones de deependencia Base de datos
var CadenaDeConexion =
builder.Configuration.GetConnectionString("SqliteConexion")!.ToString();
builder.Services.AddSingleton<string>(CadenaDeConexion);

//Inyecciones de deependencia de los repositorios
builder.Services.AddScoped<IUsuarioRepository,UsuarioRepository>();
builder.Services.AddScoped<ITableroRepository,TableroRepository>();
builder.Services.AddScoped<ITareaRepository,TareaRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
