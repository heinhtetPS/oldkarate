using UnityEngine;
using System.Collections;

public class LevelSelect : MonoBehaviour {
	
	private Vector2 mousepos = new Vector2();
	public GUIStyle empty;
	
	//Level boxes
	private Rect Act = new Rect(100, 80, 583, 80);
	private Rect Lvl1 = new Rect(70, 200, 644, 70);
	private Rect Lvl2 = new Rect(70, 280, 644, 70);
	private Rect Lvl3 = new Rect(70, 360, 644, 70);
	private Rect Lvl4 = new Rect(70, 440, 644, 70);
	private Rect boss1 = new Rect(70, 520, 644, 70);
	
	//Level details boxes
	private Rect picbox = new Rect(Screen.width / 2 + 180, 170, 320, 200);
	private Rect posterbox = new Rect(Screen.width / 2 + 120, 140, 438, 468);
	private Rect description = new Rect(Screen.width / 2 + 160, 380, 360, 200);
	private Rect scorebox = new Rect(Screen.width / 2 + 180, 480, 200, 50);
	private Rect combobox = new Rect(Screen.width / 2 + 180, 500, 480, 50);
	private Rect rankbox = new Rect(Screen.width / 2 + 180, 540, 200, 50);
	
	//other
	private Rect moneybase = new Rect(Screen.width / 2 + 210, 60, 22, 22);
	
	private Rect todojo = new Rect(Screen.width / 2 - 290, Screen.height - 70, 580, 68);
	
	public GUISkin levelselectskin, levelselect2;
	public GUIStyle comicstyleblack, comicstylewhite;
	private bool leveldetails = false, demoon = false;
	private int chosenlevel;
	
	public Texture2D topright, pic, textbox, kog, graybox, demopic, afrobust, rb1, rb2, rb3, rb4, rb5, rb6, 
	coin, redsc, greensc, bluesc, blacksc, whitesc, leftarrow, rightarrow, wood;
	public GameObject blackbg;
	
	//button flashes
	private bool dojoflash, optionsflash, exitflash, dojobuttonp = false, optionsbuttonp = false, exitbutp = false;
	private bool lvl1flash, lvl2flash, lvl3flash, lvl4flash, lvl5flash, lvl1bp = false, lvl2bp = false, lvl3bp = false, lvl4bp = false, lvl5bp = false;
	private float flashtimer;
	
	//rainbow
	private int rainbownumber = 0;
	private bool rainbowon = false;
	private float rainbowtimer = 0;
	
	public Texture2D[] levelpics = new Texture2D[]{};
	
	private string[] leveldescriptions = new string[]
	{
		"THIS IS A PLACEHOLDER, NUMBER ZERO.",
		"Rick wakes up, crazy shit happens.",
		"Take a ride on the pain train.",
		"Rick is a screw up and now he has to fix it.",
		"Rick's search brought him to this sketchy place."
	};
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		mousepos = new Vector2(Input.mousePosition.x - 5, Screen.height - Input.mousePosition.y - 5);
		
		//flashing 
		if (dojobuttonp)
		{
			flashtimer += Time.deltaTime;	
			if (flashtimer > 0.05f)
			{
				dojoflash = !dojoflash;	
				flashtimer = 0;
			}
		}
		
		if (optionsbuttonp)
		{
			flashtimer += Time.deltaTime;	
			if (flashtimer > 0.05f)
			{
				optionsflash = !optionsflash;	
				flashtimer = 0;
			}
		}
		
		if (exitbutp)
		{
			flashtimer += Time.deltaTime;	
			if (flashtimer > 0.05f)
			{
				exitflash = !exitflash;	
				flashtimer = 0;
			}
		}
		
		if (lvl1bp)
		{
			flashtimer += Time.deltaTime;	
			if (flashtimer > 0.05f)
			{
				lvl1flash = !lvl1flash;	
				flashtimer = 0;
			}
		}
		
		if (lvl2bp)
		{
			flashtimer += Time.deltaTime;	
			if (flashtimer > 0.05f)
			{
				lvl2flash = !lvl2flash;	
				flashtimer = 0;
			}
		}
		
		if (lvl3bp)
		{
			flashtimer += Time.deltaTime;	
			if (flashtimer > 0.05f)
			{
				lvl3flash = !lvl3flash;	
				flashtimer = 0;
			}
		}
		
		if (lvl4bp)
		{
			flashtimer += Time.deltaTime;	
			if (flashtimer > 0.05f)
			{
				lvl4flash = !lvl4flash;	
				flashtimer = 0;
			}
		}
		
