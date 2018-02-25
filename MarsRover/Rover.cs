using System;
using System.Collections;

namespace MarsRover
{
    public class Rover
    {

        // X, Y, Direction of Position (where does Rover stay)
        public Position Position { get; set; } = new Position();

        // X, Y, Direction of Destinastion (where should Rover go)
        public Position Destination { get; set; } = new Position();

        // What commands has Rover underwent
        public ArrayList Commands = new ArrayList();

        // On which axes rover wants to go

        public bool PosEqualDestX()
        {
            return Position.X == Destination.X;
        }

        public bool PosEqualDestY()
        {
            return Position.Y == Destination.Y;
        }

        // Relativity of current position to final destination

        public bool DestNorth()
        {
            return Position.Y < Destination.Y;
        }

        public bool DestSouth()
        {
            return Position.Y > Destination.Y;
        }

        public bool DestEast()
        {
            return Position.X < Destination.X;
        }

        public bool DestWest()
        {
            return Position.X > Destination.X;
        }

        // Which direction is rover facing at it's current position

        public bool PosToNorth()
        {
            return Position.Direction == Direction.N;
        }

        public bool PosToSouth()
        {
            return Position.Direction == Direction.S;
        }

        public bool PosToEast()
        {
            return Position.Direction == Direction.E;
        }

        public bool PosToWest()
        {
            return Position.Direction == Direction.W;
        }

        // Which direction is rover facing at it's destination

        public bool DestToNorth()
        {
            return Destination.Direction == Direction.N;
        }

        public bool DestToSouth()
        {
            return Destination.Direction == Direction.S;
        }

        public bool DestToEast()
        {
            return Destination.Direction == Direction.E;
        }
        public bool DestToWest()
        {
            return Destination.Direction == Direction.W;
        }

        public string CommandsToString()
        {
            char[] CharArray = Commands.ToArray(typeof(char)) as char[];

            return new string(CharArray);
        }

        // Converting string data to Position type

        public Position GetDestination(string Input)
        {

            try
            {
                string[] StringArray = Input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (StringArray.Length > 3)
                { throw new FormatException(); }

                int intX = Int32.Parse(StringArray[0]);
                int intY = Int32.Parse(StringArray[1]);
                var direction = new Direction { };

                if (StringArray[2] == "N")
                { direction = Direction.N; }
                else if (StringArray[2] == "E")
                { direction = Direction.E; }
                else if (StringArray[2] == "S")
                { direction = Direction.S; }
                else if (StringArray[2] == "W")
                { direction = Direction.W; }
                else
                { throw new FormatException(); }

                return new Position { X = intX, Y = intY, Direction = direction };
            }
            catch (Exception)
            {

                Console.WriteLine("Wrong format input!");

                return new Position { X = Position.X, Y = Position.Y, Direction = Position.Direction };


            }
        }

        // Creates ArrayList of commands required to locate to from Position to Destination parameters
        public void LocateToDestination()
        {
            ClearCommands();

            if (!PosEqualDestX() || !PosEqualDestY())
            {

                // Moving on Y axis

                while (!PosEqualDestY())
                {

                    if (PosToSouth() && DestNorth())
                    {
                        Move("RR");
                    }

                    if (PosToNorth() && DestSouth())
                    {
                        Move("RR");
                    }

                    if (PosToEast() && DestNorth())
                    {
                        Move("L");
                    }

                    if (PosToEast() && DestSouth())
                    {
                        Move("R");
                    }

                    if (PosToWest() && DestNorth())
                    {
                        Move("R");
                    }

                    if (PosToWest() && DestSouth())
                    {
                        Move("L");
                    }

                    while (DestNorth() && PosToNorth())
                    {
                        Move("M");
                    }

                    while (DestSouth() && PosToSouth())
                    {
                        Move("M");
                    }
                }

                // -- Moving on X axis

                while (!PosEqualDestX())
                {
                    if (PosToWest() && DestEast())
                    {
                        Move("RR");
                    }

                    if (PosToEast() && DestWest())
                    {
                        Move("RR");
                    }

                    if (PosToNorth() && DestEast())
                    {
                        Move("R");
                    }

                    if (PosToNorth() && DestWest())
                    {
                        Move("L");
                    }

                    if (PosToSouth() && DestEast())
                    {
                        Move("L");
                    }

                    if (PosToSouth() && DestWest())
                    {
                        Move("R");
                    }

                    while (DestEast() && PosToEast())
                    {
                        Move("M");
                    }

                    while (DestWest() && PosToWest())
                    {
                        Move("M");
                    }
                }
            }

            // Rotation at the end of movement

            // Rover wants to be face north

            if (DestToNorth() && PosToEast())
            {
                Move("L");
            }

            if (DestToNorth() && PosToSouth())
            {
                Move("RR");
            }

            if (DestToNorth() && PosToWest())
            {
                Move("R");
            }

            // Rover wants to be face east

            if (DestToEast() && PosToNorth())
            {
                Move("R");
            }

            if (DestToEast() && PosToSouth())
            {
                Move("L");
            }

            if (DestToEast() && PosToWest())
            {
                Move("RR");
            }

            // Rover wants to be face south

            if (DestToSouth() && PosToNorth())
            {
                Move("RR");
            }

            if (DestToSouth() && PosToEast())
            {
                Move("R");
            }

            if (DestToSouth() && PosToWest())
            {
                Move("L");
            }

            // Rover wants to be face west

            if (DestToWest() && PosToNorth())
            {
                Move("L");
            }

            if (DestToWest() && PosToEast())
            {
                Move("RR");
            }

            if (DestToWest() && PosToSouth())
            {
                Move("R");
            }
        }

        public void ClearCommands()
        {
            Commands = new ArrayList();
        }

        // Code made together

        public void Move(string instructions)
        {
            foreach (var instruction in instructions.ToCharArray())
            {
                MoveOnce(instruction);
            }
        }

        public void MoveOnce(char instruction)
        {
            if (instruction == 'M')
            {
                if (Position.Direction == Direction.N)
                { Position.Y += 1; }
                if (Position.Direction == Direction.E)
                { Position.X += 1; }
                if (Position.Direction == Direction.S)
                { Position.Y -= 1; }
                if (Position.Direction == Direction.W)
                { Position.X -= 1; }
            }

            if (instruction == 'R')
            {
                var d = Position.Direction + 1;
                if (d == (Direction)4)
                {
                    d = Direction.N;
                }
                Position.Direction = d;
            }

            if (instruction == 'L')
            {
                var d = Position.Direction - 1;
                if (d == (Direction)(-1))
                {
                    d = Direction.W;
                }
                Position.Direction = d;
            }

            Commands.Add(instruction);
        }

    };

    public class Position
    {
        public Direction Direction { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }

    public enum Direction
    {
        N,
        E,
        S,
        W
    }
}