using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : IInitialize, IFixedTick, ITerminate {
    private Player player;
    private Rigidbody2D rb;
    private Transform playerTransform;
    private InputManager inputManager;

    private GameObject thrusterR;
    private GameObject thrusterL;

    public PlayerMovement(Player player, InputManager inputManager) {
        this.player = player;
        this.inputManager = inputManager;
    }
    public void Initialize() {
        rb = player.rb;
        playerTransform = player.rb.transform;

        // Calculate positions for where to put thrusters
        var cCollider = rb.GetComponent<CircleCollider2D>();
        Vector2 topOfShip = rb.transform.position;
        topOfShip += new Vector2(cCollider.offset.x, cCollider.offset.y + cCollider.radius);

        thrusterR = new GameObject("thruster_r");
        thrusterR.transform.parent = playerTransform;
        thrusterR.transform.position = new Vector2(topOfShip.x + cCollider.radius, topOfShip.y);

        thrusterL = new GameObject("thruster_l");
        thrusterL.transform.parent = playerTransform;
        thrusterL.transform.position = new Vector2(topOfShip.x - cCollider.radius, topOfShip.y);


    }
    public void FixedTick() {
        switch(inputManager.moveVector.x) {
            case < 0:
                // Move to the left
                rb.AddForceAtPosition(thrusterL.transform.right * -1, thrusterL.transform.position);
                break;
            case > 0:
                // Move to the right
                rb.AddForceAtPosition(thrusterR.transform.right, thrusterR.transform.position);
                break;
        }
        if(inputManager.moveVector.y > 0) {
            rb.AddForceAtPosition((rb.transform.up * 10f), rb.position);
        }
    }
    public void Terminate() {
    }
}
