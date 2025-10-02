using System;

class Program
{
    static void Main()
    {
        bool continueLoop = true;

        while (continueLoop)
        {
            Console.Write("Please enter a number: ");
            
            // Try to read and parse the input as an integer
            if (int.TryParse(Console.ReadLine(), out int number))
            {
                if (number % 2 == 0)
                {
                    Console.WriteLine($"{number} is even.");
                }
                else
                {
                    Console.WriteLine($"{number} is odd.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid integer.");
            }

            // Ask if the user wants to continue
            Console.Write("Do you want to enter another number? (y/n): ");
            string response = Console.ReadLine()?.Trim().ToLower();

            // Continue only if the user types 'y' or 'yes'
            continueLoop = (response == "y" || response == "yes");
        }

        Console.WriteLine("Thank you for using the program!");
    }
}