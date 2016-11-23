using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(PackAnimal))]
public class PackAnimalCI : Editor
{

    override public void OnInspectorGUI()
    {
        PackAnimal packAnimal = (PackAnimal)target;

        packAnimal.ActualLoad = EditorGUILayout.IntField("Actual Load: ", packAnimal.ActualLoad);
        packAnimal.MaxLoad = EditorGUILayout.IntField("Max Load: ", packAnimal.MaxLoad);
        packAnimal.butcherFoodAmount = EditorGUILayout.IntField("Butcher Food Amount: ", packAnimal.butcherFoodAmount);

        if (GUILayout.Button("Butcher it!"))
        {
            //Todo: Über eine Eventsystem regeln
            GameObject caravanObj = GameObject.Find("Caravan");
            Caravan caravan = caravanObj.GetComponent<Caravan>();

            packAnimal.gameObject.transform.parent = GameObject.Find("Graveyard").transform;
            caravan.removePackAnimal(packAnimal);
            caravan.gainProviant(packAnimal.butcherAnimal());
        }

    }
}
