using UnityEngine;
using System.Collections;

public class pausemenu : MonoBehaviour {

	public bool paused = false, playerpause = false, wayofthekarate = false;
	public bool pausemenud = false, showtips = false;
	
	Player playerscript;
	
	private Vector2 mousepos;
	
	private Rect fullscreen = new Rect(0,0, Screen.width, Screen.height);
	private Rect optionbox1 = new Rect();
	private Rect optionbox2 = new Rect();
	private Rect optionbox3 = new Rect();
	private Rect optionbox4 = new Rect();
	private Rect optionbox5 = new Rect();
	private Rect optionbox6 = new Rect();
	private Rect text1 = new Rect();
	private Rect text2 = new Rect();
	private Rect text3 = new Rect();
	private Rect text4 = new Rect();
	private Rect text5 = new Rect();
	private Rect text6 = new Rect();
	private Rect yyball = new Rect();
	
	//way of the karate
	private string karatetext;
	private Rect picbox = new Rect(Screen.width / 2 - 140, 90, 250, 200);
	private Rect descriptionb = new Rect(Screen.width / 2 - 235, 320, 423, 296);
	private Rect topicsheader = new Rect(20, 20, 200, 50);
	private Rect textcontent = new Rect(Screen.width / 2 - 205, 350, 360, 250);
	
	private float boxmover1, boxmover2, boxmover3, boxmover4, boxmover5, boxmover6, verticalmover;
	private bool ballon1 = false, ballon2 = false, ballon3 = false, ballon4 = false, ballon5 = false, ballon6 = false;
	
	public Font myFont;
	public GUIStyle myStyle, nullstyle, comic;
	public GUISkin pauseskin;
	public Texture2D pausebackg, optionbox, resume, yy, levelselect, moves, karate, descriptionbox, pic, exit, rb1, rb2, rb3, rb4, rb5, rb6;
	public GameObject blackbg;
	
	//rainbow
	private int rainbownumber = 0;
	private bool rainbowon = false;
	private float rainbowtimer = 0;
	
	public Texture2D[] tutorialpics = new Texture2D[]{};
	
