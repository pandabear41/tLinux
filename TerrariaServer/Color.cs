using System;
namespace Terraria
{
    public struct Color
    {
        public byte R
        {
            get;
            set;
        }
        public byte G
        {
            get;
            set;
        }
        public byte B
        {
            get;
            set;
        }
        public byte A
        {
            get;
            set;
        }
        public static Color Black
        {
            get;
            private set;
        }
        public static Color White
        {
            get;
            private set;
        }
        public Color(int r, int g, int b)
        {
            this = new Color((byte)r, (byte)g, (byte)b);
        }
        public Color(int r, int g, int b, int a)
        {
            this = new Color((byte)r, (byte)g, (byte)b, (byte)a);
        }
        public Color(byte r, byte g, byte b)
        {
            this = default(Color);
            this.R = r;
            this.G = g;
            this.B = b;
            this.A = 1;
        }
        public Color(byte r, byte g, byte b, byte a)
        {
            this = default(Color);
            this.R = r;
            this.G = g;
            this.B = b;
            this.A = a;
        }
        static Color()
        {
            Color.Black = new Color(0, 0, 0, 1);
            Color.White = new Color(1, 1, 1, 1);
        }
    }
}
