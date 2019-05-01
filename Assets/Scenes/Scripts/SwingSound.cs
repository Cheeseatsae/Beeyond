using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingSound : MonoBehaviour
{
    private bool _hasPlayed = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "BlockerBottom")
        {
            if (!_hasPlayed)
            {
                _hasPlayed = true;
                AudioManagerScript.Playsound("HeavyObjectFallyingGrass03");
            }
        }
    }
}
