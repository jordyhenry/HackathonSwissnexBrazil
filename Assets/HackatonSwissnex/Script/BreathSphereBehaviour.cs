using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathSphereBehaviour : MonoBehaviour
{
    public float growthSpeed = 5f;
    private bool isGrowing = false;
    public List<HandBehaviour> handsInside;

    Coroutine closeHandsCoroutine;

    private void Update()
    {
        GrowthSphere();
    }

    void GrowthSphere()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isGrowing = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isGrowing = false;
            transform.localScale = Vector3.one;

            if (closeHandsCoroutine != null)
                StopCoroutine(IECloseHands());
            StartCoroutine(IECloseHands());
            //make transparent
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (isGrowing)
                transform.localScale += Vector3.one * growthSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        HandBehaviour hand = other.GetComponent<HandBehaviour>();
        if(hand != null)
        {
            handsInside.Add(hand);
            hand.isDecreasing = false;
            hand.SetRotation(transform.parent.position);
        }
    }

    IEnumerator IECloseHands()
    {
        float lastDistance = 0f;
        WaitForSeconds wait = new WaitForSeconds(.5f);
        for (int i = 0; i < handsInside.Count; i++)
        {
            handsInside[i].isDecreasing = true;
            handsInside[i].StopHolding();

            float currentDistance = Vector3.Distance(transform.position, handsInside[i].transform.position);
            float waitTime = (currentDistance - lastDistance) / 2f;
            wait = new WaitForSeconds(waitTime);
            lastDistance = currentDistance;
            yield return wait;
        }
        handsInside.Clear();
    }
}
