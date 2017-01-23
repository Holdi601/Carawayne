using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using System;

public class HuntingGround : TacticalGame
{

    private List<HexaPos> animalsPos;
    private int initialDirection;
    private int chasingRounds;

    public HuntingGround(int _initialDirection, int _chasingRounds)
    {
        initialDirection = _initialDirection;
        chasingRounds = _chasingRounds;
        animalsPos=new List<HexaPos>();
        switch (initialDirection)
        {
            case 3:
                animalsPos.Add(new HexaPos(4,4));
                animalsPos.Add(new HexaPos(4,6));
                animalsPos.Add(new HexaPos(4,8));
                animalsPos.Add(new HexaPos(4,10));
                break;
            case 2:
                animalsPos.Add(new HexaPos(8, 13));
                animalsPos.Add(new HexaPos(7, 12));
                animalsPos.Add(new HexaPos(5, 11));
                animalsPos.Add(new HexaPos(4, 10));
                break;
            case 1:
                animalsPos.Add(new HexaPos(8, 13));
                animalsPos.Add(new HexaPos(10, 12));
                animalsPos.Add(new HexaPos(11, 11));
                animalsPos.Add(new HexaPos(13, 10));
                break;
            case 0:
                animalsPos.Add(new HexaPos(13, 10));
                animalsPos.Add(new HexaPos(13, 8));
                animalsPos.Add(new HexaPos(13, 6));
                animalsPos.Add(new HexaPos(13, 4));
                break;
            case 5:
                animalsPos.Add(new HexaPos(13, 4));
                animalsPos.Add(new HexaPos(11, 3));
                animalsPos.Add(new HexaPos(10, 2));
                animalsPos.Add(new HexaPos(8, 1));
                break;
            case 4:
                animalsPos.Add(new HexaPos(8, 1));
                animalsPos.Add(new HexaPos(7, 2));
                animalsPos.Add(new HexaPos(5, 3));
                animalsPos.Add(new HexaPos(4, 4));
                break;
        }
    }

    public override void clear()
    {
        List<HuntedAnimal> huntedAnimals = SceneHandler.getAllMeeplesFromType<HuntedAnimal>();
        foreach (HuntedAnimal huntedAnimal in huntedAnimals)
        {
            SceneHandler.Destroy(huntedAnimal.gameObject);
        }
    }

    override public void init()
    {
        //Todo: Disable all Copmanion Gameobjects which are note required
        base.init();
        foreach (HexaPos pos in animalsPos)
        {
            SceneHandler.createMeeple<HuntedAnimal>("animal", pos);
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
            int chasingDir = (initialDirection+3)%6;
            HexaPos escapingPos = Map.tilesInRange(huntedAnimal.Pos, 1)[chasingDir];

            huntedAnimal.moveTowardsTarget(escapingPos);
        }
        chasingRounds--;
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
        if (chasingRounds<=0)
        {
            return true;
        }
        //List<HuntedAnimal> huntedAnimals = SceneHandler.getAllMeeplesFromType<HuntedAnimal>();

        //foreach (HuntedAnimal huntedAnimal in huntedAnimals)
        //{
        //    if (!huntedAnimal.hasEscaped)
        //    {
        //        return false;
        //    }
        //}

        return false;
    }

    public override bool hasPlayerAvailableMoves()
    {
        List<Hunter> hunters = SceneHandler.getAllMeeplesFromType<Hunter>();

        //Player has available moves left if at least one of his mercenaries action outstanding
        foreach (Hunter hunter in hunters)
        {
            if (hunter.HasActionOutstanding)
            {
                return true;
            }
        }

        return false;
    }

    public override void highlightPossibleAgents()
    {
        List<Hunter> hunters = SceneHandler.getAllMeeplesFromType<Hunter>();

        foreach (Hunter hunter in hunters)
        {
            if (hunter.HasActionOutstanding)
            {
                MeshRenderer meepTileMesh = SceneHandler.smallMap[hunter.Pos.x, hunter.Pos.y].GetComponent<MeshRenderer>();
                meepTileMesh.material = Initialisation.activeAgentTileMaterial;
            }
        }
    }
}
