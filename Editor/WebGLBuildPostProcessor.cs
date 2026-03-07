using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

namespace UniJS.Editor
{
    public class WebGLBuildPostProcessor : IPostprocessBuildWithReport
    {
        public int callbackOrder => 0;
        private const string NpmPackage = "@pizzapopcorn/unijs";
        private const string FilePath = "dist/unity.min.js";

        public void OnPostprocessBuild(BuildReport report)
        {
            if (report.summary.platform != BuildTarget.WebGL) return;

            string buildPath = report.summary.outputPath;
            string indexPath = Path.Combine(buildPath, "index.html");

            if (!File.Exists(indexPath)) return;

            var settings = UniJSConfig.GetOrCreateSettings();
            string htmlContent = File.ReadAllText(indexPath);
            
            //Importing CDN if configured
            if (settings.includeCDN)
            {
                if (htmlContent.Contains("dist/unity.min.js"))
                {
                    Debug.LogWarning("<color=#FF9800><b>[UniJS]</b> Detected CDN already imported in template.</color>");
                }
                else
                {
                    var versionTag = string.IsNullOrEmpty(settings.targetCDNVersion) ? "" : $"@{settings.targetCDNVersion}";
                    htmlContent = htmlContent.Replace("</head>", $"  <script src=\"https://cdn.jsdelivr.net/npm/{NpmPackage}{versionTag}/{FilePath}\"></script>\n</head>");
                    Debug.Log("[UniJS] CDN library imported.");
                }
            }

            //JS code extraction to separate file
            var match = Regex.Match(htmlContent, @"<script>(.*?)createUnityInstance.*?</script>", RegexOptions.Singleline);
            
            if (match.Success)
            {
                var jsContent = match.Value.Replace("<script>", "").Replace("</script>", "");
                File.WriteAllText(Path.Combine(buildPath, "index.js"), jsContent);
                htmlContent = Regex.Replace(htmlContent, @"<script>.*?</script>", 
                    "<script src=\"index.js\"></script>", RegexOptions.Singleline);
                Debug.Log("[UniJS] Code extracted to a separate index.js file.");
            }
            
            if (settings.includeControllerScript)
            {
                var scriptName = string.IsNullOrEmpty(settings.controllerScriptName) ? "unityController.js" : $"{settings.controllerScriptName}.js";
                htmlContent = htmlContent.Replace("</body>", $"  <script src=\"{scriptName}\"></script>\n  </body>");
                Debug.Log("[UniJS] Controller script tag added.");
            }
            
            Debug.Log("<color=#4CAF50><b>[UniJS]</b> Successfully processed template for UniJS.</color>");
            File.WriteAllText(indexPath, htmlContent);
        }
    }
}