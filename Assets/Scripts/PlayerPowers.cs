using System;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

/* This should also just be a MonoBehaviour... */
// Then I can utilize it as a component on a prefab and check for OnTriggerEnter, etc.
public class PlayerPowers : ITick, IInitialize {
    private Player player;
    private Transform playerTransform;
    private GameObject powerObj;
    private SpriteRenderer powerSpriteRenderer;

    private GameObject shieldObj;
    private SpriteRenderer shieldSprite;
    private int activeShields = 0;
    private Color activeShieldColor = Color.yellow;

    public PlayerPowers(Player player) {
        this.player = player;
    }

    public void Initialize() {
        playerTransform = player.transform;

        GameObject powerPrefab = Resources.Load<GameObject>("Power");
        powerObj = Object.Instantiate(powerPrefab);
        powerSpriteRenderer = powerObj.GetComponent<SpriteRenderer>();

        // Instantiate shield graphics. The bubble
        shieldObj = new GameObject();
        shieldObj.name = "shield_graphics";
        shieldSprite = shieldObj.AddComponent<SpriteRenderer>();
        shieldSprite.sprite = Resources.Load<Sprite>("Bubble");
        shieldSprite.sortingOrder = 10;
        shieldSprite.color = new Color(255, 255, 255, 0); // Make invisible
    }

    public void Tick() {
        // Bubble is always around the player, it is just invisible at times
        shieldObj.transform.position = player.transform.position;

        // What power is being held
        switch(player.availablePower) {
            case Player.PowerUps.NONE:
                powerSpriteRenderer.color = new Color(255, 255, 255, 0);
                break;
            case Player.PowerUps.SHIELD:
                powerSpriteRenderer.color = new Color(255, 255, 0, 1);
                AnimateHeldPower();
                if(InputManager.usePressed > 0) {
                    player.availablePower = Player.PowerUps.NONE;
                    ActivateShield();
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        // What power is active
        shieldSprite.color = player.activePower == Player.PowerUps.SHIELD ? activeShieldColor : new Color(255, 255, 255, 0); // Invisible
    }

    private float amp = 1.5f;
    private float freq = 4.0f;
    private void AnimateHeldPower() {
        // Move power around player using cos and sin
        Vector3 playerPos = playerTransform.position;
        float x = playerPos.x + (Mathf.Cos(Time.time * freq) * amp);
        float y = playerPos.y + (Mathf.Sin(Time.time * freq) * amp);
        powerObj.transform.position = new Vector3(x, y, 0);
    }

    // Make player invulnerable for 5 seconds
    private async void ActivateShield() {
        activeShields++;
        player.activePower = Player.PowerUps.SHIELD;
        activeShieldColor = Color.yellow;
        await Task.Delay(4000);

        if(activeShields == 1) {
            activeShieldColor = Color.red;
        }
        await Task.Delay(1000);

        activeShields--;

        if(activeShields == 0) { // To not cancel new PowerUp!
            player.activePower = Player.PowerUps.NONE;
        }
    }
}
