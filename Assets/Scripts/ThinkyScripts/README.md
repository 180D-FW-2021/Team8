# Thinky Scripts

Main scripts used in the Thinky Think minigame
- The data management scripts here access the Speech Recognition transcript from the Unity Google Streaming Speech-to-Text API
- The transcript is updated once per frame (recognizes the various user inputs multiple times throughout the game)
- This transcript is compared with a list of acceptable responses for each of the mini-games
- For easy and medium, order does not matter when the player is repeating what they saw
- For hard mode, order matters as the steps must be completed in sequence!
