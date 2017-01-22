using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickWorker : MonoBehaviour {
    static bool radialMenuBool;
    Ray ray;
    RaycastHit hit;
	// Use this for initialization
	void Start () {
        radialMenuBool = false;
        
	}
	
	// Update is called once per frame
	void Update () {
        if (radialMenuBool)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                destroyRadialMenu();
            }
            
        }else
        {
            if (Input.GetMouseButtonDown(1))
            {
                ray= Camera.main.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out hit))
                {
                    
                    GameObject hittedOBJ = hit.collider.gameObject;
                    Companion com = hittedOBJ.GetComponent<Companion>();
                    if (com != null)
                    {
                        SceneHandler.activeCompanion = com;
                        Vector3 pos = hittedOBJ.transform.position;
                        pos.y += 1.2f;
                        switch (com.GetType().Name)
                        {
                            case "Healer":
                                generateRadialMenu(pos, "Healer");
                                break;

                            case "Hunter":
                                generateRadialMenu(pos, "Worker");
                                break;

                            case "Mercenary":
                                generateRadialMenu(pos, "Mercenary");
                                break;

                            case "Merchant":
                                
                                break;

                            case "Prince":
                                generateRadialMenu(pos, "Prince");
                                break;

                            case "Scout":
                                generateRadialMenu(pos, "Scout");
                                break;

                            case "PackAnimal":
                                generateRadialMenu(pos, "Camel");
                                break;

                            default:
                                break;
                        }
                    }
                    
                }
            }
        }
    }

    void generateRadialMenu(Vector3 position, string Class)
    {
        int hexasToCreate = 0;
        if (Class == "Camel")
        {
            hexasToCreate = 1;
        }else if(Class=="Worker" || Class == "Mercenary")
        {
            hexasToCreate = 2;
        }
        else if(Class=="Scout" || Class =="Prince" || Class=="Healer")
        {
            hexasToCreate = 3;
        }


        GameObject radialMenu = new GameObject("RadialMenu");
        radialMenuBool = true;
        GameObject[] elements = new GameObject[hexasToCreate];
        float angle = 360f / hexasToCreate;

        for(int i=0; i<hexasToCreate; i++)
        {
            switch (Class)
            {
                case "Camel":
                    elements[i] = generateRadialElement("goSacrifice");
                    break;

                case "Worker":
                    if (i < 1) elements[i] = generateRadialElement("getStatistics");
                    else elements[i] = generateRadialElement("leaveBehind");
                    break;

                case "Mercenary":
                    if (i < 1) elements[i] = generateRadialElement("getStatistics");
                    else elements[i] = generateRadialElement("leaveBehind");
                    break;

                case "Scout":
                    if (i < 1) elements[i] = generateRadialElement("getStatistics");
                    else if(i<2)elements[i] = generateRadialElement("leaveBehind");
                    else elements[i] = generateRadialElement("goScout");
                    break;

                case "Prince":
                    if (i < 1) elements[i] = generateRadialElement("getStatistics");
                    else if(i<2) elements[i] = generateRadialElement("goFeast");
                    else elements[i] = generateRadialElement("goVIP");
                    break;

                case "Healer":
                    if (i < 1) elements[i] = generateRadialElement("getStatistics");
                    else if(i<2) elements[i] = generateRadialElement("leaveBehind");
                    else elements[i] = generateRadialElement("goHeal");
                    break;

                default:
                    elements[i] = new GameObject("error");
                    break;
            }

            elements[i].transform.Translate(new Vector3(-1f, 0, 0));
            elements[i].transform.RotateAround(radialMenu.transform.position, new Vector3(0, 1, 0), angle * i);
            elements[i].transform.localEulerAngles = new Vector3(90, 0, 0);
            elements[i].transform.parent = radialMenu.transform;
            elements[i].transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

        }
        



        radialMenu.transform.position = position;

    }

    public static void destroyRadialMenu()
    {
        if (radialMenuBool)
        {
            GameObject rdm = GameObject.Find("RadialMenu");
            if (rdm != null)
            {
                Destroy(rdm);
            }
            radialMenuBool = false;
        }
    }
    GameObject generateRadialElement(string ScriptType)
    {
        GameObject elementGo = new GameObject("element");
        GameObject hexaGo = new GameObject("hexaSprite");
        GameObject textGo = new GameObject("text");
        hexaGo.transform.parent = elementGo.transform;
        textGo.transform.parent = elementGo.transform;
        SpriteRenderer sr = hexaGo.AddComponent<SpriteRenderer>();
        sr.sprite = (Sprite)Instantiate(Initialisation.sprt);
        TextMesh tm = textGo.AddComponent<TextMesh>();
        tm.fontSize = 140;
        textGo.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        tm.alignment = TextAlignment.Center;
        tm.anchor = TextAnchor.MiddleCenter;
        textGo.transform.localPosition = new Vector3(0, 0, -0.0001f);
        BoxCollider bc= hexaGo.AddComponent<BoxCollider>();
        bc.size = new Vector3(5, 4.4f, 0.01f);

        switch (ScriptType)
        {
            case "leaveBehind":
                hexaGo.AddComponent<LeaveBehind>();
                tm.text = "Zurücklassen";
                break;

            case "goScout":
                hexaGo.AddComponent<GoScout>();
                tm.text = "Spähen";
                break;

            case "goSacrifice":
                hexaGo.AddComponent<GoSacrifice>();
                tm.text = "Opfern";
                break;

            case "getStatistics":
                hexaGo.AddComponent<GetStatistics>();
                tm.text = "Statistiken";
                break;

            case "goHeal":
                hexaGo.AddComponent<GoHeal>();
                tm.text = "Heilen";
                break;

            case "goFeast":
                hexaGo.AddComponent<GoFeast>();
                tm.text = "Festmahl";
                break;

            case "goVIP":
                hexaGo.AddComponent<GoVIP>();
                tm.text = "Wichtige\n Persönlichkeit";
                break;

            default:break;
        }

        return elementGo;
    }

    

    void testfunct()
    {
        Debug.Log("Hey it is me");
    }

}
