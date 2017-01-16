using UnityEngine;
using System.Collections;

public class slomocontrols : MonoBehaviour {
	
	public movedirection objectscript1, objectscript2;
	
	public moveandanim karatebust, cyrusbust;
	
	public moveandreset q, qq, qqq, qqqq, qqqqq, qqqqqq, qqqqqqq, ww, www, wwww;
	
	private bool titlenew = false, keypressed = false, messagewindow = false;
	public Texture2D logo, titlescreen, spinner;
	public GameObject spsun, blackbg;
	public Color fade;
	public Color fade2;
	public Color Redbg = new Color(50, 0 ,0 , 1);
	public float fadenum = 0f, fadenum2 = 0f, logonum = 0f;
	
	private Rect Menuloc = new Rect(Screen.width / 2 - 90, Screen.height / 2 + 180, 180, 30);
	private Rect popupbox = new Rect(Screen.width / 2 + 100, Screen.height / 2 + 100, 350, 120);
	private Rect Logorect = new Rect(Screen.width / 2 - 272, Screen.height / 2 - 230, 545, 250);
	
	public GUIStyle mystyle;
	public GUISkin empty;
	
	private float bgchange, eventtimer;
	
	public float face1change, face2change, slomoon, stoptime, screenchange;
	
	public AudioClip gsmash;
	
	// Use this for initialization
	void Start () {
		
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		fade2 = new Color(255, 255, 255, 0.1f + fadenum2);
		
		//timers
		bgchange += Time.deltaTime;
		eventtimer += Time.deltaTime;

		
		if (bgchange > 7)
		{
			camera.backgroundColor = Redbg;
			q.direction = "left";
			q.thesprite.HFlip();
			qq.direction = "left";
			qq.thesprite.HFlip();
			qqq.direction = "left";
			qqq.thesprite.HFlip();
			qqqq.direction = "left";
			qqqq.thesprite.HFlip();
			qqqqq.direction = "left";
			qqqqq.thesprite.HFlip();
			qqqqqq.direction = "left";
			qqqqqq.thesprite.HFlip();
			qqqqqqq.direction = "left";
			qqqqqqq.thesprite.HFlip();
			ww.direction = "left";
			ww.thesprite.HFlip();
			www.direction = "left";
			www.thesprite.HFlip();
			wwww.direction = "left";
			wwww.thesprite.HFlip();
		}
		
		//karateman mouth
		if (eventtimer > face1change)
		{
			karatebust.spriteanims.Play("afroface");
		}
		
		//cyrus mouth
		if (eventtimer > face2change)
		{
			cyrusbust.spriteanims.Play("cyrusface");
		}
		
		//slomo
		if (eventtimer > slomoon)
		{
			Time.timeScale = 0.3f;	
		}
		
		//stop fighters
		if (eventtimer > stoptime)
		{
			objectscript1.movespeed = 0;	
			objectscript2.movespeed = 0;
		}
		
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			titlenew = true;
			fadenum2 += 1f;
			
		}
		
		//fade out screen
		if (eventtimer > screenchange && fade2.a <= 255)
		{
			titlenew = true;
			fadenum2 += 0.01f;
		}
		
		
		if (titlenew)
		{	
			if (GameObject.FindGameObjectWithTag("Test") == null)
			{
				Instantiate(spsun, new Vector3(-20, 130, -135), Quaternion.identity);
				Instantiate(blackbg, new Vector3(0, 0, -50), Quaternion.Euler(new Vector3(-90, 0, 0)));
			}
			
			
			
		}
		
