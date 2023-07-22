# CellularAutomata

## Introcution

This is a cellular automaton simulator, by default configured with the rules of The Game of Life. It's written in C#, with the graphical interface coded in SFML.Net.

## Features
+ **Pause** and **Edit** modes
+ **Light** and **Dark** modes
+ Randomization
+ Alive cell count

## Controls
<kbd>P</kbd> - pause mode
<kbd>E</kbd> - edit mode
<kbd>D</kbd> - dark mode

<kbd>C</kbd> - clear the board
<kbd>R</kbd> - randomize the board
<kbd>G</kbd> - toggle border

### Edit mode controls
<kbd>LMB</kbd> - enable (vivify) a cell
<kbd>RMB</kbd> - disable (kill) a cell

## Customization
+ You can change the vivification and killing rules to your liking by adding appropriate lambdas in the `Application.cs` file
+ You can change the board size and cell count in the `Application.cs` file
