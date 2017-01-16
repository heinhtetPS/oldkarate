using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	
	public Font myFont;
	public GUIStyle myStyle;
	
	// Use this for initialization
	void Start () {
		
		GUIStyle myStyle = new GUIStyle();
		myStyle.normal.textColor = Color.white;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
		
    	myStyle.font = myFont;
		GUI.Label(new Rect(0, 100, 350, 100), "KARATEMAN (WIP)" , myStyle);
		
		if (GUI.Button(new Rect(10, 150, 100, 25), "Start Game"))
		{
			Application.LoadLevel(4);
		}
		
	}
}
