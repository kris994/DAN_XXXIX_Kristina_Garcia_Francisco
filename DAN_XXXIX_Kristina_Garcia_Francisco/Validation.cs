using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_XXXIX_Kristina_Garcia_Francisco
{
    class Validation
    {
        /// <summary>
        /// The word can not be empty
        /// </summary>
        public string CheckIfNullOrEmpty()
        {
            string word = Console.ReadLine();
            BackToMainMenu(word);
            while (string.IsNullOrEmpty(word) || string.IsNullOrWhiteSpace(word))
            {
                Console.Write("The input cannot be empty. Please try again: ");
                word = Console.ReadLine();
                BackToMainMenu(word);
            }
            return word;
        }

        /// <summary>
        /// Valid positive int input
        /// </summary>
        public int ValidPositiveNumber(int maxNumber)
        {
            string s = Console.ReadLine();
            BackToMainMenu(s);
            int Num;
            bool b = Int32.TryParse(s, out Num);
            while (!b || (Num < 0 || Num > maxNumber))
            {
                Console.Write("Invalid input. Try again: ");
                s = Console.ReadLine();
                BackToMainMenu(s);
                b = Int32.TryParse(s, out Num);
            }
            return Num;
        }

        public string YesNo()
        {
            string choose = Console.ReadLine().ToLower();

            BackToMainMenu(choose);
            while (choose != "yes" && choose != "no")
            {
                Console.Write("Invalid input. Try again: ");
                BackToMainMenu(choose);
                choose = Console.ReadLine().ToLower();
            }

            return choose;
        }

        /// <summary>
        /// Returns the user back to the main menu
        /// </summary>
        /// <param name="input">input that returns the user</param>
        public void BackToMainMenu(string input)
        {
            if (input.ToLower() == "b")
            {
                Program.Main(null);
            }
        }
    }
}
