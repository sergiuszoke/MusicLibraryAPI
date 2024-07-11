using MusicLibraryDAL.Models;
using Newtonsoft.Json;

namespace MusicLibraryDAL
{
    public class DataSeeder
    {
        private readonly string _jsonFilePath = "../MusicLibraryDAL/data.json";
        private readonly DataContext _dataContext;

        public DataSeeder(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void SeedArtist()
        {
            if (File.Exists(_jsonFilePath) && !_dataContext.Artists.Any())
            {
                var jsonContent = File.ReadAllText(_jsonFilePath);
                var artists = JsonConvert.DeserializeObject<List<Artist>>(jsonContent);

                _dataContext.AddRange(artists);
                _dataContext.SaveChanges();
            }
        }
    }
}
