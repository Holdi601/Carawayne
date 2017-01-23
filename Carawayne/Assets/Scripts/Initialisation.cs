using UnityEngine;
using System.Collections;

public class Initialisation : MonoBehaviour
{
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
    public GameObject planeSample_prefab;

    public Material highlightMat;
    public Material innerTileActiveMat;
    public Material innerTileMat;
    public Material lookOutMat;
    public Material transParent_Prefab;
    public Material activeAgentTileMat;
    public Material texture_Material_prefab;

    public GameObject hpbar_prefab;
    public GameObject prefab_soldier;
    public GameObject prefab_healer;
    public GameObject prefab_king;
    public GameObject prefab_worker;
    public GameObject prefab_scout;
    public GameObject prefab_raider;
    public GameObject prefab_camel;
    public GameObject prefab_food;
    public GameObject prefab_skull;
    public GameObject prefab_lookOutTower;
    public GameObject prefab_staticHostile;
    public GameObject prefab_aligator;
    public GameObject prefab_antilope;
    public GameObject prefab_jackal;
    public GameObject prefab_sandworm;
    public GameObject prefab_sandshark;
    public GameObject prefab_gollok;

    public Sprite prefab_sprt;
   

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
    public static Material innerTileActiveMate;
    public static Material innerTileMate;
    public static Material activeAgentTileMaterial;
    public static Material lookOutMate;
    public static Material transparentMat;

    public static GameObject soldier;
    public static GameObject healer;
    public static GameObject king;
    public static GameObject worker;
    public static GameObject scout;
    public static GameObject raider;
    public static GameObject camel;
    public static GameObject food;
    public static GameObject skull;
    public static GameObject lookOutTower;
    public static GameObject staticHostile;
    public static GameObject aligator;
    public static GameObject antilope;
    public static GameObject jackal;
    public static GameObject sandworm;
    public static GameObject sandshark;
    public static GameObject gollok;
    public static GameObject planeSample;


    public static GameObject hpbar;
    public static Material texture_Material;
    public static GameObject mapGO, innerTileHolderGO, tileHolderGo;
    public static Sprite sprt;

    // Use this for initialization

    void Awake()
    {
        sprt = prefab_sprt;
        skull = prefab_skull;
        hpbar = hpbar_prefab;
        texture_Material = texture_Material_prefab;
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
        finishTile = finishTile_prefab;
        planeSample = planeSample_prefab;
        transparentMat = transParent_Prefab;

        innerTileMate = innerTileMat;
        innerTileActiveMate = innerTileActiveMat;
        activeAgentTileMaterial = activeAgentTileMat;

        soldier = prefab_soldier;
        healer = prefab_healer;
        king = prefab_king;
        worker = prefab_worker;
        scout = prefab_scout;
  
        raider = prefab_raider;
        camel = prefab_camel;
        food = prefab_food;
        lookOutTower = prefab_lookOutTower;
        staticHostile = prefab_staticHostile;
        antilope = prefab_antilope;



      
        
    }


    void Update()
    {

    }
}
