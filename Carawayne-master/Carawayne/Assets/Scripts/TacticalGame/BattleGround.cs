using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleGround : TacticalGame
{

    private List<HexaPos> opponentsPos; 

    public BattleGround(List<HexaPos> _posList)
    {
        opponentsPos = _posList;
    }

    override public void init()
    {
        base.init();
        
        //Todo: Disable all Copmanion Gameobjects which are note required

        foreach (HexaPos pos in opponentsPos)
        {
            SceneHandler.createMeeple<Opponent>("Opponent", pos);
        }
    }

    public override void opponentsTurn()
    {
        List<Opponent> opponents = SceneHandler.getAllMeeplesFromType<Opponent>();

        Debug.Log("OPPONENTS TURN: " + opponents.Count + " opponents attacking.");

        //Todo: Instant execution. Should be successively according to animation
        foreach (Opponent opponent in opponents)
        {
            //Todo: Find target closest to opponent
            //Todo: Check if target is in fighting Range
            //Todo: If not in fighting range. Walk towards target

            opponent.targetMeeple = SceneHandler.getAllMeeplesFromType<Companion>()[0];

            //Placeholder
            if (Map.distance(opponent.Pos, opponent.targetMeeple.Pos) <= 1)
            {
                opponent.fight(opponent.targetMeeple);
            }
            else
            {
                int dir = Map.getDirection(opponent.Pos, opponent.targetMeeple.Pos);
                SceneHandler.setMeeplePos(opponent.gameObject, Map.tilesInRange(opponent.Pos, 1)[dir]);
            }
        }
    }

    public override bool isTacticalGameWon()
    {
        List<Opponent> opponents = SceneHandler.getAllMeeplesFromType<Opponent>();

        //Player wins this tactical game if there are no more opponents on the tactical grid.
        if(opponents == null || opponents.Count == 0)
        {
            return true;
        }

        return false;
    }

    public override bool isTacticalGameLost()
    {
        List<Mercenary> mercenaries = SceneHandler.getAllMeeplesFromType<Mercenary>();

        //Player loses this tactical game if he has no more mercenaries on the tactical grid.
        if (mercenaries == null || mercenaries.Count == 0)
        {
            return true;
        }

        return false;
    }

    public override bool hasPlayerAvailableMoves()
    {
        List<Mercenary> mercenaries = new List<Mercenary>();

        //Player has available moves left if at least one of his mercenaries action outstanding
        foreach (Mercenary mercenary in mercenaries)
        {
            if (mercenary.hasActionOutstanding)
            {
                return true;
            }
        }

        return false;
    }

}
