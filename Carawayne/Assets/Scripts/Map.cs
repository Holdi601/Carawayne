using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public enum TileSpace { Small, Large};

public struct HexaPos
{
    public int x;
    public int y;
    public bool init;

    public HexaPos(int xp, int yp)
    {
        x = xp;
        y = yp;
        init = true;
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        string result = x.ToString() + " "+ y.ToString();
        return result;
    }

    public static bool operator ==(HexaPos p1, HexaPos p2)
    {
        if(p1.x==p2.x && p1.y == p2.y)
        {
            return true;
        }else
        {
            return false;
        }
    }

    public static bool operator !=(HexaPos p1, HexaPos p2)
    {
        if (p1.x == p2.x && p1.y == p2.y)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

}

public class Map : MonoBehaviour {
    //Creates a map
    private static GameObject[,] landscape;

    public static GameObject[,] createSmallHexa()
    {
        //15x15 Feld gebraucht
        GameObject[,] result = new GameObject[15, 15];
        GameObject godfather = GameObject.Find("innerTileHolder");
        GameObject father = new GameObject("Playfield");
        GameObject tiles = new GameObject("inTiles");
        GameObject meepleCollections = new GameObject("MeepleCollection");
        tiles.transform.parent = father.transform;
        meepleCollections.transform.parent = father.transform;
        bool scndRow = false;

        for(int y=0; y<15; y++)
        {

            for(int x=0; x<15; x++)
            {
                result[x, y] = (GameObject)Instantiate(Initialisation.innerTile);
                result[x, y].AddComponent<MeshCollider>();
                result[x, y].transform.name = "innerTile: " + x.ToString() + " " + y.ToString();
                result[x, y].transform.parent = tiles.transform;
                Vector3 position = new Vector3();
                if (scndRow)
                {
                    position = new Vector3(0.866f + x * 1.732f, 0, y * 1.5f);
                }
                else
                {
                    position = new Vector3(x * 1.732f, 0, y * 1.5f);
                }
                innerTile it = result[x, y].AddComponent<innerTile>();
                it.posX = x;
                it.posY = y;
                result[x, y].transform.position = position;
                if(y==0|| y == 14)
                {
                    result[x, y].SetActive(false);
                }
                if (y % 2 != 0 && x == 13)
                {
                    result[x, y].SetActive(false);
                }

                if (x < 4 || x>13 )
                    {
                        result[x, y].SetActive(false);
                   
                    }
                    if((y==1|| y==13) &&(x<8 || x>8))
                {
                    result[x, y].SetActive(false);
                }
                    if((y==2 || y==12)&&(x<7|| x > 10))
                {
                    result[x, y].SetActive(false);
                }
                    if((y==3|| y == 11) && (x < 5 || x > 11))
                {
                   result[x, y].SetActive(false);
                }
                

            }
            if (scndRow)
            {
                scndRow = false;
            }else
            {
                scndRow = true;
            }

        }

        
        father.transform.position = new Vector3(-0.976f, 0.014f, -0.697f);
        father.transform.localScale = new Vector3(0.0666f, 0.0666f, 0.0666f);
        father.transform.parent = godfather.transform;
        return result;
    }

