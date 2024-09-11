using Unity.Entities;
using UnityEngine;

public struct PlayerComponent : IComponentData
{
    public float moveSpeed;
    public float shootCoolDown;
    public Entity bulletPrefab;
}
