using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;



// The idea is to move away from Unity Components. Have as much as possible in pure C#
// This is mostly recommended for large-scale projects. It is a bit overkill for a 2 week school assignment
public class Player : ITick, IInitialize, ITerminate {

    private GameObject player;

    public Rigidbody2D rb { get; private set; }

    private Vector2 moveVector;

    private LevelManager levelManager;
    private BoxCollider2D feet;
    private CircleCollider2D hurtBox;

    private bool isInEndZone = false;
    private bool isgroundColliderNull;

    public Player(LevelManager levelManager) {
        this.levelManager = levelManager;
    }

    private void Start() {
        isgroundColliderNull = levelManager.groundCollider == null;
    }
    public void Initialize() {
        var playerPrefab = Resources.Load<GameObject>("Player");

        player = GameObject.Instantiate(playerPrefab, levelManager.spawnPoint, new Quaternion());
        rb = player.GetComponent<Rigidbody2D>();
        feet = player.GetComponent<BoxCollider2D>();
        hurtBox = player.GetComponent<CircleCollider2D>();
    }

    public void Tick() {
        // Player manages itself. It checks if it is triggering an end. Then calls the levelManager to end
        if(!isInEndZone && feet.IsTouching(levelManager.endTrigger)) {
            Debug.Log("End reached! Disabling inputs!");
            rb.gravityScale = 0.6f; // Gives better feeling. That of landing and not bouncing
            GameState.goalReached = true;
            isInEndZone = true;
        }
        if(hurtBox.IsTouching(levelManager.groundCollider)) {
            Debug.Log("DEATH!");
            // Pause game!
            GameState.playerIsDead = true;
        }
    }


    public void Terminate() {
    }
}
