using System.Collections;
using UnityEngine;

//Makes the attached object following the mouse

public class Ragdoll_MouseFollower : MonoBehaviour
{

	[SerializeField]private float followSpeed = 9f;
	[SerializeField]private float distCam = 5f;
	private bool picked;

	void Update ()
	{
		if (picked) {
			Vector3 mousePos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, distCam);
			Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint (mousePos);

			Vector3 newPos = Vector3.Lerp (transform.position, mouseWorldPos, 1.0f - Mathf.Exp (-followSpeed * Time.deltaTime));
			transform.position = newPos;
		}
	}

	void OnMouseDown ()
	{
		picked = !picked;
	}
}
