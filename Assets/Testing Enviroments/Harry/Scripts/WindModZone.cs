using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace Harry
{
    public class WindModZone : MonoBehaviour
    {

        public float newMultX;
        public float newMultY;
        public float newWindClamp;
        
        private struct beeMod
        {
            public GameObject bee;
            public float oldMultX;
            public float oldMultY;
            public float windClamp;
        }
        
        private List<beeMod> bees = new List<beeMod>();

        [Range(0,1)] public float transparency;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<BeeController>() != null || other.gameObject.layer == 15)
            {
                BeeController thisBee = other.GetComponent<BeeController>();
            
                beeMod b = new beeMod();
                b.bee = thisBee.gameObject;
                b.oldMultX = thisBee.windSpeedMult;
                b.oldMultY = thisBee.windYAxisDivider;
                b.windClamp = thisBee.windSpeedClamp;
            
                bees.Add(b);
            
                thisBee.windSpeedMult = newMultX;
                thisBee.windYAxisDivider = newMultY;
                thisBee.windSpeedClamp = newWindClamp;
            }

        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<BeeController>() != null || other.gameObject.layer == 15)
            {
                BeeController thisBee = other.GetComponent<BeeController>();

                for (int i = 0; i < bees.Count; i++)
                {
                    if (bees[i].bee == thisBee.gameObject)
                    {
                        thisBee.windSpeedMult = bees[i].oldMultX;
                        thisBee.windYAxisDivider = bees[i].oldMultY;
                        thisBee.windSpeedClamp = bees[i].windClamp;

                        bees.RemoveAt(i);

                    }
                }
            }

        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, GetComponent<Collider>().bounds.size);
            Gizmos.color = new Color(1, 0, 0, transparency);
            Gizmos.DrawCube(transform.position, GetComponent<Collider>().bounds.size);
            
        } 
    } 

}

