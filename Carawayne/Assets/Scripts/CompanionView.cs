using UnityEngine;
using System.Collections;
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
        remove.onClick.AddListener(comp.kill);
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
}
