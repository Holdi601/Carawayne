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
        Debug.Log("TACTICAL GAME:" + GetType() + " STARTS");
        //Todo: Do run Game stuff. Sound. Animation. etc...
        SceneHandler.activeMode = GameMode.TACTICAL;
        highlightPossibleAgents();
        checkStartUpCondition();
        //Do initialization stuff from specific tactical game
    }

    public void endTurn()
    {
        //Todo: Do endTurn stuf. Sound. Animation. etc...
        //Todo: disable companion gameobjects if neccessary

        Debug.Log("PLAYER TURN ENDED.");

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
        highlightPossibleAgents();

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
            Debug.Log("PLAYER WINS TACTICAL GAME");
            stop();
            return false;
        }
        else if (isTacticalGameLost())
        {
            //Todo: Trigger lose implications. Sound. Animation. Etc...
            Debug.Log("PLAYER LOSES TACTICAL GAME");
            stop();
            return false;
        }

        return true;
    }

    public void newTurn()
    {
        turn++;

        highlightPossibleAgents();

        //Reset all movement Points from all opponent agents
        List<Companion> companions = SceneHandler.getAllMeeplesFromType<Companion>();

        Debug.Log("NEW TURN: " + turn);

        foreach (Companion companion in companions)
        {
            companion.HasActionOutstanding = true;
        }
        Map.highlightAllInnerTiles(false);
        //Todo: enable copmanion gameobjects if neccessary
    }

    virtual public void stop()
    {
        Debug.Log("TACTICAL GAME: " + GetType() + " ENDED!");

        //Todo: trigger stop status. Sound Animation etc...
        clear();
        SceneHandler.activeMode = GameMode.EXPLORATION;
    }

    virtual public void init()
    {
        Debug.Log("TACTICAL GAME: " + GetType() + " INITIALIZE");
    }

    public abstract void clear();

    public abstract void opponentsTurn();

    public abstract bool isTacticalGameWon();

    public abstract bool isTacticalGameLost();

    public abstract bool hasPlayerAvailableMoves();

    public abstract void highlightPossibleAgents();

    public abstract void checkStartUpCondition();
}
