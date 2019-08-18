using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Survey : MonoBehaviour {
	public Transform Eye;

	private CharacterController Surveycontrl;
	[SerializeField]private float Surveyspeed;
	[SerializeField][Range(0.1f,5f)] private float sensitivity = 1f; 
	private float gravity;
	void Start()
	{
		Surveycontrl = GetComponent<CharacterController> ();
		gravity = -9.8f;
	}
	void FixedUpdate()
	{
		float H = Input.GetAxisRaw ("Vertical")*Surveyspeed;
		float V = Input.GetAxisRaw ("Horizontal") * Surveyspeed;
		float Mousex = Input.GetAxisRaw ("Mouse X")*sensitivity;
		float Mousey = Input.GetAxisRaw ("Mouse Y")*sensitivity;

		Vector3 movements = new Vector3 (V ,gravity*Time.deltaTime, H);
		movements = transform.rotation * movements;
		Vector3 bodyrot = new Vector3 (0f, Mousex, 0f);
		transform.Rotate(bodyrot);
		LookRot (Mousey);
		Surveycontrl.Move (movements);
	}
	void LookRot(float Mousey)
	{
		Eye.Rotate(new Vector3(-Mousey,0f,0f));
	}
}
