using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateBefore(typeof(EnemySystem))]
[BurstCompile]
public partial struct BulletSystem : ISystem
{
    private void OnUpdate(ref SystemState state)
    {
        EntityManager entityManager = state.EntityManager;
        NativeArray<Entity> allEntities = entityManager.GetAllEntities();

        foreach (Entity entity in allEntities)
        {
            if (entityManager.HasComponent<BulletComponent>(entity))
            {
                LocalTransform bulletTransform = entityManager.GetComponentData<LocalTransform>(entity);
                BulletComponent bulletComponent = entityManager.GetComponentData<BulletComponent>(entity);
                bulletTransform.Position += bulletComponent.speed * SystemAPI.Time.DeltaTime * bulletTransform.Up();
                entityManager.SetComponentData(entity, bulletTransform);

                NativeArray<Entity> enemyEntities = entityManager.GetAllEntities(Allocator.Temp);
                
                foreach (Entity enemy in enemyEntities)
                {
                    if (entityManager.HasComponent<EnemyComponent>(enemy))
                    {
                        LocalTransform enemyTransform = entityManager.GetComponentData<LocalTransform>(enemy);
                        EnemyComponent enemyComponent = entityManager.GetComponentData<EnemyComponent>(enemy);
                        float3 enemyPos = enemyTransform.Position;
                        
                        if (math.distance(bulletTransform.Position, enemyPos) < bulletComponent.collisionRadius)
                        {
                            entityManager.DestroyEntity(entity);

                            enemyComponent.health -= 50;
                            entityManager.SetComponentData(enemy, enemyComponent);
                            break;
                        }
                    }
                }
                enemyEntities.Dispose();
            }
        }
        allEntities.Dispose();
    }
}
