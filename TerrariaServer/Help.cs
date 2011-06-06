using System;
using System.Collections;
namespace Terraria
{
    public class Help
    {
        private static System.Collections.ArrayList arr = new System.Collections.ArrayList();
        public static void add(string var, string var2)
        {
            Help.arr.Add(var + " - " + var2);
        }
        public static void add(string var)
        {
            Help.arr.Add(var);
        }
        public static int getPageCount()
        {
            return (int)System.Math.Floor((double)Help.arr.Count / 5.0);
        }
        public static string getHelpPage(int page)
        {
            string result;
            if (page > Help.getPageCount())
            {
                result = "";
            }
            else
            {
                string text = string.Concat(new object[]
				{
					"Page ", 
					page, 
					" of ", 
					Help.getPageCount()
				});
                for (int i = page * 5; i < page * 5 + 5; i++)
                {
                    if (Help.arr.Count <= i)
                    {
                        break;
                    }
                    text = text + System.Environment.NewLine + Help.arr[i];
                }
                result = text;
            }
            return result;
        }
    }
}
