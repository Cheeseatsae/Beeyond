using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiteTailScript : MonoBehaviour
{
    private Rigidbody rb;

    [Range(0f,2f)] public float windStrength = 1f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(-(Roo.WindScript.windSpeed * windStrength), 0, 0);
    }
}
