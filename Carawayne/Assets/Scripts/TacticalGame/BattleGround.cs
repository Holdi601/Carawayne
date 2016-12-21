using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleGround : TacticalGame {

    public override void init()
    {
        //Todo: Disable all Copmanion Gameobjects which are note required
        //Todo: Place enemies on tactical Grid border/or whatever
        SceneHandler.meeples.Add(new Opponent(new Vector2(2, 1), "Opponent 1"));
        SceneHandler.meeples.Add(new Opponent(new Vector2(4, 0), "Opponent 2"));
        SceneHandler.meeples.Add(new Opponent(new Vector2(1, 2), "Opponent 3"));
        SceneHandler.meeples.Add(new Opponent(new Vector2(3, 2), "Opponent 4"));
    }

    public override void opponentsTurn()
    {
        List<Opponent> opponents = SceneHandler.getAllMeeplesFromType<Opponent>();

        //Todo: Instant execution. Should be successively according to animation
        foreach (Opponent opponent in opponents)
        {
            //Todo: Find target closest to opponent
            //Todo: Check if target is in fighting Range
            //Todo: If not in fighting range. Walk towards target
            //opponent.moveTo();   

            //Placeholder
            opponent.fight(SceneHandler.getAllMeeplesFromType<Companion>()[0]);
        }
    }

    public override bool isTacticalGameWon()
    {
        List<Opponent> opponents = new List<Opponent>();

        //Player wins this tactical game if there are no more opponents on the tactical grid.
        if(opponents == null || opponents.Count == 0)
        {
            return true;
        }

        return false;
    }

    public override bool isTacticalGameLost()
    {
        List<Mercenary> mercenaries = new List<Mercenary>();

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
