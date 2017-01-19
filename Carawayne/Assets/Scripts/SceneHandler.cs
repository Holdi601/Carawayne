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
    public static HexaPos finishTile;
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
        finishTile = EventFunctionCollection.GetRandomField();
        Map.setFinish(finishTile);
        
        //WICHTIG DRIN BEHALTEN!!!
        caravanPosition = new HexaPos(0, 0);
        

        //Map.discoverAllTiles(true);
        //Gesamt Skalierung des Brettspiels hilfreich für die Kameraführung. Momentan skalierung 10x
        Initialisation.mapGO.transform.localScale = new Vector3(10, 10, 10);
        turn = 0;
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
        
        activeCompanion = null;
        activeMode = GameMode.EXPLORATION;
        rand = new System.Random();

        //List<HexaPos> opponentsPos = new List<HexaPos>();
        //opponentsPos.Add(new HexaPos(12,12));
        //opponentsPos.Add(new HexaPos(12,13));
        //opponentsPos.Add(new HexaPos(12,14));
        //opponentsPos.Add(new HexaPos(12,15));
        //TacticalGame game = new BattleGround(opponentsPos);
        //game.init();
        //game.run();

        //while (activeMode == GameMode.TACTICAL)
        //{
        //    Mercenary activeMeeple = getAllMeeplesFromType<Mercenary>()[0];
        //    Opponent targetMeeple = getAllMeeplesFromType<Opponent>()[0];
        //    int dist = Map.distance(activeMeeple.Pos, targetMeeple.Pos);

        //    if (dist <= 1)
        //    {
        //        activeMeeple.fight(targetMeeple);
        //    }
        //    else
        //    {
        //        activeMeeple.moveTowardsTarget(targetMeeple.Pos);
        //    }
        //    game.onPlayerInteractionEnded();
        //}

        //HUNTINGGAME
        
        TacticalGame game = new HuntingGround(5,2);
        game.init();
        game.run();

        
        while (activeMode == GameMode.TACTICAL)
        {
            List<Hunter> hunters = getAllMeeplesFromType<Hunter>();
            foreach (Hunter hunter in hunters)
            {
                HuntedAnimal targetMeeple = getAllMeeplesFromType<HuntedAnimal>()[0];
                int dist = Map.distance(hunter.Pos, targetMeeple.Pos);

                if (dist <= 6)
                {
                    hunter.hunt(targetMeeple);
                }
                else
                {
                    hunter.moveTowardsTarget(targetMeeple.Pos);
                }
            }
            
            game.onPlayerInteractionEnded();
            
            
        }

        

    }

    public static T createMeeple<T>(string _name, HexaPos _hexPos) where T:Meeple
    {
        GameObject meepleObj = null;

        switch (typeof(T).ToString())
        {
            case "Mercenary":
                meepleObj = (GameObject)Instantiate(Initialisation.soldier);
                break;
            case "Hunter":
                meepleObj = (GameObject)Instantiate(Initialisation.worker);
                break;
            case "Prince":
                meepleObj = (GameObject)Instantiate(Initialisation.king);
                break;
            case "HuntedAnimal":
                meepleObj = (GameObject)Instantiate(Initialisation.antilope);
                break;
            case "Opponent":
                meepleObj = (GameObject)Instantiate(Initialisation.raider);
                break;
            case "Healer":
                meepleObj = (GameObject)Instantiate(Initialisation.healer);
                break;
            case "Scout":
                meepleObj = (GameObject)Instantiate(Initialisation.scout);
                break;
            case "PackAnimal":
                meepleObj = (GameObject)Instantiate(Initialisation.camel);
                break;
            default:
                return null;
        }
        
        Meeple meeple = meepleObj.AddComponent<T>();
        Vector3 scaleSave = meeple.transform.localScale;
        Vector3 posSave = meeple.transform.localPosition;
        meeple.Pos = _hexPos;
        meeple.meepleName = _name;

        positionAndParent_Meeple(meepleObj, _hexPos);
        meeple.transform.localScale = scaleSave;
        meeple.transform.localPosition = posSave;
        

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

    public static void endTurn(bool _moved)
    {
        if (turn > 0)
        {
            Debug.Log("The turn ended. Did the Caravan moved? " + _moved);
            GameObject tileHolder = GameObject.Find("tileHolder");
            SoundHelper sh = tileHolder.GetComponent<SoundHelper>();
            if (_moved)
            {
                sh.Play("move");
            }
            else
            {
                sh.Play("rest");
            }
        }

        turn++;
    }


    public static void moveCaravan(HexaPos pos)
    {
        lastPosition = caravanPosition;
        Vector3 pubPos = Map.MapTileToPosition(pos);
        Initialisation.innerTileHolderGO.transform.localPosition = pubPos;
        Map.setActiveTile(pos, lastPosition);
        Map.discover(pos);
        caravanPosition = pos;
        endTurn(true);

    }

    public static void positionAndParent_Meeple(GameObject Meeple, HexaPos position)
    {
        GameObject par = GameObject.Find("MeepleCollection");
        GameObject fath = new GameObject(Meeple.GetComponent<Meeple>().meepleName);
        
        fath.transform.localScale = par.transform.lossyScale;
        fath.transform.position = par.transform.position;

        
        Meeple.transform.localScale = par.transform.lossyScale;
        Meeple.transform.position = par.transform.position;
        
        //Meeple.transform.Translate(new Vector3(0, 0.4f, 0), Space.Self);
        fath.transform.parent = par.transform;
        Meeple.transform.parent = fath.transform;
        setMeeplePos(Meeple, position);
        
        Meeple mip = Meeple.GetComponent<Meeple>();
        if (mip.GetType().IsSubclassOf(typeof(Companion)))
        {
            Companion c = Meeple.GetComponent<Companion>();
            if (c.GetType().Name != "PackAnimal")
            {
                c.setFoodPackages_hpBar();
            }
            
        }
        
    }

    public static void positionAndParent_HP(GameObject Meeple, GameObject hp)
    {
        hp.transform.localScale = Meeple.transform.lossyScale;
        hp.transform.position = Meeple.transform.position;
        hp.transform.parent = Meeple.transform.parent;
    }

    public static void positionAndParent_Food(GameObject Meeple, GameObject food)
    {
        
        food.transform.localScale = Meeple.transform.lossyScale;
        food.transform.position = Meeple.transform.position;
        food.transform.parent = Meeple.transform.parent;
    }

    public static void setMeeplePos(GameObject meepleObj, HexaPos position)
    {
        Meeple meeple = meepleObj.GetComponentInChildren<Meeple>();
        smallMap[meeple.Pos.x, meeple.Pos.y].GetComponent<innerTile>().meep = null;

        meepleObj.transform.parent.transform.localPosition = Map.MapTileToPosition(position);
        meeple.Pos = position;

        if (!(position.x<0||position.x>14||position.y<0||position.y>14))
        {
            smallMap[position.x, position.y].GetComponent<innerTile>().meep = meeple;
        }
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
                    Debug.Log("You won. You cunt.");
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
