using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CellularAutomata
{
    class CellularAutomaton
    {
        public CellularAutomaton(int x, int y) 
        { 
            lattice = new Lattice(x, y);
            VivifyRules = new List<Func<Cell, bool>>();
            KillRules = new List<Func<Cell, bool>>();

            cellsAliveCount = 0;
        }

        public void AddVivifyRule(Func<Cell, bool> rule)
        {
            VivifyRules.Add(rule);
        }

        public void AddKillRule(Func<Cell, bool> rule)
        {
            KillRules.Add(rule);
        }

        public void EvaluateRules()
        {
            bool[,] vivifyList = new bool[lattice.WIDTH, lattice.HEIGHT];
            bool[,] killList = new bool[lattice.WIDTH, lattice.HEIGHT];

            Parallel.For(0, lattice.WIDTH, (i) => 
            {
                for (int j = 0; j < lattice.HEIGHT; ++j)
                {
                    Cell cell = lattice[i, j];

                    bool life = true;
                    if (VivifyRules.Count > 0)
                    {
                        foreach (Func<Cell, bool> rule in VivifyRules)
                        {
                            life = life && rule(cell);
                        }
                    }

                    bool death = true;
                    if (KillRules.Count > 0)
                    {
                        foreach (Func<Cell, bool> rule in KillRules)
                        {
                            death = death && rule(cell);
                        }
                    }


                    if (life && death) { Console.WriteLine($"({cell.X}, {cell.Y}) - conflict"); } // Conflict
                    else if (life) { vivifyList[i, j] = true; }
                    else if (death) { killList[i, j] = true; }
                }
            });


            // Vivify and kill

            Parallel.For(0, lattice.WIDTH, (i) => 
            {
                for (int j = 0; j < lattice.HEIGHT; ++j)
                {
                    if (vivifyList[i, j])
                    {
                        lattice[i, j].Vivify();
                    }
                    else if (killList[i, j])
                    {
                        lattice[i, j].Kill();
                    }
                }
            });

            cellsAliveCount = lattice.CountAlive();
        }

        public void Clear()
        {
            lattice.KillAll();
            cellsAliveCount = 0;
        }

        public void RandomVivify(int min, int max)
        {
            int randint = new Random().Next(min, max);
            for (int i = 0; i < randint; ++i)
            {
                int x = new Random().Next(0, lattice.WIDTH);
                int y = new Random().Next(0, lattice.HEIGHT);
                lattice[x, y].Vivify();
            }
        }

        public Cell this[int x, int y]
        {
            get { return lattice[x, y]; }
        }

        public Lattice LATTICE
        { 
            get { return lattice; }
        }

        public int AliveCellsCount
        { 
            get { return cellsAliveCount; } 
        }

        

        Lattice lattice;
        List<Func<Cell, bool>> VivifyRules;
        List<Func<Cell, bool>> KillRules;
        int cellsAliveCount;
    }
}
