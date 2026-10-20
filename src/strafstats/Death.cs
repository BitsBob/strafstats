using System;
using FishNet;
using HarmonyLib;

namespace strafstats
{
    [HarmonyPatch(typeof(GameManager), "RecordPlayerDeath")]
    static class DeathPatch
    {
        static void Postfix(GameManager __instance, int PlayerId, uint SpawnGeneration)
        {
            try
            {
                if (!InstanceFinder.NetworkManager.IsServer)
                {
                    return;
                }

                if (SpawnGeneration != __instance.currentTakeGeneration) return;

                string name = "?";
                ulong steam = 0;

                if (ClientInstance.playerInstances.TryGetValue(PlayerId, out var client))
                {
                    name = client.PlayerNameTag;
                    steam = client.PlayerSteamID;
                }

                // to be replaced with Discord Endpoint
                Plugin.Log.LogInfo($"[DEATH] id={PlayerId} name={name} steam={steam} gen={SpawnGeneration}");
            }

            catch (Exception e)
            {
                Plugin.Log.LogError($"Failed to log death: {e}");
            }
        }

    }
}