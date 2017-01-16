using UnityEngine;
using System.Collections;

public class Karateoboxnew : MonoBehaviour {
	
	//initialization
	private GameObject Karateman;
	private Player playerscript;
	private Tutorialshit tutscript;
	private pausemenu pausescript;
	
	public bool tutorial = false, checkedagain = false;
	
	//content
	public bool heavyon = false, staticheavystartup = false, directionreceived = false;
	public bool heavyisReady = true, junklinready = true, Dkickready = true, axekickready = true;
	public bool doingaxe = false, doingjunkle = false;
	private float axeandjunklereset;
	private float heavyCD, junklinCD, DkickCD, axekickCD;
	public float staticheavydelay;
	public int direction;
	private float hurricaneEffectdelay, DkickEffectdelay;
	public int junkcombo, axecombo, DKcombo;
	
	//FOR GUI
	public Font myFont;
	public GUIStyle myStyle;
	
	public int combocounter;
	public bool comboing = false, wordisup = false;
	public float combotimer;
	private float worddisplaytimer;
	private float Xoffset;
	
	private Rect numberpos = new Rect(Screen.width - 240, 120, 49, 61);
	private Rect hitspos = new Rect(Screen.width - 188, 140, 137, 43);
	private Rect wordbasepos = new Rect();
	
	public Texture2D cool, fresh, cowa, stylin, swag, trippin, fever, ridiculous, impossible, legend, comboword;
	public Texture2D one, two, three, four, five, six, seven, eight, nine, zero, hits;
	
	//other media
	public AudioClip smacksound1, smacksound2, finalsmack, pickup, coin, heal, smash;
	public ParticleSystem smack; 
	public GameObject lines;
	
	void Start () {
		
		
		
		Karateman = GameObject.FindGameObjectWithTag("Player");
		playerscript = (Player)Karateman.GetComponent("Player");
		tutscript = (Tutorialshit)GameObject.FindGameObjectWithTag("MainCamera").GetComponent("Tutorialshit");
		pausescript = (pausemenu)GameObject.FindGameObjectWithTag("MainCamera").GetComponent("pausemenu");
		
		(gameObject.collider as SphereCollider).radius = 55f;
		
		if (GameObject.FindGameObjectWithTag("Tutorial") != null)
			tutorial = true;
		
		Makelines();
	
	}
	
	// Update is called once per frame
	void Update () {
		
		//double check if tutorial
		if (!checkedagain)
		{
			if (GameObject.FindGameObjectWithTag("Tutorial") == null)
			tutorial = false;
			checkedagain = true;
		}
		
		//combofreeze
		if (GameObject.FindGameObjectWithTag("Combofreeze") != null && comboing)
		combotimer = 0;
		
		//moving hitbox a bit down for shoryu otherwise normal follow
		if (playerscript.junklin)
			transform.position =  new Vector3
			(Karateman.transform.position.x, Karateman.transform.position.y - 180, -100);
		else
		transform.position = new Vector3
			(Karateman.transform.position.x, Karateman.transform.position.y, -100);
		
		//heavychargin
		if (Input.GetButton("fight2") && playerscript.state != Player.State.Explosion && !playerscript.stunned && !playerscript.blocking ||
			Input.GetKeyDown(KeyCode.LeftArrow) && playerscript.state != Player.State.Explosion && !playerscript.stunned && !playerscript.blocking)
		{
			playerscript.heavycounter++;
			if (playerscript.heavycounter <= 50)
			heavyon = true;
		} 
		
		//canceling heavy related anims when button stops beign pressed
		if (Input.GetButtonUp("fight2") || Input.GetKeyUp(KeyCode.LeftArrow))
		{
			playerscript.heavycounter = 0;
			if (playerscript.karateanim.IsPlaying("constipate"))
			{
				playerscript.animating = false;
				playerscript.animationtimer = 0;
				playerscript.karateanim.Stop();
			}
		}
		
		//INPUT SPECIFICATION (IMPORTANT)
		if (heavyon)
		{
			if (!playerscript.linkwindow)
			staticheavystartup = true;
			Karateman.rigidbody.velocity = Vector3.zero;
			
			if (playerscript.dynastystyle)
			{
				if (playerscript.attackcounter == 1)	
				doingaxe = true;
				
				if (playerscript.attackcounter == 2)	
				doingjunkle = true;
				
			}
			
			//Dhoryu KEN
			if (Input.GetAxis("Vertical") > 0 && junklinready && !directionreceived && !playerscript.blocking ||
				Input.GetButton("Ascent") && junklinready && !directionreceived && !playerscript.blocking)
			{
				StartCoroutine ( DirectionWindow (1, 1f) );
				if (playerscript.linkwindow)
					StartCoroutine ( Resetplayerbools (0.3f) );
				if (!playerscript.linkwindow)
					StartCoroutine ( Resetplayerbools (1) );
				StartCoroutine (Delayedheavyonreset (0.5f));
				doingjunkle = true;
				StartCoroutine ( Delayedreadyoff (1) );
				return;
			}
			
			//AXE KICK
			if (Input.GetAxis("Vertical") < 0 && axekickready && !directionreceived && !playerscript.blocking ||
				Input.GetButton("Descent") && axekickready && !directionreceived && !playerscript.blocking)
			{
				
				StartCoroutine ( DirectionWindow (2, 1.2f) );
				if (playerscript.linkwindow)
					StartCoroutine ( Resetplayerbools (0.3f) );	
				if (!playerscript.linkwindow)
					StartCoroutine ( Resetplayerbools (0.9f) );
				StartCoroutine (Delayedheavyonreset (0.2f));
				doingaxe = true;
				StartCoroutine ( Delayedreadyoff (2) );
				return;
			}
			
			//DKICK left
			if (Input.GetAxis("Horizontal") < 0 && Dkickready && !directionreceived && !playerscript.blocking ||
				Input.GetAxis("Xbox_horizontal") < 0 && Dkickready && !directionreceived && !playerscript.blocking)
			{
				
				StartCoroutine ( DirectionWindow (3, 1.2f) );
				if (playerscript.linkwindow)
					StartCoroutine ( Resetplayerbools (1f) );	
				if (!playerscript.linkwindow)
					StartCoroutine ( Resetplayerbools (1.3f) );
				StartCoroutine (Delayedheavyonreset (0.5f));
				StartCoroutine ( Delayedreadyoff (3) );
				return;
			}
			
			//DKICK right
			if (Input.GetAxis("Horizontal") > 0 && Dkickready && !directionreceived && !playerscript.blocking ||
				Input.GetAxis("Xbox_horizontal") > 0 && Dkickready && !directionreceived && !playerscript.blocking)
			{
				
				StartCoroutine ( DirectionWindow (3, 1.2f) );
				if (playerscript.linkwindow)
					StartCoroutine ( Resetplayerbools (1f) );	
				if (!playerscript.linkwindow)
					StartCoroutine ( Resetplayerbools (1.3f) );
				StartCoroutine (Delayedheavyonreset (0.5f));
				StartCoroutine ( Delayedreadyoff (4) );
				return;
			}
			
			
			StartCoroutine (ResetHeavy (0.2f));
			
		}//END  | HEAFY ON
		
		
		
		
		//STATES & CALCULATINS && COOLDOWNS & RESETS
		#region CDs and shit
		
		//for HURRICANE STAY
		if (playerscript.hurricane)
		hurricaneEffectdelay += Time.deltaTime;
		
		//increase hitbox upon special move
		if (playerscript.groundsmash || playerscript.hurricane || playerscript.forceon)
		{	
			if (PlayerPrefs.GetString("GroundsmashT1") == "black")
				StartCoroutine ( IncreaseCollider (120) );
			if (playerscript.forceon && PlayerPrefs.GetString("ForceT1") == "white")
				StartCoroutine ( IncreaseCollider (150) );
			else if (playerscript.forceon)
				StartCoroutine ( IncreaseCollider (100) );
			else
			StartCoroutine ( IncreaseCollider (90) );
		}

		//highest combo calculation
		if (combocounter > playerscript.highestcombo)
		{
			playerscript.highestcombo = combocounter;	
		}

		//RESETS and COOLDOWNS
		if (comboing)
			combotimer += Time.deltaTime;
		
		//combo timer 3 seconds
		if (combotimer - Time.deltaTime > 3f)
		{
			comboing = false;	
			combocounter = 0;
			combotimer = 0;
		}
		
		//dmg delay for static heavies
		if (staticheavystartup)
			staticheavydelay+= Time.deltaTime;	
		if (staticheavydelay >= 0.38f)
		{
			staticheavystartup = false;
			staticheavydelay = 0;	
		}
		
		//general heavy cd i dunno if its useful or not
		if (!heavyisReady)
			heavyCD += Time.deltaTime;
		if (heavyCD >= 1f)
		{
			heavyisReady = true;
			heavyCD = 0;
		}

		//3 individual heavy move CD resets from attacking
		if (!junklinready && playerscript.spacebar - junkcombo >= 3)
		{
			junklinready = true;
			junkcombo = 0;
		}
		
		if (!axekickready && playerscript.spacebar - axecombo >= 3)
		{
			axekickready = true;
			axecombo = 0;
		}
		
		if (!Dkickready && playerscript.spacebar - DKcombo >= 3)
		{
			Dkickready = true;
			DKcombo = 0;
		}
		
		//bools for doing shorter moves
		if (doingaxe || doingjunkle)
		{
			axeandjunklereset += Time.deltaTime;
			
			if (axeandjunklereset > 0.4f)
			{	
				doingaxe = false;
				doingjunkle = false;
				axeandjunklereset = 0;
			}
			
		}
		
		#endregion
		
	}//END | UPDATE
	
