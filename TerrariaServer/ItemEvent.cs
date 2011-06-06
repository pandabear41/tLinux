using System;
namespace Terraria
{
    public class ItemEvent : Event
    {
        private Item item;
        public ItemEvent(Item item)
        {
            this.item = item;
        }
        public Item getItem()
        {
            return this.item;
        }
    }
}
