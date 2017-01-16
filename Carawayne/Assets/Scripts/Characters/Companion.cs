using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Companion : Meeple {

    public int proviantDemand;
    public int ProviantDemandMax;
   
    private int _Strength;
    public int strength
    {
        get
        {
            return _Strength;
        }
        set
        {
            _Strength = value;

            if (hpbar != null)
            {
                updateHPbar();
            }
            

        }
    }
    public int strengthMax;
    private float proviantRation;
    public bool hasActionOutstanding;

    //Reinhold
    private GameObject[] foodObjects;
    private GameObject hpbar;
    private Texture2D hpbar_content;
    public int proviantDemandMax
    {
        get
        {
            return ProviantDemandMax;
        }
        set
        {
            ProviantDemandMax = value;
            if (foodObjects != null)
            {
                for(int i=0; i<foodObjects.Length; i++)
                {
                    if (foodObjects[i] != null)
                    {
                        Destroy(foodObjects[i]);
                    }
                }
            }
            foodObjects = new GameObject[value];
            for(int i=0; i<value; i++)
            {
                foodObjects[i] = (GameObject)Instantiate(Initialisation.food);
                foodObjects[i].transform.name = "FoodPack_" + i.ToString();
            }

        }
    }

    void updateHPbar()
    {
        int percentage = Convert.ToInt16(((float)_Strength / (float)strengthMax) * 100f);
        
        for (int i = 0; i < 100; i++)
        {
            if (i < percentage)
            {
                hpbar_content.SetPixel(i + 1, 1, new Color(0f, 1f, 0f));
            }
            else
            {
                hpbar_content.SetPixel(i + 1, 1, new Color(1f, 0f, 0f));
            }
        }
        hpbar_content.Apply();
        MeshRenderer mr = hpbar.GetComponent<MeshRenderer>();
        Material m = mr.material;
        m.SetTexture("_MainTex", hpbar_content);
        mr.material = m;
    }


    public void setFoodPackages_hpBar (){

        if (meepleName != null)
        {
            hpbar_content = new Texture2D(100, 1);
            hpbar = (GameObject)Instantiate(Initialisation.hpbar);
            Material m = (Material)Instantiate(Initialisation.texture_Material);
            MeshRenderer mr = hpbar.GetComponent<MeshRenderer>();
            mr.material = m;
            SceneHandler.positionAndParent_HP(gameObject, hpbar);
            hpbar.transform.localScale = hpbar.transform.localScale * 0.4f;
            

            for (int i = 0; i < foodObjects.Length; i++)
            {


                SceneHandler.positionAndParent_Food(gameObject, foodObjects[i]);
                foodObjects[i].transform.Translate(new Vector3(0.3f, 0, 0));
                foodObjects[i].transform.RotateAround(foodObjects[i].transform.parent.transform.position, new Vector3(0, 1, 0), (i * 30));
                foodObjects[i].transform.localScale = foodObjects[i].transform.localScale * 0.1f;
                foodObjects[i].transform.Translate(new Vector3(0, 0.0258f, 0), Space.Self);


            }
            updateHPbar();
        }

        
    }

    //public Companion(HexaPos _pos, string _name, int _proviantDemand, int _strength): base(_pos, _name)
    //{
    //    proviantDemand = _proviantDemand;
    //    strength = _strength;
    //    strengthMax = _strength;
    //    proviantDemandMax = _proviantDemand;
    //    ProviantRation = 1.0f;
    //    hasActionOutstanding = true;
    //}

   void Awake()
    {
        ProviantRation = 1.0f;
        hasActionOutstanding = true;
        walkRange = 1;

        
    }

    public void moveTo(HexaPos _pos)
    {
        //Todo: Missing implementation movement
        hasActionOutstanding = false;
    }

    public void doAction(/*Action _action*/)
    {
        //Todo: Implement if decide to generic action instead of individual subClass actions (fight, heal, etc...)
        //Todo: OnPlayerInteractionEnded trigger for tactical game. Raise Event OnPlayerInteraction
        hasActionOutstanding = false;
    }

    //Consume a proviant ration
    public int consumeRation()
    {
        //Loose strength depending on difference between ProviantDemand and ProviantDemandMax
        Strength -= (proviantDemandMax - proviantDemand);

        //Return consumed proviant
        return proviantDemand;
    }

    public void damaged(int _amount)
    {
        //If companion already on 0 aka "bewusstlos". --> Kill companion.
        if (strength <= 0 && _amount > 0)
        {
            Alive = false;
        }
        else
        {
            Strength -= _amount;
        }
    }

    //GETTER & SETTER
    //---------------
    public int Strength
    {
        get { return strength; }
        set {
            strength = Math.Max(value, 0);
            strength = Math.Min(value, strengthMax);
        }
    }

    public float ProviantRation
    {
        get { return proviantRation; }
        set {
            proviantRation = Math.Max(value, 0);
            proviantRation = Math.Min(value, 2);
            //If ProviantRation changes --> change proviantDemand
            proviantDemand = (int)(proviantDemand * proviantRation);

           

        }
    }

    public int ProviantDemand
    {
        get { return proviantDemand; }
        set
        {
            proviantDemand = Math.Max(value, 0);
            proviantDemand = Math.Min(value, proviantDemandMax);

           
               
                for(int i=0; i<foodObjects.Length; i++)
                {
                    if (i < proviantDemand)
                    {
                        foodObjects[i].SetActive(true);
                    }else
                    {
                        foodObjects[i].SetActive(false);
                    }
                }
            


        }
    }

    void OnMouseDown()
    {
        SceneHandler.activeCompanion = this;
        List<HexaPos> walkableTilesPos = new List<HexaPos>();
        walkableTilesPos = Map.tilesInRange(SceneHandler.activeCompanion.Pos, SceneHandler.activeCompanion.walkRange);

        Map.highlightAllInnerTiles(false);

        foreach (HexaPos tilePos in walkableTilesPos)
        {
            if (tilePos.x < 15 && tilePos.x > -1 && tilePos.y < 15 && tilePos.y > -1)
            {
                SceneHandler.smallMap[tilePos.x, tilePos.y].GetComponent<innerTile>().setHighlighted(true);
            }
            
        }
    }
}
