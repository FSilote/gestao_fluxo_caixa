using CodeChallenger.Lancamentos.WebApi.AppStart.Middlewares;
using CodeChallenger.Lancamentos.WebApi.AppStart.Services;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureSeriLog();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

builder.ConfigureMediatR();
builder.ConfigureCors();
builder.ConfigureAuthorization();
builder.ConfigureMvc(null!);
// builder.ConfigureVersioning();
builder.ConfigureSwagger();
builder.ConfigureRepository();
builder.ConfigureMessaging();
builder.ConfigureEncryption();
builder.ConfigureTokenServices();

// Build the WebApplication
var app = builder.Build();

// Configure Default Middlewares
app.ConfigureCors();
app.ConfigureMvc();
app.ConfigureAuthentication();
app.ConfigureEndpoints();
// app.ConfigureSwagger();
// 
// app.Run();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
