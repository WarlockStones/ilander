using Cinemachine;
using Unity;
using UnityEditor;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.UI;


/* This is not very extendable. Break the UIManager into smaller parts. */
/* A better solution than to instantiate the canvas parts would be to have a
   MonoBehaviour UICanvasReferences script on the prefab which just hooks up
   to all of the different parts, buttons, etc. Then reference that from here. */
public class UIManager : IInitialize, ITick {
    private bool fadeOutMenu = false;
    private CanvasScaler canvasScaler;
    private LevelManager levelManager;
    private GameObject mainMenu;

    private GameObject pauseMenu;

    public UIManager(LevelManager levelManager) {
        this.levelManager = levelManager;
    }

    public void InstantiateMainMenu() {
        var uiEventSystemPrefab = Resources.Load<GameObject>("UIEventSystem");
        var uiEventSystem = Object.Instantiate(uiEventSystemPrefab);

        var mainMenuPrefab = Resources.Load<GameObject>("MainMenuCanvas");
        mainMenu = Object.Instantiate(mainMenuPrefab);

        // I know that this is dumb. I should use MonoBehaviour when applicable
        var startButtonPrefab = Resources.Load<GameObject>("StartButton");
        var startButtonObj = Object.Instantiate(startButtonPrefab, mainMenu.transform, false);
        startButtonObj.GetComponent<Button>().onClick.AddListener(OnStartClicked);

        canvasScaler = mainMenu.GetComponent<CanvasScaler>();
    }

    public void Initialize() {
        var uiEventSystemPrefab = Resources.Load<GameObject>("UIEventSystem");
        var uiEventSystem = Object.Instantiate(uiEventSystemPrefab);

        var pauseMenuPrefab = Resources.Load<GameObject>("PauseMenuCanvas");
        pauseMenu = Object.Instantiate(pauseMenuPrefab);

        var resumeButtonPrefab = Resources.Load<GameObject>("ResumeButton");
        var resumeButtonObj = Object.Instantiate(resumeButtonPrefab, pauseMenu.transform, false);
        resumeButtonObj.GetComponent<Button>().onClick.AddListener(OnResumeClicked);

        var quitButtonPrefab = Resources.Load<GameObject>("QuitButton");
        var quitButtonObj = Object.Instantiate(quitButtonPrefab, pauseMenu.transform, false);
        quitButtonObj.GetComponent<Button>().onClick.AddListener(OnQuitClicked);

        pauseMenu.SetActive(false);
    }
    private void OnQuitClicked() {
        // This should probably be in game-state if we need to do more than just close application
        Application.Quit();
    }
    private void OnResumeClicked() {
        GameState.TogglePause();
    }

    private void OnStartClicked() {
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
    public void SetPauseMenu(bool b) {
        pauseMenu.SetActive(b);
    }
}
