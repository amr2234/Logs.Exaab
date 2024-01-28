using Logs.Exaab.Models;
using Logs.Exaab.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

builder.Host.UseSerilog((context, config) =>
{
	config.ReadFrom.Configuration(context.Configuration);
});
builder.Services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
builder.Services.Configure<MongoDBSetting>(
  builder.Configuration.GetSection("MongoDBSettings"));
builder.Services.AddSingleton(sp =>
  sp.GetRequiredService<IOptions<MongoDBSetting>>().Value);
Log.Logger = new LoggerConfiguration()
	.Enrich.FromLogContext()
	.WriteTo.Console()
	.CreateLogger();
builder.Services.AddScoped<ILogsService, LogsService>();

var app = builder.Build();
app.UseStaticFiles();
app.MapControllerRoute(
name: "default",
pattern: "{controller=Logs}/{action=GetData}"
);

try
{
	Log.Warning("Logging is Satrting");
	app.Run();

}
catch (Exception ex)
{
	Log.Fatal(ex, "Failed to start");
}
finally
{
	Log.CloseAndFlush();

}
