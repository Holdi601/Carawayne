using UnityEngine;

public class SceneHandler : MonoBehaviour
{

    public int turn = 0;
    public Caravan playerCaravan;

    private Transform companionList;
    private Transform packAnimalList;

    void Start()
    {
        companionList = GameObject.Find("CompanionsList").transform.FindChild("GridGroupLayout");
        packAnimalList = GameObject.Find("PackAnimalList").transform.FindChild("GridGroupLayout");
        createView();
        EventManager.StartListening("ViewChanged", createView);
    }

    void Update()
    {
        
    }

    private void createView()
    {
        
        removeChildrenFrom(companionList);
        removeChildrenFrom(packAnimalList);

        foreach (Companion comp in playerCaravan.companions)
        {
            addCompanionToView(comp);    
        }

        foreach (PackAnimal packAnimal in playerCaravan.packAnimals)
        {
            addPackAnimalToView(packAnimal);
        }
    }

    private void addCompanionToView(Companion comp)
    {
        GameObject compListItem = Instantiate(Resources.Load("Prefabs/CompanionItem_Interactable", typeof(GameObject))) as GameObject;
        compListItem.transform.SetParent(companionList.transform, false);

        CompanionView compView = compListItem.GetComponent<CompanionView>();
        compView.comp = comp;
    }

    private void addPackAnimalToView(PackAnimal _packAnimal)
    {
        GameObject compListItem = Instantiate(Resources.Load("Prefabs/PackAnimalItem", typeof(GameObject))) as GameObject;
        compListItem.transform.SetParent(packAnimalList.transform, false);

        PackAnimalView packAnimalView = compListItem.GetComponent<PackAnimalView>();
        packAnimalView.packAnimal = _packAnimal;
    }

    private void removeChildrenFrom(Transform _rootEl)
    {
        foreach (Transform child in _rootEl.GetComponentsInChildren<Transform>())
        {
            if (_rootEl != child)
                Destroy(child.gameObject);
        }
    }

    public void endTurn()
    {
        turn++;
        playerCaravan.consumeProviant(playerCaravan.foodUptakePerRound);
    }


}
