using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct EnemyComponent : IComponentData
{
    public float health;
    public float speed;
}
