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
            var content = await client.GetStringAsync("https://raw.githubusercontent.com/rbronfen-bova/UnityAuxiliaries/master/manifest.json");
            var manifestFilePath = Path.Combine(Application.dataPath, "../Packages/manifest.json");
            await File.WriteAllTextAsync(manifestFilePath, content);
            UnityEditor.PackageManager.Client.Resolve();
        }

        [MenuItem("Tools/Packages/Install/TextMeshPro")]
        private static void InstallTextMeshPro() =>
            InstallPackage("com.unity.textmeshpro");

        [MenuItem("Tools/Packages/Install/New Input System")]
        private static void InstallNewInputSystem() =>
            InstallPackage("com.unity.inputsystem");

        private static void InstallPackage(string package)
        {
            UnityEditor.PackageManager.Client.Add(package);
            UnityEditor.PackageManager.Client.Resolve();
        }
    }
}