    public static GameObject[,] createLandscape(int size_X, int size_Y, double chance_Forest=0.15, double chance_Mountain=0.05, double chance_Oasis=0.01,double chanceboost=0.2)
    {
        GameObject[,] result = new GameObject[size_X, size_Y];
        GameObject father = GameObject.Find("tileHolder");
        bool scndRow = false;
        System.Random nRand = new System.Random();
        double randNum = nRand.NextDouble();
        if((chance_Forest<0 || chance_Mountain<0 || chance_Oasis < 0) || (chance_Oasis + chance_Mountain + chance_Forest) > 1)
        {
            return null;
        }
        double tempChance_Forest = chance_Forest;
        double tempChance_Mountain = chance_Mountain;
        double tempChance_Oasis = chance_Oasis;
        

        for (int y=0; y<size_Y; y++)
        {

            for(int x=0; x<size_X; x++)
            {
                result[x, y] = new GameObject("Tile: " + x.ToString() + " " + y.ToString());
                result[x, y].transform.parent = father.transform;
                Vector3 position = new Vector3();
                if (scndRow)
                {
                    position = new Vector3(0.866f + x * 1.732f, 0, y * 1.5f);
                }else
                {
                    position = new Vector3(x * 1.732f, 0, y * 1.5f);
                }

               
                Tile tempTile =  result[x, y].AddComponent<Tile>();
                randNum = nRand.NextDouble();
                //Ab hier erfolgt die Zuteilung des Chancesboost.
                if (x == 0 && y == 0)
                {
                    if (randNum < chance_Forest)
                    {
                        tempTile.tile_type = tileType.Forrest;
                    }else if(randNum < (chance_Forest + chance_Mountain))
                    {
                        tempTile.tile_type = tileType.Mountain;
                    }else if(randNum <(chance_Forest +chance_Mountain + chance_Oasis))
                    {
                        tempTile.tile_type = tileType.Oasis;
                    }else
                    {
                        tempTile.tile_type = tileType.Desert;
                    }

                }else if(x==0 && y != 0)
                {
                    Tile t1 = result[x, (y - 1)].GetComponent<Tile>();
                    if (scndRow)
                    {
                        Tile t2 = result[(x + 1), (y - 1)].GetComponent<Tile>();
                        if(t1.tile_type==tileType.Forrest || t2.tile_type == tileType.Forrest)
                        {
                            tempChance_Forest = chance_Forest + chanceboost;
                        }else
                        {
                            tempChance_Forest = chance_Forest;
                        }
                        if(t1.tile_type == tileType.Mountain || t2.tile_type == tileType.Mountain)
                        {
                            tempChance_Mountain = chance_Mountain + chanceboost;
                        }else
                        {
                            tempChance_Mountain = chance_Mountain;
                        }
                        if(t1.tile_type == tileType.Oasis || t2.tile_type == tileType.Oasis)
                        {
                            tempChance_Oasis = chance_Oasis;
                        }else
                        {
                            tempChance_Oasis = chance_Oasis;
                        }
                    }else
                    {
                        if (t1.tile_type == tileType.Forrest)
                        {
                            tempChance_Forest = chance_Forest + chanceboost;
                        }else
                        {
                            tempChance_Forest = chance_Forest;
                        }
                        if (t1.tile_type == tileType.Mountain)
                        {
                            tempChance_Mountain = chance_Mountain + chanceboost;
                        }
                        else
                        {
                            tempChance_Mountain = chance_Mountain;
                        }
                        if (t1.tile_type == tileType.Oasis)
                        {
                            tempChance_Oasis = chance_Oasis;
                        }
                        else
                        {
                            tempChance_Oasis = chance_Oasis;
                        }
                    }
                }else if(y==0 && x != 0)
                {
                    Tile t1 = result[(x-1), y].GetComponent<Tile>();
                    if(t1.tile_type == tileType.Forrest)
                    {
                        tempChance_Forest = chance_Forest + chanceboost;
                    }else
                    {
                        tempChance_Forest = chance_Forest;
                    }if(t1.tile_type == tileType.Mountain)
                    {
                        tempChance_Mountain = chance_Mountain + chanceboost;
                    }
                    else
                    {
                        tempChance_Mountain = chance_Mountain;
                    }
                    if (t1.tile_type == tileType.Oasis)
                    {
                        tempChance_Oasis = chance_Oasis;
                    }else
                    {
                        tempChance_Oasis = chance_Oasis;
                    }
                }
                else if(x!=(size_X-1) && y!=0)
                {
                    Tile t1 = result[(x - 1), y].GetComponent<Tile>();
                    Tile t2 = result[x, (y - 1)].GetComponent<Tile>();
                    Tile t3;
                    if (scndRow)
                    {

                        t3 = result[(x +1), (y - 1)].GetComponent<Tile>();
                    }else
                    {
                        t3 = result[(x -1), (y - 1)].GetComponent<Tile>();
                    }
                    if(t1.tile_type == tileType.Forrest || t2.tile_type == tileType.Forrest || t3.tile_type == tileType.Forrest)
                    {
                        tempChance_Forest = chance_Forest + chanceboost;
                    }else
                    {
                        tempChance_Forest = chance_Forest;
                    }
                    if (t1.tile_type == tileType.Mountain || t2.tile_type == tileType.Mountain || t3.tile_type == tileType.Mountain)
                    {
                        tempChance_Mountain = chance_Mountain + chanceboost;
                    }else
                    {
                        tempChance_Mountain = chance_Mountain;
                    }
                    if(t1.tile_type == tileType.Oasis || t2.tile_type == tileType.Oasis || t3.tile_type == tileType.Oasis)
                    {
                        tempChance_Oasis = chance_Oasis;
                    }else
                    {
                        tempChance_Oasis = chance_Oasis;
                    }
                } else 
                {
                    Tile t1 = result[(x - 1), y].GetComponent<Tile>();
                    Tile t2 = result[x, (y - 1)].GetComponent<Tile>();
                    
                    if (scndRow)
                    {

                        
                    }
                    else
                    {
                       
                    }
                    if (t1.tile_type == tileType.Forrest || t2.tile_type == tileType.Forrest)
                    {
                        tempChance_Forest = chance_Forest + chanceboost;
                    }
                    else
                    {
                        tempChance_Forest = chance_Forest;
                    }
                    if (t1.tile_type == tileType.Mountain || t2.tile_type == tileType.Mountain )
                    {
                        tempChance_Mountain = chance_Mountain + chanceboost;
                    }
                    else
                    {
                        tempChance_Mountain = chance_Mountain;
                    }
                    if (t1.tile_type == tileType.Oasis || t2.tile_type == tileType.Oasis )
                    {
                        tempChance_Oasis = chance_Oasis;
                    }
                    else
                    {
                        tempChance_Oasis = chance_Oasis;
                    }
                }
                if(x!=0 || y != 0)
                { //Zuweisung des Tiletypes
                    if (randNum < tempChance_Forest)
                    {
                        tempTile.tile_type = tileType.Forrest;
                    }
                    else if (randNum < (tempChance_Forest + tempChance_Mountain))
                    {
                        tempTile.tile_type = tileType.Mountain;
                    }
                    else if (randNum < (tempChance_Forest + tempChance_Mountain + tempChance_Oasis))
                    {
                        tempTile.tile_type = tileType.Oasis;
                    }
                    else
                    {
                        tempTile.tile_type = tileType.Desert;
                    }
                }

                GameObject undiscovered = (GameObject)Instantiate(Initialisation.undiscoveredTile);
                undiscovered.transform.name = "undiscovered";
                undiscovered.transform.parent = result[x, y].transform;
                undiscoveredTile ut = undiscovered.AddComponent<undiscoveredTile>();
                ut.position = new HexaPos(x, y);
                GameObject discovered;
                randNum = nRand.NextDouble();
                //Erstellung der jeweiligen Gameobjekte
                if (tempTile.tile_type == tileType.Desert)
                {
                    if (randNum < 0.33)
                    {
                        tempTile.genField = 1;
                        discovered = (GameObject)Instantiate(Initialisation.sandTile_1);
                    }
                    else if (randNum < 0.66)
                    {
                        tempTile.genField = 2;
                        discovered = (GameObject)Instantiate(Initialisation.sandTile_2);
                    }
                    else
                    {
                        tempTile.genField = 3;
                        discovered = (GameObject)Instantiate(Initialisation.sandTile_3);
                    }
                } else if (tempTile.tile_type == tileType.Forrest)
                {
                    if (randNum < 0.33)
                    {
                        tempTile.genField = 1;
                        discovered = (GameObject)Instantiate(Initialisation.forrestTile_1);
                    }
                    else if (randNum < 0.66)
                    {
                        tempTile.genField = 2;
                        discovered = (GameObject)Instantiate(Initialisation.forrestTile_2);
                    }
                    else
                    {
                        tempTile.genField = 3;
                        discovered = (GameObject)Instantiate(Initialisation.forrestTile_3);
                    }
                } else if(tempTile.tile_type == tileType.Mountain)
                {
                    if (randNum < 0.33)
                    {
                        tempTile.genField = 1;
                        discovered = (GameObject)Instantiate(Initialisation.mountainTile_1);
                    }
                    else if (randNum < 0.66)
                    {
                        tempTile.genField = 2;
                        discovered = (GameObject)Instantiate(Initialisation.mountainTile_2);
                    }
                    else
                    {
                        tempTile.genField = 3;
                        discovered = (GameObject)Instantiate(Initialisation.mountainTile_3);
                    }
                }
                else
                {
                    if (randNum < 0.33)
                    {
                        tempTile.genField = 1;
                        discovered = (GameObject)Instantiate(Initialisation.oasisTile_1);
                    }
                    else if (randNum < 0.66)
                    {
                        tempTile.genField = 2;
                        discovered = (GameObject)Instantiate(Initialisation.oasisTile_2);
                    }
                    else
                    {
                        tempTile.genField = 3;
                        discovered = (GameObject)Instantiate(Initialisation.oasisTile_3);
                    }
                }
                discoveredTile dT= discovered.AddComponent<discoveredTile>();
                dT.position = new HexaPos(x, y);
                undiscovered.AddComponent<MeshCollider>();
                discovered.AddComponent<MeshCollider>();
                discovered.transform.name = "actualTile";
                discovered.transform.parent = result[x, y].transform;
                discovered.SetActive(false);
                result[x, y].transform.position = position;
            }

            if (scndRow)
            {
                scndRow = false;
            }else
            {
                scndRow = true;
            }
            
        }
        
        landscape = result;
        return result;
    }
	
