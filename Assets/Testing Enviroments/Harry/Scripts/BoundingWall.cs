using System.Collections;
using System.Collections.Generic;
using Harry;
using UnityEngine;

public class BoundingWall : MonoBehaviour
{

    public int requirement = 1;
    public bool pushEnabled = true;
    public float pushMult = 1;

    private void Start()
    {
        HiveInteractable.PollenCollected += CheckPollenRequirement;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!pushEnabled) return;
        
        // if we can, push object away from invis wall while inside trigger
        if (other.gameObject.GetComponent<Rigidbody>() != null)
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.left * pushMult);
        }
    }

    // if event triggers amount of times required delete wall
    public void CheckPollenRequirement(int i)
    {
        if (i >= requirement)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        HiveInteractable.PollenCollected -= CheckPollenRequirement;
    }
}
