// Keeps track on the current level, and objects of importance in it. spawn_point, end_zone, etc.
// This is a terrible class. It is far too bloated and should be borken into smaller pieces

using Cinemachine;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : IInitialize, ITick {
    public Vector3 spawnPoint = new Vector3(0, 0, 0);
    public BoxCollider2D endTrigger;
    public EdgeCollider2D groundCollider;
    private float endTimer = 0;
    public readonly List<BoxCollider2D> powerUpColliders = new List<BoxCollider2D>();

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

        // I would prefer to not utilize Unity Editor tags. But, this worked better than to loop and compare name string
        // Preferable it would be a MonoBehaviour component that holds an "interface tag"
        GameObject[] powerUps = GameObject.FindGameObjectsWithTag("PowerUp");
        foreach(GameObject o in powerUps) {
            BoxCollider2D col = o.GetComponent<BoxCollider2D>();
            if(col == null) {
                Debug.LogWarning("'power_up' found but it is missing a BoxCollider2D!");
                continue;
            }
            powerUpColliders.Add(col);
        }

        // bunkers = GameObject.FindGameObjectsWithTag("Bunker");
    }
    public void Tick() {
        if(GameState.goalReached) {
            if(endTimer > 2 && !GameState.playerIsDead) {
                LoadNextLevel();
            }
            endTimer += Time.unscaledDeltaTime; // Unscaled to run while paused
        }

        if(GameState.playerIsDead) {
            Time.timeScale = 0;

            if(endTimer > 1.5) {
                SceneManager.LoadScene(GameState.currentSceneIndex);
            }
            endTimer += Time.unscaledDeltaTime;
        }
    }

    public void LoadNextLevel() {
        GameState.currentSceneIndex++;
        SceneManager.LoadScene(GameState.currentSceneIndex);
    }
}
