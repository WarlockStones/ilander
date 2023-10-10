using Cinemachine;
using Unity;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.UI;


/* This is not very extendable. Break the UIManager into smaller parts. */
/* A better solution than to instantiate the canvas parts would be to have a
   MonoBehaviour UICanvasReferences script on the prefab which just hooks up
   to all of the different parts, buttons, etc. Then reference that from here. */
public class UIManager : ITick {
    // private GameObject mainMenuPrefab;
    private bool fadeOutMenu = false;
    private CanvasScaler canvasScaler;
    private LevelManager levelManager;
    private GameObject mainMenu;

    public UIManager(LevelManager levelManager) {
        this.levelManager = levelManager;
    }

    public void InstantiateMainMenu() {
        var uiEventSystemPrefab = Resources.Load<GameObject>("UIEventSystem");
        var uiEventSystem = Object.Instantiate(uiEventSystemPrefab);

        var mainMenuPrefab = Resources.Load<GameObject>("MainMenuCanvas");
        mainMenu = Object.Instantiate(mainMenuPrefab);

        var startButtonPrefab = Resources.Load<GameObject>("StartButton");
        var startButtonObj = Object.Instantiate(startButtonPrefab, mainMenu.transform, false);
        startButtonObj.GetComponent<Button>().onClick.AddListener(OnStartClick);

        canvasScaler = mainMenu.GetComponent<CanvasScaler>();
    }
    private void OnStartClick() {
        fadeOutMenu = true;
    }

    private float cosVal = 0;
    private const float freq = 1.5f;
    public void Tick() {
        if(fadeOutMenu) {
            // Cosine for simple smoother fade-out animation
            canvasScaler.scaleFactor = Mathf.Cos(cosVal * freq);
            cosVal += (Time.deltaTime);
            if(cosVal > 1) {
                mainMenu.gameObject.SetActive(false);
                fadeOutMenu = false;
                // Initialize Play State! Do not just LoadNextLevel. We must sort out the GameState!
                // We need our Executor to swap over to "PlayState" Execution
                levelManager.LoadNextLevel();
            }
        }
    }
}
