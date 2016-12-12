using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class SceneHandler {

    //Todo: get Tiles from MapGenerator
    //public static List<MapTile>
    public static List<Meeple> meeples;
    public static List<Sandstorm> sandstorms;
    public static int turn; 
    //Todo: Deserialized Events from xml. Missing Serializeable class
    //public static List<Events> 
    public static Companion activeCompanion;
    public static GameMode activeMode;

    public static void endTurn()
    {
        
    }

}
