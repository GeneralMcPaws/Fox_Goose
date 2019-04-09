# Fox and Goose Grid Game

The famous Fox and  Goose board game is a two player turn based game. In the variation implemented here the player playing as the Fox can move in any direction (including diagonally) by one cell. Each time they move they get a point for how many cells they moved. The player playing as the Goose can put a new Goose in any empty grid cell.

The exception to the one cell move rule, is when there is a Goose next to the Fox. In that case the Fox has to jump over the Goose behind it. For each jump the Fox gets 2 points. If after they jump there is another Goose adjacent to it, they have to play again and jump again. The game ends if the Fox has no more moves left or if they reach 30 points.

[Rules of the original version of the game](https://www.mastersofgames.com/rules/fox-geese-rules.htm)

## Requirements

* Unity 5.7.x and above
