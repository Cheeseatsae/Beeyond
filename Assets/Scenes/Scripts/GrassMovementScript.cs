using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassMovementScript : MonoBehaviour
{
    private Cloth grassCloth;
    [Range(0f,2f)] public float windMultiplierLowerRange = 0.5f;
    [Range(0f, 2f)] public float windMultiplierUpperRange = 1f;

    private float _windMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        grassCloth = GetComponent<Cloth>();

        //set random attributes
        grassCloth.bendingStiffness = Random.Range(.5f, 1f);
        grassCloth.friction = Random.Range(.2f, .3f);
        grassCloth.damping = Random.Range(.05f, .15f);

        _windMultiplier = Random.Range(windMultiplierLowerRange, windMultiplierUpperRange);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        grassCloth.externalAcceleration = new Vector3(-(Roo.WindScript.windSpeed * _windMultiplier), 1f, 0f);
    }
}
