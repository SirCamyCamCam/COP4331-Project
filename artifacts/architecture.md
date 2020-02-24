# Program Organization

[High Level Program Organization Diagram](https://drive.google.com/file/d/1rf1bcVQuNXr1jEMKfpWL9t0AQpL9ZQ_a/view?usp=sharing)

# Major Classes

[Major Classes UML Diagram](https://drive.google.com/file/d/1PpFmCsUxAFgKxv27XWKySDtos1Y4dXyZ/view?usp=sharing)

# Data Design

No Database needed. All values and information needed to run the game will be stored in a few global variables within classes. These variables will be either local to the class or stored in a static class accessible by other classes if needed across the game.

# Business Rules

The game is expected to be an executable.
The game is expected to be playable. Other than these, guidelines are listed on the professor's syllabus

# User Interface Design

[User Interface Design](https://docs.google.com/document/d/1kpGgwfg6r6enp68Jdj-jGx1Z2RlYuP_FJamOdQT0AVk/edit?usp=sharing)

# Resource Management

Resourses will be managed in the Unity Editor using the performace tool. Recource usage in processing movements of the ants will be most vital to keep as low as possible as this will use most processing power. As few update functions as possible will be used and no physcis will be used.

# Security

No major security requirements will be needed. There is no login or vital privacy information that needs to be protected. However, use of private values and methods will be used by default.

# Performance

A standard of 30 fps will be required with 1,000 ants spawned. GPU performace will not need to be evaluated and will not be very demanding. CPU performace optimization will be most vital. 

# Scalability

As a single player game, there will only ever be 1 user at any given time. No local multiplayer or online multiplayer functionality will be considered. 

# Interoperability

The game is only expected to run on Windows machines.

# Internationalization/Localization

The game will only be for domestic release within the United States.

# Input/Output

All input will be received through the keyboard and mouse. All output will be displayed within the executable on the player's screen.

# Error Processing

A standard of printing any errors to the console will be implimented. Using Unity's error pause functionality, "Debug.LogError("Message");" will be the stanard if any error are caught with try catch and if statements.

# Fault Tolerance

An error with an ant will simply remove the ant from game and any other error will hault the game.

# Architectural Feasibility



# Overengineering



# Build-vs-Buy Decisions

All resourses used will be original and built by the students. Upon further development, if there is a functionality beyond the student's scrope, a Unity Plugin will be considered.

# Reuse



# Change Strategy


