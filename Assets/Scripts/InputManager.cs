using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : IInitialize, ITick, ITerminate {

    private PlayerActions actions;
    public bool disableInput;

    public Vector2 moveVector { get; private set; }

    public void Initialize() {
        actions = new PlayerActions();
        actions.gameplay.Enable();
        // actions.gameplay.jump.performed += Jump;
    }

    public void Tick() {
        if(GameState.goalReached) disableInput = true;
        moveVector = disableInput ?  new Vector2(0,0) : actions.gameplay.move.ReadValue<Vector2>();
    }
    public void Terminate() {
        actions.gameplay.Disable();
    }
}
