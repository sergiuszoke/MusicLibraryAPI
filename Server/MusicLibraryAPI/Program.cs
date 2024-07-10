using Microsoft.EntityFrameworkCore;
using MusicLibraryDAL;

var builder = WebApplication.CreateBuilder(args);

var dbFilePath = builder.Configuration.GetConnectionString("DefaultConnection");
var projectPath = Directory.GetParent(Directory.GetCurrentDirectory());
var connectionString = string.Format(dbFilePath, projectPath);
builder.Services.AddDbContext<DataContext>(options => options.UseSqlite(connectionString));
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DataContext>();
    context.Database.Migrate();
}

app.Run();
