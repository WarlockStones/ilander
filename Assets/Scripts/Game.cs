using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

/* The Game Manager. Loads settings, scenes, state, and Executor. */
public class Game{
    [RuntimeInitializeOnLoadMethod]
    public static void InitGame() {
        Debug.Log("Game!");
        // var gameStatePrefab = Resources.Load<GameState>("GameState");
        // var gameState = GameObject.Instantiate(gameStatePrefab);
        // gameState.Initialize();

        // create the actual game object and add component from code
        // var go = new GameObject();
        // go.name = "GameState";
        // var gameState = go.AddComponent<GameState>();
        // gameState.Initialize()

        /* Initialize everything that needs to be in every scene */
        var executorPrefab = Resources.Load<Executor>("Executor");
        var executor = GameObject.Instantiate(executorPrefab);
    }
}
