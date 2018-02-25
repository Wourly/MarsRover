using System;
using FluentAssertions;
using Xunit;

namespace MarsRover.Tests
{
     public class Tests
    {
        // String to Position
        [Theory]
        [InlineData("1 1 N", 1, 1, Direction.N)]
        [InlineData("2 4 E", 2, 4, Direction.E)]
        [InlineData("-3 -3 W", -3, -3, Direction.W)]
        [InlineData("2 1 S", 2, 1, Direction.S)]
        [InlineData("496 1384 S", 496, 1384, Direction.S)]
        public void CoordsInStringAreConvertedToPositionType(string String, int sampleX, int sampleY, Direction sampleDir)
        {
            // -- arrange
            var rover = new Rover();

            // -- assert
            rover.GetDestination(String).ShouldBeEquivalentTo(new Position { X = sampleX, Y = sampleY, Direction = sampleDir });
        }

        // Commands recorded as string
        [Theory]
        [InlineData("RMLM")]
        [InlineData("MMLMRRMLMMRMM")]
        [InlineData("RRRRRRMMMLLLMMM")]
        [InlineData("MMRRLMMRRLMMLLMRR")]
        public void CommandsDataAreRecordedAsArrayListAndConvertedBackToString(string String)
        {
            // -- arrange
            var rover = new Rover();

            // -- act 
            rover.Move(String);

            // -- assert
            rover.CommandsToString().Should().Be(String);
        }

        // Should rover even move?
        [Theory]
        [InlineData(0, 0, 0, 0, true, true)]
        [InlineData(-1, 0, 0, 0, false, true)]
        [InlineData(4916, 6, 19, 0, false, false)]
        [InlineData(66, -1, 66, 2, true, false)]

        public void EqualityInCoords(int posX, int posY, int destX, int destY, bool truthX, bool truthY)
        {
            // -- arrange
            var rover = new Rover
            {
                Position = new Position { X = posX, Y = posY, Direction = Direction.W },
                Destination = new Position { X = destX, Y = destY, Direction = Direction.W }
            };

            // -- assert
            rover.PosEqualDestX().ShouldBeEquivalentTo(truthX);
            rover.PosEqualDestY().ShouldBeEquivalentTo(truthY);
        }

        [Fact]
        public void EqualityInCoordsAccordingToDirections()
        {
            // -- arrange
            var rover = new Rover
            {
                Position = new Position { X = 0, Y = 0, Direction = Direction.N },
                Destination = new Position { X = 0, Y = 0, Direction = Direction.N }
            };

            // -- assert
            rover.DestNorth().ShouldBeEquivalentTo(false);
            rover.DestSouth().ShouldBeEquivalentTo(false);
            rover.DestEast().ShouldBeEquivalentTo(false);
            rover.DestWest().ShouldBeEquivalentTo(false);
        }

        [Fact]
        public void DestinationCoordinatesSouthWestFromPositionCoordinates()
        {
            // -- arrange
            var rover = new Rover
            {
                Position = new Position { X = 0, Y = 0, Direction = Direction.W },
                Destination = new Position { X = -1, Y = -1, Direction = Direction.E }
            };

            // -- assert
            rover.DestNorth().ShouldBeEquivalentTo(false);
            rover.DestSouth().ShouldBeEquivalentTo(true);
            rover.DestEast().ShouldBeEquivalentTo(false);
            rover.DestWest().ShouldBeEquivalentTo(true);
        }

        [Fact]
        public void DestinationCoordinatesSouthOnlyFromPositionCoordinates()
        {
            // -- arrange
            var rover = new Rover
            {
                Position = new Position { X = 0, Y = 0, Direction = Direction.S },
                Destination = new Position { X = 0, Y = -1, Direction = Direction.N }
            };

            // -- assert
            rover.DestNorth().ShouldBeEquivalentTo(false);
            rover.DestSouth().ShouldBeEquivalentTo(true);
            rover.DestEast().ShouldBeEquivalentTo(false);
            rover.DestWest().ShouldBeEquivalentTo(false);
        }

