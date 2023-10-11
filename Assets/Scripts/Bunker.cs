using Cinemachine;
using System.Threading.Tasks;
using UnityEngine;
using Object = System.Object;

public class Bunker : IInitialize, ITick {
    private Player player;
    private bool isEnabled = true;
    private Transform activeCannon;
    private GameObject bullet;
    private float shootTimer;
    private float shootCD = 2f;
    private float aggroRange = 21;

    private GameObject bunker;

    private GameObject bulletPrefab;
    public Bunker(Player player) {
        this.player = player;
    }

    public void Initialize() {
        bunker = GameObject.FindGameObjectWithTag("Bunker");
        if(bunker == null) {
            Debug.LogWarning("Bunker: No bunkers found in scene. Disabling Bunker behaviour");
            isEnabled = false;
            return;
        }

        // To a void a very rare bug
        if(bunker.transform.position == new Vector3(0, 0, 0)) {
            Debug.LogWarning("Warning a bunker's position is 0.0.0, calculating dot product will not be possible!");
            Debug.Log("Moving 0.0.0 bunker 0.1 in x to avoid dot-product bug! Please fix in scene to mute this message");
            Vector3 newPos = new Vector3(0.1f, 0, 0);
            bunker.transform.position = newPos;
        }
        bulletPrefab = Resources.Load<GameObject>("Bullet");
    }


    public void Tick() {
        if(!isEnabled) {
            return;
        }

        float dist = Vector3.Distance(bunker.transform.position, player.transform.position);
        if(dist > aggroRange) {
            return;
        }

        // Determines which turret should fire at the target. Using vector dot product!
        var dotProd = Vector2.Dot(bunker.transform.position - player.transform.position, bunker.transform.right);
        if(dotProd > 0) {
            activeCannon = bunker.transform.GetChild(0).transform; // Left
        }
        else {
            activeCannon = bunker.transform.GetChild(1).transform; // Right
        }

        if(activeCannon == null) {
            return;
        }
        Vector3 offset = player.transform.position - activeCannon.position;
        Quaternion lookAtRotation = Quaternion.LookRotation(Vector3.forward, offset);
        activeCannon.rotation = lookAtRotation;

        // TODO: Improve this with Pooling - Rasmus R.
        if(shootTimer > shootCD) {
            bullet = UnityEngine.Object.Instantiate(bulletPrefab);
            bullet.transform.position = activeCannon.transform.position;
            bullet.transform.rotation = lookAtRotation;
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * 15, ForceMode2D.Impulse);
            shootTimer = 0;
            WaitAndDestroy(bullet);
        }
        shootTimer += Time.deltaTime;

        // This is also really bad but the game is so simple it does not matter
        if(bullet != null && bullet.GetComponent<BoxCollider2D>().IsTouching(player.hurtBox)) {
            player.Hit();
        }
    }

    private async void WaitAndDestroy(GameObject t) {
        await Task.Delay(5000);
        UnityEngine.Object.Destroy(t);
    }
}
