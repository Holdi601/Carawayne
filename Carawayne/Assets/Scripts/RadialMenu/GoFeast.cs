using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoFeast : MonoBehaviour {

    void OnMouseDown()
    {
        List<Prince> allPrince = SceneHandler.getAllMeeplesFromType<Prince>();
        if (!allPrince[0].goFeastused)
        {
            List<Companion> allComp = SceneHandler.getAllMeeplesFromType<Companion>();
            List<PackAnimal> allPack = SceneHandler.getAllMeeplesFromType<PackAnimal>();
            int CompCount = allComp.Count - allPack.Count;
            int amountFood = CompCount * 3;

            foreach(PackAnimal p in allPack)
            {
                int tempMount = p.loadCapacity - p.ActualLoad;
                if (tempMount > 0)
                {
                    if (tempMount >= amountFood)
                    {
                        p.ActualLoad += amountFood;
                        break;
                    }
                    else
                    {
                        int amountToFill = amountFood - tempMount;
                        p.ActualLoad += amountToFill;
                        amountFood = amountFood - tempMount; 
                    }
                }
            }

            allPrince[0].goFeastused = true;
        }
        ClickWorker.destroyRadialMenu();
    }
}
