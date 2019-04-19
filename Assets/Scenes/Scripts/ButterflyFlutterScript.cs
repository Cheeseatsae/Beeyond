using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyFlutterScript : MonoBehaviour
{
    [Range(0.1f, 5f)] public float minAnimationSpeed = 1.75f;
    [Range(0.1f, 5f)] public float maxAnimationSpeed = 2.25f;

    // Start is called before the first frame update
    
    void Awake()
    {
        GetComponentInChildren<Animator>().speed = Random.Range(minAnimationSpeed, maxAnimationSpeed);
    }

}
