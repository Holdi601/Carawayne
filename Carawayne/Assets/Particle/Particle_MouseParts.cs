using System.Collections;
using UnityEngine;

//Makes the attached object following the mouse

public class Particle_MouseParts : MonoBehaviour
{

	[SerializeField]private float followSpeed = 9f;
	[SerializeField]private float distCam = 5f;

	void Update ()
	{
		Vector3 mousePos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, distCam);
		Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint (mousePos);

		Vector3 newPos = Vector3.Lerp (transform.position, mouseWorldPos, 1.0f - Mathf.Exp (-followSpeed * Time.deltaTime));
		transform.position = newPos;
	}
}
