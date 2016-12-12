using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Caravan))]
public class CaravanCI : Editor
{
    private string CharName = "Insert name!";
    private int foodConsumption = 1;
    private int packload = 1;
    private int moral = 10;

    override public void OnInspectorGUI()
    {
        Caravan caravan = (Caravan)target;

        DrawDefaultInspector();

        //Input area for charakter generation
        EditorGUILayout.LabelField("- Charakter creation -");
        CharName = EditorGUILayout.TextField(CharName);
        foodConsumption = EditorGUILayout.IntField("Food consumption: ", foodConsumption);
        moral = EditorGUILayout.IntField("Moral: ", moral);

        //Create charakter section
        if (GUILayout.Button("Create Companion"))
        {
            GameObject go = new GameObject(CharName);
            go.AddComponent<Companion>();
            go.transform.parent = caravan.transform.FindChild("Companions");

            Companion comp = go.GetComponent<Companion>();
            comp.CharName = CharName;
            comp.OriginalFoodDemand = foodConsumption;
            comp.MaxCondition = moral;
            comp.ActualFoodDemand = foodConsumption;
            comp.ActualCondition = moral;
        }

        EditorGUILayout.LabelField("- PackAnimal creation -");
        packload = EditorGUILayout.IntField("Pack load: ", packload);

        if (GUILayout.Button("Create PackAnimal"))
        {
            GameObject go = new GameObject("PackAnimal");
            go.AddComponent<PackAnimal>();
            go.transform.parent = caravan.transform.FindChild("PackAnimals");

            PackAnimal packAnimal = go.GetComponent<PackAnimal>();
            packAnimal.actualLoad = packload;
            packAnimal.maxLoad = packload;
        }
    }
}