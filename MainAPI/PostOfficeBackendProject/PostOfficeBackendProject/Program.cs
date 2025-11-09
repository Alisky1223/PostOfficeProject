using PostOfficeBackendProject.src.Application.Service;
using PostOfficeBackendProject.src.Infrastructure.Extention;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddInfrastructure();

builder.Services.AddDatabase(builder.Configuration);

builder.Services.AddAuthentication(builder.Configuration);
builder.Services.AddPolicies();

builder.Services.AddMiddleware(builder.Configuration);

var app = builder.Build();

//Seeding Database with Critical Informations
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeederService>();
    await seeder.SeedAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
