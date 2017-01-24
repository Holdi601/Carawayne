using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine.SceneManagement;
using System;

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
    public static bool scoutingActive;
    public static bool healingActive;

    public static int foodStorage;
    public static int foodUptakePerRound;


    public static TacticalGame activeTacticalGame;
    public static System.Random rand;

    void Start()
    {
        
        //Create large Landscape
        largeMap = Map.createLandscape(20, 20);
        scoutingActive = false;
        healingActive = false;
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
        moveCaravan(new HexaPos(0, 0));
        meeples = new List<Meeple>();
        sandstorms = new List<Sandstorm>();
        
        activeCompanion = null;
        rand = new System.Random();

        activeMode = GameMode.PREPARATION;

        createMeeple<Hunter>("prep1", new HexaPos(2, 11), true);
        createMeeple<Mercenary>("prep2", new HexaPos(4, 12), true);
        createMeeple<Healer>("prep3", new HexaPos(5, 13), true);
        createMeeple<Scout>("prep4", new HexaPos(7, 14), true);
        createMeeple<PackAnimal>("prep5", new HexaPos(13, 12), true);
        createMeeple<Prince>("Prince", new HexaPos(8, 7));

        
    }

    //Update FoodUptake Variable
    public static void updateActualFoodUptake()
    {
        foodUptakePerRound = getFoodUptake();
    }

    //Get Actual Food Uptake
    public static int getFoodUptake()
    {
        int amount = 0;
        List<Companion> companions = getAllMeeplesFromType<Companion>();
        foreach (Companion comp in companions)
        {
            amount += comp.proviantDemand;
        }
        return amount;
    }

    //Update actual food stock variable
    public static void updateActualFoodStock()
    {
        foodStorage = storagedProviant();
    }

    public static T createMeeple<T>(string _name, HexaPos _hexPos, bool _neutral = false) where T:Meeple
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
        meepleObj.name = _name;
        Vector3 scaleSave = meeple.transform.localScale;
        Vector3 posSave = meeple.transform.localPosition;
        meeple.Pos = _hexPos;
        meeple.meepleName = _name;

        positionAndParent_Meeple(meepleObj, _hexPos);
        meeple.transform.localScale = scaleSave;
        meeple.transform.localPosition = posSave;

        GameObject tileHolder = GameObject.Find("tileHolder");
        SoundHelper sh = tileHolder.GetComponent<SoundHelper>();
        sh.Play("spawn");

        if (!_neutral)
        {
            meeples.Add(meeple);
        }
        
        highlightUnguidedPackAnimals();

        return (T)meeple;
    }

    public static void highlightUnguidedPackAnimals()
    {
        List<PackAnimal> packAnimals = getAllMeeplesFromType<PackAnimal>();
        foreach (PackAnimal pAnimal in packAnimals)
        {
            if (!pAnimal.isGuided())
            {
                List<HexaPos> guidedPoses = pAnimal.getGuidedPos();

                foreach (HexaPos guidPos in guidedPoses)
                {
                    MeshRenderer unguidedTile = smallMap[guidPos.x, guidPos.y].GetComponent<MeshRenderer>();
                    unguidedTile.material = Initialisation.innerTileForbiddenMaterial;
                }
            }
        }
    }

    public void startJourney()
    {
        Debug.Log("START JOUNREY");
        List<PackAnimal> packAnimals = getAllMeeplesFromType<PackAnimal>();
        foreach (PackAnimal packAnimal in packAnimals)
        {
            if (!packAnimal.isGuided())
            {
                return;
            }
        }

        List<Companion> comps = getAllMeeplesFromType<Companion>();
        if (comps.Count <= 0)
        {
            return;
        }

        Destroy(GameObject.Find("PrepareStartJourneyButton"));

        GameObject tmp = GameObject.Find("prep1").transform.parent.gameObject;
        Destroy(tmp);
        tmp = GameObject.Find("prep2").transform.parent.gameObject;
        Destroy(tmp);
        tmp = GameObject.Find("prep3").transform.parent.gameObject;
        Destroy(tmp);
        tmp = GameObject.Find("prep4").transform.parent.gameObject;
        Destroy(tmp);
        tmp = GameObject.Find("prep5").transform.parent.gameObject;
        Destroy(tmp);

        activeMode = GameMode.EXPLORATION;
    }

    public static void consumeProviant(int _amount)
    {

        List<PackAnimal> packAnimals = getAllMeeplesFromType<PackAnimal>();
        List<Companion> companions = getAllMeeplesFromType<Companion>();

        int i = 0;
        foreach (Companion comp in companions)
        {
            if (comp.GetType() != typeof(PackAnimal))
            {
                int rest = 0;

                //Rest penalty through Starving or half rations
                rest = (comp.ProviantDemandMax - comp.ProviantDemand);
                comp.Strength -= rest;

                //Rest penalty through not having enough food on packAnimal
                if (packAnimals.Count <= 0)
                {
                    rest = comp.ProviantDemand;
                }else
                {
                    rest = packAnimals[i].unload(comp.ProviantDemand);
                }
                

                while (rest > 0 && i < packAnimals.Count - 1)
                {
                    i++;
                    rest = packAnimals[i].unload(comp.ProviantDemand);
                }

                if (rest > 0)
                {
                    comp.Strength -= rest;
                }
            }
        }

        //List<PackAnimal> packAnimals = getAllMeeplesFromType<PackAnimal>();

        //Debug.Log("Caravan consumes proviant " + _amount);

        //foreach (PackAnimal packAnimal in packAnimals)
        //{
        //    _amount = packAnimal.unload(_amount);
        //    if (_amount <= 0)
        //    {
        //        break;
        //    }
        //}

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
                consumeProviant(getFoodUptake());
            }
            else
            {
                sh.Play("rest");
                consumeProviant(getFoodUptake());
            }

            List<PackAnimal> packAnimals = getAllMeeplesFromType<PackAnimal>();
            foreach (PackAnimal packAnimal in packAnimals)
            {
                if (!packAnimal.isGuided())
                {
                    meeples.Remove(packAnimal);
                    GameObject tmp = packAnimal.gameObject.transform.parent.gameObject;
                    Destroy(tmp);
                    sh.Play("leftBehind");
                }
            }

            List<Companion> comps = getAllMeeplesFromType<Companion>();
            List<Prince> prince = getAllMeeplesFromType<Prince>();
            if (comps.Count <= 0 || prince.Count <= 0)
            {
                endGame(comps);
            }
            Map.highlightAllInnerTiles(false);

            foreach (Companion comp in comps)
            {
                comp.HasActionOutstanding = true;
            }

            tileType lTileType = largeMap[caravanPosition.x, caravanPosition.y].GetComponent<Tile>().tile_type;

            if (lTileType == tileType.Forrest)
            {
                List<Hunter> hunter = getAllMeeplesFromType<Hunter>();
                if (hunter.Count > 0)
                {
                    activeTacticalGame = new HuntingGround(rand.Next(0,6), 2);
                    activeTacticalGame.init();
                    activeTacticalGame.run();
                }
            } else if (lTileType == tileType.Oasis)
            {
                foreach (Companion comp in comps)
                {
                    comp.Strength += 5;
                }
            }
            else
            {
                int rndEnemySpawn = rand.Next(0, 15);
                if (rndEnemySpawn < 4)
                {
                    List<Mercenary> mercs = getAllMeeplesFromType<Mercenary>();
                    if (mercs.Count > 0)
                    {
                        activeTacticalGame = new BattleGround(rand.Next(0, 6));
                        activeTacticalGame.init();
                        activeTacticalGame.run();
                    }
                    else
                    {
                        endGame(comps);
                    }
                }
            }
        }
        turn++;
    }

    public static void endGame(List<Companion> comps)
    {
        foreach (Companion comp in comps)
        {
            meeples.Remove(comp);
            GameObject tmp = comp.gameObject.transform.parent.gameObject;
            Destroy(tmp);
        }

        SceneManager.LoadScene("Prototype v0.6");
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
        Meeple.transform.localEulerAngles = new Vector3(0, 180, 0);
        
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

        if (activeMode == GameMode.TACTICAL && meeple.GetType().IsSubclassOf(typeof(Companion)))
        {
            activeCompanion.HasActionOutstanding = false;
        }

        highlightUnguidedPackAnimals();
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
        
        if (Map.distance(pos, caravanPosition)<2 && activeMode==GameMode.EXPLORATION &&!scoutingActive)
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
        }else if (Map.distance(pos, caravanPosition) < 3 && activeMode == GameMode.EXPLORATION && scoutingActive)
        {
            Map.discover(pos);
            scoutingActive = false;
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
