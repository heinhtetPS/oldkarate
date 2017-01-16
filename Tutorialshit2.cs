using UnityEngine;
using System.Collections;

public class Tutorialshit2 : MonoBehaviour {
	
	//UI Stuff
	public Font myFont;
	public GUIStyle myStyle;
	public GUISkin tutorial;
	
	private Rect textbox = new Rect(Screen.width / 2 - 325, Screen.height - 100, 650, 90);
	private Rect textboxtop = new Rect(Screen.width / 2 - 325, 100, 650, 90);
	private Rect objective1 = new Rect(0, 160, 280, 40);
	
	//grabbing scripts
	public Level2Spawn spawnerscript;
	public Karateoboxnew obox;
	public PlayerInfo infoscript;
	public GUIicons guiscript;
	public Player playerscript;
	public Camera main;
	private Vector3 zoomposition = new Vector3(0, 0, 0);
	
	public float timekeeper = 0f;
	
	//parameter bools
	private bool welcomeon = false, secondmessage = false, thirdmessage = false, fourthmessage = false, fifthmessage = false,
	sixthmessage = false, seventhmessage = false, eighthmessage = false, ninthmessage = false, tenthmessage = false, elventhmessage = false,
	twmessage = false, thmessage = false;
	
	//event bools
	public bool StartSpawner = false, welcomedone = false, seconddone = false, thirddone = false, fourthdone = false, fifthdone = false,
	sixthdone = false, ballsdone = false, seventhdone = false, eighthdone = false, ninthdone = false, bgmplayed = false, levelstarted = false;
	public bool firsttime = true, blockcounter = false, blockdone = false, parrycounter = false, parrydone = false, tenthdone = false, 
	elventhdone = false, twelfthdone = false, thirteendone = false, hurrcounter = false, hurrdone = false;
	private bool pressenter = false, flashing = false;
	
	//tutorial events
	private int tutorialnumber = 0;
	
	//counters
	public int blockcount = 0, parrycount = 0, abilityuse = 0; 
	private float flashtimer = 0;
	
	//media
	public AudioClip whee, bgm, tutorialmusic;
	public Texture2D enterimage, enterimg2;
	public GameObject chiball;
	public GameObject disabler;

