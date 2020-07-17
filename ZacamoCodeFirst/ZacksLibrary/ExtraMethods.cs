using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZacksLibrary
{
    public static class ExtraMethods
    {
        public static int ValidInt(string number)
        {
            int validInt;
            bool isNum = int.TryParse(number, out validInt);
            while (!isNum)
            {
                Console.WriteLine("Please Enter A Valid Number");
                isNum = int.TryParse(Console.ReadLine(), out validInt);
            }

            return validInt;
        }

        public static double ValidDouble(string number)
        {
            double validDouble;
            bool isNum = double.TryParse(number, out validDouble);
            while (!isNum)
            {
                Console.WriteLine("Please Enter A Valid Number");
                isNum = double.TryParse(Console.ReadLine(), out validDouble);
            }

            return validDouble;
        }

        public static string TitleString(string phrase)
        {
            string[] words = phrase.Split();
            List<string> newPhrase = new List<string>();

            foreach (string word in words)
            {
                newPhrase.Add(char.ToUpper(word[0]) + word.Substring(1));
            }

            string titleString = string.Join(" ", newPhrase);

            return titleString;
        }
    }
}
