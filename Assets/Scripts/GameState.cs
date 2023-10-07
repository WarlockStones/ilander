// Keeps track on the state of the game.
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState {
    public static bool goalReached = false;
    public static bool playerIsDead = false;
    public static int currentSceneIndex = 0;

    public static void ResetStateVariables() {
        goalReached = false;
        playerIsDead = false;
    }

    public GameState() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public static void InitExecutor() {
        var executorGO = new GameObject();
        executorGO.name = "Executor";
        var executor = executorGO.AddComponent<Executor>();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
        currentSceneIndex = scene.buildIndex;
        ResetStateVariables();
        InitExecutor();
        Debug.Log("GameState is initializing an Executor");
    }

}
