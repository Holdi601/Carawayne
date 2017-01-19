using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventFunctionCollection : MonoBehaviour
{
    // Parse all the stuff to do all the things :D
    public static void CallEventFunctions(string[] eventCode, string[] consequenceCode)
    {
        Debug.Log("Calling Event Functions!");

        string[][] eventCodeExecutorPairs = new string[eventCode.Length][];
        string[][] consequenceCodeExecutorPairs = new string[consequenceCode.Length][];

        for (int i = 0; i < eventCode.Length; i++)
        {
            eventCodeExecutorPairs[i] = eventCode[i].Split("|".ToCharArray());

            FindFunctions(eventCodeExecutorPairs[i], false);
        }

        for (int i = 0; i < consequenceCode.Length; i++)
        {
            consequenceCodeExecutorPairs[i] = consequenceCode[i].Split("|".ToCharArray());

            FindFunctions(consequenceCodeExecutorPairs[i], true);
        }
    }

    private static void FindFunctions(string[] code, bool consequence)
    {
        int paramInt_01 = 0;
        int paramInt_02 = 0;
        int paramInt_03 = 0;

        HexaPos paramHex_01 = new HexaPos();
        HexaPos paramHex_02 = new HexaPos();
        HexaPos paramHex_03 = new HexaPos();

        tileType paramTileType_01 = tileType.Desert;
        tileType paramTileType_02 = tileType.Desert;
        tileType paramTileType_03 = tileType.Desert;

        
        string paramMeeple_01 = "";
        string paramMeeple_02 = "";
        string paramMeeple_03 = "";

        bool paramBool_01 = false;
        bool paramBool_02 = false;
        bool paramBool_03 = false;


        for (int i = 0; i < code.Length; i++)
        {
            Debug.Log("Event-Code-Slot: " + i + ": " + code[i]);
        }

        if (code.Length > 1)
        {
            if (code[1].Contains(","))
            {
                string[] temp = code[1].Split(",".ToCharArray());
                paramHex_01.x = int.Parse(temp[0]);
                paramHex_01.y = int.Parse(temp[1]);
            }
            else
            {
                switch (code[1])
                {
                    case "false":
                        paramBool_01 = false;
                        break;
                    case "true":
                        paramBool_01 = true;
                        break;

                    case "1":
                        paramInt_01 = 1;
                        break;
                    case "2":
                        paramInt_01 = 2;
                        break;
                    case "3":
                        paramInt_01 = 3;
                        break;
                    case "4":
                        paramInt_01 = 4;
                        break;
                    case "5":
                        paramInt_01 = 5;
                        break;
                    case "6":
                        paramInt_01 = 6;
                        break;
                    case "7":
                        paramInt_01 = 7;
                        break;
                    case "8":
                        paramInt_01 = 8;
                        break;
                    case "9":
                        paramInt_01 = 9;
                        break;

                    case "Desert":
                        paramTileType_01 = tileType.Desert;
                        break;
                    case "Oasis":
                        paramTileType_01 = tileType.Oasis;
                        break;
                    case "Forest":
                        paramTileType_01 = tileType.Forrest;
                        break;
                    case "Mountain":
                        paramTileType_01 = tileType.Mountain;
                        break;

                        
                    case "Healer": 
                    case "Mercenary":  
                    case "Hunter":   
                    case "Prince":        
                    case "HuntedAnimal":
                    case "Opponent":
                    case "Scout":
                    case "PackAnimal":
                        paramMeeple_01 = code[1];
                        break;
                }
            }
        }
        if (code.Length > 2)
        {
            if (code[2].Contains(","))
            {
                string[] temp = code[2].Split(",".ToCharArray());
                paramHex_02.x = int.Parse(temp[0]);
                paramHex_02.y = int.Parse(temp[1]);
            }
            else
            {
                switch (code[2])
                {
                    case "false":
                        paramBool_02 = false;
                        break;
                    case "true":
                        paramBool_02 = true;
                        break;

                    case "1":
                        paramInt_02 = 1;
                        break;
                    case "2":
                        paramInt_02 = 2;
                        break;
                    case "3":
                        paramInt_02 = 3;
                        break;
                    case "4":
                        paramInt_02 = 4;
                        break;
                    case "5":
                        paramInt_02 = 5;
                        break;
                    case "6":
                        paramInt_02 = 6;
                        break;
                    case "7":
                        paramInt_02 = 7;
                        break;
                    case "8":
                        paramInt_02 = 8;
                        break;
                    case "9":
                        paramInt_02 = 9;
                        break;

                    case "Desert":
                        paramTileType_02 = tileType.Desert;
                        break;
                    case "Oasis":
                        paramTileType_02 = tileType.Oasis;
                        break;
                    case "Forest":
                        paramTileType_02 = tileType.Forrest;
                        break;
                    case "Mountain":
                        paramTileType_02 = tileType.Mountain;
                        break;

                    case "Healer":
                    case "Mercenary":
                    case "Hunter":
                    case "Prince":
                    case "HuntedAnimal":
                    case "Opponent":
                    case "Scout":
                    case "PackAnimal":
                        paramMeeple_02 = code[2];
                        break;
                }
            }
        }
        if (code.Length > 3)
        {
            if (code[3].Contains(","))
            {
                string[] temp = code[3].Split(",".ToCharArray());
                paramHex_03.x = int.Parse(temp[0]);
                paramHex_03.y = int.Parse(temp[1]);
            }
            else
            {
                switch (code[3])
                {
                    case "false":
                        paramBool_03 = false;
                        break;
                    case "true":
                        paramBool_03 = true;
                        break;

                    case "1":
                        paramInt_03 = 1;
                        break;
                    case "2":
                        paramInt_03 = 2;
                        break;
                    case "3":
                        paramInt_03 = 3;
                        break;
                    case "4":
                        paramInt_03 = 4;
                        break;
                    case "5":
                        paramInt_03 = 5;
                        break;
                    case "6":
                        paramInt_03 = 6;
                        break;
                    case "7":
                        paramInt_03 = 7;
                        break;
                    case "8":
                        paramInt_03 = 8;
                        break;
                    case "9":
                        paramInt_03 = 9;
                        break;

                    case "desert":
                        paramTileType_03 = tileType.Desert;
                        break;
                    case "oasis":
                        paramTileType_03 = tileType.Oasis;
                        break;
                    case "forest":
                        paramTileType_03 = tileType.Forrest;
                        break;
                    case "mountain":
                        paramTileType_03 = tileType.Mountain;
                        break;

                    case "Healer":
                    case "Mercenary":
                    case "Hunter":
                    case "Prince":
                    case "HuntedAnimal":
                    case "Opponent":
                    case "Scout":
                    case "PackAnimal":
                        paramMeeple_03 = code[3];
                        break;
                }
            }
        }

        switch(code[0])
        {
            case "GETFIELDSOFTYPE":
                if(!consequence)
                {
                    GetFieldsOfType(paramTileType_01);
                }
                else
                {
                    // Wait until (something), then trigger Consequence functions 
                }
                break;

                // TODOOOO
        }
    }

    //Returns 0-5, möglicherweise vollkommen unnötig
    public static int GetRandomDirection()
    {
        System.Random rnd = new System.Random();
        return rnd.Next(0, 5);
    }

    public static tileType GetRandomFieldType()
    {
        System.Random rnd = new System.Random();
        int nmbr = rnd.Next(0, 3);
        switch (nmbr)
        {
            case 0: return tileType.Desert;
            case 1: return tileType.Forrest;
            case 2: return tileType.Mountain;
            case 3: return tileType.Oasis;
            default: return tileType.Desert;
        }
    }

    public static HexaPos GetRandomField()
    {
        System.Random rnd = new System.Random();
        int x = rnd.Next(0, SceneHandler.largeMap.GetLength(0));
        int y = rnd.Next(0, SceneHandler.largeMap.GetLength(1));

        HexaPos res = new HexaPos(x, y);
        return res;
    }

    public static int GetDirection(HexaPos pos, HexaPos tgtPos)
    {
        return Map.getDirection(pos, tgtPos);
    }

    public static List<HexaPos> GetFieldsOfType(tileType type)
    {
        return Map.GetFieldsOfType(type);
    }

    public static List<Tile> GetFieldsOfType_t(tileType type)
    {
        return Map.GetFieldsOfType_t(type);
    }

    public static List<GameObject> GetFieldsOfType_g(tileType type)
    {
        return Map.GetFieldsOfType_g(type);
    }

    public static List<HexaPos> GetFieldsHidden()
    {
        return Map.GetFieldsHidden();
    }

    public static List<HexaPos> GetFieldsInRadius(int radius)
    {
        return Map.GetFieldsInRadius(radius);
    }

    public static void FieldChangeType(HexaPos pos, tileType type)
    {
        Map.FieldChangeType(pos, type);
    }

    public static HexaPos getPositionDirectionalByDistance(HexaPos _originalPos, int _dir, int _dist)
    {
        return Map.getPositionDirectionalByDistance(_originalPos, _dir, _dist);
    }

    //Get the field in the direction 1-6, with an distance of Y from the player.
    private static void GetFieldInDirection(int direction, int field)
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
    private static void MarkEventOutcome(bool outcome)
    {

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

    private static void PlayerToField(HexaPos gotoTile)
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

        switch (typeof(Meeple).ToString())
        {
            case "Mercenary":
                
                break;
            case "Hunter":
                
                break;
            case "Prince":
                
                break;
            case "HuntedAnimal":
                
                break;
            case "Opponent":
                
                break;
            case "Scout":
                
                break;
            case "PackAnimal":
                
                break;
            default:
                return null;
        }
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