		if (lvl5bp)
		{
			flashtimer += Time.deltaTime;	
			if (flashtimer > 0.05f)
			{
				lvl5flash = !lvl5flash;	
				flashtimer = 0;
			}
		}
		
		
		//rainbow activation
		if (PlayerPrefs.GetInt("Newabilitynotice") > 0)
			rainbowon = true;
		else rainbowon = false;
		
		//rainbow
		if (rainbowon)
		{
			rainbowtimer += Time.deltaTime;
			
			if (rainbowtimer > 0.02f)
			{
				rainbownumber++;
				if (rainbownumber > 6)
					rainbownumber = 1;
				rainbowtimer = 0;
			}
		}
	
	}
	
	
	
	void Showleveldetails(int levelnumber)
	{
		leveldetails = true;
		GUI.DrawTexture(picbox, levelpics[chosenlevel]);
		GUI.Label(description, leveldescriptions[levelnumber], comicstyleblack);
		GUI.Label(scorebox, "Highscore:     " + PlayerPrefs.GetInt("Level_" + chosenlevel + "_Highscore"), comicstyleblack);
		GUI.Label(combobox, "Highest Combo:     " + PlayerPrefs.GetInt("Level_" + chosenlevel +"_Combo"), comicstyleblack);
		GUI.Label(rankbox, "Rank:      " + Gradingscale(PlayerPrefs.GetInt("Level_" + chosenlevel + "_Highscore")), comicstyleblack);	
	}
	
	IEnumerator Dofadeout(string level)
	{
		yield return new WaitForSeconds(0.5f);
		
		Instantiate(blackbg, new Vector3(0,0, -800), Quaternion.Euler(new Vector3(270, 0, 0)));
		
		yield return new WaitForSeconds(1);
		
		
		Application.LoadLevel(level);
	}
	
	string Gradingscale(int highscore)
	{
		
		if (highscore <= 0)
		return "N/A";
		
		if (chosenlevel == 1)
		{
			if (highscore < 5000)
				return "F";
			if (highscore >= 5000 && highscore < 10000)
				return "D";
			if (highscore >= 10000 && highscore < 20000)
				return "C";
			if (highscore >= 20000 && highscore < 30000)
				return "B";
			if (highscore >= 30000 && highscore < 48000)
				return "A";
			if (highscore >= 48000)
				return "S";
		}
		
		if (chosenlevel == 2)
		{
			if (highscore < 10000)
				return "F";
			if (highscore >= 10000 && highscore < 20000)
				return "D";
			if (highscore >= 20000 && highscore < 30000)
				return "C";
			if (highscore >= 30000 && highscore < 40000)
				return "B";
			if (highscore >= 40000 && highscore < 50000)
				return "A";
			if (highscore >= 50000)
				return "S";
		}
		
		if (chosenlevel == 3)
		{
			if (highscore < 20000)
				return "F";
			if (highscore >= 20000 && highscore < 30000)
				return "D";
			if (highscore >= 30000 && highscore < 40000)
				return "C";
			if (highscore >= 40000 && highscore < 50000)
				return "B";
			if (highscore >= 50000 && highscore < 60000)
				return "A";
			if (highscore >= 60000)
				return "S";
		}
		
		if (chosenlevel == 4)
		{
			if (highscore < 20000)
				return "F";
			if (highscore >= 20000 && highscore < 30000)
				return "D";
			if (highscore >= 30000 && highscore < 40000)
				return "C";
			if (highscore >= 40000 && highscore < 50000)
				return "B";
			if (highscore >= 50000 && highscore < 60000)
				return "A";
			if (highscore >= 60000)
				return "S";
		}
		
		
		
		return null;
		
	}
	
	
	void OnGUI ()
	{
		if (GameObject.FindGameObjectWithTag("Fader") == null)
		{
			
		GUI.skin = levelselectskin;
		
		GUI.Button(new Rect(Act.x - 49, Act.y + 12, 53, 51), leftarrow, empty);
		GUI.Button(new Rect(Act.x + 580, Act.y + 12, 53, 51), rightarrow, empty);
		GUI.DrawTexture(Act, topright);
		
		GUI.Label(new Rect(Act.x + 100, Act.y + 15, 400, 100), "Act 1: Way of the Karate");
		GUI.DrawTexture(posterbox, textbox);
		
		//box for afrostats
		GUI.DrawTexture(new Rect(Screen.width - 572, 0, 572, 80), wood);
		
		//Levels-------------------------------------------------------------------
		//need gray boxes for locked levels
		
		//Level 1
			
		if (lvl1flash)
			GUI.skin = levelselect2;
		if (!lvl1flash)
			GUI.skin = levelselectskin;
		
		if (Lvl1.Contains(new Vector2(mousepos.x, mousepos.y)))
		{
			leveldetails = true;
			chosenlevel = 1;
		}
		if (GUI.Button(Lvl1, " Chapter 1: The Resurrection "))
		{
			lvl1bp = true;	
				
			if (PlayerPrefs.GetInt("Skipcomic") == 0)
			StartCoroutine ( Dofadeout ("Comicpg1") );
				
			if (PlayerPrefs.GetInt("Skipcomic") == 1)
			StartCoroutine ( Dofadeout ("Level1") );
			
		}

		if (GUI.Button(new Rect(Screen.width / 2 + 40, 210, 100, 25), "Reset"))
		{
			PlayerPrefs.SetInt("Level_1_Highscore", 0);
			PlayerPrefs.SetInt("Level_1_Combo", 0);
			PlayerPrefs.SetInt("Level_1_Rank", 0);
			PlayerPrefs.SetInt("Level_1_S", 0);
			PlayerPrefs.SetInt("Level_1_A", 0);
			PlayerPrefs.SetInt("Level_1_B", 0);
		}
			
		
		//Level 2
		if (lvl2flash)
			GUI.skin = levelselect2;
		if (!lvl2flash)
			GUI.skin = levelselectskin;
		if (PlayerPrefs.GetInt("Level2Unlock") > 0)
		{
			if (Lvl2.Contains(new Vector2(mousepos.x, mousepos.y)))
			{
				leveldetails = true;
				chosenlevel = 2;
			}
			if (GUI.Button(Lvl2, " Chapter 2: The Pain Train Express "))
			{
				lvl2bp = true;	
					
				if (PlayerPrefs.GetInt("Skipcomic") == 0)
				StartCoroutine ( Dofadeout ("Comicpg4") );
				
				if (PlayerPrefs.GetInt("Skipcomic") == 1)
				StartCoroutine ( Dofadeout ("Level2") );	
					
			}
	
			if (GUI.Button(new Rect(Screen.width / 2 + 40, 293, 100, 25), "Reset"))
			{
				PlayerPrefs.SetInt("Level_2_Highscore", 0);
				PlayerPrefs.SetInt("Level_2_Combo", 0);
				PlayerPrefs.SetInt("Level_2_Rank", 0);
				PlayerPrefs.SetInt("Level_2_S", 0);
				PlayerPrefs.SetInt("Level_2_A", 0);
				PlayerPrefs.SetInt("Level_2_B", 0);
			}
		}
//		if (PlayerPrefs.GetInt("Level2Unlock") == 0)
//			GUI.DrawTexture(Lvl2, graybox);
		
		//Level 3
		if (lvl3flash)
			GUI.skin = levelselect2;
		if (!lvl3flash)
			GUI.skin = levelselectskin;
		if (PlayerPrefs.GetInt("Level3Unlock") > 0)
		{
			if (Lvl3.Contains(new Vector2(mousepos.x, mousepos.y)))
			{
				leveldetails = true;
				chosenlevel = 3;
			}
			if (GUI.Button(Lvl3, " Chapter 3: Sunset Tribulation "))
			{
				lvl3bp = true;	
					
				if (PlayerPrefs.GetInt("Skipcomic") == 0)
				StartCoroutine ( Dofadeout ("Comicpg5") );
				
				if (PlayerPrefs.GetInt("Skipcomic") == 1)
				StartCoroutine ( Dofadeout ("Level3") );	
					
			}

			if (GUI.Button(new Rect(Screen.width / 2 + 40, 376, 100, 25), "Reset"))
			{
				PlayerPrefs.SetInt("Level_3_Highscore", 0);
				PlayerPrefs.SetInt("Level_3_Combo", 0);
				PlayerPrefs.SetInt("Level_3_Rank", 0);
				PlayerPrefs.SetInt("Level_3_S", 0);
				PlayerPrefs.SetInt("Level_3_A", 0);
				PlayerPrefs.SetInt("Level_3_B", 0);
			}
		}
			
			
		
		//Level 4
		if (lvl4flash)
			GUI.skin = levelselect2;
		if (!lvl4flash)
			GUI.skin = levelselectskin;
		if (PlayerPrefs.GetInt("Level4Unlock") > 0)
		{
			if (Lvl4.Contains(new Vector2(mousepos.x, mousepos.y)))
			{
				leveldetails = true;
				chosenlevel = 4;
			}
			
			if (GUI.Button(Lvl4, " Chapter 4: Gangsters' Paradise "))
				{
					lvl4bp = true;
					StartCoroutine ( Dofadeout ("Level4") );
					
				}
			
			if (GUI.Button(new Rect(Screen.width / 2 + 40, 459, 100, 25), "Reset"))
			{
				PlayerPrefs.SetInt("Level_4_Highscore", 0);
				PlayerPrefs.SetInt("Level_4_Combo", 0);
				PlayerPrefs.SetInt("Level_4_Rank", 0);
				PlayerPrefs.SetInt("Level_4_S", 0);
				PlayerPrefs.SetInt("Level_4_A", 0);
				PlayerPrefs.SetInt("Level_4_B", 0);
			}
		}

	
		//Level 5 or boss?	
		if (lvl5flash)
			GUI.skin = levelselect2;
		if (!lvl5flash)
			GUI.skin = levelselectskin;
		if (PlayerPrefs.GetInt("Level4Unlock") > 0)
		{
			if (GUI.Button(boss1, "Boss coming soon! "))
			{
				
			}
		}
		
		
		
		//Level details inside box
		if (leveldetails)
		{
			Showleveldetails(chosenlevel);
			
		}
			
		if (demoon)
		{
			GUI.DrawTexture(new Rect(0,0, Screen.width, Screen.height), demopic);
			if (Input.anyKeyDown)
				demoon = false;
		}
		
			
		
		//DEV SHIT BETTER DELETE-----------------------------------------
		
		if (GUI.Button(new Rect(Screen.width - 260, 80, 140, 25), "Unlock all"))
		{
			PlayerPrefs.SetInt("Level2Unlock", 1);
			PlayerPrefs.SetInt("Level3Unlock", 1);
			PlayerPrefs.SetInt("Level4Unlock", 1);
		}
		
		if (GUI.Button(new Rect(Screen.width - 130, 80, 140, 25), "Lock all"))
		{
			PlayerPrefs.SetInt("Level2Unlock", 0);
			PlayerPrefs.SetInt("Level3Unlock", 0);
			PlayerPrefs.SetInt("Level4Unlock", 0);
		}
			
		if (GUI.Button(new Rect(Screen.width - 130, 100, 140, 25), "Sample"))
		{
			Application.LoadLevel("cinematic2");	
		}
		
		if (dojoflash)
			GUI.skin = levelselect2;
		if (!dojoflash)
			GUI.skin = levelselectskin;
		
		if (GUI.Button(todojo, " To Dojo "))
		{
			audio.Play();
			dojobuttonp = true;
			StartCoroutine ( Dofadeout ("Dojo") );
		}

		//bottom left
		if (optionsflash)
			GUI.skin = levelselect2;
		if (!optionsflash)
			GUI.skin = levelselectskin;
			
		if (GUI.Button(new Rect(todojo.x - 210, todojo.y, 200, 66), " Options "))
		{
			audio.Play();
			optionsbuttonp = true;
			StartCoroutine ( Dofadeout ("Optionsscreen") );
		}

		//bottom right	
		if (exitflash)
			GUI.skin = levelselect2;
		if (!exitflash)
			GUI.skin = levelselectskin;
		if (GUI.Button(new Rect(todojo.x + 590, todojo.y, 200, 66), "Exit Game"))
		{
			exitbutp = true;
			Application.Quit();
		}	
			
			//Moneys && Scrolls	
			GUI.skin = null;
		
			
			if (rainbowon)
			{
				
				shinynewtext(new Rect(todojo.x, todojo.y, 52, 17));
					
			}
			
		}//fader
		
	}
	
	void shinynewtext(Rect rectlocation)
	{
		if (rainbownumber == 1)
		GUI.DrawTexture(new Rect(rectlocation.x, rectlocation.y, 52, 17), rb1);
		
		if (rainbownumber == 2)
		GUI.DrawTexture(new Rect(rectlocation.x, rectlocation.y, 52, 17), rb2);
		
		if (rainbownumber == 3)
		GUI.DrawTexture(new Rect(rectlocation.x, rectlocation.y, 52, 17), rb3);
		
		if (rainbownumber == 4)
		GUI.DrawTexture(new Rect(rectlocation.x, rectlocation.y, 52, 17), rb4);
		
		if (rainbownumber == 5)
		GUI.DrawTexture(new Rect(rectlocation.x, rectlocation.y, 52, 17), rb5);
		
		if (rainbownumber == 6)
		GUI.DrawTexture(new Rect(rectlocation.x, rectlocation.y, 52, 17), rb6);	
		
	}
}
