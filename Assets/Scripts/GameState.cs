// Keeps track on the state of the game.
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameState {
    public static bool goalReached = false;
    public static bool playerIsDead = false;
    public static int currentSceneIndex = 0;
    public static bool gameIsPaused;

    private static Executor executor;

    // MainMenu state and Play State are different!
    // On start of program we enter MainMenu (scene0). Then on hitting "Start" we go into Play State

    private void ResetStateVariables() {
        goalReached = false;
        playerIsDead = false;
    }

    public static void TogglePause() {
        if(!gameIsPaused) {
            // Pause the game
            gameIsPaused = true;
            Time.timeScale = 0;
            executor.uiManager.SetPauseMenu(true);
        }
        else if(gameIsPaused) {
            // Resume the game
            gameIsPaused = false;
            Time.timeScale = 1;
            executor.uiManager.SetPauseMenu(false);
        }
    }

    public GameState() {
        SceneManager.sceneLoaded += OnSceneLoaded;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public static void InitExecutor() {
        var executorGO = new GameObject();
        executorGO.name = "Executor";
        executor = executorGO.AddComponent<Executor>();
    }

    // This does not run when we hit "Play"
    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
        Time.timeScale = 1;
        currentSceneIndex = scene.buildIndex;
        ResetStateVariables();
        InitExecutor();
    }
}
