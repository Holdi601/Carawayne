using UnityEngine;

public class WatchtowerToGoal : MonoBehaviour
{

	public Transform goal;
	private float damping = 3;
	private int rnd;

	// Use this for initialization
	void Start ()
	{
		
		InvokeRepeating ("RandomGen", 0, 2);
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 lookPos;
		lookPos = SceneHandler.largeMap[SceneHandler.finishTile.x, SceneHandler.finishTile.y].transform.FindChild("actualTile").transform.position - transform.position;

		if (rnd == 0) {
			lookPos.x += 10;
			lookPos.z += 10;
		}
		if (rnd == 1) {
			lookPos.z -= 10;
			lookPos.x -= 10;
		}

		lookPos.y = 0;
		var rotation = Quaternion.LookRotation (lookPos);
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * damping);
	}

	void RandomGen ()
	{
		rnd = Random.Range (0, 2);

	}
}
