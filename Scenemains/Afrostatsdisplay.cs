using UnityEngine;
using System.Collections;

public class Afrostatsdisplay : MonoBehaviour {
	
	private Rect mainpic = new Rect();
	
	
	public GUIStyle regularfont, empty;
	public Texture2D afropic, tinycash, tinyred, tinygreen, tinyblue, tinyblack, tinywhite;
	private Color clear = new Color(0,0,0,0);
	public bool drawstats = true;
	
	Dojomain dojoscript;
	LevelSelect lvlscript;
	
	// Use this for initialization
	void Start () {
		
		//Initialize

			mainpic = new Rect(Screen.width - 492, 20, 52, 44);
		
		if (Application.loadedLevelName == "Dojo")
			dojoscript = (Dojomain)GameObject.FindGameObjectWithTag("MainCamera").GetComponent("Dojomain");	
		

		
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
		
		
		if (GameObject.FindGameObjectWithTag("Fader") == null)
		{
			
			GUI.depth = -100;
			if (Application.loadedLevelName == "Dojo")
			{	
				if (dojoscript.RunesUI)
				drawstats = false;
				else drawstats = true;
			}
			
			if (drawstats)
			{
				//pic
				GUI.Button(mainpic, afropic, empty);
				//Level
				regularfont.fontSize = 20;
				GUI.Label(new Rect(mainpic.x + 65, mainpic.y - 10, 300, 30), "Level " + PlayerPrefs.GetInt("Playerlevel").ToString() + " Karate noob", regularfont);
				regularfont.fontSize = 16;
				//XP left
				regularfont.fontSize = 14;
				GUI.Label(new Rect(mainpic.x + 390, mainpic.y - 20, 300, 25), "Next lvl: " + PlayerPrefs.GetFloat("XPtonextlevel").ToString() + "xp", regularfont);
				regularfont.fontSize = 16;
				
				//moneye west
				GUI.DrawTexture(new Rect(mainpic.x + 65, mainpic.y + 22, 22, 22), tinycash);
				GUI.Label(new Rect(mainpic.x + 88, mainpic.y + 25 , 200, 50), PlayerPrefs.GetInt("Money").ToString(), regularfont);
				GUI.DrawTexture(new Rect(mainpic.x + 155, mainpic.y + 22, 6, 22), tinyred);
				GUI.Label(new Rect(mainpic.x + 165, mainpic.y + 25, 200, 50), PlayerPrefs.GetInt("Redscroll").ToString(), regularfont);
				GUI.DrawTexture(new Rect(mainpic.x + 205, mainpic.y + 22, 6, 22), tinygreen);
				GUI.Label(new Rect(mainpic.x + 215, mainpic.y + 25, 200, 50), PlayerPrefs.GetInt("Greenscroll").ToString(), regularfont);
				GUI.DrawTexture(new Rect(mainpic.x + 255, mainpic.y + 22, 6, 22), tinyblue);
				GUI.Label(new Rect(mainpic.x + 265, mainpic.y + 25, 200, 50), PlayerPrefs.GetInt("Bluescroll").ToString(), regularfont);
				GUI.DrawTexture(new Rect(mainpic.x + 305, mainpic.y + 22, 6, 22), tinyblack);
				GUI.Label(new Rect(mainpic.x + 315, mainpic.y + 25, 200, 50), PlayerPrefs.GetInt("Blackscroll").ToString(), regularfont);
				GUI.DrawTexture(new Rect(mainpic.x + 355, mainpic.y  + 22, 6, 22), tinywhite);
				GUI.Label(new Rect(mainpic.x + 365, mainpic.y + 25, 200, 50), PlayerPrefs.GetInt("Whitescroll").ToString(), regularfont);
		
			}
		}
		
	}
}
