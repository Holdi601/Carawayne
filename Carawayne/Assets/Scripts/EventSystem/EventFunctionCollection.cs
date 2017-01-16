using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventFunctionCollection : MonoBehaviour
{
    // will probably need multiple params
    public static void CallEventFunctions(string eventCode, string eventConsequenceCode)
    {
        Debug.Log("Calling Event Functions!");
        Debug.Log("CODE RECEIVED: " + eventCode + " / " + eventConsequenceCode);
        //switch(stuff)
        //{
        // find Functions to call here
        //}

        // Then call them

        // z.B. MoveSandstorm(10, 10);
    }

    //Returns the nearest sandstorm.
    private void GetNearestSandstorm()
    {

    }

    //Move the sandstorm in the direction, move it X fields.
    private static void MoveSandstorm(int direction, int fields)
    {

    }

    //Player cannot move to the next field for X rounds and has to take a break.
    private static void DisallowPlayerMove(int rounds)
    {

    }

    //Returns 0-5, möglicherweise vollkommen unnötig
    private static void GetRandomDirection()
    {

    }

    //Returns fieldtype
    private static void GetRandomFieldType()
    {

    }

    //as long as this is active, trigger consequence of next i events
    private static void TriggerConsequenceOnNextEvents(int i)
    {

    }

    //as long as this is active, consequences cannot be triggered of next i events
    private static void TriggerNoConsequenceOnNextEvents(int i)
    {

    }

    //Trigger an event for the current field.
    private static void TriggerEvent()
    {

    }

    //triggers specific event. consequence can be triggered by other events
    //private static void TriggerEvent(TileType type, Event _event)
    //{

    //}

    //triggers specific event, with or without consequence
    //private static void TriggerEvent(TileType type, Event _event, bool consequence)
    //{

    //}

    //triggers the consequence of last event, if possible.
    private static void TriggerLastEventConsequence()
    {

    }

    //Saves a var for later use
    private static void MarkEventOutcome(string outcome)
    {

    }

    //Or field.type
    //private static void GetFieldsOfType(TileType type)
    //{

    //}

    //Or field.hidden
    private static void GetFieldsHidden()
    {

    }

    private static void GetFieldsInRadius(int radius)
    {

    }

    //GetFieldInDirection(int dir) [0-5] //Instead use
    private static void GetFieldsInRange(int dir)
    {

    }
    // and use the 6 items in list 

    //private static void FieldChangeType(TileType type)
    //{

    //}

    //Get the field in the direction 1-6, with an distance of Y from the player.
    private static void GetFieldInDirection(int direction, int field)
    {

    }

    private static void PlayerToField(Tile gotoTile)
    {

    }

    // adds a useable or tradeable token to the player group, which would require additional efforts to implement
    //private static void PlayerAddItemToInventory(Item item)
    //{

    //}

    // ?
    private static void CombatStartFight()
    {

    }

    // ?
    private static void CombatAddEnemy()
    {

    }

    //Add resources to all available camels
    private static void AddResources()
    {

    }

    //keep in different action; remove resources from all camels evenly if possible
    private static void RemoveResourcesFromSingleCamel()
    {

    }

    private static void RemoveResourcesFromAllCamels()
    {

    }

    //Returns an int for all wasted resources this round
    private static int GetResourceUsage()
    {
        var usage = 0;
        return usage;
    }

    private static Meeple[] GetMeeplesOfType(Meeple type)
    {
        var meeple = new Meeple[5];
        return meeple;
    }

    //zone can be[0 - 2]
    private static Meeple[] GetMeeplesInZone(int zone)
    {
        var meeple = new Meeple[5];
        return meeple;
    }

    //prefer workers before mercs before healers before prince
    private static Meeple GetMeepleLowestPower(int power)
    {
        var meeple = new Meeple();
        return meeple;
    }

    //prefer workers before mercs before healers before prince
    private static Meeple GetMeepleHighestPower(int power)
    {
        var meeple = new Meeple();
        return meeple;
    }

    private static void GetRandomMeeple()
    {

    }

    private static void AddMeeple(Meeple meeple, string name, int power)
    {

    }

    //Or Meeple.Power = X
    private static void SetPowerMeeple(Meeple meeple)
    {

    }

    //Also used negativly
    private static void AddPowerToMeeple(int Power)
    {
        // Meeple.AddPower(Power);
    }

    //Used in single event
    private static void ResurrectMeeple(Meeple meeple)
    {
        //Meeple.Resurrect(meeple);
    }

    private static void AllowMeepleKick(bool kickable)
    {
        //Meeple.AllowKick(boolean kickable);
    }

    //Returns last slain meeple
    private static void GetMeepleLastDeath()
    {

    }

    //Used in single event
    private static void GetDeadMeepleAmount()
    {

    }

    //Mark one or multiple meeple for a further, delayed actions
    private static void MarkMeeple(Meeple[] meeple)
    {

    }

    //Kill a certain meeple
    private static void KillMeeple()
    {
        //Meeple.Kill();
    }

    //Reduces resource cost for a certain meeple this round, eg the scout
    private static void ReduceMeepleResourceCost(Meeple meeple, int rounds)
    {

    }

    //reduce all ability cost of certain meeple for X rounds
    private static void ReduceMeepleAbilityCost(Meeple meeple, int rounds)
    {

    }

    //Might be replaced by removing meeple.gollok and spawn an enemy.gollok instead on his position
    private static void TransformIntoEnemy(Meeple meeple)
    {

    }

    //Reset worm power on miss
    private static void OnMissPlayer()
    {

    }
}
