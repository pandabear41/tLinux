using System;
namespace Terraria
{
    public class PlayerEvent : Event
    {
        private Player player;
        public PlayerEvent(Player player)
        {
            this.player = player;
        }
        public Player getPlayer()
        {
            return this.player;
        }
    }
}
