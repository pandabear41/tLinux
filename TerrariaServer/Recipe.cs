using System;
namespace Terraria
{
    public class Recipe
    {
        public Item createItem = new Item();
        public static int maxRecipes = 5000;
        public static int maxRequirements = 10;
        private static Recipe newRecipe = new Recipe();
        public static int numRecipes = 0;
        public Item[] requiredItem = new Item[Recipe.maxRequirements];
        public int[] requiredTile = new int[Recipe.maxRequirements];
        public Recipe()
        {
            for (int i = 0; i < Recipe.maxRequirements; i++)
            {
                this.requiredItem[i] = new Item();
                this.requiredTile[i] = -1;
            }
        }
        private static void addRecipe()
        {
            Main.recipe[Recipe.numRecipes] = Recipe.newRecipe;
            Recipe.newRecipe = new Recipe();
            Recipe.numRecipes++;
        }
        public void Create()
        {
            for (int i = 0; i < Recipe.maxRequirements; i++)
            {
                if (this.requiredItem[i].type == 0)
                {
                    break;
                }
                int num = this.requiredItem[i].stack;
                for (int j = 0; j < 44; j++)
                {
                    if (Main.player[Main.myPlayer].inventory[j].IsTheSameAs(this.requiredItem[i]))
                    {
                        if (Main.player[Main.myPlayer].inventory[j].stack > num)
                        {
                            Item item = Main.player[Main.myPlayer].inventory[j];
                            item.stack -= num;
                            num = 0;
                        }
                        else
                        {
                            num -= Main.player[Main.myPlayer].inventory[j].stack;
                            Main.player[Main.myPlayer].inventory[j] = new Item();
                        }
                    }
                    if (num <= 0)
                    {
                        break;
                    }
                }
            }
            Recipe.FindRecipes();
        }
        public static void FindRecipes()
        {
            int num = Main.availableRecipe[Main.focusRecipe];
            float num2 = Main.availableRecipeY[Main.focusRecipe];
            for (int i = 0; i < Recipe.maxRecipes; i++)
            {
                Main.availableRecipe[i] = 0;
            }
            Main.numAvailableRecipes = 0;
            for (int i = 0; i < Recipe.maxRecipes; i++)
            {
                if (Main.recipe[i].createItem.type == 0)
                {
                    break;
                }
                bool flag = true;
                for (int j = 0; j < Recipe.maxRequirements; j++)
                {
                    if (Main.recipe[i].requiredItem[j].type == 0)
                    {
                        break;
                    }
                    int num3 = Main.recipe[i].requiredItem[j].stack;
                    for (int k = 0; k < 44; k++)
                    {
                        if (Main.player[Main.myPlayer].inventory[k].IsTheSameAs(Main.recipe[i].requiredItem[j]))
                        {
                            num3 -= Main.player[Main.myPlayer].inventory[k].stack;
                        }
                        if (num3 <= 0)
                        {
                            break;
                        }
                    }
                    if (num3 > 0)
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    bool flag2 = true;
                    for (int j = 0; j < Recipe.maxRequirements; j++)
                    {
                        if (Main.recipe[i].requiredTile[j] == -1)
                        {
                            break;
                        }
                        if (!Main.player[Main.myPlayer].adjTile[Main.recipe[i].requiredTile[j]])
                        {
                            flag2 = false;
                            break;
                        }
                    }
                    if (flag2)
                    {
                        Main.availableRecipe[Main.numAvailableRecipes] = i;
                        Main.numAvailableRecipes++;
                    }
                }
            }
            for (int i = 0; i < Main.numAvailableRecipes; i++)
            {
                if (num == Main.availableRecipe[i])
                {
                    Main.focusRecipe = i;
                    break;
                }
            }
            if (Main.focusRecipe >= Main.numAvailableRecipes)
            {
                Main.focusRecipe = Main.numAvailableRecipes - 1;
            }
            if (Main.focusRecipe < 0)
            {
                Main.focusRecipe = 0;
            }
            float num4 = Main.availableRecipeY[Main.focusRecipe] - num2;
            for (int i = 0; i < Recipe.maxRecipes; i++)
            {
                Main.availableRecipeY[i] -= num4;
            }
        }
        public static void SetupRecipes()
        {
            Recipe.newRecipe.createItem.SetDefaults(28);
            Recipe.newRecipe.createItem.stack = 2;
            Recipe.newRecipe.requiredItem[0].SetDefaults(5);
            Recipe.newRecipe.requiredItem[1].SetDefaults(23);
            Recipe.newRecipe.requiredItem[1].stack = 2;
            Recipe.newRecipe.requiredItem[2].SetDefaults(31);
            Recipe.newRecipe.requiredItem[2].stack = 2;
            Recipe.newRecipe.requiredTile[0] = 13;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Healing Potion");
            Recipe.newRecipe.requiredItem[0].SetDefaults(28);
            Recipe.newRecipe.requiredItem[0].stack = 2;
            Recipe.newRecipe.requiredItem[1].SetDefaults(183);
            Recipe.newRecipe.requiredTile[0] = 13;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(110);
            Recipe.newRecipe.createItem.stack = 2;
            Recipe.newRecipe.requiredItem[0].SetDefaults(75);
            Recipe.newRecipe.requiredItem[1].SetDefaults(23);
            Recipe.newRecipe.requiredItem[1].stack = 2;
            Recipe.newRecipe.requiredItem[2].SetDefaults(31);
            Recipe.newRecipe.requiredItem[2].stack = 2;
            Recipe.newRecipe.requiredTile[0] = 13;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Mana Potion");
            Recipe.newRecipe.requiredItem[0].SetDefaults(110);
            Recipe.newRecipe.requiredItem[0].stack = 2;
            Recipe.newRecipe.requiredItem[1].SetDefaults(183);
            Recipe.newRecipe.requiredTile[0] = 13;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(226);
            Recipe.newRecipe.requiredItem[0].SetDefaults(28);
            Recipe.newRecipe.requiredItem[1].SetDefaults(110);
            Recipe.newRecipe.requiredTile[0] = 13;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(227);
            Recipe.newRecipe.requiredItem[0].SetDefaults("Healing Potion");
            Recipe.newRecipe.requiredItem[1].SetDefaults("Mana Potion");
            Recipe.newRecipe.requiredTile[0] = 13;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(67);
            Recipe.newRecipe.createItem.stack = 5;
            Recipe.newRecipe.requiredItem[0].SetDefaults(60);
            Recipe.newRecipe.requiredTile[0] = 13;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Bottle");
            Recipe.newRecipe.createItem.stack = 2;
            Recipe.newRecipe.requiredItem[0].SetDefaults("Glass");
            Recipe.newRecipe.requiredTile[0] = 17;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(8);
            Recipe.newRecipe.createItem.stack = 3;
            Recipe.newRecipe.requiredItem[0].SetDefaults(23);
            Recipe.newRecipe.requiredItem[0].stack = 1;
            Recipe.newRecipe.requiredItem[1].SetDefaults(9);
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(235);
            Recipe.newRecipe.requiredItem[0].SetDefaults(166);
            Recipe.newRecipe.requiredItem[1].SetDefaults(23);
            Recipe.newRecipe.requiredItem[1].stack = 5;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Glass");
            Recipe.newRecipe.createItem.stack = 1;
            Recipe.newRecipe.requiredItem[0].SetDefaults(169);
            Recipe.newRecipe.requiredItem[0].stack = 2;
            Recipe.newRecipe.requiredTile[0] = 17;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Clay Pot");
            Recipe.newRecipe.requiredItem[0].SetDefaults(133);
            Recipe.newRecipe.requiredItem[0].stack = 6;
            Recipe.newRecipe.requiredTile[0] = 17;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Gray Brick");
            Recipe.newRecipe.requiredItem[0].SetDefaults(3);
            Recipe.newRecipe.requiredItem[0].stack = 2;
            Recipe.newRecipe.requiredTile[0] = 17;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Gray Brick Wall");
            Recipe.newRecipe.createItem.stack = 4;
            Recipe.newRecipe.requiredItem[0].SetDefaults("Gray Brick");
            Recipe.newRecipe.requiredTile[0] = 18;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Red Brick");
            Recipe.newRecipe.requiredItem[0].SetDefaults(133);
            Recipe.newRecipe.requiredItem[0].stack = 2;
            Recipe.newRecipe.requiredTile[0] = 17;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Red Brick Wall");
            Recipe.newRecipe.createItem.stack = 4;
            Recipe.newRecipe.requiredItem[0].SetDefaults("Red Brick");
            Recipe.newRecipe.requiredTile[0] = 18;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Copper Brick");
            Recipe.newRecipe.requiredItem[0].SetDefaults(3);
            Recipe.newRecipe.requiredItem[0].stack = 1;
            Recipe.newRecipe.requiredItem[1].SetDefaults("Copper Ore");
            Recipe.newRecipe.requiredItem[1].stack = 1;
            Recipe.newRecipe.requiredTile[0] = 17;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Copper Brick Wall");
            Recipe.newRecipe.createItem.stack = 4;
            Recipe.newRecipe.requiredItem[0].SetDefaults("Copper Brick");
            Recipe.newRecipe.requiredTile[0] = 18;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Silver Brick Wall");
            Recipe.newRecipe.createItem.stack = 4;
            Recipe.newRecipe.requiredItem[0].SetDefaults("Silver Brick");
            Recipe.newRecipe.requiredTile[0] = 18;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Silver Brick");
            Recipe.newRecipe.requiredItem[0].SetDefaults(3);
            Recipe.newRecipe.requiredItem[0].stack = 1;
            Recipe.newRecipe.requiredItem[1].SetDefaults("Silver Ore");
            Recipe.newRecipe.requiredItem[1].stack = 1;
            Recipe.newRecipe.requiredTile[0] = 17;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Gold Brick Wall");
            Recipe.newRecipe.createItem.stack = 4;
            Recipe.newRecipe.requiredItem[0].SetDefaults("Gold Brick");
            Recipe.newRecipe.requiredTile[0] = 18;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Gold Brick");
            Recipe.newRecipe.requiredItem[0].SetDefaults(3);
            Recipe.newRecipe.requiredItem[0].stack = 1;
            Recipe.newRecipe.requiredItem[1].SetDefaults("Gold Ore");
            Recipe.newRecipe.requiredItem[1].stack = 1;
            Recipe.newRecipe.requiredTile[0] = 17;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Hellstone Brick");
            Recipe.newRecipe.requiredItem[0].SetDefaults(174);
            Recipe.newRecipe.requiredItem[1].SetDefaults(1);
            Recipe.newRecipe.requiredTile[0] = 17;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(192);
            Recipe.newRecipe.requiredItem[0].SetDefaults(173);
            Recipe.newRecipe.requiredItem[0].stack = 2;
            Recipe.newRecipe.requiredTile[0] = 17;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(30);
            Recipe.newRecipe.createItem.stack = 4;
            Recipe.newRecipe.requiredItem[0].SetDefaults(2);
            Recipe.newRecipe.requiredTile[0] = 18;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(26);
            Recipe.newRecipe.createItem.stack = 4;
            Recipe.newRecipe.requiredItem[0].SetDefaults(3);
            Recipe.newRecipe.requiredTile[0] = 18;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(93);
            Recipe.newRecipe.createItem.stack = 4;
            Recipe.newRecipe.requiredItem[0].SetDefaults(9);
            Recipe.newRecipe.requiredTile[0] = 18;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(94);
            Recipe.newRecipe.requiredItem[0].SetDefaults(9);
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(25);
            Recipe.newRecipe.requiredItem[0].SetDefaults(9);
            Recipe.newRecipe.requiredItem[0].stack = 6;
            Recipe.newRecipe.requiredTile[0] = 18;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(34);
            Recipe.newRecipe.requiredItem[0].SetDefaults(9);
            Recipe.newRecipe.requiredItem[0].stack = 4;
            Recipe.newRecipe.requiredTile[0] = 18;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Sign");
            Recipe.newRecipe.requiredItem[0].SetDefaults(9);
            Recipe.newRecipe.requiredItem[0].stack = 6;
            Recipe.newRecipe.requiredTile[0] = 18;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(48);
            Recipe.newRecipe.requiredItem[0].SetDefaults(9);
            Recipe.newRecipe.requiredItem[0].stack = 8;
            Recipe.newRecipe.requiredItem[1].SetDefaults(22);
            Recipe.newRecipe.requiredItem[1].stack = 2;
            Recipe.newRecipe.requiredTile[0] = 18;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(32);
            Recipe.newRecipe.requiredItem[0].SetDefaults(9);
            Recipe.newRecipe.requiredItem[0].stack = 8;
            Recipe.newRecipe.requiredTile[0] = 18;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(36);
            Recipe.newRecipe.requiredItem[0].SetDefaults(9);
            Recipe.newRecipe.requiredItem[0].stack = 10;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(24);
            Recipe.newRecipe.requiredItem[0].SetDefaults(9);
            Recipe.newRecipe.requiredItem[0].stack = 7;
            Recipe.newRecipe.requiredTile[0] = 18;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(196);
            Recipe.newRecipe.requiredItem[0].SetDefaults(9);
            Recipe.newRecipe.requiredItem[0].stack = 8;
            Recipe.newRecipe.requiredTile[0] = 18;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(40);
            Recipe.newRecipe.createItem.stack = 3;
            Recipe.newRecipe.requiredItem[0].SetDefaults(9);
            Recipe.newRecipe.requiredItem[1].SetDefaults(3);
            Recipe.newRecipe.requiredTile[0] = 18;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(39);
            Recipe.newRecipe.requiredItem[0].SetDefaults(9);
            Recipe.newRecipe.requiredItem[0].stack = 10;
            Recipe.newRecipe.requiredTile[0] = 18;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Bed");
            Recipe.newRecipe.requiredItem[0].SetDefaults(9);
            Recipe.newRecipe.requiredItem[0].stack = 15;
            Recipe.newRecipe.requiredItem[1].SetDefaults("Silk");
            Recipe.newRecipe.requiredItem[1].stack = 5;
            Recipe.newRecipe.requiredTile[0] = 18;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Silk");
            Recipe.newRecipe.requiredItem[0].SetDefaults(150);
            Recipe.newRecipe.requiredItem[0].stack = 10;
            Recipe.newRecipe.requiredTile[0] = 18;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Flaming Arrow");
            Recipe.newRecipe.createItem.stack = 5;
            Recipe.newRecipe.requiredItem[0].SetDefaults(40);
            Recipe.newRecipe.requiredItem[0].stack = 5;
            Recipe.newRecipe.requiredItem[1].SetDefaults(8);
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(33);
            Recipe.newRecipe.requiredItem[0].SetDefaults(3);
            Recipe.newRecipe.requiredItem[0].stack = 20;
            Recipe.newRecipe.requiredItem[1].SetDefaults(9);
            Recipe.newRecipe.requiredItem[1].stack = 4;
            Recipe.newRecipe.requiredItem[2].SetDefaults(8);
            Recipe.newRecipe.requiredItem[2].stack = 3;
            Recipe.newRecipe.requiredTile[0] = 18;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(20);
            Recipe.newRecipe.requiredItem[0].SetDefaults(12);
            Recipe.newRecipe.requiredItem[0].stack = 3;
            Recipe.newRecipe.requiredTile[0] = 17;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Copper Pickaxe");
            Recipe.newRecipe.requiredItem[0].SetDefaults(20);
            Recipe.newRecipe.requiredItem[0].stack = 12;
            Recipe.newRecipe.requiredItem[1].SetDefaults(9);
            Recipe.newRecipe.requiredItem[1].stack = 4;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Copper Axe");
            Recipe.newRecipe.requiredItem[0].SetDefaults(20);
            Recipe.newRecipe.requiredItem[0].stack = 9;
            Recipe.newRecipe.requiredItem[1].SetDefaults(9);
            Recipe.newRecipe.requiredItem[1].stack = 3;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Copper Hammer");
            Recipe.newRecipe.requiredItem[0].SetDefaults(20);
            Recipe.newRecipe.requiredItem[0].stack = 10;
            Recipe.newRecipe.requiredItem[1].SetDefaults(9);
            Recipe.newRecipe.requiredItem[1].stack = 3;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Copper Broadsword");
            Recipe.newRecipe.requiredItem[0].SetDefaults(20);
            Recipe.newRecipe.requiredItem[0].stack = 8;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Copper Shortsword");
            Recipe.newRecipe.requiredItem[0].SetDefaults(20);
            Recipe.newRecipe.requiredItem[0].stack = 7;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Copper Bow");
            Recipe.newRecipe.requiredItem[0].SetDefaults(20);
            Recipe.newRecipe.requiredItem[0].stack = 7;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Copper Helmet");
            Recipe.newRecipe.requiredItem[0].SetDefaults(20);
            Recipe.newRecipe.requiredItem[0].stack = 15;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Copper Chainmail");
            Recipe.newRecipe.requiredItem[0].SetDefaults(20);
            Recipe.newRecipe.requiredItem[0].stack = 25;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Copper Greaves");
            Recipe.newRecipe.requiredItem[0].SetDefaults(20);
            Recipe.newRecipe.requiredItem[0].stack = 20;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Copper Watch");
            Recipe.newRecipe.requiredItem[0].SetDefaults(20);
            Recipe.newRecipe.requiredItem[0].stack = 10;
            Recipe.newRecipe.requiredItem[1].SetDefaults(85);
            Recipe.newRecipe.requiredTile[0] = 14;
            Recipe.newRecipe.requiredTile[1] = 15;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Copper Chandelier");
            Recipe.newRecipe.requiredItem[0].SetDefaults(20);
            Recipe.newRecipe.requiredItem[0].stack = 4;
            Recipe.newRecipe.requiredItem[1].SetDefaults(8);
            Recipe.newRecipe.requiredItem[1].stack = 4;
            Recipe.newRecipe.requiredItem[2].SetDefaults(85);
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(22);
            Recipe.newRecipe.requiredItem[0].SetDefaults(11);
            Recipe.newRecipe.requiredItem[0].stack = 3;
            Recipe.newRecipe.requiredTile[0] = 17;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(35);
            Recipe.newRecipe.requiredItem[0].SetDefaults(22);
            Recipe.newRecipe.requiredItem[0].stack = 5;
            Recipe.newRecipe.requiredTile[0] = 18;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(205);
            Recipe.newRecipe.requiredItem[0].SetDefaults(22);
            Recipe.newRecipe.requiredItem[0].stack = 3;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(1);
            Recipe.newRecipe.requiredItem[0].SetDefaults(22);
            Recipe.newRecipe.requiredItem[0].stack = 12;
            Recipe.newRecipe.requiredItem[1].SetDefaults(9);
            Recipe.newRecipe.requiredItem[1].stack = 3;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(10);
            Recipe.newRecipe.requiredItem[0].SetDefaults(22);
            Recipe.newRecipe.requiredItem[0].stack = 9;
            Recipe.newRecipe.requiredItem[1].SetDefaults(9);
            Recipe.newRecipe.requiredItem[1].stack = 3;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(7);
            Recipe.newRecipe.requiredItem[0].SetDefaults(22);
            Recipe.newRecipe.requiredItem[0].stack = 10;
            Recipe.newRecipe.requiredItem[1].SetDefaults(9);
            Recipe.newRecipe.requiredItem[1].stack = 3;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(4);
            Recipe.newRecipe.requiredItem[0].SetDefaults(22);
            Recipe.newRecipe.requiredItem[0].stack = 8;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(6);
            Recipe.newRecipe.requiredItem[0].SetDefaults(22);
            Recipe.newRecipe.requiredItem[0].stack = 7;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Iron Bow");
            Recipe.newRecipe.requiredItem[0].SetDefaults(22);
            Recipe.newRecipe.requiredItem[0].stack = 7;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Iron Helmet");
            Recipe.newRecipe.requiredItem[0].SetDefaults(22);
            Recipe.newRecipe.requiredItem[0].stack = 20;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Iron Chainmail");
            Recipe.newRecipe.requiredItem[0].SetDefaults(22);
            Recipe.newRecipe.requiredItem[0].stack = 30;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Iron Greaves");
            Recipe.newRecipe.requiredItem[0].SetDefaults(22);
            Recipe.newRecipe.requiredItem[0].stack = 25;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Iron Chain");
            Recipe.newRecipe.requiredItem[0].SetDefaults(22);
            Recipe.newRecipe.requiredItem[0].stack = 3;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(21);
            Recipe.newRecipe.requiredItem[0].SetDefaults(14);
            Recipe.newRecipe.requiredItem[0].stack = 4;
            Recipe.newRecipe.requiredTile[0] = 17;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Silver Pickaxe");
            Recipe.newRecipe.requiredItem[0].SetDefaults(21);
            Recipe.newRecipe.requiredItem[0].stack = 12;
            Recipe.newRecipe.requiredItem[1].SetDefaults(9);
            Recipe.newRecipe.requiredItem[1].stack = 4;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Silver Axe");
            Recipe.newRecipe.requiredItem[0].SetDefaults(21);
            Recipe.newRecipe.requiredItem[0].stack = 9;
            Recipe.newRecipe.requiredItem[1].SetDefaults(9);
            Recipe.newRecipe.requiredItem[1].stack = 3;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Silver Hammer");
            Recipe.newRecipe.requiredItem[0].SetDefaults(21);
            Recipe.newRecipe.requiredItem[0].stack = 10;
            Recipe.newRecipe.requiredItem[1].SetDefaults(9);
            Recipe.newRecipe.requiredItem[1].stack = 3;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Silver Broadsword");
            Recipe.newRecipe.requiredItem[0].SetDefaults(21);
            Recipe.newRecipe.requiredItem[0].stack = 8;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Silver Bow");
            Recipe.newRecipe.requiredItem[0].SetDefaults(21);
            Recipe.newRecipe.requiredItem[0].stack = 7;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Silver Helmet");
            Recipe.newRecipe.requiredItem[0].SetDefaults(21);
            Recipe.newRecipe.requiredItem[0].stack = 20;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Silver Chainmail");
            Recipe.newRecipe.requiredItem[0].SetDefaults(21);
            Recipe.newRecipe.requiredItem[0].stack = 30;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Silver Greaves");
            Recipe.newRecipe.requiredItem[0].SetDefaults(21);
            Recipe.newRecipe.requiredItem[0].stack = 25;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Silver Watch");
            Recipe.newRecipe.requiredItem[0].SetDefaults(21);
            Recipe.newRecipe.requiredItem[0].stack = 10;
            Recipe.newRecipe.requiredItem[1].SetDefaults(85);
            Recipe.newRecipe.requiredTile[0] = 14;
            Recipe.newRecipe.requiredTile[1] = 15;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Silver Chandelier");
            Recipe.newRecipe.requiredItem[0].SetDefaults(21);
            Recipe.newRecipe.requiredItem[0].stack = 4;
            Recipe.newRecipe.requiredItem[1].SetDefaults(8);
            Recipe.newRecipe.requiredItem[1].stack = 4;
            Recipe.newRecipe.requiredItem[2].SetDefaults(85);
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(19);
            Recipe.newRecipe.requiredItem[0].SetDefaults(13);
            Recipe.newRecipe.requiredItem[0].stack = 4;
            Recipe.newRecipe.requiredTile[0] = 17;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Gold Pickaxe");
            Recipe.newRecipe.requiredItem[0].SetDefaults(19);
            Recipe.newRecipe.requiredItem[0].stack = 12;
            Recipe.newRecipe.requiredItem[1].SetDefaults(9);
            Recipe.newRecipe.requiredItem[1].stack = 4;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Gold Axe");
            Recipe.newRecipe.requiredItem[0].SetDefaults(19);
            Recipe.newRecipe.requiredItem[0].stack = 9;
            Recipe.newRecipe.requiredItem[1].SetDefaults(9);
            Recipe.newRecipe.requiredItem[1].stack = 3;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Gold Hammer");
            Recipe.newRecipe.requiredItem[0].SetDefaults(19);
            Recipe.newRecipe.requiredItem[0].stack = 10;
            Recipe.newRecipe.requiredItem[1].SetDefaults(9);
            Recipe.newRecipe.requiredItem[1].stack = 3;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Gold Broadsword");
            Recipe.newRecipe.requiredItem[0].SetDefaults(19);
            Recipe.newRecipe.requiredItem[0].stack = 8;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Gold Shortsword");
            Recipe.newRecipe.requiredItem[0].SetDefaults(19);
            Recipe.newRecipe.requiredItem[0].stack = 7;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Gold Bow");
            Recipe.newRecipe.requiredItem[0].SetDefaults(19);
            Recipe.newRecipe.requiredItem[0].stack = 7;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Gold Helmet");
            Recipe.newRecipe.requiredItem[0].SetDefaults(19);
            Recipe.newRecipe.requiredItem[0].stack = 25;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Gold Chainmail");
            Recipe.newRecipe.requiredItem[0].SetDefaults(19);
            Recipe.newRecipe.requiredItem[0].stack = 35;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Gold Greaves");
            Recipe.newRecipe.requiredItem[0].SetDefaults(19);
            Recipe.newRecipe.requiredItem[0].stack = 30;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Gold Watch");
            Recipe.newRecipe.requiredItem[0].SetDefaults(19);
            Recipe.newRecipe.requiredItem[0].stack = 10;
            Recipe.newRecipe.requiredItem[1].SetDefaults(85);
            Recipe.newRecipe.requiredTile[0] = 14;
            Recipe.newRecipe.requiredTile[1] = 15;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Gold Chandelier");
            Recipe.newRecipe.requiredItem[0].SetDefaults(19);
            Recipe.newRecipe.requiredItem[0].stack = 4;
            Recipe.newRecipe.requiredItem[1].SetDefaults(8);
            Recipe.newRecipe.requiredItem[1].stack = 4;
            Recipe.newRecipe.requiredItem[2].SetDefaults(85);
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Candle");
            Recipe.newRecipe.requiredItem[0].SetDefaults(19);
            Recipe.newRecipe.requiredItem[1].SetDefaults(8);
            Recipe.newRecipe.requiredTile[0] = 18;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(57);
            Recipe.newRecipe.requiredItem[0].SetDefaults(56);
            Recipe.newRecipe.requiredItem[0].stack = 4;
            Recipe.newRecipe.requiredTile[0] = 17;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(44);
            Recipe.newRecipe.requiredItem[0].SetDefaults(57);
            Recipe.newRecipe.requiredItem[0].stack = 8;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Unholy Arrow");
            Recipe.newRecipe.createItem.stack = 2;
            Recipe.newRecipe.requiredItem[0].SetDefaults(40);
            Recipe.newRecipe.requiredItem[0].stack = 2;
            Recipe.newRecipe.requiredItem[1].SetDefaults(69);
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(45);
            Recipe.newRecipe.requiredItem[0].SetDefaults(57);
            Recipe.newRecipe.requiredItem[0].stack = 10;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(46);
            Recipe.newRecipe.requiredItem[0].SetDefaults(57);
            Recipe.newRecipe.requiredItem[0].stack = 10;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Shadow Helmet");
            Recipe.newRecipe.requiredItem[0].SetDefaults(57);
            Recipe.newRecipe.requiredItem[0].stack = 15;
            Recipe.newRecipe.requiredItem[1].SetDefaults(86);
            Recipe.newRecipe.requiredItem[1].stack = 10;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Shadow Scalemail");
            Recipe.newRecipe.requiredItem[0].SetDefaults(57);
            Recipe.newRecipe.requiredItem[0].stack = 25;
            Recipe.newRecipe.requiredItem[1].SetDefaults(86);
            Recipe.newRecipe.requiredItem[1].stack = 20;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Shadow Greaves");
            Recipe.newRecipe.requiredItem[0].SetDefaults(57);
            Recipe.newRecipe.requiredItem[0].stack = 20;
            Recipe.newRecipe.requiredItem[1].SetDefaults(86);
            Recipe.newRecipe.requiredItem[1].stack = 15;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Nightmare Pickaxe");
            Recipe.newRecipe.requiredItem[0].SetDefaults(57);
            Recipe.newRecipe.requiredItem[0].stack = 12;
            Recipe.newRecipe.requiredItem[1].SetDefaults(86);
            Recipe.newRecipe.requiredItem[1].stack = 6;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("The Breaker");
            Recipe.newRecipe.requiredItem[0].SetDefaults(57);
            Recipe.newRecipe.requiredItem[0].stack = 10;
            Recipe.newRecipe.requiredItem[1].SetDefaults(86);
            Recipe.newRecipe.requiredItem[1].stack = 5;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Grappling Hook");
            Recipe.newRecipe.requiredItem[0].SetDefaults(85);
            Recipe.newRecipe.requiredItem[0].stack = 3;
            Recipe.newRecipe.requiredItem[1].SetDefaults(118);
            Recipe.newRecipe.requiredItem[1].stack = 1;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(117);
            Recipe.newRecipe.requiredItem[0].SetDefaults(116);
            Recipe.newRecipe.requiredItem[0].stack = 6;
            Recipe.newRecipe.requiredTile[0] = 17;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(198);
            Recipe.newRecipe.requiredItem[0].SetDefaults(117);
            Recipe.newRecipe.requiredItem[0].stack = 20;
            Recipe.newRecipe.requiredItem[1].SetDefaults(177);
            Recipe.newRecipe.requiredItem[1].stack = 10;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(199);
            Recipe.newRecipe.requiredItem[0].SetDefaults(117);
            Recipe.newRecipe.requiredItem[0].stack = 20;
            Recipe.newRecipe.requiredItem[1].SetDefaults(178);
            Recipe.newRecipe.requiredItem[1].stack = 10;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(200);
            Recipe.newRecipe.requiredItem[0].SetDefaults(117);
            Recipe.newRecipe.requiredItem[0].stack = 20;
            Recipe.newRecipe.requiredItem[1].SetDefaults(179);
            Recipe.newRecipe.requiredItem[1].stack = 10;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(201);
            Recipe.newRecipe.requiredItem[0].SetDefaults(117);
            Recipe.newRecipe.requiredItem[0].stack = 20;
            Recipe.newRecipe.requiredItem[1].SetDefaults(181);
            Recipe.newRecipe.requiredItem[1].stack = 10;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(202);
            Recipe.newRecipe.requiredItem[0].SetDefaults(117);
            Recipe.newRecipe.requiredItem[0].stack = 20;
            Recipe.newRecipe.requiredItem[1].SetDefaults(182);
            Recipe.newRecipe.requiredItem[1].stack = 10;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(203);
            Recipe.newRecipe.requiredItem[0].SetDefaults(117);
            Recipe.newRecipe.requiredItem[0].stack = 20;
            Recipe.newRecipe.requiredItem[1].SetDefaults(180);
            Recipe.newRecipe.requiredItem[1].stack = 10;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(204);
            Recipe.newRecipe.requiredItem[0].SetDefaults(117);
            Recipe.newRecipe.requiredItem[0].stack = 35;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(127);
            Recipe.newRecipe.requiredItem[0].SetDefaults(95);
            Recipe.newRecipe.requiredItem[1].SetDefaults(117);
            Recipe.newRecipe.requiredItem[1].stack = 30;
            Recipe.newRecipe.requiredItem[2].SetDefaults(75);
            Recipe.newRecipe.requiredItem[2].stack = 10;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(197);
            Recipe.newRecipe.requiredItem[0].SetDefaults(98);
            Recipe.newRecipe.requiredItem[1].SetDefaults(117);
            Recipe.newRecipe.requiredItem[1].stack = 20;
            Recipe.newRecipe.requiredItem[2].SetDefaults(75);
            Recipe.newRecipe.requiredItem[2].stack = 5;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Meteor Helmet");
            Recipe.newRecipe.requiredItem[0].SetDefaults(117);
            Recipe.newRecipe.requiredItem[0].stack = 25;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Meteor Suit");
            Recipe.newRecipe.requiredItem[0].SetDefaults(117);
            Recipe.newRecipe.requiredItem[0].stack = 35;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Meteor Leggings");
            Recipe.newRecipe.requiredItem[0].SetDefaults(117);
            Recipe.newRecipe.requiredItem[0].stack = 30;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Meteor Shot");
            Recipe.newRecipe.createItem.stack = 100;
            Recipe.newRecipe.requiredItem[0].SetDefaults(117);
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(151);
            Recipe.newRecipe.requiredItem[0].SetDefaults(154);
            Recipe.newRecipe.requiredItem[0].stack = 25;
            Recipe.newRecipe.requiredItem[1].SetDefaults(150);
            Recipe.newRecipe.requiredItem[1].stack = 40;
            Recipe.newRecipe.requiredTile[0] = 18;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(152);
            Recipe.newRecipe.requiredItem[0].SetDefaults(154);
            Recipe.newRecipe.requiredItem[0].stack = 35;
            Recipe.newRecipe.requiredItem[1].SetDefaults(150);
            Recipe.newRecipe.requiredItem[1].stack = 50;
            Recipe.newRecipe.requiredTile[0] = 18;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(153);
            Recipe.newRecipe.requiredItem[0].SetDefaults(154);
            Recipe.newRecipe.requiredItem[0].stack = 30;
            Recipe.newRecipe.requiredItem[1].SetDefaults(150);
            Recipe.newRecipe.requiredItem[1].stack = 45;
            Recipe.newRecipe.requiredTile[0] = 18;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(175);
            Recipe.newRecipe.requiredItem[0].SetDefaults(174);
            Recipe.newRecipe.requiredItem[0].stack = 6;
            Recipe.newRecipe.requiredItem[1].SetDefaults(173);
            Recipe.newRecipe.requiredItem[1].stack = 2;
            Recipe.newRecipe.requiredTile[0] = 77;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(119);
            Recipe.newRecipe.requiredItem[0].SetDefaults(175);
            Recipe.newRecipe.requiredItem[0].stack = 15;
            Recipe.newRecipe.requiredItem[1].SetDefaults(55);
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(120);
            Recipe.newRecipe.requiredItem[0].SetDefaults(175);
            Recipe.newRecipe.requiredItem[0].stack = 25;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(121);
            Recipe.newRecipe.requiredItem[0].SetDefaults(175);
            Recipe.newRecipe.requiredItem[0].stack = 35;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(122);
            Recipe.newRecipe.requiredItem[0].SetDefaults(175);
            Recipe.newRecipe.requiredItem[0].stack = 35;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(217);
            Recipe.newRecipe.requiredItem[0].SetDefaults(175);
            Recipe.newRecipe.requiredItem[0].stack = 35;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(219);
            Recipe.newRecipe.requiredItem[0].SetDefaults(175);
            Recipe.newRecipe.requiredItem[0].stack = 20;
            Recipe.newRecipe.requiredItem[1].SetDefaults("Handgun");
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(231);
            Recipe.newRecipe.requiredItem[0].SetDefaults(175);
            Recipe.newRecipe.requiredItem[0].stack = 30;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(232);
            Recipe.newRecipe.requiredItem[0].SetDefaults(175);
            Recipe.newRecipe.requiredItem[0].stack = 40;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(233);
            Recipe.newRecipe.requiredItem[0].SetDefaults(175);
            Recipe.newRecipe.requiredItem[0].stack = 35;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(190);
            Recipe.newRecipe.requiredItem[0].SetDefaults("Silver Broadsword");
            Recipe.newRecipe.requiredItem[1].SetDefaults(208);
            Recipe.newRecipe.requiredItem[1].stack = 40;
            Recipe.newRecipe.requiredItem[2].SetDefaults(209);
            Recipe.newRecipe.requiredItem[2].stack = 20;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(191);
            Recipe.newRecipe.requiredItem[0].SetDefaults(208);
            Recipe.newRecipe.requiredItem[0].stack = 40;
            Recipe.newRecipe.requiredItem[1].SetDefaults(209);
            Recipe.newRecipe.requiredItem[1].stack = 30;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(185);
            Recipe.newRecipe.requiredItem[0].SetDefaults(84);
            Recipe.newRecipe.requiredItem[1].SetDefaults(208);
            Recipe.newRecipe.requiredItem[1].stack = 30;
            Recipe.newRecipe.requiredItem[2].SetDefaults(210);
            Recipe.newRecipe.requiredItem[2].stack = 3;
            Recipe.newRecipe.requiredTile[0] = 16;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Depth Meter");
            Recipe.newRecipe.requiredItem[0].SetDefaults(20);
            Recipe.newRecipe.requiredItem[0].stack = 10;
            Recipe.newRecipe.requiredItem[1].SetDefaults(21);
            Recipe.newRecipe.requiredItem[1].stack = 8;
            Recipe.newRecipe.requiredItem[2].SetDefaults(19);
            Recipe.newRecipe.requiredItem[2].stack = 6;
            Recipe.newRecipe.requiredTile[0] = 14;
            Recipe.newRecipe.requiredTile[1] = 15;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(193);
            Recipe.newRecipe.requiredItem[0].SetDefaults(173);
            Recipe.newRecipe.requiredItem[0].stack = 20;
            Recipe.newRecipe.requiredTile[0] = 17;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Goggles");
            Recipe.newRecipe.requiredItem[0].SetDefaults(38);
            Recipe.newRecipe.requiredItem[0].stack = 2;
            Recipe.newRecipe.requiredTile[0] = 18;
            Recipe.newRecipe.requiredTile[1] = 15;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Sunglasses");
            Recipe.newRecipe.requiredItem[0].SetDefaults("Black Lens");
            Recipe.newRecipe.requiredItem[0].stack = 2;
            Recipe.newRecipe.requiredTile[0] = 18;
            Recipe.newRecipe.requiredTile[1] = 15;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults("Mana Crystal");
            Recipe.newRecipe.requiredItem[0].SetDefaults(75);
            Recipe.newRecipe.requiredItem[0].stack = 10;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(43);
            Recipe.newRecipe.requiredItem[0].SetDefaults(38);
            Recipe.newRecipe.requiredItem[0].stack = 10;
            Recipe.newRecipe.requiredTile[0] = 26;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(70);
            Recipe.newRecipe.requiredItem[0].SetDefaults(67);
            Recipe.newRecipe.requiredItem[0].stack = 30;
            Recipe.newRecipe.requiredItem[1].SetDefaults(68);
            Recipe.newRecipe.requiredItem[1].stack = 15;
            Recipe.newRecipe.requiredTile[0] = 26;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(71);
            Recipe.newRecipe.createItem.stack = 100;
            Recipe.newRecipe.requiredItem[0].SetDefaults(72);
            Recipe.newRecipe.requiredItem[0].stack = 1;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(72);
            Recipe.newRecipe.createItem.stack = 1;
            Recipe.newRecipe.requiredItem[0].SetDefaults(71);
            Recipe.newRecipe.requiredItem[0].stack = 100;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(72);
            Recipe.newRecipe.createItem.stack = 100;
            Recipe.newRecipe.requiredItem[0].SetDefaults(73);
            Recipe.newRecipe.requiredItem[0].stack = 1;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(73);
            Recipe.newRecipe.createItem.stack = 1;
            Recipe.newRecipe.requiredItem[0].SetDefaults(72);
            Recipe.newRecipe.requiredItem[0].stack = 100;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(73);
            Recipe.newRecipe.createItem.stack = 100;
            Recipe.newRecipe.requiredItem[0].SetDefaults(74);
            Recipe.newRecipe.requiredItem[0].stack = 1;
            Recipe.addRecipe();
            Recipe.newRecipe.createItem.SetDefaults(74);
            Recipe.newRecipe.createItem.stack = 1;
            Recipe.newRecipe.requiredItem[0].SetDefaults(73);
            Recipe.newRecipe.requiredItem[0].stack = 100;
            Recipe.addRecipe();
        }
    }
}
