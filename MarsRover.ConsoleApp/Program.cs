using System;
using System.Collections;

namespace MarsRover.ConsoleApp

{

    class Program
    {
        static void Main()
        {

            var rover = new Rover();

            // For calculations of two coordinates, but not overwriting coordinates of "real" rover 
            var rover2 = new Rover();

            Console.WriteLine("For command input, type: 'command'.");
            Console.WriteLine("For automatic navigation, type: 'locate'.");
            Console.WriteLine("For solutions of homework commands, type: 'homework'.");
            Console.WriteLine("To get back to this menu, type: 'back'.");
            Console.WriteLine();
            Console.WriteLine($"Current position is: X: {rover.Position.X}, Y: {rover.Position.Y}, facing: {rover.Position.Direction}.");

            while (true)
            {
                Console.WriteLine("");

                string instructions = Console.ReadLine();

                // -- commanding "M, L, R"

                if (instructions == "command")
                {
                    Console.WriteLine("Input commands: 'L' to turn left, 'R' to turn right, 'M' to move forward.");
                    Console.WriteLine();

                    while (instructions != "back")
                    {
                        Console.WriteLine($"X: {rover.Position.X}, Y: {rover.Position.Y}, to {rover.Position.Direction}");

                        instructions = Console.ReadLine();

                        if (instructions != "back")
                        {
                            rover.Move(instructions);
                        }
                    }
                }

                // -- finding path to location

                if (instructions == "locate")
                {
                    Console.WriteLine("Input destination coordinates and direction in format:");
                    Console.WriteLine("X Y D");
                    Console.WriteLine("D is direction, substitute it with 'N, E, S, W' as 'North, East, South, West'");
                    Console.WriteLine();

                    while (instructions != "back")
                    {
                        Console.WriteLine($"X: {rover.Position.X}, Y: {rover.Position.Y}, to {rover.Position.Direction}");

                        instructions = Console.ReadLine();

                        if (instructions != "back")
                        {
                            rover.Destination = rover.GetDestination(instructions);

                            rover.LocateToDestination();

                            Console.WriteLine(rover.CommandsToString());
                            Console.WriteLine();
                        }
                    }
                }

                if (instructions == "homework")
                {

                        while (instructions != "back")
                        {

                            Console.WriteLine("Input position and destination coordinates and direction in format:");
                            Console.WriteLine("X Y D");
                            Console.WriteLine("D is direction, substitute it with 'N, E, S, W' as 'North, East, South, West'");
                            Console.WriteLine();

                            rover2.Position = new Position { X = 0, Y = 0, Direction = Direction.N };
                            rover2.Destination = new Position { X = -2, Y = 3, Direction = Direction.E };

                                Console.WriteLine("Position:");
                                Console.WriteLine($"X: {rover2.Position.X}, Y: {rover2.Position.Y}, to {rover2.Position.Direction}");
                                Console.WriteLine("Destination:");
                                Console.WriteLine($"X: {rover2.Destination.X}, Y: {rover2.Destination.Y}, to {rover2.Destination.Direction}");
                                rover2.LocateToDestination();
                                Console.WriteLine(rover2.CommandsToString());
                                Console.WriteLine();

                            rover2.Position = new Position { X = -1, Y = -1, Direction = Direction.W };
                            rover2.Destination = new Position { X = -2, Y = -2, Direction = Direction.S };

                                Console.WriteLine("Position:");
                                Console.WriteLine($"X: {rover2.Position.X}, Y: {rover2.Position.Y}, to {rover2.Position.Direction}");
                                Console.WriteLine("Destination:");
                                Console.WriteLine($"X: {rover2.Destination.X}, Y: {rover2.Destination.Y}, to {rover2.Destination.Direction}");
                                rover2.LocateToDestination();
                                Console.WriteLine(rover2.CommandsToString());
                                Console.WriteLine();

                            rover2.Position = new Position { X = 5, Y = -3, Direction = Direction.N };
                            rover2.Destination = new Position { X = -10, Y = -13, Direction = Direction.S };

                                Console.WriteLine("Position:");
                                Console.WriteLine($"X: {rover2.Position.X}, Y: {rover2.Position.Y}, to {rover2.Position.Direction}");
                                Console.WriteLine("Destination:");
                                Console.WriteLine($"X: {rover2.Destination.X}, Y: {rover2.Destination.Y}, to {rover2.Destination.Direction}");
                                rover2.LocateToDestination();
                                Console.WriteLine(rover2.CommandsToString());
                                Console.WriteLine();


                        while (instructions != "back")
                            {
                                Console.WriteLine("Input start coordinates:");

                                instructions = Console.ReadLine();

                                if (instructions != "back")
                                {
                                    rover2.Position = (rover2.GetDestination(instructions));

                                    Console.WriteLine("Input end coordinates:");

                                    instructions = Console.ReadLine();

                                    if (instructions != "back")
                                    {
                                        rover2.Destination = (rover2.GetDestination(instructions));

                                        rover2.LocateToDestination();

                                        Console.WriteLine(rover2.CommandsToString());
                                        Console.WriteLine();
                                    }
                                }
                            }
                        }
                }
                if (instructions == "back")
                {
                    Console.WriteLine();
                    Console.WriteLine("Back to menu.");
                }
            }
        }
    }
}
