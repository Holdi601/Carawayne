using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Prince : Companion {
    public bool goVIPused = false;
    public bool goFeastused = false;

    void Awake()
    {
        
        proviantDemand = 3;
        proviantDemandMax =3;
        strength = 10;
        strengthMax = 10;
        walkRange = 2;
        setFoodPackages_hpBar();
    }

}
