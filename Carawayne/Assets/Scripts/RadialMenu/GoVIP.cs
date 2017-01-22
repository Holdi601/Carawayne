using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoVIP : MonoBehaviour {

    void OnMouseDown()
    {
        
        List<Prince> allPrince = SceneHandler.getAllMeeplesFromType<Prince>();
        if (!allPrince[0].goVIPused)
        {
            List<Healer> allHeal = SceneHandler.getAllMeeplesFromType<Healer>();
            List<Hunter> allHunter = SceneHandler.getAllMeeplesFromType<Hunter>();
            List<Mercenary> allMerc = SceneHandler.getAllMeeplesFromType<Mercenary>();
            List<Merchant> allMerch = SceneHandler.getAllMeeplesFromType<Merchant>();

            List<Scout> allScout = SceneHandler.getAllMeeplesFromType<Scout>();
            foreach (Healer h in allHeal)
            {
                h.Strength += 2;
            }
            foreach (Hunter h in allHunter)
            {
                h.Strength += 2;
            }
            foreach (Mercenary h in allMerc)
            {
                h.Strength += 2;
            }
            foreach (Merchant h in allMerch)
            {
                h.Strength += 2;
            }
            foreach (Prince h in allPrince)
            {
                h.Strength += 2;
            }
            foreach (Scout h in allScout)
            {
                h.Strength += 2;
            }
            allPrince[0].goVIPused = true;
            
        }
        ClickWorker.destroyRadialMenu();
    }
}