	void OnTriggerEnter(Collider otherObject)
	{	
		#region ground smash and piledriver collisions-------------------------------------------
			//ground 1
			if (otherObject.tag == "Ground" && playerscript.groundsmash)
			{
				EnemyGround gscript = (EnemyGround)otherObject.gameObject.GetComponent("EnemyGround");
				gscript.Die();
				if (comboing)
				combocount();
			}
			
			//g2 and g3
			if (otherObject.tag == "Ground3" && playerscript.groundsmash ||
				otherObject.tag == "Ground2" && playerscript.groundsmash)
			{
				EnemyGround3 gscript = (EnemyGround3)otherObject.gameObject.GetComponent("EnemyGround3");
				gscript.Die();
				if (comboing)
				combocount();
			}
			
			//punk1
			if (otherObject.tag == "Enemy" && playerscript.groundsmash 
				&& otherObject.gameObject.transform.position.y < -200)
			{
				playerscript.hits++;
				Punk1 enemyscript = (Punk1)otherObject.gameObject.GetComponent("Punk1");
				Instantiate(smack, 
				new Vector3(otherObject.transform.position.x, otherObject.transform.position.y, -120), otherObject.transform.rotation);
				enemyscript.dbzmode = true;
				enemyscript.comboedcount++;
				enemyscript.comboedreset = 0;
				enemyscript.Bleedcash();
				if (enemyscript.health <= 4)
				Makelines();
				enemyscript.Takedamage(4);
				if (comboing)
				combocount();
			}
			
			//punkthrower
			if (otherObject.tag == "Enemythrower" && playerscript.groundsmash 
				&& otherObject.gameObject.transform.position.y < -200)
			{
				playerscript.hits++;
				Punkthrower enemyscript = (Punkthrower)otherObject.gameObject.GetComponent("Punkthrower");
				Instantiate(smack, 
				new Vector3(otherObject.transform.position.x, otherObject.transform.position.y, -120), otherObject.transform.rotation);
				enemyscript.dbzmode = true;
				enemyscript.comboedcount++;
				enemyscript.comboedreset = 0;
				enemyscript.Bleedcash();
				if (enemyscript.health <= 4)
				Makelines();
				enemyscript.Takedamage(4);
				if (comboing)
				combocount();
			}
			
			//punk2
			if (otherObject.tag == "Enemy2" && playerscript.groundsmash 
				&& otherObject.gameObject.transform.position.y < -200)
			{
				Punk2 punk2script = (Punk2)otherObject.gameObject.GetComponent("Punk2");
				punk2script.dbzmode = true;
				Instantiate(smack, 
				new Vector3(otherObject.transform.position.x, otherObject.transform.position.y, -120), otherObject.transform.rotation);
				otherObject.gameObject.rigidbody.velocity = new Vector3(punk2script.Getnegativediff().x, punk2script.Getnegativediff().y, 
																		otherObject.gameObject.rigidbody.velocity.z);
				playerscript.hits++;
				punk2script.comboedcount++;
				punk2script.comboedreset = 0;
				punk2script.Bleedcash();
				if (punk2script.health <= 4)
				Makelines();
				punk2script.Takedamage(4);
				if (comboing)
					combocount();
			}
			
			//punk3
			if (otherObject.tag == "Enemy3" && playerscript.groundsmash 
				&& otherObject.gameObject.transform.position.y < -200)
			{
				Punk3 punk3script = (Punk3)otherObject.gameObject.GetComponent("Punk3");
				punk3script.dbzmode = true;
				punk3script.shaking();
				Instantiate(smack, 
				new Vector3(otherObject.transform.position.x, otherObject.transform.position.y, -120), otherObject.transform.rotation);
				otherObject.gameObject.rigidbody.velocity = new Vector3(punk3script.Getnegativediff().x, punk3script.Getnegativediff().y, 
																		otherObject.gameObject.rigidbody.velocity.z);
				playerscript.hits++;
				punk3script.comboedcount++;
				punk3script.comboedreset = 0;
				punk3script.Bleedcash();
				if (punk3script.health <= 4)
				Makelines();
				punk3script.Takedamage(4);
				if (comboing)
					combocount();
			}
			
			//creeper
			if (otherObject.tag == "Creep" && playerscript.groundsmash)
			{
				creeper creepscript = (creeper)otherObject.gameObject.GetComponent("creeper");
				creepscript.stoproutine(2);
				if (comboing)
				combocount();
			}
			
			//ATM
			if (otherObject.tag == "ATM" && playerscript.groundsmash)
			{
				atmachine atmscript = (atmachine)otherObject.gameObject.GetComponent("atmachine");
				otherObject.audio.Play();
					atmscript.shake();
					if (atmscript.health <= playerscript.heavydmg)
						Makelines();
					atmscript.health -= playerscript.heavydmg;
					atmscript.ttg += 0.5f;
					atmscript.releasecoins();
				if (comboing)
				combocount();
				
			}
			
		
		//piledriver collisions
		if (playerscript.caughtone && playerscript.piledrive)
		{
			if (otherObject.tag == "Enemy")
			{
				Punk1 enemyscript = (Punk1)otherObject.gameObject.GetComponent("Punk1");
				enemyscript.FlyAway();
				if (comboing)
				combocount();
				
			}
			
			if (otherObject.tag == "Enemythrower")
			{
				Punkthrower enemyscript = (Punkthrower)otherObject.gameObject.GetComponent("Punkthrower");
				enemyscript.FlyAway();
				if (comboing)
				combocount();
				
			}
			
			if (otherObject.tag == "Enemy2")
			{
				Punk2 enemyscript = (Punk2)otherObject.gameObject.GetComponent("Punk2");
				enemyscript.dbzmode = true;
				enemyscript.Takedamage(playerscript.heavydmg);
				if (comboing)
				combocount();
				
			}
			
			if (otherObject.tag == "Enemy3")
			{
				Punk3 enemyscript = (Punk3)otherObject.gameObject.GetComponent("Punk3");
				enemyscript.dbzmode = true;
				enemyscript.Takedamage(playerscript.heavydmg);
				if (comboing)
				combocount();
				
			}
			
			if (otherObject.tag == "Hardcore")
			{
				EnemyWrestler enemyscript = (EnemyWrestler)otherObject.gameObject.GetComponent("EnemyWrestler");
				enemyscript.FlyAway();
				if (comboing)
				combocount();
				
			}
			
			if (otherObject.tag == "Hardcore2")
			{
				EnemyWrestler2 enemyscript = (EnemyWrestler2)otherObject.gameObject.GetComponent("EnemyWrestler2");
				enemyscript.dbzmode = true;
				enemyscript.TakeDamage(playerscript.heavydmg);
				if (comboing)
				combocount();
				
			}
			
			if (otherObject.tag == "Hardcore3")
			{
				EnemyWrestler3 enemyscript = (EnemyWrestler3)otherObject.gameObject.GetComponent("EnemyWrestler3");
				enemyscript.dbzmode = true;
				enemyscript.TakeDamage(playerscript.heavydmg);
				if (comboing)
				combocount();
				
			}
			
			if (otherObject.tag == "Ninja1")
			{
				Ninja1 enemyscript = (Ninja1)otherObject.gameObject.GetComponent("Ninja1");
				enemyscript.dbzmode = true;
				enemyscript.TakeDamage(playerscript.heavydmg);
				if (comboing)
				combocount();
				
			}
			
			if (otherObject.tag == "Ninja2")
			{
				Ninja2 enemyscript = (Ninja2)otherObject.gameObject.GetComponent("Ninja2");
				enemyscript.dbzmode = true;
				enemyscript.TakeDamage(playerscript.heavydmg);
				if (comboing)
				combocount();
				
			}
			
			if (otherObject.tag == "Bomb")
			{
				zoomzoom enemyscript = (zoomzoom)otherObject.gameObject.GetComponent("zoomzoom");
				enemyscript.flyaway();
				enemyscript.health --;
				if (comboing)
				combocount();
				
			}
			
			
			
			
		}//caughtone && piledrive
		
		#endregion
		
		
		
		#region item pickup collisions----------------------------------------------
		if (otherObject.tag == "Sushi")
		{
			playerscript.Healthup(5);	
			audio.PlayOneShot(heal);
			Destroy(otherObject.gameObject);
		}

		if (otherObject.tag == "Speedball")
		{
			audio.PlayOneShot(pickup);
			Destroy(otherObject.gameObject);
		}
		
		if (otherObject.tag == "Money")
		{
			playerscript.moneythisround++;
			audio.PlayOneShot(coin);
			Destroy(otherObject.gameObject);
		}
		
		if (otherObject.tag == "redscroll")
		{
			playerscript.redpickup++;
			audio.PlayOneShot(pickup);
			Destroy(otherObject.gameObject);
		}
		
		if (otherObject.tag == "greenscroll")
		{
			playerscript.greenpickup++;
			audio.PlayOneShot(pickup);
			Destroy(otherObject.gameObject);
		}
		
		if (otherObject.tag == "bluescroll")
		{
			playerscript.bluepickup++;
			audio.PlayOneShot(pickup);
			Destroy(otherObject.gameObject);
		}

		if (otherObject.tag == "Chi")
		{
			playerscript.balls++;	
			if (playerscript.balls > 5)
				playerscript.balls = 5;
			audio.PlayOneShot(pickup);
			Destroy(otherObject.gameObject);
		}
		
		#endregion
		
		
		
		
	}//ontrigger enter
	
	
	void OnTriggerStay(Collider otherObject)
	{	
		
	#region Hurricane collisions----------------------------------------------
		//hurricane collisions-----------------------------------------------------
		if (playerscript.hurricane)
		{
			
			if (otherObject.tag == "Bullet")
			{
				if (PlayerPrefs.GetString("HurricaneT2") != "white")
				otherObject.gameObject.rigidbody.velocity = new Vector3(Random.Range(-1000,1000), Random.Range(-1000,1000), 0);
				
				if (PlayerPrefs.GetString("HurricaneT2") == "white")
				otherObject.gameObject.rigidbody.velocity = new Vector3(Random.Range(-500,500), Random.Range(-500,500), 0);
				
			}
			
			if (otherObject.tag == "Bomb")
			{
				if (hurricaneEffectdelay > 0.1f)
				{
					zoomzoom zzscript = (zoomzoom)otherObject.gameObject.GetComponent("zoomzoom");
					Instantiate(smack, 
					new Vector3(otherObject.transform.position.x, otherObject.transform.position.y, -120), otherObject.transform.rotation);
					audio.PlayOneShot(smacksound1);
					playerscript.hits++;
					if (PlayerPrefs.GetString("HurricaneT2") == "white")
						zzscript.slowed = true;
					if (zzscript.isActive)
						zzscript.health--;
						zzscript.flyaway();
					hurricaneEffectdelay = 0;
					
						if (comboing)
						combocount();
				}
			}
			
			if (otherObject.tag == "Enemythrower")
			{
				Punkthrower enemyscript = (Punkthrower)otherObject.gameObject.GetComponent("Punkthrower");
				if (PlayerPrefs.GetString("HurricaneT2") == "black")
				{
					enemyscript.wallbounce = true;
					return;
				}
				enemyscript.comboedcount++;
				enemyscript.comboedreset = 0;
				enemyscript.Bleedcash();
				Makelines();
				enemyscript.FlyAway();
				if (comboing)
					combocount();
				
			}
			
			if (otherObject.tag == "Enemy")
			{
				Punk1 enemyscript = (Punk1)otherObject.gameObject.GetComponent("Punk1");
				if (PlayerPrefs.GetString("HurricaneT2") == "black")
				{
					enemyscript.wallbounce = true;
					return;
				}
				enemyscript.comboedcount++;
				enemyscript.comboedreset = 0;
				enemyscript.Bleedcash();
				Makelines();
				enemyscript.FlyAway();
				if (GameObject.FindGameObjectWithTag("Tutorial") != null  && PlayerPrefs.GetInt("Currentlevel") == 2)
				{
					Tutorialshit2 tutscript = (Tutorialshit2)GameObject.FindGameObjectWithTag("MainCamera").GetComponent("Tutorialshit2");
					tutscript.abilityuse++;
				}
				if (comboing)
					combocount();
				
			}
			if (otherObject.tag == "Enemy2")
			{
				Punk2 enemyscript = (Punk2)otherObject.gameObject.GetComponent("Punk2");
				if (PlayerPrefs.GetString("HurricaneT2") == "white")
					enemyscript.slowed = true;
				if (PlayerPrefs.GetString("HurricaneT2") == "black")
				{
					enemyscript.wallbounce = true;
					return;
				}
				if (hurricaneEffectdelay > 0.1f)
				{
					enemyscript.dbzmode = true;
					Instantiate(smack, 
					new Vector3(otherObject.transform.position.x, otherObject.transform.position.y, -120), otherObject.transform.rotation);
					enemyscript.comboedcount++;
					enemyscript.comboedreset = 0;
					enemyscript.Bleedcash();
					audio.PlayOneShot(smacksound1);
					playerscript.hits++;
					if (enemyscript.health <= playerscript.attackdmg)
						Makelines();
					enemyscript.Takedamage(playerscript.attackdmg);
					hurricaneEffectdelay = 0;
					if (comboing)
						combocount();
				}
			}
			if (otherObject.tag == "Enemy3")
			{
				Punk3 enemyscript = (Punk3)otherObject.gameObject.GetComponent("Punk3");
				if (PlayerPrefs.GetString("HurricaneT2") == "white")
					enemyscript.slowed = true;
				if (PlayerPrefs.GetString("HurricaneT2") == "black")
				{
					enemyscript.wallbounce = true;
					return;
				}
				if (hurricaneEffectdelay > 0.1f)
				{
					enemyscript.dbzmode = true;
					Instantiate(smack, 
					new Vector3(otherObject.transform.position.x, otherObject.transform.position.y, -120), otherObject.transform.rotation);
					audio.PlayOneShot(smacksound1);
					playerscript.hits++;
					enemyscript.Takedamage(playerscript.attackdmg);
					hurricaneEffectdelay = 0;
					if (comboing)
						combocount();
				}
				
			}
			if (otherObject.tag == "Hardcore")
			{
				EnemyWrestler wrestscript = (EnemyWrestler)otherObject.gameObject.GetComponent("EnemyWrestler");
				playerscript.hits++;
				Makelines();
				wrestscript.FlyAway();
					if (comboing)
					combocount();
				if (GameObject.FindGameObjectWithTag("Tutorial") != null  && PlayerPrefs.GetInt("Currentlevel") == 2)
				{
					Tutorialshit2 tutscript = (Tutorialshit2)GameObject.FindGameObjectWithTag("MainCamera").GetComponent("Tutorialshit2");
					tutscript.abilityuse++;
				}
			}
			
			if (otherObject.tag == "Hardcore2")
			{
				EnemyWrestler2 wrest2script = (EnemyWrestler2)otherObject.gameObject.GetComponent("EnemyWrestler2");
				if (PlayerPrefs.GetString("HurricaneT2") == "white")
					wrest2script.slowed = true;
				if (hurricaneEffectdelay > 0.1f)
				{
					wrest2script.TakeDamage(playerscript.heavydmg);
					Instantiate(smack, 
					new Vector3(otherObject.transform.position.x, otherObject.transform.position.y, -120), otherObject.transform.rotation);
					audio.PlayOneShot(smacksound1);
					playerscript.hits++;
					if (wrest2script.health <=0)
						wrest2script.FlyAway();
					if (comboing)
					combocount();
					hurricaneEffectdelay = 0;
				}
			}
			
			if (otherObject.tag == "Hardcore3")
			{
				EnemyWrestler3 wrestscript = (EnemyWrestler3)otherObject.gameObject.GetComponent("EnemyWrestler3");
				if (PlayerPrefs.GetString("HurricaneT2") == "white")
					wrestscript.slowed = true;
				if (hurricaneEffectdelay > 0.1f)
				{
					wrestscript.TakeDamage(playerscript.heavydmg);
					Instantiate(smack, 
					new Vector3(otherObject.transform.position.x, otherObject.transform.position.y, -120), otherObject.transform.rotation);
					audio.PlayOneShot(smacksound1);
					playerscript.hits++;
					if (wrestscript.health <=0)
						wrestscript.FlyAway();
					if (comboing)
					combocount();
					hurricaneEffectdelay = 0;
				}
			}
			
			if (otherObject.tag == "Creep")
			{
				creeper creepscript = (creeper)otherObject.gameObject.GetComponent("creeper");
				creepscript.stoproutine(2);
				if (PlayerPrefs.GetString("HurricaneT2") == "white")
					creepscript.slowed = true;
				if (comboing && hurricaneEffectdelay > 0.2f)
				{
					combocount();
					hurricaneEffectdelay = 0;
				}
			}
			
			if (otherObject.tag == "Ground")
				{
					EnemyGround gscript = (EnemyGround)otherObject.gameObject.GetComponent("EnemyGround");
					gscript.Die();
					if (comboing)
					combocount();
				}
			
			
			
			if (otherObject.tag == "Ground2")
				{
					EnemyGround3 gscript = (EnemyGround3)otherObject.gameObject.GetComponent("EnemyGround3");
					gscript.Die();
					if (comboing)
					combocount();
				}
			
			if (otherObject.tag == "Ground3")
				{
					EnemyGround3 gscript = (EnemyGround3)otherObject.gameObject.GetComponent("EnemyGround3");
					gscript.Die();
					if (comboing)
					combocount();
				}
			
			if (otherObject.tag == "Ninja1")
			{
				Ninja1 ninja1script = (Ninja1)otherObject.gameObject.GetComponent("Ninja1");
				ninja1script.FlyAway();
				if (comboing)
					combocount();
				
			}
			
			if (otherObject.tag == "Ninja2")
			{
				Ninja2 ninja2script = (Ninja2)otherObject.gameObject.GetComponent("Ninja2");
				if (hurricaneEffectdelay > 0.2f)
				{
					ninja2script.dbzmode = true;
					Instantiate(smack, 
					new Vector3(otherObject.transform.position.x, otherObject.transform.position.y, -120), otherObject.transform.rotation);
					audio.PlayOneShot(smacksound1);
					playerscript.hits++;
					ninja2script.health--;
					hurricaneEffectdelay = 0;
					if (comboing)
						combocount();
				}
				
			}
			
			if (otherObject.tag == "ATM")
				{
					atmachine atmscript = (atmachine)otherObject.gameObject.GetComponent("atmachine");
					if (hurricaneEffectdelay > 0.25f)
					{
						Instantiate(smack, 
						new Vector3(otherObject.transform.position.x, otherObject.transform.position.y, -120), otherObject.transform.rotation);
						audio.PlayOneShot(smacksound1);
						otherObject.audio.Play();
						atmscript.shake();
						if (atmscript.health <= playerscript.attackdmg)
						Makelines();
						atmscript.health -= playerscript.attackdmg;
						atmscript.ttg += 0.2f;
						atmscript.releasecoins();
						playerscript.hits++;
						atmscript.health --;
						if (comboing)
						combocount();
						hurricaneEffectdelay = 0;
					}
					
				}
			
			if (otherObject.tag == "Boss")
				{
					Boss1 boss1script = (Boss1)otherObject.gameObject.GetComponent("Boss1");
					if (hurricaneEffectdelay > 0.1f)
					{
						boss1script.GetHit(1);
						if (comboing && boss1script.gothitdelay > 0.05f)
						combocount();
					}
				}
			
			if (otherObject.tag == "Minion")
				{
					Boss1 boss1script = (Boss1)otherObject.transform.parent.gameObject.GetComponent("Boss1");
					if (hurricaneEffectdelay > 0.1f)
					{
						boss1script.m1GetHit(1);
						if (comboing && boss1script.gothitdelay > 0.05f)
						combocount();
					}
				}
			
			if (otherObject.tag == "Minion2")
				{
					Boss1 boss1script = (Boss1)otherObject.transform.parent.gameObject.GetComponent("Boss1");
					if (hurricaneEffectdelay > 0.1f)
					{
						boss1script.m2GetHit(1);
						if (comboing && boss1script.gothitdelay > 0.05f)
						combocount();
					}
				}
			
			
		}
		#endregion
	
		#region Grabbing for piledriver-------------------------------------------------------------
		
		if (playerscript.grabbing && !playerscript.caughtone)
		{
			if (otherObject.tag == "Enemy")
			{
				Punk1 enemyscript = (Punk1)otherObject.gameObject.GetComponent("Punk1");
				enemyscript.grabbed = true;
				playerscript.caughtone = true;
				
			}
			
			if (otherObject.tag == "Enemythrower")
			{
				Punkthrower enemyscript = (Punkthrower)otherObject.gameObject.GetComponent("Punkthrower");
				enemyscript.grabbed = true;
				playerscript.caughtone = true;
				
			}
			
			if (otherObject.tag == "Enemy2")
			{
				Punk2 enemyscript = (Punk2)otherObject.gameObject.GetComponent("Punk2");
				enemyscript.grabbed = true;
				playerscript.caughtone = true;
				
			}
			
			if (otherObject.tag == "Enemy3")
			{
				Punk3 enemyscript = (Punk3)otherObject.gameObject.GetComponent("Punk3");
				enemyscript.grabbed = true;
				playerscript.caughtone = true;
				
			}
			
			if (otherObject.tag == "Hardcore")
			{
				EnemyWrestler enemyscript = (EnemyWrestler)otherObject.gameObject.GetComponent("EnemyWrestler");
				if (enemyscript.dbzmode)
				{
					enemyscript.grabbed = true;
					playerscript.caughtone = true;
				}
			}
			
			if (otherObject.tag == "Hardcore2")
			{
				EnemyWrestler2 enemyscript = (EnemyWrestler2)otherObject.gameObject.GetComponent("EnemyWrestler2");
				if (enemyscript.dbzmode)
				{
					enemyscript.grabbed = true;
					playerscript.caughtone = true;
				}
			}
			
			if (otherObject.tag == "Hardcore3")
			{
				EnemyWrestler3 enemyscript = (EnemyWrestler3)otherObject.gameObject.GetComponent("EnemyWrestler3");
				if (enemyscript.dbzmode)
				{
					enemyscript.grabbed = true;
					playerscript.caughtone = true;
				}
			}
			
		}
		
		#endregion
		
		//Tackle collisions
		if (playerscript.tackleon)
		{
			if (otherObject.tag == "Enemy")
			{
				Punk1 enemyscript = (Punk1)otherObject.gameObject.GetComponent("Punk1");
				if (!enemyscript.gotdmgbytackle)
				{
					Instantiate(smack, 
					new Vector3(otherObject.transform.position.x, otherObject.transform.position.y, -120), otherObject.transform.rotation);
					audio.PlayOneShot(smash);
					playerscript.hits++;
					if (PlayerPrefs.GetString("TackleT1") != "black") 
					{
						Karateman.rigidbody.velocity = Vector3.zero;
						enemyscript.Takedamage(playerscript.attackdmg);
						enemyscript.Bleedcash();
					}
					enemyscript.gotdmgbytackle =  true;
					if (enemyscript.dbzmode)
					enemyscript.CancelDBZ(Poolphysics(otherObject.gameObject.transform.position));
					if (!enemyscript.dbzmode)
					otherObject.gameObject.rigidbody.velocity = Poolphysics(otherObject.gameObject.transform.position);
					enemyscript.tackled = true;
					enemyscript.comboedcount++;
					enemyscript.comboedreset = 0;
					enemyscript.sametarget = 0;
					if (enemyscript.health <= playerscript.attackdmg)
						Makelines();
					
					if (comboing)
						combocount();
				}
			}
			
			if (otherObject.tag == "Enemythrower")
			{
				Punkthrower enemyscript = (Punkthrower)otherObject.gameObject.GetComponent("Punkthrower");
				if (!enemyscript.gotdmgbytackle)
				{
					Instantiate(smack, 
					new Vector3(otherObject.transform.position.x, otherObject.transform.position.y, -120), otherObject.transform.rotation);
					audio.PlayOneShot(smash);
					playerscript.hits++;
					if (PlayerPrefs.GetString("TackleT1") != "black") 
					{
						Karateman.rigidbody.velocity = Vector3.zero;
						enemyscript.Takedamage(playerscript.attackdmg);
						enemyscript.Bleedcash();
					}
					enemyscript.gotdmgbytackle =  true;
					if (enemyscript.dbzmode)
					enemyscript.CancelDBZ(Poolphysics(otherObject.gameObject.transform.position));
					if (!enemyscript.dbzmode)
					otherObject.gameObject.rigidbody.velocity = Poolphysics(otherObject.gameObject.transform.position);
					enemyscript.tackled = true;
					enemyscript.comboedcount++;
					enemyscript.comboedreset = 0;
					enemyscript.sametarget = 0;
					if (enemyscript.health <= playerscript.attackdmg)
						Makelines();
					
					if (comboing)
						combocount();
				}
			}
			
			if (otherObject.tag == "Enemy2")
			{
				Punk2 enemyscript = (Punk2)otherObject.gameObject.GetComponent("Punk2");
				if (!enemyscript.gotdmgbytackle)
				{
					Instantiate(smack, 
					new Vector3(otherObject.transform.position.x, otherObject.transform.position.y, -120), otherObject.transform.rotation);
					audio.PlayOneShot(smash);
					playerscript.hits++;
					Karateman.rigidbody.velocity = Vector3.zero;
	//				otherObject.gameObject.rigidbody.velocity = Poolphysics(otherObject.gameObject.transform.position);
					enemyscript.gotdmgbytackle =  true;
					if (enemyscript.dbzmode)
					enemyscript.CancelDBZ(Poolphysics(otherObject.gameObject.transform.position));
					if (!enemyscript.dbzmode)
					otherObject.gameObject.rigidbody.velocity = Poolphysics(otherObject.gameObject.transform.position);
					enemyscript.tackled = true;
					enemyscript.Takedamage(playerscript.attackdmg);
					enemyscript.Bleedcash();
					enemyscript.comboedcount++;
					enemyscript.comboedreset = 0;
					enemyscript.sametarget = 0;
					if (enemyscript.health <= playerscript.attackdmg)
						Makelines();
					
					if (comboing)
						combocount();
				}
			}
			
			if (otherObject.tag == "Enemy3")
			{
				Punk3 enemyscript = (Punk3)otherObject.gameObject.GetComponent("Punk3");
				if (!enemyscript.gotdmgbytackle)
				{
//					playerscript.StopCoroutine("DragonDash");
					Instantiate(smack, 
					new Vector3(otherObject.transform.position.x, otherObject.transform.position.y, -120), otherObject.transform.rotation);
					audio.PlayOneShot(smash);
					playerscript.hits++;
					Karateman.rigidbody.velocity = Vector3.zero;
	//				otherObject.gameObject.rigidbody.velocity = Poolphysics(otherObject.gameObject.transform.position);
					enemyscript.gotdmgbytackle =  true;
					if (enemyscript.dbzmode)
					enemyscript.CancelDBZ(Poolphysics(otherObject.gameObject.transform.position));
					if (!enemyscript.dbzmode)
					otherObject.gameObject.rigidbody.velocity = Poolphysics(otherObject.gameObject.transform.position);
					enemyscript.tackled = true;
					enemyscript.Takedamage(playerscript.attackdmg);
					enemyscript.Bleedcash();
					enemyscript.comboedcount++;
					enemyscript.comboedreset = 0;
					enemyscript.sametarget = 0;
					if (enemyscript.health <= playerscript.attackdmg)
						Makelines();
					
					if (comboing)
						combocount();
				}
			}
			
			
			
			
		}//END | Tacklescript
		
		
		//FOrce collisions
		if (playerscript.forceon)
		{
			if (otherObject.tag == "Enemy")
			{
				Punk1 enemyscript = (Punk1)otherObject.gameObject.GetComponent("Punk1");
				
				if (PlayerPrefs.GetString("ForceT1") == "black")
				otherObject.gameObject.rigidbody.velocity = Getdiff(otherObject.gameObject.transform.position) * - 3;
				else
				otherObject.gameObject.rigidbody.velocity = Getdiff(otherObject.gameObject.transform.position) * - 6;
				enemyscript.sametarget = 0;
				
				if (PlayerPrefs.GetString("ForceT1") == "black" && !enemyscript.gotdmgbyforce)
				{
					Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);	
					audio.PlayOneShot(smacksound1);
					enemyscript.Takedamage(0.5f);
					if (enemyscript.health <= 0.5f)
					Makelines();
					enemyscript.Bleedcash();
					enemyscript.gotdmgbyforce = true;
					
				}
				
				if (PlayerPrefs.GetString("ForceT2") == "black")
				{
					enemyscript.dbzmode = true;
					enemyscript.canhityou = false;
				}
				
				if (PlayerPrefs.GetString("ForceT2") == "white" && !enemyscript.gotdmgbyforce)
				{
					int kdchance = Random.Range(1,3);
					if (kdchance == 1)
					enemyscript.getknocked = true;
					enemyscript.gotdmgbyforce = true;
				}
				
			}
			
			if (otherObject.tag == "Enemythrower")
			{
				Punkthrower enemyscript = (Punkthrower)otherObject.gameObject.GetComponent("Punkthrower");
				
				if (PlayerPrefs.GetString("ForceT1") == "black")
				otherObject.gameObject.rigidbody.velocity = Getdiff(otherObject.gameObject.transform.position) * - 3;
				else
				otherObject.gameObject.rigidbody.velocity = Getdiff(otherObject.gameObject.transform.position) * - 6;
				enemyscript.sametarget = 0;
				
				if (PlayerPrefs.GetString("ForceT1") == "black" && !enemyscript.gotdmgbyforce)
				{
					Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);	
					audio.PlayOneShot(smacksound1);
					enemyscript.Takedamage(0.5f);
					if (enemyscript.health <= 0.5f)
					Makelines();
					enemyscript.Bleedcash();
					enemyscript.gotdmgbyforce = true;
					
				}
				
				if (PlayerPrefs.GetString("ForceT2") == "black")
				{
					enemyscript.dbzmode = true;
					enemyscript.canhityou = false;
				}
				
				if (PlayerPrefs.GetString("ForceT2") == "white" && !enemyscript.gotdmgbyforce)
				{
					int kdchance = Random.Range(1,3);
					if (kdchance == 1)
					otherObject.gameObject.rigidbody.velocity = Getdiff(otherObject.gameObject.transform.position) * - 6;
					enemyscript.gotdmgbyforce = true;
				}
				
			}
			
