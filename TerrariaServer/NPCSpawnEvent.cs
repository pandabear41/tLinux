using System;
namespace Terraria
{
    public class NPCSpawnEvent : Event
    {
        private int x;
        private int y;
        private int type;
        public NPCSpawnEvent(int x, int y, int type)
        {
            this.x = x;
            this.y = y;
            this.type = type;
        }
        public int getX()
        {
            return this.x;
        }
        public int getY()
        {
            return this.y;
        }
        public int getType()
        {
            return this.type;
        }
    }
}