	// Use this for initialization
	void Start () 
	{
		spawnerscript = (Level2Spawn)GameObject.FindGameObjectWithTag("Spawner").GetComponent("Level2Spawn");
		disabler = GameObject.FindGameObjectWithTag("Tutorial");
		if (PlayerPrefs.GetInt("Level3Unlock") == 1)
			firsttime = false;
		
		if (!firsttime)
			Destroy(disabler);
		
		//welcome message
		if (firsttime)
		{
			StartCoroutine ( Wait3seconds () );
			playerscript.balls = 0;
			audio.clip = tutorialmusic;
			audio.Play();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		timekeeper += Time.deltaTime;
		
		
		#region Tutorial messages timing & conditions------------------------------------------------
		if (firsttime)
		{
			
			//Here they come again
			if (tutorialnumber == 2 && timekeeper > 3)
				thirdmessageplusspawn();
			//OMG DID U SEEN THEM NIG...
			if (spawnerscript.wrestlersdone && tutorialnumber == 3)
				StartCoroutine ( ToggleFifth () );
			
			//Have some balls...
			if (fifthdone && !sixthdone)
				sixthmsg();
			if (seventhdone && !eighthdone)
			{
				pressenter = true;
				eighthmessage = true;
			}
			if (eighthdone && !ninthdone)
			{
				pressenter = true;
				ninthmessage = true;
			}
			if (tenthdone && !elventhdone && timekeeper > 2)
			{
				pressenter = true;
				elventhmessage = true;
			}
			if (elventhdone && !twelfthdone)
			{
				pressenter = true;
				twmessage = true;
			}
//			//balls are spawned
//			if (sixthdone && timekeeper < 0.2f && !ballsdone)
//				StartCoroutine ( SpawnBalls () );
//			//I will teach you there!....
//			if (ballsdone && timekeeper >= 6f && !seventhdone)
//				StartCoroutine ( ToggleSeventh (5) );
//			//mothaflukkaz...
//			if (seventhdone && !eighthdone)
//				StartCoroutine ( ToggleEighth (4) );
//			if (eighthdone && timekeeper >= 15f && !levelstarted)
//				startlevel();
		}
			
		#endregion
		
		if (!firsttime && !levelstarted)
			startlevel();
		
//		if (!audio.isPlaying && bgmplayed)
//		{
//			audio.PlayOneShot(bgm);	
//			
//		}
		
		if (Input.GetKeyDown(KeyCode.P))
		{
			if (blockcounter)
				blockcount++;
			
			if (parrycounter)
				parrycount++;
			
			if (hurrcounter)
				abilityuse++;
			
		}
		
		//flashing image
		if (pressenter)
		{
			flashtimer += Time.deltaTime;	
			if (flashtimer > 0.5f)
			{
				flashing = !flashing;	
				flashtimer = 0;
			}
		}
		
		
		//General pressing enter stuff
		if (Input.GetKeyDown(KeyCode.Return))
		{
			if (pressenter)
			{
				flashing = false;
				tutorialnumber ++;
				timekeeper = 0;
				pressenter = false;
			}
		}
		
	}
	
	IEnumerator Wait3seconds ()
	{
		yield return new WaitForSeconds(4);
		
		welcomeon = true;
		pressenter = true;
		
	}
	
//	IEnumerator ToggleWelcome (float time)
//	{
//		welcomeon = true;
//		
//		
//		yield return new WaitForSeconds(time);
//		
//		welcomeon = false;
//		welcomedone = true;
//		
//	}
	
	void secondmsg()
	{
		secondmessage = true;	
		pressenter = true;
		welcomeon = false;
	}
	
//	IEnumerator ToggleSecond (float time)
//	{
//		secondmessage = true;
//		welcomeon = false;
//		
//		
//		yield return new WaitForSeconds(time);
//		
//		secondmessage = false;
//		seconddone = true;
//		
//	}
	
	void thirdmessageplusspawn()
	{
		tutorialnumber = 3;
		StartSpawner = true;
		
	}
	
//		IEnumerator ToggleFourth (float time)
//	{
//		fourthmessage = true;
//		thirdmessage = false;
//		
//		yield return new WaitForSeconds(time);
//		
//		fourthmessage = false;
//		fourthdone = true;
//		
//	}
	
	IEnumerator ToggleFifth()
	{
		yield return new WaitForSeconds(1.5f);
		
//		audio.Stop();
		tutorialnumber = 4;
		
//		yield return new WaitForSeconds(3);
//		
//		tutorialnumber = 5;
		
	}
	
	void sixthmsg()
	{
		sixthmessage = true;
		pressenter = true;
		killall();
		StartSpawner = false;
	}
	
//		IEnumerator ToggleSixth (float time)
//	{
//		sixthmessage = true;
//		fifthmessage = false;
//		timekeeper = 0f;
//		
//		yield return new WaitForSeconds(time);
//		
//		sixthmessage = false;
//		sixthdone = true;
//		
//		
//	}
	
		IEnumerator ToggleSeventh (float time)
	{
		seventhmessage = true;
		sixthmessage = false;
		
		yield return new WaitForSeconds(time);
		
		seventhmessage = false;
		seventhdone = true;
		
	}
	
	void learnability()
	{
		PlayerPrefs.SetInt("Hurricaneabilitystate", 1);
		PlayerPrefs.SetString("Slot2", "Hurricane");
		guiscript.Slot2content = "Hurricane";
		audio.PlayOneShot(whee);
		guiscript.Zoomevent();

	}
	
		IEnumerator ToggleEighth (float time)
	{
		eighthmessage = true;
		audio.Play();
		bgmplayed = true;
		seventhmessage = false;
		Destroy(disabler);
		obox.tutorial = false;
		
		yield return new WaitForSeconds(time);
		
		eighthmessage = false;
		eighthdone = true;
		
	}
	
//	IEnumerator ToggleTheycome (float time)
//	{
//		Theycome = true;
//		movement2 = false;
//		ms2msgdone = true;
//		movement = false;
//		ms1msgdone = true;
//		timekeeper = 0;
//		enemyhere = true;
//		
//		yield return new WaitForSeconds(time);
//		
//		Theycome = false;
//		
//	}
//	
//		IEnumerator ToggleGJ (float time)
//	{
//		GJ = true;
//		attack1info = false;
//		
//		yield return new WaitForSeconds(time);
//		
//		GJ = false;
//		GJdone = true;
//		timekeeper = 0;
//		firstdefeatone = true;
//		
//	}
	
	IEnumerator SpawnBalls()
	{
		ballsdone = true;
		
		Instantiate(chiball, new Vector3(-200, 350, -110), Quaternion.identity);
		
		yield return new WaitForSeconds(0.3f);
		
		Instantiate(chiball, new Vector3(-100, 350, -110), Quaternion.identity);
		
		yield return new WaitForSeconds(0.3f);
		
		Instantiate(chiball, new Vector3(0, 350, -110), Quaternion.identity);
		
		yield return new WaitForSeconds(0.3f);
		
		Instantiate(chiball, new Vector3(100, 350, -110), Quaternion.identity);
		
		yield return new WaitForSeconds(0.3f);
		
		Instantiate(chiball, new Vector3(200, 350, -110), Quaternion.identity);
		
		timekeeper = 0;
	}
	
	public void spawn1ball()
	{
		Instantiate(chiball, new Vector3(-200, 350, -110), Quaternion.identity);
	}
	
	void startlevel()
	{
		levelstarted = true;
		spawnerscript.wave = 1;
		spawnerscript.enemyCount = 0;
		spawnerscript.firstdefeated = true;
		if (!bgmplayed)
		{
			audio.clip = bgm;
			audio.Play();
			bgmplayed = true;
		}
		StartSpawner = true;
	}
	
	void killall()
	{
		GameObject[] enemiesonscreen = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject[] throwers = GameObject.FindGameObjectsWithTag("Enemythrower");
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
		
		foreach (GameObject t in throwers)
		{
			Punkthrower enemyscript = (Punkthrower)t.GetComponent("Punkthrower");
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
//		GUI.color = Color.red;
//		GUI.Label(new Rect(0, 220, 200, 100), "tutnum: " + tutorialnumber.ToString());
		
		myStyle.font = myFont;
		GUI.color = Color.white;
		GUI.skin  = tutorial;
		
		if (welcomeon)
		{
			myStyle.fontSize = 18;
			GUI.Label(textbox, "Listen whelp, put away your unwarrented self-importance and heed my words.");
			if (Input.GetKeyDown(KeyCode.Return))
			{
				welcomeon = false;
				welcomedone = true;
			}
		}
		
		if (tutorialnumber == 1)
		{
			pressenter = true;
			GUI.Label(textbox, "Some enemies will be tough to defeat with your regular attacks. The key to a good offense is sometimes a good defense.");
		}	
		
		if (tutorialnumber == 2) 
		{
			GUI.Label(textbox, "Its time to prove your worth. \nHere they come!!");
		}	
		
		if (tutorialnumber == 4)
		{
			pressenter = true;
			GUI.Label(textbox, "Rick: OH GOD! \nDid you see those guys? How am I supposed to even fight them?");
			if (Input.GetKeyDown(KeyCode.Return))
			killall();
		}
		
		if (tutorialnumber == 5)
		{
			pressenter = true;
			GUI.Label(textbox, "Hmph. I suppose you are in need of another lesson?\nListen well, mongrel.");
		}
		
		if (tutorialnumber == 6)
		{
			pressenter = true;
			GUI.Label(textbox, "Hold the left SHIFT button to block incoming attacks or projectiles.\nBe careful, as taking too many hits will get you staggered.");
			if (Input.GetKeyDown(KeyCode.Return))
			{
				spawnerscript.wave = -2;
				StartSpawner = true;
				blockcounter = true;
				blockcount = 0;
			}
		}
		
		if (blockcounter)
		{
			GUI.Label(objective1, "Projectiles blocked: " + blockcount + " / 5");
			if (blockcount > 5)
				blockcount = 5;
			if (blockcount == 5)
			{
				blockcounter = false;	
				killall();
				StartSpawner = false;
				tutorialnumber = 8;
				blockcount = 0;
			}
		}
		
		
		if (tutorialnumber == 8)
		{
			pressenter = true;
			GUI.Label(textbox, "Rick: Aw yea! I think I'm getting the hang of this.");
		}
		
		if (tutorialnumber == 9)
		{
			pressenter = true;
			GUI.Label(textbox, "Rick: But sensei, if those hardcore Cyrus look-alikes come back, I can't just defend." +
			 	" I'm not the weak Rick I used to be and I'm gonna show them by delivering some pain!");
		}

		if (tutorialnumber == 10)
		{
			pressenter = true;
			GUI.Label(textbox, "Have patience, brute.\nIn fact, the next exercise will be all about patience.");
		}
		
		if (tutorialnumber == 11)
		{
			pressenter = true;
			GUI.Label(textbox, "The parry and counter maneuver requires discipline but it rewards you greatly once you succeed. Now, try it!");	
			if (Input.GetKeyDown(KeyCode.Return))
			{
				parrycount = 0;
				parrycounter = true;
				spawnerscript.wave = -1;
				StartSpawner = true;
			}
		}
		
		if (tutorialnumber == 12)
		{
			GUI.Label(textbox, "Pressing the block key the moment before an attack hits will cause you to parry the attack, rendering the enemy vulnerable.");	
		}
		
		if (parrycounter)
		{
			GUI.Label(new Rect(0, 170, 280, 40), "Attacks parried: " + parrycount + " / 5");
			
			if (parrycount > 5)
				parrycount = 5;
			if (parrycount == 5)
			{
				parrycounter = false;	
				parrydone = true;
				killall();
				StartSpawner = false;
				tutorialnumber = 13;
			}
		}
		
		if (tutorialnumber == 13)
		{
			pressenter = true;
			GUI.Label(textbox, "Stop! I see that you are ready for the secret technique of Majin-ryuu Karate.");	
		}
		
		if (tutorialnumber == 14)
		{
			pressenter = true;
			GUI.Label(textbox, "Now, concentrate your mind...\nOpen the inner third eye to see beyond reality.");	
		}
		
		if (tutorialnumber == 15)
		{
			pressenter = true;
			GUI.Label(textbox, "Rick: ...! Sensei! I see a sphere shaped object!");	
			if (Input.GetKeyDown(KeyCode.Return))
			{
				StartCoroutine ( SpawnBalls() );
			}
		}
		
		if (tutorialnumber == 16)
		{
			pressenter = true;
			GUI.Label(textboxtop, "Those are the spheres of life and soul. Consuming them will grant you with power beyond imagination.");	
		}
		
		if (tutorialnumber == 17)
		{
			pressenter = true;
			GUI.Label(textboxtop, "You will see the gauge under your health bar being filled up as you collect these spheres. That is called Soul meter.");	
		}
		
		if (tutorialnumber == 18)
		{
			pressenter = true;
			GUI.Label(textboxtop, "By sacrificing your soul power, you can perform techniques that are beyond human capability.");	
		}
		
		if (tutorialnumber == 19)
		{
			pressenter = true;
			GUI.Label(textboxtop, "Naturally, you are ignorant of such sophisticated techniques. Let me teach you one.");	
		}
		
		if (tutorialnumber == 20)
		{
			GUI.Label(textbox, "Now, craft an image, in this case, a gale of wind, in your mind. Concentrate on that image and let it flow through you.");	
			if (timekeeper > 4)
			{
				learnability();
				tutorialnumber = 21;
				timekeeper = 0;
			} 
		}
		
		
		if (tutorialnumber == 21)
		{
			pressenter = true;
			GUI.Label(textbox, "Now, let it spill over! Project your imagination to reality! Turn yourself into a devastating hurricane that destroys all!");
			if (Input.GetKeyDown(KeyCode.Return))
			{
				Player playerscript = (Player)GameObject.FindGameObjectWithTag("Player").GetComponent("Player");
				playerscript.Tutorialhurricane();
			} 
		}
		
		if (tutorialnumber == 22)
		{
			pressenter = true;
			GUI.Label(textbox, "Rick: UWOHAHAHAHHA!");
		}
		
		if (tutorialnumber == 23)
		{
			pressenter = true;
			GUI.Label(textbox, "You have learned Hurricane! Hurricane damages all enemies in a radius around you.\nPress 2 or R to use your new ability.");
			if (Input.GetKeyDown(KeyCode.Return))
			{
				hurrcounter = true;
				spawnerscript.wave = 0;
				StartSpawner = true;
			} 
		}
		
		if (hurrcounter)
		{
			GUI.Label(new Rect(0, 170, 310, 40), "Hit enemies with Hurricane: " + abilityuse + " / 10");
			GUI.Label(textbox, "If you run out of soul, remember that parrying enemies will restore one bar.");
			
			if (abilityuse > 10)
				abilityuse = 10;
			if (abilityuse == 10)
			{
				hurrcounter = false;	
				hurrdone = true;
				killall();
				StartSpawner = false;
				tutorialnumber = 25;
			}
		}
		
		if (tutorialnumber == 25)
		{
			pressenter  = true;
			GUI.Label(textbox, "Rick: All these years of harassment! It's time for some payback!");	
			if (Input.GetKeyDown(KeyCode.Return))
			{
				tutorialnumber = 99;
				startlevel();
			}
		}

		if (pressenter)
		{
			if (tutorialnumber >= 16 && tutorialnumber < 20)
			{
				if (flashing)
				GUI.DrawTexture(new Rect(textboxtop.x + 590, textboxtop.y + 65, 47, 18), enterimage);
			
				if (!flashing)
				GUI.DrawTexture(new Rect(textboxtop.x + 590, textboxtop.y + 65, 47, 18), enterimg2);
				
			} else {
			
			if (flashing)
			GUI.DrawTexture(new Rect(textbox.x + 590, textbox.y + 65, 47, 18), enterimage);
			
			if (!flashing)
			GUI.DrawTexture(new Rect(textbox.x + 590, textbox.y + 65, 47, 18), enterimg2);
				
			}
		
		} 
		
	}
}
