# Fry Fry Scripts

Scripts developed for and used on the Unity end of Fry Fry

The two scripts used within the scene are:
- **ChoppingGameManager.cs:** Manages the MQTT communication, records IMU movements and manages whether the player is hitting the correct motions, and manages the win/loss condition and leaderboard display
- **fryingPanMovements.cs:** Moves the main frying pan in the scene along with the gyroscope values sent over MQTT from the Raspberry Pi controller to Unity, independent of the game manager
