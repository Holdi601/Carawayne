using UnityEngine;
using System.Collections;

public class SceneHandler : MonoBehaviour
{

    public int turn = 0;
    public Caravan playerCaravan;

    void Update()
    {
    }

    public void endTurn()
    {
        turn++;
        playerCaravan.consumeProviant(playerCaravan.foodUptakePerRound);
    }

}
