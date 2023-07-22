using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CellularAutomata
{
    internal class Application
    {
        public void Run()
        {
            CellularAutomaton cellularAutomaton = new CellularAutomaton(64, 36);
            uint[] windowResolution = new uint[2] { 1600, 900 };
            uint cellSize = 25;
            
            
            Random random = new Random();
            
            Lattice lattice = cellularAutomaton.LATTICE;

            cellularAutomaton.AddVivifyRule((cell) => {
                Cell[,] neighbors = cell.Neighborhood();
                int livingNeighbors = 0;
                foreach (var neighbor in neighbors)
                { 
                    if (neighbor != null)
                    {
                        if (neighbor.IsAlive) ++livingNeighbors;
                    }
                }

                return !(cell.IsAlive) && livingNeighbors == 3;
            });

            cellularAutomaton.AddKillRule((cell) => {
                Cell[,] neighbors = cell.Neighborhood();
                int livingNeighbors = 0;
                foreach (var neighbor in neighbors)
                {
                    if (neighbor != null)
                    {
                        if (neighbor.IsAlive) ++livingNeighbors;
                    }
                }

                return 3 < livingNeighbors || livingNeighbors < 2;
            });

            cellularAutomaton.RandomVivify(5, (int)(cellularAutomaton.LATTICE.WIDTH * cellularAutomaton.LATTICE.HEIGHT / 3) + 1);

            
            SFMLBoard window = new SFMLBoard(windowResolution[0], windowResolution[1], cellularAutomaton, (int)cellSize);
            window.Run();
        }
    }
}
