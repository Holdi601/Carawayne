using UnityEngine;
using System.Collections;
using System;

public class CameraController : MonoBehaviour {
    GameObject cameraGimbalGO;
    GameObject cameraGo;
    GameObject cameraTGTGO;
    float xMin, zMin, xMax, zMax, yMin, yMax;
    public static bool cameraControl = true;
    Vector3 backuppos;
    Quaternion backupRot;
    int leftEdge, rightEdge, upperEdge, lowerEdge;
    

	// Use this for initialization
	void Start () {
        cameraGimbalGO = GameObject.Find("CameraGimbal");
        cameraGo = GameObject.Find("Camera");
        cameraTGTGO = GameObject.Find("CameraLookAtTGT");
        setCameraBoundries();
        leftEdge = Convert.ToInt16(Screen.width*0.1f);
        rightEdge = Screen.width - leftEdge;
        lowerEdge = Convert.ToInt16(Screen.height * 0.1f);
        upperEdge = Screen.height - lowerEdge;

        
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 mousePos = Input.mousePosition;
        //Debug.Log(mousePos);
        if (cameraControl)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                showMap();
            }
            if (Input.GetKeyUp(KeyCode.M))
            {
                destroyMap();
            }

            if (Input.GetKey(KeyCode.Mouse2))
            {
                cameraTGTGO.transform.localEulerAngles += new Vector3(0, Input.GetAxis("Mouse X") * Time.deltaTime*80, 0);
                
                //cameraGimbalGO.transform.RotateAround(SceneHandler.largeMap[SceneHandler.caravanPosition.x, SceneHandler.caravanPosition.y].transform.position, new Vector3(0, 1, 0), Input.GetAxis("Mouse X"));
            }

            if (Input.GetKey(KeyCode.Space))
            {
                resetCamera();
            }

            if (Input.mouseScrollDelta != new Vector2(0,0))
            {
                if(Input.mouseScrollDelta.y >0 && cameraGimbalGO.transform.position.y > yMin)
                {
                    Vector3 translate = new Vector3(0, (Mathf.Sin(cameraGo.transform.eulerAngles.x * ((2 * Mathf.PI) / 360) )*2*-1)*(0.1f*cameraGo.transform.position.y), (Mathf.Cos(cameraGo.transform.eulerAngles.x *((2*Mathf.PI)/360))*2));
                    cameraGimbalGO.transform.Translate(translate);
                }
                if(Input.mouseScrollDelta.y<0 && cameraGimbalGO.transform.position.y < yMax)
                {
                    Vector3 translate = new Vector3(0, Mathf.Sin(cameraGo.transform.eulerAngles.x * ((2 * Mathf.PI) / 360)) * 2 * (0.1f * cameraGo.transform.position.y), (Mathf.Cos(cameraGo.transform.eulerAngles.x * ((2 * Mathf.PI) / 360)) * 2*-1));
                    cameraGimbalGO.transform.Translate(translate);
                }
            }

