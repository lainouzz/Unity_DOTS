# Unity_DOTS

# NOTE: The current release is the stable version, can't get my current new build to work, a unity crash happens after the splash screen!!!

## Project explanation
 In this project we created a Unity 2D project using Unity DOTS(Data-Oriented Technology Stack), the goal was to create a simple space shooter game, where we needed some type of wave of enemies, player movement and shooting to kill the enemies.

 ## Setup
 When setting up a DOTS project we first need to install the Entities plugin that Unity provides, installing "Entity graphics" will also install some other useful tools needed for the entity, note that installing "Entity" plugin is fine too. After installing this, it's good to install Unity's new input system(fine to keep the old system), once this is installed we create a game input, where we handle Move(WASD, with value Vector2) and Shoot(SPACE with value any).

 ## Movement
 To create a moving player we first create a player prefab, we then create 3 scripts for the player: "PlayerAuthioring", "PlayerComponent", and "PlayerSystem", in these three scripts we declare our variables like speed, position, etc, we bake the variables and we handle all the movement in the "PlayerSystem".

 Now we need to create 2 more scripts for input: "InputSystem" and "InputComponent", same as before we create our variables for movement and a bool to check if we are shooting or not.

 ## Shooting 
For shooting we need to create a BulletSystem and BulletComponent, in my bulletComponent I collision radius for checking if the bullet is hitting the enemy later on. To explain it really briefly, all I do is loop through all enemies then I check the distance between bullet and enemy, then I deal damage(-50 in this case), and then I destroy the enemy. we use " entityManager.GetAllEntities()" to get all enemy entities.
!!!please note i didn't find a way to use collision to work with dots/entities, the way we handle collision is to check the bullet.position is close to enemy.position, it works but ofc it's not perfect!!!

## Optimization
Unity's DOTS and ECS are designed to minimize the heap allocation by using struct, we use struct for all the Components and systems scripts, this reduces the memory fragmentation and avoids garbage collection issues.
You might notice how we also use "Allocator.Temp" for ToEntityArray/NativeArray, this is if the memory to only be allocated temporarily and is quickly disposed of.

We use [BurstCompiler] for most scripts, the burst compiler further optimizes the stack usage by reducing the number of function calls, this is also good for improving performance on both CPU and memory.
For all our Authoring scripts we define our variables, like speed, health, etc, these are then later converted to ECS components at runtime via the "Bake"

Stack allocations are used in the Transforms, like playerTransform, bulletTransform etc, this is ti ensure fast memory allocation and deallocation.
Heap is used for Components, like PlayerComponment, BulletComponents etc, we do use structs as mentioned above to minimize the heap allocation.

A lot of the scripts use pretty much the same "optimizations" due to the small project there isn't much to optimize, but if we would instantiate thousands and thousands of entities then we would notice it much better, but now we just spawn a few enemies each second.
