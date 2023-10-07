using UnityEngine;

/* This is (almost) the ONLY gameObject that inherits from MonoBehaviour.
   It manages all other of those functions through interfaces, to manage execution order.
   Some objects that only control their own data like, Animations, Effects, and Complex Behaviour.
   Things that are not dependent to run per tick on outside data can also be normal MonoBehaviours. */
public class Executor : MonoBehaviour {
    private Player player;
    private PlayerMovement playerMovement;
    private InputManager inputManager;
    private LevelManager levelManager;

    private void Awake() {

    }

    private void Start() {
        inputManager = new InputManager();
        inputManager.Initialize();
        levelManager = new LevelManager();
        levelManager.Initialize();
        player = new Player(levelManager);
        player.Initialize();
        playerMovement = new PlayerMovement(player, inputManager);
        playerMovement.Initialize();
    }

    private void FixedUpdate() {
        playerMovement.FixedTick();
    }

    private void Update() {
        inputManager.Tick();
        player.Tick();
        levelManager.Tick();
        Debug.Log("Executing");
    }

    private void OnDestroy() {
        playerMovement.Terminate();
        player.Terminate();
        inputManager.Terminate();
    }
}


/* Executor Interfaces */
public interface ITick {
    public void Tick();
}

public interface IInitialize {
    public void Initialize();
}

public interface ITerminate {
    public void Terminate();
}

public interface IFixedTick {
    public void FixedTick();
}
