using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public partial class InputSystem : SystemBase
{
    private GameInput gameInput;
    
    protected override void OnCreate()
    {
        if (!SystemAPI.TryGetSingleton<InputComponent>(out InputComponent input))
        {
            EntityManager.CreateEntity(typeof(InputComponent));
        }

        gameInput = new GameInput();
        gameInput.Enable();
    }
    
    protected override void OnUpdate()
    {
        Vector2 moveVector = gameInput.GamePlay.Move.ReadValue<Vector2>();
        bool isPressingSpace = Mathf.Approximately(gameInput.GamePlay.Shoot.ReadValue<float>(), 1) ? true : false;
        
        SystemAPI.SetSingleton(new InputComponent {movement = moveVector, pressingSpace = isPressingSpace});
    }

}
