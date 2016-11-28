using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PackAnimalView : MonoBehaviour
{

    public Text animalClass;
    public SpriteRenderer portrait;
    public StatusBar statusBar;
    public Button remove;
    public Button add;
    public Button lose;
    public PackAnimal packAnimal;

	// Use this for initialization
	void Start () {
        remove.onClick.AddListener(butcherAnimal);
        add.onClick.AddListener(load);
        lose.onClick.AddListener(unload);
    }
	
	// Update is called once per frame
	void Update () {
        statusBar.progress = (packAnimal.ActualLoad / (float)packAnimal.maxLoad);
        statusBar.statusText.text = packAnimal.ActualLoad + "/" + packAnimal.maxLoad;
        animalClass.text = packAnimal.name;
    }

    private void butcherAnimal()
    {
        GameObject caravanObj = GameObject.Find("Caravan");
        Caravan caravan = caravanObj.GetComponent<Caravan>();

        packAnimal.gameObject.transform.parent = GameObject.Find("Graveyard").transform;
        caravan.removePackAnimal(packAnimal);
        caravan.gainProviant(packAnimal.butcherAnimal());
    }

    private void load()
    {
        packAnimal.load(1);
    }

    private void unload()
    {
        packAnimal.unload(1);
    }
}
