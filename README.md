# Unity_DOTS

## Project explanation
 In this project we create a unity 2D project using Unity DOTS(Data-Oriented Technology Stack), the goal was to create a simple space shooter game, where we need some type of wave of enemies, player movement and shooting to kill the enemies.

 ## Setup
 When setting up a DOTS project we first need to install the Entities plugin that unity provieds, installing "Entity graphics" will also install some other useful tools needed for entity, note that installing "Entity" plugin is fine too. After installing this, it's good to install unitys new input system(fine to keep old system), once this is installed we create a gameinput, where we handle Move(WASD, with value Vector2) and Shoot(SPACE with value any).

 ## Movement
 To create a moving player we first create a player prefab, we then create 3 scripts for the player: "PlayerAuthioring", "PlayerComponent", "PlayerSystem", in this three scripts we delcare our variables like speed, position etc, we bake the variables and we handle all the movement in the "PlayerSystem".

 Now we need to create 2 more script for input: "InputSystem" and "InputComponent", same as before we create our variables for movement and a bool to check if we are shooting or not.

 ## Shooting 
For shooting we need to create a BulletSystem and BulletComponent, in my bulletComponent I collision radius for checking if bullet is hitting the enemy later on.To explain it real short, all i do is loop though all enemies then i check the distance betwwen bullet and enemy, then i deal damage(-50 in this case) then i destroy the enemy. we use " entityManager.GetAllEntities()" to get all enemy entities.
