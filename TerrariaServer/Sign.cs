using System;
namespace Terraria
{
    public class Sign
    {
        public const int maxSigns = 1000;
        public string text;
        public int x;
        public int y;
        public static void KillSign(int x, int y)
        {
            for (int i = 0; i < 1000; i++)
            {
                if (Main.sign[i] != null && Main.sign[i].x == x && Main.sign[i].y == y)
                {
                    Main.sign[i] = null;
                }
            }
        }
        public static int ReadSign(int i, int j)
        {
            int k = (int)(Main.tile[i, j].frameX / 18);
            int num = (int)(Main.tile[i, j].frameY / 18);
            while (k > 1)
            {
                k -= 2;
            }
            int num2 = i - k;
            int num3 = j - num;
            int result;
            if (Main.tile[num2, num3].type != 55)
            {
                Sign.KillSign(num2, num3);
                result = -1;
            }
            else
            {
                int num4 = -1;
                for (int l = 0; l < 1000; l++)
                {
                    if (Main.sign[l] != null && Main.sign[l].x == num2 && Main.sign[l].y == num3)
                    {
                        num4 = l;
                        break;
                    }
                }
                if (num4 < 0)
                {
                    for (int l = 0; l < 1000; l++)
                    {
                        if (Main.sign[l] == null)
                        {
                            num4 = l;
                            Main.sign[l] = new Sign();
                            Main.sign[l].x = num2;
                            Main.sign[l].y = num3;
                            Main.sign[l].text = "";
                            result = num4;
                            return result;
                        }
                    }
                }
                result = num4;
            }
            return result;
        }
        public static void TextSign(int i, string text)
        {
            if (Main.tile[Main.sign[i].x, Main.sign[i].y] == null || !Main.tile[Main.sign[i].x, Main.sign[i].y].active || Main.tile[Main.sign[i].x, Main.sign[i].y].type != 55)
            {
                Main.sign[i] = null;
            }
            else
            {
                Main.sign[i].text = text;
            }
        }
    }
}
