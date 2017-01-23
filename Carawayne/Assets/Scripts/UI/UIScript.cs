using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public enum uiStateEnum { Base, CharacterSingle, CharacterGroup, Event};

    //Thanks, Stackoverflow: http://stackoverflow.com/questions/29289983/how-to-add-event-listener-to-a-enum-type-to-check-value-changed-in-unity3d-c-sh
    private uiStateEnum _uiState;
    public uiStateEnum uiState
    {
        get
        {
            return _uiState;
        }
        set
        {
            _uiState = value;
            OnEnumChange();
            Debug.Log("UI-State: " + _uiState);
        }
    }

    [SerializeField]
    private GameObject _uiCharBar;

    [SerializeField]
    private GameObject _uiBackground;

    [SerializeField]
    private GameObject _uiCharSingle;

    [SerializeField]
    private Image _uiCharSinglePortrait;
    [SerializeField]
    private Text _uiCharSingleName;
    [SerializeField]
    private Text _uiCharSinglePower;
    //[SerializeField]
    //private GameObject _uiCharSingleRes; //TODO: Mirror Reinhold Ressource Script thingy
    [SerializeField]
    private Text _uiCharSingleAbility1;
    [SerializeField]
    private Text _uiCharSingleAbility2;



    [SerializeField]
    private GameObject _uiGroup;

    [SerializeField]
    private GameObject _uiEvent;

    [SerializeField]
    private GameObject _uiHiddenClicker;

    public List<GameObject> characters = new List<GameObject>();

    public Image _imagePrince;
    public Image _imageMercenary;
    public Image _imageScout;
    public Image _imageHunter;
    public Image _imageHealer;
    public Image _imageCamel;

    //SceneHandler meeples.
    //public List<gameObject> characters;

    //Marks the current/last selected char
    //TODO: Might need some failsafe if less than X chars in group
    public int currentMarkedChar;

    // Use this for initialization
    void Start () {
        for (int i = 0; i < SceneHandler.meeples.Count; i++)
        {
            
        }
	}

    //On enum change, change UI
    void OnEnumChange()
    {
        switch (_uiState)
        {
            case uiStateEnum.Base:
                _uiCharBar.active = true;
                _uiBackground.active = false;
                _uiCharSingle.active = false;
                _uiGroup.active = false;
                _uiEvent.active = false;
                _uiHiddenClicker.active = false;
                break;
            case uiStateEnum.CharacterSingle:
                _uiCharBar.active = true;
                _uiBackground.active = true;
                _uiCharSingle.active = true;
                _uiGroup.active = false;
                _uiEvent.active = false;
                _uiHiddenClicker.active = true;
                break;
            case uiStateEnum.CharacterGroup:
                _uiCharBar.active = true;
                _uiBackground.active = true;
                _uiCharSingle.active = false;
                _uiGroup.active = true;
                _uiEvent.active = false;
                _uiHiddenClicker.active = true;
                break;
            case uiStateEnum.Event:
                _uiCharBar.active = false;
                _uiBackground.active = false;
                _uiCharSingle.active = false;
                _uiGroup.active = false;
                _uiEvent.active = true;
                _uiHiddenClicker.active = false;
                break;
            default:
                _uiCharBar.active = true;
                _uiBackground.active = false;
                _uiCharSingle.active = false;
                _uiGroup.active = false;
                _uiEvent.active = false;
                _uiHiddenClicker.active = false;
                break;
        }
    }

    public void UICharacter(int charId)
    {
        uiState = uiStateEnum.CharacterSingle;
        currentMarkedChar = charId;
        //TODO: Something something
        //Get SceneHandler Meeple Stats here and:
        /*
        _uiCharSinglePortrait = ;
        _uiCharSingleName = SceneHandler.meeples(charId).name;
        _uiCharSinglePower = SceneHandler.meeples(charId).power;
        _uiCharSingleAbility1 = SceneHandler.meeples(charId).abilityName1; //If existing
        _uiCharSingleAbility2 = SceneHandler.meeples(charId).abilityName2; //If existing
         */
    }

    public void UIGroup()
    {
        uiState = uiStateEnum.CharacterGroup;
    }

    public void UIEvent()
    {
        uiState = uiStateEnum.Event;
    }

    public void UiCloseAll()
    {
        uiState = uiStateEnum.Base;
    }

    // Update is called once per frame
    void Update () {
    }
}
