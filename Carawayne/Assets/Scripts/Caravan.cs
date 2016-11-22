using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Caravan : MonoBehaviour
{
    public int foodStorage;
    public int foodUptakePerRound;
    public List<Companion> companions;
    public List<PackAnimal> packAnimals;

    //MONOBEHAVIOUR FUNCTIONS
    //-----------------------
    void Start()
    {
        //Start Listening to events
        EventManager.StartListening("FoodUptakeChanged", updateActualFoodUptake);
        EventManager.StartListening("FoodStockChanged", updateActualFoodStock);

        //Add CompanionGameObjects to CaravanCompanions
        companions = gameObject.transform.FindChild("Companions").GetComponentsInChildren<Companion>().ToList();
        packAnimals = gameObject.transform.FindChild("PackAnimals").GetComponentsInChildren<PackAnimal>().ToList();

        foodStorage = getFoodStock();
        foodUptakePerRound = getFoodUptake();
    }

    void Update()
    {
      
        //Following update-routine is for live inspector editing!
        //!!

        //Identify discepancies between caravaonCompanions and caravanObjectChilds
        if (companions.Count != gameObject.transform.FindChild("Companions").childCount)
        {
            companions = gameObject.transform.FindChild("Companions").GetComponentsInChildren<Companion>().ToList();
            EventManager.TriggerEvent("FoodUptakeChanged");
        }

        //Identify discepancies between caravaonpackANimals and packAnimalObjectChilds
        if (packAnimals.Count != gameObject.transform.FindChild("PackAnimals").childCount)
        {
            packAnimals = gameObject.transform.FindChild("PackAnimals").GetComponentsInChildren<PackAnimal>().ToList();
            EventManager.TriggerEvent("FoodStockChanged");
        }
    }

    //CARAVAN FUNCTIONS
    //----------------

    //Consume a given amount of proviant
    public void consumeProviant(int _amount)
    {
        int i = 0;
        foreach (Companion comp in companions)
        {
            int rest = 0;

            //Rest penalty through Starving or half rations
            rest = (comp.OriginalFoodDemand - comp.ActualFoodDemand);
            comp.ActualCondition -= rest;

            //Rest penalty through not having enough food on packAnimal
            rest = packAnimals[i].unload(comp.ActualFoodDemand);
            while (rest < 0 && i < packAnimals.Count - 1)
            {
                i++;
                rest = packAnimals[i].unload(comp.ActualFoodDemand);
            }
            
            if (rest < 0)
            {
                if (comp.conditionState == MeepleConditionState.UNCONSCIOUS)
                {
                    comp.ConditionState = MeepleConditionState.DEAD;
                }
                comp.ActualCondition -= Mathf.Abs(rest);
            }
        }
    }

    //Gain a given amount of proviant
    public void gainProviant(int _amount)
    {
        int rest = 0;
        foreach (PackAnimal packAnimal in packAnimals)
        {
            rest = packAnimal.load(_amount);
            if (rest <= 0)
            {
                break;
            }
        }
    }

    //Update FoodUptake Variable
    public void updateActualFoodUptake()
    {
        foodUptakePerRound = getFoodUptake();
    }

    //Get Actual Food Uptake
    public int getFoodUptake()
    {
        int amount = 0;
        foreach (Companion comp in companions)
        {
            amount += comp.ActualFoodDemand;
        }
        return amount;
    }

    //Update actual food stock variable
    public void updateActualFoodStock()
    {
        foodStorage = getFoodStock();
    }

    //get actual food stock
    public int getFoodStock()
    {
        int amount = 0;
        foreach (PackAnimal packAnimal in packAnimals)
        {
            amount += packAnimal.actualLoad;
        }
        return amount;
    }

    //add a companion to caravan
    public void addCompanion(Companion _comp)
    {
        companions.Add(_comp);
        EventManager.TriggerEvent("FoodUptakeChanged");
    }

    //remove a companion from caravan
    public void removeCompanion(Companion _comp)
    {
        companions.Remove(_comp);
        EventManager.TriggerEvent("FoodUptakeChanged");
    }

}
