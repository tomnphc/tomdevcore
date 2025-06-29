using UnityEngine;
using UnityEditor;

namespace TomDev.Editor
{
    [InitializeOnLoad]
    public static class OdinInspectorDetector
    {
        static OdinInspectorDetector()
        {
            CheckOdinInspector();
        }
        
        [MenuItem("TomDev/Check Odin Inspector")]
        public static void CheckOdinInspector()
        {
            bool hasOdinInspector = false;
            
            // Check if Odin Inspector assemblies are available
            try
            {
                System.Reflection.Assembly.Load("Sirenix.OdinInspector.Attributes");
                System.Reflection.Assembly.Load("Sirenix.OdinInspector.Editor");
                hasOdinInspector = true;
            }
            catch
            {
                hasOdinInspector = false;
            }
            
            // Set define symbol
            string defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone);
            
            if (hasOdinInspector)
            {
                if (!defines.Contains("ODIN_INSPECTOR"))
                {
                    defines += ";ODIN_INSPECTOR";
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, defines);
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, defines);
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, defines);
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.WebGL, defines);
                    
                    Debug.Log("[TomDev] Odin Inspector detected! Enhanced inspector features enabled.");
                }
            }
            else
            {
                if (defines.Contains("ODIN_INSPECTOR"))
                {
                    defines = defines.Replace(";ODIN_INSPECTOR", "").Replace("ODIN_INSPECTOR;", "").Replace("ODIN_INSPECTOR", "");
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, defines);
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, defines);
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, defines);
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.WebGL, defines);
                    
                    Debug.Log("[TomDev] Odin Inspector not found. Using standard Unity inspector.");
                }
            }
        }
    }
} 