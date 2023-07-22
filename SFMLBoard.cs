using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CellularAutomata
{
    internal class SFMLBoard
    {
        public SFMLBoard(uint width, uint height, CellularAutomaton celAut, int cellSize)
        {
            this.vMode = new VideoMode(width, height);
            this.rWindow = new RenderWindow(vMode, "Cellular Automaton", Styles.Close);
            this.cellularAutomaton = celAut;
            this.lattice = cellularAutomaton.LATTICE;

            gridWidth = lattice.WIDTH;
            gridHeight = lattice.HEIGHT;
            this.cellSize = cellSize;
            gridTLCorner = new int[2] { Convert.ToInt32((width - gridWidth * cellSize)/2), 
                                        Convert.ToInt32((height - gridHeight * cellSize) / 2) };

            grid = new RectangleShape[gridWidth, gridHeight];
            for (int i = 0; i < gridWidth; ++i)
            {
                for (int j = 0; j < gridHeight; ++j)
                {
                    grid[i, j] = new RectangleShape(new SFML.System.Vector2f(cellSize, cellSize))
                    {
                        Position = new SFML.System.Vector2f(gridTLCorner[0] + i * this.cellSize, gridTLCorner[1] + j * this.cellSize),
                        FillColor = Color.White,
                        OutlineColor = Color.Black,
                        OutlineThickness = borderThickness
                    };
                }
            }
            
            rWindow.Closed += (sender, e) => rWindow.Close();
            rWindow.KeyPressed += (sender, e) =>
            {
                Window window = sender as Window;
                switch (e.Code)
                {
                    case Keyboard.Key.Escape: 
                        window.Close();
                        break;
                    case Keyboard.Key.P:
                        pause = !pause;
                        break;
                    case Keyboard.Key.R:
                        cellularAutomaton.Clear();
                        cellularAutomaton.RandomVivify(0, gridWidth * gridHeight);
                        break;
                    case Keyboard.Key.C:
                        cellularAutomaton.Clear();
                        break;
                    case Keyboard.Key.E:
                        editmode = !editmode;
                        break;
                    case Keyboard.Key.D:
                        (backgroundColor[0], backgroundColor[1]) = (backgroundColor[1], backgroundColor[0]);
                        break;
                    case Keyboard.Key.G:
                        borderThickness = -(borderThickness - 1);
                        break;
                    default: 
                        break;
                }
            };

            rWindow.MouseButtonPressed += (sender, e) =>
            {
                if (!editmode) return;

                int mX = e.X;
                int mY = e.Y;
                Cell? clickedCell = null;

                if (gridTLCorner[0] <= mX && mX <= gridTLCorner[0] + gridWidth * cellSize
                    && gridTLCorner[1] <= mY && mY <= gridTLCorner[1] + gridHeight * cellSize)
                {
                    int x = (mX - gridTLCorner[0]) / cellSize;
                    int y = (mY - gridTLCorner[1]) / cellSize;

                    clickedCell = lattice[x, y];
                }

                if (clickedCell == null) return;

                if (e.Button == Mouse.Button.Left)
                {
                    clickedCell.Vivify();
                }
                else if (e.Button == Mouse.Button.Right)
                {
                    clickedCell.Kill();
                }
            };

        }

        private void DrawGrid()
        {
            for (int i = 0; i < gridWidth; ++i)
            {
                for (int j = 0; j < gridHeight; ++j)
                {
                    RectangleShape cellField = grid[i, j];
                    cellField.FillColor = lattice[i, j].IsAlive ? new Color(220, 0, 0) : backgroundColor[0];
                    cellField.OutlineThickness = borderThickness;
                    rWindow.Draw(cellField);
                }
            }
        }

        public void Run()
        {
            ulong t = 0;
            int alive = cellularAutomaton.AliveCellsCount;
            while (rWindow.IsOpen)
            {
                rWindow.DispatchEvents();
                rWindow.Clear();

                DrawGrid();
                rWindow.Display();

                if (!pause && !editmode)
                {
                    if (t > 0 && t % 60 == 0)
                    {
                        cellularAutomaton.EvaluateRules();
                        rWindow.SetTitle($"Cellular Automaton | Alive: {alive}");
                    }
                    ++t;
                    alive = cellularAutomaton.AliveCellsCount;
                }

                else if (editmode)
                {
                    rWindow.SetTitle($"Cellular Automaton | Alive: {alive} | EDIT MODE: LMB - vivify, RMB - kill");
                }

                else 
                {
                    rWindow.SetTitle($"Cellular Automaton | Alive: {alive} | PAUSE"); 
                }
            }
        }

        VideoMode vMode;
        RenderWindow rWindow;
        CellularAutomaton cellularAutomaton;
        Lattice lattice;
        int gridWidth, gridHeight, cellSize;
        int[] gridTLCorner;
        RectangleShape[,] grid;

        Color[] backgroundColor = new Color[2] { Color.White, new Color(44, 44, 44) };
        int borderThickness = 1;

        bool pause = false;
        bool editmode = false;
    }
}