        [Fact]
        public void DestinationCoordinatesNorthEastFromPositionCoordinates()
        {
            // -- arrange
            var rover = new Rover
            {
                Position = new Position { X = 0, Y = 0, Direction = Direction.N },
                Destination = new Position { X = 1, Y = 1, Direction = Direction.N }
            };

            // -- assert
            rover.DestNorth().ShouldBeEquivalentTo(true);
            rover.DestSouth().ShouldBeEquivalentTo(false);
            rover.DestEast().ShouldBeEquivalentTo(true);
            rover.DestWest().ShouldBeEquivalentTo(false);
        }

        // -- Checking where is Rover heading
        // ----------------------------------

        [Fact]
        public void RoverIsHeadingToNorth()
        {
            // -- arrange
            var rover = new Rover
            {
                Position = new Position { X = 0, Y = 0, Direction = Direction.N }
            };

            // -- assert
            rover.PosToNorth().ShouldBeEquivalentTo(true);
            rover.PosToSouth().ShouldBeEquivalentTo(false);
            rover.PosToEast().ShouldBeEquivalentTo(false);
            rover.PosToWest().ShouldBeEquivalentTo(false);
        }

        [Fact]
        public void RoverIsHeadingToSouth()
        {
            // -- arrange
            var rover = new Rover
            {
                Position = new Position { X = 0, Y = 0, Direction = Direction.S }
            };

            // -- assert
            rover.PosToNorth().ShouldBeEquivalentTo(false);
            rover.PosToSouth().ShouldBeEquivalentTo(true);
            rover.PosToEast().ShouldBeEquivalentTo(false);
            rover.PosToWest().ShouldBeEquivalentTo(false);
        }

        [Fact]
        public void RoverIsHeadingToEast()
        {
            // -- arrange
            var rover = new Rover
            {
                Position = new Position { X = 0, Y = 0, Direction = Direction.E }
            };

            // -- assert
            rover.PosToNorth().ShouldBeEquivalentTo(false);
            rover.PosToSouth().ShouldBeEquivalentTo(false);
            rover.PosToEast().ShouldBeEquivalentTo(true);
            rover.PosToWest().ShouldBeEquivalentTo(false);
        }

        [Fact]
        public void RoverIsHeadingToWest()
        {
            // -- arrange
            var rover = new Rover
            {
                Position = new Position { X = 0, Y = 0, Direction = Direction.W }
            };

            // -- assert
            rover.PosToNorth().ShouldBeEquivalentTo(false);
            rover.PosToSouth().ShouldBeEquivalentTo(false);
            rover.PosToEast().ShouldBeEquivalentTo(false);
            rover.PosToWest().ShouldBeEquivalentTo(true);
        }

        // -- Checking where should Rover be heading
        // -----------------------------------------

        [Fact]
        public void RoverShouldBeHeadingToNorth()
        {
            // -- arrange
            var rover = new Rover
            {
                Destination = new Position { X = 0, Y = 0, Direction = Direction.N }
            };

            // -- assert
            rover.DestToNorth().ShouldBeEquivalentTo(true);
            rover.DestToSouth().ShouldBeEquivalentTo(false);
            rover.DestToEast().ShouldBeEquivalentTo(false);
            rover.DestToWest().ShouldBeEquivalentTo(false);
        }

        [Fact]
        public void RoverShouldBeHeadingToSouth()
        {
            // -- arrange
            var rover = new Rover
            {
                Destination = new Position { X = 0, Y = 0, Direction = Direction.S }
            };

            // -- assert
            rover.DestToNorth().ShouldBeEquivalentTo(false);
            rover.DestToSouth().ShouldBeEquivalentTo(true);
            rover.DestToEast().ShouldBeEquivalentTo(false);
            rover.DestToWest().ShouldBeEquivalentTo(false);
        }

        [Fact]
        public void RoverShouldBeHeadingToEast()
        {
            // -- arrange
            var rover = new Rover
            {
                Destination = new Position { X = 0, Y = 0, Direction = Direction.E }
            };

            // -- assert
            rover.DestToNorth().ShouldBeEquivalentTo(false);
            rover.DestToSouth().ShouldBeEquivalentTo(false);
            rover.DestToEast().ShouldBeEquivalentTo(true);
            rover.DestToWest().ShouldBeEquivalentTo(false);
        }

