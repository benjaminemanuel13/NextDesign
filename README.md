# Next Game Designer

Have the Sprites, 8x8 Tiles and 16x16 Tiles saving to DB and generating correctly ready to insert into your sajmplus Assembly projects.

May be z88dk compatible (I can't remember the db lines struture in z88dk but would be easy to tweek)

TileMap designer next to do, though I might do the assembly generation (skeleton game with sprites and tiles from here) next.

Be sure to read readme.txt in CoreGame folder!  Important if you want to use vscode to debug and run.

[Edit 16/08/2025]

Have added capability to add new Sprites, 8x8 Tiles and 16x16 Tiles which save in DB when added.

[Edit 18/08/2025]

The sprite, tile8 and tile16 forms populate from default data but you can't actually edit/save them without clicking on them in the Project Window.  Once selected it enables the Save button on the form.

[Edit 25/08/2025]

Have added speech recognition/synthesis and basic AI Agent (Dynamic) ready to make the project Speech / AI driven.
You need to have Azure OpenAI and OpenAI keys (you can choose one or the other and wangle the code to use the one you want) and an Azure Speech Service Key/Endpoint for these features.

[Edit 25/08/2025 - pt2]

If you are following this project you may be interested in SmileMidi, a midi controller (keyboard) driven sound system the will eventually be used to generate sound for the Next Game.
See: https://github.com/benjaminemanuel13/SmileMidi  (its early days just now but plays tones when keyboard used) don't forget to add the NuGet package from the NuGets folder.

If you find this useful please let me know at benjaminemanuel13@gmail.com
