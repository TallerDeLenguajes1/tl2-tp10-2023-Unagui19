using tl2_tp10_2023_Unagui19.Repositorios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>// builder para las sesiones
{
    options.IdleTimeout = TimeSpan.FromSeconds(60); //me da el tiempo que esta activa la sesion 
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});



var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

var builder1 = WebApplication.CreateBuilder(args);
// Add services to the container.
builder1.Services.AddControllersWithViews();
var CadenaDeConexion =
builder1.Configuration.GetConnectionString("SqliteConexion")!.ToString();
builder1.Services.AddSingleton<string>(CadenaDeConexion);
// Aquí se realiza la inyección de los repositorios
builder.Services.AddScoped<IUsuarioRepository,UsuarioRepositorio>();
builder.Services.AddScoped<ITableroRepository,TableroRepository>();
builder.Services.AddScoped<ITareaRepository,TareaRepository>();

var app2 = builder.Build();



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
