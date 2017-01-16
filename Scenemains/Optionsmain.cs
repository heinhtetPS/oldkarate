using UnityEngine;
using System.Collections;

public class Optionsmain : MonoBehaviour {
	
	bool Skipcomics = false;
	bool keyboardonly = false;
	bool autocombo = false;
	
	// Use this for initialization
	void Start () {
		
		if (PlayerPrefs.GetInt("Skipcomic") == 0)
			Skipcomics = false;
		if (PlayerPrefs.GetInt("Skipcomic") == 1)
			Skipcomics = true;
		
		if (PlayerPrefs.GetInt("Mousecontrols") == 1)
		{
			keyboardonly = false;
		}
		
		if (PlayerPrefs.GetInt("Mousecontrols") == 0)
			keyboardonly = true;
		
		
		if (PlayerPrefs.GetInt("Autocombo") == 0)
			autocombo = false;
		if (PlayerPrefs.GetInt("Autocombo") == 1)
			autocombo = true;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Skipcomics)
			PlayerPrefs.SetInt("Skipcomic", 1);
		if (!Skipcomics)
			PlayerPrefs.SetInt("Skipcomic", 0);
		
		
		
		if (keyboardonly)
		{
			PlayerPrefs.SetInt("Mousecontrols", 0);	
			PlayerPrefs.SetInt("Altcontrols", 0);
		}
		if (!keyboardonly)
			PlayerPrefs.SetInt("Mousecontrols", 1);	
		
		
		if (autocombo)
			PlayerPrefs.SetInt("Autocombo", 1);	
		if (!autocombo)
			PlayerPrefs.SetInt("Autocombo", 0);
	}
	
	void OnGUI()
	{
		GUI.color = Color.white;
		GUI.Box(new Rect(50, 50, Screen.width * 0.8f, Screen.height *  0.8f), " ");
		GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 25, 400, 200), "MORE OPTIONS COMING SOON!");
		
		//skip comics
		Skipcomics = GUI.Toggle(new Rect(100, 100, 100, 50), Skipcomics, "Skip comics");
		GUI.Label(new Rect(100, 80, 300, 20), "Skip comic cutscenes before stages?");
		
		//mouse and wii movement
		GUI.Label(new Rect(100, 160, 300, 100), "Way of the Karate uses mouse and keyboard controls by default. If you would like to use alternative controls, (such as a console controller/fightstick) or " +
			"if you would like to use only the keyboard, use the following option. WARNING: Work in Progress.");

		keyboardonly = GUI.Toggle(new Rect(100, 255, 250, 20), keyboardonly, "Mouse-less controls");
		
		
//		//auto combo
//		GUI.Label(new Rect(100, 290, 350, 50), "Check this option if you want to use dynasty-style combo system instead of open-combo.");
//		autocombo = GUI.Toggle(new Rect(100, 320, 250, 20), autocombo, "Use Dynasty mode");
		
		//back to level select
		if (GUI.Button(new Rect(Screen.width - 120, Screen.height - 50, 100, 25), "Back"))
		{
			Doublecheckprefs();
			
			if (PlayerPrefs.GetInt("Firsttime") == 1)
			Application.LoadLevel("LevelSelect");
			
			if (PlayerPrefs.GetInt("Firsttime") == 0)
			Application.LoadLevel("cinematic1");
			
		}
		
	}
	
	void Doublecheckprefs()
	{
		if (Skipcomics)
			PlayerPrefs.SetInt("Skipcomic", 1);
		if (!Skipcomics)
			PlayerPrefs.SetInt("Skipcomic", 0);
		
		
		if (keyboardonly)
		{
			PlayerPrefs.SetInt("Mousecontrols", 0);	
			PlayerPrefs.SetInt("Altcontrols", 0);
		}
		if (!keyboardonly)
			PlayerPrefs.SetInt("Mousecontrols", 1);
		
		
		if (autocombo)
			PlayerPrefs.SetInt("Autocombo", 1);	
		if (!autocombo)
			PlayerPrefs.SetInt("Autocombo", 0);
	}
}
