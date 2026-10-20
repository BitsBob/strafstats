using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

namespace strafstats
{
    [BepInPlugin("audey.strafstats", "Straftat Stats", "0.1.0")]
    public class Plugin : BaseUnityPlugin
    {
        internal static ManualLogSource Log;
        internal static ConfigEntry<string> ApiUrl;

        void Awake()
        {
            Log = Logger;
            var harmony =new Harmony("audey.straftat.stats");
            try { harmony.PatchAll(); } 
            catch (System.Exception e) { Log.LogError($"Failed to patch: {e}"); }

            foreach (var m in harmony.GetPatchedMethods())
            {
                Log.LogInfo($"Patched: {m.DeclaringType.FullName}.{m.Name}");
            }

            Log.LogInfo("Plugin Strafstats loaded");
        }
    }
}