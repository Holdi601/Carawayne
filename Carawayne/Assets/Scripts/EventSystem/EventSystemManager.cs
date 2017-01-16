using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class EventSystemManager : MonoBehaviour
{
    [SerializeField]
    private Text _eventTitle;

    [SerializeField]
    private Text _eventText;

    [SerializeField]
    private Text _eventConsequenceText;

    [SerializeField]
    private Image _eventImage;

    // serialized just for testing
    [SerializeField]
    private int _eventNumber;

    [SerializeField]
    private string _eventTerrain;

    private int _eventNumberLastFrame;

    private XmlDocument _xmlEventsForest;
    private XmlDocument _xmlEventsMountain;
    private XmlDocument _xmlEventsDesert;
    private XmlDocument _xmlEventsOasis;

    private string _valueTitle;
    private string _valueConsequenceText;
    private string _valueEventText;
    private Sprite _valueEventImage;
    private string _codeEvent;
    private string _codeEventConsequence;

    void Start()
    {
        _valueTitle = "";
        _valueConsequenceText = "";
        _valueEventText = "";
        _valueEventImage = null;
        _codeEvent = "";
        _codeEventConsequence = "";
        _eventTerrain = "";
        _eventNumber = 0;
        _eventNumberLastFrame = 0;

        _xmlEventsForest = new XmlDocument();
        _xmlEventsMountain = new XmlDocument();
        _xmlEventsDesert = new XmlDocument();
        _xmlEventsOasis = new XmlDocument();


        // Not sure if this works after building standalone !
        _xmlEventsForest.Load("Assets/Scripts/EventSystem/TaskForest.xml");
        _xmlEventsMountain.Load("Assets/Scripts/EventSystem/TaskMountain.xml");
        _xmlEventsDesert.Load("Assets/Scripts/EventSystem/TaskDesert.xml");
        _xmlEventsOasis.Load("Assets/Scripts/EventSystem/TaskOasis.xml");
    }

    // for testing only
    void Update()
    {
        if (_eventNumber != _eventNumberLastFrame)
        {
            ActivateEvent(_eventTerrain, _eventNumber);   
        }
        _eventNumberLastFrame = _eventNumber;

    }

    public void ActivateEvent(string eventTerrain, int eventNumber)
    {
        ReadXML(eventTerrain, eventNumber);
        SetEventUI();
    }

    private void ReadXML(string terrain, int eventNr)
    {
        XmlDocument xml = new XmlDocument();
        switch(terrain)
        {
            case "forest":
                xml = _xmlEventsForest;
                break;

            case "mountain":
                xml = _xmlEventsMountain;
                break;

            case "desert":
                xml = _xmlEventsDesert;
                break;

            case "oasis":
                xml = _xmlEventsOasis;
            break;
        }

        if (eventNr != 0)
        {
            XmlNode requiredTask = xml.SelectSingleNode("rootnode/task[@nr=" + eventNr.ToString() + "]");

            _valueTitle = requiredTask.Attributes["title"].Value;
            _valueEventText = requiredTask.SelectSingleNode("eventText").InnerText;
            _valueConsequenceText = requiredTask.SelectSingleNode("consequenceText").InnerText;

            // TODO: find image file from xml path to display in the "_eventImage" object
            _valueEventImage = null;

            _codeEvent = requiredTask.SelectSingleNode("code").InnerText;
            _codeEventConsequence = requiredTask.SelectSingleNode("consequenceCode").InnerText;
        }
    }

    private void SetEventUI()
    {
        if (!gameObject.GetComponent<Canvas>().enabled)
        {
            gameObject.GetComponent<Canvas>().enabled = true;
        }
        
        _eventTitle.text = _valueTitle;
        _eventText.text = _valueEventText;
        _eventConsequenceText.text = _valueConsequenceText;

        // TODO: add Attribute "eventImage" (path to image file) to XML files for usage here 
        _eventImage.sprite = _valueEventImage;
    }

    public void OnCloseButtonClick()
    {
        CallEventFunctions(_codeEvent, _codeEventConsequence);

        _valueTitle = "";
        _valueConsequenceText = "";
        _valueEventText = "";
        _valueEventImage = null;
        _codeEvent = "";
        _codeEventConsequence = "";
        _eventTerrain = "";
        _eventNumber = 0;
        _eventNumberLastFrame = 0;
    }

    private void CallEventFunctions(string codeEvent, string codeEventConsequence)
    {
        EventFunctionCollection.CallEventFunctions(codeEvent, codeEventConsequence);

        // Idee:
        
        // codeEvent:
        // 1. Haupt - Eventfunktion
        // 2. (Optionaler) zu übergebender Parameter (z.B. Menge hinzuzufügender Proviant)
        // 3. markOutcome [true/false] (Ergebnis zwischenspeichern ja/nein)

        // codeEventConsequence:
        // 1. hasConsequence [true/false] (hat das Event eine Konsequenz ja/nein)
        // 2. Haupt - Konsequenzfunktion
        // 3. (Optionaler) zu übergebender Parameter (z.B. verzögerung, mit der die Konsequenz eintrifft)
    }
}
