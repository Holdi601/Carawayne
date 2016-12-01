using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Companion : Meeple
{
    public string CharName;
    public CompanionClasses charClass;
    public int maxCondition;
    public int actualCondition;
    public int originalFoodDemand;
    public int actualFoodDemand;
    public MeepleConditionState conditionState;
    public MeepleFoodRationState foodRationState;

    //MONOBEHAVIOUR FUNCTIONS
    //----------------
    void Start()
    {
        ConditionState = MeepleConditionState.COMMON;
        FoodRationState = MeepleFoodRationState.FULL_RATION;
    }

    //COMPANION FUNCTIONS
    //----------------

    public void starve()
    {

    }

    //public void kill()
    //{
    //    ActualCondition = 0;
    //    ConditionState = MeepleConditionState.DEAD;
    //    //gameObject.transform.parent = GameObject.Find("Graveyard").transform;
    //}

    public void gainStrength()
    {
        ActualCondition++;
    }

    public void LoseStrength()
    {
        ActualCondition--;
    }

    // GETTER & SETTER
    //----------------
    public int ActualCondition
    {
        get { return actualCondition; }
        set
        {
            actualCondition = value;

            if (ConditionState == MeepleConditionState.UNCONSCIOUS && actualCondition <= 0)
                ConditionState = MeepleConditionState.DEAD;
            if (ConditionState != MeepleConditionState.DEAD && actualCondition <= 0)
                ConditionState = MeepleConditionState.UNCONSCIOUS;
            else if (actualCondition > 0)
                ConditionState = MeepleConditionState.COMMON;

            if (actualCondition > maxCondition)
            {
                actualCondition = maxCondition;
            } else if (actualCondition < 0)
            {
                actualCondition = 0;
            }
        }
    }

    public string charName
    {
        get { return CharName; }
        set { CharName = value; }
    }

    public MeepleConditionState ConditionState
    {
        get { return conditionState; }
        set
        {
            conditionState = value;

            switch (conditionState)
            {
                case MeepleConditionState.DEAD:
                    actualCondition = 0;
                    gameObject.transform.parent = GameObject.Find("Graveyard").transform;
                    // Todo: Trigger Dead/remove
                    break;

                case MeepleConditionState.COMMON:

                    gameObject.transform.parent = GameObject.Find("Companions").transform;
                    if (actualCondition <= 0)
                        actualCondition = 1;
                    break;

                case MeepleConditionState.UNCONSCIOUS:
                    gameObject.transform.parent = GameObject.Find("Companions").transform;
                    actualCondition = 0;
                    break;
            }

            EventManager.TriggerEvent("ConditionChanged");
        }
    }

    public int ActualFoodDemand
    {
        get { return actualFoodDemand; }
        set
        {
            actualFoodDemand = value;

            if (actualFoodDemand <= 0)
            {
                actualFoodDemand = 0;
                FoodRationState = MeepleFoodRationState.STARVING;
            }
            else if (actualFoodDemand >= originalFoodDemand)
            {
                actualFoodDemand = originalFoodDemand;
                FoodRationState = MeepleFoodRationState.FULL_RATION;
            }
            else
            {
                FoodRationState = MeepleFoodRationState.HALF_RATION;
            }

            EventManager.TriggerEvent("FoodUptakeChanged");
        }
    }

    public MeepleFoodRationState FoodRationState
    {
        get { return foodRationState; }
        set
        {
            foodRationState = value;

            switch (foodRationState)
            {
                case MeepleFoodRationState.FULL_RATION:
                    actualFoodDemand = OriginalFoodDemand;
                    break;

                case MeepleFoodRationState.HALF_RATION:
                    actualFoodDemand = (int)(OriginalFoodDemand / 2.0f);
                    break;

                case MeepleFoodRationState.STARVING:
                    actualFoodDemand = 0;
                    break;
            }

            EventManager.TriggerEvent("FoodUptakeChanged");
        }
    }

    public int MaxCondition
    {
        get { return maxCondition; }
        set
        {
            maxCondition = value;
            if (maxCondition < 0)
                maxCondition = 0;
        }
    }

    public int OriginalFoodDemand
    {
        get { return originalFoodDemand; }
        set
        {
            originalFoodDemand = value;
            if (originalFoodDemand < 0)
                originalFoodDemand = 0;
        }
    }

}
