using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CaravanView : MonoBehaviour
{

    public Text foodUptake;
    public Text foodCap;
    public Text turn;
    public Caravan car;
    public SceneHandler sceneHandler;

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    foodCap.text = "Uptake: " + car.foodStorage.ToString();
	    turn.text = "Turn: " + sceneHandler.turn;
        foodUptake.text = "Storage: " + car.foodUptakePerRound.ToString();
	}
}