    public static List<HexaPos> tilesInRange(HexaPos hP, int range)
    {
        List<HexaPos> results = new List<HexaPos>();
       // int seitenl = range + 1;

        for (int i = 1; i <= range; i++)
        {


            HexaPos ecke1 = new HexaPos();
            HexaPos ecke2 = new HexaPos();
            HexaPos ecke3 = new HexaPos();
            HexaPos ecke4 = new HexaPos();
            HexaPos ecke5 = new HexaPos();
            HexaPos ecke6 = new HexaPos();
            ecke1.x = hP.x - i;
            ecke1.y = hP.y;
            ecke4.x = hP.x + i;
            ecke4.y = hP.y;
            if (hP.y % 2 == 0)
            {
                ecke2.x = hP.x - ((i + 1) / 2);
                ecke2.y = hP.y - i;
                ecke3.x = hP.x + (i / 2);
                ecke3.y = hP.y - i;
                ecke5.x = hP.x + (i / 2);
                ecke5.y = hP.y + i;
                ecke6.x = hP.x - ((i + 1) / 2);
                ecke6.y = hP.y + i;
            }
            else
            {
                
                ecke2.x = hP.x - (i / 2);
                ecke2.y = hP.y - i;
                ecke3.x = hP.x + (i / 2) + 1;
                ecke3.y = hP.y - i;
                ecke5.x = hP.x + (i / 2) + 1;
                ecke5.y = hP.y + i;
                ecke6.x = hP.x + (i / 2);
                ecke6.y = hP.y + i;
            }
            results.Add(ecke1);
            results.Add(ecke2);
            results.Add(ecke3);
            results.Add(ecke4);
            results.Add(ecke5);
            results.Add(ecke6);

            if (i > 1)
            {
                int placesToFill = i - 1;
                HexaPos tempEck1 = ecke1;
                HexaPos tempEck2 = ecke2;
                HexaPos tempEck3 = ecke3;
                HexaPos tempEck4 = ecke4;
                HexaPos tempEck5 = ecke5;
                HexaPos tempEck6 = ecke6;

                for (int z = 1; z <= placesToFill; z++)
                {
                    tempEck2.x = tempEck2.x + 1;
                    tempEck5.x = tempEck5.x - 1;

                    if (tempEck1.y % 2 == 0)
                    {
                        tempEck1.y = tempEck1.y - 1;
                    } else
                    {
                        tempEck1.x = tempEck1.x + 1;
                        tempEck1.y = tempEck1.y - 1;
                    }
                    if (tempEck3.y % 2 == 0)
                    {
                        tempEck3.y = tempEck3.y + 1;
                    }else
                    {
                        tempEck3.y = tempEck3.y + 1;
                        tempEck3.x = tempEck3.x + 1;
                    }
                    if (tempEck4.y % 2 == 0)
                    {
                        tempEck4.x = tempEck4.x - 1;
                        tempEck4.y = tempEck4.y + 1;
                    }else
                    {
                        tempEck4.y = tempEck4.y + 1;
                    }
                    if (tempEck6.y % 2 == 0)
                    {
                        tempEck6.x = tempEck6.x - 1;
                        tempEck6.y = tempEck6.y - 1;
                    }else
                    {
                        tempEck6.y = tempEck6.y - 1;
                    }

                    results.Add(tempEck1);
                    results.Add(tempEck2);
                    results.Add(tempEck3);
                    results.Add(tempEck4);
                    results.Add(tempEck5);
                    results.Add(tempEck6);
                }
                killTheNegative(results);
            }
        }

        return results;
    }

