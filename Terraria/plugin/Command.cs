using System;
using System.Collections;
using System.Linq;
using System.Text;
namespace Terraria
{
    
	public class Command
	{
        public string[] command;
        public int iscommand;
        public void parse(Player player, string text)
        {
            iscommand = 0;
            command = text.Split(new char[] { ' ' });
            if (command[0].StartsWith("/"))
            {
                //thingy is a command
                iscommand = 1;
            }
        }
	}
}
