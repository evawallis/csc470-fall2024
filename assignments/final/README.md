# Early Bird Gets the Worm

## Gameplay Description:

“Early Bird Gets the Worm” is a world exploration game. The player plays as a sparrow whose goal is to explore the forest (world) and find a worm. The player can receive hints and clues by collecting seeds, scattered throughout the forest, and talking to other NPCs (ex. Squid, Pudu). Specifically, if the player collects and brings enough seeds to the squid, he will reward the sparrow with a map with the exact location of the worm. There are a couple of static scenes in the game where the sparrow talks to another character, which add to the immersive storytelling aspect of the game. There is no way to lose. ![Sparrow Gameplay](/assignments/final/sparrow%20gameplay.png)

## Input:

The player moves around using the arrow keys. The player can jump or fly by pressing the spacebar once or multiple times respectively. The player can eat (required to collect a seed) by pressing ‘e’. The conversation elements are prewritten, but the user can advance the story by pressing the spacebar. The static ‘scene’ elements during the main gameplay (sparrow interaction with squid and sparrow interaction with worm) are triggered by the proximity of the player to the location of the NPC. The player kills (eats) the worm and ends the game. UI buttons allow the user to see the instructions, close dialogue boxes, and view the map when available. ![Sparrow Input](/assignments/final/sparrow%20input.png)

## Visual Style:

I was inspired by the game “A Short Hike” for the premise and visual style. I utilized a low-poly forest environment and created a color palette based on the colors of the elements (greens, browns, yellows, blues). I found and used a [Unity Asset package](https://assetstore.unity.com/packages/3d/characters/animals/quirky-series-free-animals-pack-178235) of a variety of animals and animations. From this package, I used the sparrow, squid, pudu, and worm. This gave a cohesive look to the characters in the game. Additionally, I used animation states like run, jump, fly, spin, eat, amongst others. The package also included shape keys to represent different eye/face motions, which I used to represent different emotions. For example, the squid is sleeping, the worm gets scared when the bird gets close, and the worm dies after the bird eats him. Not only did I choose this package for the cohesiveness in the design of the characters, but the animations helped to show lively emotion and interaction. When the sparrow and pudu have their initial conversation, the animation helps indicate who is speaking. Even when the player is idle, the sparrow has an idle animation state (ex. bouncing). I chose a font with a casual, childlike handwriting, which I felt fit the low-poly appearance of the game. ![Sparrow Visual](/assignments/final/sparrow%20visual.png)

## Audio Style:

Each character has a unique voice to play when speaking (ex. Animal Crossing voices). There is a mellow, relaxing music in the background to pair with the visual style and theme. There are footsteps sounds when the sparrow is walking, air whooshing sounds when flying, and water sounds when near the ponds or talking with the squid. Natural sounds and ambience lend to the effectiveness of gameplay.

## Story/Theme Description: 

A sparrow enters a forest and is greeted by a deer called Pudu. Pudu introduces a special adventure for Sparrow to go on, in lieu of searching for breakfast. Pudu states the basic rules (collect seeds, uncover clues, find the worm) and the Sparrow eagerly heads off.

Sparrow adventures through the forest collecting small, yellow seeds. Upon adventuring through the forest, Sparrow comes across a squid who is taking a nap in the pond. When Sparrow approaches, Squid says that if Sparrow brings 20 seeds, he will help him find the worm, before going back to sleep. Sparrow keeps on collecting seeds and returns to Squid. As promised, Squid takes the seeds and provides Sparrow with a map. A purple star shows where Squid is located, and a pink X represents the worm’s location. Sparrow continues on, following the map to the location of the worm. When Sparrow approaches, the worm begins to shiver in fear. Sparrow eats the worm, completing Pudu’s mission, and finally getting breakfast. An ending scene shows Pudu and Sparrow celebrating with a dead worm, with an option to  play again. ![Sparrow Story](/assignments/final/sparrow%20story.png)

## Low-Bar: 

Working game. Sparrow's goal is to find the worm. Basic keyboard implementation (space for jump, arrow keys for movement). Seeds to demonstrate path to worm from spawn point and can be collected but not exchanged. No Squid. No Pudu. No end scene. Basic start scene with rules but no characters or story. Basic UI (seed counter and instructions box only). No animations. No audio. No world boundaries. Worm static location. No option to play again.

## Target:

Working game. Sparrow's goal is to find the worm. Basic keyboard implementation (space for jump, arrow keys for movement, ‘e’ to eat). Seeds are scattered randomly and can be collected to pay for a hint. Basic movement animations but no emotive animations (no shape keys). Squid gives hint with a direction (north, south, east, west) but no map. Start scene shows Pudu giving instructions but no Sparrow and no 2-way conversation. No scene fade ins/outs. No end scene, play again button shows after worm death. No audio. World boundaries implemented. Worm static location. 

## High-Bar:

Working game. Sparrow's goal is to find the worm. Basic keyboard implementation (space for jump, arrow keys for movement, ‘e’ to eat). Seeds are scattered randomly and can be collected to pay for a hint. Movement animations (run, fly, eat, idle bounce, attacked, spin, jump, swim) and emotive animations (blink, happy, dead, scared, sleep, dizzy, sad). Squid gives a hint by providing a map, and states requirement of >=20 seeds if amount not already reached. Start scene is a conversation between Sparrow and Pudu with animations. World boundaries reroute Sparrow in the opposite direction. Worm moves location every new game. Scene fades and post processing. End scene with all characters animated and play again button. Audio with natural sounds (footsteps, wind, water), background music, and unique voices for each character while speaking.
