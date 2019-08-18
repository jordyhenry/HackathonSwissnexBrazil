using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSphere : MonoBehaviour
{
    public Material mat;
    float scaleSpeed = 10f;
    float rotationSpeed = 5f;

    void Update()
    {
        float val = transform.localScale.x;
        if (Input.GetKey(KeyCode.Space))
            val += scaleSpeed * Time.deltaTime;
        else
            val -= scaleSpeed * Time.deltaTime;

        float dist = Mathf.Clamp(Input.GetAxis("Jump"), 0f, .07f);
        mat.SetFloat("_Distortion", dist);
        //val = Mathf.Clamp(val, 1f, 30f);
        //transform.localScale = Vector3.one * val;
        //transform.Rotate(0, Time.deltaTime * rotationSpeed, 0);
    }
}
