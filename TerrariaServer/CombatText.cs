using System;
namespace Terraria
{
    public class CombatText
    {
        public bool active;
        public float alpha;
        public int alphaDir = 1;
        public Color color;
        public int lifeTime = 0;
        public Vector2 position;
        public float rotation;
        public float scale = 1f;
        public string text;
        public Vector2 velocity;
        public static void NewText(Rectangle location, Color color, string text)
        {
            if (Main.netMode != 2)
            {
                for (int i = 0; i < 100; i++)
                {
                    if (!Main.combatText[i].active)
                    {
                        Vector2 vector = default(Vector2);
                        Main.combatText[i].alpha = 1f;
                        Main.combatText[i].alphaDir = -1;
                        Main.combatText[i].active = true;
                        Main.combatText[i].scale = 0f;
                        Main.combatText[i].rotation = 0f;
                        Main.combatText[i].position.X = (float)location.X + (float)location.Width * 0.5f - vector.X * 0.5f;
                        Main.combatText[i].position.Y = (float)location.Y + (float)location.Height * 0.25f - vector.Y * 0.5f;
                        CombatText expr_FA_cp_0 = Main.combatText[i];
                        expr_FA_cp_0.position.X = expr_FA_cp_0.position.X + (float)Main.rand.Next(-(int)((double)location.Width * 0.5), (int)((double)location.Width * 0.5) + 1);
                        CombatText expr_146_cp_0 = Main.combatText[i];
                        expr_146_cp_0.position.Y = expr_146_cp_0.position.Y + (float)Main.rand.Next(-(int)((double)location.Height * 0.5), (int)((double)location.Height * 0.5) + 1);
                        Main.combatText[i].color = color;
                        Main.combatText[i].text = text;
                        Main.combatText[i].velocity.Y = -7f;
                        Main.combatText[i].lifeTime = 60;
                        break;
                    }
                }
            }
        }
        public void Update()
        {
            if (this.active)
            {
                this.alpha += (float)this.alphaDir * 0.05f;
                if ((double)this.alpha <= 0.6)
                {
                    this.alpha = 0.6f;
                    this.alphaDir = 1;
                }
                if (this.alpha >= 1f)
                {
                    this.alpha = 1f;
                    this.alphaDir = -1;
                }
                this.velocity.Y = this.velocity.Y * 0.92f;
                this.velocity.X = this.velocity.X * 0.93f;
                this.position += this.velocity;
                this.lifeTime--;
                if (this.lifeTime <= 0)
                {
                    this.scale -= 0.1f;
                    if ((double)this.scale < 0.1)
                    {
                        this.active = false;
                    }
                    this.lifeTime = 0;
                }
                else
                {
                    if (this.scale < 1f)
                    {
                        this.scale += 0.1f;
                    }
                    if (this.scale > 1f)
                    {
                        this.scale = 1f;
                    }
                }
            }
        }
        public static void UpdateCombatText()
        {
            for (int i = 0; i < 100; i++)
            {
                if (Main.combatText[i].active)
                {
                    Main.combatText[i].Update();
                }
            }
        }
    }
}
