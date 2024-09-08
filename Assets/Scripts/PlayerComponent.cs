using Unity.Entities;

public struct PlayerComponent : IComponentData
{
    public float moveSpeed;
    public float shootCoolDown;
    public Entity bulletPrefab;
}
