using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Harry
{
    public class ChangetoFollow : MonoBehaviour
    {
        
        public delegate void ChangeToFindFlowerState();

        public static ChangeToFindFlowerState GoFindFlowers;
        // Start is called before the first frame update
        private void OnTriggerEnter(Collider other)
        {
            GoFindFlowers?.Invoke();
            this.gameObject.SetActive(false);
        }
    }


}

