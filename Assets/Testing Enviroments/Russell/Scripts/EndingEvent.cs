using System;
using System.Collections;
using System.Collections.Generic;
using Harry;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Harry
{
    public class EndingEvent : MonoBehaviour
    {
        public float pauseBeforeCredits;
        public float panelDuration;
        public float panelFadeTime;
        public float pauseBetweenPanels;

        public CanvasGroup[] credits;
        public CanvasGroup finalPanel;
        public CanvasGroup brownOutPanel;

        public event Action SpawnTheBees; 
        // Start is called before the first frame update
        void Start()
        {
            
        }
    
        // Update is called once per frame
        void Update()
        {
            
        }
    
        public void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerBeeController>())
            {
                SpawnTheBees();
                StartCoroutine(RollCredits());
            }
        }


        IEnumerator RollCredits()
        {
            yield return new WaitForSeconds(pauseBeforeCredits);

            foreach (CanvasGroup _panel in credits)
            {
                while (_panel.alpha < 1)
                {
                    _panel.alpha += Time.deltaTime / panelFadeTime;
                    yield return null;
                }
                yield return new WaitForSeconds(panelDuration);
                while (_panel.alpha > 0)
                {
                    _panel.alpha -= Time.deltaTime / panelFadeTime;
                    yield return null;
                }
                yield return new WaitForSeconds(pauseBetweenPanels);
            }
            yield return new WaitForSeconds(pauseBetweenPanels*2f);

            while (finalPanel.alpha < 1)
            {
                finalPanel.alpha += Time.deltaTime / (panelFadeTime);
                yield return null;
            }
            yield return new WaitForSeconds(panelDuration);
            while (finalPanel.alpha > 0)
            {
                finalPanel.alpha -= Time.deltaTime / (panelFadeTime);
                yield return null;
            }

            yield return new WaitForSeconds(pauseBetweenPanels);
            AudioManagerScript.Playsound("windStop");
            AudioManagerScript.Playsound("atmosExploringStop");

            while (brownOutPanel.alpha < 1)
            {
                brownOutPanel.alpha += Time.deltaTime / (panelFadeTime * 2f);
                yield return null;
            }
            yield return new WaitForSeconds(1f);

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            yield return null;
        }
    }


}
