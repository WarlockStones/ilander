// Keeps track on the current level, and objects of importance in it. spawn_point, end_zone, etc.

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : IInitialize, ITick{
    public Vector3 spawnPoint = new Vector3(0, 0, 0);
    public BoxCollider2D endTrigger;
    public EdgeCollider2D groundCollider;
    private float endTimer = 0;

    public void Initialize() {
        var spawnObj = GameObject.Find("spawn_point");
        if(spawnObj == null) {
            Debug.LogWarning("LevelManager could not find object called 'spawn_point'. Defaulting to 0,0,0");
            spawnPoint = new Vector3(0, 0, 0);
        }
        else {
            spawnPoint = spawnObj.transform.position;
        }

        var endObj = GameObject.Find("end_trigger");
        if(endObj == null) {
            Debug.LogWarning("Could not find 'end_trigger' game object. Finishing level may not be possible!");
            endTrigger = null;
        }
        else {
            endTrigger = endObj.GetComponent<BoxCollider2D>();
        }

        var groundObj = GameObject.Find("ground");
        if(groundObj == null) {
            Debug.LogWarning("Could not find 'ground' game object. Death may not be possible!");
        }
        else {
            groundCollider = groundObj.GetComponent<EdgeCollider2D>();
            if(groundCollider == null) {
                Debug.LogWarning("'ground' object does not have a valid EdgeCollider2D. Death may not be possible!");
            }
        }

    }
    public void Tick() {
        if(GameState.goalReached) {
            // Start ticking endTimer
            if(endTimer > 2) {
                // Check if player is alive > If true > Change Level to next level
                if(!GameState.playerIsDead) {
                    // Load next scene
                    GameState.currentSceneIndex++;
                    SceneManager.LoadScene(GameState.currentSceneIndex);
                }
            }
            endTimer += Time.deltaTime;
        }
    }
}
