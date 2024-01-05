using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace RBronfenBova.Auxiliaries
{
    public class SetupTools
    {
        [MenuItem("Tools/Directories/Create Default Directories")]
        private static void CreateDefaultDirectories()
        {
            Directory.CreateDirectory(Path.Combine(Application.dataPath, "_Project"));
            Directory.CreateDirectory(Path.Combine(Application.dataPath, "_Project/Art"));
            Directory.CreateDirectory(Path.Combine(Application.dataPath, "_Project/Scenes"));
            Directory.CreateDirectory(Path.Combine(Application.dataPath, "_Project/Scripts"));
            AssetDatabase.Refresh();
        }
        
        [MenuItem("Tools/Packages/Set To Defaults")]
        private static async Task SetupPackages()
        {
            using var client = new HttpClient();
            var content = await client.GetStringAsync("https://gist.githubusercontent.com/rbronfen-bova/b4429cf09e571515207dd19f535ee84f/raw");
            var manifestFilePath = Path.Combine(Application.dataPath, "../Packages/manifest.json");
            await File.WriteAllTextAsync(manifestFilePath, content);
            UnityEditor.PackageManager.Client.Resolve();
        }

        [MenuItem("Tools/Packages/Install/TextMeshPro")]
        private static void InstallTextMeshPro()
        {
            UnityEditor.PackageManager.Client.Add("com.unity.textmeshpro");
            UnityEditor.PackageManager.Client.Resolve();
        }
    }
}
