using UnityEngine;
using System.Collections;

public class Initialisation : MonoBehaviour {
    //Enter the prefabs in Editor for the Tiles here
    public GameObject sandTile_1_prefab;
    public GameObject sandTile_2_prefab;
    public GameObject sandTile_3_prefab;
    public GameObject sandTile_active_prefab;

    public GameObject forrestTile_1_prefab;
    public GameObject forrestTile_2_prefab;
    public GameObject forrestTile_3_prefab;
    public GameObject forrestTile_active_prefab;

    public GameObject mountainTile_1_prefab;
    public GameObject mountainTile_2_prefab;
    public GameObject mountainTile_3_prefab;
    public GameObject mountainTile_active_prefab;

    public GameObject oasisTile_1_prefab;
    public GameObject oasisTile_2_prefab;
    public GameObject oasisTile_3_prefab;
    public GameObject oasisTile_active_prefab;

    public GameObject undiscoveredTile_prefab;
    public GameObject finishTile_prefab;
    public GameObject innerTile_prefab;
    public GameObject soldier_prefab;
    public GameObject worker_prefab;
    public GameObject king_prefab;
    public GameObject healer_prefab;
    public GameObject scout_prefab;
    public GameObject lookOutTower_prefab;
    public GameObject defensiveAnimal_prefab;
    public GameObject aggressiveAnimal_prefab;
    public GameObject raider_prefab;
    public GameObject camel_prefab;
    public GameObject food_prefab;
    public GameObject staticHostile_prefab;
    public GameObject planeSample_prefab;

    public Material highlightMat;
    public Material lookOutMat;
    public Material transParent_Prefab;

    //Shared prefabs for the map Creator e.g.
    public static GameObject sandTile_1;
    public static GameObject sandTile_2;
    public static GameObject sandTile_3;
    public static GameObject sandTile_active;
           
    public static GameObject forrestTile_1;
    public static GameObject forrestTile_2;
    public static GameObject forrestTile_3;
    public static GameObject forrestTile_active;
            
    public static GameObject mountainTile_1;
    public static GameObject mountainTile_2;
    public static GameObject mountainTile_3;
    public static GameObject mountainTile_active;
           
    public static GameObject oasisTile_1;
    public static GameObject oasisTile_2;
    public static GameObject oasisTile_3;
    public static GameObject oasisTile_active;
            
    public static GameObject undiscoveredTile;
    public static GameObject specialundiscoveredTile;
    public static GameObject innerTile;
    public static GameObject finishTile;

    public static Material highlightMate;
    public static Material lookOutMate;
    public static Material transparentMat;

    public static GameObject soldier;
    public static GameObject healer;
    public static GameObject king;
    public static GameObject worker;
    public static GameObject scout;
    public static GameObject defensiveAnimal;
    public static GameObject aggressiveAnimal;
    public static GameObject raider;
    public static GameObject camel;
    public static GameObject food;
    public static GameObject lookOutTower;
    public static GameObject staticHostile;

    public static GameObject planeSample;

    public static GameObject mapGO, innerTileHolderGO, tileHolderGo;

    // Use this for initialization
    void Awake () {
        sandTile_1 = sandTile_1_prefab;
        sandTile_2 = sandTile_2_prefab;
        sandTile_3 = sandTile_3_prefab;
        forrestTile_1 = forrestTile_1_prefab;
        forrestTile_2 = forrestTile_2_prefab;
        forrestTile_3 = forrestTile_3_prefab;
        mountainTile_1 = mountainTile_1_prefab;
        mountainTile_2 = mountainTile_2_prefab;
        mountainTile_3 = mountainTile_3_prefab;
        oasisTile_1 = oasisTile_1_prefab;
        oasisTile_2 = oasisTile_2_prefab;
        oasisTile_3 = oasisTile_3_prefab;
        undiscoveredTile = undiscoveredTile_prefab;
        highlightMate = highlightMat;
        innerTile = innerTile_prefab;
        king = king_prefab;
        soldier = soldier_prefab;
        worker = worker_prefab;
        healer = healer_prefab;
        defensiveAnimal = defensiveAnimal_prefab;
        aggressiveAnimal = aggressiveAnimal_prefab;
        raider = raider_prefab;
        camel = camel_prefab;
        food = food_prefab;
        mapGO = GameObject.Find("Map");
        innerTileHolderGO = GameObject.Find("innerTileHolder");
        tileHolderGo = GameObject.Find("tileHolder");
        innerTileHolderGO.transform.parent = mapGO.transform;
        tileHolderGo.transform.parent = mapGO.transform;
        sandTile_active = sandTile_active_prefab;
        forrestTile_active = forrestTile_active_prefab;
        oasisTile_active = oasisTile_active_prefab;
        mountainTile_active = mountainTile_active_prefab;
        lookOutMate = lookOutMat;
        lookOutTower = lookOutTower_prefab;
        finishTile = finishTile_prefab;
        staticHostile = staticHostile_prefab;
        planeSample = planeSample_prefab;
        transparentMat = transParent_Prefab;

    }
	
	
}
