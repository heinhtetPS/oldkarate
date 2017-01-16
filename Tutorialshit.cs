using UnityEngine;
using System.Collections;

public class Tutorialshit : MonoBehaviour {
	
	public Font myFont;
	public GUIStyle myStyle;
	public Level1Spawn spawnerscript;
	public Karateoboxnew obox;
	public pausemenu pausescript;
	public GUIicons guiscript;
	private GameObject Karateman;
	
	public GUISkin messages, objectives;
	
	public float timekeeper = 0f;
	public int axekick = 0, shoryu = 0, dkick = 0, LMB = 0, linkcombo = 0;
	
	private Rect textRect = new Rect(130, 100, 700, 70);
	private Rect textRect3lines = new Rect(110, 100, 700, 100);
	
	private Rect firstline = new Rect(Screen.width / 2 - 325, Screen.height - 100, 650, 90);
	private Rect secondline = new Rect(150, 100, 700, 90);
	private Rect thirdline = new Rect(130, 160, 700, 70);
	
	private Rect tutorialpic1 = new Rect(Screen.width / 2 + 260, Screen.height / 2 - 180, 200, 350);
	private Rect tutorialpic2 = new Rect(Screen.width / 2 + 260, Screen.height / 2 - 180, 200, 450);
	private Rect tutorialpic3 = new Rect(Screen.width / 2 + 260, Screen.height / 2 - 180, 200, 150);
	
	//parameter bools
	public bool welcomedone = false, ms1msgdone = false, ms2msgdone = false, movementcleared = false, enemyhere = false, tutpaused = false,
	firstdefeatone = false, GJdone = false, atk2msgdone = false, tryatkdone = false, tryatk2done = false, GJ2done = false, finishdone = false, linkcounter = false;
	
	//event bools
	private bool welcomeon = false, movement = false, movement2 = false, Theycome = false, attack1info = false, attack1info2 = false, attack1info3 = false, atk1inf2done = false,
	summonedonce = false, attack2info = false, tryattack2 = false, axeckickcounter = false, GJ = false, moreheavy = false, GJ2 = false,
	finishit = false, bgmplayed = false, linkdone = false, extraline = false;
	public bool levelstarted = false;
	
	public Texture2D enterimg, enterimg2;
	private bool pressenter = false, flashing = false;
	private float flashtimer = 0;
	
	private int tutorialnumber = 0;
	
	//typewriter
	private string fullstring;
	private string currentstring;
	
	private int stringcounter = 0, tutorialindex = 0;
	private float stringtimer;
	
	public bool firsttime = true, doonce = false, audioplayed = false, RMBdone = false;
	private float audiodelay;
	public GameObject disabler, arrow, movementtest;
	movementtest movescript;
	public bool SpawnerOn = false;
	
	public AudioClip bgm, tutorialm, goodjob;
	
	private string[] tutorialtext = new string[]
	{
		"So...how do you feel, Rick?\nTry using the WSAD keys to move.",
		"Achieving air control is an essential skill \nif you intend to survive in this depraved world.",
		"Try it! A and D move you horizontally.\nThe W and S keys are used as a Double Jump and Quick Descent respectively.",
		"Steel yourself! Those delinquents have returned. \nUse LMB to attack them.",
		"If using mouse controls, you will attack towards your cursor. Otherwise, you will attack towards the last direction inputted.\nKeep those hooligans in your sights!",
		"After 4 hits, most punks will get knocked down. Hitting them in this state will deal less damage each hit.",
		"Soon, you will learn more efficient ways of fighting, but for now, show me you can handle the basics.",
		"Enough!\nYour progress is satisfactory. Let us continue forward.",
		"Use RMB + a direction to use a heavy attack.\nHeavy attacks are slower but provide utility.",
		"For example, RMB + left or right will cause a flaming kick that goes though enemies. Show me what you can do!",
		"This is the final test, and the most important one. You must master this in order to continue pursuing the way of the Karate.\nIt would be prudent to pay heed.",
		"Using momentum from LMB attacks, you can make faster heavy attack executions. Mix heavys with normal attacks to make linked combos!",
		"Now you can make short work of the rest of these punks!"
	};

