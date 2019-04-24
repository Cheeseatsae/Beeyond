using System.Collections;
using System.Collections.Generic;
using Harry;
using UnityEngine;

namespace Harry
{
    public class FlowerEndList : MonoBehaviour
    {
    
        public GameObject[] finalflowers;
    
        public int numberOfBees;
        public GameObject beePrefab;
        public Transform spawnpoint;
    
        private void Awake()
        {
            foreach (EndingEvent ending in FindObjectsOfType<EndingEvent>())
            {
                ending.SpawnTheBees += SpawnBees;
            }
        }
    
        // Start is called before the first frame update


        public void SpawnBees()
        {
            for (int i = 0; i < numberOfBees; i++)
            {
                GameObject bee = Instantiate(beePrefab, transform);
                AIBeeController ai = bee.GetComponent<AIBeeController>();
                ai.ChangeState(ai.finalState);
            }
        }
    }

}