    private static void killTheNegative(List<HexaPos> li)
    {
        for(int i=0; i<li.Count; i++)
        {
            HexaPos l = li[i];
            if(l.x<0 || l.y < 0)
            {
                li.RemoveAt(i);
                killTheNegative(li);
                break;
            }
        }
    }

    public static void discoverAllTiles(bool st)
    {
        for(int j=0; j<landscape.GetLength(0); j++)
        {

            for(int i=0; i<landscape.GetLength(1); i++)
            {
                GameObject undiscov = landscape[j, i].transform.FindChild("undiscovered").gameObject;
                undiscov.SetActive(!st);
                GameObject actmap = landscape[j, i].transform.FindChild("actualTile").gameObject;
                actmap.SetActive(st);
            }

        }
    }

    public static void discover(HexaPos tile)
    {
        GameObject undiscov = landscape[tile.x, tile.y].transform.FindChild("undiscovered").gameObject;
        undiscov.SetActive(false);
        GameObject actmap = landscape[tile.x, tile.y].transform.FindChild("actualTile").gameObject;
        actmap.SetActive(true);
    }

    public static Vector3 MapTileToPosition(HexaPos h)
    {
        Vector3 result;
       
            float newY = h.y * 1.5f;
            float newX = 0;

            if (h.y % 2 == 0)
            {
                newX = h.x * (0.866f * 2);
            }
            else
            {
                newX = h.x * (0.866f * 2) + 0.866f;
            }
            result = new Vector3(newX, 0, newY);
        
        

        return result;
    }

    public static int distance(HexaPos pos_1, HexaPos pos_2)
    {
        bool notYethit = true;
        if(pos_1.x == pos_2.x && pos_1.y == pos_2.y)
        {
            return 0;
        }

        
        int distCounter = 0;
        HexaPos temp = pos_1;
        while (notYethit)
        {
            int xdist = Mathf.Abs(pos_2.x - temp.x);
            int ydist = Mathf.Abs(pos_2.y - temp.y);
            distCounter++;
            List<HexaPos> tilesAround = tilesInRange(temp, 1);
            //Check if Tile is around
            foreach(HexaPos h in tilesAround)
            {
                if (h == pos_2)
                {
                    return distCounter;
                }
            }
            //Check the best Tile Direction
            foreach(HexaPos h in tilesAround)
            {
                int distCurX = Mathf.Abs(pos_2.x - h.x);
                int distCurY = Mathf.Abs(pos_2.y - h.y);
                if(distCurX<xdist && distCurY < ydist)
                {
                    temp = h;
                    xdist = distCurX;
                    ydist = distCurY;
                    break;
                }
                else if(distCurX<xdist || distCurY < ydist)
                {
                    temp = h;
                    xdist = distCurX;
                    ydist = distCurY;
                }
            }
        }
        

        return  0;
    }

