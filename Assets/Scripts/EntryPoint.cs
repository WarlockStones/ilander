using UnityEngine;
using UnityEngine.SceneManagement;

// The EntryPoint (main) of the game. This is always what will be initialized on start of the game
public class EntryPoint {


    [RuntimeInitializeOnLoadMethod]
    public static void InitGame() {
        // var gameStatePrefab = Resources.Load<GameState>("GameState");
        // var gameState = GameObject.Instantiate(gameStatePrefab);
        // gameState.Initialize();

        // create the actual game object and add component from code
        // var go = new GameObject();
        // go.name = "GameState";
        // var gameState = go.AddComponent<GameState>();
        // gameState.Initialize()

        /* The Game Manager. Loads settings, scenes, game state, and Executor. ???*/
        /* Initialize everything that needs to be in every scene */
        GameState gameState = new GameState();
        GameState.InitExecutor();
    }
}
