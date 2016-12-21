using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;

public static class SceneHandler {

    //Todo: get Tiles from MapGenerator
    //public static List<MapTile>
    public static List<Meeple> meeples;
    public static List<Sandstorm> sandstorms;
    public static int turn; 
    //Todo: Deserialized Events from xml. Missing Serializeable class
    //public static List<Events> 
    public static Companion activeCompanion;
    public static GameMode activeMode;
    public static TacticalGame activeTacticalGame;
    public static Random rand;

    public static void init()
    {
        meeples = new List<Meeple>();
        sandstorms = new List<Sandstorm>();
        turn = 0;
        activeCompanion = null;
        activeMode = GameMode.EXPLORATION;
        rand = new Random();

        Debug.Log("START SCENE");
        meeples.Add(new Hunter(new Vector2(0, 1), "Hunter", 1, 10));
        meeples.Add(new Healer(new Vector2(1, 3), "Healer", 1, 10));
        meeples.Add(new PackAnimal(new Vector2(1, 1), "PackAnimal 1", 10, 5));
        meeples.Add(new PackAnimal(new Vector2(1, 2), "PackAnimal 2", 20, 15));
        meeples.Add(new Mercenary(new Vector2(3, 1), "Mercenery", 1, 10));
        meeples.Add(new Scout(new Vector2(2, 1), "Scout", 1, 10));

        TacticalGame game = new BattleGround();
        game.run();

        while (activeMode == GameMode.TACTICAL)
        {
            getAllMeeplesFromType<Mercenary>()[0].fight(getAllMeeplesFromType<Opponent>()[0]);
            game.onPlayerInteractionEnded();

        }
        

    }

    public static void endTurn()
    {
        
    }

    public static void consumeProviant(int _amount)
    {
        List<PackAnimal> packAnimals = getAllMeeplesFromType<PackAnimal>();

        Debug.Log("Caravan consumes proviant " + _amount);

        foreach (PackAnimal packAnimal in packAnimals)
        {
            _amount = packAnimal.unload(_amount);
            if (_amount <= 0)
            {
                break;
            }
        }

    }

    public static void gainProviant(int _amount)
    {
        List<PackAnimal> packAnimals = getAllMeeplesFromType<PackAnimal>();

        Debug.Log("Caravan gains proviant " + _amount);

        foreach (PackAnimal packAnimal in packAnimals)
        {
            _amount = packAnimal.load(_amount);
            if (_amount <= 0)
            {
                break;
            }
        }
    }

    public static int storagedProviant()
    {
        List<PackAnimal> packAnimals = getAllMeeplesFromType<PackAnimal>();
        int storagedProviant = 0;

        foreach (PackAnimal packAnimal in packAnimals)
        {
            storagedProviant += packAnimal.ActualLoad;
        }
        return storagedProviant;
    }

    public static int[] rollDices(int _dice, int _diceValue)
    {
        int[] diceCup = new int[_dice];
        for (int i = 0; i < _dice; i++)
        {
            diceCup[i] = rollDice(_diceValue);
        }
        return diceCup;
    }

    public static int rollDice(int _diceValue)
    {
        int rolledValue = rand.Next(1, _diceValue + 1);
        return rolledValue;
    }

    public static List<T> getAllMeeplesFromType<T>(bool _includeSubClasses = true)
    {
        List<T> genericMeeples = new List<T>();
        foreach (Meeple meeple in meeples)
        {
            
            if (meeple.GetType().IsSubclassOf(typeof(T)) || meeple.GetType() == typeof(T))
            {
                T genericMeeple = (T)(object)meeple;
                genericMeeples.Add(genericMeeple);
            }
        }
        return genericMeeples;
    }

    public static void closestMeepleTo()
    {
        //Todo: Missing IMplementation
    }

    public static T convertClass<T>(T readData)
    {
        if (readData is T)
        {
            return (T)readData;
        }
        try
        {
            return (T)Convert.ChangeType(readData, typeof(T));
        }
        catch (InvalidCastException)
        {
            return default(T);
        }
    }
}
