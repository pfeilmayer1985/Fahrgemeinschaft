using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fahrgemeinschaft
{
    public class HandleUserInputC
    {
        public HandleUserInputC()
        { }

        public string HandleUserTextInput(bool checkSpecialCharacter=false)
        {
            string userInput = "";

            bool pressedRightKey = true;
            do
            {
                string inputToBeChecked = Console.ReadLine();
                if (checkSpecialCharacter && inputToBeChecked.Any(ch => !Char.IsLetterOrDigit(ch)))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\nYou need help, mate! Are you for real?\nHow about you take your pills and try once again: ");
                    Console.ResetColor();
                    continue;

                }

                if (string.IsNullOrWhiteSpace(inputToBeChecked))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\nYou need help, mate! How about you take your pills and try once again: ");
                    Console.ResetColor();
                }
                else
                {
                    pressedRightKey = false;
                    userInput = inputToBeChecked.TrimEnd().TrimStart();
                }

            } while (pressedRightKey);
            return userInput;
        }

        public int HandleUserNumbersInput()
        {
            int userInput = 0;

            bool itIsntANumber = true;
            do
            {
                string inputToBeChecked = Console.ReadLine();

                inputToBeChecked = inputToBeChecked.TrimEnd().TrimStart();

                if (!int.TryParse(inputToBeChecked, out userInput))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\nYou need help, mate!\nHow about you take your pills and try once again: ");
                    Console.ResetColor();
                    continue;
                }
                else
                {
                    itIsntANumber = false;
                    userInput = Convert.ToInt32(inputToBeChecked.TrimEnd().TrimStart());
                }

                if (userInput < 1 || userInput > 9)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\nThis a Carpool App, not social media. Try again: ");
                    Console.ResetColor();
                    itIsntANumber = true;

                }


            } while (itIsntANumber);
            return userInput;
        }
    }


}
