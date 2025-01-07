using CodeChallenger.Lancamentos.Adapters.Repository.Seed;
using CodeChallenger.Lancamentos.Domain.Encryption;
using CodeChallenger.Lancamentos.Domain.Repository;
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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var writeRepository = scope.ServiceProvider.GetRequiredService<IWriteRepository>();
        var sha512Service = scope.ServiceProvider.GetRequiredService<ISha512Service>();

        var seeder = new DataSeeder(writeRepository, sha512Service);
        await seeder.SeedData();
    }
}

app.Run();
