using Microsoft.EntityFrameworkCore;
using PuppyStore.Server;
using PuppyStore.Server.Data;
using PuppyStore.Server.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Server;

var builder = WebApplication.CreateBuilder(args);

// ------------------------
// SERVICES
// ------------------------
builder.Services.AddControllers();

builder.Services.AddDbContext<PuppyStoreDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Correct CORS registration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowClient", policy =>
    {
        policy.WithOrigins("http://localhost:5091", "https://localhost:5091")   // <-- PUT CLIENT URL HERE
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddHttpClient();


var app = builder.Build();

// ------------------------
// MIDDLEWARE PIPELINE
// ------------------------
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// APPLY CORS HERE
app.UseCors("AllowClient");

app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
