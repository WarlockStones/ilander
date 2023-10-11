using UnityEngine;

/* This is (almost) the ONLY gameObject that inherits from MonoBehaviour.
   It manages all other of those functions through interfaces, to manage execution order.
   Some objects that only control their own data like, Animations, Effects, and Complex Behaviour.
   Things that are not dependent to run per tick on outside data can also be normal MonoBehaviours. */
public class Executor : MonoBehaviour {
    private Player player;
    private PlayerMovement playerMovement;
    private PlayerPowers playerPowers;
    private InputManager inputManager;
    private LevelManager levelManager;
    public UIManager uiManager; // public so that GameState can read it! And avoid static variables
    private Bunker bunker;

    private bool isMenuState;

    public Executor() {
        // For now we only have one scene for menuState. So we can hard-code it (ugly)
        if(GameState.currentSceneIndex == 0) {
            isMenuState = true;
        }
    }

    private void Awake() {

    }

    private void Start() {
        levelManager = new LevelManager();
        uiManager = new UIManager(levelManager);

        if (GameState.currentSceneIndex == 0)
        {
            uiManager.InstantiateMainMenu();
        }
        if(!isMenuState) {
            InitializePlayState();
        }
    }

    public void InitializePlayState() {
        inputManager = new InputManager();
        inputManager.Initialize();
        levelManager = new LevelManager();
        levelManager.Initialize();
        uiManager.Initialize();
        player = new Player(levelManager);
        player.Initialize();
        playerMovement = new PlayerMovement(player, inputManager);
        playerMovement.Initialize();
        playerPowers = new PlayerPowers(player);
        playerPowers.Initialize();
        bunker = new Bunker(player);
        bunker.Initialize();
    }

    private void FixedUpdate() {
        if(isMenuState) return;
        playerMovement.FixedTick();
    }

    private void Update() {
        uiManager.Tick();

        if(isMenuState) return;
        inputManager.Tick();
        player.Tick();
        playerPowers.Tick();
        levelManager.Tick();
        bunker.Tick();
    }

    private void OnDestroy() {

        if(isMenuState) return;
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
