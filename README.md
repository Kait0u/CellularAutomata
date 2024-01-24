# CellularAutomata

## Introduction

This is a cellular automaton simulator, by default configured with the rules of The Game of Life. It's written in C#, with the graphical interface coded in SFML.Net.

## Features
+ **Pause** and **Edit** modes
+ **Light** and **Dark** modes
+ Randomization
+ Alive cell count

## Controls
<kbd>P</kbd> - pause mode<br>
<kbd>E</kbd> - edit mode<br>
<kbd>D</kbd> - dark mode<br>
<br>
<kbd>C</kbd> - clear the board<br>
<kbd>R</kbd> - randomize the board<br>
<kbd>G</kbd> - toggle border<br>

### Edit mode controls
<kbd>LMB</kbd> - enable (vivify) a cell<br>
<kbd>RMB</kbd> - disable (kill) a cell<br>

## Customization
+ You can change the vivification and killing rules to your liking by adding appropriate lambdas in the `Application.cs` file
+ You can change the board size and cell count in the `Application.cs` file
