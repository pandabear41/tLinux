using System;
namespace Terraria
{
    public class ChatEvent : PlayerEvent
    {
        private string chat;
        public ChatEvent(string chat, Player player)
            : base(player)
        {
            this.chat = chat;
        }
        public string getChat()
        {
            return this.chat;
        }
    }
}
