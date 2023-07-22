using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CellularAutomata
{
    class Cell
    {
        public Cell(Lattice parent, int x, int y, bool status = false)
        {
            this.parent = parent;
            this.x = x;
            this.y = y;
            this.status = status;
        }

        public void Vivify()
        {
            status = true;
        }

        public void Kill()
        { 
            status = false; 
        }

        public Cell[,] Neighborhood()
        {
            Cell[,] neighbors = new Cell[3, 3];
            for (int i = -1; i <= 1; ++i)
            {
                for (int j = -1; j <= 1; ++j)
                {
                    if (i == 0 && j == 0) continue;
                    else
                    {
                        neighbors[i + 1, j + 1] = parent[x + i, y + j];
                    }
                }
            }

            return neighbors;
        }

        public bool IsAlive 
        { 
            get { return status; }
        }

        public int X
        { get { return x; } }

        public int Y 
        { get { return y; } }

        bool status;
        Lattice parent;
        int x, y;
    }

    class Lattice
    {
        public Lattice(int x, int y)
        {
            this.width = x;
            this.height = y;
            grid = new Cell[width, height];
            for (int i = 0; i < width; ++i)
            {
                for (int j = 0; j < height; ++j)
                {
                    grid[i, j] = new Cell(this, i, j, false);
                    //Console.WriteLine($"({i}, {j}) added!");
                }
            }
        }
     
        public Cell this[int x, int y] 
        {
            get 
            { 
                if (x < 0) x = width + x % width;
                if (y < 0) y = height + y % height;
                //Console.WriteLine($"({x % width}, {y % height}) access attempt"); 
                return grid[x % width, y % height]; 
            }
        }

        public void KillAll()
        {
            foreach (Cell cell in grid)
            {
                cell.Kill();
            }
        }

        public int CountAlive()
        { 
            int count = 0;

            Parallel.For(0, HEIGHT, (i) => 
            { 
                for(int j = 0; j < WIDTH;  ++j)
                {
                    if (this[i, j].IsAlive) ++count;
                }
            });

            return count;
        }

        public int WIDTH
        { get { return width; } }

        public int HEIGHT 
        { get { return height; } }

        int width, height;
        Cell[,] grid;
    }

    
}
