using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public PlayerControls playerControls { get; private set; }
    public PlayerControls.GameplayActions gameplayControls { get; private set; }

    private void Awake()
    {
        playerControls = new PlayerControls();
        gameplayControls = playerControls.Gameplay;
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    private void OnDisable() {
        playerControls.Disable();
    }
}
