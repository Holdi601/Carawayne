using UnityEngine;

//Providing particles for picked unit and controls them

//[ExecuteInEditMode]
public class Particles_ActiveUnit : MonoBehaviour
{

	[SerializeField] private float rotSpeed = 5;
	[SerializeField] private float pulsingSpeed = 5;
	[SerializeField] private float scaleAmount = 0.2f;
	[SerializeField] private ParticleSystem[] subParts;
	public int pulsingPartNo = 2;
	private Transform pulsingPart;


	// Use this for initialization
	void Start ()
	{
		subParts = GetComponentsInChildren<ParticleSystem> ();
		pulsingPart = subParts [pulsingPartNo].gameObject.transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.Rotate (Vector3.up, rotSpeed * Time.deltaTime);
		float scale = 0;
		scale = Mathf.PingPong (Time.time / pulsingSpeed, scaleAmount) + (1 - scaleAmount);
		pulsingPart.localScale = new Vector3 (scale, scale, scale);


		DebugControls ();
	}

	void DebugControls ()
	{
		if (Input.GetMouseButtonDown (0) == true) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit)) {
				SetParticle (hit.point);
			}
		}
		if (Input.GetMouseButtonDown (1)) {
			StopParticle ();
		}
	}

	public void SetParticle (Vector3 pos)
	{
		StopParticle ();
		transform.position = pos;
		foreach (ParticleSystem sP in subParts) {
			sP.gameObject.SetActive (true);
			sP.Play ();
		}
	}

	public void StopParticle ()
	{
		foreach (ParticleSystem sP in subParts) {
			sP.gameObject.SetActive (false);
			sP.Stop ();
			sP.Clear ();
		}
	}
}
