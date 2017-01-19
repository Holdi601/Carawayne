using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meeple_Animation : MonoBehaviour
{

	[SerializeField]private GameObject abilityPart;
	[SerializeField]private Animator anim;
	public bool debugControls;

	public enum Animation
	{
		Idle,
		Ability,
		Walk,
		Die,
		Reset}

	;


	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator> ();
	}

	void Update ()
	{
		if (debugControls) {
			DebugControls ();
		}
	}

	void DebugControls ()
	{
		if (Input.GetKeyDown (KeyCode.H)) {
			ChangeAnimation (Animation.Walk);
		}
		if (Input.GetKeyDown (KeyCode.J)) {
			ChangeAnimation (Animation.Idle);
		}
		if (Input.GetKeyDown (KeyCode.K)) {
			ChangeAnimation (Animation.Ability);
		}

		if (Input.GetKeyDown (KeyCode.L)) {
			ChangeAnimation (Animation.Die);
		}
		if (Input.GetKeyDown (KeyCode.G)) {
			ChangeAnimation (Animation.Reset);
		}
	}


	public void ChangeAnimation (Animation aniType)
	{
		switch (aniType) {
		case Animation.Ability:
			anim.SetTrigger ("ability");
			break;
		case Animation.Idle:
			anim.SetBool ("walking", false);
			break;
		case Animation.Walk:
			anim.SetBool ("walking", true);
			break;
		case Animation.Die:
			anim.SetTrigger ("death");
			break;
		case Animation.Reset:
			anim.SetTrigger ("resurrected");
			break;
		default:
			anim.SetBool ("walking", false);
			break;
		}
	}



	public void SpawnAbilityParticle ()
	{
		Instantiate (abilityPart, transform.position, Quaternion.identity);
	}
}
