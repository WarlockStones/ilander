using Cinemachine;
using UnityEngine;
// I would like for something fancier. Maybe Megaman style camera. But for now just use Cinemachine
public class PlayerCamera : IInitialize {
    private Player player;
    public PlayerCamera(Player player) {
        this.player = player;
    }

    public void Initialize() {
        var mainCamPrefab = Resources.Load<GameObject>("MainCamera");
        var mainCam = UnityEngine.Object.Instantiate(mainCamPrefab);

        var virtualCamPrefab = Resources.Load<GameObject>("VirtualCamera");
        var virtualCam = UnityEngine.Object.Instantiate(virtualCamPrefab);

        virtualCam.GetComponent<CinemachineVirtualCamera>().m_Follow = player.rb.transform;
    }
}
