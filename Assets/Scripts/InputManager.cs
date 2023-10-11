using UnityEngine;

public class InputManager : IInitialize, ITick, ITerminate {

    private PlayerActions actions;
    public static float usePressed;
    public bool disableInput;

    public static Vector2 moveVector { get; private set; }

    public void Initialize() {
        actions = new PlayerActions();
        actions.gameplay.Enable();
        actions.gameplay.pause.performed += ctx => GameState.TogglePause();
    }

    public void Tick() {
        if(GameState.goalReached) disableInput = true;
        moveVector = disableInput ?  new Vector2(0,0) : actions.gameplay.move.ReadValue<Vector2>();
        usePressed = actions.gameplay.use.ReadValue<float>();
    }
    public void Terminate() {
        actions.gameplay.Disable();
    }
}
