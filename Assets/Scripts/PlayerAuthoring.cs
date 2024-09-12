using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PlayerAuthoring : MonoBehaviour
{
   public float moveSpeed = 5;
   public float shootCoolDown = 1;
   public GameObject bulletPrefab;

   public class PlayerBake : Baker<PlayerAuthoring>
   {
      public override void Bake(PlayerAuthoring authoring)
      {
         Entity playerEntity = GetEntity(TransformUsageFlags.None);
         
         AddComponent(playerEntity, new PlayerComponent
         {
            moveSpeed = authoring.moveSpeed,
            shootCoolDown = authoring.shootCoolDown,
            bulletPrefab = GetEntity(authoring.bulletPrefab, TransformUsageFlags.None)
         });
      }
   }
}
