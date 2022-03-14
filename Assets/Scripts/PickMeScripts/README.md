# Pick Me Scripts

Main scripts used by the Picky Pick minigame

- fs_read_hands_csv.cs reads from a .csv file ("wrist_single.csv") written by the Python hand detection script and updates the position of the in-game cursor.
-- The Python hand detection script writes the relative position of the user's hand, and that position is read every frame.
- fs_read_hands_csv.cs also updates the position of the cursor it is attached to.
- All other scripts in this folder are from old versions of the game (which used to involve speech-to-text and TCP-based I/O) and are no longer used.
-- The scene also uses PickMe.cs which is located in /Assets . That script controls the timer for the game.