	private string[] karatetips = new string[]
	{
		"THIS IS A PLACEHOLDER, NUMBER ZERO.",
		
		"Press the Left Mouse Button to execute a normal attack. Rick will attack in the direction that the mouse is located. If your mouse is to the left of Rick, he will attack left and vice-versa. " +
		"\n\nNormal attacks will hurt most enemies but it is not recommended to use them exclusively.",
		
		"Press the Right Mouse button and any of the WSAD direction keys to perform one of three unique attacks: Dragonkick, Thunder Strike, and Rising Falcon. " +
		"\n\nEach unique attack can only be done once in the air, unless Rick hits an enemy three times or bounces off the ground.",
		
		"Dragon kick: Rick performs a kick that lets him slash through the air in the specified direction. This move lets Rick pierce through enemies. " +
		"\n\nGreat for both offensive and positioning purposes. ",
		
		"Thunder strike: Rick smashes his opponent down onto the ground, causing impact damage to enemies on the floor. Addtionally, the enemy will bounce back in front of Rick, yielding extra combo opportunities. " +
		"\n\nThunder strike is also the only Unique attack that resets Normal attack damage scaling.",
		
		"Rising Falcon: Rick releases a thrusting uppercut with dreadful force, knocking down any regular punk that is caught by it. It is also the fastest traveling Heavy attack. " +
		"\n\nExtremely useful to when trying to avoid threats traveling horizontally.",
		
		"Pressing the Left Mouse button continuously will make Rick perform a 3-hit combo string. Ricks combo count is displayed on the top right and attacking enemies continuously will increase this number, adding to your score. " +
		"\n\nHowever, after the 4th normal attack hit on the same enemy, Ricks damage will be reduced and combo count will cease to advance. Therefore, it is recommended to mix heavy attacks with normal attacks.",
		
		"Executing a heavy attack right after a normal attack will result in a linked heavy. Linked heavys are faster versions of heavy attacks meant to be used to chain strings of normal attacks together." +
		"\n\nCombining normal attacks and heavy attacks will allow Rick to make devastating combos. Experiment with them!",
		
		"By pressing the corresponding button for the assigned skill slot (seen in the top right), Rick will perform the skill equipped in the slot. Different skills will have different costs associated to them. " +
		"\n\nBody skills do not cost and are mostly used to aid Rick with combos. Tech skills cost a moderate amount of chi but require some strategy to use well. Mind skills cost the most chi but they are the most devastating sets of moves. Equip the skills in the Dojo skill customization menu!",
		
		"Rick performs a specific special strike assigned from the Dojo customization menu. Each special strike has a unique effect and will cost a different amount. " +
		"\n\nA Melee-range special strike move can usually be implemented in between normal attack combos and will return half its cost if it lands. ",
		
		"By pressing the W key, Rick performs a double jump which lets him go up by small amount. Can only be done once in the air, unless Rick hits an enemy three times or bounces off the ground. " +
		"\n\nBy pressing the S key, Rick quickly descends down to the ground.",
		
		"By quickly double-tapping on A or D, Rick performs a rapid horizontal dash. " +
		"\n\nThis can only be done once in the air, unless Rick hits an enemy three times or bounces off the ground.",
		
		"Pressing and holding the SHIFT key puts Rick in his blocking stance. While blocking , Rick receives no damage from the direction he is facing. " +
		"\n\nHowever, this action reduces the block gauge and once it depletes, Rick is stunned for 2 seconds. Blocking also slows his travel speed allowing for more precise movement.",
		
		"Using block just before Rick is hit with an attack will result in Rick performing a parry on the enemy. When Rick parries the enemy’s attack, Rick will get one Chi bar and the enemy will be stunned for 1 second. " +
		"\n\nIt’s a great way to start off a combo from a difficult situation.",
		
		"The yellow bar indicates your health. When this bar is depleted, Rick loses a life. When Rick takes damage, you will notice a red bar. Red bar is recoverable health. By eating health recovery items such as sushi, Rick can recover red life. " +
		"\n\nRick can also acquire temporary bonus (Green) health by eating sushi while at maximum health."
	};
	
	void Start () 
	{
		if (GameObject.FindGameObjectWithTag("Player") != null)
		playerscript = (Player)GameObject.FindGameObjectWithTag("Player").GetComponent("Player");
	
	}
	

