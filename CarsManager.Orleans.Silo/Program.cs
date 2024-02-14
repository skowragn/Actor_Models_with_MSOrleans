using CarsManager.Orleans.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddOrleansHost(builder.Configuration);

builder.Services.AddHostedService<HubListUpdater>();

builder.Services.AddCors(policy =>
{
    policy.AddPolicy("_myAllowSpecificOrigins", builder =>
     builder.WithOrigins("https://localhost:7256")
      .SetIsOriginAllowed((host) => true) // this for using localhost address
      .AllowAnyMethod()
      .AllowAnyHeader()
      .AllowCredentials());
});

builder.Services.AddSignalR().AddJsonProtocol();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseCors("_myAllowSpecificOrigins");
app.MapHub<LocationHub>("/locationHub");
app.MapGet("/", () => "Grain Silo is active !");
app.Run();