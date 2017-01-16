using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI () {
		
		GUI.Label(new Rect(150, 100, 100, 100), "GAME OVER");
		GUI.contentColor = Color.red;
		GUI.Label(new Rect(150, 120, 100, 100), "Try again?");
		
		if (GUI.Button(new Rect(140, 150, 50, 25), "Yes"))
		{
			Application.LoadLevel(PlayerPrefs.GetInt("Currentlevel"));
			
		}
		
		
		if (GUI.Button(new Rect(200, 150, 50, 25), "No"))
			Application.Quit();
			
		if (GUI.Button(new Rect(140, 190, 150, 25), "Level Select"))
			Application.LoadLevel("LevelSelect");
		
	}
}
