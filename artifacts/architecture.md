# Program Organization

[High Level Program Organization Diagram](https://drive.google.com/file/d/1rf1bcVQuNXr1jEMKfpWL9t0AQpL9ZQ_a/view?usp=sharing)

# Major Classes

[Major Classes UML Diagram](https://drive.google.com/file/d/1PpFmCsUxAFgKxv27XWKySDtos1Y4dXyZ/view?usp=sharing)

# Data Design

No Database needed. All values and information needed to run the game will be stored in a few global variables within classes. These variables will be either local to the class or stored in a static class accessible by other classes if needed across the game. All classes and vlues saved within the classes are stored locally on the user's machine.

# Business Rules

| Business Rule ID | Rule |
| ----------------- | ---- | 
| 000 | The game is expected to be an executable | 
| 001 | Users shall not be able to adjust files |
| 002 | Users can only adjust their own game settings and options |
| 003 | Users will be able to adjust key-binding, sound, and game options | 

# User Interface Design

[User Interface Design](https://docs.google.com/document/d/1kpGgwfg6r6enp68Jdj-jGx1Z2RlYuP_FJamOdQT0AVk/edit?usp=sharing)

# Resource Management

Resourses used by the application will primarily game assets such as sprites, fonts, backgrounds, audio clips, and animations. Recource usage in processing movements of the ants will be most vital to keep as low as possible as this will use most processing power. Very little graphics recourses will be used as it is a 2D game. As the number of ants increase and the gameobjects in the scene increase it is expected that the game will perform worse as time progresses. Lower end system may not be able process all the information required to move many ants throughout the scene in a smooth fashion. As few update functions as possible will be used to minimize overhead and function calling. There will be no physcis used as the ants will simply move across the screen, creatly reducing performace impact.

# Security

No major security requirements will be needed. There is no login or vital privacy information that needs to be protected. However, use of private values and methods will be used by default. The user shall also no be able to edit values within the game's files.

# Performance

As a small 2D game, performace should be fast upon first initialization. Frame rate will drop over time as the game progesses and more ants continue to spawn and die. The number of gameobject may reacha critcal point for the user's machine and they may see a major performace drop. The user will be able to edit their settings in order to help maintain the best possible performance. Values indicating the current game stats will be updated only 5 times per second to reduce performace impact. This will still be perceived as smooth however be much more efficient than updating 60 times per second or more.  

# Scalability

As a single player game, there will only ever be 1 user at any given time. No local multiplayer or online multiplayer functionality will be considered. The game will be modular, allowing for the creation of waypoints in game and spawning of more ants as the user chooses. 

# Interoperability

The game does not share any data between devices, services, or other applications. 

# Internationalization/Localization

Possible internationalization/localization will be handled by importing the appropriate fonts for languages that use different scripts and translating the game's content and interfaces.

# Input/Output

All input will be received through the keyboard and mouse. There will be no external controllers needed and will only run on a PC with a keyboard and mouse. All output will be displayed within the executable on the user's screen.

# Error Processing

A standard of printing any errors to the console will be implimented. Using Unity's error pause functionality, Debug.LogError will be the stanard if any error are caught with try catch and if statements. They will immediately hault the game and be dealt with.

# Fault Tolerance

An error with an ant will simply remove the ant from game. All the ant's functions will be passed to another ant when another ant's functionality is avaliable. An error with a waypoint will destroy the waypoint and begin to rebuild it. Re-connecting all other waypoints and returning to the previous values. Any error with the UI will be noted in the console.

# Architectural Feasibility

The application is technically feasible for base functions of an ant colony. Vital game mechanics such as finding correct waypoints to travel to, performing tasks, and creation of waypoints are most feasible. Editing of waypoints, enemy attacks, and dragging waypoints may be less feasible. 
The game will be artistically feasible. General assests such as backgrounds, ants, waypoint indicators, and paths will be created in photo editing software. Animations and visual effects may be less feasible. 
The application is economically feasible, all production using free versions of Trello, Unity, GitHub, and photo editing software.

# Overengineering

Possible overengineering may occur when creating ant functionality. Adding too many tasks to a single ant to perform and then deciding from those task may be over-kill. Overengineering should be avoided by focusing on core functionality of ants at start to have a fully functioning colony.

# Build-vs-Buy Decisions

All resourses used will be original and built by the students. Upon further development, if there is a functionality beyond the student's scrope, a Unity Plugin will be considered. The only third-party libraries that will be used are the default Unity libraries. 

# Reuse

The game's assets could be reused for future projects such as Audio and Waypoint managers. Slight modification may be required based on requierments but general audio and waypoint navigation will be do-able. 

# Change Strategy

1. Identify the requested change to be done.
2. Discuss the change with group members.
3. Determine high-level impacts.
4. Determine functional impacts.
5. Modify the affected user stories.
6. Add to product backlog. 
