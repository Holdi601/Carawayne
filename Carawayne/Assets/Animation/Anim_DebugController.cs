using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_DebugController : MonoBehaviour
{

	public Meeple_Animation[] animScripts;
	public int currentControl = 0;

	// Use this for initialization
	void Start ()
	{
		animScripts = GameObject.FindObjectsOfType<Meeple_Animation> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.I)) {
			ActivateAllControls ();
		}
		if (Input.GetKeyDown (KeyCode.O)) {
			DeactivateAllControls ();
		}
		if (Input.GetKeyDown (KeyCode.P)) {
			ActivateNextController ();
		}
	}

	void ActivateAllControls ()
	{
		foreach (Meeple_Animation me in animScripts) {
			me.debugControls = true;
		}
	}

	void DeactivateAllControls ()
	{
		foreach (Meeple_Animation me in animScripts) {
			me.debugControls = false;
		}
	}

	void ActivateNextController ()
	{
		DeactivateAllControls ();
		animScripts [currentControl].debugControls = true;
		currentControl++;
		if (currentControl > animScripts.Length - 1)
			currentControl = 0;
	}
}
