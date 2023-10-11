using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : ITick, IInitialize, ITerminate {

    private GameObject playerGO;

    public Rigidbody2D rb { get; private set; }
    public Transform transform;

    private Vector2 moveVector;

    private LevelManager levelManager;
    private BoxCollider2D feet;
    public CapsuleCollider2D hurtBox;
    private CircleCollider2D pickUpCollider;
    private List<BoxCollider2D> powerUpColliders;

    private bool touchedEndZone = false;

    // Only allow one active PowerUp at a time
    public enum PowerUps {
        NONE,
        SHIELD,
    }

    public PowerUps availablePower = PowerUps.NONE;
    public PowerUps activePower = PowerUps.NONE;

    public Player(LevelManager levelManager) {
        this.levelManager = levelManager;
        this.powerUpColliders = levelManager.powerUpColliders;
    }

    public void Initialize() {
        var playerPrefab = Resources.Load<GameObject>("Player");

        playerGO = GameObject.Instantiate(playerPrefab, levelManager.spawnPoint, new Quaternion());
        rb = playerGO.GetComponent<Rigidbody2D>();
        transform = playerGO.GetComponent<Transform>();
        feet = playerGO.GetComponent<BoxCollider2D>();
        hurtBox = playerGO.GetComponent<CapsuleCollider2D>();
        pickUpCollider = playerGO.GetComponent<CircleCollider2D>();
    }

    public void Tick() {
        // Player manages itself. Puts flags in GameState which other classes can poll
        if(!touchedEndZone && feet.IsTouching(levelManager.endTrigger)) {
            Debug.Log("End reached! Disabling inputs!");
            rb.gravityScale = 0.6f; // Gives better feeling, that of landing and not bouncing
            GameState.goalReached = true;
            touchedEndZone = true;
        }

        if(hurtBox.IsTouching(levelManager.groundCollider)) {
            Hit();
        }

        foreach(BoxCollider2D col in powerUpColliders) {
            if(pickUpCollider.IsTouching(col)) {
                col.gameObject.SetActive(false);
                GetRandomPower();
            }
        }

    }

    ///  Attempt to kill player
    public void Hit() {
        if(activePower == PowerUps.SHIELD) {
            return;
        }
        GameState.playerIsDead = true;
    }

    // To scale in the future with more PowerUps!
    private void GetRandomPower() {
        int power = UnityEngine.Random.Range(1, 2);
        availablePower = (PowerUps)power;
    }

    public void Terminate() {
    }
}
