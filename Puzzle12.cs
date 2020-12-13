using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public static class Puzzle12
    {
        #region Part 1
        public static void SolvePuzzle12_P1(string inputfile)
        {
            List<KeyValuePair<char, int>> route = new List<KeyValuePair<char, int>>();
            using (StreamReader sr = new StreamReader(inputfile))
            {
                string buffer = sr.ReadLine();
                while (buffer != null)
                {
                    route.Add(new KeyValuePair<char, int>(buffer[0], int.Parse(buffer.Substring(1))));
                    buffer = sr.ReadLine();
                }
            }
            NavigateP1(route);
        }

        public static void NavigateP1(List<KeyValuePair<char, int>> route)
        {
            int NS = 0;
            int EW = 0;
            int direction = 90;
            foreach (var step in route)
            {
               // Console.WriteLine($"D: {direction} - {step.Key}{step.Value}");
                switch (step.Key)
                {
                    case 'N':
                        Move(0, ref NS, ref EW, step.Value);
                        break;
                    case 'E':
                        Move(90, ref NS, ref EW, step.Value);
                        break;
                    case 'S':
                        Move(180, ref NS, ref EW, step.Value);
                        break;
                    case 'W':
                        Move(270, ref NS, ref EW, step.Value);
                        break;
                    case 'L':
                    case 'R':
                        direction = ChangeDirection(direction, step.Key, step.Value);
                        break;
                    case 'F':
                        Move(direction, ref NS, ref EW, step.Value);
                        break;
                }
            }
            Console.WriteLine($"NS:{NS}, EW:{EW}, Manhatten Distance = {Math.Abs(NS) + Math.Abs(EW)}");
        }

        #endregion

        public static void Move(int degrees, ref int NS, ref int EW, int units)
        {
            if (degrees == 0) NS += units;
            if (degrees == 90) EW += units;
            if (degrees == 180) NS -= units;
            if (degrees == 270) EW -= units;
        }

        public static int ChangeDirection(int curDir, char rotate, int units)
        {

            curDir += rotate == 'L' ? (-1 * units) : units;
            if (curDir < 0) curDir = 360 + curDir;
            if (curDir >= 360) curDir = curDir - 360;
            return curDir;
        }

        #region P2
        public static void SolvePuzzle12_P2(string inputfile)
        {
            List<KeyValuePair<char, int>> route = new List<KeyValuePair<char, int>>();
            using (StreamReader sr = new StreamReader(inputfile))
            {
                string buffer = sr.ReadLine();
                while (buffer != null)
                {
                    route.Add(new KeyValuePair<char, int>(buffer[0], int.Parse(buffer.Substring(1))));
                    buffer = sr.ReadLine();
                }
            }
            NavigateP2(route);
        }

        public static void NavigateP2(List<KeyValuePair<char, int>> route)
        {
            Point waypoint = new Point(1, 10);
            Point ship = new Point(0, 0);
            foreach (var step in route)
            {
                // Console.WriteLine($"D: {direction} - {step.Key}{step.Value}");
                switch (step.Key)
                {
                    case 'N':
                        Move(0, ref waypoint.NS, ref waypoint.EW, step.Value);
                        break;
                    case 'E':
                        Move(90, ref waypoint.NS, ref waypoint.EW, step.Value);
                        break;
                    case 'S':
                        Move(180, ref waypoint.NS, ref waypoint.EW, step.Value);
                        break;
                    case 'W':
                        Move(270, ref waypoint.NS, ref waypoint.EW, step.Value);
                        break;
                    case 'L':
                    case 'R':
                        waypoint = RotateWaypoint(waypoint, ship, step.Key, step.Value);
                        break;
                    case 'F':
                        MoveTowardsWaypoint(ref waypoint, ref ship, step.Value);
                        break;
                }
                //Console.WriteLine($"After {step.Key}{step.Value}, Waypoint: E{waypoint.EW}, N{waypoint.NS} Ship: E{ship.EW}, N{ship.NS}");
            }
            Console.WriteLine($"EW:{ship.EW}, NS:{ship.NS}, Manhatten Distance = {Math.Abs(ship.NS) + Math.Abs(ship.EW)}");
        }

        private static void MoveTowardsWaypoint(ref Point waypoint, ref Point ship, int units)
        {
            ship.NS += waypoint.NS * units;
            ship.EW += waypoint.EW * units;
        }

        private static Point RotateWaypoint(Point waypoint, Point ship, char direction, int units)
        {
            int degrees = ChangeDirection(0, direction, units);

            if (degrees == 0) return waypoint;
            if (degrees == 90) return new Point(waypoint.EW * -1, waypoint.NS);
            if (degrees == 180) return new Point(waypoint.NS * -1, waypoint.EW * -1);
            return new Point(waypoint.EW, waypoint.NS * -1);
        }

        private class Point
        {
            public int NS;
            public int EW;
            public Point(int ns, int ew)
            {
                NS = ns;
                EW = ew;
            }
        }
        #endregion
    }
}