	void Update ()
	{
		if (Input.GetKeyDown("escape"))
		{
			pausemenud = !pausemenud;
			wayofthekarate = false;
			boxmover1 = 0;
			boxmover2 = 0;
			boxmover3 = 0;
			boxmover4 = 0;
			boxmover5 = 0;
			boxmover6 = 0;
			verticalmover = 0;
		}
		
		mousepos = new Vector2(Input.mousePosition.x - 5, Screen.height - Input.mousePosition.y - 5);
		
		
		if (pausemenud)
		{
			Time.timeScale = 0;
			moveboxesin();
		}
		
		
		if (!pausemenud && !paused && playerscript.state != Player.State.Explosion)
		{
			Time.timeScale = 1;
			boxmover1 = 0;
			boxmover2 = 0;
			boxmover3 = 0;
			boxmover4 = 0;
			boxmover5 = 0;
			boxmover6 = 0;
			verticalmover = 0;
		}
		
		optionbox1 = new Rect (-451 + boxmover1, Screen.height / 2 - 200 + verticalmover, 451, 62);
		optionbox2 = new Rect (-451 + boxmover2, Screen.height / 2 - 124 + verticalmover, 451, 62);
		optionbox3 = new Rect (-451 + boxmover3, Screen.height / 2 - 48 + verticalmover, 451, 62);
		optionbox4 = new Rect (-451 + boxmover4, Screen.height / 2 + 28 + verticalmover, 451, 62);
		optionbox5 = new Rect (-451 + boxmover5, Screen.height / 2 + 104 + verticalmover, 451, 62);
		optionbox6 = new Rect (-451 + boxmover5, Screen.height / 2 + 180 + verticalmover, 451, 62);
		
		text1 = new Rect (-451 + 80 + boxmover1, Screen.height / 2 - 200 + verticalmover, 451, 62);
		text2 = new Rect (-451 + 30 + boxmover2, Screen.height / 2 - 124 + verticalmover, 451, 62);
		text3 = new Rect (-451 + 30 + boxmover3, Screen.height / 2 - 48 + verticalmover, 451, 62);
		text4 = new Rect (-451 - 10 + boxmover4, Screen.height / 2 + 28 + verticalmover, 451, 62);
		text5 = new Rect (-451 + 50 + boxmover5, Screen.height / 2 + 104 + verticalmover, 451, 62);
		text6 = new Rect (-451 + 50 + boxmover6, Screen.height / 2 + 180 + verticalmover, 451, 62);
		
		//Mouseover boxes for YY
		if (optionbox1.Contains(new Vector2(mousepos.x, mousepos.y)))
			ballon1 = true;
		else ballon1 = false;
		if (optionbox2.Contains(new Vector2(mousepos.x, mousepos.y)))
			ballon2 = true;
		else ballon2 = false;
		if (optionbox3.Contains(new Vector2(mousepos.x, mousepos.y)))
			ballon3 = true;
		else ballon3 = false;
		if (optionbox4.Contains(new Vector2(mousepos.x, mousepos.y)))
			ballon4 = true;
		else ballon4 = false;
		if (optionbox5.Contains(new Vector2(mousepos.x, mousepos.y)))
			ballon5 = true;
		else ballon5 = false;
		if (optionbox6.Contains(new Vector2(mousepos.x, mousepos.y)))
			ballon6 = true;
		else ballon6 = false;
		
		
		//rainbow activation
		if (PlayerPrefs.GetInt("Newabilitynotice") > 0)
			rainbowon = true;
		else rainbowon = false;
		
		//rainbow
		if (rainbowon)
		{
			rainbowtimer += 0.02f;
			
			if (rainbowtimer > 0.1f)
			{
				rainbownumber++;
				if (rainbownumber > 6)
					rainbownumber = 1;
				rainbowtimer = 0;
			}
		}
	
	}//end of update
	
	void moveboxesin()
	{
		if (boxmover1 <= 451)
		{
			boxmover1 += 12f;
			if (boxmover1 > 451)
				boxmover1 = 451;
		}
		if (boxmover2 <= 451)
		{
			boxmover2 += 11f;
			if (boxmover2 > 451)
				boxmover2 = 451;
		}
		if (boxmover3 <= 451)
		{
			boxmover3 += 10f;
			if (boxmover3 > 451)
				boxmover3 = 451;
		}
		if (boxmover4 <= 451)
		{
			boxmover4 += 9f;
			if (boxmover4 > 451)
				boxmover4 = 451;
		}
		if (boxmover5 <= 451)
		{
			boxmover5 += 8f;
			if (boxmover5 > 451)
				boxmover5 = 451;
		}
		if (boxmover6 <= 451)
		{
			boxmover6 += 7f;
			if (boxmover6 > 451)
				boxmover6 = 451;
		}
	}
	
	void movealloutexceptkarate()
	{
		if (boxmover1 >= -451)
		{
			boxmover1 -= 12;
			if (boxmover1 < -451)
				boxmover1 = -451;
		}
		
		if (boxmover2 >= -451)
		{
			boxmover2 -= 12;
			if (boxmover2 < -451)
				boxmover2 = -451;
		}
		
		if (boxmover3 >= -451)
		{
			boxmover3 -= 12;
			if (boxmover3 < -451)
				boxmover3 = -451;
		}
		
		if (boxmover5 >= -451)
		{
			boxmover5 -= 12;
			if (boxmover5 < -451)
				boxmover5 = -451;
		}
		
		if (boxmover6 >= -451)
		{
			boxmover6 -= 12;
			if (boxmover6 < -451)
				boxmover6 = -451;
		}
		
		
		
			verticalmover -= 8;
			if (verticalmover < -328)
				verticalmover = -328;
		
		
	}
	