			if (otherObject.tag == "Enemy2")
			{
				Punk2 enemyscript = (Punk2)otherObject.gameObject.GetComponent("Punk2");
				
				if (PlayerPrefs.GetString("ForceT1") == "black")
				otherObject.gameObject.rigidbody.velocity = Getdiff(otherObject.gameObject.transform.position) * - 3;
				else
				otherObject.gameObject.rigidbody.velocity = Getdiff(otherObject.gameObject.transform.position) * - 6;
				enemyscript.sametarget = 0;
				
				if (PlayerPrefs.GetString("ForceT1") == "black" && !enemyscript.gotdmgbyforce)
				{
					Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);	
					audio.PlayOneShot(smacksound1);
					enemyscript.Takedamage(0.5f);
					if (enemyscript.health <= 0.5f)
					Makelines();
					enemyscript.Bleedcash();
					enemyscript.gotdmgbyforce = true;
				}
				
				if (PlayerPrefs.GetString("ForceT2") == "black")
				{
					enemyscript.dbzmode = true;
					enemyscript.canhityou = false;
				}
				
				if (PlayerPrefs.GetString("ForceT2") == "white" && !enemyscript.gotdmgbyforce)
				{
					int kdchance = Random.Range(1,3);
					if (kdchance == 1)
					enemyscript.getknocked = true;
					enemyscript.gotdmgbyforce = true;
				}
			}
			
			if (otherObject.tag == "Enemy3")
			{
				Punk3 enemyscript = (Punk3)otherObject.gameObject.GetComponent("Punk3");
				
				if (PlayerPrefs.GetString("ForceT1") == "black")
				otherObject.gameObject.rigidbody.velocity = Getdiff(otherObject.gameObject.transform.position) * - 3;
				else
				otherObject.gameObject.rigidbody.velocity = Getdiff(otherObject.gameObject.transform.position) * - 6;
				enemyscript.sametarget = 0;
				
				if (PlayerPrefs.GetString("ForceT1") == "black" && !enemyscript.gotdmgbyforce)
				{
					Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);	
					audio.PlayOneShot(smacksound1);
					enemyscript.Takedamage(0.5f);
					if (enemyscript.health <= 0.5f)
					Makelines();
					enemyscript.Bleedcash();
					enemyscript.gotdmgbyforce = true;
				}
				
				if (PlayerPrefs.GetString("ForceT2") == "black")
				{
					enemyscript.dbzmode = true;
					enemyscript.canhityou = false;
				}
				
				if (PlayerPrefs.GetString("ForceT2") == "white" && !enemyscript.gotdmgbyforce)
				{
					int kdchance = Random.Range(1,3);
					if (kdchance == 1)
					enemyscript.getknocked = true;
					enemyscript.gotdmgbyforce = true;
				}
			}
			
			if (otherObject.tag == "Hardcore")
			{
				EnemyWrestler enemyscript = (EnemyWrestler)otherObject.gameObject.GetComponent("EnemyWrestler");
				
				if (PlayerPrefs.GetString("ForceT1") == "black")
				otherObject.gameObject.rigidbody.velocity = Getdiff(otherObject.gameObject.transform.position) * - 1;
				else
				otherObject.gameObject.rigidbody.velocity = Getdiff(otherObject.gameObject.transform.position) * - 2;
				
				
				if (PlayerPrefs.GetString("ForceT1") == "black" && !enemyscript.gotdmgbyforce)
				{
					Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);	
					audio.PlayOneShot(smacksound1);
					enemyscript.TakeDamage(0.5f);
					if (enemyscript.health <= 0.5f)
					Makelines();
					enemyscript.Bleedcash();
					enemyscript.gotdmgbyforce = true;
				}
				
				if (PlayerPrefs.GetString("ForceT2") == "black")
				{
					enemyscript.dbzmode = true;
				}
				
				if (PlayerPrefs.GetString("ForceT2") == "white" && !enemyscript.gotdmgbyforce)
				{
					int kdchance = Random.Range(1,3);
					if (kdchance == 1)
					enemyscript.balloon = true;
					enemyscript.gotdmgbyforce = true;
				}
				
			}
			
			if (otherObject.tag == "Hardcore2")
			{
				EnemyWrestler2 enemyscript = (EnemyWrestler2)otherObject.gameObject.GetComponent("EnemyWrestler2");
				
				if (PlayerPrefs.GetString("ForceT1") == "black")
				otherObject.gameObject.rigidbody.velocity = Getdiff(otherObject.gameObject.transform.position) * - 1;
				else
				otherObject.gameObject.rigidbody.velocity = Getdiff(otherObject.gameObject.transform.position) * - 2;
				
				if (PlayerPrefs.GetString("ForceT1") == "black" && !enemyscript.gotdmgbyforce)
				{
					Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);	
					audio.PlayOneShot(smacksound1);
					enemyscript.TakeDamage(0.5f);
					if (enemyscript.health <= 0.5f)
					Makelines();
					enemyscript.gotdmgbyforce = true;
				}
				
				if (PlayerPrefs.GetString("ForceT2") == "black")
				{
					enemyscript.dbzmode = true;
				}
				
				if (PlayerPrefs.GetString("ForceT2") == "white" && !enemyscript.gotdmgbyforce)
				{
					int kdchance = Random.Range(1,3);
					if (kdchance == 1)
					enemyscript.balloon = true;
					enemyscript.gotdmgbyforce = true;
				}

			}
			
			if (otherObject.tag == "Hardcore3")
			{
				EnemyWrestler3 enemyscript = (EnemyWrestler3)otherObject.gameObject.GetComponent("EnemyWrestler3");
				
				if (PlayerPrefs.GetString("ForceT1") == "black")
				otherObject.gameObject.rigidbody.velocity = Getdiff(otherObject.gameObject.transform.position) * - 1;
				else
				otherObject.gameObject.rigidbody.velocity = Getdiff(otherObject.gameObject.transform.position) * - 2;
				
				if (PlayerPrefs.GetString("ForceT1") == "black" && !enemyscript.gotdmgbyforce)
				{
					Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);	
					audio.PlayOneShot(smacksound1);
					enemyscript.TakeDamage(0.5f);
					if (enemyscript.health <= 0.5f)
					Makelines();
					enemyscript.gotdmgbyforce = true;
				}
				
				if (PlayerPrefs.GetString("ForceT2") == "black")
				{
					enemyscript.dbzmode = true;
				}
				
				if (PlayerPrefs.GetString("ForceT2") == "white" && !enemyscript.gotdmgbyforce)
				{
					int kdchance = Random.Range(1,3);
					if (kdchance == 1)
					enemyscript.balloon = true;
				}
				enemyscript.gotdmgbyforce = true;
			}
			
			if (otherObject.tag == "Ninja2")
			{
				Ninja2 enemyscript = (Ninja2)otherObject.gameObject.GetComponent("Ninja2");
				
				if (PlayerPrefs.GetString("ForceT1") == "black")
				enemyscript.teleevade();
				else
				enemyscript.teleevade();
				
				if (PlayerPrefs.GetString("ForceT1") == "black" && !enemyscript.gotdmgbyforce)
				{
					Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);	
					audio.PlayOneShot(smacksound1);
					enemyscript.Takedamage(0.5f);
					if (enemyscript.health <= 0.5f)
					Makelines();
					enemyscript.Bleedcash();
					enemyscript.gotdmgbyforce = true;
				}
				
				if (PlayerPrefs.GetString("ForceT2") == "black")
				{
					enemyscript.dbzmode = true;
					enemyscript.canhityou = false;
				}
				
				if (PlayerPrefs.GetString("ForceT2") == "white" && !enemyscript.gotdmgbyforce)
				{
//					enemyscript.getknocked = true;
					enemyscript.gotdmgbyforce = true;
				}
				
			}
			
			
			
		}//END | Forcescript
		
		//heavy attack effects script
		//HERE COMES THE RETARDATION
		
		//JUNKLIN/DHORYUKENZA
		//manual controls NON dynasty
		if (!playerscript.dynastystyle)
		{
			if (playerscript.junklin && !staticheavystartup|| playerscript.linkwindow)
			{
				if (otherObject.tag == "Enemy")
					{
						Punk1 enemyscript = (Punk1)otherObject.gameObject.GetComponent("Punk1");
					
					//pressing up
					if (direction == 1)
						{
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							audio.PlayOneShot(smacksound2);
							audio.PlayOneShot(smacksound2);
							audio.PlayOneShot(smacksound2);
							if (transform.position.x > otherObject.transform.position.x)
							enemyscript.CancelDBZ(new Vector3(-200, 909, otherObject.rigidbody.velocity.z));
							if (transform.position.x <= otherObject.transform.position.x)
							enemyscript.CancelDBZ(new Vector3(200, 909, otherObject.rigidbody.velocity.z));
							if (enemyscript.getknocked)
							enemyscript.knockdowntimer = 0;
							enemyscript.getknocked = true;
							enemyscript.comboedcount += 1;
							enemyscript.comboedreset = 0;
							enemyscript.Takedamage(playerscript.attackdmg);
							enemyscript.sametarget = 0;
							enemyscript.Bleedcash();
							playerscript.linkwindow = true;
							playerscript.dbzmode = true;
							playerscript.additionalairtime = true;
							playerscript.linktimer = 0f;
						
							if (tutorial  && PlayerPrefs.GetInt("Currentlevel") == 1)
							{
								tutscript.shoryu++;
								if (tutscript.linkcounter && playerscript.numberforlinks == 1)
								tutscript.linkcombo ++;
							}
							
							ResetDirection();
						if (comboing)
						combocount();
						}
				
					}
				
				if (otherObject.tag == "Enemythrower")
					{
						Punkthrower enemyscript = (Punkthrower)otherObject.gameObject.GetComponent("Punkthrower");
					
					//pressing up
					if (direction == 1)
						{
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							audio.PlayOneShot(smacksound2);
							audio.PlayOneShot(smacksound2);
							audio.PlayOneShot(smacksound2);
							if (transform.position.x > otherObject.transform.position.x)
							enemyscript.CancelDBZ(new Vector3(-200, 909, otherObject.rigidbody.velocity.z));
							if (transform.position.x <= otherObject.transform.position.x)
							enemyscript.CancelDBZ(new Vector3(200, 909, otherObject.rigidbody.velocity.z));
							enemyscript.comboedcount += 1;
							enemyscript.comboedreset = 0;
							enemyscript.Takedamage(playerscript.attackdmg);
							enemyscript.sametarget = 0;
							enemyscript.Bleedcash();
							playerscript.linkwindow = true;
							playerscript.dbzmode = true;
							playerscript.additionalairtime = true;
							playerscript.linktimer = 0f;
							
							ResetDirection();
						if (comboing)
						combocount();
						}
				
					}
				
				if (otherObject.tag == "Enemy2")
					{
						Punk2 enemyscript = (Punk2)otherObject.gameObject.GetComponent("Punk2");
					
					//pressing up
					if (direction == 1)
						{
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							audio.PlayOneShot(smacksound2);
							audio.PlayOneShot(smacksound2);
							audio.PlayOneShot(smacksound2);
							if (transform.position.x > otherObject.transform.position.x)
							enemyscript.CancelDBZ(new Vector3(-200, 909, otherObject.rigidbody.velocity.z));
							if (transform.position.x <= otherObject.transform.position.x)
							enemyscript.CancelDBZ(new Vector3(200, 909, otherObject.rigidbody.velocity.z));
							if (enemyscript.getknocked)
							enemyscript.knockdowntimer = 0;
							enemyscript.getknocked = true;
							playerscript.linkwindow = true;
							playerscript.dbzmode = true;
							playerscript.additionalairtime = true;
							playerscript.linktimer = 0f;
							enemyscript.comboedcount += 1;
							enemyscript.comboedreset = 0;
							enemyscript.sametarget = 0;
							enemyscript.Bleedcash();
							enemyscript.Takedamage(playerscript.attackdmg);
							ResetDirection();
						if (comboing)
						combocount();
						}
				
					}
				
				if (otherObject.tag == "Enemy3")
					{
						Punk3 enemyscript = (Punk3)otherObject.gameObject.GetComponent("Punk3");
					
					//pressing up
					if (direction == 1)
						{
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							audio.PlayOneShot(smacksound2);
							audio.PlayOneShot(smacksound2);
							audio.PlayOneShot(smacksound2);
							if (transform.position.x > otherObject.transform.position.x)
							enemyscript.CancelDBZ(new Vector3(-200, 909, otherObject.rigidbody.velocity.z));
							if (transform.position.x <= otherObject.transform.position.x)
							enemyscript.CancelDBZ(new Vector3(200, 909, otherObject.rigidbody.velocity.z));
							if (enemyscript.getknocked)
							enemyscript.knockdowntimer = 0;
							enemyscript.getknocked = true;
							playerscript.linkwindow = true;
							playerscript.dbzmode = true;
							playerscript.additionalairtime = true;
							playerscript.linktimer = 0f;
							enemyscript.comboedcount += 1;
							enemyscript.comboedreset = 0;
							enemyscript.sametarget = 0;
							enemyscript.Bleedcash();
							enemyscript.Takedamage(playerscript.attackdmg);
							ResetDirection();
						if (comboing)
						combocount();
						}
				
					}
				
				if (otherObject.tag == "Hardcore")
					{
						EnemyWrestler enemyscript = (EnemyWrestler)otherObject.gameObject.GetComponent("EnemyWrestler");
					
					//pressing up
					if (direction == 1 && enemyscript.dbzmode)
						{
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							audio.PlayOneShot(smacksound2);
							audio.PlayOneShot(smacksound2);
							audio.PlayOneShot(smacksound2);
							enemyscript.balloon = true;
							if (transform.position.x > otherObject.transform.position.x)
							enemyscript.CancelDBZ(new Vector3(-200, 794, otherObject.rigidbody.velocity.z));
							if (transform.position.x <= otherObject.transform.position.x)
							enemyscript.CancelDBZ(new Vector3(200, 794, otherObject.rigidbody.velocity.z));
							playerscript.linkwindow = true;
							playerscript.dbzmode = true;
							playerscript.additionalairtime = true;
							playerscript.linktimer = 0f;
							enemyscript.comboedcount += 1;
							enemyscript.comboedreset = 0;
							enemyscript.Bleedcash();
							enemyscript.TakeDamage(playerscript.attackdmg);
							enemyscript.health --;
							ResetDirection();
						if (comboing)
						combocount();
						}
				
					}
				
				if (otherObject.tag == "Hardcore2")
					{
						EnemyWrestler2 enemyscript = (EnemyWrestler2)otherObject.gameObject.GetComponent("EnemyWrestler2");
					
					//pressing up
					if (direction == 1 && enemyscript.dbzmode)
						{
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							audio.PlayOneShot(smacksound2);
							audio.PlayOneShot(smacksound2);
							audio.PlayOneShot(smacksound2);
							enemyscript.balloon = true;
							if (transform.position.x > otherObject.transform.position.x)
							enemyscript.CancelDBZ(new Vector3(-200, 794, otherObject.rigidbody.velocity.z));
							if (transform.position.x <= otherObject.transform.position.x)
							enemyscript.CancelDBZ(new Vector3(200, 794, otherObject.rigidbody.velocity.z));
							playerscript.linkwindow = true;
							playerscript.dbzmode = true;
							playerscript.additionalairtime = true;
							enemyscript.TakeDamage(playerscript.attackdmg);
							playerscript.linktimer = 0f;
							ResetDirection();
						if (comboing)
						combocount();
						}
				
					}
				
				if (otherObject.tag == "Hardcore3")
					{
						EnemyWrestler3 enemyscript = (EnemyWrestler3)otherObject.gameObject.GetComponent("EnemyWrestler3");
					
					//pressing up
					if (direction == 1 && enemyscript.dbzmode)
						{
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							audio.PlayOneShot(smacksound2);
							audio.PlayOneShot(smacksound2);
							audio.PlayOneShot(smacksound2);
							enemyscript.balloon = true;
							if (transform.position.x > otherObject.transform.position.x)
							enemyscript.CancelDBZ(new Vector3(-200, 794, otherObject.rigidbody.velocity.z));
							if (transform.position.x <= otherObject.transform.position.x)
							enemyscript.CancelDBZ(new Vector3(200, 794, otherObject.rigidbody.velocity.z));
							playerscript.linkwindow = true;
							playerscript.dbzmode = true;
							playerscript.additionalairtime = true;
							playerscript.linktimer = 0f;
							ResetDirection();
						if (comboing)
						combocount();
						}
				
					}
				
					if (otherObject.tag == "Bomb")
					{
						zoomzoom zzscript = (zoomzoom)otherObject.gameObject.GetComponent("zoomzoom");
					
					//pressing up
					if (direction == 1)
						{
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							audio.PlayOneShot(smacksound2);
							audio.PlayOneShot(smacksound2);
							audio.PlayOneShot(smacksound2);
							zzscript.flyaway();
							playerscript.linkwindow = true;
							playerscript.dbzmode = true;
							playerscript.additionalairtime = true;
							playerscript.linktimer = 0f;
							ResetDirection();
						if (comboing)
						combocount();
						}
				
					}
				
					if (otherObject.tag == "ATM" && direction == 1)
					{
						atmachine atmscript = (atmachine)otherObject.gameObject.GetComponent("atmachine");
					
						Instantiate(smack, 
							new Vector3(otherObject.transform.position.x, otherObject.transform.position.y, -120), otherObject.transform.rotation);
							audio.PlayOneShot(smacksound1);
							otherObject.audio.Play();
							atmscript.shake();
							if (atmscript.health <= playerscript.attackdmg)
							Makelines();
							atmscript.health -= playerscript.attackdmg;
							atmscript.ttg += 0.5f;
							atmscript.releasecoins();
							playerscript.hits++;
							atmscript.health --;
							ResetDirection();
							if (comboing)
							combocount();
					
					
					}
				
				if (PlayerPrefs.GetString("SpiritbombT1") == "white" || PlayerPrefs.GetString("SpiritbombT2") == "white")
					{
						if (otherObject.tag == "Sbomb")
						{
							if (direction > 0)
							{
								Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
								audio.PlayOneShot(smacksound2);
								SpiritBomb sbscript = (SpiritBomb)otherObject.gameObject.GetComponent("SpiritBomb");	
								sbscript.TriggerExplosion();
								ResetDirection();
							}
							
						}
					
					}
			
			}//END | Junklin
			
		}//END !dynasty
		
		//Shoryu dynasty style
		if (playerscript.dynastystyle && heavyon)
		{
			if (playerscript.linkwindow && playerscript.attackcounter == 2 && doingjunkle)
			{
				if (otherObject.tag == "Enemy")
					{
						Punk1 enemyscript = (Punk1)otherObject.gameObject.GetComponent("Punk1");
					
						Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
						Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
						Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
						audio.PlayOneShot(smacksound2);
						audio.PlayOneShot(smacksound2);
						audio.PlayOneShot(smacksound2);
						if (transform.position.x > otherObject.transform.position.x)
						enemyscript.CancelDBZ(new Vector3(-200, 909, otherObject.rigidbody.velocity.z));
						if (transform.position.x <= otherObject.transform.position.x)
						enemyscript.CancelDBZ(new Vector3(200, 909, otherObject.rigidbody.velocity.z));
						if (enemyscript.getknocked)
						enemyscript.knockdowntimer = 0;
						enemyscript.getknocked = true;
						enemyscript.comboedcount += 1;
						enemyscript.comboedreset = 0;
						enemyscript.Takedamage(playerscript.attackdmg);
						enemyscript.sametarget = 0;
						enemyscript.Bleedcash();
						playerscript.linkwindow = true;
						playerscript.dbzmode = true;
						playerscript.additionalairtime = true;
						playerscript.linktimer = 0f;
					
						if (tutorial  && PlayerPrefs.GetInt("Currentlevel") == 1)
						{
							tutscript.shoryu++;
							if (tutscript.linkcounter && playerscript.numberforlinks == 1)
							tutscript.linkcombo ++;
						}
							
						ResetDirection();
						if (comboing)
						combocount();
				
					}
				
				if (otherObject.tag == "Enemythrower")
					{
						Punkthrower enemyscript = (Punkthrower)otherObject.gameObject.GetComponent("Punkthrower");
					
						Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
						Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
						Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
						audio.PlayOneShot(smacksound2);
						audio.PlayOneShot(smacksound2);
						audio.PlayOneShot(smacksound2);
						if (transform.position.x > otherObject.transform.position.x)
						enemyscript.CancelDBZ(new Vector3(-200, 909, otherObject.rigidbody.velocity.z));
						if (transform.position.x <= otherObject.transform.position.x)
						enemyscript.CancelDBZ(new Vector3(200, 909, otherObject.rigidbody.velocity.z));
						enemyscript.comboedcount += 1;
						enemyscript.comboedreset = 0;
						enemyscript.Takedamage(playerscript.attackdmg);
						enemyscript.sametarget = 0;
						enemyscript.Bleedcash();
						playerscript.linkwindow = true;
						playerscript.dbzmode = true;
						playerscript.additionalairtime = true;
						playerscript.linktimer = 0f;
							
						ResetDirection();
						if (comboing)
						combocount();
				
					}
				
				if (otherObject.tag == "Enemy2")
					{
						Punk2 enemyscript = (Punk2)otherObject.gameObject.GetComponent("Punk2");
					
						Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
						Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
						Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
						audio.PlayOneShot(smacksound2);
						audio.PlayOneShot(smacksound2);
						audio.PlayOneShot(smacksound2);
						if (transform.position.x > otherObject.transform.position.x)
						enemyscript.CancelDBZ(new Vector3(-200, 909, otherObject.rigidbody.velocity.z));
						if (transform.position.x <= otherObject.transform.position.x)
						enemyscript.CancelDBZ(new Vector3(200, 909, otherObject.rigidbody.velocity.z));
						if (enemyscript.getknocked)
						enemyscript.knockdowntimer = 0;
						enemyscript.getknocked = true;
						playerscript.linkwindow = true;
						playerscript.dbzmode = true;
						playerscript.additionalairtime = true;
						playerscript.linktimer = 0f;
						enemyscript.comboedcount += 1;
						enemyscript.comboedreset = 0;
						enemyscript.sametarget = 0;
						enemyscript.Bleedcash();
						enemyscript.Takedamage(playerscript.attackdmg);
						ResetDirection();
						if (comboing)
						combocount();
				
					}
				
				if (otherObject.tag == "Enemy3")
					{
						Punk3 enemyscript = (Punk3)otherObject.gameObject.GetComponent("Punk3");

						Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
						Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
						Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
						audio.PlayOneShot(smacksound2);
						audio.PlayOneShot(smacksound2);
						audio.PlayOneShot(smacksound2);
						if (transform.position.x > otherObject.transform.position.x)
						enemyscript.CancelDBZ(new Vector3(-200, 909, otherObject.rigidbody.velocity.z));
						if (transform.position.x <= otherObject.transform.position.x)
						enemyscript.CancelDBZ(new Vector3(200, 909, otherObject.rigidbody.velocity.z));
						if (enemyscript.getknocked)
						enemyscript.knockdowntimer = 0;
						enemyscript.getknocked = true;
						playerscript.linkwindow = true;
						playerscript.dbzmode = true;
						playerscript.additionalairtime = true;
						playerscript.linktimer = 0f;
						enemyscript.comboedcount += 1;
						enemyscript.comboedreset = 0;
						enemyscript.sametarget = 0;
						enemyscript.Bleedcash();
						enemyscript.Takedamage(playerscript.attackdmg);
						ResetDirection();
						if (comboing)
						combocount();
						
				
					}
				
				if (otherObject.tag == "Hardcore")
					{
						EnemyWrestler enemyscript = (EnemyWrestler)otherObject.gameObject.GetComponent("EnemyWrestler");
					
						Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
						Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
						Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
						audio.PlayOneShot(smacksound2);
						audio.PlayOneShot(smacksound2);
						audio.PlayOneShot(smacksound2);
						enemyscript.balloon = true;
						if (transform.position.x > otherObject.transform.position.x)
						enemyscript.CancelDBZ(new Vector3(-200, 794, otherObject.rigidbody.velocity.z));
						if (transform.position.x <= otherObject.transform.position.x)
						enemyscript.CancelDBZ(new Vector3(200, 794, otherObject.rigidbody.velocity.z));
						playerscript.linkwindow = true;
						playerscript.dbzmode = true;
						playerscript.additionalairtime = true;
						playerscript.linktimer = 0f;
						enemyscript.comboedcount += 1;
						enemyscript.comboedreset = 0;
						enemyscript.Bleedcash();
						enemyscript.TakeDamage(playerscript.attackdmg);
						enemyscript.health --;
						ResetDirection();
						if (comboing)
						combocount();
				
					}
				
				if (otherObject.tag == "Hardcore2")
					{
						EnemyWrestler2 enemyscript = (EnemyWrestler2)otherObject.gameObject.GetComponent("EnemyWrestler2");
					
						Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
						Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
						Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
						audio.PlayOneShot(smacksound2);
						audio.PlayOneShot(smacksound2);
						audio.PlayOneShot(smacksound2);
						enemyscript.balloon = true;
						if (transform.position.x > otherObject.transform.position.x)
						enemyscript.CancelDBZ(new Vector3(-200, 794, otherObject.rigidbody.velocity.z));
						if (transform.position.x <= otherObject.transform.position.x)
						enemyscript.CancelDBZ(new Vector3(200, 794, otherObject.rigidbody.velocity.z));
						playerscript.linkwindow = true;
						playerscript.dbzmode = true;
						playerscript.additionalairtime = true;
						enemyscript.TakeDamage(playerscript.attackdmg);
						playerscript.linktimer = 0f;
						ResetDirection();
						if (comboing)
						combocount();
						
					}
				
				if (otherObject.tag == "Hardcore3")
					{
						EnemyWrestler3 enemyscript = (EnemyWrestler3)otherObject.gameObject.GetComponent("EnemyWrestler3");

					if (enemyscript.dbzmode)
						{
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							audio.PlayOneShot(smacksound2);
							audio.PlayOneShot(smacksound2);
							audio.PlayOneShot(smacksound2);
							enemyscript.balloon = true;
							if (transform.position.x > otherObject.transform.position.x)
							enemyscript.CancelDBZ(new Vector3(-200, 794, otherObject.rigidbody.velocity.z));
							if (transform.position.x <= otherObject.transform.position.x)
							enemyscript.CancelDBZ(new Vector3(200, 794, otherObject.rigidbody.velocity.z));
							playerscript.linkwindow = true;
							playerscript.dbzmode = true;
							playerscript.additionalairtime = true;
							playerscript.linktimer = 0f;
							ResetDirection();
						if (comboing)
						combocount();
						}
				
					}
				
					if (otherObject.tag == "Bomb")
					{
						zoomzoom zzscript = (zoomzoom)otherObject.gameObject.GetComponent("zoomzoom");
					
						Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
						Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
						Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
						audio.PlayOneShot(smacksound2);
						audio.PlayOneShot(smacksound2);
						audio.PlayOneShot(smacksound2);
						zzscript.flyaway();
						playerscript.linkwindow = true;
						playerscript.dbzmode = true;
						playerscript.additionalairtime = true;
						playerscript.linktimer = 0f;
						ResetDirection();
						if (comboing)
						combocount();
						
				
					}
				
					if (otherObject.tag == "ATM")
					{
						atmachine atmscript = (atmachine)otherObject.gameObject.GetComponent("atmachine");
					
						Instantiate(smack, 
						new Vector3(otherObject.transform.position.x, otherObject.transform.position.y, -120), otherObject.transform.rotation);
						audio.PlayOneShot(smacksound1);
						otherObject.audio.Play();
						atmscript.shake();
						if (atmscript.health <= playerscript.attackdmg)
						Makelines();
						atmscript.health -= playerscript.attackdmg;
						atmscript.ttg += 0.5f;
						atmscript.releasecoins();
						playerscript.hits++;
						atmscript.health --;
						ResetDirection();
						if (comboing)
						combocount();
					
					}
				
				if (PlayerPrefs.GetString("SpiritbombT1") == "white" || PlayerPrefs.GetString("SpiritbombT2") == "white")
					{
						if (otherObject.tag == "Sbomb")
						{
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							audio.PlayOneShot(smacksound2);
							SpiritBomb sbscript = (SpiritBomb)otherObject.gameObject.GetComponent("SpiritBomb");	
							sbscript.TriggerExplosion();
							ResetDirection();
							
						}
					
					}
			
			}//END | Junklin
			
		}//end dynastystyle
		
		// Regular Axekick 
		if (!playerscript.dynastystyle)
		{
			if (!staticheavystartup && doingaxe || playerscript.linkwindow && doingaxe)
			{
				if (otherObject.tag == "Enemy")
					{
						Punk1 enemyscript = (Punk1)otherObject.gameObject.GetComponent("Punk1");
					
						if (direction == 2)
						{
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							otherObject.rigidbody.velocity += new Vector3(0, -1700, 0);
							playerscript.dbzmode = true;
							playerscript.additionalairtime = true;
							playerscript.linkwindow = true;
							enemyscript.dbzmode = false;
							enemyscript.falling = true;
							enemyscript.comboedcount++;
							enemyscript.comboedreset = 0;
							enemyscript.Bleedcash();
							enemyscript.sametarget = 0;
							audio.PlayOneShot(smacksound2);
							if (enemyscript.health <= playerscript.heavydmg)
							{
								Makelines();
								audio.PlayOneShot(finalsmack);
							}
							enemyscript.Takedamage(playerscript.heavydmg);
							heavyisReady = true;
						
							if (tutorial  && PlayerPrefs.GetInt("Currentlevel") == 1)
							{
								tutscript.axekick++;
								if (tutscript.linkcounter && playerscript.numberforlinks == 1)
								tutscript.linkcombo ++;
							}
								
							ResetDirection();
							if (comboing)
							combocount();
						}
					
					}
				
				if (otherObject.tag == "Enemythrower")
					{
						Punkthrower enemyscript = (Punkthrower)otherObject.gameObject.GetComponent("Punkthrower");
					
						//pressing down
						if (direction == 2)
						{
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							otherObject.rigidbody.velocity += new Vector3(0, -1700, 0);
							playerscript.dbzmode = true;
							playerscript.additionalairtime = true;
							playerscript.linkwindow = true;
							enemyscript.dbzmode = false;
							enemyscript.falling = true;
							enemyscript.comboedcount++;
							enemyscript.comboedreset = 0;
							enemyscript.Bleedcash();
							enemyscript.sametarget = 0;
							audio.PlayOneShot(smacksound2);
							if (enemyscript.health <= playerscript.heavydmg)
							{
								Makelines();
								audio.PlayOneShot(finalsmack);
							}
							enemyscript.Takedamage(playerscript.heavydmg);
							heavyisReady = true;
							ResetDirection();
							if (comboing)
						combocount();
						}
					
					}
				
				
				//for punk2 same shit
				if (otherObject.tag == "Enemy2")
					{
						Punk2 enemyscript = (Punk2)otherObject.gameObject.GetComponent("Punk2");
					
						//pressing down
						if (direction == 2)
						{
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							otherObject.rigidbody.velocity += new Vector3(0, -1700, 0);
							enemyscript.dbzmode = false;
							enemyscript.falling = true;
							playerscript.additionalairtime = true;
							playerscript.linkwindow = true;
							enemyscript.comboedcount++;
							enemyscript.comboedreset = 0;
							enemyscript.sametarget = 0;
							enemyscript.Bleedcash();
								audio.PlayOneShot(smacksound2);
							if (enemyscript.health <= playerscript.heavydmg)
							{
								Makelines();
								audio.PlayOneShot(finalsmack);
							}
							enemyscript.Takedamage(playerscript.heavydmg);
							heavyisReady = true;
							ResetDirection();
							if (comboing)
							combocount();
						}
					
					}
				
				//for punk3 same shit
				if (otherObject.tag == "Enemy3")
					{
						Punk3 enemyscript = (Punk3)otherObject.gameObject.GetComponent("Punk3");
					
						//pressing down
						if (direction == 2)
						{
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							
								audio.PlayOneShot(smacksound2);
							
							if (enemyscript.health <= playerscript.heavydmg)
							{
								Makelines();
								audio.PlayOneShot(finalsmack);
							}
							enemyscript.Takedamage(playerscript.heavydmg);
							otherObject.rigidbody.velocity += new Vector3(0, -1700, 0);
							enemyscript.dbzmode = false;
							playerscript.dbzmode = true;
							enemyscript.falling = true;
							playerscript.additionalairtime = true;
							playerscript.linkwindow = true;
							enemyscript.comboedcount++;
							enemyscript.comboedreset = 0;
							enemyscript.sametarget = 0;
							enemyscript.Bleedcash();
							heavyisReady = true;
							ResetDirection();
							if (comboing)
							combocount();
						}
					
					}
				
				//for Wrestler1
				if (otherObject.tag == "Hardcore")
					{
						EnemyWrestler enemyscript = (EnemyWrestler)otherObject.gameObject.GetComponent("EnemyWrestler");
					
						//pressing down
						if (direction == 2 && enemyscript.dbzmode)
						{
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							audio.PlayOneShot(smacksound2);
							if (enemyscript.health <= playerscript.heavydmg)
							{
								Makelines();
								audio.PlayOneShot(finalsmack);
							}
							enemyscript.TakeDamage(playerscript.heavydmg);
							enemyscript.CancelDBZ(new Vector3 (0, -1700, 0));
							playerscript.dbzmode = true;
							enemyscript.falling = true;
							playerscript.additionalairtime = true;
							playerscript.linkwindow = true;
							heavyisReady = true;
							ResetDirection();
							if (comboing)
							combocount();
						}
					
					}
				
				//for Wrestler2
				if (otherObject.tag == "Hardcore2")
					{
						EnemyWrestler2 enemyscript = (EnemyWrestler2)otherObject.gameObject.GetComponent("EnemyWrestler2");
					
						//pressing down
						if (direction == 2 && enemyscript.dbzmode)
						{
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							audio.PlayOneShot(smacksound2);
							enemyscript.TakeDamage(playerscript.heavydmg);
							enemyscript.CancelDBZ(new Vector3 (0, -1700, 0));
							playerscript.dbzmode = true;
							enemyscript.falling = true;
							playerscript.additionalairtime = true;
							playerscript.linkwindow = true;
							heavyisReady = true;
							ResetDirection();
							if (comboing)
							combocount();
						}
					
					}
				
				
				if (otherObject.tag == "Ninja1")
					{
						Ninja1 ninja1script = (Ninja1)otherObject.gameObject.GetComponent("Ninja1");
					
						//pressing down
						if (direction == 2)
						{
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							audio.PlayOneShot(smacksound2);
							ninja1script.health -= playerscript.heavydmg;
							otherObject.rigidbody.velocity += new Vector3(0, -1700, 0);
							playerscript.dbzmode = true;
	//						enemyscript.falling = true;
							playerscript.additionalairtime = true;
							playerscript.linkwindow = true;
							heavyisReady = true;
							ResetDirection();
							if (comboing)
							combocount();
						}
					
					}
				
				if (otherObject.tag == "Ninja2")
					{
						Ninja2 ninja2script = (Ninja2)otherObject.gameObject.GetComponent("Ninja2");
					
						//pressing down
						if (direction == 2)
						{
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							audio.PlayOneShot(smacksound2);
							ninja2script.health -= playerscript.heavydmg;
							otherObject.rigidbody.velocity += new Vector3(0, -1700, 0);
							playerscript.dbzmode = true;
	//						enemyscript.falling = true;
							playerscript.additionalairtime = true;
							playerscript.linkwindow = true;
							heavyisReady = true;
							ResetDirection();
							if (comboing)
							combocount();
						}
					
					}
				
				if (otherObject.tag == "Bomb")
					{
						zoomzoom zzscript = (zoomzoom)otherObject.gameObject.GetComponent("zoomzoom");
					
						//pressing down
						if (direction == 2)
						{
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							audio.PlayOneShot(smacksound2);
							playerscript.dbzmode = true;
							playerscript.additionalairtime = true;
							playerscript.linkwindow = true;
							if (zzscript.isActive)
							zzscript.health--;
							zzscript.flyaway();
							heavyisReady = true;
							ResetDirection();
							if (comboing)
							combocount();
						}
					
					}
				
				if (otherObject.tag == "ATM")
					{
						atmachine atmscript = (atmachine)otherObject.gameObject.GetComponent("atmachine");
					
						//pressing down
						if (direction == 2)
						{
							playerscript.dbzmode = true;
							playerscript.additionalairtime = true;
							playerscript.linkwindow = true;
							Instantiate(smack, 
							new Vector3(otherObject.transform.position.x, otherObject.transform.position.y, -120), otherObject.transform.rotation);
							audio.PlayOneShot(smacksound1);
							otherObject.audio.Play();
							atmscript.shake();
							if (atmscript.health <= playerscript.heavydmg)
							Makelines();
							atmscript.health -= playerscript.heavydmg;
							atmscript.ttg += 0.5f;
							atmscript.releasecoins();
							playerscript.hits++;
							atmscript.health --;
							heavyisReady = true;
							ResetDirection();
							if (comboing)
							combocount();
						}
					
						
					}// END | ATM
				
				if (otherObject.tag == "Boss")
					{
						Boss1 bossscript = (Boss1)otherObject.gameObject.GetComponent("Boss1");
					
						//pressing down
						if (direction == 2)
						{
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							audio.PlayOneShot(smacksound2);
							playerscript.dbzmode = true;
							playerscript.additionalairtime = true;
							playerscript.linkwindow = true;
							bossscript.GetHit(1);
							heavyisReady = true;
							ResetDirection();
							if (comboing)
							combocount();
						}
					
					}
				
				if (otherObject.tag == "Minion")
					{
						Boss1 bossscript = (Boss1)otherObject.transform.parent.gameObject.GetComponent("Boss1");
					
						//pressing down
						if (direction == 2)
						{
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							audio.PlayOneShot(smacksound2);
							playerscript.dbzmode = true;
							playerscript.additionalairtime = true;
							playerscript.linkwindow = true;
							bossscript.m1GetHit(1);
							heavyisReady = true;
							ResetDirection();
							if (comboing)
							combocount();
						}
					
					}
				
				if (otherObject.tag == "Minion2")
					{
						Boss1 bossscript = (Boss1)otherObject.transform.parent.gameObject.GetComponent("Boss1");
					
						//pressing down
						if (direction == 2)
						{
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							audio.PlayOneShot(smacksound2);
							playerscript.dbzmode = true;
							playerscript.additionalairtime = true;
							playerscript.linkwindow = true;
							bossscript.m2GetHit(1);
							heavyisReady = true;
							ResetDirection();
							if (comboing)
							combocount();
						}
					
					}
	
			}//end | reguolar axekick
			
		}//END | !dynasty
		
		
		//axekick nasty style
		if (playerscript.dynastystyle && heavyon)
		{
			if (playerscript.linkwindow && playerscript.attackcounter == 1 && doingaxe)
			{
				if (otherObject.tag == "Enemy")
					{
						Punk1 enemyscript = (Punk1)otherObject.gameObject.GetComponent("Punk1");
					
						Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
						otherObject.rigidbody.velocity += new Vector3(0, -1700, 0);
						playerscript.dbzmode = true;
						playerscript.additionalairtime = true;
						playerscript.linkwindow = true;
						enemyscript.dbzmode = false;
						enemyscript.falling = true;
						enemyscript.comboedcount++;
						enemyscript.comboedreset = 0;
						enemyscript.Bleedcash();
						enemyscript.sametarget = 0;
						audio.PlayOneShot(smacksound2);
						if (enemyscript.health <= playerscript.heavydmg)
						{
							Makelines();
							audio.PlayOneShot(finalsmack);
						}
						enemyscript.Takedamage(playerscript.heavydmg);
						heavyisReady = true;
					
						if (tutorial  && PlayerPrefs.GetInt("Currentlevel") == 1)
						{
							tutscript.axekick++;
							if (tutscript.linkcounter && playerscript.numberforlinks == 1)
							tutscript.linkcombo ++;
						}
							
						ResetDirection();
						if (comboing)
						combocount();
					
					}
				
				
				if (otherObject.tag == "Enemythrower")
					{
						Punkthrower enemyscript = (Punkthrower)otherObject.gameObject.GetComponent("Punkthrower");
					
						Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
						otherObject.rigidbody.velocity += new Vector3(0, -1700, 0);
						playerscript.dbzmode = true;
						playerscript.additionalairtime = true;
						playerscript.linkwindow = true;
						enemyscript.dbzmode = false;
						enemyscript.falling = true;
						enemyscript.comboedcount++;
						enemyscript.comboedreset = 0;
						enemyscript.Bleedcash();
						enemyscript.sametarget = 0;
						audio.PlayOneShot(smacksound2);
						if (enemyscript.health <= playerscript.heavydmg)
						{
							Makelines();
							audio.PlayOneShot(finalsmack);
						}
						enemyscript.Takedamage(playerscript.heavydmg);
						heavyisReady = true;
						ResetDirection();
						if (comboing)
						combocount();
					
					}
				
				
				//for punk2 same shit
				if (otherObject.tag == "Enemy2")
					{
						Punk2 enemyscript = (Punk2)otherObject.gameObject.GetComponent("Punk2");
					
						Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
						otherObject.rigidbody.velocity += new Vector3(0, -1700, 0);
						enemyscript.dbzmode = false;
						enemyscript.falling = true;
						playerscript.additionalairtime = true;
						playerscript.linkwindow = true;
						enemyscript.comboedcount++;
						enemyscript.comboedreset = 0;
						enemyscript.sametarget = 0;
						enemyscript.Bleedcash();
							audio.PlayOneShot(smacksound2);
						if (enemyscript.health <= playerscript.heavydmg)
						{
							Makelines();
							audio.PlayOneShot(finalsmack);
						}
						enemyscript.Takedamage(playerscript.heavydmg);
						heavyisReady = true;
						ResetDirection();
						if (comboing)
						combocount();
					
					}
				
				//for punk3 same shit
				if (otherObject.tag == "Enemy3")
					{
						Punk3 enemyscript = (Punk3)otherObject.gameObject.GetComponent("Punk3");
					
						Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
						
						audio.PlayOneShot(smacksound2);
						
						if (enemyscript.health <= playerscript.heavydmg)
						{
							Makelines();
							audio.PlayOneShot(finalsmack);
						}
						enemyscript.Takedamage(playerscript.heavydmg);
						otherObject.rigidbody.velocity += new Vector3(0, -1700, 0);
						enemyscript.dbzmode = false;
						playerscript.dbzmode = true;
						enemyscript.falling = true;
						playerscript.additionalairtime = true;
						playerscript.linkwindow = true;
						enemyscript.comboedcount++;
						enemyscript.comboedreset = 0;
						enemyscript.sametarget = 0;
						enemyscript.Bleedcash();
						heavyisReady = true;
						ResetDirection();
						if (comboing)
						combocount();
					
					}
				
				//for Wrestler1
				if (otherObject.tag == "Hardcore")
					{
						EnemyWrestler enemyscript = (EnemyWrestler)otherObject.gameObject.GetComponent("EnemyWrestler");
					
						if (enemyscript.dbzmode)
						{
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							audio.PlayOneShot(smacksound2);
							if (enemyscript.health <= playerscript.heavydmg)
							{
								Makelines();
								audio.PlayOneShot(finalsmack);
							}
							enemyscript.TakeDamage(playerscript.heavydmg);
							enemyscript.CancelDBZ(new Vector3 (0, -1700, 0));
							playerscript.dbzmode = true;
							enemyscript.falling = true;
							playerscript.additionalairtime = true;
							playerscript.linkwindow = true;
							heavyisReady = true;
							ResetDirection();
							if (comboing)
							combocount();
						}
					
					}
				
				//for Wrestler2
				if (otherObject.tag == "Hardcore2")
					{
						EnemyWrestler2 enemyscript = (EnemyWrestler2)otherObject.gameObject.GetComponent("EnemyWrestler2");
					
						if (enemyscript.dbzmode)
						{
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							audio.PlayOneShot(smacksound2);
							enemyscript.TakeDamage(playerscript.heavydmg);
							enemyscript.CancelDBZ(new Vector3 (0, -1700, 0));
							playerscript.dbzmode = true;
							enemyscript.falling = true;
							playerscript.additionalairtime = true;
							playerscript.linkwindow = true;
							heavyisReady = true;
							ResetDirection();
							if (comboing)
							combocount();
						}
					
					}
				
				
				if (otherObject.tag == "Ninja2")
					{
						Ninja2 ninja2script = (Ninja2)otherObject.gameObject.GetComponent("Ninja2");
					
						Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
						audio.PlayOneShot(smacksound2);
						ninja2script.health -= playerscript.heavydmg;
						otherObject.rigidbody.velocity += new Vector3(0, -1700, 0);
						playerscript.dbzmode = true;
//						enemyscript.falling = true;
						playerscript.additionalairtime = true;
						playerscript.linkwindow = true;
						heavyisReady = true;
						ResetDirection();
						if (comboing)
						combocount();
					
					}
				
				if (otherObject.tag == "Bomb")
					{
						zoomzoom zzscript = (zoomzoom)otherObject.gameObject.GetComponent("zoomzoom");
					
						Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
						audio.PlayOneShot(smacksound2);
						playerscript.dbzmode = true;
						playerscript.additionalairtime = true;
						playerscript.linkwindow = true;
						if (zzscript.isActive)
						zzscript.health--;
						zzscript.flyaway();
						heavyisReady = true;
						ResetDirection();
						if (comboing)
						combocount();
					
					}
				
				if (otherObject.tag == "ATM")
					{
						atmachine atmscript = (atmachine)otherObject.gameObject.GetComponent("atmachine");
					
						playerscript.dbzmode = true;
						playerscript.additionalairtime = true;
						playerscript.linkwindow = true;
						Instantiate(smack, 
						new Vector3(otherObject.transform.position.x, otherObject.transform.position.y, -120), otherObject.transform.rotation);
						audio.PlayOneShot(smacksound1);
						otherObject.audio.Play();
						atmscript.shake();
						if (atmscript.health <= playerscript.heavydmg)
						Makelines();
						atmscript.health -= playerscript.heavydmg;
						atmscript.ttg += 0.5f;
						atmscript.releasecoins();
						playerscript.hits++;
						atmscript.health --;
						heavyisReady = true;
						ResetDirection();
						if (comboing)
						combocount();
						
					}// END | ATM
				
				if (otherObject.tag == "Boss")
					{
						Boss1 bossscript = (Boss1)otherObject.gameObject.GetComponent("Boss1");
					
						Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
						audio.PlayOneShot(smacksound2);
						playerscript.dbzmode = true;
						playerscript.additionalairtime = true;
						playerscript.linkwindow = true;
						bossscript.GetHit(1);
						heavyisReady = true;
						ResetDirection();
						if (comboing)
						combocount();
					
					}
				
				if (otherObject.tag == "Minion")
					{
						Boss1 bossscript = (Boss1)otherObject.transform.parent.gameObject.GetComponent("Boss1");

						Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
						audio.PlayOneShot(smacksound2);
						playerscript.dbzmode = true;
						playerscript.additionalairtime = true;
						playerscript.linkwindow = true;
						bossscript.m1GetHit(1);
						heavyisReady = true;
						ResetDirection();
						if (comboing)
						combocount();
					
					}
				
				if (otherObject.tag == "Minion2")
					{
						Boss1 bossscript = (Boss1)otherObject.transform.parent.gameObject.GetComponent("Boss1");
					
						Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
						audio.PlayOneShot(smacksound2);
						playerscript.dbzmode = true;
						playerscript.additionalairtime = true;
						playerscript.linkwindow = true;
						bossscript.m2GetHit(1);
						heavyisReady = true;
						ResetDirection();
						if (comboing)
						combocount();
					
					}
	
			}//end | axekick
			
			
			
		}//END dynastystyle
		
		//DRAGON KICK ONLY
		if (!staticheavystartup || playerscript.linkwindow)
		{
			if (otherObject.tag == "Enemy")
				{
					Punk1 enemyscript = (Punk1)otherObject.gameObject.GetComponent("Punk1");
				
					if (playerscript.leftkick || playerscript.rightkick)
					{
							
							if (!enemyscript.gotdmgbydkick)
							{
								Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
									audio.PlayOneShot(smacksound2);
									audio.PlayOneShot(smacksound2);
									audio.PlayOneShot(smacksound2);
									enemyscript.sametarget = 0;
								if (enemyscript.comboedcount == 3)
								{
									if (transform.position.x > otherObject.transform.position.x)
									enemyscript.CancelDBZ(new Vector3(-400, 744, otherObject.rigidbody.velocity.z));
									if (transform.position.x <= otherObject.transform.position.x)
									enemyscript.CancelDBZ(new Vector3(400, 744, otherObject.rigidbody.velocity.z));
									enemyscript.getknocked = true;
								}
								if (enemyscript.health <= playerscript.attackdmg)
								{
									Makelines();
									audio.PlayOneShot(finalsmack);
								}
								enemyscript.Takedamage (playerscript.attackdmg);
								if (comboing && DkickEffectdelay <= 0.2f)
								{
									combocount();
									enemyscript.comboedcount++;
									enemyscript.comboedreset = 0;
									enemyscript.Bleedcash();
								}
								
								if (tutorial  && PlayerPrefs.GetInt("Currentlevel") == 1)
								{
									tutscript.dkick++;
									if (tutscript.linkcounter && playerscript.numberforlinks == 1)
									tutscript.linkcombo ++;
								}
						
						
								enemyscript.gotdmgbydkick = true;
							}
							
							if (enemyscript.health <= 0)
								enemyscript.DelayedFly();
								ResetDirection();
						
					}
				}
			
			if (otherObject.tag == "Enemythrower")
				{
					Punkthrower enemyscript = (Punkthrower)otherObject.gameObject.GetComponent("Punkthrower");
				
					if (playerscript.leftkick || playerscript.rightkick)
					{
							
							if (!enemyscript.gotdmgbydkick)
							{
								Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
									audio.PlayOneShot(smacksound2);
									audio.PlayOneShot(smacksound2);
									audio.PlayOneShot(smacksound2);
									enemyscript.sametarget = 0;
								if (enemyscript.comboedcount == 3)
								{
									if (transform.position.x > otherObject.transform.position.x)
									enemyscript.CancelDBZ(new Vector3(-400, 744, otherObject.rigidbody.velocity.z));
									if (transform.position.x <= otherObject.transform.position.x)
									enemyscript.CancelDBZ(new Vector3(400, 744, otherObject.rigidbody.velocity.z));
								}
								if (enemyscript.health <= playerscript.attackdmg)
								{
									Makelines();
									audio.PlayOneShot(finalsmack);
								}
								enemyscript.Takedamage (playerscript.attackdmg);
								if (comboing && DkickEffectdelay <= 0.2f)
								{
									combocount();
									enemyscript.comboedcount++;
									enemyscript.comboedreset = 0;
									enemyscript.Bleedcash();
								}
								enemyscript.gotdmgbydkick = true;
							}
							if (tutorial && PlayerPrefs.GetInt("Currentlevel") == 1)
							tutscript.dkick++;
							if (enemyscript.health <= 0)
								enemyscript.DelayedFly();
								ResetDirection();
						
					}
				}
			
			
			//for punk2 same shit
			if (otherObject.tag == "Enemy2")
				{
					Punk2 enemyscript = (Punk2)otherObject.gameObject.GetComponent("Punk2");
				
					//pressing left or right
					if (playerscript.leftkick || playerscript.rightkick)
					{
							
							if (!enemyscript.gotdmgbydkick)
							{
								Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
								
									audio.PlayOneShot(smacksound2);
									audio.PlayOneShot(smacksound2);
									audio.PlayOneShot(smacksound2);
								enemyscript.sametarget = 0;
								if (enemyscript.comboedcount == 3)
								{
									if (transform.position.x > otherObject.transform.position.x)
									enemyscript.CancelDBZ(new Vector3(-400, 744, otherObject.rigidbody.velocity.z));
									if (transform.position.x <= otherObject.transform.position.x)
									enemyscript.CancelDBZ(new Vector3(400, 744, otherObject.rigidbody.velocity.z));
									enemyscript.getknocked = true;
								}
								if (enemyscript.health <= playerscript.attackdmg)
								{
									Makelines();
									audio.PlayOneShot(finalsmack);
								}
								enemyscript.Takedamage(playerscript.attackdmg);
								if (comboing && DkickEffectdelay <= 0.2f)
								{
									combocount();
									enemyscript.comboedcount++;
									enemyscript.comboedreset = 0;
									enemyscript.Bleedcash();
								}
								enemyscript.gotdmgbydkick = true;
							}
							if (enemyscript.health <= 0)
								enemyscript.DelayedFly();
								ResetDirection();
							
					}
				}
			
			//for punk3 same shit
			if (otherObject.tag == "Enemy3")
				{
					Punk3 enemyscript = (Punk3)otherObject.gameObject.GetComponent("Punk3");
				
					//pressing left or right
					if (playerscript.leftkick || playerscript.rightkick)
					{
							
						if (!enemyscript.gotdmgbydkick)
						{
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
								audio.PlayOneShot(smacksound2);
								audio.PlayOneShot(smacksound2);
								audio.PlayOneShot(smacksound2);
							enemyscript.sametarget = 0;
							if (enemyscript.comboedcount == 3)
								{
									if (transform.position.x > otherObject.transform.position.x)
									enemyscript.CancelDBZ(new Vector3(-400, 744, otherObject.rigidbody.velocity.z));
									if (transform.position.x <= otherObject.transform.position.x)
									enemyscript.CancelDBZ(new Vector3(400, 744, otherObject.rigidbody.velocity.z));
									enemyscript.getknocked = true;
								}
							if (enemyscript.health <= playerscript.attackdmg)
							{
								Makelines();
								audio.PlayOneShot(finalsmack);
							}
							enemyscript.Takedamage(playerscript.attackdmg);
							if (comboing && DkickEffectdelay <= 0.2f)
							{
								combocount();
								enemyscript.comboedcount++;
								enemyscript.comboedreset = 0;
								enemyscript.Bleedcash();
							}
							enemyscript.gotdmgbydkick = true;
						}
						if (enemyscript.health <= 0)
							enemyscript.DelayedFly();
							ResetDirection();
							
					}
				}
			
			//for Wrestler1
			if (otherObject.tag == "Hardcore")
				{
					EnemyWrestler enemyscript = (EnemyWrestler)otherObject.gameObject.GetComponent("EnemyWrestler");
				
					//pressing left or right
					if (playerscript.leftkick || playerscript.rightkick)
					{
							
							if (!enemyscript.gotdmgbydkick && enemyscript.dbzmode)
							{
								Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
								audio.PlayOneShot(smacksound2);
								audio.PlayOneShot(smacksound2);
								audio.PlayOneShot(smacksound2);
								if (enemyscript.health <= playerscript.attackdmg)
								{
									Makelines();
									audio.PlayOneShot(finalsmack);
								}
								enemyscript.TakeDamage(playerscript.attackdmg);
								if (comboing && DkickEffectdelay <= 0.2f)
								{
									combocount();
									enemyscript.comboedcount++;
									enemyscript.comboedreset = 0;
									enemyscript.Bleedcash();
								}
								enemyscript.gotdmgbydkick = true;
							}
								ResetDirection();
							
					}
				}
			
			//for Wrestler2
			if (otherObject.tag == "Hardcore2")
				{
					EnemyWrestler2 enemyscript = (EnemyWrestler2)otherObject.gameObject.GetComponent("EnemyWrestler2");
				
					//pressing left or right
					if (playerscript.leftkick || playerscript.rightkick)
					{
							
							if (!enemyscript.gotdmgbydkick && enemyscript.dbzmode)
							{
								Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
								audio.PlayOneShot(smacksound2);
								audio.PlayOneShot(smacksound2);
								audio.PlayOneShot(smacksound2);
								enemyscript.TakeDamage(playerscript.attackdmg);
								if (comboing && DkickEffectdelay <= 0.2f)
								{
									combocount();
								}
								enemyscript.gotdmgbydkick = true;
							}
								ResetDirection();
							
					}
				}
			
			
			if (otherObject.tag == "Ninja1")
				{
					Ninja1 ninja1script = (Ninja1)otherObject.gameObject.GetComponent("Ninja1");
				
					//pressing left or right
					if (playerscript.leftkick || playerscript.rightkick)
					{
							
							if (!ninja1script.gothitbydkick)
							{
								Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
								audio.PlayOneShot(smacksound2);
								audio.PlayOneShot(smacksound2);
								audio.PlayOneShot(smacksound2);
								ninja1script.health -= playerscript.attackdmg;
								if (comboing && DkickEffectdelay <= 0.2f)
								{
									combocount();
								}
								ninja1script.gothitbydkick = true;
							}
							if (ninja1script.health <= 0)
								ninja1script.DelayedFly();
								ResetDirection();
					}
				}
			
			if (otherObject.tag == "Ninja2")
				{
					Ninja2 ninja2script = (Ninja2)otherObject.gameObject.GetComponent("Ninja2");
				
					//pressing left or right
					if (playerscript.leftkick || playerscript.rightkick)
					{
							
							if (!ninja2script.gothitbydkick)
							{
								Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
								audio.PlayOneShot(smacksound2);
								audio.PlayOneShot(smacksound2);
								audio.PlayOneShot(smacksound2);
								ninja2script.health -= playerscript.attackdmg;
								if (comboing && DkickEffectdelay <= 0.2f)
								{
									combocount();
								}
								ninja2script.gothitbydkick = true;
							}
							if (ninja2script.health <= 0)
								ninja2script.DelayedFly();
								ResetDirection();
					}
				}
			
			if (otherObject.tag == "Bomb")
				{
					zoomzoom zzscript = (zoomzoom)otherObject.gameObject.GetComponent("zoomzoom");
				
					//pressing left or right
					if (playerscript.leftkick || playerscript.rightkick)
					{
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							audio.PlayOneShot(smacksound2);
							if (zzscript.isActive)
							zzscript.health--;
							zzscript.flyaway();
								ResetDirection();
							if (comboing && DkickEffectdelay <= 0.2f)
							{
								combocount();
							}
					}
				}
			
			if (otherObject.tag == "ATM")
				{
					atmachine atmscript = (atmachine)otherObject.gameObject.GetComponent("atmachine");
				
					//pressing left or right
					if (playerscript.leftkick || playerscript.rightkick)
					{	
						if (!atmscript.hitbydkick)
						{
								Instantiate(smack, 
						new Vector3(otherObject.transform.position.x, otherObject.transform.position.y, -120), otherObject.transform.rotation);
						audio.PlayOneShot(smacksound1);
						otherObject.audio.Play();
						atmscript.shake();
						if (atmscript.health <= playerscript.attackdmg)
						Makelines();
						atmscript.health -= playerscript.attackdmg;
						atmscript.ttg += 0.3f;
						atmscript.releasecoins();
						playerscript.hits++;
						atmscript.health --;
						atmscript.hitbydkick = true;
						heavyisReady = true;
						ResetDirection();
						if (comboing)
						combocount();
								combocount();
						}
					}
				}// END | ATM
			
			if (otherObject.tag == "Boss")
				{
					Boss1 bossscript = (Boss1)otherObject.gameObject.GetComponent("Boss1");
				
					//pressing left or right
					if (playerscript.leftkick || playerscript.rightkick)
					{
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							audio.PlayOneShot(smacksound2);
							bossscript.GetHit(1);
								ResetDirection();
							if (comboing && DkickEffectdelay <= 0.2f)
							{
								combocount();
							}
					}
				}
			
			if (otherObject.tag == "Minion")
				{
					Boss1 bossscript = (Boss1)otherObject.transform.parent.gameObject.GetComponent("Boss1");
				
					//pressing left or right
					if (playerscript.leftkick || playerscript.rightkick)
					{
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							audio.PlayOneShot(smacksound2);
							bossscript.m1GetHit(1);
								ResetDirection();
							if (comboing && DkickEffectdelay <= 0.2f)
							{
								combocount();
							}
					}
				}
			
			if (otherObject.tag == "Minion2")
				{
					Boss1 bossscript = (Boss1)otherObject.transform.parent.gameObject.GetComponent("Boss1");
				
					//pressing left or right
					if (playerscript.leftkick || playerscript.rightkick)
					{
							Instantiate(smack, otherObject.transform.position, otherObject.transform.rotation);
							audio.PlayOneShot(smacksound2);
							bossscript.m2GetHit(1);
								ResetDirection();
							if (comboing && DkickEffectdelay <= 0.2f)
							{
								combocount();
							}
					}
				}
			
			
			
		}//end | Dkick
		
		
	}//end | OntriggerStay
	
	
	#region PostUpdate Functions
	
	public IEnumerator ResetHeavy(float time)
	{
		yield return new WaitForSeconds(time);
		
		heavyon = false;
	}
	
	void ResetDirection()
	{
		direction = 0;		
	}
	
	public void combocount()
	{
		combocounter++;
		combotimer = 0f;
	}

	IEnumerator IncreaseCollider (float size)
	{
		(gameObject.collider as SphereCollider).radius = size;
		
		yield return new WaitForSeconds(2);
		
		(gameObject.collider as SphereCollider).radius = 55;
		
	}

	IEnumerator DirectionWindow(int dir, float delay)
	{
		direction = dir;
		directionreceived = true;
		
		yield return new WaitForSeconds(delay);
		
		direction = 0;
		directionreceived = false;
		
	}
	
	IEnumerator Delayedreadyoff(int dir)
	{
		yield return new WaitForSeconds(0.2f);
		
		//shoryu
		if (dir == 1)
		{
			junklinready = false;
			axekickready = false;
			Dkickready = false;
			junkcombo = (int)playerscript.spacebar;
		}
		
		//axekick
		if (dir == 2)
		{
			axekickready = false;
			junklinready = false;
			Dkickready = false;
			axecombo = (int)playerscript.spacebar;
		}
		
		if (dir == 3 || dir == 4)
		{
			junklinready = false;
			axekickready = false;
			Dkickready = false;
			DKcombo = (int)playerscript.spacebar;
		}
		
		
	}

	IEnumerator Resetplayerbools(float delay)
	{	
		yield return new WaitForSeconds(delay);
		
		playerscript.junklin = false;	
		doingaxe = false;
		playerscript.doingheavy = false;
		
		
	}
	
	IEnumerator Delayedheavyonreset(float delay)
	{	
		yield return new WaitForSeconds(delay);
		
		heavyon = false;
		
		
	}
	
	public void resetobox(float delay)
	{	
		StartCoroutine ( Resetplayerbools (delay) );
	}
	
	public void Makelines()
	{
		if (GameObject.FindGameObjectWithTag("LINES") == null)
		Instantiate(lines, new Vector3(Karateman.transform.position.x, Karateman.transform.position.y, -200), Quaternion.Euler(Vector3.zero));	
	}
	#endregion
	
	void OnGUI ()
	{
		myStyle.font = myFont;
		
		if (!playerscript.endtime && !playerscript.stagetime && !pausescript.pausemenud)
		{
			//Some small stats above the health bar
			GUI.Label(new Rect(125, 15, 300, 200), "Spare Change: $" + playerscript.moneythisround.ToString(), myStyle);
			GUI.Label(new Rect(345, 15, 300, 200), " Highest Combo: " + playerscript.highestcombo.ToString(), myStyle);
			
			//combo numbers
			if (comboing)
			{
				DrawNumber();
				
				GUI.color = Color.white;
				GUI.DrawTexture(hitspos, hits);
				
				
				//combo words
				if (combocounter >= 5)
				{
					ComboWords();
					if (worddisplaytimer - Time.deltaTime < 2)
					{
						wordisup = true;
						WriteComboWord();
					}
				}
			}
		
		}
		
	}
	
	void ComboWords()
	{
		
		if (combocounter >= 5)
		{
			comboword = cool;
			Xoffset = 0;
		}
		
		if (combocounter >= 10)
		{
			comboword = fresh;
			Xoffset = 0;
		} 

		if (combocounter >= 15)
		{
			comboword = cowa;
			Xoffset = 60;
		} 
		
		if (combocounter >= 20)
		{
			comboword = swag;
			Xoffset = 0;
		} 
		
		if (combocounter >= 30)
		{
			comboword = stylin;
			Xoffset = 20;
		} 
		if (combocounter >= 40)
		{
			comboword = trippin;
			Xoffset = 20;
		} 
		if (combocounter >= 50)
		{
			comboword = fever;
			Xoffset = 20;
		} 
		if (combocounter >= 60)
		{
			comboword = ridiculous;
			Xoffset = 50;
		}
		if (combocounter >= 70)
		{
			comboword = impossible;
			Xoffset = 50;
		}
		if (combocounter >= 80)
		{
			comboword = legend;
			Xoffset = 50;
		}
		
		
	}
	
	void DrawNumber()
	{
		if (combocounter == 1)
			GUI.DrawTexture(numberpos, one);
		if (combocounter == 2)
			GUI.DrawTexture(numberpos, two);
		if (combocounter == 3)
			GUI.DrawTexture(numberpos, three);
		if (combocounter == 4)
			GUI.DrawTexture(numberpos, four);
		if (combocounter == 5)
			GUI.DrawTexture(numberpos, five);
		if (combocounter == 6)
			GUI.DrawTexture(numberpos, six);
		if (combocounter == 7)
			GUI.DrawTexture(numberpos, seven);
		if (combocounter == 8)
			GUI.DrawTexture(numberpos, eight);
		if (combocounter == 9)
			GUI.DrawTexture(numberpos, nine);
		if (combocounter >= 10 && combocounter < 20)
		{
			GUI.DrawTexture(new Rect (numberpos.x - 40, numberpos.y, numberpos.width, numberpos.height), one);
		}
		if (combocounter == 10)
			GUI.DrawTexture(numberpos, zero);
		if (combocounter == 11)
			GUI.DrawTexture(numberpos, one);
		if (combocounter == 12)
			GUI.DrawTexture(numberpos, two);
		if (combocounter == 13)
			GUI.DrawTexture(numberpos, three);
		if (combocounter == 14)
			GUI.DrawTexture(numberpos, four);
		if (combocounter == 15)
			GUI.DrawTexture(numberpos, five);
		if (combocounter == 16)
			GUI.DrawTexture(numberpos, six);
		if (combocounter == 17)
			GUI.DrawTexture(numberpos, seven);
		if (combocounter == 18)
			GUI.DrawTexture(numberpos, eight);
		if (combocounter == 19)
			GUI.DrawTexture(numberpos, nine);
		if (combocounter >= 20 && combocounter < 30)
		{
			GUI.DrawTexture(new Rect (numberpos.x - 40, numberpos.y, numberpos.width, numberpos.height), two);
		}
		if (combocounter == 20)
			GUI.DrawTexture(numberpos, zero);
		if (combocounter == 21)
			GUI.DrawTexture(numberpos, one);
		if (combocounter == 22)
			GUI.DrawTexture(numberpos, two);
		if (combocounter == 23)
			GUI.DrawTexture(numberpos, three);
		if (combocounter == 24)
			GUI.DrawTexture(numberpos, four);
		if (combocounter == 25)
			GUI.DrawTexture(numberpos, five);
		if (combocounter == 26)
			GUI.DrawTexture(numberpos, six);
		if (combocounter == 27)
			GUI.DrawTexture(numberpos, seven);
		if (combocounter == 28)
			GUI.DrawTexture(numberpos, eight);
		if (combocounter == 29)
			GUI.DrawTexture(numberpos, nine);
		if (combocounter >= 30 && combocounter < 40)
		{
			GUI.DrawTexture(new Rect (numberpos.x - 40, numberpos.y, numberpos.width, numberpos.height), three);
		}
		if (combocounter == 30)
			GUI.DrawTexture(numberpos, zero);
		if (combocounter == 31)
			GUI.DrawTexture(numberpos, one);
		if (combocounter == 32)
			GUI.DrawTexture(numberpos, two);
		if (combocounter == 33)
			GUI.DrawTexture(numberpos, three);
		if (combocounter == 34)
			GUI.DrawTexture(numberpos, four);
		if (combocounter == 35)
			GUI.DrawTexture(numberpos, five);
		if (combocounter == 36)
			GUI.DrawTexture(numberpos, six);
		if (combocounter == 37)
			GUI.DrawTexture(numberpos, seven);
		if (combocounter == 38)
			GUI.DrawTexture(numberpos, eight);
		if (combocounter == 39)
			GUI.DrawTexture(numberpos, nine);
		if (combocounter >= 40 && combocounter < 50)
		{
			GUI.DrawTexture(new Rect (numberpos.x - 40, numberpos.y, numberpos.width, numberpos.height), four);
		}
		if (combocounter == 40)
			GUI.DrawTexture(numberpos, zero);
		if (combocounter == 41)
			GUI.DrawTexture(numberpos, one);
		if (combocounter == 42)
			GUI.DrawTexture(numberpos, two);
		if (combocounter == 43)
			GUI.DrawTexture(numberpos, three);
		if (combocounter == 44)
			GUI.DrawTexture(numberpos, four);
		if (combocounter == 45)
			GUI.DrawTexture(numberpos, five);
		if (combocounter == 46)
			GUI.DrawTexture(numberpos, six);
		if (combocounter == 47)
			GUI.DrawTexture(numberpos, seven);
		if (combocounter == 48)
			GUI.DrawTexture(numberpos, eight);
		if (combocounter == 49)
			GUI.DrawTexture(numberpos, nine);
		if (combocounter >= 50 && combocounter < 60)
		{
			GUI.DrawTexture(new Rect (numberpos.x - 40, numberpos.y, numberpos.width, numberpos.height), five);
		}
		if (combocounter == 50)
			GUI.DrawTexture(numberpos, zero);
		if (combocounter == 51)
			GUI.DrawTexture(numberpos, one);
		if (combocounter == 52)
			GUI.DrawTexture(numberpos, two);
		if (combocounter == 53)
			GUI.DrawTexture(numberpos, three);
		if (combocounter == 54)
			GUI.DrawTexture(numberpos, four);
		if (combocounter == 55)
			GUI.DrawTexture(numberpos, five);
		if (combocounter == 56)
			GUI.DrawTexture(numberpos, six);
		if (combocounter == 57)
			GUI.DrawTexture(numberpos, seven);
		if (combocounter == 58)
			GUI.DrawTexture(numberpos, eight);
		if (combocounter == 59)
			GUI.DrawTexture(numberpos, nine);
		if (combocounter >= 60 && combocounter < 70)
		{
			GUI.DrawTexture(new Rect (numberpos.x - 40, numberpos.y, numberpos.width, numberpos.height), six);
		}
		if (combocounter == 60)
			GUI.DrawTexture(numberpos, zero);
		if (combocounter == 61)
			GUI.DrawTexture(numberpos, one);
		if (combocounter == 62)
			GUI.DrawTexture(numberpos, two);
		if (combocounter == 63)
			GUI.DrawTexture(numberpos, three);
		if (combocounter == 64)
			GUI.DrawTexture(numberpos, four);
		if (combocounter == 65)
			GUI.DrawTexture(numberpos, five);
		if (combocounter == 66)
			GUI.DrawTexture(numberpos, six);
		if (combocounter == 67)
			GUI.DrawTexture(numberpos, seven);
		if (combocounter == 68)
			GUI.DrawTexture(numberpos, eight);
		if (combocounter == 69)
			GUI.DrawTexture(numberpos, nine);
		if (combocounter >= 70 && combocounter < 80)
		{
			GUI.DrawTexture(new Rect (numberpos.x - 40, numberpos.y, numberpos.width, numberpos.height), seven);
		}
		if (combocounter == 70)
			GUI.DrawTexture(numberpos, zero);
		if (combocounter == 71)
			GUI.DrawTexture(numberpos, one);
		if (combocounter == 72)
			GUI.DrawTexture(numberpos, two);
		if (combocounter == 73)
			GUI.DrawTexture(numberpos, three);
		if (combocounter == 74)
			GUI.DrawTexture(numberpos, four);
		if (combocounter == 75)
			GUI.DrawTexture(numberpos, five);
		if (combocounter == 76)
			GUI.DrawTexture(numberpos, six);
		if (combocounter == 77)
			GUI.DrawTexture(numberpos, seven);
		if (combocounter == 78)
			GUI.DrawTexture(numberpos, eight);
		if (combocounter == 79)
			GUI.DrawTexture(numberpos, nine);
		if (combocounter >= 80 && combocounter < 90)
		{
			GUI.DrawTexture(new Rect (numberpos.x - 40, numberpos.y, numberpos.width, numberpos.height), eight);
		}
		if (combocounter == 80)
			GUI.DrawTexture(numberpos, zero);
		if (combocounter == 81)
			GUI.DrawTexture(numberpos, one);
		if (combocounter == 82)
			GUI.DrawTexture(numberpos, two);
		if (combocounter == 83)
			GUI.DrawTexture(numberpos, three);
		if (combocounter == 84)
			GUI.DrawTexture(numberpos, four);
		if (combocounter == 85)
			GUI.DrawTexture(numberpos, five);
		if (combocounter == 86)
			GUI.DrawTexture(numberpos, six);
		if (combocounter == 87)
			GUI.DrawTexture(numberpos, seven);
		if (combocounter == 88)
			GUI.DrawTexture(numberpos, eight);
		if (combocounter == 89)
			GUI.DrawTexture(numberpos, nine);
		if (combocounter >= 90 && combocounter < 100)
		{
			GUI.DrawTexture(new Rect (numberpos.x - 40, numberpos.y, numberpos.width, numberpos.height), nine);
		}
		if (combocounter == 90)
			GUI.DrawTexture(numberpos, zero);
		if (combocounter == 91)
			GUI.DrawTexture(numberpos, one);
		if (combocounter == 92)
			GUI.DrawTexture(numberpos, two);
		if (combocounter == 93)
			GUI.DrawTexture(numberpos, three);
		if (combocounter == 94)
			GUI.DrawTexture(numberpos, four);
		if (combocounter == 95)
			GUI.DrawTexture(numberpos, five);
		if (combocounter == 96)
			GUI.DrawTexture(numberpos, six);
		if (combocounter == 97)
			GUI.DrawTexture(numberpos, seven);
		if (combocounter == 98)
			GUI.DrawTexture(numberpos, eight);
		if (combocounter == 99)
			GUI.DrawTexture(numberpos, nine);
		if (combocounter >= 100 && combocounter < 200)
		{
			GUI.DrawTexture(new Rect (numberpos.x - 80, numberpos.y, numberpos.width, numberpos.height), one);
		}
		if (combocounter >= 100 && combocounter < 110)
		{
			GUI.DrawTexture(new Rect (numberpos.x - 40, numberpos.y, numberpos.width, numberpos.height), zero);
		}
		if (combocounter == 100)
			GUI.DrawTexture(numberpos, zero);
		if (combocounter == 101)
			GUI.DrawTexture(numberpos, one);
		if (combocounter == 102)
			GUI.DrawTexture(numberpos, two);
		if (combocounter == 103)
			GUI.DrawTexture(numberpos, three);
		if (combocounter == 104)
			GUI.DrawTexture(numberpos, four);
		if (combocounter == 105)
			GUI.DrawTexture(numberpos, five);
		if (combocounter == 106)
			GUI.DrawTexture(numberpos, six);
		if (combocounter == 107)
			GUI.DrawTexture(numberpos, seven);
		if (combocounter == 108)
			GUI.DrawTexture(numberpos, eight);
		if (combocounter == 109)
			GUI.DrawTexture(numberpos, nine);
		if (combocounter >= 110 && combocounter < 120)
		{
			GUI.DrawTexture(new Rect (numberpos.x - 40, numberpos.y, numberpos.width, numberpos.height), one);
		}
		if (combocounter == 110)
			GUI.DrawTexture(numberpos, zero);
		if (combocounter == 111)
			GUI.DrawTexture(numberpos, one);
		if (combocounter == 112)
			GUI.DrawTexture(numberpos, two);
		if (combocounter == 113)
			GUI.DrawTexture(numberpos, three);
		if (combocounter == 114)
			GUI.DrawTexture(numberpos, four);
		if (combocounter == 115)
			GUI.DrawTexture(numberpos, five);
		if (combocounter == 116)
			GUI.DrawTexture(numberpos, six);
		if (combocounter == 117)
			GUI.DrawTexture(numberpos, seven);
		if (combocounter == 118)
			GUI.DrawTexture(numberpos, eight);
		if (combocounter == 119)
			GUI.DrawTexture(numberpos, nine);
		if (combocounter >= 120 && combocounter < 130)
		{
			GUI.DrawTexture(new Rect (numberpos.x - 40, numberpos.y, numberpos.width, numberpos.height), two);
		}
		if (combocounter == 120)
			GUI.DrawTexture(numberpos, zero);
		if (combocounter == 121)
			GUI.DrawTexture(numberpos, one);
		if (combocounter == 122)
			GUI.DrawTexture(numberpos, two);
		if (combocounter == 123)
			GUI.DrawTexture(numberpos, three);
		if (combocounter == 124)
			GUI.DrawTexture(numberpos, four);
		if (combocounter == 125)
			GUI.DrawTexture(numberpos, five);
		if (combocounter == 126)
			GUI.DrawTexture(numberpos, six);
		if (combocounter == 127)
			GUI.DrawTexture(numberpos, seven);
		if (combocounter == 128)
			GUI.DrawTexture(numberpos, eight);
		if (combocounter == 129)
			GUI.DrawTexture(numberpos, nine);
		if (combocounter >= 130 && combocounter < 140)
		{
			GUI.DrawTexture(new Rect (numberpos.x - 40, numberpos.y, numberpos.width, numberpos.height), three);
		}
		if (combocounter == 130)
			GUI.DrawTexture(numberpos, zero);
		if (combocounter == 131)
			GUI.DrawTexture(numberpos, one);
		if (combocounter == 132)
			GUI.DrawTexture(numberpos, two);
		if (combocounter == 133)
			GUI.DrawTexture(numberpos, three);
		if (combocounter == 134)
			GUI.DrawTexture(numberpos, four);
		if (combocounter == 135)
			GUI.DrawTexture(numberpos, five);
		if (combocounter == 136)
			GUI.DrawTexture(numberpos, six);
		if (combocounter == 137)
			GUI.DrawTexture(numberpos, seven);
		if (combocounter == 138)
			GUI.DrawTexture(numberpos, eight);
		if (combocounter == 139)
			GUI.DrawTexture(numberpos, nine);
		if (combocounter >= 140 && combocounter < 150)
		{
			GUI.DrawTexture(new Rect (numberpos.x - 40, numberpos.y, numberpos.width, numberpos.height), four);
		}
		if (combocounter == 140)
			GUI.DrawTexture(numberpos, zero);
		if (combocounter == 141)
			GUI.DrawTexture(numberpos, one);
		if (combocounter == 142)
			GUI.DrawTexture(numberpos, two);
		if (combocounter == 143)
			GUI.DrawTexture(numberpos, three);
		if (combocounter == 144)
			GUI.DrawTexture(numberpos, four);
		if (combocounter == 145)
			GUI.DrawTexture(numberpos, five);
		if (combocounter == 146)
			GUI.DrawTexture(numberpos, six);
		if (combocounter == 147)
			GUI.DrawTexture(numberpos, seven);
		if (combocounter == 148)
			GUI.DrawTexture(numberpos, eight);
		if (combocounter == 149)
			GUI.DrawTexture(numberpos, nine);
		if (combocounter >= 150 && combocounter < 160)
		{
			GUI.DrawTexture(new Rect (numberpos.x - 40, numberpos.y, numberpos.width, numberpos.height), five);
		}
		if (combocounter == 150)
			GUI.DrawTexture(numberpos, zero);
		if (combocounter == 151)
			GUI.DrawTexture(numberpos, one);
		if (combocounter == 152)
			GUI.DrawTexture(numberpos, two);
		if (combocounter == 153)
			GUI.DrawTexture(numberpos, three);
		if (combocounter == 154)
			GUI.DrawTexture(numberpos, four);
		if (combocounter == 155)
			GUI.DrawTexture(numberpos, five);
		if (combocounter == 156)
			GUI.DrawTexture(numberpos, six);
		if (combocounter == 157)
			GUI.DrawTexture(numberpos, seven);
		if (combocounter == 158)
			GUI.DrawTexture(numberpos, eight);
		if (combocounter == 159)
			GUI.DrawTexture(numberpos, nine);
		if (combocounter >= 160 && combocounter < 170)
		{
			GUI.DrawTexture(new Rect (numberpos.x - 40, numberpos.y, numberpos.width, numberpos.height), six);
		}
		if (combocounter == 160)
			GUI.DrawTexture(numberpos, zero);
		if (combocounter == 161)
			GUI.DrawTexture(numberpos, one);
		if (combocounter == 162)
			GUI.DrawTexture(numberpos, two);
		if (combocounter == 163)
			GUI.DrawTexture(numberpos, three);
		if (combocounter == 164)
			GUI.DrawTexture(numberpos, four);
		if (combocounter == 165)
			GUI.DrawTexture(numberpos, five);
		if (combocounter == 166)
			GUI.DrawTexture(numberpos, six);
		if (combocounter == 167)
			GUI.DrawTexture(numberpos, seven);
		if (combocounter == 168)
			GUI.DrawTexture(numberpos, eight);
		if (combocounter == 169)
			GUI.DrawTexture(numberpos, nine);
		if (combocounter >= 170 && combocounter < 180)
		{
			GUI.DrawTexture(new Rect (numberpos.x - 40, numberpos.y, numberpos.width, numberpos.height), seven);
		}
		if (combocounter == 170)
			GUI.DrawTexture(numberpos, zero);
		if (combocounter == 171)
			GUI.DrawTexture(numberpos, one);
		if (combocounter == 172)
			GUI.DrawTexture(numberpos, two);
		if (combocounter == 173)
			GUI.DrawTexture(numberpos, three);
		if (combocounter == 174)
			GUI.DrawTexture(numberpos, four);
		if (combocounter == 175)
			GUI.DrawTexture(numberpos, five);
		if (combocounter == 176)
			GUI.DrawTexture(numberpos, six);
		if (combocounter == 177)
			GUI.DrawTexture(numberpos, seven);
		if (combocounter == 178)
			GUI.DrawTexture(numberpos, eight);
		if (combocounter == 179)
			GUI.DrawTexture(numberpos, nine);
		if (combocounter >= 180 && combocounter < 190)
		{
			GUI.DrawTexture(new Rect (numberpos.x - 40, numberpos.y, numberpos.width, numberpos.height), eight);
		}
		if (combocounter == 180)
			GUI.DrawTexture(numberpos, zero);
		if (combocounter == 181)
			GUI.DrawTexture(numberpos, one);
		if (combocounter == 182)
			GUI.DrawTexture(numberpos, two);
		if (combocounter == 183)
			GUI.DrawTexture(numberpos, three);
		if (combocounter == 184)
			GUI.DrawTexture(numberpos, four);
		if (combocounter == 185)
			GUI.DrawTexture(numberpos, five);
		if (combocounter == 186)
			GUI.DrawTexture(numberpos, six);
		if (combocounter == 187)
			GUI.DrawTexture(numberpos, seven);
		if (combocounter == 188)
			GUI.DrawTexture(numberpos, eight);
		if (combocounter == 189)
			GUI.DrawTexture(numberpos, nine);
		if (combocounter >= 190 && combocounter < 200)
		{
			GUI.DrawTexture(new Rect (numberpos.x - 40, numberpos.y, numberpos.width, numberpos.height), nine);
		}
		if (combocounter == 190)
			GUI.DrawTexture(numberpos, zero);
		if (combocounter == 191)
			GUI.DrawTexture(numberpos, one);
		if (combocounter == 192)
			GUI.DrawTexture(numberpos, two);
		if (combocounter == 193)
			GUI.DrawTexture(numberpos, three);
		if (combocounter == 194)
			GUI.DrawTexture(numberpos, four);
		if (combocounter == 195)
			GUI.DrawTexture(numberpos, five);
		if (combocounter == 196)
			GUI.DrawTexture(numberpos, six);
		if (combocounter == 197)
			GUI.DrawTexture(numberpos, seven);
		if (combocounter == 198)
			GUI.DrawTexture(numberpos, eight);
		if (combocounter == 199)
			GUI.DrawTexture(numberpos, nine);
		if (combocounter >= 200 && combocounter < 300)
		{
			GUI.DrawTexture(new Rect (numberpos.x - 80, numberpos.y, numberpos.width, numberpos.height), two);
		}
		if (combocounter >= 200 && combocounter < 210)
		{
			GUI.DrawTexture(new Rect (numberpos.x - 40, numberpos.y, numberpos.width, numberpos.height), zero);
		}
		if (combocounter == 200)
			GUI.DrawTexture(numberpos, zero);
		if (combocounter == 201)
			GUI.DrawTexture(numberpos, one);
		if (combocounter == 202)
			GUI.DrawTexture(numberpos, two);
		if (combocounter == 203)
			GUI.DrawTexture(numberpos, three);
		if (combocounter == 204)
			GUI.DrawTexture(numberpos, four);
		if (combocounter == 205)
			GUI.DrawTexture(numberpos, five);
		if (combocounter == 206)
			GUI.DrawTexture(numberpos, six);
		if (combocounter == 207)
			GUI.DrawTexture(numberpos, seven);
		if (combocounter == 208)
			GUI.DrawTexture(numberpos, eight);
		if (combocounter == 209)
			GUI.DrawTexture(numberpos, nine);
		if (combocounter >= 210 && combocounter < 220)
		{
			GUI.DrawTexture(new Rect (numberpos.x - 40, numberpos.y, numberpos.width, numberpos.height), one);
		}
		if (combocounter == 210)
			GUI.DrawTexture(numberpos, zero);
		if (combocounter == 211)
			GUI.DrawTexture(numberpos, one);
		if (combocounter == 212)
			GUI.DrawTexture(numberpos, two);
		if (combocounter == 213)
			GUI.DrawTexture(numberpos, three);
		if (combocounter == 214)
			GUI.DrawTexture(numberpos, four);
		if (combocounter == 215)
			GUI.DrawTexture(numberpos, five);
		if (combocounter == 216)
			GUI.DrawTexture(numberpos, six);
		if (combocounter == 217)
			GUI.DrawTexture(numberpos, seven);
		if (combocounter == 218)
			GUI.DrawTexture(numberpos, eight);
		if (combocounter == 219)
			GUI.DrawTexture(numberpos, nine);
		if (combocounter >= 220 && combocounter < 230)
		{
			GUI.DrawTexture(new Rect (numberpos.x - 40, numberpos.y, numberpos.width, numberpos.height), two);
		}
		if (combocounter == 220)
			GUI.DrawTexture(numberpos, zero);
		if (combocounter == 221)
			GUI.DrawTexture(numberpos, one);
		if (combocounter == 222)
			GUI.DrawTexture(numberpos, two);
		if (combocounter == 223)
			GUI.DrawTexture(numberpos, three);
		if (combocounter == 224)
			GUI.DrawTexture(numberpos, four);
		if (combocounter == 225)
			GUI.DrawTexture(numberpos, five);
		if (combocounter == 226)
			GUI.DrawTexture(numberpos, six);
		if (combocounter == 227)
			GUI.DrawTexture(numberpos, seven);
		if (combocounter == 228)
			GUI.DrawTexture(numberpos, eight);
		if (combocounter == 229)
			GUI.DrawTexture(numberpos, nine);
		if (combocounter >= 230 && combocounter < 240)
		{
			GUI.DrawTexture(new Rect (numberpos.x - 40, numberpos.y, numberpos.width, numberpos.height), three);
		}
		if (combocounter == 230)
			GUI.DrawTexture(numberpos, zero);
		if (combocounter == 231)
			GUI.DrawTexture(numberpos, one);
		if (combocounter == 232)
			GUI.DrawTexture(numberpos, two);
		if (combocounter == 233)
			GUI.DrawTexture(numberpos, three);
		if (combocounter == 234)
			GUI.DrawTexture(numberpos, four);
		if (combocounter == 235)
			GUI.DrawTexture(numberpos, five);
		if (combocounter == 236)
			GUI.DrawTexture(numberpos, six);
		if (combocounter == 237)
			GUI.DrawTexture(numberpos, seven);
		if (combocounter == 238)
			GUI.DrawTexture(numberpos, eight);
		if (combocounter == 239)
			GUI.DrawTexture(numberpos, nine);
		if (combocounter >= 240 && combocounter < 250)
		{
			GUI.DrawTexture(new Rect (numberpos.x - 40, numberpos.y, numberpos.width, numberpos.height), four);
		}
		if (combocounter == 240)
			GUI.DrawTexture(numberpos, zero);
		if (combocounter == 241)
			GUI.DrawTexture(numberpos, one);
		if (combocounter == 242)
			GUI.DrawTexture(numberpos, two);
		if (combocounter == 243)
			GUI.DrawTexture(numberpos, three);
		if (combocounter == 244)
			GUI.DrawTexture(numberpos, four);
		if (combocounter == 245)
			GUI.DrawTexture(numberpos, five);
		if (combocounter == 246)
			GUI.DrawTexture(numberpos, six);
		if (combocounter == 247)
			GUI.DrawTexture(numberpos, seven);
		if (combocounter == 248)
			GUI.DrawTexture(numberpos, eight);
		if (combocounter == 249)
			GUI.DrawTexture(numberpos, nine);
		if (combocounter >= 250)
		{
			GUI.DrawTexture(new Rect (numberpos.x - 40, numberpos.y, numberpos.width, numberpos.height), four);
			GUI.DrawTexture(numberpos, zero);
		}
	}
	
	void WriteComboWord()
	{
		wordbasepos = new Rect ((Screen.width - 280) - Xoffset, 190, 305, 47);
		GUI.color = Color.white;
		if (comboword == trippin || comboword == stylin)
		{
			GUI.DrawTexture(wordbasepos, comboword, ScaleMode.ScaleToFit);
			worddisplaytimer = 0;
			wordisup = false;
		}
		else
		{
			GUI.DrawTexture(wordbasepos, comboword, ScaleMode.ScaleToFit);
			worddisplaytimer = 0;
			wordisup = false;	
		}
	}
	
	public Vector3 Getdiff(Vector3 enemyposition)
	{
		Vector3 diff = Karateman.transform.position - enemyposition; 
		return diff;
	}
	
	Vector3 Poolphysics(Vector3 enemypos)
	{
		float Xtravel = 0, Ytravel = 0;
		
		//he is on your right
		if (!playerscript.facingright)
		{
			Xtravel = -1200;
		}
		
		//he is on left
		if (playerscript.facingright)
		{
			Xtravel = 1200;
		}
		
		//he is above
		if (enemypos.y > Karateman.transform.position.y)
		{
			Ytravel = 500;
			
		}
		
		//he is below
		if (enemypos.y < Karateman.transform.position.y)
		{
			Ytravel = 600;
			
		}
		
			
		return new Vector3(Xtravel, Ytravel, 0);
	}
}
