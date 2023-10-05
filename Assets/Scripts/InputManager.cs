using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : IInitialize, ITick, ITerminate {

    private PlayerActions actions;

    public Vector2 moveVector { get; private set; }

    public void Initialize() {
        actions = new PlayerActions();
        actions.gameplay.Enable();
        // actions.gameplay.jump.performed += Jump;
    }

    public void Tick() {
        moveVector = actions.gameplay.move.ReadValue<Vector2>();
    }
    public void Terminate() {
        actions.gameplay.Disable();
    }
}