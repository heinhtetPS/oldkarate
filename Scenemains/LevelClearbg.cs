using UnityEngine;
using System.Collections;

public class LevelClearbg : MonoBehaviour {
	
	public exSprite thispic;
	public exAtlas pier1bg, salonbg, gparabg, pier2bg, pier3bg;
	
	public Color fade = new Color(255,255,255, 0.3f);

	
	// Use this for initialization
	void Start () {
		
		if (PlayerPrefs.GetInt("Currentlevel") == 1)
		thispic.SetSprite(salonbg, 0, true);
		
		if (PlayerPrefs.GetInt("Currentlevel") == 2)
		thispic.SetSprite(pier1bg, 0, true);
		
		if (PlayerPrefs.GetInt("Currentlevel") == 3)
		thispic.SetSprite(pier3bg, 0, true);
		
		if (PlayerPrefs.GetInt("Currentlevel") == 4)
		thispic.SetSprite(pier2bg, 0, true);
		 
	
	}
	
	// Update is called once per frame
	void Update () {
		
		thispic.color = fade;
	
	}
}
