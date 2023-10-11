using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : IInitialize, IFixedTick, ITerminate {
    private Player player;
    private Rigidbody2D rb;

    private Transform thrusterR;
    private Transform thrusterL;

    public PlayerMovement(Player player) {
        this.player = player;
    }
    public void Initialize() {
        rb = player.rb;

        // Find thruster else post Error. The player prefab is not valid for PlayerMovement
        thrusterR = player.transform.Find("thruster_r");
        thrusterL = player.transform.Find("thruster_l");
        if(thrusterR == null || thrusterL == null) {
            Debug.LogError("Could not find Player Thruster transforms named thruster_r or thruster_l");
        }
    }

    public void FixedTick() {
        switch(InputManager.moveVector.x) {
            case < 0:
                // Move to the left
                rb.AddForceAtPosition(thrusterL.right * -1, thrusterL.position);
                break;
            case > 0:
                // Move to the right
                rb.AddForceAtPosition(thrusterR.right, thrusterR.position);
                break;
        }
        if(InputManager.moveVector.y > 0) {
            rb.AddForceAtPosition((rb.transform.up * 10f), rb.position);
        }
    }
    public void Terminate() {
    }
}
