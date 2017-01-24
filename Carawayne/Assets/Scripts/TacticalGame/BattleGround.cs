﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BattleGround : TacticalGame
{

    private List<HexaPos> opponentsPos;
    private int initialDirection;

    public BattleGround(int _initialDirection)
    {
        initialDirection = _initialDirection;
        opponentsPos = new List<HexaPos>();
        Debug.Log("OpponentsSpanwingFromDirection:" + initialDirection);
        switch (initialDirection)
        {
            case 3:
                opponentsPos.Add(new HexaPos(4, 4));
                opponentsPos.Add(new HexaPos(4, 6));
                opponentsPos.Add(new HexaPos(4, 8));
                opponentsPos.Add(new HexaPos(4, 10));
                break;
            case 2:
                opponentsPos.Add(new HexaPos(8, 13));
                opponentsPos.Add(new HexaPos(7, 12));
                opponentsPos.Add(new HexaPos(5, 11));
                opponentsPos.Add(new HexaPos(4, 10));
                break;
            case 1:
                opponentsPos.Add(new HexaPos(8, 13));
                opponentsPos.Add(new HexaPos(10, 12));
                opponentsPos.Add(new HexaPos(11, 11));
                opponentsPos.Add(new HexaPos(13, 10));
                break;
            case 0:
                opponentsPos.Add(new HexaPos(13, 10));
                opponentsPos.Add(new HexaPos(13, 8));
                opponentsPos.Add(new HexaPos(13, 6));
                opponentsPos.Add(new HexaPos(13, 4));
                break;
            case 5:
                opponentsPos.Add(new HexaPos(13, 4));
                opponentsPos.Add(new HexaPos(11, 3));
                opponentsPos.Add(new HexaPos(10, 2));
                opponentsPos.Add(new HexaPos(8, 1));
                break;
            case 4:
                opponentsPos.Add(new HexaPos(8, 1));
                opponentsPos.Add(new HexaPos(7, 2));
                opponentsPos.Add(new HexaPos(5, 3));
                opponentsPos.Add(new HexaPos(4, 4));
                break;
        }

        
    }

    override public void init()
    {
        base.init();

        //Todo: Disable all Copmanion Gameobjects which are note required
        foreach (HexaPos pos in opponentsPos)
        {
            SceneHandler.createMeeple<Opponent>("opponent", pos);
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

            opponent.targetMeeple = getClosestMeeple(opponent);

            //Placeholder
            if (Map.distance(opponent.Pos, opponent.targetMeeple.Pos) <= 1)
            {
                opponent.fight(opponent.targetMeeple);
            }
            else
            {
                opponent.moveTowardsTarget(opponent.targetMeeple.Pos);
            }
        }
    }

    public Companion getClosestMeeple(Opponent _opp)
    {
        
        List<Companion> comps = SceneHandler.getAllMeeplesFromType<Companion>();
        Companion closestMeeple = comps[0];

        foreach (Companion comp in comps)
        {
            if (Map.distance(_opp.Pos, comp.Pos) < Map.distance(_opp.Pos, closestMeeple.Pos))
            {
                closestMeeple = comp;
            }
        }

        return closestMeeple;
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
        List<Prince> prince = SceneHandler.getAllMeeplesFromType<Prince>();
        //Player loses this tactical game if he has no more mercenaries on the tactical grid.

        if (prince.Count <= 0)
        {
            SceneHandler.endGame(SceneHandler.getAllMeeplesFromType<Companion>());
        }

        if (mercenaries == null || mercenaries.Count == 0)
        {
            return true;
        }

        return false;
    }

    public override bool hasPlayerAvailableMoves()
    {
        List<Mercenary> mercenaries = SceneHandler.getAllMeeplesFromType<Mercenary>();

        Debug.Log("PLAZERTURNENDED");

        //Player has available moves left if at least one of his mercenaries action outstanding
        foreach (Mercenary mercenary in mercenaries)
        {
            if (mercenary.HasActionOutstanding)
            {
                return true;
            }
        }

        return false;
    }

    public override void clear()
    {

        List<Opponent> opps = SceneHandler.getAllMeeplesFromType<Opponent>();
        foreach (Opponent opponent in opps)
        {
            SceneHandler.meeples.Remove(opponent);
            SceneHandler.Destroy(opponent.gameObject);
        }
    }

    public override void highlightPossibleAgents()
    {
        List<Mercenary> mercenaries = SceneHandler.getAllMeeplesFromType<Mercenary>();

        foreach (Mercenary merc in mercenaries)
        {
            if (merc.HasActionOutstanding)
            {
                MeshRenderer meepTileMesh = SceneHandler.smallMap[merc.Pos.x, merc.Pos.y].GetComponent<MeshRenderer>();
                meepTileMesh.material = Initialisation.activeAgentTileMaterial;
            }
        }
    }

}