	void revertvertical()
	{
		verticalmover += 8;
		if (verticalmover > 0)
			verticalmover = 0;
		
	}
	
	void OnGUI () 
	{
		myStyle.font = myFont;
		
		if (pausemenud)
		{
			GUI.DrawTexture(fullscreen, pausebackg);
			myStyle.fontSize = 30;
			GUI.color = Color.white;
			GUI.Label(new Rect(120, 50, 500, 150), "Game Paused", myStyle);

			GUI.skin = pauseskin;
			
				if (GUI.Button(optionbox1, "  "))
				{
					paused = false;
					pausemenud = false;
				}
			
				if (GUI.Button(optionbox2, " RESTART LEVEL "))
				{
					StartCoroutine( Dofadeout (PlayerPrefs.GetInt("Currentlevel")) );
				}
			
				if (GUI.Button(optionbox3, "  "))
				{
					StartCoroutine( Dofadeout ("LevelSelect") );
				}
			
				if (GUI.Button(optionbox4, " "))
				{
//					if (!wayofthekarate)
//					{
//						wayofthekarate = true;
//						return;
//					}
//				
//					if (wayofthekarate)
//					{
//						revertvertical();
//						if (verticalmover == 0)
//						wayofthekarate = false;
//						return;
//					}
					wayofthekarate = !wayofthekarate;
					if (verticalmover != 0)
					verticalmover = 0;
				}
			
				if (GUI.Button(optionbox5, " TO DOJO "))
				{
					StartCoroutine( Dofadeout ("Dojo") );
				}
			
				if (GUI.Button(optionbox6, "  "))
				{
					Application.Quit();
				}
			
			//text for boxes
			GUI.DrawTexture(text1, resume);
			GUI.DrawTexture(text3, levelselect);
//			GUI.DrawTexture(text3, resume);
			GUI.DrawTexture(text4, karate);
//			GUI.DrawTexture(text5, karate);
			GUI.DrawTexture(text6, exit);
			if (PlayerPrefs.GetInt("Dojotutorial") == 0 || PlayerPrefs.GetInt("Newabilitynotice") > 0)
				rainbowon = true;
				
				
//			GUI.DrawTexture(new Rect(text5.x + 380, text5.y, 48, 48), newshithere);
			
			
			//yy ball
			if (ballon1)
				GUI.DrawTexture(new Rect(optionbox1.x, optionbox1.y, 56, 56), yy);
			if (ballon2)
				GUI.DrawTexture(new Rect(optionbox2.x, optionbox2.y, 56, 56), yy);
			if (ballon3)
				GUI.DrawTexture(new Rect(optionbox3.x, optionbox3.y, 56, 56), yy);
			if (ballon4)
				GUI.DrawTexture(new Rect(optionbox4.x, optionbox4.y, 56, 56), yy);
			if (ballon5)
				GUI.DrawTexture(new Rect(optionbox5.x, optionbox5.y, 56, 56), yy);
			if (ballon6)
				GUI.DrawTexture(new Rect(optionbox6.x, optionbox6.y, 56, 56), yy);
			
			//windows
			if (wayofthekarate)
			{
				WayoftheKarate();
				
			}
			
			if (rainbowon)
			{
				shinynewtext(new Rect(text5.x + 380, text5.y, 52, 17));
				
			}
			
		}//if pause
		
		
	}
	
