# The-Cube
Game with simple Box    
![App Screenshot](https://i.imgur.com/kzoTbFG.jpeg)    
The game provides a straightforward and addictive experience. The main objective is to progress through multiple levels, each with its own set of challenges. Players must collect coins scattered randomly across the map to reach a certain score and advance to the next level. However, they must be cautious of enemies that will attempt to hinder their progress.

![App Screenshot](https://i.imgur.com/vwEtWVV.jpeg)        

## Game Elements
### GameManager: 
Controls the game logic, including enemy generation, difficulty management, and tracking the player's score.

### Pool Manager: 
Manages the recycling and reusing of game resources, such as bullets and game objects, to optimize performance and improve efficiency.

### MapGenerator: 
Generates the game environment, including maps and the distribution of elements such as coins and enemies.
![App Screenshot](https://i.imgur.com/8puovpz.jpeg)   
### UI Manager: 
Controls the game's user interface, displaying relevant information such as the current score, number of lives, and other UI elements.

### Allocate Elements: 
Distributes elements on the map, such as coins and lives, to provide additional challenges and rewards to the player.
![App Screenshot](https://i.imgur.com/iYkZxHK.jpeg)   
### Camera: 
Follows the player in the game to ensure they are always centered on the screen and can see their surroundings clearly.

## Game Architecture
The game's architecture is based on a modular and componentized approach, where each game element has specific responsibilities. The Singleton pattern is used to ensure the uniqueness of certain objects and facilitate communication between them. This provides a clear and maintainable structure, allowing for efficient future expansions and improvements to the game.

## Game Execution Process
The game begins with a simple start screen, from where players can start a new game. Once the game is initiated, the GameManager takes control and begins generating the game environment. Players must collect coins and avoid enemies as they progress through the levels. The game continues until the player loses all their lives or completes all available levels.

## Possible Future Improvements
Improve enemy AI to provide a more balanced and exciting challenge.
Incorporate sound effects to enhance the game's auditory experience, including GameOver and Start screens.
Enhance the GameOver and Start screens with animations and game configuration options for a more immersive experience.
Implement a smooth transition between levels to improve cohesion and narrative flow.

Thank you for checking out our game! Feel free to explore the code and contribute your own improvements.
