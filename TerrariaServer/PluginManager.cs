using System;
using System.Collections;
using System.IO;
using System.Reflection;
namespace Terraria
{
    internal class PluginManager
    {
        public static bool pluginsLoaded = false;
        private static System.Collections.ArrayList plugins = new System.Collections.ArrayList();
        public static void loadPlugins()
        {
            try
            {
                Console.WriteLine("Loading plugins...");
                foreach (string str in Directory.GetFiles(@"plugins\"))
                {
                    FileInfo info = new FileInfo(str);
                    if (info.Extension.Equals(".dll"))
                    {
                        Console.WriteLine("Loading plugin: " + info.Name);
                        Assembly assembly = Assembly.LoadFrom(info.FullName);
                        foreach (Type type in assembly.GetTypes())
                        {
                            if (!type.IsAbstract && (type.BaseType == typeof(Plugin)))
                            {
                                Plugin plugin = (Plugin)Activator.CreateInstance(type);
                                plugin.Initialize();
                                plugins.Add(plugin);
                                break;
                            }
                        }
                    }
                }
                pluginsLoaded = true;
                Console.WriteLine("Plugins loaded!");
            }
            catch (ReflectionTypeLoadException exception)
            {
                Console.WriteLine(exception.ToString());
                foreach (Exception exception2 in exception.LoaderExceptions)
                {
                    Console.WriteLine(exception2.ToString());
                }
                Console.WriteLine("Problem loading plugins!");
            }
        }

 

 

        public static void callHook(Hook hook, Event ev = null)
        {
            foreach (Plugin plugin in PluginManager.plugins)
            {
                try
                {
                    if (plugin.containsHook(hook))
                    {
                        switch (hook)
                        {
                            case Hook.TILE_CHANGE:
                                {
                                    plugin.onTileChanged((TileEvent)ev);
                                    break;
                                }
                            case Hook.PLAYER_COMMAND:
                                {
                                    plugin.onPlayerCommand((CommandEvent)ev);
                                    break;
                                }
                            case Hook.PLAYER_CHAT:
                                {
                                    plugin.onPlayerChat((ChatEvent)ev);
                                    break;
                                }
                            case Hook.SERVER_UPDATE:
                                {
                                    plugin.onServerUpdate();
                                    break;
                                }
                            case Hook.PLAYER_JOIN:
                                {
                                    plugin.onPlayerJoin((PlayerEvent)ev);
                                    break;
                                }
                            case Hook.NPC_SPAWN:
                                {
                                    plugin.onNPCSpawn((NPCSpawnEvent)ev);
                                    break;
                                }
                            case Hook.PLAYER_SPAWN:
                                {
                                    plugin.onPlayerSpawn((PlayerEvent)ev);
                                    break;
                                }
                            case Hook.ITEM_SPAWN:
                                {
                                    plugin.onItemSpawn((ItemEvent)ev);
                                    break;
                                }
                            case Hook.PLAYER_HURT:
                                {
                                    plugin.onPlayerHurt((PlayerHurtEvent)ev);
                                    break;
                                }
                            case Hook.PLAYER_DEATH:
                                {
                                    plugin.onPlayerDeath((PlayerEvent)ev);
                                    break;
                                }
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine(ex.ToString());
                    System.Console.WriteLine("Error calling hook: " + hook.ToString() + " on plugin: " + plugin.pluginName);
                }
            }
        }
    }
}
