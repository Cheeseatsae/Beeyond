using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBee_FlyidleToFlying : MonoBehaviour
{
    public Animator animator;
    float InputX;
    public float InputY;
    public static bool disableBeeAnimator = false;

    // Start is called before the first frame update
    void Start()
    {
        //Get the animator
        animator = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessAnimations();
    }

    void ProcessAnimations()
    {
        if (disableBeeAnimator) { animator.SetFloat("InputX", 1f); return; }
        InputY = Input.GetAxis("Vertical");
        InputX = Input.GetAxis("Horizontal");
        animator.SetFloat("InputY", InputY);
        animator.SetFloat("InputX", InputX);
    }
}
