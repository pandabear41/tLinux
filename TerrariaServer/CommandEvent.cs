using System;
namespace Terraria
{
    public class CommandEvent : PlayerEvent
    {
        private string[] cmd;
        public CommandEvent(string[] cmd, Player player)
            : base(player)
        {
            this.cmd = cmd;
        }
        public string[] getCommandArray()
        {
            return this.cmd;
        }
        public string getCommand()
        {
            return this.cmd[0];
        }
    }
}
