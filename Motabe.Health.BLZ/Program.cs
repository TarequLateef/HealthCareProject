using Health.Motabea.Core.DTOs;
using Health.Motabea.Core.DTOs.External;
using Motabe.Health.BLZ.Data;
using Motabe.Health.BLZ.Data.Interface;
using Motabe.Health.BLZ.Data.Services.Adress;
using Motabe.Health.BLZ.Data.Services.ExternalData;
using Motabe.Health.BLZ.Data.Services.Patients;
using Motabe.Health.BLZ.Data.Services.PatientService;
using Motabe.Health.BLZ.Data.Services.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(HealthMap));

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(CookieData.CookieName).AddCookie();
builder.Services.AddSignalR();
/*builder.Services.AddSingleton<WeatherForecastService>();*/

builder.Services.AddHttpClient<FackeClass, FakeClassDTO>(client =>
    client.BaseAddress=new Uri("https://localhost:7061/api/"));
builder.Services.AddTransient<ICookieService, CookieService>();

builder.Services.AddScoped<AutherService>();
builder.Services.AddScoped<UserDataService>();
builder.Services.AddScoped<UserPassService>();
builder.Services.AddScoped<PatientBaseService>();
builder.Services.AddScoped<BookingService>();
builder.Services.AddScoped<PatientDetailsService>();
builder.Services.AddScoped<CountryService>();
builder.Services.AddScoped<GovernService>();
builder.Services.AddScoped<CityService>();
builder.Services.AddScoped<WorkService>();
builder.Services.AddScoped<SypoService>();
builder.Services.AddScoped<PatientSympService>();
builder.Services.AddScoped<AnalysisService>();
builder.Services.AddScoped<PatAnalService>();
builder.Services.AddScoped<BiometricService>();
builder.Services.AddScoped<PatBioService>();
builder.Services.AddScoped<CtService>();
builder.Services.AddScoped<PatCtService>();
builder.Services.AddScoped<PatRayService>();
builder.Services.AddScoped<RaysService>();
builder.Services.AddScoped<MedicineService>();
builder.Services.AddScoped<PatMedService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapHub<ConfirmBookHub>(HubVars.Url);
app.MapFallbackToPage("/_Host");

app.Run();
