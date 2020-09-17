using System;
using System.Collections.Generic;

namespace Zork
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");

            Commands command = Commands.UNKNOWN;
            while (command != Commands.QUIT)
            {
                Console.WriteLine(Rooms[CurrentRoomIndex]);
                Console.Write(">");
                command = ToCommand(Console.ReadLine().Trim());

                switch (command)
                {
                    case Commands.QUIT:
                        Console.WriteLine("Thank you for playing!");
                        break;

                    case Commands.LOOK:
                        Console.WriteLine("This is an open field west of a white house, with a boarded front door.\nA rubber mat saying 'Welcome to Zork!' lies by the door");
                        break;

                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:
                        if (Move(command) == false)
                        {
                            Console.WriteLine("The way is shut!");
                        }
                        break;

                    default:
                        Console.WriteLine("Unknown command.");
                        break;
                }
            }
        }

        private static bool Move(Commands command)

        {
            bool isValidMove = true;
            switch (command)
            {
                case Commands.EAST when CurrentRoomIndex < Rooms.Length - 1:
                    CurrentRoomIndex++;
                    break;


                case Commands.WEST when CurrentRoomIndex > 0:
                    CurrentRoomIndex--;
                    break;

                default:
                    isValidMove = false;
                    break;

            }
            return isValidMove;
        }
        private static string[] Rooms =
        {
            "Forest",
            "West of House",
            "Behind House",
            "Clearing",
            "Canyon"
        };
        private static Commands ToCommand(string commandString) => Enum.TryParse<Commands>(commandString, true, out Commands result) ? result : Commands.UNKNOWN;
        private static int CurrentRoomIndex = 1;
    }
}
