using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public GameObject ObjectToDestroy;
    public GameObject GameCam;

    public GameObject GamePanel;
    public GameObject StartMenu;

    public GameObject BlackoutPanel;
    public float secondsBeforeFadein;
    public float fadeRate;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Fadein(BlackoutPanel));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            StartGame();
        }

    }

    IEnumerator Fadein(GameObject _panel)
    {
        CanvasGroup cg = _panel.GetComponent<CanvasGroup>();
        yield return new WaitForSeconds(secondsBeforeFadein);

        while (cg.alpha > 0)
        {
            cg.alpha -= Time.deltaTime / fadeRate;
            yield return null;
        }
        _panel.SetActive(false);

        yield return null;
    }


    public void StartGame()
    {
        GamePanel.SetActive(false);
        Roo.CameraMovementScript.cameraClampMinY = 1.5f;
        AudioManagerScript.Playsound("music");
    }
}
