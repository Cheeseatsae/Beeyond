using System.Collections;
using System.Collections.Generic;
using Harry;
using UnityEngine;

public class BoundingWall : MonoBehaviour
{

    public int requirement = 1;
    public bool destructable = false;
    public bool pushEnabled = true;
    public float pushMult = 4;

    private void Start()
    {
        if (destructable)
            HiveInteractable.PollenCollected += CheckPollenRequirement;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!pushEnabled) return;
        
        // if we can, push object away from invis wall while inside trigger
        if (other.gameObject.GetComponent<BeeController>() != null)
        {
            // position - contact point to push bee away from collider
            Vector3 pushDir = other.transform.position - GetComponent<Collider>().ClosestPointOnBounds(other.transform.position);
            pushDir = Vector3.Normalize(pushDir);

            other.gameObject.GetComponent<Rigidbody>().AddForce(pushDir * pushMult);
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
        if (destructable)
            HiveInteractable.PollenCollected -= CheckPollenRequirement;
    }
}
