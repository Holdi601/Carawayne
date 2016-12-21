using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public enum TileSpace { Small, Large};

public struct hexaPos
{
    public int x;
    public int y;

    public static bool operator ==(hexaPos p1, hexaPos p2)
    {
        if(p1.x==p2.x && p1.y == p2.y)
        {
            return true;
        }else
        {
            return false;
        }
    }

    public static bool operator !=(hexaPos p1, hexaPos p2)
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
        GameObject father = new GameObject("Father");
        bool scndRow = false;

        for(int y=0; y<15; y++)
        {

            for(int x=0; x<15; x++)
            {
                result[x, y] = (GameObject)Instantiate(Initialisation.innerTile);
                result[x, y].transform.name = "innerTile: " + x.ToString() + " " + y.ToString();
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
                GameObject discovered;
                randNum = nRand.NextDouble();
                //Erstellung der jeweiligen Gameobjekte
                if (tempTile.tile_type == tileType.Desert)
                {
                    if (randNum < 0.33)
                    {
                        discovered = (GameObject)Instantiate(Initialisation.sandTile_1);
                    }
                    else if (randNum < 0.66)
                    {
                        discovered = (GameObject)Instantiate(Initialisation.sandTile_2);
                    }
                    else
                    {
                        discovered = (GameObject)Instantiate(Initialisation.sandTile_3);
                    }
                } else if (tempTile.tile_type == tileType.Forrest)
                {
                    if (randNum < 0.33)
                    {
                        discovered = (GameObject)Instantiate(Initialisation.forrestTile_1);
                    }
                    else if (randNum < 0.66)
                    {
                        discovered = (GameObject)Instantiate(Initialisation.forrestTile_2);
                    }
                    else
                    {
                        discovered = (GameObject)Instantiate(Initialisation.forrestTile_3);
                    }
                } else if(tempTile.tile_type == tileType.Mountain)
                {
                    if (randNum < 0.33)
                    {
                        discovered = (GameObject)Instantiate(Initialisation.mountainTile_1);
                    }
                    else if (randNum < 0.66)
                    {
                        discovered = (GameObject)Instantiate(Initialisation.mountainTile_2);
                    }
                    else
                    {
                        discovered = (GameObject)Instantiate(Initialisation.mountainTile_3);
                    }
                }
                else
                {
                    if (randNum < 0.33)
                    {
                        discovered = (GameObject)Instantiate(Initialisation.oasisTile_1);
                    }
                    else if (randNum < 0.66)
                    {
                        discovered = (GameObject)Instantiate(Initialisation.oasisTile_2);
                    }
                    else
                    {
                        discovered = (GameObject)Instantiate(Initialisation.oasisTile_3);
                    }
                }
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
	
    public static List<hexaPos> tilesInRange(hexaPos hP, int range)
    {
        List<hexaPos> results = new List<hexaPos>();
        int seitenl = range + 1;

        for (int i = 1; i <= range; i++)
        {


            hexaPos ecke1 = new hexaPos();
            hexaPos ecke2 = new hexaPos();
            hexaPos ecke3 = new hexaPos();
            hexaPos ecke4 = new hexaPos();
            hexaPos ecke5 = new hexaPos();
            hexaPos ecke6 = new hexaPos();
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
                hexaPos tempEck1 = ecke1;
                hexaPos tempEck2 = ecke2;
                hexaPos tempEck3 = ecke3;
                hexaPos tempEck4 = ecke4;
                hexaPos tempEck5 = ecke5;
                hexaPos tempEck6 = ecke6;

                for (int z = 1; z <= placesToFill; z++)
                {
                    tempEck2.x = tempEck2.y + 1;
                    tempEck5.x = tempEck5.y - 1;

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

    private static void killTheNegative(List<hexaPos> li)
    {
        for(int i=0; i<li.Count; i++)
        {
            hexaPos l = li[i];
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

    public static Vector3 MapTileToPosition(int x, int y, TileSpace space)
    {
        Vector3 result;
        if(space== TileSpace.Large)
        {
            float newY = y * 1.5f;
            float newX = 0;

            if (y % 2 == 0)
            {
                newX = x * (0.866f * 2);
            }
            else
            {
                newX = x * (0.866f * 2) + 0.866f;
            }
            result = new Vector3(newX, 0, newY);
        }
        else
        {
            result = new Vector3();
        }
        

        return result;
    }

    public static int distance(hexaPos pos_1, hexaPos pos_2)
    {
        bool notYethit = true;
        if(pos_1.x == pos_2.x && pos_1.y == pos_2.y)
        {
            return 0;
        }

        
        int distCounter = 0;
        hexaPos temp = pos_1;
        while (notYethit)
        {
            int xdist = Mathf.Abs(pos_2.x - temp.x);
            int ydist = Mathf.Abs(pos_2.y - temp.y);
            distCounter++;
            List<hexaPos> tilesAround = tilesInRange(temp, 1);
            //Check if Tile is around
            foreach(hexaPos h in tilesAround)
            {
                if (h == pos_2)
                {
                    return distCounter;
                }
            }
            //Check the best Tile Direction
            foreach(hexaPos h in tilesAround)
            {
                int distCurX = Mathf.Abs(pos_2.x - h.x);
                int distCurY = Mathf.Abs(pos_2.y - h.y);
               // if(distCurX<xdist || distCurY<)
            }
        }
        

        return  0;
    }

}