	IEnumerator Dofadeout(string level)
	{
		Instantiate(blackbg, new Vector3(0,0, -800), Quaternion.Euler(new Vector3(270, 0, 0)));
		pausemenud = false;
		
		
		
		yield return new WaitForSeconds(1);
		
		Application.LoadLevel(level);
	}
	
	IEnumerator Dofadeout(int level)
	{
		Instantiate(blackbg, new Vector3(0,0, -800), Quaternion.Euler(new Vector3(270, 0, 0)));
		pausemenud = false;
		
		yield return new WaitForSeconds(1);
		
		Application.LoadLevel(level);
	}
	
	void WayoftheKarate()
	{
		movealloutexceptkarate();
		
		GUI.skin = null;
		GUI.DrawTexture(descriptionb, descriptionbox);
		
		if (GUI.Button(new Rect(topicsheader.x, topicsheader.y + 165, topicsheader.width, topicsheader.height), "Normal Attacks"))
		{
			Querytip(1);
			
		}
		if (GUI.Button(new Rect(topicsheader.x, topicsheader.y + 215, topicsheader.width, topicsheader.height), "Heavy Attacks"))
		{
			Querytip(2);
			
		}
				if (GUI.Button(new Rect(topicsheader.x, topicsheader.y + 265, topicsheader.width - 50, topicsheader.height - 25), "Dragon Kick"))
				{
					Querytip(3);
					
				}
				if (GUI.Button(new Rect(topicsheader.x, topicsheader.y + 290, topicsheader.width - 50, topicsheader.height - 25), "Thunderstrike"))
				{
					Querytip(4);
					
				}
				if (GUI.Button(new Rect(topicsheader.x, topicsheader.y + 315, topicsheader.width - 50, topicsheader.height - 25), "Rising Falcon"))
				{
					Querytip(5);
					
				}
		if (GUI.Button(new Rect(topicsheader.x, topicsheader.y + 340, topicsheader.width, topicsheader.height), "Combos"))
		{
			Querytip(6);
			
		}
				if (GUI.Button(new Rect(topicsheader.x, topicsheader.y + 390, topicsheader.width - 50, topicsheader.height - 25), "Advanced Combos"))
				{
					Querytip(7);
					
				}
		if (GUI.Button(new Rect(topicsheader.x, topicsheader.y + 415, topicsheader.width, topicsheader.height), "Unique Skills"))
		{
			Querytip(8);
			
		}
				if (GUI.Button(new Rect(topicsheader.x, topicsheader.y + 465, topicsheader.width - 50, topicsheader.height - 25), "Special Strike"))
				{
					Querytip(9);
					
				}
		if (GUI.Button(new Rect(topicsheader.x, topicsheader.y + 490, topicsheader.width, topicsheader.height), "Double Jump/Descent"))
		{
			Querytip(10);
			
		}
				if (GUI.Button(new Rect(topicsheader.x, topicsheader.y + 540, topicsheader.width - 50, topicsheader.height - 25), "Dashing"))
				{
					Querytip(11);
					
				}
				if (GUI.Button(new Rect(topicsheader.x, topicsheader.y + 565, topicsheader.width - 50, topicsheader.height - 25), "Blocking"))
				{
					Querytip(12);
					
				}
				if (GUI.Button(new Rect(topicsheader.x, topicsheader.y + 590, topicsheader.width - 50, topicsheader.height - 25), "Parry"))
				{
					Querytip(13);
					
				}
		if (GUI.Button(new Rect(topicsheader.x, topicsheader.y + 615, topicsheader.width, topicsheader.height), "Health Colors"))
		{
			Querytip(14);
			
		}
		
		
		if (showtips)
		{
			GUI.color = Color.black;
			GUI.Label(textcontent, karatetext, comic);
			GUI.color =  Color.white;
			GUI.DrawTexture(picbox, pic);
		}
	}
	
	void Querytip(int topicnumber)
	{
		showtips = true;
		pic = tutorialpics[topicnumber];
		karatetext = karatetips[topicnumber];
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
