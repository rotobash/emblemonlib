# emblemonlib
A library for RPGs

##Purpose
I created this library as a collection of classes I thought I could reuse to facilitate RPG development. As the library goes into a more complete state I will add API documentation, but for now I'm interested in making something functional.

##Components
These components are either implemented or in progress
* Characters
  * Overworld characters, Battle characters or hybrid of both
* Combat
  * Moves that can be used by characters (spells, attacks, etc.)
  * Character Parties
  * Leveling curves
  * Character stats
  * Perhaps a simple battle cycle
* Utilities
  * Animations
  * Parser that reads XML representations of moves into the game
  * Parser that reads XML representations of characters into the game
  * Parser that reads XML representations of maps into the game
  * Particle Effects (for cool spells)

I intend to work on easing scene transitions, and polishing animations more. Some things are better left for the main game to handle itself (e.g. GUI) as I don't want to create a dependence on the game instance itself.

I also want to make tools to easily generate the XML data files used for characters, maps, moves, etc.
