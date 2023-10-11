# SpaceLander 
 ## Project info 
  This project was created by David Mikulic using the Unity Engine.
  ### Unity Version
   Unity 2022.3.8f1
  ### Sources and inspiration
   For most of my projects, I experimented until I got it working, but for some things, like the PointGen script, I used external sources:
   - PointGen script: https://extremelearning.com.au/how-to-evenly-distribute-points-on-a-sphere-more-effectively-than-the-canonical-fibonacci-lattice/
   Kerbal Space Program was also a huge inspiration. Other than that I just used the Unity documentation (https://docs.unity.com/) and got great help from classmates.
  ### Packages
    All packages are included in the Git Repository, but just in case, I will list them here.
    - Input System
    - Post Processing
    - TextMeshPro
    - Unity UI
  ### Assets
   I used the asset Milky Way Skybox (https://assetstore.unity.com/packages/2d/textures-materials/milky-way-skybox-94001) for this project. All other assets, including the lander model, the planet texture, and the planet models (unity ones were incredibly low poly), were created by me in Blender. 
  ### Controls
   #### Pause
    Pausing and unpausing while in-game is done using esc or P.
   #### Camera Controls
    The camera is controlled in two ways.
    1. Camera zooming, which moves the camera along the axis aligned both with the lander object and with the camera. The camera is zoomed using the scrollwheel.
    2. Camera panning, which rotates the camera around the lander object while taking the closest planet into account. The camera is panned by holding down the right mouse button while moving the mouse. 
   #### Spaceship Controls
    The spaceship is controlled in two ways.
    1. Translation:
     Using Space activates the engine, which thrusts the player in the direction the lander is facing.
    2. Rotation:
     The spaceship can rotate along three axes: Pitch, roll, and yaw. The controls are as such:
     Pitch: W and S
     Yaw: A and D
     Roll: Q and E
  ### Game Objective
   In every level except for the first, your objective is to use your fuel sparingly and navigate to go through hoops, and to keep your ship in good shape. Hoops give you points, and your highscore is saved between sessions.
  ## Developer Instructions
   In this section, I will outline how one should go about playing the game from the Unity Editor, as well as how one should examine the project in the easiest way possible.
   ### Starting Scene
    To start playing, open the scene MainMenu.
   ### Navigating the project.
    The project is essentially built as such: There is one scene, the afforementioned MainMenu, that acts as the hub. The rest of the scenes in the Build Settings are levels. Every level is structured in the same way: There is a (1) GameManager, a light source, and a number of (2) planets.
     1. GameManager
      The Game Manager loads necessary resources, like the player prefab, and UI objects.
     2. Planet
      A planet is an object which exerts gravitational force on the player, generates hoops, and detects hard collisions to feed to the player.
    The Player prefab is one of the more important Assets in the project. Within it, we find the LanderModel, the Main Camera, and some canvas stuff.
   #### the LanderModel in the Player prefab
    The LanderModel holds the scripts LanderController, GravityManager, PlayerValues, and UIController. In order, their functions are:
    - The LanderController handles all the movement of the Lander Model, and acts as a relay for some things that want to access other scripts on the Lander.
    - The GravityManager calculates the gravity acting on the player based on the positions and masses of every planet.
    - PlayerValues stores and alters relevant information about the Lander like fuel level, health, and engine power.
    - The UIController manages the UI that is visible to the player at all times during gameplay.
   #### Saving Systems
    The game employs two kinds of persistent data. Firstly, it uses PlayerPrefs to store the settings that can be altered from the main menu options. Secondly, a JSON file is created to store the highscores of each level. The scripts responsible for these systems are SaveSettings and HighScoreManager respectively. I could have stored everything in PlayerPrefs, but I wanted to try both.
 ## Code Structure and goal
  As best as I could, I tried to make my systems as modular and expandable as possible. This can be seen the most clearly in the PointGen script, where I use it to generate hoops, repair stations, and refuel stations.

  I tried to make my code as legible as I could, but in some places, it was simply not possible. In the PointGen script, in the CameraController, and in the LanderController, the math is just not quite as easily understandable as it would have been on paper. I try to amend this by adding comments where it is the least understandable.
 ## Improvements
  While I am quite happy with my project, there are some improvements I would like to make:
  - The threshold for damage should be much lower for the landing legs than the main body. This makes the level "Tumbling Thomas" especially frustrating.
  - Sometimes the damage applied is unexpectedly small. 
  - It is very very difficult to enter and control an orbit. If you do manage to enter an orbit, it is likely to be very eccentric and you probably have no clue how high the apoapsis (highest point of the orbit) and periapsis (lowest point of the orbit) respectively, are. Also, how close you are to them, time-wise, would be incredibly useful. If I was to continue this project, I would like to add this orbital data to be easily accessible to the player.
  - The camera unlocks from the closest planet when you get too far away from it. This gives somewhat of a nice effect, as though you are entering "deep space", but I don't know why it happens right now. I would want to find out why.
  - There is a script called Copilot in Assets\Scripts\Movement that I wanted to have essentially act as an autopilot for the Lander rotation, however, this proved somewhat more complicated than expected, and I would need to do the math for that. Essentially, I would want to get a target angular velocity given a target direction, and then apply a velocity to approach the target angular velocity. For now, that script is unused, but if it did work, landing would be a lot easier.
  - The game is fun for about 2 minutes. The entire premise and objective of collecting points is tedious, and you don't really feel like playing the level again and again to improve your highscore. Ideally, it would be more of a simulator, but that would probably require me to build the physics from scratch (colliders seem to be unstable at large scales, and I would want leapfrog integration instead of euler, like PhysX does).

  Thank you for reading :)