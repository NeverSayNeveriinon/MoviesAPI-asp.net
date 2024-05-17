using System.Text.Json;
// using Newtonsoft.Json;
// using Newtonsoft.Json.Converters;


namespace Infrastructure.Helpers;

public static class HelperMethods
{
    public static List<TEntity> JsonToListEntity<TEntity>(string Seed_Path) where TEntity : class
    {
        // Read the json file into a string
        string Seed_Json = File.ReadAllText(Seed_Path);

        // Deserialize the json file to 'a List of TEntity'
        
        // Way 1 (With 'Text.Json')
        List<TEntity> Seed_List = JsonSerializer.Deserialize<List<TEntity>>(Seed_Json)!;
        
        // Way 2 (With 'Newtonsoft')
        // List<TEntity> Seed_List = JsonConvert.DeserializeObject<List<TEntity>>(Seed_Json)!;

        return Seed_List;
    }
}