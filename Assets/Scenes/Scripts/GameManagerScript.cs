using System.Collections;
using System.Collections.Generic;
using Harry;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    private CanvasGroup _previousPanel;

    public GameObject GameCam;

    [Range (0f,2f)] public float menuTransitionSpeed;
    public CanvasGroup GamePanel;
    public CanvasGroup StartPanel;
    public CanvasGroup SettingsPanel;
    public CanvasGroup PausePanel;

    public CanvasGroup BlackoutPanel;
    public CanvasGroup HiveMindPanel;
    public CanvasGroup BeeyondPanel;

    public float secondsBeforeFadein;
    public float fadeRate;
    public float secondsToShowLogo;

    private CanvasGroup[] _panels; 
    public static bool _isGameRunning = false;

    public CanvasGroup[] tutorialPanels;
    private bool _tutorialsFaded = false;

    // Start is called before the first frame update
    void Start()
    {
        _previousPanel = StartPanel;
        _panels = GamePanel.GetComponentsInChildren<CanvasGroup>(); // get a list of all active panels
        Debug.Log(_panels.Length);

        StartCoroutine(StartUp()); // cleans up all active panels and resets to a begining state
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey(KeyCode.Escape)|| Input.GetKeyDown(KeyCode.JoystickButton7)) && _isGameRunning)
        {
            PauseGame();
        }

        if (AudioManagerScript.gameProgression > 0 && !_tutorialsFaded) { _tutorialsFaded = true; CloseTutorials(secondsBeforeFadein, fadeRate); }
    }

    public void PauseGame()
    {
        _isGameRunning=false;
        StartCoroutine(Activate(GamePanel, 0f, menuTransitionSpeed));
        StartCoroutine(Activate(PausePanel, 0f, menuTransitionSpeed));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        _isGameRunning=true;
        RenderSettings.skybox.SetFloat("_Exposure", 0.33f);
        AudioManagerScript.Playsound("musicStop");
        ButterflyWaypoint.waypoints.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResumeGame()
    {
		_isGameRunning = true;
        CloseAllPanels(0, menuTransitionSpeed);
    }

    public void CloseTutorials(float _pTime, float _transTime)
    {
        foreach (CanvasGroup _p in tutorialPanels)
        {
            StartCoroutine(Deactivate(_p, _pTime, _transTime));
        }
    }

    public void CloseAllPanels(float _pTime, float _transTime)
    {
        foreach (CanvasGroup _p in _panels)
        {
            StartCoroutine(Deactivate(_p, _pTime, _transTime));
        }
    }

    public void ToSettingsFromPause()
    {
        _previousPanel = PausePanel;
        StartCoroutine(Activate(SettingsPanel, menuTransitionSpeed, menuTransitionSpeed));
        StartCoroutine(Deactivate(PausePanel, 0f, menuTransitionSpeed));
    }

    public void ToSettingsFromStart()
    {
        _previousPanel = StartPanel;
        StartCoroutine(Activate(SettingsPanel, menuTransitionSpeed, menuTransitionSpeed));
        StartCoroutine(Deactivate(StartPanel, 0f, menuTransitionSpeed));
    }

    public void Back()
    {
        StartCoroutine(Activate(_previousPanel, menuTransitionSpeed, menuTransitionSpeed));
        StartCoroutine(Deactivate(SettingsPanel, 0f, menuTransitionSpeed));
    }

    public void StartGame()
    {
        _isGameRunning = true;
        CloseAllPanels(0f, menuTransitionSpeed);
        Roo.CameraMovementScript.cameraClampMinY = 1.5f;
        AudioManagerScript.Playsound("music");
    }

    IEnumerator  StartUp()
    {
        Roo.CameraMovementScript.cameraClampMinY = 22f; // set camera clamp just in case
        StartCoroutine(Deactivate(BeeyondPanel, 0f, 0f)); // just in case
        StartCoroutine(Deactivate(HiveMindPanel, 0f, 0f)); // just in case
        Roo.LightningScript.lightningActive = false; //just in case (static variable)
        _isGameRunning = false; //just in case (static variable)

        AudioManagerScript.gameProgression = 0f; // resets music back to start
        //AnimationBee_FlyidleToFlying.disableBeeAnimator = false; // make sure bee animations are running.

        BlackoutPanel.alpha = 1f; // turn on to keep continuity from unity splash (make sure bg colour matches)

        yield return new WaitForSeconds(fadeRate);

        //flash studio logo for x seconds
        StartCoroutine(Activate(HiveMindPanel, 0f, fadeRate));
        yield return new WaitForSeconds(secondsToShowLogo);
        StartCoroutine(Deactivate(HiveMindPanel, 0f, fadeRate));
        yield return new WaitForSeconds(fadeRate*1.1f);
        //flash game logo for x seconds
        StartCoroutine(Activate(BeeyondPanel, 0f, fadeRate));
        yield return new WaitForSeconds(secondsToShowLogo);
        StartCoroutine(Deactivate(BeeyondPanel, 0f, fadeRate));
        yield return new WaitForSeconds(fadeRate*1.1f);
        StartCoroutine(Deactivate(BlackoutPanel, secondsBeforeFadein/2, fadeRate)); // fade to game menu

        CloseAllPanels(0f, 0f); 
        yield return new WaitForSeconds(secondsBeforeFadein/2f);

        //turn all relevant panels on at start of game
        StartCoroutine(Activate(GamePanel, 0, 0));
        StartCoroutine(Activate(StartPanel, 0, 0));

        yield return null;
    }

    IEnumerator Deactivate(CanvasGroup _panel, float _pauseTime, float _transitionTime)
    {
        yield return new WaitForSeconds(_pauseTime);

        while (_panel.alpha > 0)
        {
            _panel.alpha -= Time.deltaTime / _transitionTime;
            yield return null;
        }
        _panel.interactable = false;
        _panel.blocksRaycasts = false;

        yield return null;
    }

    IEnumerator Activate(CanvasGroup _panel, float _pauseTime, float _transitionTime)
    {
        yield return new WaitForSeconds(_pauseTime);

        while (_panel.alpha < 1)
        {
            _panel.alpha += Time.deltaTime / _transitionTime;
            yield return null;
        }
        _panel.interactable = true;
        _panel.blocksRaycasts = true;

        yield return null;
    }
}
