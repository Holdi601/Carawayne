using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HuntingGround : TacticalGame
{

    private List<HexaPos> animalsPos;

    public HuntingGround(List<HexaPos> _posList)
    {
        animalsPos = _posList;
    }

    public void init()
    {
        //Todo: Disable all Copmanion Gameobjects which are note required

        foreach (HexaPos pos in animalsPos)
        {
            SceneHandler.createMeeple<Opponent>("Opponent", pos);
        }
    }

    public override void opponentsTurn()
    {
        List<HuntedAnimal> huntedAnimals = SceneHandler.getAllMeeplesFromType<HuntedAnimal>();

        //Todo: Instant execution. Should be successively according to animation
        foreach (HuntedAnimal huntedAnimal in huntedAnimals)
        {
            //Todo: Find direction towards exit
            //Todo: Move Towards exit
            //huntedAnimal.moveTo();

            //placeholder
            huntedAnimal.hasEscaped = true;
        }
    }

    //Player wins this tactical game if there are no more huntedAnimals on the tactical grid.
    public override bool isTacticalGameWon()
    {
        List<HuntedAnimal> huntedAnimals = SceneHandler.getAllMeeplesFromType<HuntedAnimal>();

        if (huntedAnimals == null || huntedAnimals.Count == 0)
        {
            return true;
        }

        return false;
    }

    //Player loses this tactical game if all hunted Animals escaped
    public override bool isTacticalGameLost()
    {
        List<HuntedAnimal> huntedAnimals = SceneHandler.getAllMeeplesFromType<HuntedAnimal>();

        foreach (HuntedAnimal huntedAnimal in huntedAnimals)
        {
            if (!huntedAnimal.hasEscaped)
            {
                return false;
            }
        }

        return true;
    }

    public override bool hasPlayerAvailableMoves()
    {
        List<Hunter> hunters = new List<Hunter>();

        //Player has available moves left if at least one of his mercenaries action outstanding
        foreach (Hunter hunter in hunters)
        {
            if (hunter.hasActionOutstanding)
            {
                return true;
            }
        }

        return false;
    }

}
