using Newtonsoft.Json;
using System.IO;
using System;

namespace Zork
{
    public class Game
    {
        public World World { get; private set; }

        [JsonIgnore]
        public Player Player { get; private set; }

        [JsonIgnore]
        private bool IsRunning { get; set; }
        public Game(World world, Player player)
        {
            World = world;
            Player = player;
        }
        public static Game Load(string filename)
        {
            Game game = JsonConvert.DeserializeObject<Game>(File.ReadAllText(filename));
            game.Player = game.World.SpawnPlayer();

            return game;
        }
        public void Run()
        {
            IsRunning = true;
            Room previousRoom = null;
            while (IsRunning)
            {
                Console.WriteLine(Player.Location);
                if (previousRoom != Player.Location)
                {
                    Console.WriteLine(Player.Location.Description);
                    previousRoom = Player.Location;
                }
                Console.Write("\n> ");
                Commands command = ToCommand(Console.ReadLine().Trim());

                switch (command)
                {
                    case Commands.QUIT:
                        IsRunning = false;
                        break;

                    case Commands.LOOK:
                        Console.WriteLine(Player.Location.Description);
                        break;

                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:

                        Directions direction = Enum.Parse<Directions>(command.ToString(), true);
                        if (Player.Move(direction) == false)
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
        private static Commands ToCommand(string commandString) => Enum.TryParse<Commands>(commandString, true, out Commands result) ? result : Commands.UNKNOWN;
    }
}
