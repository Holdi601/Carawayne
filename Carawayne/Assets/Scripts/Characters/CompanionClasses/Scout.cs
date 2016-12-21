using UnityEngine;
using System.Collections;

public class Scout : Companion {

    public Scout(Vector2 _pos, string _name, int _proviantDemand, int _strength) : base(_pos, _name, _proviantDemand, _strength)
    {
    }

    public void scout(Tile[] _tile)
    {
        hasActionOutstanding = false;
        //Todo: Missing implementation
    }

}
