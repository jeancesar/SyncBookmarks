using System.Text.Json;

namespace SyncEdgeToChrome
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            string chromeBookmarksPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                         @"Google\Chrome\User Data\Default\Bookmarks");

            string edgeBookmarksPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                       @"Microsoft\Edge\User Data\Default\Bookmarks");

            var edgeBookmarks = ReadBookmarks(edgeBookmarksPath);

            WriteBookmarks(chromeBookmarksPath, edgeBookmarks);

            Console.WriteLine("Favoritos do Edge foram sincronizados e sobrescritos no Chrome com sucesso.");
        }

        static Dictionary<string, object> ReadBookmarks(string path)
        {
            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<Dictionary<string, object>>(json);
        }

        static void WriteBookmarks(string path, Dictionary<string, object> bookmarks)
        {
            string json = JsonSerializer.Serialize(bookmarks, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);
        }
    }
}
