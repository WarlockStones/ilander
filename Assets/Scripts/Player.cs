using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;



// The idea is to move away from Unity Components. Have as much as possible in pure C#
// This is mostly recommended for large-scale projects. It is a bit overkill for a 2 week school assignment
public class Player : ITick, IFixedTick, IInitialize, ITerminate {

    private GameObject player;

    public Rigidbody2D rb { get; private set; }

    private Vector2 moveVector;
    public void Initialize() {
        var playerPrefab = Resources.Load<GameObject>("Player");
        // TODO: Create player spawn point. Maybe use tags?
        player = GameObject.Instantiate(playerPrefab, new Vector3(7f, 4f, 0f), new Quaternion());
        rb = player.GetComponent<Rigidbody2D>();
    }

    public void FixedTick() {
    }

    public void Tick() {
    }

    public void Terminate() {
    }
}
