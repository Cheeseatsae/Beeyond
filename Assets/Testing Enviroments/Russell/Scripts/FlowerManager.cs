using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Harry
{
    public class FlowerManager : MonoBehaviour
    {
        public List<GameObject> FlowerList = new List<GameObject>();
        // Start is called before the first frame update
        void Start()
        {
            HiveInteractable.ActivateNextFlowers += GetFlowerList;
        }
    
        // Update is called once per frame
        void Update()
        {
            
        }

        public void GetFlowerList(int count)
        {
            FlowerList.Clear();
            foreach (GameObject flower in GameObject.FindGameObjectsWithTag("Flower"))
            {
                
                if (flower.GetComponent<FlowerInteraction>().flowerLevel == count && flower.GetComponent<FlowerInteraction>().playerOnly == false && flower.GetComponent<FlowerInteraction>().state != Interactable.State.Occupied)
                {
                    FlowerList.Add(flower);
                }
            }
        }
    }
}