	// Use this for initialization
	void Start () 
	{
		Karateman = GameObject.FindGameObjectWithTag("Player");
		spawnerscript = (Level1Spawn)GameObject.FindGameObjectWithTag("Spawner").GetComponent("Level1Spawn");
		disabler = GameObject.FindGameObjectWithTag("Tutorial");
		
		if (PlayerPrefs.GetInt("Level2Unlock") == 1)
			firsttime = false;
		
		if (!firsttime)
		{
			Startlevel();
		}
		
		//welcome message
		if (firsttime)
		{
			StartCoroutine ( Wait3seconds () );
			audio.clip = tutorialm;
			fullstring = tutorialtext[tutorialindex];
			PlayBGM();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (firsttime)
		{
			timekeeper += Time.deltaTime;
			
			if (LMB > 10)
				LMB = 10;
			
			
			#region Tutorial messages timing & conditions------------------------------------------------
			
			//air control...
			if (!movementcleared && !enemyhere && welcomedone && !ms1msgdone)
			{
				movement = true;
				pressenter = true;
			}
			//try it out...
			if (ms1msgdone && !movementcleared && !enemyhere && !ms2msgdone)
				movement2 = true;
			//Here they come...
			if (timekeeper - Time.deltaTime >= 10 && movementcleared && !enemyhere)
			{
				StartCoroutine ( ToggleTheycome (5) );
				StartCoroutine ( tutorialpause (2) );
			}
			//attacks towards mouse...
			if (timekeeper - Time.deltaTime >= 5f && enemyhere && !firstdefeatone)
			{
				attack1info = true;
				if (pausescript.playerpause)
				pressenter = true;
				Summonarrow();
			}
			if (spawnerscript.firstdefeated)
			{
				//GJ for defeating...
				if (!firstdefeatone && !GJdone)
					GJstuff();
				//There's also RMB stuff...
				if (timekeeper - Time.deltaTime >= 0f && firstdefeatone && GJdone && !atk2msgdone)
				{
					attack2info = true;
					pressenter = true;
				}
				//For example, this and this, try it out!
				if (atk2msgdone && !tryatk2done && !axeckickcounter)
				Trydkick();
				//excellence
				if (axeckickcounter && RMBdone && tryatk2done)
				thegj2();
				
				//finish it
				if (linkdone && RMBdone && !finishdone)
				{
					StartCoroutine ( ToggleFinishit (5) );
					audio.Stop();
					PlayBGM();
				}
				
			}
		
		}
		
		if (!firsttime && !bgmplayed)
			PlayBGM();
		
		#endregion
		
		//flashing image
		if (pressenter)
		{
			flashtimer += Time.deltaTime;	
			if (flashtimer > 0.3f)
			{
				flashing = !flashing;	
				flashtimer = 0;
			}
		}
		
		
		//pressing enter stuff
		if (Input.GetKeyDown(KeyCode.Return))
		{
			if (pressenter)
			{
				if (stringcounter >= fullstring.Length)
				{
					tutorialnumber++;
					tutorialindex++;
					stringcounter = 0;
					fullstring = tutorialtext[tutorialindex];
					pressenter = false;
					flashing = false;
				}
				else
				{
					stringcounter = fullstring.Length;
					currentstring = fullstring;
				}
				
			}
		}
		
		if (tutorialnumber > 0)
			WriteString();
		
		if (welcomeon)
		{
			if (Input.GetKeyDown(KeyCode.Return))
			{
				welcomeon = false;
				welcomedone = true;
			}
			
		}
		
		if (tutpaused && atk1inf2done)
		{
			if (Input.GetKeyDown(KeyCode.Return))
			{
				LMB = 0;
				tutpaused = false;
				pausescript.playerpause = false;
			}
			
		}
		
		if (movement)
		{
			if (Input.GetKeyDown(KeyCode.Return))
			{
				movement = false;
				ms1msgdone = true;
				
			}
			
		}
		
		if (tutorialnumber == 3)
		{
			if (GameObject.FindGameObjectWithTag("Test") == null)
			Instantiate (movementtest, Vector3.zero, Quaternion.identity);
			movescript = (movementtest)movementtest.GetComponent("movementtest");
			
			if (movementcleared)
			{
				if (!audioplayed)
				audio.PlayOneShot(goodjob);
				tutorialnumber = 4;
				audioplayed = true;
			}
			
		}
		
		if (attack1info && !attack1info2 && !attack1info3)
		{
			
			if (Input.GetKeyDown(KeyCode.Return))
			{
				attack1info2 = true;
			}
			
			
		}
		
		if (attack1info2 && !atk1inf2done)
		{
			
			if (Input.GetKeyDown(KeyCode.Return))
			{
				attack1info2 = false;
				atk1inf2done = true;
			}
			
		}
		
		if (GJ)
		{
			if (Input.GetKeyDown(KeyCode.Return))
			{
				GJ = false;
				GJdone = true;
				timekeeper = 0;
				firstdefeatone = true;
				
			}
			
		}
		
		if (attack2info)
		{
			if (Input.GetKeyDown(KeyCode.Return))
			{
				attack2info = false;
				atk2msgdone = true;
				dkick = 0; axekick = 0; shoryu = 0;
			}
			
		}
		
		if (tryattack2)
		{
			if (Input.GetKeyDown(KeyCode.Return))
			{
				tryattack2 = false;
				tryatk2done = true;
				spawnerscript.wave = 0;
				SpawnerOn = true;
				
			}
			
		}
		
		if (GJ2)
		{
			if (Input.GetKeyDown(KeyCode.Return))
			{
				extraline =  true;
				GJ2 = false;
			}
			
		}
		
		if (extraline)
		{
			if (Input.GetKeyDown(KeyCode.Return))
			{
				GJ2 = false;
				GJ2done = true;
				linkcounter = true;
				
			}
			
		}
		
		if (audioplayed)
		{
			audiodelay += Time.deltaTime;	
			if (audiodelay > 2f)
			{
				audioplayed = false;	
				audiodelay = 0;
			}
			
		}
		
		// player conditions to fulfill
		
		if (dkick > 3)
			dkick = 3;
		if (axekick > 3)
			axekick = 3;
		if (shoryu > 3)
			shoryu = 3;
		
		
		if (dkick == 3 && axekick == 3 && shoryu == 3)
			RMBdone = true;
		
		if (linkcombo > 5)
			linkcombo = 5;
		if (linkcombo == 5)
		{
			linkdone = true;
			linkcounter = false;
		}
		

		
		//music stuff
			if (GameObject.FindGameObjectWithTag("Tutorial") != null)
			{
				audio.clip = tutorialm;
				if (!audio.isPlaying)
				PlayBGM();
			}
			if (GameObject.FindGameObjectWithTag("Tutorial") == null)
			{
				audio.clip = bgm;
				if (!audio.isPlaying)
				PlayBGM();
			}
		
		
		if (finishdone && GameObject.FindGameObjectWithTag("Enemy") == null)
			Startlevel();
		
	}//END | UPdate
	
	void Summonarrow()
	{
		if (!summonedonce)
		{
			Instantiate(arrow, Vector3.zero, Quaternion.identity);
			summonedonce = true;
		}
		
	}
	
	IEnumerator tutorialpause(float time)
	{
		yield return new WaitForSeconds(time);	
		
		pausescript.playerpause = true;
		tutpaused = true;
	}
	
	IEnumerator Wait3seconds ()
	{
		yield return new WaitForSeconds(3);
		
		Welcome();
		
	}
	
	IEnumerator ToggleWelcome (float time)
	{
		welcomeon = true;
		
		
		yield return new WaitForSeconds(time);
		
		welcomeon = false;
		welcomedone = true;
		
	}
	
	void Welcome()
	{
		tutorialnumber = 1;
		pressenter = true;
	}
	
	IEnumerator ToggleMovement (float time)
	{
		movement = true;
		
		
		yield return new WaitForSeconds(time);
		
		movement = false;
		ms1msgdone = true;
		
		
	}
	
	
	IEnumerator ToggleMovement2 (float time)
	{
		movement2 = true;
		
		yield return new WaitForSeconds(time);
		
		
		movement2 = false;
		ms2msgdone = true;
		
		
	}
	
	IEnumerator ToggleTheycome (float time)
	{
		Theycome = true;
		movement2 = false;
		ms2msgdone = true;
		movement = false;
		ms1msgdone = true;
		timekeeper = 0;
		enemyhere = true;
		SpawnerOn = true;
		spawnerscript.wave = -1;
		
		
		yield return new WaitForSeconds(time);
		
		Theycome = false;
		
	}
	
	IEnumerator Toggleatkinfo (float time)
	{
		attack1info = true;
		timekeeper = 0;
		
		if (firstdefeatone)
			attack1info = false;
		
		
		yield return new WaitForSeconds(time);
		

		attack1info = false;
		
		
	}
	
	IEnumerator ToggleGJ (float time)
	{
		GJ = true;
		attack1info = false;
		killall();
		
		yield return new WaitForSeconds(time);
		
		GJ = false;
		GJdone = true;
		timekeeper = 0;
		firstdefeatone = true;
		
	}
	
	void GJstuff()
	{
		GJ = true;
		pressenter = true;
		attack1info = false;	
		Destroy(GameObject.FindGameObjectWithTag("Tutorialonly"));
		killall();
	}
	
	IEnumerator Toggleatkinfo2 (float time)
	{
		attack2info = true;
		
		yield return new WaitForSeconds(time);
		
		attack2info = false;
		atk2msgdone = true;
		
	}
	
	IEnumerator ToggleTryattack2 (float time)
	{
		tryattack2 = true;
		dkick = 0;
		axeckickcounter = true;
		
		yield return new WaitForSeconds(time);
		
		tryattack2 = false;
		tryatk2done = true;
		spawnerscript.wave = 0;
		SpawnerOn = true;
		
	}
	
	void Trydkick()
	{
		tryattack2 = true;
		pressenter = true;
		dkick = 0;
		axeckickcounter = true;
		
	}
	
	IEnumerator ToggleMoreHeavy (float time)
	{
		moreheavy = true;
		
		yield return new WaitForSeconds(time);
		
		moreheavy = false;
		
	}
	
	IEnumerator ToggleGJ2 (float time)
	{
		axeckickcounter = false;
		GJ2 = true;
		tryattack2 = false;
		timekeeper = 0;
	
		
		yield return new WaitForSeconds(time);
		

		GJ2 = false;
		GJ2done = true;
		
	}
	
	void thegj2()
	{
		axeckickcounter = false;
		GJ2 = true;
		pressenter = true;
		tryattack2 = false;
		timekeeper = 0;
		
	}
	
	IEnumerator ToggleFinishit (float time)
	{
		finishit = true;
		extraline = false;
		spawnerscript.duringtrial = false;
		SpawnerOn = false;
		timekeeper = 0;
		
		yield return new WaitForSeconds(time);
		
		finishit = false;
		killall();
		finishdone = true;
		
		
		
	}
	
	void Startlevel()
	{
		if (!doonce)
		{
			guiscript.enabled = true;
			Destroy(disabler);
			levelstarted = true;
			spawnerscript.firstdefeated = true;
			spawnerscript.seconddefeated = true;
			spawnerscript.wave = 1;
			spawnerscript.MaxEnemies = 10;
			spawnerscript.enemyCount = 0;
			SpawnerOn = true;
			doonce = true;
		}
		
	}
	
	void PlayBGM()
	{
		if (!audio.isPlaying)
		audio.Play();	
		bgmplayed = true;
	}
	
	void PlayBGMtutorial()
	{
		if (!audio.isPlaying)
		audio.PlayOneShot(tutorialm);	
		bgmplayed = true;
	}
	
	void WriteString()
	{
		if (stringcounter < fullstring.Length)
		{
			stringtimer += Time.deltaTime;
			
			if (stringtimer > 0.05f)
			{
				currentstring = fullstring.Substring(0, stringcounter);
				stringcounter++;
				stringtimer = 0;	
			}
		}
		
	}
	
	void killall()
	{
		GameObject[] enemiesonscreen = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject[] g2onscreen = GameObject.FindGameObjectsWithTag("Enemy2");
		GameObject[] g3onscreen = GameObject.FindGameObjectsWithTag("Enemy3");
		GameObject[] shit1s = GameObject.FindGameObjectsWithTag("Ground");
		
		
		foreach (GameObject g in enemiesonscreen)
		{
			Punk1 enemyscript = (Punk1)g.GetComponent("Punk1");
			enemyscript.Bleedcash();
			enemyscript.comboedcount = 999;
			enemyscript.Takedamage(9999);
		}
		
		foreach (GameObject g2 in g2onscreen)
		{
			Punk2 enemyscript2 = (Punk2)g2.GetComponent("Punk2");	
			enemyscript2.Bleedcash();
			enemyscript2.comboedcount = 999;
			enemyscript2.Takedamage(9999);
		}
		
		foreach (GameObject g3 in g3onscreen)
		{
			Punk3 enemyscript3 = (Punk3)g3.GetComponent("Punk3");
			enemyscript3.Bleedcash();
			enemyscript3.comboedcount = 999;
			enemyscript3.Takedamage(9999);
		}
		
		foreach (GameObject s1 in shit1s)
		{
			EnemyGround enemyscript4 = (EnemyGround)s1.GetComponent("EnemyGround");	
			enemyscript4.health -= 9999;
		}
		
	}
	
	void OnGUI()
	{
		myStyle.font = myFont;
		GUI.skin = messages;
		
		GUI.Label(new Rect(0, 500, 400, 100), fullstring);
		
		if (!pausescript.pausemenud)
		{
			if (tutorialnumber == 1)
			{
				GUI.Label(firstline, currentstring);
				//how do you feel try wsad
				pressenter = true;
			}
			
			if (tutorialnumber == 2)
			{
				GUI.Label(firstline, currentstring);
				//air control is essential
				pressenter = true;
			}	
			
			if (tutorialnumber == 3)
			{
				GUI.Label(firstline, currentstring);
				//movement test is here
			}	
			
			if (tutorialnumber == 4)
			{
				GUI.Label(firstline, currentstring);
				//Here they come
				pressenter = true;
			}
			
			if (tutorialnumber == 5)
			{
				GUI.Label(firstline, currentstring);
				//LMB explanation
				pressenter = true;
				
			}
			
			if (tutorialnumber == 6)
			{
				GUI.Label(firstline, currentstring);
				//LMB test is here
				GUI.skin = objectives;
				GUI.Label(new Rect(0, 225, 280, 50), "Successful Attack: " + LMB + " / 10");
				GUI.skin = messages;
			}
			
			if (tutorialnumber == 7)
			{
				GUI.Label(firstline, currentstring);
				pressenter = true;
			}
			
			if (tutorialnumber == 8)
			{
				GUI.Label(firstline, currentstring);
				
				GUI.color = Color.white;

			}
			
			if (tutorialnumber == 9)
			{
				GUI.Label(firstline, currentstring);
				
				GUI.color = Color.white;
//				GUI.DrawTexture(tutorialpic3, dkickpic);
			}
			
			if (axeckickcounter && tryattack2)
			{
				GUI.skin = objectives;
				GUI.Label(new Rect(0, 225, 300, 90), 
						"Left or Right + RMB: " + dkick + " / 3" + 
						"\nDown + RMB: " + axekick + " / 3" + 
						"\nUp + RMB: " + shoryu + " / 3");


				GUI.skin = messages;
			}
		
			if (axeckickcounter && !tryattack2)
			{
				GUI.skin = objectives;
				GUI.Label(new Rect(0, 160, 300, 90), 
						"Left or Right + RMB: " + dkick + " / 3" + 
						"\nDown + RMB: " + axekick + " / 3" + 
						"\nUp + RMB: " + shoryu + " / 3");
				GUI.skin = messages;
			}
			
			if (tutorialnumber == 10)
			{
				GUI.Label(firstline, currentstring);
			}
		
			if (tutorialnumber == 11)
			{
				GUI.Label(firstline, currentstring);
			}
		
			if (linkcounter)
			{
				GUI.skin = objectives;
				GUI.Label(new Rect(0, 160, 280, 40), "Linked Combos: " + linkcombo + " / 5");
				GUI.skin = messages;
			}
			
			if (tutorialnumber == 12)
			{
				GUI.Label(firstline, currentstring);
				//now take care of the rest of these punks
			}
		 
			if (pressenter)
			{
				if (flashing)
				GUI.DrawTexture(new Rect(firstline.x + 590, firstline.y + 65, 47, 18), enterimg);
			
				if (!flashing)
				GUI.DrawTexture(new Rect(firstline.x + 590, firstline.y + 65, 47, 18), enterimg2);
			
			}
		}
	}
}
