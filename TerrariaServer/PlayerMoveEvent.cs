using System;
namespace Terraria
{
    public class PlayerMoveEvent : Event
    {
        private Player player;
        private Vector2 oldPos;
        private Vector2 newPos;
        public PlayerMoveEvent(Player player, Vector2 oldPos, Vector2 newPos)
        {
            this.player = player;
            this.oldPos = oldPos;
            this.newPos = newPos;
        }
        public Vector2 getOldPos()
        {
            return this.oldPos;
        }
        public Vector2 getNewPos()
        {
            return this.newPos;
        }
        public Player getPlayer()
        {
            return this.player;
        }
    }
}
