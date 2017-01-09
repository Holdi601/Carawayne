using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneHandler :MonoBehaviour{

    //Todo: get Tiles from MapGenerator
    //public static List<MapTile>

    public static List<Meeple> meeples;
    public static List<Sandstorm> sandstorms;
    public static int turn; 
    //Todo: Deserialized Events from xml. Missing Serializeable class
    //public static List<Events> 
    public static Companion activeCompanion;
    public static GameMode activeMode;
    public static GameObject[,] largeMap;
    public static GameObject[,] smallMap;
    public static HexaPos caravanPosition;
    private static HexaPos lastPosition;
    public static HexaPos selectedMapTile;
    public static Meeple selectedMeeple;
    public static int tacticalDirection=4;

    public static TacticalGame activeTacticalGame;
    public static System.Random rand;

    void Start()
    {
        
        //Create large Landscape
        largeMap = Map.createLandscape(20, 20);
        
        //Create tactical Map;
        smallMap = Map.createSmallHexa();
        Map.setLookout(5);

        //WICHTIG DRIN BEHALTEN!!!
        caravanPosition = new HexaPos(0, 0);
        
        //Map.discoverAllTiles(true);

        //Map.discoverAllTiles(true);
        //Gesamt Skalierung des Brettspiels hilfreich für die Kameraführung. Momentan skalierung 10x
        Initialisation.mapGO.transform.localScale = new Vector3(10, 10, 10);

        //Testing Area
        moveCaravan(new HexaPos(0, 0));

        meeples = new List<Meeple>();

        createMeeple<Mercenary>("Bernd", new HexaPos(5, 5));
        createMeeple<Mercenary>("Brutus", new HexaPos(6, 5));
        createMeeple<Scout>("Kyle", new HexaPos(7, 6));
        createMeeple<Hunter>("Tobi", new HexaPos(6, 6));
        createMeeple<Hunter>("Horst", new HexaPos(7, 7));
        createMeeple<Prince>("Kaliffa", new HexaPos(8, 8));
        createMeeple<PackAnimal>("Camel1", new HexaPos(9, 10));

        sandstorms = new List<Sandstorm>();
        turn = 0;
        activeCompanion = null;
        activeMode = GameMode.EXPLORATION;
        rand = new System.Random();

        List<HexaPos> opponentsPos = new List<HexaPos>();
        opponentsPos.Add(new HexaPos(12,12));
        opponentsPos.Add(new HexaPos(12,13));
        opponentsPos.Add(new HexaPos(12,14));
        opponentsPos.Add(new HexaPos(12,15));
        TacticalGame game = new BattleGround(opponentsPos);
        game.init();
        game.run();

        while (activeMode == GameMode.TACTICAL)
        {
            getAllMeeplesFromType<Mercenary>()[0].fight(getAllMeeplesFromType<Opponent>()[0]);
            game.onPlayerInteractionEnded();
        }

    }

    public static T createMeeple<T>(string _name, HexaPos _hexPos) where T:Meeple
    {
        GameObject meepleObj = (GameObject)Instantiate(Initialisation.soldier);
        
        Meeple meeple = meepleObj.AddComponent<T>();
        meeple.Pos = _hexPos;
        meeple.meepleName = _name;

        positionAndParent_Meeple(meepleObj, _hexPos);

        meeples.Add(meeple);
        return (T)meeple;
    }

    public static void createCompanion(Companion _comp)
    {
        
    }

    public static void createOpponent()
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

    public static void endTurn()
    {
        
    }


    public static void moveCaravan(HexaPos pos)
    {
        lastPosition = caravanPosition;
        Vector3 pubPos = Map.MapTileToPosition(pos);
        Initialisation.innerTileHolderGO.transform.localPosition = pubPos;
        Map.setActiveTile(pos, lastPosition);
        Map.discover(pos);
        caravanPosition = pos;
        endTurn();

    }

    public static void positionAndParent_Meeple(GameObject Meeple, HexaPos position)
    {
        GameObject par = GameObject.Find("MeepleCollection");
        Meeple.transform.localScale = par.transform.lossyScale;
        Meeple.transform.position = par.transform.position;
        Meeple.transform.parent = par.transform;
        setMeeplePos(Meeple, position);

    }

    public static void setMeeplePos(GameObject Meeple, HexaPos position)
    {
        Meeple.transform.localPosition = Map.MapTileToPosition(position);
        Meeple.GetComponent<Meeple>().Pos = position;
    }

    public static void rotateCaravan(int rot) //Mins GUZS; plus UZS
    {
        GameObject tacticalMap = GameObject.Find("Playfield");
        tacticalMap.transform.RotateAround(Initialisation.innerTileHolderGO.transform.position, new Vector3(0, 1, 0), (float)(rot * 60));
        int temp = tacticalDirection + rot;
        while(temp<0 || temp > 5)
        {
            if (temp < 0)
            {
                temp = temp + 6;
            }
            else
            {
                temp = temp - 6;
            }
        }
        tacticalDirection = temp;
    }
    public static void setRotationDir(int dir)
    {
        int offset = dir - tacticalDirection;
        rotateCaravan(offset);
    }

    public static void clickMaptile(HexaPos pos)
    {
        
        if (Map.distance(pos, caravanPosition)<2 && activeMode==GameMode.EXPLORATION)
        {
            if (selectedMapTile == pos)
            {
                if (largeMap[pos.x, pos.y].GetComponent<Tile>().special == specialTile.None)
                {
                    Map.discover(pos);

                    if(largeMap[pos.x, pos.y].GetComponent<Tile>().tile_type == tileType.Oasis)
                    {
                        //Special Oasis ressources action Here + setNextevent here
                    }else if (largeMap[pos.x, pos.y].GetComponent<Tile>().tile_type == tileType.Forrest)
                    {
                        //Special Forrest Action Here + Ressource + setNextevent here
                    }
                    else if (largeMap[pos.x, pos.y].GetComponent<Tile>().tile_type == tileType.Mountain)
                    {
                        //Special Mountain Action here + Ressource + setNextevent here
                    }
                    else if(largeMap[pos.x, pos.y].GetComponent<Tile>().tile_type == tileType.Desert)
                    {
                        //Special Desert Action here + setNextevent here
                    }

                    moveCaravan(pos);
                    
                }else if(largeMap[pos.x, pos.y].GetComponent<Tile>().special == specialTile.Lookout)
                {
                    Map.discover(pos);
                }else if(largeMap[pos.x, pos.y].GetComponent<Tile>().special == specialTile.Finish)
                {
                    //Code to win the game here.
                }
                
            }
            else
            {
                largeMap[pos.x, pos.y].transform.FindChild("actualTile").GetComponent<discoveredTile>().highlight(true);
                largeMap[pos.x, pos.y].transform.FindChild("undiscovered").GetComponent<undiscoveredTile>().highlight(true);

                
                int tileDir = Map.getDirection(caravanPosition, pos);
                tileDir = tileDir * -1;
                setRotationDir(tileDir);
                
                if (selectedMapTile.init)
                {
                    if (selectedMapTile != caravanPosition)
                    {
                        largeMap[selectedMapTile.x, selectedMapTile.y].transform.FindChild("actualTile").GetComponent<discoveredTile>().highlight(false);
                        largeMap[selectedMapTile.x, selectedMapTile.y].transform.FindChild("undiscovered").GetComponent<undiscoveredTile>().highlight(false);
                    }
                    
                }
                selectedMapTile = pos;
            }
        }
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
}
