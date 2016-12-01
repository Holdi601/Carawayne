using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CompanionView : MonoBehaviour
{

    public Text charName;
    public Text charClass;
    public SpriteRenderer portrait;
    public StatusBar statusBar;
    public Button remove;
    public Button add;
    public Button lose;
    public Companion comp;

	// Use this for initialization
	void Start () {
        remove.onClick.AddListener(kill);
        add.onClick.AddListener(comp.gainStrength);
        lose.onClick.AddListener(comp.LoseStrength);
    }
	
	// Update is called once per frame
	void Update () {
        statusBar.progress = (comp.ActualCondition / (float)comp.maxCondition);
        statusBar.statusText.text = comp.ActualCondition + "/" + comp.maxCondition;
        charName.text = comp.CharName;
        charClass.text = comp.charClass.ToString();
    }

    public void kill()
    {
        comp.ActualCondition = 0;
        comp.ConditionState = MeepleConditionState.DEAD;
        GameObject.Find("Caravan").GetComponent<Caravan>().removeCompanion(comp);
    }

    public void changeClass(int _id)
    {
        //Todo: Sprite von Klasse namen ableiten
        Caravan car = GameObject.Find("Caravan").GetComponent<Caravan>();
        

        switch (_id)
        {
            case 0:
                changeCharTo("Prince");
                portrait.sprite = loadSprite("Prince");
                break;
            case 1:
                changeCharTo("Worker");
                portrait.sprite = loadSprite("Worker");
                break;
            case 2:
                changeCharTo("Hunter");
                portrait.sprite = loadSprite("Hunter");
                break;
            case 3:
                changeCharTo("Soldier");
                portrait.sprite = loadSprite("Soldier");
                break;
            case 4:
                changeCharTo("Healer");
                portrait.sprite = loadSprite("Healer");
                break;
            case 5:
                changeCharTo("Scout");
                portrait.sprite = loadSprite("Scout");
                break;
            case 6:
                changeCharTo("Trader");
                portrait.sprite = loadSprite("Trader");
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                break;
            case 10:
                break;
        }
        comp.gameObject.name = comp.charClass + "_" + comp.charName;
    }

    private Sprite loadSprite(string _name)
    {
        Sprite sprite = Resources.Load("Sprites/" + _name, typeof(Sprite)) as Sprite;
        return sprite;
    }

    private Companion loadCompanion(string _name)
    {
        GameObject comp = Resources.Load("Prefabs/Companions/" + _name, typeof(GameObject)) as GameObject;
        return comp.GetComponent<Companion>();
    }

    public void changeNameTo(string _name)
    {
        comp.CharName = _name;
        comp.gameObject.name = comp.charClass + "_" + comp.charName;
    }

    private void changeCharTo(string _name)
    {
        Companion temp_comp = loadCompanion(_name);
        comp.charClass = temp_comp.charClass;
        comp.MaxCondition = temp_comp.MaxCondition;
        comp.ActualCondition = temp_comp.MaxCondition;
        comp.OriginalFoodDemand = temp_comp.OriginalFoodDemand;
        comp.ActualFoodDemand = temp_comp.OriginalFoodDemand;
    }
}
