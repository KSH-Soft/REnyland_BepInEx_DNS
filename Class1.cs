using System;
using BepInEx;
using System.Reflection;

namespace RenylandDNSMod
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    public class RenylandDNS : BaseUnityPlugin
    {
        public const string pluginGuid = "axsys.renyland.renylanddns";
        public const string pluginName = "REnyland_DNS";
        public const string pluginVersion = "1.0.0.0";
        public void Awake()
        {
            Logger.LogInfo("Renyland_DNS Load");
            ServerManager serverManager = FindObjectOfType<ServerManager>();

            if (serverManager != null)
            {
                serverManager.RemoteServerBaseUrl = "XXX";
                Logger.LogInfo($"R.B. Server Url modifié");
            }
            else
            {
                Logger.LogError("ServerManager non trouvé!");
            }
            ChangeCdnUrl("GetThingDefinition_CDN", "XXX");
            ChangeCdnUrl("GetThingDefinitionAreaBundle_CDN", "XXX");
            ModifyServerBaseUrl();
        }

        private void ModifyServerBaseUrl()
        {
            Type serverManagerType = typeof(ServerManager);
            var serverManagerInstance = UnityEngine.Object.FindObjectOfType(serverManagerType);
            if (serverManagerInstance != null)
            {
                FieldInfo fieldInfo = serverManagerType.GetField("serverBaseUrl", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                try
                {
                    fieldInfo.SetValue(serverManagerInstance, "XXX");
                    Logger.LogInfo("serverBaseUrl a été modifié avec succès.");
                }
                catch (Exception ex)
                {
                    Logger.LogError($"Erreur lors de la modification de serverBaseUrl: {ex.Message}");
                }
            }
            else
            {
                Logger.LogError("L'instance de ServerManager n'a pas été trouvée.");
            }
        }
        private void ChangeCdnUrl(string fieldName, string newUrl)
        {
            Type serverUrlsType = typeof(ServerURLs);
            FieldInfo fieldInfo = serverUrlsType.GetField(fieldName, BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic);
            try
            {
                fieldInfo.SetValue(null, newUrl);
                Logger.LogInfo($"URL modifiée: {fieldName} = {newUrl}");
            }
            catch (Exception ex)
            {
                Logger.LogError($"Erreur lors de la modification de l'URL {fieldName}: {ex.Message}");
            }
                        
        }
    }
}
