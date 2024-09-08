using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class EnemyAuthoring : MonoBehaviour
{
    public float health;
    public float3 initPosition;
    public float speed;
    public class EnemyBake : Baker<EnemyAuthoring>
    {
        public override void Bake(EnemyAuthoring authoring)
        {
            Entity enemyEntity = GetEntity(TransformUsageFlags.Dynamic);
            
            AddComponent(enemyEntity, new EnemyComponent
            {
                health = authoring.health,
                speed = authoring.speed
            });
            
            AddComponent(enemyEntity, new LocalTransform
            {
                Position = authoring.initPosition,
                Rotation = quaternion.identity,
                Scale = 1f
            });
        }
    }
}
