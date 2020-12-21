using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public static class Puzzle17
    {

        #region P1
        public static void SolvePuzzle17_P1(string inputfile)
        {
            Dictionary<string, Cube> activeCubes = new Dictionary<string, Cube>();
            using (System.IO.StreamReader sr = new System.IO.StreamReader(inputfile))
            {
                int x = 0;
                string buffer = sr.ReadLine();
                while (buffer != null)
                {
                    for (int y = 0; y < buffer.Length; y++)
                    {
                        if (buffer[y] == '#')
                            activeCubes.Add($"{x}_{y}_{0}", new Cube(x, y, 0));
                    }
                    buffer = sr.ReadLine();
                    x++;
                }
            }

            for (int i = 1; i < 7; i++)
            {
                activeCubes = RunCycle(activeCubes);
            }
            Console.WriteLine($"Active cubes: {activeCubes.Count}");
        }

        private static Dictionary<string, Cube> RunCycle(Dictionary<string, Cube> activeCubes)
        {
            Dictionary<string, Cube> newCubes = new Dictionary<string, Cube>();

            foreach (var c in activeCubes)
            {
                long activeAdj = 0;
                foreach (var d in c.Value.AdjacentCubes())
                {
                    if (activeCubes.ContainsKey(d.ID))
                        activeAdj += 1;
                    else
                    {
                        if (!newCubes.ContainsKey(d.ID))
                        {
                            long ncadjCount = 0;
                            foreach (var e in d.AdjacentCubes())
                            {
                                if (activeCubes.ContainsKey(e.ID))
                                    ncadjCount += 1;
                            }
                            if (ncadjCount == 3)
                                newCubes.Add(d.ID, d);
                        }
                    }
                }
                if (activeAdj == 2 || activeAdj == 3)
                {
                    newCubes.Add(c.Key, c.Value);
                }
            }

            return newCubes;
        }

        private class Cube
        {
            public long X = 0;
            public long Y = 0;
            public long Z = 0;
            public string ID = string.Empty;
            public Cube(long x, long y, long z)
            {
                X = x; Y = y; Z = z;
                ID = $"{x}_{y}_{z}";
            }
            public List<Cube> AdjacentCubes()
            {
                List<Cube> cubes = new List<Cube>();
                for (int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        for (int z = -1; z < 2; z++)
                        {
                            if (!(x == 0 && y == 0 && z == 0))
                                cubes.Add(new Cube(X + x, Y + y, Z + z));
                        }
                    }
                }
                return cubes;
            }
        }
        #endregion

        #region P2
        public static void SolvePuzzle17_P2(string inputfile)
        {
            Dictionary<string, CubeP2> activeCubes = new Dictionary<string, CubeP2>();
            using (System.IO.StreamReader sr = new System.IO.StreamReader(inputfile))
            {
                int x = 0;
                string buffer = sr.ReadLine();
                while (buffer != null)
                {
                    for (int y = 0; y < buffer.Length; y++)
                    {
                        if (buffer[y] == '#')
                            activeCubes.Add($"{x}_{y}_0_0", new CubeP2(x, y, 0, 0));
                    }
                    buffer = sr.ReadLine();
                    x++;
                }
            }

            for (int i = 1; i < 7; i++)
            {
                activeCubes = RunCycleP2(activeCubes);
            }
            Console.WriteLine($"Active cubes: {activeCubes.Count}");
        }

        private static Dictionary<string, CubeP2> RunCycleP2(Dictionary<string, CubeP2> activeCubes)
        {
            Dictionary<string, CubeP2> newCubes = new Dictionary<string, CubeP2>();

            foreach (var c in activeCubes)
            {
                long activeAdj = 0;
                foreach (var d in c.Value.AdjacentCubes())
                {
                    if (activeCubes.ContainsKey(d.ID))
                        activeAdj += 1;
                    else
                    {
                        if (!newCubes.ContainsKey(d.ID))
                        {
                            long ncadjCount = 0;
                            foreach (var e in d.AdjacentCubes())
                            {
                                if (activeCubes.ContainsKey(e.ID))
                                    ncadjCount += 1;
                            }
                            if (ncadjCount == 3)
                                newCubes.Add(d.ID, d);
                        }
                    }
                }
                if (activeAdj == 2 || activeAdj == 3)
                {
                    newCubes.Add(c.Key, c.Value);
                }
            }

            return newCubes;
        }

        private class CubeP2
        {
            public long X = 0;
            public long Y = 0;
            public long Z = 0;
            public long W = 0;
            public string ID = string.Empty;
            public CubeP2(long x, long y, long z, long w)
            {
                X = x; Y = y; Z = z; W = w;
                ID = $"{x}_{y}_{z}_{w}";
            }
            public List<CubeP2> AdjacentCubes()
            {
                List<CubeP2> cubes = new List<CubeP2>();
                for (int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        for (int z = -1; z < 2; z++)
                        {
                            for (int w = -1; w < 2; w++)
                            {
                                if (!(x == 0 && y == 0 && z == 0 && w == 0))
                                    cubes.Add(new CubeP2(X + x, Y + y, Z + z, W + w));
                            }
                        }
                    }
                }
                return cubes;
            }
        }
        #endregion

    }
}
