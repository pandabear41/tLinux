using System;
namespace Terraria
{
    public class Lighting
    {
        public const int offScreenTiles = 21;
        public static float[,] color = new float[Main.screenWidth + 42 + 10, Main.screenHeight + 42 + 10];
        private static int firstTileX;
        private static int firstTileY;
        private static int firstToLightX;
        private static int firstToLightY;
        private static int lastTileX;
        private static int lastTileY;
        private static int lastToLightX;
        private static int lastToLightY;
        private static float lightColor = 0f;
        public static int lightCounter = 0;
        public static int lightPasses = 2;
        public static int lightSkip = 1;
        private static int maxTempLights = 100;
        private static float[] tempLight = new float[Lighting.maxTempLights];
        private static int tempLightCount;
        private static int[] tempLightX = new int[Lighting.maxTempLights];
        private static int[] tempLightY = new int[Lighting.maxTempLights];
        public static void addLight(int i, int j, float Lightness)
        {
            if (Lighting.tempLightCount != Lighting.maxTempLights && i - Lighting.firstTileX + 21 >= 0 && i - Lighting.firstTileX + 21 < Main.screenWidth / 16 + 42 + 10 && j - Lighting.firstTileY + 21 >= 0 && j - Lighting.firstTileY + 21 < Main.screenHeight / 16 + 42 + 10)
            {
                for (int k = 0; k < Lighting.tempLightCount; k++)
                {
                    if (Lighting.tempLightX[k] == i && Lighting.tempLightY[k] == j && Lightness <= Lighting.tempLight[k])
                    {
                        return;
                    }
                }
                Lighting.tempLightX[Lighting.tempLightCount] = i;
                Lighting.tempLightY[Lighting.tempLightCount] = j;
                Lighting.tempLight[Lighting.tempLightCount] = Lightness;
                Lighting.tempLightCount++;
            }
        }
        public static float Brightness(int x, int y)
        {
            int num = x - Lighting.firstTileX + 21;
            int num2 = y - Lighting.firstTileY + 21;
            float result;
            if (num < 0 || num2 < 0 || num >= Main.screenWidth / 16 + 42 + 10 || num2 >= Main.screenHeight / 16 + 42 + 10)
            {
                result = 0f;
            }
            else
            {
                result = Lighting.color[num, num2];
            }
            return result;
        }
        public static Color GetBlackness(int x, int y)
        {
            int num = x - Lighting.firstTileX + 21;
            int num2 = y - Lighting.firstTileY + 21;
            Color result;
            if (num < 0 || num2 < 0 || num >= Main.screenWidth / 16 + 42 + 10 || num2 >= Main.screenHeight / 16 + 42 + 10)
            {
                result = Color.Black;
            }
            else
            {
                result = new Color(0, 0, 0, (int)((byte)(255f - 255f * Lighting.color[num, num2])));
            }
            return result;
        }
        public static Color GetColor(int x, int y)
        {
            int num = x - Lighting.firstTileX + 21;
            int num2 = y - Lighting.firstTileY + 21;
            Color result;
            if (num < 0 || num2 < 0 || num >= Main.screenWidth / 16 + 42 + 10 || num2 >= Main.screenHeight / 16 + 42 + 10)
            {
                result = Color.Black;
            }
            else
            {
                result = new Color((int)((byte)(255f * Lighting.color[num, num2])), (int)((byte)(255f * Lighting.color[num, num2])), (int)((byte)(255f * Lighting.color[num, num2])), 255);
            }
            return result;
        }
        public static Color GetColor(int x, int y, Color oldColor)
        {
            int num = x - Lighting.firstTileX + 21;
            int num2 = y - Lighting.firstTileY + 21;
            Color result;
            if (Main.gameMenu)
            {
                result = oldColor;
            }
            else
            {
                if (num < 0 || num2 < 0 || num >= Main.screenWidth / 16 + 42 + 10 || num2 >= Main.screenHeight / 16 + 42 + 10)
                {
                    result = Color.Black;
                }
                else
                {
                    Color white = Color.White;
                    white.R = (byte)((float)oldColor.R * Lighting.color[num, num2]);
                    white.G = (byte)((float)oldColor.G * Lighting.color[num, num2]);
                    white.B = (byte)((float)oldColor.B * Lighting.color[num, num2]);
                    result = white;
                }
            }
            return result;
        }
        private static void LightColor(int i, int j)
        {
            int num = i - Lighting.firstToLightX;
            int num2 = j - Lighting.firstToLightY;
            try
            {
                if (Lighting.color[num, num2] > Lighting.lightColor)
                {
                    Lighting.lightColor = Lighting.color[num, num2];
                }
                else
                {
                    if (Lighting.lightColor == 0f)
                    {
                        return;
                    }
                    Lighting.color[num, num2] = Lighting.lightColor;
                }
                float num3 = 0.04f;
                if (Main.tile[i, j].active && Main.tileBlockLight[(int)Main.tile[i, j].type])
                {
                    num3 = 0.16f;
                }
                float num4 = Lighting.lightColor - num3;
                if (num4 < 0f)
                {
                    Lighting.lightColor = 0f;
                }
                else
                {
                    Lighting.lightColor -= num3;
                    if (Lighting.lightColor > 0f && (!Main.tile[i, j].active || !Main.tileSolid[(int)Main.tile[i, j].type]) && (double)j < Main.worldSurface)
                    {
                        Main.tile[i, j].lighted = true;
                    }
                }
            }
            catch
            {
            }
        }
        public static int LightingX(int lightX)
        {
            int result;
            if (lightX < 0)
            {
                result = 0;
            }
            else
            {
                if (lightX >= Main.screenWidth / 16 + 42 + 10)
                {
                    result = Main.screenWidth / 16 + 42 + 10 - 1;
                }
                else
                {
                    result = lightX;
                }
            }
            return result;
        }
        public static int LightingY(int lightY)
        {
            int result;
            if (lightY < 0)
            {
                result = 0;
            }
            else
            {
                if (lightY >= Main.screenHeight / 16 + 42 + 10)
                {
                    result = Main.screenHeight / 16 + 42 + 10 - 1;
                }
                else
                {
                    result = lightY;
                }
            }
            return result;
        }
        public static void LightTiles(int firstX, int lastX, int firstY, int lastY)
        {
            Lighting.firstTileX = firstX;
            Lighting.lastTileX = lastX;
            Lighting.firstTileY = firstY;
            Lighting.lastTileY = lastY;
            Lighting.lightCounter++;
            if (Lighting.lightCounter <= Lighting.lightSkip)
            {
                Lighting.tempLightCount = 0;
                int num = Main.screenWidth / 16 + 42 + 10;
                int num2 = Main.screenHeight / 16 + 42 + 10;
                if ((int)(Main.screenPosition.X / 16f) < (int)(Main.screenLastPosition.X / 16f))
                {
                    for (int i = num - 1; i > 1; i--)
                    {
                        for (int j = 0; j < num2; j++)
                        {
                            Lighting.color[i, j] = Lighting.color[i - 1, j];
                        }
                    }
                }
                else
                {
                    if ((int)(Main.screenPosition.X / 16f) > (int)(Main.screenLastPosition.X / 16f))
                    {
                        for (int i = 0; i < num - 1; i++)
                        {
                            for (int j = 0; j < num2; j++)
                            {
                                Lighting.color[i, j] = Lighting.color[i + 1, j];
                            }
                        }
                    }
                }
                if ((int)(Main.screenPosition.Y / 16f) < (int)(Main.screenLastPosition.Y / 16f))
                {
                    for (int j = num2 - 1; j > 1; j--)
                    {
                        for (int i = 0; i < num; i++)
                        {
                            Lighting.color[i, j] = Lighting.color[i, j - 1];
                        }
                    }
                }
                else
                {
                    if ((int)(Main.screenPosition.Y / 16f) > (int)(Main.screenLastPosition.Y / 16f))
                    {
                        for (int j = 0; j < num2 - 1; j++)
                        {
                            for (int i = 0; i < num; i++)
                            {
                                Lighting.color[i, j] = Lighting.color[i, j + 1];
                            }
                        }
                    }
                }
            }
            else
            {
                Lighting.lightCounter = 0;
                Lighting.firstToLightX = Lighting.firstTileX - 21;
                Lighting.firstToLightY = Lighting.firstTileY - 21;
                Lighting.lastToLightX = Lighting.lastTileX + 21;
                Lighting.lastToLightY = Lighting.lastTileY + 21;
                for (int i = 0; i < Main.screenWidth / 16 + 42 + 10; i++)
                {
                    for (int j = 0; j < Main.screenHeight / 16 + 42 + 10; j++)
                    {
                        Lighting.color[i, j] = 0f;
                    }
                }
                for (int i = 0; i < Lighting.tempLightCount; i++)
                {
                    if (Lighting.tempLightX[i] - Lighting.firstTileX + 21 >= 0 && Lighting.tempLightX[i] - Lighting.firstTileX + 21 < Main.screenWidth / 16 + 42 + 10 && Lighting.tempLightY[i] - Lighting.firstTileY + 21 >= 0 && Lighting.tempLightY[i] - Lighting.firstTileY + 21 < Main.screenHeight / 16 + 42 + 10)
                    {
                        Lighting.color[Lighting.tempLightX[i] - Lighting.firstTileX + 21, Lighting.tempLightY[i] - Lighting.firstTileY + 21] = Lighting.tempLight[i];
                    }
                }
                Lighting.tempLightCount = 0;
                Main.evilTiles = 0;
                Main.meteorTiles = 0;
                Main.jungleTiles = 0;
                Main.dungeonTiles = 0;
                for (int i = Lighting.firstToLightX; i < Lighting.lastToLightX; i++)
                {
                    for (int j = Lighting.firstToLightY; j < Lighting.lastToLightY; j++)
                    {
                        if (i >= 0 && i < Main.maxTilesX && j >= 0 && j < Main.maxTilesY)
                        {
                            if (Main.tile[i, j] == null)
                            {
                                Main.tile[i, j] = new Tile();
                            }
                            if (Main.tile[i, j].active)
                            {
                                if (Main.tile[i, j].type == 23 || Main.tile[i, j].type == 24 || Main.tile[i, j].type == 25 || Main.tile[i, j].type == 32)
                                {
                                    Main.evilTiles++;
                                }
                                else
                                {
                                    if (Main.tile[i, j].type == 27)
                                    {
                                        Main.evilTiles -= 5;
                                    }
                                    else
                                    {
                                        if (Main.tile[i, j].type == 37)
                                        {
                                            Main.meteorTiles++;
                                        }
                                        else
                                        {
                                            if (Main.tileDungeon[(int)Main.tile[i, j].type])
                                            {
                                                Main.dungeonTiles++;
                                            }
                                            else
                                            {
                                                if (Main.tile[i, j].type == 60 || Main.tile[i, j].type == 61)
                                                {
                                                    Main.jungleTiles++;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (Main.tile[i, j] == null)
                            {
                                Main.tile[i, j] = new Tile();
                            }
                            if (Main.lightTiles)
                            {
                                Lighting.color[i - Lighting.firstTileX + 21, j - Lighting.firstTileY + 21] = (float)Main.tileColor.R / 255f;
                            }
                            if (Main.tile[i, j].lava)
                            {
                                float num3 = (float)(Main.tile[i, j].liquid / 255) * 0.6f + 0.1f;
                                if (Lighting.color[i - Lighting.firstToLightX, j - Lighting.firstToLightY] < num3)
                                {
                                    Lighting.color[i - Lighting.firstToLightX, j - Lighting.firstToLightY] = num3;
                                }
                            }
                            if ((!Main.tile[i, j].active || !Main.tileSolid[(int)Main.tile[i, j].type] || Main.tile[i, j].type == 37 || Main.tile[i, j].type == 58 || Main.tile[i, j].type == 70 || Main.tile[i, j].type == 76) && (Main.tile[i, j].lighted || Main.tile[i, j].type == 4 || Main.tile[i, j].type == 17 || Main.tile[i, j].type == 31 || Main.tile[i, j].type == 33 || Main.tile[i, j].type == 34 || Main.tile[i, j].type == 35 || Main.tile[i, j].type == 36 || Main.tile[i, j].type == 37 || Main.tile[i, j].type == 42 || Main.tile[i, j].type == 49 || Main.tile[i, j].type == 58 || Main.tile[i, j].type == 61 || Main.tile[i, j].type == 70 || Main.tile[i, j].type == 71 || Main.tile[i, j].type == 72 || Main.tile[i, j].type == 76 || Main.tile[i, j].type == 77 || Main.tile[i, j].type == 19 || Main.tile[i, j].type == 26))
                            {
                                if (Lighting.color[i - Lighting.firstToLightX, j - Lighting.firstToLightY] * 255f < (float)Main.tileColor.R && (float)Main.tileColor.R > Lighting.color[i - Lighting.firstToLightX, j - Lighting.firstToLightY] * 255f && Main.tile[i, j].wall == 0 && (double)j < Main.worldSurface && Main.tile[i, j].liquid < 255)
                                {
                                    Lighting.color[i - Lighting.firstToLightX, j - Lighting.firstToLightY] = (float)Main.tileColor.R / 255f;
                                }
                                if (Main.tile[i, j].type == 4 || Main.tile[i, j].type == 33 || Main.tile[i, j].type == 34 || Main.tile[i, j].type == 35 || Main.tile[i, j].type == 36)
                                {
                                    Lighting.color[i - Lighting.firstToLightX, j - Lighting.firstToLightY] = 1f;
                                }
                                else
                                {
                                    if (Main.tile[i, j].type == 17 && Lighting.color[i - Lighting.firstToLightX, j - Lighting.firstToLightY] < 0.8f)
                                    {
                                        Lighting.color[i - Lighting.firstToLightX, j - Lighting.firstToLightY] = 0.8f;
                                    }
                                    else
                                    {
                                        if (Main.tile[i, j].type == 77 && Lighting.color[i - Lighting.firstToLightX, j - Lighting.firstToLightY] < 0.8f)
                                        {
                                            Lighting.color[i - Lighting.firstToLightX, j - Lighting.firstToLightY] = 0.8f;
                                        }
                                        else
                                        {
                                            if (Main.tile[i, j].type == 37 && Lighting.color[i - Lighting.firstToLightX, j - Lighting.firstToLightY] < 0.6f)
                                            {
                                                Lighting.color[i - Lighting.firstToLightX, j - Lighting.firstToLightY] = 0.6f;
                                            }
                                            else
                                            {
                                                if (Main.tile[i, j].type == 58 && Lighting.color[i - Lighting.firstToLightX, j - Lighting.firstToLightY] < 0.6f)
                                                {
                                                    Lighting.color[i - Lighting.firstToLightX, j - Lighting.firstToLightY] = 0.6f;
                                                }
                                                else
                                                {
                                                    if (Main.tile[i, j].type == 76 && Lighting.color[i - Lighting.firstToLightX, j - Lighting.firstToLightY] < 0.6f)
                                                    {
                                                        Lighting.color[i - Lighting.firstToLightX, j - Lighting.firstToLightY] = 0.6f;
                                                    }
                                                    else
                                                    {
                                                        if (Main.tile[i, j].type == 42 && Lighting.color[i - Lighting.firstToLightX, j - Lighting.firstToLightY] < 0.75f)
                                                        {
                                                            Lighting.color[i - Lighting.firstToLightX, j - Lighting.firstToLightY] = 0.75f;
                                                        }
                                                        else
                                                        {
                                                            if (Main.tile[i, j].type == 49 && Lighting.color[i - Lighting.firstToLightX, j - Lighting.firstToLightY] < 0.75f)
                                                            {
                                                                Lighting.color[i - Lighting.firstToLightX, j - Lighting.firstToLightY] = 0.75f;
                                                            }
                                                            else
                                                            {
                                                                if (Main.tile[i, j].type == 70 || Main.tile[i, j].type == 71 || Main.tile[i, j].type == 72)
                                                                {
                                                                    float num4 = (float)Main.rand.Next(48, 52) * 0.01f;
                                                                    if (Lighting.color[i - Lighting.firstToLightX, j - Lighting.firstToLightY] < num4)
                                                                    {
                                                                        Lighting.color[i - Lighting.firstToLightX, j - Lighting.firstToLightY] = num4;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (Main.tile[i, j].type == 61 && Main.tile[i, j].frameX == 144 && Lighting.color[i - Lighting.firstToLightX, j - Lighting.firstToLightY] < 0.75f)
                                                                    {
                                                                        Lighting.color[i - Lighting.firstToLightX, j - Lighting.firstToLightY] = 0.75f;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (Main.tile[i, j].type == 31 || Main.tile[i, j].type == 26)
                                                                        {
                                                                            float num5 = (float)Main.rand.Next(-5, 6) * 0.01f;
                                                                            if (Lighting.color[i - Lighting.firstToLightX, j - Lighting.firstToLightY] < 0.4f + num5)
                                                                            {
                                                                                Lighting.color[i - Lighting.firstToLightX, j - Lighting.firstToLightY] = 0.4f + num5;
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                for (int k = 0; k < Lighting.lightPasses; k++)
                {
                    for (int i = Lighting.firstToLightX; i < Lighting.lastToLightX; i++)
                    {
                        Lighting.lightColor = 0f;
                        for (int j = Lighting.firstToLightY; j < Lighting.lastToLightY; j++)
                        {
                            Lighting.LightColor(i, j);
                        }
                    }
                    for (int i = Lighting.firstToLightX; i < Lighting.lastToLightX; i++)
                    {
                        Lighting.lightColor = 0f;
                        for (int j = Lighting.lastToLightY; j >= Lighting.firstToLightY; j--)
                        {
                            Lighting.LightColor(i, j);
                        }
                    }
                    for (int j = Lighting.firstToLightY; j < Lighting.lastToLightY; j++)
                    {
                        Lighting.lightColor = 0f;
                        for (int i = Lighting.firstToLightX; i < Lighting.lastToLightX; i++)
                        {
                            Lighting.LightColor(i, j);
                        }
                    }
                    for (int j = Lighting.firstToLightY; j < Lighting.lastToLightY; j++)
                    {
                        Lighting.lightColor = 0f;
                        for (int i = Lighting.lastToLightX; i >= Lighting.firstToLightX; i--)
                        {
                            Lighting.LightColor(i, j);
                        }
                    }
                }
            }
        }
    }
}
