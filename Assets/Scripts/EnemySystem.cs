using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[BurstCompile]
public partial struct EnemySystem : ISystem
{
   private EntityManager entityManager;
   private EntityCommandBuffer ecb;
   
   public void OnUpdate(ref SystemState state)
   {
      var ecbSystem = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
      ecb = ecbSystem.CreateCommandBuffer(state.WorldUnmanaged);
      
      entityManager = state.EntityManager;

      var query = entityManager.CreateEntityQuery(typeof(EnemyComponent), typeof(LocalTransform));
      var enemyEntity = query.ToEntityArray(Allocator.Temp);
     
      foreach (Entity enemy in enemyEntity)
      {
         if (entityManager.HasComponent<EnemyComponent>(enemy) && entityManager.HasComponent<LocalTransform>(enemy))
         {
            var enemyComponent = entityManager.GetComponentData<EnemyComponent>(enemy);
            var enemyTransform = entityManager.GetComponentData<LocalTransform>(enemy);
            
            if (enemyComponent.health <= 0)
            {
               ecb.DestroyEntity(enemy);
               continue;
            }
            if (enemyTransform.Position.y < -5)
            {
               ecb.DestroyEntity(enemy);
               continue;
            }
            Move(ref state, enemy, ref enemyTransform, ref enemyComponent);
         }
      }
      enemyEntity.Dispose();
   }

   private void Move(ref SystemState state,Entity enemy , ref LocalTransform enemyTransform, ref EnemyComponent enemyComponent)
   {
      enemyTransform.Position += new float3(0, -1, 0) * enemyComponent.speed * SystemAPI.Time.DeltaTime;
      
      state.EntityManager.SetComponentData(enemy, enemyTransform);
   }
}
