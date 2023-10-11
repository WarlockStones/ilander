using UnityEngine;

// The EntryPoint (main) of the game. This is always what will be initialized on start of the game
public class EntryPoint {
    [RuntimeInitializeOnLoadMethod]
    public static void InitGame() {
        GameState gameState = new GameState();
        GameState.InitExecutor();
    }
}
