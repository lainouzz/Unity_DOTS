using Unity.Burst;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Entities;
using UnityEngine;
using Random = UnityEngine.Random;

partial struct SpawnerSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        foreach (RefRW<Spawner> spawner in SystemAPI.Query<RefRW<Spawner>>())
        {
            if (spawner.ValueRO.nextSpawnTime < SystemAPI.Time.ElapsedTime)
            {
                Entity newEntity = state.EntityManager.Instantiate(spawner.ValueRO.prefab);
                float3 pos = new float3(Random.Range(-8, 8), 4, 0);
                
                state.EntityManager.SetComponentData(newEntity, new LocalTransform
                {
                    Position = pos,
                    Scale = 1f
                });
                spawner.ValueRW.nextSpawnTime = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.spawnRate;
            }
        }
    }
}