    public static void save(string filePath)
    {
        StringBuilder sb = new StringBuilder();
        for(int y=0; y<landscape.GetLength(1); y++)
        {
            string curLine = "";
            for(int x=0; x<landscape.GetLength(0); x++)
            {
                Tile t = landscape[x, y].GetComponent<Tile>();
                if (t.tile_type == tileType.Desert)
                {
                    curLine = curLine + "0;";
                }else if (t.tile_type == tileType.Forrest)
                {
                    curLine = curLine + "1;";
                } else if(t.tile_type == tileType.Mountain)
                {
                    curLine = curLine + "2;";
                } else if (t.tile_type == tileType.Oasis)
                {
                    curLine = curLine + "3;";
                }

            }
            sb.AppendLine(curLine);

        }
        File.WriteAllText(filePath, sb.ToString());
    }

    public static GameObject[,] load(string filePath)
    {
        var sizeY = 0;
        using (var reader = File.OpenText(filePath))
        {
            while (reader.ReadLine() != null)
            {
                sizeY++;
            }
        }
        var contentreader = new StreamReader(File.OpenRead(filePath));
        string line = contentreader.ReadLine();
        string[] values = line.Split(';');
        GameObject[,] result = new GameObject[values.Length, sizeY];
        GameObject father = GameObject.Find("tileHolder");

        bool scndRow = false;
        System.Random nRand = new System.Random();
        double randNum = nRand.NextDouble();
        
            

            for (int y=0; y<sizeY; y++)
            {

                for(int x=0; x<values.Length; x++)
                {
                    result[x, y] = new GameObject("Tile: " + x.ToString() + " " + y.ToString());
                    result[x, y].transform.parent = father.transform;
                    Vector3 position = new Vector3();
                    if (scndRow)
                    {
                        position = new Vector3(0.866f + x * 1.732f, 0, y * 1.5f);
                    }
                    else
                    {
                        position = new Vector3(x * 1.732f, 0, y * 1.5f);
                    }


                    Tile tempTile = result[x, y].AddComponent<Tile>();
                    int typeInt = Convert.ToInt32(values[x]);
                    switch (typeInt)
                    {
                        case 0: tempTile.tile_type = tileType.Desert;break;
                        case 1: tempTile.tile_type = tileType.Forrest;break;
                        case 2: tempTile.tile_type = tileType.Mountain;break;
                        case 3: tempTile.tile_type = tileType.Oasis;break;
                        default:break;
                    }

                    GameObject undiscovered = (GameObject)Instantiate(Initialisation.undiscoveredTile);
                    undiscovered.transform.name = "undiscovered";
                    undiscovered.transform.parent = result[x, y].transform;
                undiscoveredTile ut = undiscovered.AddComponent<undiscoveredTile>();
                ut.position = new HexaPos(x, y);
                    GameObject discovered;
                
                    randNum = nRand.NextDouble();
                    if (tempTile.tile_type == tileType.Desert)
                    {
                        if (randNum < 0.33)
                        {
                            tempTile.genField = 1;
                            discovered = (GameObject)Instantiate(Initialisation.sandTile_1);
                        }
                        else if (randNum < 0.66)
                        {
                            tempTile.genField = 2;
                            discovered = (GameObject)Instantiate(Initialisation.sandTile_2);
                        }
                        else
                        {
                            tempTile.genField = 3;
                            discovered = (GameObject)Instantiate(Initialisation.sandTile_3);
                        }
                    }
                    else if (tempTile.tile_type == tileType.Forrest)
                    {
                        if (randNum < 0.33)
                        {
                            tempTile.genField = 1;
                            discovered = (GameObject)Instantiate(Initialisation.forrestTile_1);
                        }
                        else if (randNum < 0.66)
                        {
                            tempTile.genField = 2;
                            discovered = (GameObject)Instantiate(Initialisation.forrestTile_2);
                        }
                        else
                        {
                            tempTile.genField = 3;
                            discovered = (GameObject)Instantiate(Initialisation.forrestTile_3);
                        }
                    }
                    else if (tempTile.tile_type == tileType.Mountain)
                    {
                        if (randNum < 0.33)
                        {
                            tempTile.genField = 1;
                            discovered = (GameObject)Instantiate(Initialisation.mountainTile_1);
                        }
                        else if (randNum < 0.66)
                        {
                            tempTile.genField = 2;
                            discovered = (GameObject)Instantiate(Initialisation.mountainTile_2);
                        }
                        else
                        {
                            tempTile.genField = 3;
                            discovered = (GameObject)Instantiate(Initialisation.mountainTile_3);
                        }
                    }
                    else
                    {
                        if (randNum < 0.33)
                        {
                            tempTile.genField = 1;
                            discovered = (GameObject)Instantiate(Initialisation.oasisTile_1);
                        }
                        else if (randNum < 0.66)
                        {
                            tempTile.genField = 2;
                            discovered = (GameObject)Instantiate(Initialisation.oasisTile_2);
                        }
                        else
                        {
                            tempTile.genField = 3;
                            discovered = (GameObject)Instantiate(Initialisation.oasisTile_3);
                        }
                    }
                    discoveredTile dT = discovered.AddComponent<discoveredTile>();
                    dT.position = new HexaPos(x, y);
                    discovered.transform.name = "actualTile";
                    discovered.transform.parent = result[x, y].transform;
                undiscovered.AddComponent<MeshCollider>();
                discovered.AddComponent<MeshCollider>();
                discovered.SetActive(false);
                result[x, y].transform.position = position;

                }
                line = contentreader.ReadLine();
                if(line != null)
            {
                values = line.Split(';');
            }
                if (scndRow)
                {
                    scndRow = false;
                }
                else
                {
                    scndRow = true;
                }

            }
        


        landscape = result;
        return result;
    }


