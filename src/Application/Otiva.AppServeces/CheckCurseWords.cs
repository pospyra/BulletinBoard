using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.AppServeces
{
    public static class CheckCurseWords
    {
        private static List<string> WordsList = new List<string>
    {
        "блядь",
        "блять",
        "ебать",
        "пизда",
        "хуй",
        "хули",
        "хуёво",
        "пошел нахуй",
        "хуй",
        "пидорас",
        "хуйло",
        "хуйлуша",
        "мразота",
        "ебаная",
        "конченный",
        "конченнная",
        "конченное",
        "ебаное"
    };

        public static void CheckCurseWord(string text)
        {
            foreach(var word in WordsList)
            {
                if(text.Contains(word))
                    throw new Exception("Использование нецензурной лексики");
            }
        }
    }
}
