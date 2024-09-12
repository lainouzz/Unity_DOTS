using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public struct Spawner : IComponentData
{
    public Entity prefab;
    public float2 spawnPosition;
    public float nextSpawnTime;
    public float spawnRate;

}