    public static void setLookout(int amount)
    {
        HexaPos[] rndPos = new HexaPos[amount];
        System.Random rnd = new System.Random();

        for(int i=0; i<amount; i++)
        {
            int newX= rnd.Next(0, (landscape.GetLength(0) - 1));
            int newY = rnd.Next(0, (landscape.GetLength(1) - 1));
            rndPos[i] = new HexaPos(newX, newY);
            GameObject undiscov = landscape[newX, newY].transform.FindChild("undiscovered").gameObject;
            MeshRenderer mr = undiscov.GetComponent<MeshRenderer>();
            mr.material = Initialisation.lookOutMate;
            landscape[newX, newY].GetComponent<Tile>().special= specialTile.Lookout;
            //SpawnTowerLogic here
            GameObject actmap = landscape[newX,newY].transform.FindChild("actualTile").gameObject;
            GameObject seeingTower = (GameObject)Instantiate(Initialisation.lookOutTower);
            seeingTower.transform.position = actmap.transform.position;
            seeingTower.transform.Translate(new Vector3(0, 0.5f, 0));
            seeingTower.transform.parent = actmap.transform;
        }
        

    }

    public static void setLookout(HexaPos[] positions)
    {

        for(int i=0; i<positions.Length; i++)
        {

            GameObject undiscov = landscape[positions[i].x, positions[i].y].transform.FindChild("undiscovered").gameObject;
            MeshRenderer mr = undiscov.GetComponent<MeshRenderer>();
            mr.material = Initialisation.lookOutMate;

            //SpawnTowerLogic here
            GameObject actmap = landscape[positions[i].x, positions[i].y].transform.FindChild("actualTile").gameObject;
            GameObject seeingTower = (GameObject)Instantiate(Initialisation.lookOutTower);
            seeingTower.transform.position = actmap.transform.position;
            seeingTower.transform.Translate(new Vector3(0, 0.5f, 0));
            seeingTower.transform.parent = actmap.transform;
            landscape[positions[i].x, positions[i].y].GetComponent<Tile>().special = specialTile.Lookout;

        }
    }

    public static void setFinish(HexaPos position)
    {
        GameObject actmap = landscape[position.x, position.y].transform.FindChild("actualTile").gameObject;
        GameObject finishTile = (GameObject)Instantiate(Initialisation.finishTile);
        finishTile.transform.position = actmap.transform.position;
        finishTile.transform.localScale = actmap.transform.lossyScale;
        finishTile.transform.name = "actualTile";
        finishTile.transform.parent = actmap.transform.parent;
        discoveredTile dt = finishTile.AddComponent<discoveredTile>();
        dt.position = position;
        Destroy(actmap);
        finishTile.SetActive(false);
        landscape[position.x, position.y].GetComponent<Tile>().special = specialTile.Finish;
    }

    public static void setStaticHostiles(int amount)
    {

        System.Random rnd = new System.Random();

        for(int i=0; i<amount; i++)
        {
            int newX = rnd.Next(0, landscape.GetLength(0));
            int newY = rnd.Next(0, landscape.GetLength(1));
            GameObject actmap = landscape[newX, newY].transform.FindChild("actualTile").gameObject;
            GameObject seeingTower = (GameObject)Instantiate(Initialisation.staticHostile);
            seeingTower.transform.position = actmap.transform.position;
            seeingTower.transform.Translate(new Vector3(0, 0.5f, 0));
            seeingTower.transform.parent = actmap.transform;
            seeingTower.transform.name = "Hostile";
            landscape[newX, newY].GetComponent<Tile>().special = specialTile.Hostiles;

        }

    }

    public static void setStaticHostiles(HexaPos[] positions)
    {
        for(int i=0; i<positions.Length; i++)
        {
            //SpawnTowerLogic here
            GameObject actmap = landscape[positions[i].x, positions[i].y].transform.FindChild("actualTile").gameObject;
            GameObject seeingTower = (GameObject)Instantiate(Initialisation.staticHostile);
            seeingTower.transform.position = actmap.transform.position;
            seeingTower.transform.Translate(new Vector3(0, 0.5f, 0));
            seeingTower.transform.parent = actmap.transform;
            seeingTower.transform.name = "Hostile";
            landscape[positions[i].x, positions[i].y].GetComponent<Tile>().special = specialTile.Hostiles;
        }
    }

