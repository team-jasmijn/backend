using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddSession(e => e.Cookie.HttpOnly = true);
//database connection
string connectionString = builder.Configuration["Data:Context:ConnectionString"];
builder.Services.AddDbContext<Context>(e =>
{
    e.UseMySql(connectionString,
        new MariaDbServerVersion(new Version(10, 3, 31)), o => o.MigrationsAssembly("Data"));
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    using (var context = scope.ServiceProvider.GetService<Context>())
    {
        await context.Database.MigrateAsync();
    }
}


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
app.UseAuthorization(); 
app.UseEndpoints(e =>
{
    e.MapControllers();
    e.MapControllerRoute(
        name: "Default",
        pattern: "{area=Student}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.Run();