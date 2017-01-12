using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{

    public RawImage emptyBar;
    public RawImage fullBar;
    public Text statusText;
    [SerializeField, Range(0,1)]
    public float progress = 1;

    private float maxWidth;

	// Use this for initialization
	void Start ()
	{
	    maxWidth = fullBar.rectTransform.rect.width;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    fullBar.rectTransform.sizeDelta = new Vector2(maxWidth * progress, fullBar.rectTransform.sizeDelta.y);
    }
}
