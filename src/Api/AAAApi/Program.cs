using AAA.src.Application.Service;
using AAA.src.Infrastructure.Extentions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure();

builder.Services.AddDatabase(builder.Configuration);

builder.Services.AddAuthentication(builder.Configuration);

builder.Services.AddPolicies();

builder.Services.AddPasswordPolicy(builder.Configuration);

builder.Services.AddValidators();

var app = builder.Build();

//Seeding Database with Critical Informations
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeederService>();
    await seeder.SeedAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
