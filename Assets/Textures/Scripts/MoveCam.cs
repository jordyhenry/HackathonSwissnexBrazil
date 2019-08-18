using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour 
{
	public float CamSpeed;
	void Update()
	{
		this.transform.Translate(Vector3.forward*CamSpeed*Time.deltaTime); 
	}


}
