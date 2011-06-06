using System;
namespace Terraria
{
    public class TileEvent : PlayerEvent
    {
        private Tile tile;
        private int changeType;
        public TileEvent(Tile tile, Player player)
            : base(player)
        {
            base.setState(false);
            this.tile = tile;
        }
        public TileEvent(Tile tile, Player player, int changeType)
            : base(player)
        {
            base.setState(false);
            this.tile = tile;
            this.changeType = changeType;
        }
        public TileEvent(Tile tile)
            : base(null)
        {
            this.tile = tile;
        }
        public Tile getTile()
        {
            return this.tile;
        }
        public int getChangeType()
        {
            return this.changeType;
        }
    }
}
