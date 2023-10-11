# ilander
Copyright © 2023 Changemaker Educations AB \
iLander. A Unity game project assignment for Futuregames Game Programmer. October 2023 \
By Rasmus Rönnqvist.

# Instructions
A build is available for windows 64 bit systems. And the project source files are available on this git repository.\
The game is inspired by the graphical capabilities of the ZX Specturm. But building for 256x192 resolution proved troublesome.\
It is surprisingly difficult to make a 2D pixel-art game in Unity3D, so the game is not much of a looker but you can tell where the idea was.\
Building the game with these settings proved the most playable so thats what I chose.

## How to play
Control the lander using W-A-D or the arrow keys Up, Left, Right.\
Try to move the lander to the landing spot marked by a green flag.\
If the lander comes in contact with the terrain it breaks and the level will restart.\
Controlling is not easy, so be careful! You can only boost up and tilt the lander using it's thrusters (Left and Right)

There are green pickups you can get to gain a shield making the lander invunerable.\
When you have picked on of these green pickups, press 'Space' to active it.\

# For developers. About the code
The source-code is structured around using as little of the Unity Engine as possible.\
There is only one MonoBehaviour used for the project. This is the Executor class.\
There are many places where a MonoBehaviour would improve the code greatly but I wanted to try this as a challange.

The code first starts at EntryPoint, which creates an GameState class. This GameState class is the owner of the executor, and the 
"main" class of the project. GameState owns the Executor and from the GameState properties it influences how the game is executed.
Those are the two main classes to be aware of, GameState and Executor. The rest of the structure is rather modular, like the Player 
only handling initialization and hit/death detection, while the PlayerMovement class reads data from the InputManager and moves the player.rigidbody.

InputManager and GameState uses a few public static properties to decouple the code. It was a trade-off I made in order to have a clear and rigid 
order of execution.

## Badness not the good kind
Not using MonoBehaviour and instantiating buttons for the ui, and then making them children of another instantiated canvas is bad.
There are a few obvious cases like this where I should have just used MonoBehaviour components.
Things that are not dependent to run per tick or on outside data should have just been MonoBehaviours...

The Bunker class desperately needs a Pooling System. For now it instantiates new objects each shot, and each tick grabs the 
BoxCollider2D to see if it is touching the player's hurtbox.

HitCollision is mostly handled through MonoBehaviours, so I have to rely on collider2D.IsTouching(player.hurtbox) a bunch of times.
Should have just been simple MonoBehaviour classes.

The LevelManager is bad. I made that class early, and it just became a bloated homunculus. My idea was that if I cannot reference objects 
directly through the editor I would need a way to reference them from code. So I just made a LevelManager and tossed different functionality into it.
Example. Why is the LevelManager looking for a spawn_point? Can't just the Player who is the class that needs the spawn_point just look for it themselves?
Yes, yes they could. But I am leaving this class in as a reminder for me in the future to NOT create bloated homonculi- I mean managers.\
The LevelManager should just be a strict SceneManager for things like restarting and changing scenes.

And much more...


# Math
I tried to utilize some more advanced mathematics.\
- As the UIManager fades out the MainMenu canvas it does so using a cosine for a smoother effect.

- When a player holds a PowerUp it is animated, circling around the player using a combination of a sine and a cosine.\
  A simple use of sine and cosine to create a circle. By using cosine for the x-axis, and sine for the y-axis I created a simple spinning animation.

- The Bunker uses a vector dot product to calculate if it should us the left or the right turret to shoot the player.

# Inspiration and sources
Bunker enemy inspired by:\
Math for Gameplay Programmers, John O'Brien, GDC 2012.\
http://essentialmath.com/tutorial.htm

Look and gameplay inspired by:\
Mars Lander.\
https://www.codingame.com/training/medium/mars-lander-episode-2

Code inspired by our lecturer, Sebastian.\
https://github.com/sbularca
