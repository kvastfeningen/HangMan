using System;
using System.Collections.Generic;
using static System.Random;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace HangMan
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();

            }

        }

        private static bool MainMenu()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("1)  Hangman");

            Console.Write("\n\n");

            Console.WriteLine("s)  Sluta");
            Console.WriteLine(" ");

            Console.Write("\r\nVälj en funktion: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Hangman();
                    return true;

                case "s":
                    return false;
                default:
                    return true;
            }
        }


        static void Hangman()
            {
            Console.WriteLine();
            Console.WriteLine("Nu kör vi hänga gubbe!");
            Console.WriteLine();

            var lines = File.ReadAllLines("SvOrdlista1.txt");
                var r = new Random();
                int randomLineNumber = r.Next(0, lines.Length - 1);
                string theWord = lines[randomLineNumber];

            
            //Bildar char array för rätta tecken
            char[] rL = new char[theWord.Length];
            for (int i = 0; i < theWord.Length; i++)
            {
                rL[i] = '_';
            }
            //Visa antalet bokstäver i ordet
            foreach (char c in rL)
                Console.Write(c + " ");
            Console.WriteLine();


            int guesses = 1;
            int lengthOfTheWord = theWord.Length;
            int rightLetters = 0;
            StringBuilder guessedLetters = new StringBuilder();
            
            while (guesses < 10 && rightLetters != lengthOfTheWord)
            {
                Console.WriteLine();
                Console.WriteLine("Gissning " + guesses);
                Console.WriteLine();
                Console.WriteLine("Dessa har du provat: " + guessedLetters);
                Console.WriteLine();
                Console.WriteLine("Gissa en bokstav (eller hela ordet): ");
                

                string inputString = Console.ReadLine();
                inputString = inputString.ToLower();
               
                bool ok = false;
                   while (!ok)
                    {
                    if (string.IsNullOrEmpty(inputString))
                    {
                        Console.WriteLine("Du måste skriva in nåt med SMÅ bokstäver!");

                        inputString = Console.ReadLine();
                    }

                    else if (int.TryParse(inputString, out int n))
                    {
                        Console.WriteLine("Du måste skriva in nåt med SMÅ bokstäver!");

                        inputString = Console.ReadLine();

                    }

                    else if (inputString.Length == 1 && guessedLetters.ToString().Contains(inputString))
                    {
                        Console.WriteLine("Du har redan gissat på den här bokstaven!");

                        inputString = Console.ReadLine();
                       
                    }
                    else
                    {
                        ok = true;
                    }
                        
                    }
                

                if (inputString.Length > 1)
                {
                     
                    if(inputString == theWord) {
                        Console.WriteLine("Du vann!");
                        break;
                    }
                    else
                    {
                        guesses += 1;
                    }
                }

                else if (inputString.Length == 1)
                {
                    bool rLPlus = false;
                    char input = inputString[0];

                    guessedLetters.Append(input + " ");

                    for (int i = 0; i < theWord.Length; i++)
                {
                    if (input == theWord[i])
                    {
                        rL[i] = theWord[i];
                            if (guessedLetters.Length >= 2)
                            {
                                guessedLetters.Remove(guessedLetters.Length - 2, 2);

                            }
                          
                            rightLetters += 1;
                            rLPlus = true;
                     }
                       
                }
                  if (!rLPlus)
                    {
                        guesses += 1;
                    }    
                }
               
                


                foreach (char c in rL)
                    Console.Write(c + " ");
                Console.WriteLine();
            
            } //end while

            if (guesses == 10)
            {
                Console.WriteLine();
                Console.WriteLine("Inga fler försök. Du förlorade!");
                Console.WriteLine();
                Console.WriteLine("Ordet var: {0}", theWord);
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Du vann!!");
            }
            System.Console.ReadLine();

        }

        
    }
}
