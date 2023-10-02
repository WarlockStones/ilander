using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

// The idea is to move away from Unity Components. Have as much as possible in pure C#
// This is mostly recommended for large-scale projects. It is a bit overkill for a 2 week school assignment
public class Player : ITick, IFixedTick, IInitialize, ITerminate {

    // Rename to PlayerControls?
    private PlayerActions actions;
    private GameObject player;
    private Rigidbody2D rb;

    private Vector2 moveVector;
    public void Initialize() {
        // On enable
        // Move to an InputManager class:
        actions = new PlayerActions();
        actions.gameplay.jump.performed += Jump;
        actions.gameplay.Enable();

        // Initialize player prefab at PlayerSpawnPoint
        var playerPrefab = Resources.Load<GameObject>("Player");
        // TODO: Create player spawn point. Maybe use tags?
        player = GameObject.Instantiate(playerPrefab, new Vector3(7f,4f,0f), new Quaternion());
        rb = player.GetComponent<Rigidbody2D>();
    }

    public void FixedTick() {
        rb.AddForce(moveVector * 10, ForceMode2D.Force);

    }

    public void Tick() {
        // TODO: Fix movement for lander. Up only. Then other thrusters for left or right, tilt.
        moveVector = actions.gameplay.move.ReadValue<Vector2>();
        Debug.Log("MoveVector: "+moveVector);
    }

    public void Terminate() {
        // On terminate
        actions.gameplay.Disable();
    }

    private void Jump(InputAction.CallbackContext ctx) {
        Debug.Log("JUMP");
    }

}
