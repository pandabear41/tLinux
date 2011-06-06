using System;
namespace Terraria
{
    public class PlayerHurtEvent : Event
    {
        private Player player;
        private int dmg;
        public PlayerHurtEvent(Player player, int dmg)
        {
            this.player = player;
            this.dmg = dmg;
        }
        public int getDamage()
        {
            return this.dmg;
        }
        public Player getPlayer()
        {
            return this.player;
        }
    }
}
