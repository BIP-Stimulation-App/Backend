using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StimulationAppAPI.BLL.Service;
using StimulationAppAPI.Controllers;
using StimulationAppAPI.DAL.Context;
using StimulationAppAPI.DAL.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
var conn = builder.Configuration.GetConnectionString("ConnectionString");
builder.Services.AddControllers(option =>
    {
        option.AllowEmptyInputInBodyModelBinding = true;
        option.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
    });
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
#if DEBUG
builder.Services.AddDbContext<UserContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionStringTesting"), b => b.MigrationsAssembly("StimulationAppAPI")));
#else
builder.Services.AddDbContext<UserContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"), b => b.MigrationsAssembly("StimulationAppAPI")));

#endif
#region Services
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<MedicationService>();
builder.Services.AddScoped<ExerciseService>();

#endregion

#region Controllers
builder.Services.AddScoped<UserController>();
builder.Services.AddScoped<LoginController>();
builder.Services.AddScoped<MedicationController>();
builder.Services.AddScoped<ExerciseController>();

#endregion

EmailConfiguration.Email = builder.Configuration["EmailConfiguration:Email"];
EmailConfiguration.Name = builder.Configuration["EmailConfiguration:Name"];
EmailConfiguration.Password = builder.Configuration["EmailConfiguration:Password"];
EmailConfiguration.SmtpPort = int.Parse(builder.Configuration["EmailConfiguration:SmtpPort"]);
EmailConfiguration.SmtpSSL = bool.Parse(builder.Configuration["EmailConfiguration:SmtpSSL"]);
EmailConfiguration.SmtpServer = builder.Configuration["EmailConfiguration:SmtpServer"];

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(option =>
{
    option.AddPolicy("All", policy =>
    {
        policy.WithOrigins("*");
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
    });
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "StimulationAppAPI",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description =
            "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

var app = builder.Build();


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();
app.UseCors(corsPolicyBuilder =>
{
    corsPolicyBuilder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
});
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<UserContext>();
    Console.WriteLine("starting db migration: ");
    if (context.Database.GetPendingMigrations().Any())
    {
//#if RELEASE
        context.Database.Migrate();
//#endif
    }
}
app.Run();

