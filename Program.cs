using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static string randomCode = "";
    public static string code = "";
    public static int tries = 10;

    public static bool isSpecial = false;
    public static bool isNum = false;
    public static int charCode = 4;
    public static void Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("Welcome to mastermind!");
        Console.WriteLine("In this game, you must attempt to guess the 4 digit code... You have 10 tries.\n");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("*you can choose to edit this*");
        Settings();
        MakeCode();
        VerifyCode();
    }

    public static void Settings()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("If you would like to change the difficulty of the game, please type y/n: ");

        if (Console.ReadLine().ToLower() == "y")
        {
            Console.WriteLine("Would you like to have any special characters in the code (y/n)? ");
            isSpecial = Console.ReadLine().ToLower() == "y" ? true : false;

            Console.WriteLine("Would you like to have any numbers in your code (y/n)? ");
            isNum = Console.ReadLine().ToLower() == "y" ? true : false;

            while (true)
            {
                try
                {
                    Console.WriteLine("How long do you want your code to be? ");
                    charCode = Convert.ToInt32(Console.ReadLine().ToLower());

                    if (charCode == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("You cannot enter 0 as an answer.");
                        continue;
                    }
                    else if (charCode >= 25)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("You cannot have an answer greater than 25.");
                        continue;
                    }
                } catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Please try again.");
                    continue;
                }
                
                break;
            }
            while (true)
            {
                try
                {
                    Console.WriteLine("How many tries do you want to have? ");
                    tries = Convert.ToInt32(Console.ReadLine().ToLower());
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR please try again.");
                    continue;
                }
                break;
            }
        }
    }

    public static void MakeCode()
    {
        Random rnd = new Random();
        string uppercaseCodes = "ABCDEFGHIJKLMNOPQRSTUVWXYZ12345678910";
        string numberCodes = uppercaseCodes + "0123456789";
        string specialCodes = uppercaseCodes + "!@#$%^&*();':/[]{}";
        string specialNumCodes = uppercaseCodes + numberCodes + specialCodes;


        if (isSpecial && !isNum)
        {
            for (int x = 0; x < charCode; x++)
            {
                code += specialCodes[rnd.Next(0, specialCodes.Length)];
                randomCode += specialCodes[rnd.Next(0, specialCodes.Length)];
            }
        } else if (isNum && !isSpecial)
        {
            for (int x = 0; x < charCode; x++)
            {
                code += numberCodes[rnd.Next(0, numberCodes.Length)];
                randomCode += numberCodes[rnd.Next(0, numberCodes.Length)];
            }
        } else if (isNum && isSpecial)
        {
            for (int x = 0; x < charCode; x++)
            {
                code += specialNumCodes[rnd.Next(0, specialNumCodes.Length)];
                randomCode += specialNumCodes[rnd.Next(0, specialNumCodes.Length)];
            }
        } else
        {
            for (int x = 0; x < charCode; x++)
            {
                code += uppercaseCodes[rnd.Next(0, uppercaseCodes.Length)];
                randomCode += uppercaseCodes[rnd.Next(0, uppercaseCodes.Length)];
            }
        }
        

        randomCode += code;
        randomCode = randomCode.Scramble();
    }

    public static void VerifyCode()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"These will help you: {randomCode}");
        Thread.Sleep(1000);
        Console.WriteLine("Guess the full code. Good luck.");

        int correctPos = 0;
        int wrongPos = 0;
        string input = "";
        // X Y C G
        // Y K S C
        bool isDone = false;
        while (tries > 0)
        {
            Console.WriteLine("Please enter the code. (to quit, type q): ");
            correctPos = 0;
            wrongPos = 0;
            input = Console.ReadLine().ToUpper();

            isDone = false;

            if (input == "Q")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Thanks for coming!.");
                Environment.Exit(1);
                break;
            }

            if (input.Length < charCode || input.Length > charCode)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Please try again.");
                continue;
            }

            for (int x = 0; x < input.Length; x++)
            {
                for (int y = 0; y < code.Length; y++)
                {
                    if (input[x] == code[y] && x == y)
                    {
                        isDone = true;
                        correctPos++;
                    }
                    else if (input[x] == code[y] && x != y && !isDone)
                    {
                        wrongPos++;
                    }
                }
            }

            if (correctPos >= charCode)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Congratulations, you won!");
                break;
            }

            tries--;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("You have " + tries + " tries left.");
            Console.WriteLine("Correct positions: " + correctPos + " | Incorrect positions: " + wrongPos);
        }

        if (tries <= 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You lost the game! The code was " + code);
        }

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Would you like to play again? (y/n): ");
        if (Console.ReadLine().ToLower() == "y")
        {
            tries = 10;
            code = "";
            randomCode = "";
            Settings();
            MakeCode();
            VerifyCode();
        } else
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Thanks for playing!");
        }
    }
}

public static class Extensions
{
    public static string Scramble(this string s)
    {
        return new string(s.ToCharArray().OrderBy(x => Guid.NewGuid()).ToArray());
    }
}