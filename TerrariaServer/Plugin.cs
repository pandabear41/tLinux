using System;
using System.Collections;
namespace Terraria
{
    public abstract class Plugin
    {
        private System.Collections.ArrayList hooks = new System.Collections.ArrayList();
        public string pluginName = "N/A";
        public string pluginDescription = "N/A";
        public string pluginAuthor = "N/A";
        public string pluginVersion = "N/A";

        protected Plugin()
        {
            this.hooks = new ArrayList();
            this.pluginName = "N/A";
            this.pluginDescription = "N/A";
            this.pluginAuthor = "N/A";
            this.pluginVersion = "N/A";
        }

        public abstract void Initialize();

        public abstract void Unload();

        public virtual void onTileChanged(TileEvent ev)
        {
        }
        public virtual void onPlayerCommand(CommandEvent ev)
        {
        }
        public virtual void onPlayerChat(ChatEvent ev)
        {
        }
        public virtual void onNPCSpawn(NPCSpawnEvent ev)
        {
        }
        public virtual void onServerUpdate()
        {
        }
        public virtual void onPlayerSpawn(PlayerEvent ev)
        {
        }
        public virtual void onPlayerDeath(PlayerEvent ev)
        {
        }
        public virtual void onPlayerMove(PlayerMoveEvent ev)
        {
        }
        public virtual void onPlayerHurt(PlayerHurtEvent ev)
        {
        }
        public virtual void onPlayerJoin(PlayerEvent ev)
        {
        }
        public virtual void onItemSpawn(ItemEvent ev)
        {
        }
        public void registerHook(Hook hook)
        {
            if (!this.hooks.Contains(hook))
            {
                this.hooks.Add(hook);
            }
        }
        public bool containsHook(Hook hook)
        {
            return this.hooks.Contains(hook);
        }
    }
}
