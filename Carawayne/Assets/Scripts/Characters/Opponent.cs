using UnityEngine;

public class Opponent : Meeple
{

    public Meeple targetMeeple;
    public int dice;
    public int diceValue;

    public Opponent(Vector2 _pos, string _meepleName) : base(_pos, _meepleName)
    {
        dice = 1;
        diceValue = 6;
    }

    public void fight(Meeple _target)
    {
        if(_target.GetType().IsSubclassOf(typeof(Companion)) || _target.GetType() == typeof(Companion))
        {
            int rolledValue = SceneHandler.rollDice(diceValue);
            Companion targetComp = (Companion) _target;

            Debug.Log(meepleName + " strikes " + _target.meepleName + " with a " + rolledValue);

            targetComp.damaged(rolledValue);
        } else
        {
            Debug.Log(meepleName + " strikes " + _target.meepleName);
            _target.Alive = false;
        }

        //Todo: Implememnt actionOutstanding if neccessary. As example when queded intelligent interaction is needed from opponent side.
        //hasActionOutstanding = false;
    }

}
