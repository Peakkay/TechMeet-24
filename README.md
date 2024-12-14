Team 83

# TechMeet-24

Welcome to our , a Unity 2D game built to deliver an engaging and fun experience! This repository contains the source code and assets for the game.

## About the Game

Game_name is rpg game created in Unity. The game is about solving a murder mystery of Dr. Carrington where in the player goes through various rooms solving puzzles and finding clues related to the crime. The main objective being finding the culprit.

## Features

1. Hidden Netwworks : Each level contains puzzles tied to revealing hidden connections between objects, clues, or story elements.
2. Digital Archaeology : Puzzles involve recovering corrupted data files, decoding encrypted messages, or piecing together fragmented holograms.

Run project.exe to start the game.(Run it in 16:9 aspect ratio)

## How to play the game?

key A - to move the player left
key D - to move the player right
key W - to move the player forward
key S - to move the player backward
key space bar - to move to the next dialogue/close the dialogue box
key C - to open the Clue UI
key E - to interact with NPC/start a puzzle/collect clues and items
key 1 - Swap the room
key 0 - cancel the swap.
key Escape - skip cutscene.

How to solve puzzles?

Room 1: The Lab
a:Wire Puzzle
Objective: Connect the wires to their correct slots in the correct order.

Drag each wire and place it into its corresponding slot.
Match the wires based on color or labels (if provided).
Ensure the wires are placed in the correct sequence to complete the circuit.
Once all wires are placed correctly, the puzzle is solved.
b: Keypad Puzzle
Objective: Unlock the keypad to access a secure compartment.

Find the keycode hidden in the room (e.g., on notes, objects, or screens).
Enter the discovered keycode into the keypad.
If the code is incorrect, search for additional clues to determine the right combination.(code = 123)
Entering the correct keycode unlocks the compartment and solves the puzzle.

Room 2: The Archives
a : Sliding Tile Puzzle
Objective: Arrange the sliding tiles in the correct numerical order (1–8).

The puzzle consists of 9 tiles (1–8 and an empty space).
Click or drag tiles adjacent to the empty space to slide them.
Rearrange the tiles until they are in sequential order (1 to 8), with the empty space in the bottom-right corner.
Completing the correct arrangement unlocks the archive's data.
This is a simple puzzle which looks tricky just move the tile 8 two times and the puzzle will be solved.

b: Decipher the Code Puzzle
Objective: Decode the encrypted message to unlock vital information.

Locate the encrypted text or code in the room.
Use the key to decipher the code.
You just need to substitue the encrypted text with numbers and subtract the key number from each letter and then resubstitute the letters.
Enter the decoded message or solution into the terminal/keypad to complete the puzzle.

Room 3: The Observatory
Star Map Puzzle
Objective: Connect the stars to form the correct constellation.

Examine the clues in the room (e.g., diagrams, notes, or instructions by NPC) to identify the target constellation.
Use the star map to locate the relevant stars.
Connect the stars in the correct sequence to form the constellation.
Click stars to create lines.
Refer to the clue for the exact shape or pattern of the constellation.
Once the constellation is correctly formed, the puzzle will be solved.
Here the constellation is corona borealis nova so just start from the left most star(it will be easily visible) and connect them in the constellation's pattern.

Here’s the concise explanation for the Room 4 Access Log Puzzle:

Room 4: The Laboratory
a: Access Log Puzzle
Objective: Identify the unauthorized access entries in the system logs.

Open the access logs on the terminal.
Analyze the logs for anomalies, such as:
Unusual timestamps outside working hours.
Unknown user IDs or names not listed as authorized personnel.
Repeated failed login attempts.
Cross-reference the logs with clues found in the room (e.g., personnel lists, schedules, or notes) to confirm unauthorized entries.
Same time stamp
Select or highlight the suspicious entries to point out the unauthorized access.
Identifying all anomalies correctly will solve the puzzle.
The log access entries 2,4,5,6 are faulty.
access 2 is unauthorized access
access 4 is external device connected
access 5 and access 6 has exactly same time stamps.

b: Multi-Layer Lock Puzzle
Objective: Unlock the secure compartment by solving all three layers of the lock.

Layer 1: Enter a Value

Find the correct value or number written somewhere in the room (e.g., on notes, diagrams, or objects).
Enter this value into the lock to complete the first layer.
Actually its written on the wall(9)
Layer 2: Sequence Brute Force

The lock requires a specific 4-digit sequence (1324).
Use trial and error (brute force) or clues in the room to deduce the sequence.
Enter the correct sequence to unlock the second layer.
Layer 3: Retrieve the Key

Locate the physical key hidden somewhere in the room (e.g., inside a drawer or under an object).

## Clue UI

Purpose: Displays discovered clues and their connections to guide the player.

Clue List: Shows all discovered clues, which can be clicked for details.

## Graph System

Purpose: Visualizes suspects and the clues that implicate them, organized by room.
Structure:

Room-Based: The graph shows which clues were discovered in each room and how they connect to the suspects.
Dynamic Updates: As clues are discovered, the graph is updated with new connections to suspects.

Finally after solving all puzzles, collecting all clues and discovering all rooms , the player is required to choose who is the killer of Dr. Carrington.
