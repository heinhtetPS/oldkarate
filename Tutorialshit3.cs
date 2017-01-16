using UnityEngine;
using System.Collections;

public class Tutorialshit3 : MonoBehaviour {
	
	//UI Stuff
	public Font myFont;
	public GUIStyle myStyle;
	public GUISkin messages;
	
	//grabbing scripts
	public Level3Spawn spawnerscript;
	public Karateoboxnew obox;
	public PlayerInfo infoscript;
	public Player playerscript;
	public GUIicons guiscript;
	
	public bool firsttime = true;
	
	private int tutorialnumber = 0;
	
	public float timekeeper = 0f;
	
	//parameter bools
	private bool welcomeon = false, secondmessage = false, thirdmessage = false, fourthmessage = false, fifthmessage = false,
	sixthmessage = false, seventhmessage = false, eighthmessage = false;
	
	//event bools
	public bool StartSpawner = false, welcomedone = false, seconddone = false, thirddone = false, fourthdone = false, fifthdone = false,
	sixthdone = false, seventhdone = false, eighthdone = false, bgmplayed = false;
	
	//rectangle locs
	private Rect textbox = new Rect(Screen.width / 2 - 325, Screen.height - 100, 650, 90);
	private Rect textboxtop = new Rect(Screen.width / 2 - 325, 60, 650, 90);
	private Rect objective1 = new Rect(0, 160, 280, 40);
	
	public bool levelstarted = false;
	
	private bool pressenter =  false, flashing = false;
	private float flashtimer = 0;
	
	//media
	public Texture2D enterimage, enterimg2;
	public AudioClip whee, bgm, tutorialmusic;
	public GameObject chiball;
	public GameObject disabler;