    public static void setActiveTile(HexaPos pos, HexaPos oldPos)
    {
        GameObject parent = landscape[oldPos.x, oldPos.y];
        if (pos != oldPos)
        {
            
            GameObject actmap = parent.transform.FindChild("actualTile").gameObject;
            Tile oldTile = parent.GetComponent<Tile>();
            GameObject restore;
            if (oldTile.tile_type == tileType.Desert)
            {
                if (oldTile.genField == 1)
                {
                    restore = (GameObject)Instantiate(Initialisation.sandTile_1);
                }
                else if (oldTile.genField == 2)
                {
                    restore = (GameObject)Instantiate(Initialisation.sandTile_2);
                }
                else
                {
                    restore = (GameObject)Instantiate(Initialisation.sandTile_3);
                }
            }
            else if (oldTile.tile_type == tileType.Forrest)
            {
                if (oldTile.genField == 1)
                {
                    restore = (GameObject)Instantiate(Initialisation.forrestTile_1);
                }
                else if (oldTile.genField == 2)
                {
                    restore = (GameObject)Instantiate(Initialisation.forrestTile_2);
                }
                else
                {
                    restore = (GameObject)Instantiate(Initialisation.forrestTile_3);
                }
            }
            else if (oldTile.tile_type == tileType.Mountain)
            {
                if (oldTile.genField == 1)
                {
                    restore = (GameObject)Instantiate(Initialisation.mountainTile_1);
                }
                else if (oldTile.genField == 2)
                {
                    restore = (GameObject)Instantiate(Initialisation.mountainTile_2);
                }
                else
                {
                    restore = (GameObject)Instantiate(Initialisation.mountainTile_3);
                }
            }
            else
            {
                if (oldTile.genField == 1)
                {
                    restore = (GameObject)Instantiate(Initialisation.oasisTile_1);
                }
                else if (oldTile.genField == 2)
                {
                    restore = (GameObject)Instantiate(Initialisation.oasisTile_2);
                }
                else
                {
                    restore = (GameObject)Instantiate(Initialisation.oasisTile_3);
                }
            }
            discoveredTile dt = restore.AddComponent<discoveredTile>();
            dt.position = oldPos;
            restore.AddComponent<MeshCollider>();
            restore.transform.position = actmap.transform.position;
            restore.transform.localScale = actmap.transform.lossyScale;
            restore.transform.parent = parent.transform;
            restore.transform.name = "actualTile";
            Destroy(actmap);
        }
        //SET new active Tile here
        parent = landscape[pos.x, pos.y];
        GameObject newTile =  parent.transform.FindChild("actualTile").gameObject;
        Tile oldTileq = parent.GetComponent<Tile>();
        GameObject activeTile;
        if(oldTileq.tile_type== tileType.Desert)
        {
            activeTile = (GameObject)Instantiate(Initialisation.sandTile_active);
        }
        else if (oldTileq.tile_type == tileType.Forrest)
        {
            activeTile = (GameObject)Instantiate(Initialisation.forrestTile_active);
        }
        else if(oldTileq.tile_type == tileType.Mountain)
        {
            activeTile = (GameObject)Instantiate(Initialisation.mountainTile_active);
        }
        else
        {
            activeTile = (GameObject)Instantiate(Initialisation.oasisTile_active);
        }
        activeTile dti = activeTile.AddComponent<activeTile>();
        dti.position = pos;
        activeTile.AddComponent<MeshCollider>();
        activeTile.transform.position = newTile.transform.position;
        activeTile.transform.localScale = newTile.transform.lossyScale;
        activeTile.transform.parent = parent.transform;
        activeTile.transform.name = "actualTile";
        Destroy(newTile);
    }

    public static int getDirection(HexaPos pos, HexaPos tgtPos)
    {
        List<HexaPos> direction = tilesInRange(pos, 1);
       
        int t = 999999;
        int dir = -1;
        for(int i=0; i<direction.Count; i++)
        {
            if(distance(direction[i], tgtPos)< t)
            {
                
                t = distance(direction[i], tgtPos);
                dir = i;
            }
        }


        return dir;
    }

    public static List<HexaPos> GetFieldsOfType(tileType type)
    {
        List<HexaPos> result = new List<HexaPos>();

        for(int y=0; y<landscape.GetLength(1); y++)
        {

            for(int x=0; x<landscape.GetLength(0); x++)
            {
                Tile t = landscape[x, y].GetComponent<Tile>();
                if (t.tile_type == type)
                {
                    HexaPos temp = new HexaPos(x, y);
                    result.Add(temp);
                }
            }

        }

        return result;
    }

    public static List<Tile> GetFieldsOfType_t(tileType type)
    {
        List<Tile> result = new List<Tile>();
        for (int y = 0; y < landscape.GetLength(1); y++)
        {

            for (int x = 0; x < landscape.GetLength(0); x++)
            {
                Tile t = landscape[x, y].GetComponent<Tile>();
                if (t.tile_type == type)
                {
                    result.Add(t);
                }
            }

        }

        return result;

    }