            if (mousePos.x < leftEdge)
            {
                if (cameraTGTGO.transform.position.x > xMin)
                {
                    Vector3 transl = new Vector3(Time.deltaTime * ((mousePos.x-leftEdge)*(0.01f*cameraGo.transform.position.y)), 0, 0);
                    cameraTGTGO.transform.Translate(transl);
                }

            }
            else if (mousePos.x >rightEdge)
            {
                if (cameraTGTGO.transform.position.x < xMax)
                {
                    Vector3 transl = new Vector3(Time.deltaTime * ((mousePos.x-rightEdge)*(0.01f*cameraGo.transform.position.y)), 0, 0);
                    cameraTGTGO.transform.Translate(transl);
                }

            }
            if (mousePos.y < lowerEdge)
            {
                if (cameraTGTGO.transform.position.z > zMin)
                {
                    Vector3 transl = new Vector3(0, 0, Time.deltaTime * ((mousePos.y-lowerEdge)*(0.01f * cameraGo.transform.position.y)));
                   
                    cameraTGTGO.transform.Translate(transl);
                }

            }
            else if (mousePos.y > upperEdge)
            {
                if (cameraTGTGO.transform.position.z < zMax)
                {
                    Vector3 transl = new Vector3(0, 0, Time.deltaTime * ((mousePos.y-upperEdge)*(0.01f*cameraGo.transform.position.y)));

                    cameraTGTGO.transform.Translate(transl);
                }

            }
        }
	}
    
     void setCameraBoundries()
    {
        xMin = -10;
        xMax = SceneHandler.largeMap[(SceneHandler.largeMap.GetLength(0) - 1), 0].transform.position.x + 10;
        zMin = -30;
        zMax = SceneHandler.largeMap[0, (SceneHandler.largeMap.GetLength(1) - 1)].transform.position.z;
        yMin = 2;
        yMax = 150;
    }

    void resetCamera()
    {
        cameraTGTGO.transform.eulerAngles= new Vector3(0,0,0);
        cameraTGTGO.transform.position = new Vector3(0, 0, 0);
        Vector3 newpos = SceneHandler.largeMap[SceneHandler.caravanPosition.x, SceneHandler.caravanPosition.y].transform.position;
        cameraTGTGO.transform.Translate(newpos);
    }

    void testDistance(bool h)
    {
        float xLength = (SceneHandler.largeMap[(SceneHandler.largeMap.GetLength(0) - 1), 0].transform.position.x + 10)/2;
        float zLength = (SceneHandler.largeMap[0, (SceneHandler.largeMap.GetLength(1) - 1)].transform.position.z + 10) / 2;
        float yLength = distanceForCameraNeeded();
        Vector3 pos = new Vector3(xLength, yLength, zLength);


        if (h)
        {
            backuppos = cameraGimbalGO.transform.position;
            backupRot = cameraGo.transform.rotation;
            cameraGimbalGO.transform.position = pos;
            cameraGo.transform.eulerAngles = new Vector3(90, 0, 0);
        }
        else
        {
            cameraGimbalGO.transform.position = backuppos;
            cameraGo.transform.rotation = backupRot;

        }
    }

    float distanceForCameraNeeded()
    {
        float tan = Mathf.Tan((Camera.main.fieldOfView / 2)*Mathf.PI/180);
        float a = (SceneHandler.largeMap[0, (SceneHandler.largeMap.GetLength(1) - 1)].transform.position.z + 10) / 2;
        return a / tan;
    }

    Texture2D Screenshot()
    {

        backuppos = cameraGimbalGO.transform.position;
        backupRot = cameraGo.transform.rotation;
        float xLength = (SceneHandler.largeMap[(SceneHandler.largeMap.GetLength(0) - 1), 0].transform.position.x + 10) / 2;
        float zLength = (SceneHandler.largeMap[0, (SceneHandler.largeMap.GetLength(1) - 1)].transform.position.z + 10) / 2;
        float yLength = distanceForCameraNeeded();
        Vector3 pos = new Vector3(xLength, yLength, zLength);
        cameraGimbalGO.transform.position = pos;
        cameraGo.transform.eulerAngles = new Vector3(90, 0, 0);



        int resWidth = Camera.main.pixelWidth;
        int resHeight = Camera.main.pixelHeight;
        Camera camera = Camera.main;
        camera.clearFlags = CameraClearFlags.Depth;
        RenderTexture rt = new RenderTexture(resWidth, resHeight, 32);
        camera.targetTexture = rt;
        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.ARGB32, false);
        camera.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
        screenShot.Apply();
        camera.targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors
        Destroy(rt);
        camera.clearFlags = CameraClearFlags.Skybox;

        return screenShot;
    }

    void showMap()
    {
        Texture2D MapScreen = Screenshot();
        GameObject mapGO = (GameObject)Instantiate(Initialisation.planeSample);
        float xLength = (SceneHandler.largeMap[(SceneHandler.largeMap.GetLength(0) - 1), 0].transform.position.x + 10) / 2;
        float zLength = (SceneHandler.largeMap[0, (SceneHandler.largeMap.GetLength(1) - 1)].transform.position.z + 10) / 2;
        float yLength = distanceForCameraNeeded()-1;
        Vector3 pos = new Vector3(xLength, yLength, zLength);
    
        float tan = Mathf.Tan(((float)Camera.main.fieldOfView / (float)2) * ((float)Mathf.PI / 180f));
        float vertSize = (1f / tan) * 2f;
        float horSize = (float)Camera.main.pixelWidth * (vertSize/(float)Camera.main.pixelHeight);
       
        mapGO.transform.localScale = new Vector3(horSize, 0, vertSize);
        mapGO.transform.localScale = mapGO.transform.localScale * 0.01f;
        MeshRenderer mr = mapGO.GetComponent<MeshRenderer>();
        Material mat = (Material)Instantiate(Initialisation.transparentMat);
        mat.mainTexture = MapScreen;
        
        mr.material = mat;
        mapGO.transform.name = "minimap";
        mapGO.transform.localEulerAngles = new Vector3(0, 180, 0);
        mapGO.transform.position = pos;
        mapGO.transform.parent = cameraGo.transform;
        cameraGimbalGO.transform.position = backuppos;
        cameraGo.transform.rotation = backupRot;


    }
    void destroyMap()
    {
        GameObject miniM = GameObject.Find("minimap");
        Destroy(miniM);
    }

}
