﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.WSA;


public class Caravan : MonoBehaviour
{
    public int foodStorage;
    public int foodUptakePerRound;
    public List<Companion> companions;
    public List<PackAnimal> packAnimals;
    public List<Meeple> fooList; 

    //MONOBEHAVIOUR FUNCTIONS
    //-----------------------
    void Awake()
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
            EventManager.TriggerEvent("ViewChanged");
        }

        //Identify discepancies between caravaonpackANimals and packAnimalObjectChilds
        if (packAnimals.Count != gameObject.transform.FindChild("PackAnimals").childCount)
        {
            packAnimals = gameObject.transform.FindChild("PackAnimals").GetComponentsInChildren<PackAnimal>().ToList();
            EventManager.TriggerEvent("FoodStockChanged");
            EventManager.TriggerEvent("ViewChanged");
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
        foreach (PackAnimal packAnimal in packAnimals)
        {
            _amount = packAnimal.load(_amount);
            if (_amount <= 0)
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

    public void newCompanion()
    {
        GameObject go = Instantiate(Resources.Load("Prefabs/Companions/Worker", typeof(GameObject))) as GameObject;
        go.transform.SetParent(transform.FindChild("Companions"), false);

        Companion comp = go.GetComponent<Companion>();

        go.name = comp.charClass + "_" + comp.CharName;

        companions.Add(comp);

        EventManager.TriggerEvent("FoodUptakeChanged");
        EventManager.TriggerEvent("ViewChanged");
    }

    public void newPackAnimal()
    {
        GameObject go = Instantiate(Resources.Load("Prefabs/PackAnimals/Camel", typeof(GameObject))) as GameObject;
        go.transform.SetParent(transform.FindChild("PackAnimals"), false);

        PackAnimal pAnimal = go.GetComponent<PackAnimal>();

        packAnimals.Add(pAnimal);

        EventManager.TriggerEvent("FoodStockChanged");
        EventManager.TriggerEvent("ViewChanged");
    }

    //add a companion to caravan
    public void addCompanion(Companion _comp)
    {
        companions.Add(_comp);

        _comp.gameObject.transform.SetParent(transform.FindChild("Companions"));

        EventManager.TriggerEvent("FoodUptakeChanged");
        EventManager.TriggerEvent("ViewChanged");
    }

    //remove a companion from caravan
    public void removeCompanion(Companion _comp)
    {
        companions.Remove(_comp);

        _comp.gameObject.transform.SetParent(transform.parent.FindChild("Graveyard"));

        EventManager.TriggerEvent("FoodUptakeChanged");
        EventManager.TriggerEvent("ViewChanged");
    }

    //add a companion to caravan
    public void addPckAnimal(PackAnimal _packAnimal)
    {
        packAnimals.Add(_packAnimal);
        EventManager.TriggerEvent("FoodStockChanged");
        EventManager.TriggerEvent("ViewChanged");
    }

    //remove a companion from caravan
    public void removePackAnimal(PackAnimal _packAnimal)
    {
        packAnimals.Remove(_packAnimal);
        EventManager.TriggerEvent("FoodStockChanged");
        EventManager.TriggerEvent("ViewChanged");
    }
}
