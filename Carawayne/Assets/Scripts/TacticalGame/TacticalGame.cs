using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class TacticalGame
{

    public int turn;

    protected TacticalGame()
    {
        turn = 0;
    }

    public void run()
    {
        Debug.Log("START TACTICAL GAME FROM TYPE: " + GetType());
        //Todo: Do run Game stuff. Sound. Animation. etc...
        SceneHandler.activeMode = GameMode.TACTICAL;
        //Do initialization stuff from specific tactical game
        init();
    }

    public void endTurn()
    {
        //Todo: Do endTurn stuf. Sound. Animation. etc...
        //Todo: disable companion gameobjects if neccessary

        Debug.Log("Player turn ended.");

        //Run artificial intelligence from specific tactical game
        opponentsTurn();

        if (isGameContinuingByCheckingWinCondition())
        {
            newTurn();
        }
    }

    public void onPlayerInteractionEnded()
    {
        Debug.Log("Player did action.");

        if (isGameContinuingByCheckingWinCondition() && !hasPlayerAvailableMoves())
        {
            endTurn();
        }
    }

    private bool isGameContinuingByCheckingWinCondition()
    {
        if (isTacticalGameWon())
        {
            //Todo: trigger Win implications. Sound. Animation. Etc...
            Debug.Log("Player wins");
            stop();
            return false;
        }
        else if (isTacticalGameLost())
        {
            //Todo: Trigger lose implications. Sound. Animation. Etc...
            Debug.Log("Player lost");
            stop();
            return false;
        }

        return true;
    }

    public void newTurn()
    {
        Debug.Log("New turn begins: " + turn);

        turn++;

        //Reset all movement Points from all opponent agents
        List<Companion> companions = SceneHandler.getAllMeeplesFromType<Companion>();

        foreach (Companion companion in companions)
        {
            companion.hasActionOutstanding = true;
        }

        //Todo: enable copmanion gameobjects if neccessary
    }

    public void stop()
    {
        Debug.Log("TACTICAL GAME FROM TYPE: " + GetType() + " ENDED!");

        //Todo: trigger stop status. Sound Animation etc...
        SceneHandler.activeMode = GameMode.EXPLORATION;
    }

    public abstract void init();

    public abstract void opponentsTurn();

    public abstract bool isTacticalGameWon();

    public abstract bool isTacticalGameLost();

    public abstract bool hasPlayerAvailableMoves();

}
