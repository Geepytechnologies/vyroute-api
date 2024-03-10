using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using vyroute.Data;
using vyroute.Dto;
using vyroute.Models;
using vyroute.Repositories;
using vyroute.Services;
using static vyroute.Repositories.TransporterRepository;

var builder = WebApplication.CreateBuilder(args);
var AllowSpecificOrigins = "AllowSpecificOrigins";

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAuthorization();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("myconnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
{
    options.Password.RequiredLength = 5;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        RequireExpirationTime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value,
        ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Value,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value))
    };
});
builder.Services.Configure<EmailConfig>(builder.Configuration.GetSection("EmailConfiguration"));
//services
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<ITransporterService, TransporterService>();
builder.Services.AddScoped<ITerminalService, TerminalService>();
builder.Services.AddScoped<ITransitService, TransitService>();

//Repositories
builder.Services.AddScoped<IGenericRepository<Vehicle>, GenericRepository<Vehicle>>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IGenericRepository<Booking>, GenericRepository<Booking>>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IGenericRepository<Terminal>, GenericRepository<Terminal>>();
builder.Services.AddScoped<ITerminalRepository, TerminalRepository>();
builder.Services.AddScoped<IGenericRepository<Transporter>, GenericRepository<Transporter>>();
builder.Services.AddScoped<ITransporterRepository, TransporterRepository>();
builder.Services.AddScoped<IGenericRepository<Transit>, GenericRepository<Transit>>();
builder.Services.AddScoped<ITransitRepository, TransitRepository>();



builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins(["http://localhost:5175", "http://localhost:5173","https://vyroute.vercel.app"]).AllowAnyMethod().AllowCredentials().WithHeaders("Content-Type", "Authorization");
                      });
});





var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

    app.UseSwagger();
    app.UseSwaggerUI();

//app.MapIdentityApi<IdentityUser>();

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseCors(AllowSpecificOrigins);
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
