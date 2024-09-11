using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateAfter(typeof(InputSystem))]
public partial struct PlayerSystem : ISystem
{
    private Entity playerEntity;
    private Entity inputEntity;
    private EntityManager entityManager;
    private PlayerComponent playerComponent;
    private InputComponent inputComponent;
    private LocalTransform playerTransform;
    
    private EntityCommandBuffer ecb;

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        
    }
    
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecbSystem = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
        ecb = ecbSystem.CreateCommandBuffer(state.WorldUnmanaged);
        
        entityManager = state.EntityManager;
        playerEntity = SystemAPI.GetSingletonEntity<PlayerComponent>();
        inputEntity = SystemAPI.GetSingletonEntity<InputComponent>();

        playerComponent = entityManager.GetComponentData<PlayerComponent>(playerEntity);
        inputComponent = entityManager.GetComponentData<InputComponent>(inputEntity);
        
        Move(ref state);
        Shoot(ref state);
    }
    
    private void Move(ref SystemState state)
    {
        playerTransform = entityManager.GetComponentData<LocalTransform>(playerEntity);
        playerTransform.Position +=
            new float3(inputComponent.movement * playerComponent.moveSpeed * SystemAPI.Time.DeltaTime, 0);
        
        entityManager.SetComponentData(playerEntity, playerTransform);
    }

    private float nextTimeShoot;
    private void Shoot(ref SystemState state)
    {
        if (inputComponent.pressingSpace && nextTimeShoot < SystemAPI.Time.ElapsedTime)
        {
            EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.Temp);
            
            Entity bulletEntity = entityManager.Instantiate(playerComponent.bulletPrefab);
            ecb.AddComponent(bulletEntity, new BulletComponent
            {
                speed = 10,
                collisionRadius = 0.5f
            });
            

            LocalTransform bulletTransform = entityManager.GetComponentData<LocalTransform>(bulletEntity);
            bulletTransform.Rotation = entityManager.GetComponentData<LocalTransform>(playerEntity).Rotation;
            LocalTransform playerTransform = entityManager.GetComponentData<LocalTransform>(playerEntity);
            bulletTransform.Position =
                playerTransform.Position + playerTransform.Right() * -0.08f + playerTransform.Up() * 0.5f;
            
            ecb.SetComponent(bulletEntity, bulletTransform);
            ecb.Playback(entityManager);
            
            nextTimeShoot = (float)SystemAPI.Time.ElapsedTime + playerComponent.shootCoolDown;
        }
    }
}
