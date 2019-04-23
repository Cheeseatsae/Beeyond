using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public float timeUntilDeath;
    public float cloudSpeed;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, timeUntilDeath);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * cloudSpeed);
    }
}
