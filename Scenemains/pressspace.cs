using UnityEngine;
using System.Collections;

public class pressspace : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown(KeyCode.Space))
		{
			PlayerPrefs.SetInt("Currentlevel", 1);
			Application.LoadLevel("Level1");
		}
	}
	
	void OnGUI () 
	{
		
//		if (GUI.Button(new Rect(Screen.width - 140, 150, 100, 25), "Test Stage"))
//		{
//			Application.LoadLevel("devmode");
//		}
//		
//		if (GUI.Button(new Rect(Screen.width - 140, 180, 100, 25), "Level Select"))
//		{
//			Application.LoadLevel("LevelSelect");
//		}
	
	}
}