    public static List<GameObject> GetFieldsOfType_g(tileType type)
    {
        List<GameObject> result = new List<GameObject>();
        for (int y = 0; y < landscape.GetLength(1); y++)
        {

            for (int x = 0; x < landscape.GetLength(0); x++)
            {
                Tile t = landscape[x, y].GetComponent<Tile>();
                if (t.tile_type == type)
                {
                    result.Add(landscape[x, y]);
                }
            }

        }

        return result;
    }

    public static List<HexaPos> GetFieldsHidden()
    {
        List<HexaPos> result = new List<HexaPos>();
        for(int y=0; y<landscape.GetLength(1); y++)
        {
            for(int x=0; x<landscape.GetLength(0); x++)
            {
                GameObject discoveredGO = landscape[x,y].transform.FindChild("actualTile").gameObject;
                if (!discoveredGO.activeInHierarchy)
                {
                    HexaPos n = new HexaPos(x, y);
                    result.Add(n);
                }
            }
        }
        return result;
    }

    public static List<HexaPos> GetFieldsInRadius(int radius)
    {
        List<HexaPos> result = tilesInRange(SceneHandler.caravanPosition, radius);
        return result;
    }

    public static void FieldChangeType(HexaPos pos, tileType type)
    {
        GameObject tileToReplace = landscape[pos.x,pos.y].transform.FindChild("actualTile").gameObject;
        System.Random rand = new System.Random();
        double ntile = rand.NextDouble();
        GameObject newTile;
        if(type == tileType.Desert)
        {
            Tile t = landscape[pos.x, pos.y].GetComponent<Tile>();
            t.tile_type = tileType.Desert;

            if (ntile < 0.33)
            {
                newTile = (GameObject)Instantiate(Initialisation.sandTile_1);
                t.genField = 1;
            }
            else if(ntile < 0.66)
            {
                newTile = (GameObject)Instantiate(Initialisation.sandTile_2);
                t.genField = 2;
            }
            else
            {
                newTile = (GameObject)Instantiate(Initialisation.sandTile_3);
                t.genField = 3;
            }
            

        }
        else if(type == tileType.Forrest)
        {
            Tile t = landscape[pos.x, pos.y].GetComponent<Tile>();
            t.tile_type = tileType.Forrest;

            if (ntile < 0.33)
            {
                newTile = (GameObject)Instantiate(Initialisation.forrestTile_1);
                t.genField = 1;
            }
            else if (ntile < 0.66)
            {
                newTile = (GameObject)Instantiate(Initialisation.forrestTile_2);
                t.genField = 2;
            }
            else
            {
                newTile = (GameObject)Instantiate(Initialisation.forrestTile_3);
                t.genField = 3;
            }
        }
        else if(type == tileType.Mountain)
        {
            Tile t = landscape[pos.x, pos.y].GetComponent<Tile>();
            t.tile_type = tileType.Mountain;

            if (ntile < 0.33)
            {
                newTile = (GameObject)Instantiate(Initialisation.mountainTile_1);
                t.genField = 1;
            }
            else if (ntile < 0.66)
            {
                newTile = (GameObject)Instantiate(Initialisation.mountainTile_2);
                t.genField = 2;
            }
            else
            {
                newTile = (GameObject)Instantiate(Initialisation.mountainTile_3);
                t.genField = 3;
            }
        }
        else
        {
            Tile t = landscape[pos.x, pos.y].GetComponent<Tile>();
            t.tile_type = tileType.Oasis;

            if (ntile < 0.33)
            {
                newTile = (GameObject)Instantiate(Initialisation.oasisTile_1);
                t.genField = 1;
            }
            else if (ntile < 0.66)
            {
                newTile = (GameObject)Instantiate(Initialisation.oasisTile_2);
                t.genField = 2;
            }
            else
            {
                newTile = (GameObject)Instantiate(Initialisation.oasisTile_3);
                t.genField = 3;
            }
        }

        newTile.transform.position = tileToReplace.transform.position;
        newTile.transform.localScale = tileToReplace.transform.lossyScale;
        newTile.transform.parent = tileToReplace.transform.parent;
        if (!tileToReplace.activeInHierarchy)
        {
            newTile.SetActive(false);
        }
        Destroy(tileToReplace);
    }

    public static HexaPos getPositionDirectionalByDistance(HexaPos _originalPos, int _dir, int _dist)
    {
        switch (_dir)
        {
            case 0:
                _originalPos.x -= _dist;
                return _originalPos;
            case 1:
                _originalPos.y -= _dist;
                return _originalPos;
            case 2:
                _originalPos.x += _dist;
                _originalPos.y -= _dist;
                return _originalPos;
            case 3:
                _originalPos.x += _dist;
                return _originalPos;
            case 4:
                _originalPos.y += _dist;
                return _originalPos;
            case 5:
                _originalPos.x -= _dist;
                _originalPos.y += _dist;
                return _originalPos;
        }

        return new HexaPos(0,0);
    }

    public static void highlightAllInnerTiles(bool _isHighlighted)
    {
        foreach (GameObject tile in SceneHandler.smallMap)
        {
            tile.GetComponent<innerTile>().setHighlighted(_isHighlighted);
        }
    }

}
