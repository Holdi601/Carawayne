using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Prince : Companion {

    //Todo: Missing implementation

    //Todo: missing spells for Prince
    //public List<Spells> spells; 

    //public Prince(HexaPos _pos, string _name, int _proviantDemand, int _strength) : base(_pos, _name, _proviantDemand, _strength)
    //{
    //}

    void Awake()
    {
        proviantDemand = 3;
        proviantDemandMax =3;
        strength = 10;
        strengthMax = 10;
    }

    //public void useSpell(Spell _spell)
    //{
    //hasActionOutstanding = false; 
    //}

}