        [Fact]
        public void RoverIsShouldBeHeadingToWest()
        {
            // -- arrange
            var rover = new Rover
            {
                Destination = new Position { X = 0, Y = 0, Direction = Direction.W }
            };

            // -- assert
            rover.DestToNorth().ShouldBeEquivalentTo(false);
            rover.DestToSouth().ShouldBeEquivalentTo(false);
            rover.DestToEast().ShouldBeEquivalentTo(false);
            rover.DestToWest().ShouldBeEquivalentTo(true);
        }

        // Homework
        // Rover finds it's way and saves actions
        // --------------------------------------

        [Theory]

        [InlineData(0, 0, Direction.N, 0, 0, Direction.N, "")]

        [InlineData(0, 0, Direction.N, 0, 1, Direction.N, "M")]
        [InlineData(0, 0, Direction.N, 0, 0, Direction.E, "R")]
        [InlineData(0, 0, Direction.N, 0, 0, Direction.W, "L")]
        [InlineData(0, 0, Direction.N, 0, 0, Direction.S, "RR")] //Rover turns right twice, if 180° rotation needed

        [InlineData(0, 0, Direction.N, 0, 3, Direction.N, "MMM")]
        [InlineData(0, 2, Direction.S, 0, -2, Direction.S, "MMMM")]
        [InlineData(0, 0, Direction.N, 0, -2, Direction.S, "RRMM")]
        [InlineData(0, 0, Direction.W, 0, 6, Direction.N, "RMMMMMM")]

        [InlineData(0, 0, Direction.E, 5, 0, Direction.E, "MMMMM")]
        [InlineData(0, 0, Direction.E, -5, 0, Direction.E, "RRMMMMMRR")]

        [InlineData(0, 0, Direction.N, 2, 0, Direction.S, "RMMR")]

        // Required paths
        [InlineData(0, 0, Direction.N, -2, 3, Direction.E, "MMMLMMRR")]
        [InlineData(-1, -1, Direction.W, -2, -2, Direction.S, "LMRML")]
        [InlineData(5, -3, Direction.N, -10, -13, Direction.S, "RRMMMMMMMMMMRMMMMMMMMMMMMMMML")]

        public void RoverCanGoFromPositionToDirection(
            int startX, int startY, Direction startDir,
            int endX, int endY, Direction endDir,
            string Commands)
        {

            // -- arrange
            var rover = new Rover
            {
                Position = new Position { X = startX, Y = startY, Direction = startDir },
                Destination = new Position { X = endX, Y = endY, Direction = endDir }
            };

            // -- act

            rover.LocateToDestination();

            // -- assert

            rover.CommandsToString().Should().Be(Commands);


        }


        // Code made together
        //-------------------

        [Fact]
        public void InitialPositionsShouldBe_0_0_N()
        {
            // -- arrange

            // -- act 
            var initialPosition = new Rover().Position;

            // -- assert
            initialPosition.ShouldBeEquivalentTo(
                new Position { X = 0, Y = 0, Direction = Direction.N });
        }


        [Fact]
        public void GivenEmptyInstructionWeStayInPlace()
        {
            // -- arrange
            var rover = new Rover();
            var initialPosition = rover.Position;

            // -- act
            rover.Move("");

            // -- assert
            var finalPosition = rover.Position;
            finalPosition.ShouldBeEquivalentTo(initialPosition, "because we did not move");
        }



        [Fact]
        public void MovingFromDefaultPositionToNorth()
        {
            // -- arrange
            var rover = new Rover();

            // -- act 
            rover.Move("M");

            // -- assert
            var finalPosition = rover.Position;
            finalPosition.ShouldBeEquivalentTo(new Position { X = 0, Y = 1, Direction = Direction.N }
             , "because we moved forward from the default position");
        }

