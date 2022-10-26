using System.Text.Json.Serialization;
using System.Text.Json;
using Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
const string authenticationScheme = "bearer";

builder.Services.AddMvcCore();
builder.Services.AddDistributedMemoryCache();
//Json defaults
builder.Services.AddControllers().AddJsonOptions(e =>
{
    e.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    e.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

JsonConvert.DefaultSettings = () => new JsonSerializerSettings
{
    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
    ContractResolver = new CamelCasePropertyNamesContractResolver(),
    NullValueHandling = NullValueHandling.Ignore,
};

//authentication
builder.Services.AddAuthentication(authenticationScheme);
builder.Services.AddSession(e => e.Cookie.HttpOnly = true);
//database connection
string connectionString = builder.Configuration["Data:Context:ConnectionString"];
builder.Services.AddDbContext<Context>(e =>
{
    e.UseMySql(connectionString,
        new MariaDbServerVersion(new Version(10, 3, 31)), o => o.MigrationsAssembly("Data"));
});



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseRouting();
app.UseHsts();
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseEndpoints(e =>
{
    e.MapControllers();
    e.MapControllerRoute(
        name: "Default",
        pattern: "{area=Student}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.Run();