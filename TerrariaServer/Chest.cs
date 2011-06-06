using System;
namespace Terraria
{
    public class Chest
    {
        public Item[] item = new Item[Chest.maxItems];
        public static int maxItems = 20;
        public int x;
        public int y;
        public static int CreateChest(int X, int Y)
        {
            int result;
            for (int i = 0; i < 1000; i++)
            {
                if (Main.chest[i] != null && Main.chest[i].x == X && Main.chest[i].y == Y)
                {
                    result = -1;
                    return result;
                }
            }
            for (int i = 0; i < 1000; i++)
            {
                if (Main.chest[i] == null)
                {
                    Main.chest[i] = new Chest();
                    Main.chest[i].x = X;
                    Main.chest[i].y = Y;
                    for (int j = 0; j < Chest.maxItems; j++)
                    {
                        Main.chest[i].item[j] = new Item();
                    }
                    result = i;
                    return result;
                }
            }
            result = -1;
            return result;
        }
        public static bool DestroyChest(int X, int Y)
        {
            bool result;
            for (int i = 0; i < 1000; i++)
            {
                if (Main.chest[i] != null && Main.chest[i].x == X && Main.chest[i].y == Y)
                {
                    for (int j = 0; j < Chest.maxItems; j++)
                    {
                        if (Main.chest[i].item[j].type > 0 && Main.chest[i].item[j].stack > 0)
                        {
                            result = false;
                            return result;
                        }
                    }
                    Main.chest[i] = null;
                    result = true;
                    return result;
                }
            }
            result = true;
            return result;
        }
        public static int FindChest(int X, int Y)
        {
            int result;
            for (int i = 0; i < 1000; i++)
            {
                if (Main.chest[i] != null && Main.chest[i].x == X && Main.chest[i].y == Y)
                {
                    result = i;
                    return result;
                }
            }
            result = -1;
            return result;
        }
        public void SetupShop(int type)
        {
            for (int i = 0; i < Chest.maxItems; i++)
            {
                this.item[i] = new Item();
            }
            if (type == 1)
            {
                int i = 0;
                this.item[i].SetDefaults("Mining Helmet");
                i++;
                this.item[i].SetDefaults("Piggy Bank");
                i++;
                this.item[i].SetDefaults("Iron Anvil");
                i++;
                this.item[i].SetDefaults("Copper Pickaxe");
                i++;
                this.item[i].SetDefaults("Copper Axe");
                i++;
                this.item[i].SetDefaults("Torch");
                i++;
                this.item[i].SetDefaults("Lesser Healing Potion");
                i++;
                this.item[i].SetDefaults("Wooden Arrow");
                i++;
                this.item[i].SetDefaults("Shuriken");
                i++;
            }
            else
            {
                if (type == 2)
                {
                    int i = 0;
                    this.item[i].SetDefaults("Musket Ball");
                    i++;
                    this.item[i].SetDefaults("Flintlock Pistol");
                    i++;
                    this.item[i].SetDefaults("Minishark");
                    i++;
                }
                else
                {
                    if (type == 3)
                    {
                        int i = 0;
                        this.item[i].SetDefaults("Purification Powder");
                        i++;
                        this.item[i].SetDefaults("Acorn");
                        i++;
                        this.item[i].SetDefaults("Grass Seeds");
                        i++;
                        this.item[i].SetDefaults("Sunflower");
                        i++;
                        this.item[i].SetDefaults(114);
                        i++;
                    }
                    else
                    {
                        if (type == 4)
                        {
                            int i = 0;
                            this.item[i].SetDefaults("Grenade");
                            i++;
                            this.item[i].SetDefaults("Bomb");
                            i++;
                            this.item[i].SetDefaults("Dynamite");
                            i++;
                        }
                    }
                }
            }
        }
        public static int UsingChest(int i)
        {
            int result;
            if (Main.chest[i] != null)
            {
                for (int j = 0; j < 255; j++)
                {
                    if (Main.player[j].chest == i)
                    {
                        result = j;
                        return result;
                    }
                }
            }
            result = -1;
            return result;
        }
    }
}
