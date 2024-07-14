using Microsoft.EntityFrameworkCore;
using MusicLibraryDAL;
using MusicLibraryBLL;
using MusicLibraryDAL.Interfaces;
using MusicLibraryDAL.Repository;
using MusicLibraryBLL.Interfaces;
using MusicLibraryBLL.Services;

var builder = WebApplication.CreateBuilder(args);

var dbFilePath = builder.Configuration.GetConnectionString("DefaultConnection");
var projectPath = Directory.GetParent(Directory.GetCurrentDirectory());
var connectionString = string.Format(dbFilePath, projectPath);
builder.Services.AddDbContext<DataContext>(options => options.UseSqlite(connectionString));
builder.Services.AddControllers();

builder.Services.ConfigureAutomapper();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ISongRepository, SongRepository>();
builder.Services.AddTransient<ISongService, SongService>();

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
    var dataContext = services.GetRequiredService<DataContext>();
    dataContext.Database.Migrate();

    try
    {
        var dataSeeder = new DataSeeder(dataContext);
        dataSeeder.SeedArtist();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }
}

app.Run();
