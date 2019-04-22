﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public static string previousPanel;

    public GameObject GameCam;

    [Range (0f,2f)] public float menuTransitionSpeed;
    public CanvasGroup GamePanel;
    public CanvasGroup StartPanel;
    public CanvasGroup SettingsPanel;
    public CanvasGroup PausePanel;

    public CanvasGroup BlackoutPanel;
    public float secondsBeforeFadein;
    public float fadeRate;

    private CanvasGroup[] _panels; 
    private bool _isGameRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        _panels = GamePanel.GetComponentsInChildren<CanvasGroup>(); // get a list of all active panels
        Debug.Log(_panels.Length);

        BlackoutPanel.alpha = 1f; // turn on to keep continuity from unity splash (make sure bg colour matches)
        StartCoroutine(Deactivate(BlackoutPanel, secondsBeforeFadein, fadeRate)); // fade to game menu

        StartCoroutine(StartUp()); // cleans up all active panels and resets to a begining state
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && _isGameRunning)
        {
            StartCoroutine(Activate(GamePanel, 0f, menuTransitionSpeed));
            StartCoroutine(Activate(PausePanel, 0f, menuTransitionSpeed));
        }

        if (Input.GetKey(KeyCode.Space) && !_isGameRunning)
        {
            StartGame();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        AudioManagerScript.Playsound("musicStop");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CloseAllPanels(float _pTime, float _transTime)
    {
        foreach (CanvasGroup _p in _panels)
        {
            StartCoroutine(Deactivate(_p, _pTime, _transTime));
        }
    }

    public void StartGame()
    {
        _isGameRunning = true;
        StartCoroutine(Deactivate(GamePanel, 0f, menuTransitionSpeed));
        StartCoroutine(Deactivate(StartPanel, 0f, menuTransitionSpeed));
        StartCoroutine(Deactivate(BlackoutPanel, 0f, menuTransitionSpeed));
        Roo.CameraMovementScript.cameraClampMinY = 1.5f;
        AudioManagerScript.Playsound("music");
    }

    IEnumerator StartUp()
    {
        CloseAllPanels(0f, 0f);
        yield return new WaitForSeconds(0.1f);

        Roo.CameraMovementScript.cameraClampMinY = 22f; // set camera clamp just in case
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
