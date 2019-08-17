using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandBehaviour : MonoBehaviour
{
    [Range(0f, 1f)]
    public float animationThreshold = 0f;

    public float increaseSpeed = 1f;
    public AnimationCurve increaseCurve;
    public float descreaseSpeed = 1f;

    private Quaternion startRotation;
    private Quaternion targetRotation;
    public AnimationCurve rotationCurve;


    public bool isDecreasing = true;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();    
    }

    private void Update()
    {
        if(!animator.GetBool("Holding"))
        {
            if (isDecreasing)
                animationThreshold -= descreaseSpeed * Time.deltaTime;
            else
            {
                animationThreshold += increaseSpeed * Time.deltaTime;
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationCurve.Evaluate(Time.deltaTime));
            }

            animationThreshold = Mathf.Clamp01(animationThreshold);
            animator.SetFloat("OpenHand", increaseCurve.Evaluate(animationThreshold));

            if(animationThreshold >= .9)
            {
                animator.SetBool("Holding", true);
            }
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationCurve.Evaluate(Time.deltaTime));
        }
    }

    public void SetRotation(Vector3 cameraPosition)
    {
        startRotation = transform.rotation;
        transform.LookAt(cameraPosition);
        targetRotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y - 90f, 0));
        transform.rotation = startRotation;
    }

    public void StopHolding()
    {
        animationThreshold = .89f;
        animator.SetBool("Holding", false);

    }
}
