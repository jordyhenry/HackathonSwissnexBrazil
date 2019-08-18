using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class BreathSphereBehaviour : MonoBehaviour
{
    public float growthSpeed = 5f;
    private bool isGrowing = false;
    public List<HandBehaviour> handsInside;
    public List<HandAudio> handsAudios;
    public Material mat;

    Coroutine closeHandsCoroutine;
    public ParticleSystem particles;

    [Range(10f,256f)]
    public int particlesPerEmission = 10;
    public int maxCounter;
    private int[] classCounter;

    bool triggerPressed = false;
    private void Update()
    {
        float triggerAxis = Input.GetAxis("RightTriggerAxis");
        if(!triggerPressed)
        {
            if (triggerAxis >= .9)
                triggerPressed = true;
        }
        else
        {
            if (triggerAxis <= .1)
                triggerPressed = false;
        }

        Debug.Log("Trigger " + triggerPressed);
        if(Input.GetKeyDown(KeyCode.Joystick1Button9))
            Debug.Log("Trackpad Down");

        if(Input.GetKeyUp(KeyCode.Joystick1Button9))
            Debug.Log("Trackpad Up");
    }
    
    public void OnConnectionEvent(bool sucesss)
    {
        Debug.Log("Connected : " + sucesss);
    }

    public void OnMessageArrived(string msg)
    {
        float value = float.Parse(msg);

        if (value >= 5f)
        {
            isGrowing = true;
            particles.transform.position = transform.position;
            GrowthSphere();
            particles.Emit(particlesPerEmission);
        }
        else
        {
            if (!isGrowing) return;
            isGrowing = false;
            transform.localScale = Vector3.zero;

            if (closeHandsCoroutine != null)
                StopCoroutine(IECloseHands());

            StartCoroutine(IECloseHands());
            for (int i = 0; i < handsAudios.Count; i++)
            {
                handsAudios[i].Stop();
            }
            for (int i = 0; i < classCounter.Length; i++)
            {
                classCounter[i] = 0;
            }
        }
    }

    private void Start()
    {
        classCounter = new int[4];
    }

    void GrowthSphere()
    {
        if (isGrowing)
        {
            transform.localScale += Vector3.one * growthSpeed * Time.deltaTime;
            float dist = Mathf.Clamp(growthSpeed, 0f, .07f);
            mat.SetFloat("_Distortion", dist);
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

            HandAudio handAd = hand.GetComponent<HandAudio>();
            if (classCounter[handAd.GetClass()] < maxCounter)
            {
                classCounter[handAd.GetClass()]++;
                handsAudios.Add(handAd);
                handAd.Play();
            }
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