        [Fact]
        public void TurningRightFromDefaultPosition()
        {
            // -- arrange
            var rover = new Rover();

            // -- act 
            rover.Move("R");

            // -- assert
            var finalPosition = rover.Position;
            finalPosition.ShouldBeEquivalentTo(
                new Position { X = 0, Y = 0, Direction = Direction.E }
             , o => o.ComparingEnumsByName());
        }
        [Fact]
        public void TurningLeftFromDefaultPosition()
        {
            // -- arrange
            var rover = new Rover();

            // -- act 
            rover.Move("L");

            // -- assert
            var finalPosition = rover.Position;
            finalPosition.ShouldBeEquivalentTo(

                new Position
                {
                    X = 0,
                    Y = 0,
                    Direction = Direction.W
                }

             , o => o.ComparingEnumsByName());
        }

        [Fact]
        public void MovingFrom11E()
        {
            // -- arrange
            var rover = new Rover
            {
                Position = new Position
                {
                    X = 1,
                    Y = 1,
                    Direction = Direction.E
                }
            };

            // -- act 
            rover.Move("M");

            // -- assert
            var finalPosition = rover.Position;
            finalPosition.ShouldBeEquivalentTo(

                new Position
                {
                    X = 2,
                    Y = 1,
                    Direction = Direction.E
                }

             , o => o.ComparingEnumsByName());
        }

        [Theory]
        [InlineData(Direction.N, Direction.E)]
        [InlineData(Direction.E, Direction.S)]
        [InlineData(Direction.S, Direction.W)]
        [InlineData(Direction.W, Direction.N)]
        public void TurningRight(Direction start, Direction end)
        {
            // -- arrange
            var rover = new Rover
            {
                Position = new Position
                {
                    Direction = start
                }
            };

            // -- act 
            rover.Move("R");

            // -- assert
            rover.Position.Direction.Should().Be(end);

        }

        [Theory]
        [InlineData(Direction.N, Direction.W)]
        [InlineData(Direction.W, Direction.S)]
        [InlineData(Direction.S, Direction.E)]
        [InlineData(Direction.E, Direction.N)]
        public void TurningLeft(Direction start,
            Direction end)
        {
            // -- arrange
            var rover = new Rover
            {
                Position = new Position
                {
                    Direction = start
                }
            };

            // -- act 
            rover.Move("L");

            // -- assert
            rover.Position.Direction.Should().Be(end);

        }

        [Theory]
        [InlineData(0, 1, Direction.N)]
        [InlineData(5, 6, Direction.N)]
        [InlineData(0, -1, Direction.S)]
        [InlineData(-5, -6, Direction.S)]
        public void MoveUpAndDown(int startY, int endY, Direction direction)
        {
            // -- arrange
            var rover = new Rover
            {
                Position = new Position
                {
                    Direction = direction,
                    Y = startY
                }
            };

            // -- act 
            rover.Move("M");

            // -- assert
            rover.Position.Y.Should().Be(endY);
        }

        [Theory]
        [InlineData(0, -1, Direction.W)]
        [InlineData(-5, -6, Direction.W)]
        [InlineData(0, 1, Direction.E)]
        [InlineData(5, 6, Direction.E)]
        public void MoveLeftAndRight(int startX, int endX, Direction direction)
        {
            // -- arrange
            var rover = new Rover
            {
                Position = new Position
                {
                    Direction = direction,
                    X = startX
                }
            };

            // -- act 
            rover.Move("M");

            // -- assert
            rover.Position.X.Should().Be(endX);
        }

        [Theory]
        [InlineData("RMLM", 1, 1, Direction.N)]
        [InlineData("MMLMRRMLMMRMM", 2, 4, Direction.E)]
        [InlineData("RRRRRRMMMLLLMMM", -3, -3, Direction.W)]
        [InlineData("MMRRLMMRRLMMLLMRR", 2, 1, Direction.S)]
        public void ProcessFourInstructions(string instructions, int x, int y, Direction direction)
        {
            // -- arrange
            var rover = new Rover();

            var endPosition = new Position { X = x, Y = y, Direction = direction };
            // -- act 
            rover.Move(instructions);

            // -- assert
            rover.Position.ShouldBeEquivalentTo(endPosition);
        }
    }
}