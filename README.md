# Centipede Game

## Description
An endless 2D Centipede-like game made with unity. 

### Gameplay
Player spawns in the middle of the bottom of the screen. Target of the game - don't let centipede 
kill player. At start player has 10 lives and he can catch bonuses from mushrooms which will add 
5 lives each. Player can receive damage if:
 - Touch mushroom
 - Touch centipede
 - Centipede segment reached bottom of the screen

Controls: WASD Or Arrows - move
	  Left Ctrl Or Left Mouse Click - shoot

#### Player
Player presented as SpaceShip. He must kill all segments of centipede before it kills player

#### Centipede	
Segmented snake which every wave spawns in the middle of the top of screen. 
Each segment dies after player shoots it. After death leaves a mushroom

#### Mushroom
Static object which can damage player and after death with chance gives a random bonus.
Also randomly spawns at first Wave.

#### Bonuses
Angel - gives 5 lives
Sword - Player shoots a little faster

## Installation and launching game
Just unpack CentipedeReamake.rar and launch from Centipede Launcher shortcut 
