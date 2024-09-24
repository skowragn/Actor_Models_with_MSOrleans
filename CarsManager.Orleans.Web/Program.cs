using CarsManager.Orleans.Web.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCqrs();

builder.Services.AddMudServices();
builder.Services.AddHttpContextAccessor();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddLocalStorageServices();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddScoped<ComponentStateChangedObserver>();
builder.Services.AddSingleton<ToastService>();

builder.Services.AddScoped<ICarReservationsService, CarReservationsService>();
builder.Services.AddScoped<IBookedCarsItemsService, CarBookedItemsService>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.IsEssential = true;
});

builder.Host.AddOrleansHostClient(builder.Configuration);

builder.Services.AddSingleton<ClusterBaseServices>();

builder.Services.AddSignalR();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseDefaultFiles(); 

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseSession();
app.UseAuthorization();
app.MapRazorPages();

app.UseEndpoints(endpoints =>
{
    endpoints.MapBlazorHub();
    endpoints.MapFallbackToPage("/_Host");
});

app.Run();