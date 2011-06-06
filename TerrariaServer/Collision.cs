﻿using System;
namespace Terraria
{
    public class Collision
    {
        public static Vector2 AnyCollision(Vector2 Position, Vector2 Velocity, int Width, int Height)
        {
            Vector2 result = Velocity;
            Vector2 vector = Velocity;
            Vector2 vector2 = Position + Velocity;
            Vector2 vector3 = Position;
            int num = (int)(Position.X / 16f) - 1;
            int num2 = (int)((Position.X + (float)Width) / 16f) + 2;
            int num3 = (int)(Position.Y / 16f) - 1;
            int num4 = (int)((Position.Y + (float)Height) / 16f) + 2;
            int num5 = -1;
            int num6 = -1;
            int num7 = -1;
            int num8 = -1;
            if (num < 0)
            {
                num = 0;
            }
            if (num2 > Main.maxTilesX)
            {
                num2 = Main.maxTilesX;
            }
            if (num3 < 0)
            {
                num3 = 0;
            }
            if (num4 > Main.maxTilesY)
            {
                num4 = Main.maxTilesY;
            }
            for (int i = num; i < num2; i++)
            {
                for (int j = num3; j < num4; j++)
                {
                    if (Main.tile[i, j] != null && Main.tile[i, j].active)
                    {
                        Vector2 vector4;
                        vector4.X = (float)(i * 16);
                        vector4.Y = (float)(j * 16);
                        if (vector2.X + (float)Width > vector4.X && vector2.X < vector4.X + 16f && vector2.Y + (float)Height > vector4.Y && vector2.Y < vector4.Y + 16f)
                        {
                            if (vector3.Y + (float)Height <= vector4.Y)
                            {
                                num7 = i;
                                num8 = j;
                                if (num7 != num5)
                                {
                                    result.Y = vector4.Y - (vector3.Y + (float)Height);
                                }
                            }
                            else
                            {
                                if (vector3.X + (float)Width <= vector4.X && !Main.tileSolidTop[(int)Main.tile[i, j].type])
                                {
                                    num5 = i;
                                    num6 = j;
                                    if (num6 != num8)
                                    {
                                        result.X = vector4.X - (vector3.X + (float)Width);
                                    }
                                    if (num7 == num5)
                                    {
                                        result.Y = vector.Y;
                                    }
                                }
                                else
                                {
                                    if (vector3.X >= vector4.X + 16f && !Main.tileSolidTop[(int)Main.tile[i, j].type])
                                    {
                                        num5 = i;
                                        num6 = j;
                                        if (num6 != num8)
                                        {
                                            result.X = vector4.X + 16f - vector3.X;
                                        }
                                        if (num7 == num5)
                                        {
                                            result.Y = vector.Y;
                                        }
                                    }
                                    else
                                    {
                                        if (vector3.Y >= vector4.Y + 16f && !Main.tileSolidTop[(int)Main.tile[i, j].type])
                                        {
                                            num7 = i;
                                            num8 = j;
                                            result.Y = vector4.Y + 16f - vector3.Y + 0.01f;
                                            if (num8 == num6)
                                            {
                                                result.X = vector.X + 0.01f;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }
        public static bool CanHit(Vector2 Position1, int Width1, int Height1, Vector2 Position2, int Width2, int Height2)
        {
            int num = (int)((Position1.X + (float)(Width1 / 2)) / 16f);
            int num2 = (int)((Position1.Y + (float)(Height1 / 2)) / 16f);
            int num3 = (int)((Position2.X + (float)(Width2 / 2)) / 16f);
            int num4 = (int)((Position2.Y + (float)(Height2 / 2)) / 16f);
            while (true)
            {
                int num5 = System.Math.Abs(num - num3);
                int num6 = System.Math.Abs(num2 - num4);
                if (num == num3 && num2 == num4)
                {
                    break;
                }
                if (num5 > num6)
                {
                    if (num < num3)
                    {
                        num++;
                    }
                    else
                    {
                        num--;
                    }
                    if (Main.tile[num, num2 - 1] == null)
                    {
                        Main.tile[num, num2 - 1] = new Tile();
                    }
                    if (Main.tile[num, num2 + 1] == null)
                    {
                        Main.tile[num, num2 + 1] = new Tile();
                    }
                    if (Main.tile[num, num2 - 1].active && Main.tileSolid[(int)Main.tile[num, num2 - 1].type] && !Main.tileSolidTop[(int)Main.tile[num, num2 - 1].type] && Main.tile[num, num2 + 1].active && Main.tileSolid[(int)Main.tile[num, num2 + 1].type] && !Main.tileSolidTop[(int)Main.tile[num, num2 + 1].type])
                    {
                        goto Block_12;
                    }
                }
                else
                {
                    if (num2 < num4)
                    {
                        num2++;
                    }
                    else
                    {
                        num2--;
                    }
                    if (Main.tile[num - 1, num2] == null)
                    {
                        Main.tile[num - 1, num2] = new Tile();
                    }
                    if (Main.tile[num + 1, num2] == null)
                    {
                        Main.tile[num + 1, num2] = new Tile();
                    }
                    if (Main.tile[num - 1, num2].active && Main.tileSolid[(int)Main.tile[num - 1, num2].type] && !Main.tileSolidTop[(int)Main.tile[num - 1, num2].type] && Main.tile[num + 1, num2].active && Main.tileSolid[(int)Main.tile[num + 1, num2].type] && !Main.tileSolidTop[(int)Main.tile[num + 1, num2].type])
                    {
                        goto Block_21;
                    }
                }
                if (Main.tile[num, num2] == null)
                {
                    Main.tile[num, num2] = new Tile();
                }
                if (Main.tile[num, num2].active && Main.tileSolid[(int)Main.tile[num, num2].type] && !Main.tileSolidTop[(int)Main.tile[num, num2].type])
                {
                    goto Block_25;
                }
            }
            bool result = true;
            return result;
        Block_12:
            result = false;
            return result;
        Block_21:
            result = false;
            return result;
        Block_25:
            result = false;
            return result;
        }
        public static bool DrownCollision(Vector2 Position, int Width, int Height)
        {
            Vector2 vector = new Vector2(Position.X + (float)(Width / 2), Position.Y + (float)(Height / 2));
            int num = 10;
            int num2 = 12;
            if (num > Width)
            {
                num = Width;
            }
            if (num2 > Height)
            {
                num2 = Height;
            }
            vector = new Vector2(vector.X - (float)(num / 2), Position.Y + 2f);
            int num3 = (int)(Position.X / 16f) - 1;
            int num4 = (int)((Position.X + (float)Width) / 16f) + 2;
            int num5 = (int)(Position.Y / 16f) - 1;
            int num6 = (int)((Position.Y + (float)Height) / 16f) + 2;
            if (num3 < 0)
            {
                num3 = 0;
            }
            if (num4 > Main.maxTilesX)
            {
                num4 = Main.maxTilesX;
            }
            if (num5 < 0)
            {
                num5 = 0;
            }
            if (num6 > Main.maxTilesY)
            {
                num6 = Main.maxTilesY;
            }
            int i = num3;
            bool result;
            while (i < num4)
            {
                for (int j = num5; j < num6; j++)
                {
                    if (Main.tile[i, j] != null && Main.tile[i, j].liquid > 0)
                    {
                        Vector2 vector2;
                        vector2.X = (float)(i * 16);
                        vector2.Y = (float)(j * 16);
                        int num7 = 16;
                        float num8 = (float)(0 - Main.tile[i, j].liquid);
                        num8 /= 32f;
                        vector2.Y += num8 * 2f;
                        num7 -= (int)(num8 * 2f);
                        if (vector.X + (float)num > vector2.X && vector.X < vector2.X + 16f && vector.Y + (float)num2 > vector2.Y && vector.Y < vector2.Y + (float)num7)
                        {
                            result = true;
                            return result;
                        }
                    }
                }
                i++;
                continue;
                return result;
            }
            result = false;
            return result;
        }
        public static bool EmptyTile(int i, int j, bool ignoreTiles = false)
        {
            Rectangle rectangle = new Rectangle(i * 16, j * 16, 16, 16);
            bool result;
            if (Main.tile[i, j].active && !ignoreTiles)
            {
                result = false;
            }
            else
            {
                for (int k = 0; k < 255; k++)
                {
                    if (Main.player[k].active && rectangle.Intersects(new Rectangle((int)Main.player[k].position.X, (int)Main.player[k].position.Y, Main.player[k].width, Main.player[k].height)))
                    {
                        result = false;
                        return result;
                    }
                }
                for (int k = 0; k < 200; k++)
                {
                    if (Main.item[k].active && rectangle.Intersects(new Rectangle((int)Main.item[k].position.X, (int)Main.item[k].position.Y, Main.item[k].width, Main.item[k].height)))
                    {
                        result = false;
                        return result;
                    }
                }
                for (int k = 0; k < 1000; k++)
                {
                    if (Main.npc[k].active && rectangle.Intersects(new Rectangle((int)Main.npc[k].position.X, (int)Main.npc[k].position.Y, Main.npc[k].width, Main.npc[k].height)))
                    {
                        result = false;
                        return result;
                    }
                }
                result = true;
            }
            return result;
        }
        public static void HitTiles(Vector2 Position, Vector2 Velocity, int Width, int Height)
        {
            Vector2 vector = Position + Velocity;
            int num = (int)(Position.X / 16f) - 1;
            int num2 = (int)((Position.X + (float)Width) / 16f) + 2;
            int num3 = (int)(Position.Y / 16f) - 1;
            int num4 = (int)((Position.Y + (float)Height) / 16f) + 2;
            if (num < 0)
            {
                num = 0;
            }
            if (num2 > Main.maxTilesX)
            {
                num2 = Main.maxTilesX;
            }
            if (num3 < 0)
            {
                num3 = 0;
            }
            if (num4 > Main.maxTilesY)
            {
                num4 = Main.maxTilesY;
            }
            for (int i = num; i < num2; i++)
            {
                for (int j = num3; j < num4; j++)
                {
                    if (Main.tile[i, j] != null && Main.tile[i, j].active && (Main.tileSolid[(int)Main.tile[i, j].type] || (Main.tileSolidTop[(int)Main.tile[i, j].type] && Main.tile[i, j].frameY == 0)))
                    {
                        Vector2 vector2;
                        vector2.X = (float)(i * 16);
                        vector2.Y = (float)(j * 16);
                        if (vector.X + (float)Width >= vector2.X && vector.X <= vector2.X + 16f && vector.Y + (float)Height >= vector2.Y && vector.Y <= vector2.Y + 16f)
                        {
                            WorldGen.KillTile(i, j, true, true, false);
                        }
                    }
                }
            }
        }
        public static Vector2 HurtTiles(Vector2 Position, Vector2 Velocity, int Width, int Height, bool fireImmune = false)
        {
            Vector2 vector = Position;
            int num = (int)(Position.X / 16f) - 1;
            int num2 = (int)((Position.X + (float)Width) / 16f) + 2;
            int num3 = (int)(Position.Y / 16f) - 1;
            int num4 = (int)((Position.Y + (float)Height) / 16f) + 2;
            if (num < 0)
            {
                num = 0;
            }
            if (num2 > Main.maxTilesX)
            {
                num2 = Main.maxTilesX;
            }
            if (num3 < 0)
            {
                num3 = 0;
            }
            if (num4 > Main.maxTilesY)
            {
                num4 = Main.maxTilesY;
            }
            int i = num;
            Vector2 result;
            while (i < num2)
            {
                for (int j = num3; j < num4; j++)
                {
                    if (Main.tile[i, j] != null && Main.tile[i, j].active && (Main.tile[i, j].type == 32 || Main.tile[i, j].type == 37 || Main.tile[i, j].type == 48 || Main.tile[i, j].type == 53 || Main.tile[i, j].type == 58 || Main.tile[i, j].type == 69 || Main.tile[i, j].type == 76))
                    {
                        Vector2 vector2;
                        vector2.X = (float)(i * 16);
                        vector2.Y = (float)(j * 16);
                        int num5 = 0;
                        int type = (int)Main.tile[i, j].type;
                        int num6 = type;
                        if (num6 == 32)
                        {
                            goto IL_1F9;
                        }
                        if (num6 != 53)
                        {
                            if (num6 == 69)
                            {
                                goto IL_1F9;
                            }
                            if (vector.X + (float)Width >= vector2.X && vector.X <= vector2.X + 16f && vector.Y + (float)Height >= vector2.Y && (double)vector.Y <= (double)vector2.Y + 16.01)
                            {
                                int num7 = 1;
                                if (vector.X + (float)(Width / 2) < vector2.X + 8f)
                                {
                                    num7 = -1;
                                }
                                if (!fireImmune && (type == 37 || type == 58 || type == 76))
                                {
                                    num5 = 20;
                                }
                                if (type == 48)
                                {
                                    num5 = 40;
                                }
                                result = new Vector2((float)num7, (float)num5);
                                return result;
                            }
                        }
                        else
                        {
                            if (vector.X + (float)Width - 2f >= vector2.X && vector.X + 2f <= vector2.X + 16f && vector.Y + (float)Height - 2f >= vector2.Y && vector.Y + 2f <= vector2.Y + 16f)
                            {
                                int num7 = 1;
                                if (vector.X + (float)(Width / 2) < vector2.X + 8f)
                                {
                                    num7 = -1;
                                }
                                if (type == 53)
                                {
                                    num5 = 20;
                                }
                                result = new Vector2((float)num7, (float)num5);
                                return result;
                            }
                        }
                        goto IL_469;
                    IL_1F9:
                        if (vector.X + (float)Width > vector2.X && vector.X < vector2.X + 16f && vector.Y + (float)Height > vector2.Y && (double)vector.Y < (double)vector2.Y + 16.01)
                        {
                            int num7 = 1;
                            if (vector.X + (float)(Width / 2) < vector2.X + 8f)
                            {
                                num7 = -1;
                            }
                            num5 = 10;
                            if (type == 69)
                            {
                                num5 = 25;
                            }
                            result = new Vector2((float)num7, (float)num5);
                            return result;
                        }
                    }
                IL_469: ;
                }
                i++;
                continue;
                return result;
            }
            result = default(Vector2);
            return result;
        }
        public static bool LavaCollision(Vector2 Position, int Width, int Height)
        {
            int num = (int)(Position.X / 16f) - 1;
            int num2 = (int)((Position.X + (float)Width) / 16f) + 2;
            int num3 = (int)(Position.Y / 16f) - 1;
            int num4 = (int)((Position.Y + (float)Height) / 16f) + 2;
            if (num < 0)
            {
                num = 0;
            }
            if (num2 > Main.maxTilesX)
            {
                num2 = Main.maxTilesX;
            }
            if (num3 < 0)
            {
                num3 = 0;
            }
            if (num4 > Main.maxTilesY)
            {
                num4 = Main.maxTilesY;
            }
            int i = num;
            bool result;
            while (i < num2)
            {
                for (int j = num3; j < num4; j++)
                {
                    if (Main.tile[i, j] != null && Main.tile[i, j].liquid > 0 && Main.tile[i, j].lava)
                    {
                        Vector2 vector;
                        vector.X = (float)(i * 16);
                        vector.Y = (float)(j * 16);
                        int num5 = 16;
                        float num6 = (float)(0 - Main.tile[i, j].liquid);
                        num6 /= 32f;
                        vector.Y += num6 * 2f;
                        num5 -= (int)(num6 * 2f);
                        if (Position.X + (float)Width > vector.X && Position.X < vector.X + 16f && Position.Y + (float)Height > vector.Y && Position.Y < vector.Y + (float)num5)
                        {
                            result = true;
                            return result;
                        }
                    }
                }
                i++;
                continue;
                return result;
            }
            result = false;
            return result;
        }
        public static bool SolidTiles(int startX, int endX, int startY, int endY)
        {
            bool result;
            if (startX < 0)
            {
                result = true;
            }
            else
            {
                if (endX >= Main.maxTilesX)
                {
                    result = true;
                }
                else
                {
                    if (startY < 0)
                    {
                        result = true;
                    }
                    else
                    {
                        if (endY >= Main.maxTilesY)
                        {
                            result = true;
                        }
                        else
                        {
                            for (int i = startX; i < endX + 1; i++)
                            {
                                for (int j = startY; j < endY + 1; j++)
                                {
                                    if (Main.tile[i, j] == null)
                                    {
                                        result = false;
                                        return result;
                                    }
                                    if (Main.tile[i, j].active && Main.tileSolid[(int)Main.tile[i, j].type] && !Main.tileSolidTop[(int)Main.tile[i, j].type])
                                    {
                                        result = true;
                                        return result;
                                    }
                                }
                            }
                            result = false;
                        }
                    }
                }
            }
            return result;
        }
        public static bool StickyTiles(Vector2 Position, Vector2 Velocity, int Width, int Height)
        {
            Vector2 vector = Position;
            bool result = false;
            int num = (int)(Position.X / 16f) - 1;
            int num2 = (int)((Position.X + (float)Width) / 16f) + 2;
            int num3 = (int)(Position.Y / 16f) - 1;
            int num4 = (int)((Position.Y + (float)Height) / 16f) + 2;
            if (num < 0)
            {
                num = 0;
            }
            if (num2 > Main.maxTilesX)
            {
                num2 = Main.maxTilesX;
            }
            if (num3 < 0)
            {
                num3 = 0;
            }
            if (num4 > Main.maxTilesY)
            {
                num4 = Main.maxTilesY;
            }
            for (int i = num; i < num2; i++)
            {
                for (int j = num3; j < num4; j++)
                {
                    if (Main.tile[i, j] != null && Main.tile[i, j].active && Main.tile[i, j].type == 51)
                    {
                        Vector2 vector2;
                        vector2.X = (float)(i * 16);
                        vector2.Y = (float)(j * 16);
                        if (vector.X + (float)Width > vector2.X && vector.X < vector2.X + 16f && vector.Y + (float)Height > vector2.Y && (double)vector.Y < (double)vector2.Y + 16.01)
                        {
                            if ((double)(System.Math.Abs(Velocity.X) + System.Math.Abs(Velocity.Y)) > 0.7 && Main.rand.Next(30) == 0)
                            {
                                Color color = default(Color);
                            }
                            result = true;
                        }
                    }
                }
            }
            return result;
        }
        public static Vector2 TileCollision(Vector2 Position, Vector2 Velocity, int Width, int Height, bool fallThrough = false, bool fall2 = false)
        {
            Vector2 result = Velocity;
            Vector2 vector = Velocity;
            Vector2 vector2 = Position + Velocity;
            Vector2 vector3 = Position;
            int num = (int)(Position.X / 16f) - 1;
            int num2 = (int)((Position.X + (float)Width) / 16f) + 2;
            int num3 = (int)(Position.Y / 16f) - 1;
            int num4 = (int)((Position.Y + (float)Height) / 16f) + 2;
            int num5 = -1;
            int num6 = -1;
            int num7 = -1;
            int num8 = -1;
            if (num < 0)
            {
                num = 0;
            }
            if (num2 > Main.maxTilesX)
            {
                num2 = Main.maxTilesX;
            }
            if (num3 < 0)
            {
                num3 = 0;
            }
            if (num4 > Main.maxTilesY)
            {
                num4 = Main.maxTilesY;
            }
            for (int i = num; i < num2; i++)
            {
                for (int j = num3; j < num4; j++)
                {
                    if (Main.tile[i, j] != null && Main.tile[i, j].active && (Main.tileSolid[(int)Main.tile[i, j].type] || (Main.tileSolidTop[(int)Main.tile[i, j].type] && Main.tile[i, j].frameY == 0)))
                    {
                        Vector2 vector4;
                        vector4.X = (float)(i * 16);
                        vector4.Y = (float)(j * 16);
                        if (vector2.X + (float)Width > vector4.X && vector2.X < vector4.X + 16f && vector2.Y + (float)Height > vector4.Y && vector2.Y < vector4.Y + 16f)
                        {
                            if (vector3.Y + (float)Height <= vector4.Y)
                            {
                                if (!Main.tileSolidTop[(int)Main.tile[i, j].type] || !fallThrough || (Velocity.Y > 1f && !fall2))
                                {
                                    num7 = i;
                                    num8 = j;
                                    if (num7 != num5)
                                    {
                                        result.Y = vector4.Y - (vector3.Y + (float)Height);
                                    }
                                }
                            }
                            else
                            {
                                if (vector3.X + (float)Width <= vector4.X && !Main.tileSolidTop[(int)Main.tile[i, j].type])
                                {
                                    num5 = i;
                                    num6 = j;
                                    if (num6 != num8)
                                    {
                                        result.X = vector4.X - (vector3.X + (float)Width);
                                    }
                                    if (num7 == num5)
                                    {
                                        result.Y = vector.Y;
                                    }
                                }
                                else
                                {
                                    if (vector3.X >= vector4.X + 16f && !Main.tileSolidTop[(int)Main.tile[i, j].type])
                                    {
                                        num5 = i;
                                        num6 = j;
                                        if (num6 != num8)
                                        {
                                            result.X = vector4.X + 16f - vector3.X;
                                        }
                                        if (num7 == num5)
                                        {
                                            result.Y = vector.Y;
                                        }
                                    }
                                    else
                                    {
                                        if (vector3.Y >= vector4.Y + 16f && !Main.tileSolidTop[(int)Main.tile[i, j].type])
                                        {
                                            num7 = i;
                                            num8 = j;
                                            result.Y = vector4.Y + 16f - vector3.Y + 0.01f;
                                            if (num8 == num6)
                                            {
                                                result.X = vector.X + 0.01f;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }
        public static bool WetCollision(Vector2 Position, int Width, int Height)
        {
            Vector2 vector = new Vector2(Position.X + (float)(Width / 2), Position.Y + (float)(Height / 2));
            int num = 10;
            int num2 = Height / 2;
            if (num > Width)
            {
                num = Width;
            }
            if (num2 > Height)
            {
                num2 = Height;
            }
            vector = new Vector2(vector.X - (float)(num / 2), vector.Y - (float)(num2 / 2));
            int num3 = (int)(Position.X / 16f) - 1;
            int num4 = (int)((Position.X + (float)Width) / 16f) + 2;
            int num5 = (int)(Position.Y / 16f) - 1;
            int num6 = (int)((Position.Y + (float)Height) / 16f) + 2;
            if (num3 < 0)
            {
                num3 = 0;
            }
            if (num4 > Main.maxTilesX)
            {
                num4 = Main.maxTilesX;
            }
            if (num5 < 0)
            {
                num5 = 0;
            }
            if (num6 > Main.maxTilesY)
            {
                num6 = Main.maxTilesY;
            }
            int i = num3;
            bool result;
            while (i < num4)
            {
                for (int j = num5; j < num6; j++)
                {
                    if (Main.tile[i, j] != null && Main.tile[i, j].liquid > 0)
                    {
                        Vector2 vector2;
                        vector2.X = (float)(i * 16);
                        vector2.Y = (float)(j * 16);
                        int num7 = 16;
                        float num8 = (float)(0 - Main.tile[i, j].liquid);
                        num8 /= 32f;
                        vector2.Y += num8 * 2f;
                        num7 -= (int)(num8 * 2f);
                        if (vector.X + (float)num > vector2.X && vector.X < vector2.X + 16f && vector.Y + (float)num2 > vector2.Y && vector.Y < vector2.Y + (float)num7)
                        {
                            result = true;
                            return result;
                        }
                    }
                }
                i++;
                continue;
                return result;
            }
            result = false;
            return result;
        }
    }
}
