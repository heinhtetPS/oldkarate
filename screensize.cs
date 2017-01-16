using UnityEngine;
using System.Collections;

public class screensize : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
		GUI.TextArea(new Rect(0, 40, 150, 20), "Screensize: " + Screen.width.ToString() + "x" + Screen.height.ToString());	
		
	}
}