	// Use this for initialization
	void Start () 
	{
		spawnerscript = (Level3Spawn)GameObject.FindGameObjectWithTag("Spawner").GetComponent("Level3Spawn");
		disabler = GameObject.FindGameObjectWithTag("Tutorial");
		playerscript.balls = 0;
		if (PlayerPrefs.GetInt("Level4Unlock") == 1)
			firsttime = false;
		
		if (!firsttime)
		{
			Destroy(disabler);
			levelstarted = true;
		}
		
		if (firsttime)
		{
			StartCoroutine ( Wait3seconds () );
			playerscript.balls = 0;
			audio.PlayOneShot(tutorialmusic);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (firsttime)
		{
		
			timekeeper += Time.deltaTime;
			
			#region Tutorial messages timing & conditions------------------------------------------------
			
			//oh no the sis...
//			if (welcomedone && !seconddone)
//			{
//				secondmessage = true;	
//				pressenter = true;
//			}
//			//sis is gone...
//			if (seconddone && !thirddone)
//				StartCoroutine ( ToggleThird (4) );
//			//What are these shits? Got no balls...
//			if (thirddone && !fourthdone && spawnerscript.tutorialdefeated)
//				Fourth();
//			//Hot dog carl
//			if (fourthdone && !fifthdone && timekeeper >= 8)
//				Fif();
//			//You learned gmash...
//			if (fifthdone && !sixthdone)
//				Sex();
//			//hellzyeah sons...
//			if (sixthdone && !seventhdone && spawnerscript.tutorial2defeated && !bgmplayed)
//				Sven();
				
			#endregion
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
		
		
		//pressing enter stuff
		if (Input.GetKeyDown(KeyCode.Return))
		{
			if (pressenter)
			{
				tutorialnumber ++;
				timekeeper = 0;
				flashing = false;
				pressenter = false;
			}
		}
		
		
		
		if (!firsttime && !bgmplayed)
		{
			StartSpawner = true;
			spawnerscript.wave = 1;
			audio.Play();
			bgmplayed = true;
		}
		
	}
	
	IEnumerator Wait3seconds ()
	{
		yield return new WaitForSeconds(3);
		
		welcomeon = true;
		pressenter = true;
		
	}
	
	IEnumerator ToggleWelcome (float time)
	{
		welcomeon = true;
		
		yield return new WaitForSeconds(time);
		
		welcomeon = false;
		welcomedone = true;
		
	}
	
	IEnumerator ToggleSecond (float time)
	{
		secondmessage = true;
		welcomeon = false;
		
		
		yield return new WaitForSeconds(time);
		
		secondmessage = false;
		seconddone = true;
		
	}
	
	IEnumerator ToggleThird (float time)
	{
		thirdmessage = true;
		secondmessage = false;
		StartSpawner = true;
		
		yield return new WaitForSeconds(time);
		
		thirdmessage = false;
		thirddone = true;
		
	}
	
		IEnumerator ToggleFourth (float time)
	{
		fourthmessage = true;
		thirdmessage = false;
		
		yield return new WaitForSeconds(time);
		
		fourthmessage = false;
		fourthdone = true;
		timekeeper = 0;
		spawnerscript.enemyCount = 0;
		spawnerscript.wave = 0;
		
		StartSpawner = true;
		
	}
	
	void Fourth()
	{
		fourthmessage = true;
		thirdmessage = false;
		pressenter = true;
		
	}
	
		IEnumerator ToggleFifth (float time)
	{
		fifthmessage = true;
		fourthmessage = false;
		audio.Stop();
		
		
		yield return new WaitForSeconds(time);
		
		fifthmessage = false;
		fifthdone = true;
		
		
	}
	
	void Fif()
	{
		fifthmessage = true;
		fourthmessage = false;
		pressenter = true;
		
	}
	
		IEnumerator ToggleSixth (float time)
	{
		

		
		yield return new WaitForSeconds(time);
		
		
		
		
	}
	
	void Sex()
	{
		sixthmessage = true;
		fifthmessage = false;
		pressenter = true;
		PlayerPrefs.SetInt("Groundsmashabilitystate", 1);
		PlayerPrefs.SetString("Slot1", "Groundsmash");
		guiscript.Slot1content = "Groundsmash";
		audio.clip = whee;
		if (!audio.isPlaying)
		audio.Play();
		guiscript.Zoomevent2();
		
	}
	
		IEnumerator ToggleSeventh (float time)
	{
		seventhmessage = true;
		sixthmessage = false;
		
		
		
		yield return new WaitForSeconds(time);
		
		
		
	}
	
	void Sven()
	{
		seventhmessage = true;
		sixthmessage = false;
		pressenter = true;
		audio.clip = bgm;
		if (!audio.isPlaying)
		audio.Play();
		bgmplayed = true;
	}

	
	void startlevel()
	{
		StartSpawner = true;
		
	}
	
	void killall()
	{
		GameObject[] enemiesonscreen = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject[] g2onscreen = GameObject.FindGameObjectsWithTag("Enemy2");
		GameObject[] g3onscreen = GameObject.FindGameObjectsWithTag("Enemy3");
		GameObject[] throweronsreen = GameObject.FindGameObjectsWithTag("Enemythrower");
		GameObject[] shit1s = GameObject.FindGameObjectsWithTag("Ground");
		
		
		foreach (GameObject g in enemiesonscreen)
		{
			Punk1 enemyscript = (Punk1)g.GetComponent("Punk1");
			enemyscript.Bleedcash();
			enemyscript.comboedcount = 999;
			enemyscript.Takedamage(9999);
		}
		
		foreach (GameObject t in throweronsreen)
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
//		GUI.Label(new Rect(0, 240, 200, 100), "time: " + timekeeper.ToString());
		
		myStyle.font = myFont;
		GUI.color = Color.white;
		GUI.skin = messages;
		
		if (welcomeon)
		{
			pressenter = true;
			GUI.Label(textboxtop, "Rick: Oh no! Hold it right there, you crazy punks!");
			if (Input.GetKeyDown(KeyCode.Return))
			{
				welcomeon = false;
			}
		}
		
		if (tutorialnumber == 1)
		{
			pressenter = true;
			GUI.Label(textboxtop, "Rick: She's gone! Goddamn punks!\nI can already imagine the horror when sensei finds out about this! I have to get her back!");
		}	
		
		if (tutorialnumber == 2)
		{
			GUI.Label(textboxtop, "Rick: Dammit! More enemies!\nAnd what are these things on the ground??");
			if (timekeeper >= 4)
			{
				spawnerscript.enemyCount = 0;
				spawnerscript.wave = 0;
				StartSpawner = true;
				tutorialnumber = 3;
				timekeeper = 0;
			}
		}	
		
		if (tutorialnumber == 3)
		{
			GUI.Label(textboxtop, "Rick: Oh god, it STINGS when you step on them!!\nMy attacks aren't so effective and I don't have enough soul for Hurricane! What should I do!?");
			if (timekeeper >= 8)
			{
				tutorialnumber = 4;
				timekeeper = 0;
			}
			
		}
		
		if (tutorialnumber == 4)
		{
			pressenter = true;
			GUI.Label(textboxtop, "Hotdog man: Hey there, ye rambunctious youngster.\nIf yer lookin' to take out them stinky blobbers. I'd just squash em wit all ma strength.");
			
		}
		
		if (tutorialnumber == 5)
		{
			pressenter = true;
			GUI.Label(textboxtop, "Rick: Squash them, huh? Sometimes the simplest method is the best! How about something like this?");
			if (Input.GetKeyDown(KeyCode.Return))
			{
				Player playerscript = (Player)GameObject.FindGameObjectWithTag("Player").GetComponent("Player");
				playerscript.TutorialGsmash();
				PlayerPrefs.SetInt("Groundsmashabilitystate", 1);
				PlayerPrefs.SetString("Slot1", "Groundsmash");
				guiscript.Slot1content = "Groundsmash";
				guiscript.Zoomevent2();
			}
		}
		
		if (tutorialnumber == 6)
		{
			pressenter = true;
			GUI.Label(textboxtop, "Rick: Thats it!\nYou learned Ground Smash! Ground Smash damages foes on or near the ground and costs no soul.");
		}
		
		if (tutorialnumber == 7)
		{
			pressenter = true;
			GUI.Label(textboxtop, "Press 1 or E to use your new ability!");
		}
		
		if (tutorialnumber == 8)
		{
			pressenter = true;
			GUI.Label(textboxtop, "Rick: Alright! Thanks, random hotdog man!\nHopefully, this will put me back on track. Here I come, Sensei's little Chinese sister!");
			if (Input.GetKeyDown(KeyCode.Return))
			{
				Destroy(disabler);
				firsttime = false;
				obox.tutorial = false;
				killall();
				spawnerscript.wave = 1;
				spawnerscript.enemyCount = 0;
				spawnerscript.duringtrial = false;
				spawnerscript.firsttime = false;
				spawnerscript.tutorialdefeated = true;
				spawnerscript.tutorial2defeated = true;
				StartSpawner = true;
				levelstarted = true;
				if (!bgmplayed)
				{
					audio.Stop();
					audio.clip = bgm;
					audio.loop = true;
					audio.Play();
					bgmplayed = true;
				}
				tutorialnumber = 99;
				pressenter = false;
			}
		}
		
		if (pressenter)
		{
				
			if (flashing)
				GUI.DrawTexture(new Rect(textboxtop.x + 590, textboxtop.y + 65, 47, 18), enterimage);
			
				if (!flashing)
				GUI.DrawTexture(new Rect(textboxtop.x + 590, textboxtop.y + 65, 47, 18), enterimg2);	
		
		} 
		
		
	}
}