		if (titlenew && Input.GetKeyDown(KeyCode.Space))
		{
			keypressed = true;
			
		}
	
	}
	
	void OnGUI()
	{
		
		if (titlenew)
		{
			GUI.color = fade2;
			GUI.DrawTexture(Logorect, logo);
			
			
				GUI.color = Color.white;
				GUI.skin = empty;
				if (PlayerPrefs.GetInt("Firsttime") == 1)
				{
					GUI.skin = empty;
					if (GUI.Button(new Rect(Menuloc.x, Menuloc.y - 35, Menuloc.width, Menuloc.height), "Continue"))
					{
						Application.LoadLevel("LevelSelect");
						
					}
				}
				if (GUI.Button(Menuloc, "New Game"))
				{
					GUI.skin = empty;
					if (PlayerPrefs.GetInt("Firsttime") == 0)
					{
						InitializePprefs();
						Application.LoadLevel("LevelSelect");
						return;
					}
					
					if (PlayerPrefs.GetInt("Firsttime") == 1)
					{
						messagewindow = true;
					}
				}
				
				if (GUI.Button(new Rect(Menuloc.x, Menuloc.y + 35, Menuloc.width, Menuloc.height), "Options"))
				{
					Application.LoadLevel("Optionsscreen");
					
				}
				
				if (GUI.Button(new Rect(Menuloc.x, Menuloc.y + 70, Menuloc.width, Menuloc.height), "Exit"))
					Application.Quit();
			
			
			
			
			if (messagewindow)
			{
				GUI.Window(1, popupbox, genericmessage, "Are you sure?");		
				GUI.BringWindowToFront(1);
			}
			
		}//title page
	
	}
	
	void genericmessage(int windowID)
	{
		GUI.Label(new Rect(40, 20, 300, 100), "Starting a new game will overwrite your previous progress.");
		
		if (GUI.Button(new Rect(40, 80, 100, 25), "Yes"))
		{
			messagewindow = false;
			PlayerPrefs.SetInt("Firsttime", 0);
			InitializePprefs();
			Application.LoadLevel("LevelSelect");
		}
		
		if (GUI.Button(new Rect(220, 80, 100, 25), "No"))
		{
			messagewindow = false;	
			
		}
		
	}
	
	void InitializePprefs()
	{
		if (PlayerPrefs.GetInt("Firsttime") == 0)
		{
			PlayerPrefs.SetString("Slot1", "null");	
			PlayerPrefs.SetString("Slot2", "null");
			PlayerPrefs.SetString("Slot3", "null");
			PlayerPrefs.SetString("Slot4", "null");
			PlayerPrefs.SetString("Slot5", "null");
			
			//Body
			PlayerPrefs.SetInt("Groundsmashabilitystate", 0);
			PlayerPrefs.SetInt("Tackleabilitystate", 0);
			PlayerPrefs.SetInt("Forceabilitystate", 0);
			PlayerPrefs.SetInt("Hfistabilitystate", 0);
			PlayerPrefs.SetInt("Finalbodyabilitystate", 0);
			
			//MIND
			PlayerPrefs.SetInt("Hurricaneabilitystate", 0);
			PlayerPrefs.SetInt("Lazerabilitystate", 0);
			PlayerPrefs.SetInt("Thirdmindabilitystate", 0);
			PlayerPrefs.SetInt("Serenityabilitystate", 0);
			PlayerPrefs.SetInt("Warudoabilitystate", 0);
			
			//TECH
			PlayerPrefs.SetInt("Spiritbombabilitystate", 0);
			PlayerPrefs.SetInt("Teledoorabilitystate", 0);
			PlayerPrefs.SetInt("Cloneabilitystate", 0);
			PlayerPrefs.SetInt("Wallabilitystate", 0);
			PlayerPrefs.SetInt("Vplateabilitystate", 0);
			
			PlayerPrefs.SetInt("Sstrikeabilitystate", 0);
			
			//body
			PlayerPrefs.SetString("GroundsmashT1", "null");
			PlayerPrefs.SetString("GroundsmashT2", "null");
			
			PlayerPrefs.SetString("TackleT1", "null");
			PlayerPrefs.SetString("TackleT2", "null");
			
			PlayerPrefs.SetString("ForceT1", "null");
			PlayerPrefs.SetString("ForceT2", "null");
			
			PlayerPrefs.SetString("HfistT1", "null");
			PlayerPrefs.SetString("HfistT2", "null");
			
			PlayerPrefs.SetString("FinalbodyT1", "null");
			PlayerPrefs.SetString("FinalbodyT2", "null");
			
			//mind
			PlayerPrefs.SetString("HurricaneT1", "null");
			PlayerPrefs.SetString("HurricaneT2", "null");
			
			PlayerPrefs.SetString("LazerT1", "null");
			PlayerPrefs.SetString("LazerT2", "null");
			
			PlayerPrefs.SetString("ThirdmindT1", "null");
			PlayerPrefs.SetString("ThirdmindT2", "null");
			
			PlayerPrefs.SetString("SerenityT1", "null");
			PlayerPrefs.SetString("SerenityT2", "null");
			
			PlayerPrefs.SetString("WarudoT1", "null");
			PlayerPrefs.SetString("WarudoT2", "null");
			
			//tech
			PlayerPrefs.SetString("SpiritbombT1", "null");
			PlayerPrefs.SetString("SpiritbombT2", "null");
			
			PlayerPrefs.SetString("TeledoorT1", "null");
			PlayerPrefs.SetString("TeledoorT2", "null");
			
			PlayerPrefs.SetString("CloneT1", "null");
			PlayerPrefs.SetString("CloneT2", "null");
			
			PlayerPrefs.SetString("WallT1", "null");
			PlayerPrefs.SetString("WallT2", "null");
			
			PlayerPrefs.SetString("VplateT1", "null");
			PlayerPrefs.SetString("VplateT2", "null");
			
		
			
			PlayerPrefs.SetString("Sstrikerune", "null");
			
			PlayerPrefs.SetInt("Money", 0);
			PlayerPrefs.SetInt("Scrolls", 0);
			PlayerPrefs.SetInt("Redscroll", 0);
			PlayerPrefs.SetInt("Greenscroll", 0);
			PlayerPrefs.SetInt("Bluescroll", 0);
			PlayerPrefs.SetInt("Blackscroll", 0);
			PlayerPrefs.SetInt("Whitescroll", 0);
			
			PlayerPrefs.SetInt("Level_1_Highscore", 0);
			PlayerPrefs.SetInt("Level_2_Highscore", 0);
			PlayerPrefs.SetInt("Level_3_Highscore", 0);
			PlayerPrefs.SetInt("Level_4_Highscore", 0);
			
			PlayerPrefs.SetInt("Level_1_Combo", 0);
			PlayerPrefs.SetInt("Level_2_Combo", 0);
			PlayerPrefs.SetInt("Level_3_Combo", 0);
			PlayerPrefs.SetInt("Level_4_Combo", 0);
			
			PlayerPrefs.SetInt("Level_1_Rank", 0);
			PlayerPrefs.SetInt("Level_2_Rank", 0);
			PlayerPrefs.SetInt("Level_3_Rank", 0);
			PlayerPrefs.SetInt("Level_4_Rank", 0);
			
			PlayerPrefs.SetInt("Level2Unlock", 0);
			PlayerPrefs.SetInt("Level3Unlock", 0);
			PlayerPrefs.SetInt("Level4Unlock", 0);
			
			PlayerPrefs.SetInt("Skipcomic", 0);
			
			
			
			//EXP and stuff
			PlayerPrefs.SetInt("Playerlevel", 1);
			PlayerPrefs.SetFloat("XPtonextlevel", 400);
			PlayerPrefs.SetFloat("XPgained", 0);
			
			
			//level rewards
			PlayerPrefs.SetInt("Level_1_S", 0);
			PlayerPrefs.SetInt("Level_1_A", 0);
			PlayerPrefs.SetInt("Level_1_B", 0);
			PlayerPrefs.SetInt("Level_2_S", 0);
			PlayerPrefs.SetInt("Level_2_A", 0);
			PlayerPrefs.SetInt("Level_2_B", 0);
			PlayerPrefs.SetInt("Level_3_S", 0);
			PlayerPrefs.SetInt("Level_3_A", 0);
			PlayerPrefs.SetInt("Level_3_B", 0);
			PlayerPrefs.SetInt("Level_4_S", 0);
			PlayerPrefs.SetInt("Level_4_A", 0);
			PlayerPrefs.SetInt("Level_4_B", 0);
			
			//tutorial stuff
			PlayerPrefs.SetInt("Onetimedojonotice", 0);
			PlayerPrefs.SetInt("Newabilitynotice", 0);
			PlayerPrefs.SetInt("Dojotutorial", 0);
			
			//gameplay options
			PlayerPrefs.SetInt("Mousecontrols", 1);
			PlayerPrefs.SetInt("Autocombo", 0);
			
			PlayerPrefs.SetInt("Firsttime", 1);
		}
		
		
	}
}
