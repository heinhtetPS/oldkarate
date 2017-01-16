using UnityEngine;
using System.Collections;

public class leftrightbox : MonoBehaviour {

	public Player playerscript;
	public Karateoboxnew obox;
	public GameObject karateman;
	private Tutorialshit tutscript;
	
	public bool dmgReady;
	public float dmgCD;
	private float targettingdelay;
	private bool buttonpressed;
	
	private bool sstrikeavailable = false, sstrikeready = false;
	private float sstrikeCD;
	private bool sstrikestartup = false;
	private float startuptimer = 0;
	
	private bool hadoready = true;
	private float hadoCD;
	
	public ParticleSystem smackanimu, smackred, smackgreen, smackpurple;
	public GameObject hado, lines, piece, blackscreen, specialline;
	public AudioClip smacksound, smacksound2, finalsmack, heal, linesound;
	
	public bool displaced = false;
	float displacementtimer = 0;
	
	public BoxCollider hitbox;
	
	private int[] randomangles = { 45, -45, 90, -90, 180, -180 };
	
	void Start () {
		
		//object initialization
		karateman = GameObject.FindGameObjectWithTag("Player");
		tutscript = (Tutorialshit)GameObject.FindGameObjectWithTag("MainCamera").GetComponent("Tutorialshit");
		
		//checking for sstrike availability
		if (PlayerPrefs.GetString("Slot5") == "Sstrike")
			sstrikeavailable = true;

	}
	
	// Update is called once per frame
	void Update () {
		
		//pro method to stop the LMB miss
		buttonpressed = buttonpressed || Input.GetButtonDown("fight") || Input.GetKeyDown(KeyCode.LeftArrow);
		
		//hitbox location determination
		if (playerscript.MousetotheRight())
		{
			hitbox.transform.position = new Vector3
			(karateman.transform.position.x + 50, karateman.transform.position.y, hitbox.transform.position.z);
		}
		
		if (!playerscript.MousetotheRight())
		{
			hitbox.transform.position = new Vector3
			(karateman.transform.position.x - 50, karateman.transform.position.y, hitbox.transform.position.z);
		}
		
		
		
		//cooldown timers
		
		//delay after LAST attack
		if (!dmgReady && playerscript.attackcounter == 1)
		{
			dmgCD += Time.deltaTime;
			//working one is .33
		if (dmgCD >= 0.33f)
			{
				dmgReady = true;
				dmgCD = 0;
			}
		}
		
		//delay after FIRST attack
		if (!dmgReady && playerscript.attackcounter == 2)
		{
			dmgCD += Time.deltaTime;
			//working one is .3
		if (dmgCD >= 0.3f)
			{
				dmgReady = true;
				dmgCD = 0;
			}
		}
		
		//delay after SECOND attack
		if (!dmgReady && playerscript.attackcounter == 3)
		{
			dmgCD += Time.deltaTime;
			//working one is .35
		if (dmgCD >=0.35f)
			{
				dmgReady = true;
				dmgCD = 0;
			}
		}
		
		//sstrike cooldowns
		if (sstrikeavailable && !sstrikeready)
		{
			sstrikeCD += Time.deltaTime;
			if (sstrikeCD >= 1f)
			{
				sstrikeready = true;	
				sstrikeCD = 0;
			}
			
		}
		
		if (sstrikeavailable && !hadoready)
		{
			hadoCD += Time.deltaTime;
			if (hadoCD >= 0.2f)
			{
				hadoready = true;	
				hadoCD = 0;
			}
			
		}
		
		//sstrike startup and cooldowns
		if (Input.GetButtonDown("fight3") && PlayerPrefs.GetString("Sstrikerune") != "blue" && playerscript.balls >= 0.5f)
		{
			sstrikestartup = true;
		}
		
		
		
		if (sstrikestartup)
		{
			startuptimer += Time.deltaTime;
			if (startuptimer >= 0.32f)
			{
				sstrikestartup = false;	
				startuptimer = 0;
			}
		}
		
		//displacement for black screen effect
		if (displaced)
		{	
			displacementtimer += Time.deltaTime;
			if (displacementtimer >= 1f)
			{
					
				displaced = false;
				displacementtimer = 0;
			}
		}
		
		//shooting hado
		if (Input.GetButtonDown("fight3") && PlayerPrefs.GetString("Sstrikerune") == "blue")
		{
			if (sstrikeavailable && PlayerPrefs.GetString("Sstrikerune") == "blue" && hadoready && playerscript.balls >= 0.5f)
			{
				if (playerscript.MousetotheRight())
				Instantiate(hado, new Vector3(
							transform.position.x + 10, transform.position.y + 10, transform.position.z), transform.rotation);
				if (!playerscript.MousetotheRight())
				Instantiate(hado, new Vector3(
							transform.position.x - 10, transform.position.y + 10, transform.position.z), transform.rotation);
				playerscript.dbzmode = true;
				hadoready = false;
			}
		}
			
		
		if (playerscript.currenttarget != null)
		{
			targettingdelay += Time.deltaTime;	
		}
		

	
	}//end | Update()
	
	
	void OnTriggerStay (Collider otherObject)
	{
		
		#region regular attack hit collisions-------------------------------------------------
		if (buttonpressed && playerscript.state != Player.State.Explosion && !playerscript.stunned && !playerscript.gettinghit)
		{

			if (dmgReady && !playerscript.blocking && !playerscript.hurricane && !playerscript.groundsmash && 
				!playerscript.rightkick && !playerscript.leftkick && !playerscript.junklin && !playerscript.dashing && !obox.heavyon)
			{
				//Punk 1
				if (otherObject.tag == "Enemy")
				{
					Punk1 enemyscript = (Punk1)otherObject.gameObject.GetComponent("Punk1");
					
						//if enemy is already in KD state, make them slippery
						if (enemyscript.getknocked)
						{
							if (transform.position.x > otherObject.transform.position.x)
							enemyscript.CancelDBZ(new Vector3(-50, 555, otherObject.rigidbody.velocity.z));
							if (transform.position.x <= otherObject.transform.position.x)
							enemyscript.CancelDBZ(new Vector3(50, 555, otherObject.rigidbody.velocity.z));
						}
						
						//after 4 hit combo make them slippery
						if (enemyscript.comboedcount == 4 && !enemyscript.getknocked)
						{
							if (transform.position.x > otherObject.transform.position.x)
							enemyscript.CancelDBZ(new Vector3(-400, 444, otherObject.rigidbody.velocity.z));
							if (transform.position.x <= otherObject.transform.position.x)
							enemyscript.CancelDBZ(new Vector3(400, 444, otherObject.rigidbody.velocity.z));
							enemyscript.getknocked = true;
						}
						
						enemyscript.GetHit();
						
						audio.PlayOneShot(smacksound);
						if (enemyscript.health <= playerscript.attackdmg && enemyscript.sametarget <= 4)
						{
							obox.Makelines();
							audio.PlayOneShot(finalsmack);
						} 
						//full damage
						if (enemyscript.sametarget < 4)
						{
							enemyscript.Takedamage(playerscript.attackdmg);
							makesmack(otherObject.gameObject);
						}
						
						//player effects
						if (playerscript.dbzmode)
						playerscript.dbzmodetimer = 0;
						if (!enemyscript.getknocked && otherObject.transform.position.y > -300)
						playerscript.dbzmode = true;
						playerscript.hits++;
						playerscript.balls += 0.1f;
						tacklerefresh();
						playerscript.linkwindow = true;	
						playerscript.linktimer = 0f;
						dmgReady = false;
					
					
					obox.comboing = true;
					if (obox.comboing && enemyscript.sametarget <= 4)
					{
						obox.combocounter++;
						obox.combotimer = 0f;
						if (GameObject.FindGameObjectWithTag("Tutorial") != null && PlayerPrefs.GetInt("Currentlevel") == 1)
						{
							tutscript.LMB++;
							playerscript.numberforlinks = 1;
						}
					}
					buttonpressed = false;
					
				}//end | tag: enemy
				
				//pthrower
				if (otherObject.tag == "Enemythrower")
				{
					Punkthrower enemyscript = (Punkthrower)otherObject.gameObject.GetComponent("Punkthrower");

						if (!enemyscript.getknocked)
						{
							if (enemyscript.dbzmode)
								enemyscript.dbzmodetimer = 0;
							enemyscript.dbzmode = true;
							enemyscript.comboedcount++;
							enemyscript.comboedreset = 0;
						}
						
						if (enemyscript.comboedcount == 4)
						{
							if (transform.position.x > otherObject.transform.position.x)
							enemyscript.CancelDBZ(new Vector3(-400, 444, otherObject.rigidbody.velocity.z));
							if (transform.position.x <= otherObject.transform.position.x)
							enemyscript.CancelDBZ(new Vector3(400, 444, otherObject.rigidbody.velocity.z));
						}
						
						if (playerscript.dbzmode)
						playerscript.dbzmodetimer = 0;
						if (!enemyscript.getknocked && otherObject.transform.position.y > -300)
						playerscript.dbzmode = true;
						if (enemyscript.sametarget > 0)
						enemyscript.targetreset = 0;
						enemyscript.sametarget++;
						enemyscript.Bleedcash();
						playerscript.hits++;
						playerscript.balls += 0.1f;
						tacklerefresh();
						
						audio.PlayOneShot(smacksound);
						if (enemyscript.health <= playerscript.attackdmg && enemyscript.sametarget <= 4)
						{
							obox.Makelines();
							audio.PlayOneShot(finalsmack);
						} 
						if (enemyscript.sametarget >= 4 && enemyscript.sametarget <= 6)
						enemyscript.Takedamage(playerscript.attackdmg / 2);
						if (enemyscript.sametarget < 4)
						{
							enemyscript.Takedamage(playerscript.attackdmg);
							makesmack(otherObject.gameObject);
						}
						playerscript.linkwindow = true;	
						playerscript.linktimer = 0f;
						dmgReady = false;
					
					
					obox.comboing = true;
					if (obox.comboing && enemyscript.sametarget <= 4)
					{
						obox.combocounter++;
						obox.combotimer = 0f;
					}
					buttonpressed = false;
					
				}//end | tag: enemy
				
				//punk2
				if (otherObject.tag == "Enemy2")
				{
					Punk2 enemyscript = (Punk2)otherObject.gameObject.GetComponent("Punk2");
					
						if (!enemyscript.getknocked)
						{
							if (enemyscript.dbzmode)
								enemyscript.dbzmodetimer = 0;
							enemyscript.dbzmode = true;
							enemyscript.comboedcount++;
							enemyscript.comboedreset = 0;
						}
						if (enemyscript.getknocked)
						{
							if (transform.position.x > otherObject.transform.position.x)
							enemyscript.CancelDBZ(new Vector3(-50, 555, otherObject.rigidbody.velocity.z));
							if (transform.position.x <= otherObject.transform.position.x)
							enemyscript.CancelDBZ(new Vector3(50, 555, otherObject.rigidbody.velocity.z));
						}
						if (enemyscript.comboedcount == 6 && !enemyscript.getknocked)
						{
							if (transform.position.x > otherObject.transform.position.x)
							enemyscript.CancelDBZ(new Vector3(-400, 444, otherObject.rigidbody.velocity.z));
							if (transform.position.x <= otherObject.transform.position.x)
							enemyscript.CancelDBZ(new Vector3(400, 444, otherObject.rigidbody.velocity.z));
							enemyscript.getknocked = true;
						}
						if (playerscript.dbzmode)
						playerscript.dbzmodetimer = 0;
						if (!enemyscript.getknocked && otherObject.transform.position.y > -300)
						playerscript.dbzmode = true;
						if (enemyscript.sametarget > 0)
						enemyscript.targetreset = 0;
						enemyscript.sametarget++;
						enemyscript.Bleedcash();
						playerscript.hits++;
						playerscript.balls += 0.1f;
						tacklerefresh();
						audio.PlayOneShot(smacksound);
						if (enemyscript.health <= playerscript.attackdmg && enemyscript.sametarget <= 4)
						{
							obox.Makelines();
							audio.PlayOneShot(finalsmack);
						}
						if (enemyscript.sametarget >= 4 && enemyscript.sametarget <= 6)
						enemyscript.Takedamage(playerscript.attackdmg / 2);
						if (enemyscript.sametarget < 4)
						{
							enemyscript.Takedamage(playerscript.attackdmg);
							makesmack(otherObject.gameObject);
						}
						playerscript.linkwindow = true;	
						playerscript.linktimer = 0f;
						dmgReady = false;
					
					obox.comboing = true;
					if (obox.comboing && enemyscript.sametarget <= 4)
					{
						obox.combocounter++;
						obox.combotimer = 0f;
					}
					buttonpressed = false;
				}//end | tag: enemy2
				
				//punk3
				if (otherObject.tag == "Enemy3")
				{
					Punk3 enemyscript = (Punk3)otherObject.gameObject.GetComponent("Punk3");

						if (!enemyscript.getknocked)
						{
							if (enemyscript.dbzmode)
								enemyscript.dbzmodetimer = 0;
							enemyscript.dbzmode = true;
							enemyscript.comboedcount++;
							enemyscript.comboedreset = 0;
						}
						if (enemyscript.getknocked)
						{
							if (transform.position.x > otherObject.transform.position.x)
							enemyscript.CancelDBZ(new Vector3(-50, 555, otherObject.rigidbody.velocity.z));
							if (transform.position.x <= otherObject.transform.position.x)
							enemyscript.CancelDBZ(new Vector3(50, 555, otherObject.rigidbody.velocity.z));
						}
						if (enemyscript.comboedcount == 8 && !enemyscript.getknocked)
						{
							if (transform.position.x > otherObject.transform.position.x)
							enemyscript.CancelDBZ(new Vector3(-400, 444, otherObject.rigidbody.velocity.z));
							if (transform.position.x <= otherObject.transform.position.x)
							enemyscript.CancelDBZ(new Vector3(400, 444, otherObject.rigidbody.velocity.z));
							enemyscript.getknocked = true;
						}
						if (playerscript.dbzmode)
						playerscript.dbzmodetimer = 0;
						if (!enemyscript.getknocked && otherObject.transform.position.y > -300)
						playerscript.dbzmode = true;
						playerscript.hits++;
						playerscript.balls += 0.1f;
						tacklerefresh();
						playerscript.linkwindow = true;	
						playerscript.linktimer = 0f;
						if (enemyscript.sametarget > 0)
						enemyscript.targetreset = 0;
						enemyscript.sametarget++;
						enemyscript.Bleedcash();
						audio.PlayOneShot(smacksound);
						if (enemyscript.health <= playerscript.attackdmg && enemyscript.sametarget <= 4)
						{
							obox.Makelines();
							audio.PlayOneShot(finalsmack);
						}
						if (enemyscript.sametarget >= 4 && enemyscript.sametarget <= 6)
						enemyscript.Takedamage(playerscript.attackdmg / 2);
						if (enemyscript.sametarget < 4)
						{
							enemyscript.Takedamage(playerscript.attackdmg);
							makesmack(otherObject.gameObject);
						}
						
						dmgReady = false;
					
					obox.comboing = true;
					if (obox.comboing && enemyscript.sametarget <= 4)
					{
						obox.combocounter++;
						obox.combotimer = 0f;
					}
					buttonpressed = false;
				}//end | tag: enemy3
				
				//wrestler1
				if (otherObject.tag == "Hardcore")
				{
					EnemyWrestler enemyscript = (EnemyWrestler)otherObject.gameObject.GetComponent("EnemyWrestler");
					if (enemyscript.dbzmode || enemyscript.otg || enemyscript.stunned || enemyscript.balloon)
					{
						if (playerscript.dbzmode)
							playerscript.dbzmodetimer = 0;
						playerscript.dbzmode = true;
						if (enemyscript.dbzmode)
							enemyscript.dbzmodetimer = 0;
						Instantiate(smackanimu, 
						new Vector3(otherObject.transform.position.x, otherObject.transform.position.y, -120), otherObject.transform.rotation);
						audio.PlayOneShot(smacksound);
						enemyscript.shaking();
						playerscript.hits++;
						playerscript.balls += 0.1f;
						tacklerefresh();
						enemyscript.comboedcount++;
						enemyscript.comboedreset = 0;
						enemyscript.Bleedcash();
						if (enemyscript.health <= playerscript.attackdmg)
						{
							obox.Makelines();
							audio.PlayOneShot(finalsmack);
						}
						enemyscript.TakeDamage(playerscript.attackdmg);
						playerscript.linkwindow = true;	
						playerscript.linktimer = 0f;
						dmgReady = false;
					
						obox.comboing = true;
						if (obox.comboing)
						{
							obox.combocounter++;
							obox.combotimer = 0f;
						}
					}
					buttonpressed = false;
					
				}//end | tag: Wrestler1
				
				//w2
				if (otherObject.tag == "Hardcore2")
				{
					EnemyWrestler2 enemyscript = (EnemyWrestler2)otherObject.gameObject.GetComponent("EnemyWrestler2");
					if (enemyscript.dbzmode)
					{
						if (playerscript.dbzmode)
							playerscript.dbzmodetimer = 0;
						playerscript.dbzmode = true;
						if (enemyscript.dbzmode)
							enemyscript.dbzmodetimer = 0;
						Instantiate(smackanimu, 
						new Vector3(otherObject.transform.position.x, otherObject.transform.position.y, -120), otherObject.transform.rotation);
						audio.PlayOneShot(smacksound);
						enemyscript.shaking();
						playerscript.hits++;
						playerscript.balls += 0.1f;
						tacklerefresh();
						enemyscript.TakeDamage(playerscript.attackdmg);
						playerscript.linkwindow = true;	
						playerscript.linktimer = 0f;
						dmgReady = false;
					
						obox.comboing = true;
						if (obox.comboing)
						{
							obox.combocounter++;
							obox.combotimer = 0f;
						}
					}
					buttonpressed = false;
				}//end | tag: Wrestler2
				
				//w3
				if (otherObject.tag == "Hardcore3")
				{
					EnemyWrestler3 enemyscript = (EnemyWrestler3)otherObject.gameObject.GetComponent("EnemyWrestler3");
					if (enemyscript.dbzmode)
					{
						if (playerscript.dbzmode)
							playerscript.dbzmodetimer = 0;
						playerscript.dbzmode = true;
						if (enemyscript.dbzmode)
							enemyscript.dbzmodetimer = 0;
						Instantiate(smackanimu, 
						new Vector3(otherObject.transform.position.x, otherObject.transform.position.y, -120), otherObject.transform.rotation);
						audio.PlayOneShot(smacksound);
						enemyscript.shaking();
						playerscript.hits++;
						playerscript.balls += 0.1f;
						tacklerefresh();
						enemyscript.TakeDamage(playerscript.attackdmg);
						playerscript.linkwindow = true;	
						playerscript.linktimer = 0f;
						dmgReady = false;
					
						obox.comboing = true;
						if (obox.comboing)
						{
							obox.combocounter++;
							obox.combotimer = 0f;
						}
					}
					buttonpressed = false;
				}//end | tag: Wrestler3
				
				//ground1
				if (otherObject.tag == "Ground")
				{
					EnemyGround enemyscript = (EnemyGround)otherObject.gameObject.GetComponent("EnemyGround");
					
//						if (playerscript.dbzmode)
//						playerscript.dbzmodetimer = 0;
//						playerscript.dbzmode = true;
						playerscript.hits++;
						playerscript.balls += 0.1f;
						tacklerefresh();
						audio.PlayOneShot(smacksound);
//						if (enemyscript.health <= playerscript.attackdmg && enemyscript.sametarget <= 4)
//						{
//							obox.Makelines();
//							audio.PlayOneShot(finalsmack);
//						}
						enemyscript.health -= playerscript.attackdmg;
						makesmack(otherObject.gameObject);
						playerscript.linkwindow = true;	
						playerscript.linktimer = 0f;
						dmgReady = false;
					
					obox.comboing = true;
					if (obox.comboing)
					{
						obox.combocounter++;
						obox.combotimer = 0f;
					}
					buttonpressed = false;
				}//end | tag: groundshit
				
				//g2 and g3
				if (otherObject.tag == "Ground2" || otherObject.tag == "Ground3")
				{
					EnemyGround3 enemyscript = (EnemyGround3)otherObject.gameObject.GetComponent("EnemyGround3");
					
//						if (playerscript.dbzmode)
//						playerscript.dbzmodetimer = 0;
//						playerscript.dbzmode = true;
						playerscript.hits++;
						playerscript.balls += 0.1f;
						tacklerefresh();
						audio.PlayOneShot(smacksound);
//						if (enemyscript.health <= playerscript.attackdmg && enemyscript.sametarget <= 4)
//						{
//							obox.Makelines();
//							audio.PlayOneShot(finalsmack);
//						}
						enemyscript.health -= playerscript.attackdmg;
						makesmack(otherObject.gameObject);
						playerscript.linkwindow = true;	
						playerscript.linktimer = 0f;
						dmgReady = false;
					
					obox.comboing = true;
					if (obox.comboing)
					{
						obox.combocounter++;
						obox.combotimer = 0f;
					}
					buttonpressed = false;
				}//end | tag: groundshit
				
//				//ninja
//				if (otherObject.tag == "Ninja1")
//				{
//					Ninja1 enemyscript = (Ninja1)otherObject.gameObject.GetComponent("Ninja1");
//
//					playerscript.dbzmode = true;
//					enemyscript.dbzmode = true;
//					Instantiate(smackanimu, 
//					new Vector3(otherObject.transform.position.x, otherObject.transform.position.y, -120), otherObject.transform.rotation);
//					audio.PlayOneShot(smacksound);
//					playerscript.hits++;
//					playerscript.balls += 0.1f;
//					enemyscript.TakeDamage(playerscript.attackdmg);
//					playerscript.linkwindow = true;	
//					playerscript.linktimer = 0f;
//					dmgReady = false;
//					
//					obox.comboing = true;
//					if (obox.comboing)
//					{
//						obox.combocounter++;
//						obox.combotimer = 0f;
//					}
//					buttonpressed = false;
//				}//end | tag: ninja1
				
				//ninja
				if (otherObject.tag == "Ninja2")
				{
					Ninja2 enemyscript = (Ninja2)otherObject.gameObject.GetComponent("Ninja2");

					playerscript.dbzmode = true;
					enemyscript.dbzmode = true;
					Instantiate(smackanimu, 
					new Vector3(otherObject.transform.position.x, otherObject.transform.position.y, -120), otherObject.transform.rotation);
					audio.PlayOneShot(smacksound);
					playerscript.hits++;
					playerscript.balls += 0.1f;
					tacklerefresh();
					enemyscript.TakeDamage(playerscript.attackdmg);
					playerscript.linkwindow = true;	
					playerscript.linktimer = 0f;
					dmgReady = false;
					
					obox.comboing = true;
					if (obox.comboing)
					{
						obox.combocounter++;
						obox.combotimer = 0f;
					}
					buttonpressed = false;
				}//end | tag: ninja2
				
				//zoomzoom
				if (otherObject.tag == "Bomb")
				{
					zoomzoom zzscript = (zoomzoom)otherObject.gameObject.GetComponent("zoomzoom");
					playerscript.dbzmode = true;
					Instantiate(smackanimu, 
					new Vector3(otherObject.transform.position.x, otherObject.transform.position.y, -120), otherObject.transform.rotation);
					audio.PlayOneShot(smacksound);
					playerscript.hits++;
					playerscript.balls += 0.1f;
					tacklerefresh();
					if (zzscript.isActive)
						zzscript.health--;
					zzscript.flyaway();
					playerscript.linkwindow = true;	
					playerscript.linktimer = 0f;
					dmgReady = false;
				
					obox.comboing = true;
					if (obox.comboing)
					{
						obox.combocounter++;
						obox.combotimer = 0f;
					}
					
					buttonpressed = false;
				}
				
				//ROBOT boss
				if (otherObject.tag == "Boss")
				{
					Boss1 boss1script = (Boss1)otherObject.gameObject.GetComponent("Boss1");
						if (playerscript.dbzmode)
							playerscript.dbzmodetimer = 0;
						playerscript.dbzmode = true;
						Instantiate(smackanimu, 
						new Vector3(otherObject.transform.position.x, otherObject.transform.position.y, -120), otherObject.transform.rotation);
						audio.PlayOneShot(smacksound);
						playerscript.hits++;
						playerscript.balls += 0.1f;
						tacklerefresh();
						boss1script.GetHit(playerscript.attackdmg);
						playerscript.linkwindow = true;	
						playerscript.linktimer = 0f;
						dmgReady = false;
					
						obox.comboing = true;
						if (obox.comboing)
						{
							obox.combocounter++;
							obox.combotimer = 0f;
						}
					
					buttonpressed = false;
				}
				
				//ROBOT boss minions
				if (otherObject.tag == "Minion")
				{
					Boss1 boss1script = (Boss1)otherObject.transform.parent.gameObject.GetComponent("Boss1");
					if (playerscript.dbzmode)
							playerscript.dbzmodetimer = 0;
					playerscript.dbzmode = true;
					Instantiate(smackanimu, 
					new Vector3(otherObject.transform.position.x, otherObject.transform.position.y, -120), otherObject.transform.rotation);
					audio.PlayOneShot(smacksound);
					playerscript.hits++;
					playerscript.balls += 0.1f;
					tacklerefresh();
					boss1script.m1GetHit(playerscript.attackdmg);
					playerscript.linkwindow = true;	
					playerscript.linktimer = 0f;
					dmgReady = false;
				
					obox.comboing = true;
					if (obox.comboing)
					{
						obox.combocounter++;
						obox.combotimer = 0f;
					}				
					buttonpressed = false;
				}// end | tag: minion
				
				if (otherObject.tag == "Minion2")
				{
					Boss1 boss1script = (Boss1)otherObject.transform.parent.gameObject.GetComponent("Boss1");
					if (playerscript.dbzmode)
							playerscript.dbzmodetimer = 0;
					playerscript.dbzmode = true;
					Instantiate(smackanimu, 
					new Vector3(otherObject.transform.position.x, otherObject.transform.position.y, -120), otherObject.transform.rotation);
					audio.PlayOneShot(smacksound);
					playerscript.hits++;
					playerscript.balls += 0.1f;
					tacklerefresh();
					boss1script.m2GetHit(playerscript.attackdmg);
					playerscript.linkwindow = true;	
					playerscript.linktimer = 0f;
					dmgReady = false;
				
					obox.comboing = true;
					if (obox.comboing)
					{
						obox.combocounter++;
						obox.combotimer = 0f;
					}				
					buttonpressed = false;
				}// end | tag: minion
				
				//apple checkpoint
				if (otherObject.tag == "Checkpoint")
				{
					Checkpointthing enemyscript = (Checkpointthing)otherObject.gameObject.GetComponent("Checkpointthing");
					if (playerscript.dbzmode)
							playerscript.dbzmodetimer = 0;
					playerscript.dbzmode = true;
					Instantiate(smackanimu, 
					new Vector3(otherObject.transform.position.x, otherObject.transform.position.y, -120), otherObject.transform.rotation);
					audio.PlayOneShot(smacksound);
					playerscript.hits++;
					playerscript.balls += 0.1f;
					tacklerefresh();
					enemyscript.health -= playerscript.attackdmg;
					playerscript.linkwindow = true;	
					playerscript.linktimer = 0f;
					dmgReady = false;
				
					obox.comboing = true;
					if (obox.comboing)
					{
						obox.combocounter++;
						obox.combotimer = 0f;
					}				
					buttonpressed = false;
				}// end | tag: check
				
				//sbomb
				if (otherObject.tag == "Sbomb" && PlayerPrefs.GetString("SpiritbombT2") == "white" || 
					otherObject.tag == "Sbomb" && PlayerPrefs.GetString("SpiritbombT1") == "white")
				{
					SpiritBomb sbscript = (SpiritBomb)otherObject.gameObject.GetComponent("SpiritBomb");
					if (sbscript.state == SpiritBomb.State.Armed)
					sbscript.TriggerExplosion();
					if (playerscript.dbzmode)
							playerscript.dbzmodetimer = 0;
					playerscript.dbzmode = true;
					Instantiate(smackanimu, 
					new Vector3(otherObject.transform.position.x, otherObject.transform.position.y, -120), otherObject.transform.rotation);
					audio.PlayOneShot(smacksound);
					playerscript.hits++;
					tacklerefresh();
					playerscript.linkwindow = true;	
					playerscript.linktimer = 0f;
					dmgReady = false;
				
					obox.comboing = true;
					if (obox.comboing)
					{
						obox.combocounter++;
						obox.combotimer = 0f;
					}	
					buttonpressed = false;
					
				}// end | tag: Sbomb
				
				//atm
				if (otherObject.tag == "ATM")
				{
					atmachine atmscript = (atmachine)otherObject.gameObject.GetComponent("atmachine");
					if (playerscript.dbzmode)
							playerscript.dbzmodetimer = 0;
					playerscript.dbzmode = true;
					Instantiate(smackanimu, 
					new Vector3(otherObject.transform.position.x, otherObject.transform.position.y, -120), otherObject.transform.rotation);
					audio.PlayOneShot(smacksound);
					playerscript.hits++;
					tacklerefresh();
					otherObject.audio.Play();
					atmscript.shake();
					if (atmscript.health <= playerscript.attackdmg)
						obox.Makelines();
					atmscript.health -= playerscript.attackdmg;
					atmscript.ttg += 0.3f;
					atmscript.releasecoins();
					
					playerscript.linkwindow = true;	
					playerscript.linktimer = 0f;
					dmgReady = false;
				
					obox.comboing = true;
					if (obox.comboing)
					{
						obox.combocounter++;
						obox.combotimer = 0f;
					}				
					buttonpressed = false;
				}// end | tag: minion	
				
			}// end | dmgready
			
			//reset the pro method
			StartCoroutine ( Resetthebutton (0.2f) );
			
			
		}// end | lightattack
#endregion
		
		//SAPACIEL STRYKE BABY DIS ONE IS FO U
		if (sstrikeready && playerscript.state != Player.State.Explosion && !playerscript.stunned && !playerscript.gettinghit
			&& !playerscript.blocking && !playerscript.hurricane && !playerscript.groundsmash && !playerscript.rightkick && !playerscript.leftkick 
			&& !playerscript.junklin && !playerscript.dashing && !obox.heavyon)
		{
			if (startuptimer >= 0.3f)
			{
				//sstrike punk1
				if (otherObject.tag == "Enemy")
				{
					Punk1 enemyscript = (Punk1)otherObject.gameObject.GetComponent("Punk1");
					if (playerscript.dbzmode)
						playerscript.dbzmodetimer = 0;
						playerscript.dbzmode = true;
					if (enemyscript.dbzmode)
						enemyscript.dbzmodetimer = 0;
						enemyscript.dbzmode = true;
						enemyscript.displaced = true;
						Changezposition();
						audio.PlayOneShot(smacksound2);
						audio.PlayOneShot(linesound);
						playerscript.hits++;
						playerscript.balls += 0.25f;
						enemyscript.Takedamage(playerscript.attackdmg);
						playerscript.linkwindow = true;	
						playerscript.linktimer = 0f;
						string rune = QuerySstrike();
						if (rune == "yellow")
						{
							if (enemyscript.bombed)
							enemyscript.Takedamage(9999);
							enemyscript.bombed = true;
							Instantiate(smackred, 
							new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -210), otherObject.transform.rotation);
						}
						else if (rune == "green")
						{
							enemyscript.poisoned = true;
							Instantiate(smackgreen, 
							new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -210), otherObject.transform.rotation);
						}
						else if (rune == "red")
						{
							playerscript.Healthup(playerscript.attackdmg);
							audio.PlayOneShot(heal);
							Instantiate(smackred, 
							new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -210), otherObject.transform.rotation);
						}
						else
						Instantiate(smackanimu, 
						new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -210), otherObject.transform.rotation);
						sstrikeready = false;
					
					
					obox.comboing = true;
					if (obox.comboing)
					{
						obox.combocounter++;
						obox.combotimer = 0f;
					}
					
				}//end punk1
				
				//sstrike punkthrower
				if (otherObject.tag == "Enemythrower")
				{
					Punkthrower enemyscript = (Punkthrower)otherObject.gameObject.GetComponent("Punkthrower");
					if (playerscript.dbzmode)
						playerscript.dbzmodetimer = 0;
						playerscript.dbzmode = true;
					if (enemyscript.dbzmode)
						enemyscript.dbzmodetimer = 0;
						enemyscript.dbzmode = true;
						enemyscript.displaced = true;
						Changezposition();
						audio.PlayOneShot(smacksound2);
						audio.PlayOneShot(linesound);
						playerscript.hits++;
						playerscript.balls += 0.25f;
						enemyscript.Takedamage(playerscript.attackdmg);
						playerscript.linkwindow = true;	
						playerscript.linktimer = 0f;
						string rune = QuerySstrike();
						if (rune == "yellow")
						{
							if (enemyscript.bombed)
							enemyscript.Takedamage(9999);
							enemyscript.bombed = true;
							Instantiate(smackred, 
							new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -210), otherObject.transform.rotation);
						}
						else if (rune == "green")
						{
							enemyscript.poisoned = true;
							Instantiate(smackgreen, 
							new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -210), otherObject.transform.rotation);
						}
						else if (rune == "red")
						{
							playerscript.Healthup(playerscript.attackdmg);
							audio.PlayOneShot(heal);
							Instantiate(smackred, 
							new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -210), otherObject.transform.rotation);
						}
						else
						Instantiate(smackanimu, 
						new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -210), otherObject.transform.rotation);
						sstrikeready = false;
					
					
					obox.comboing = true;
					if (obox.comboing)
					{
						obox.combocounter++;
						obox.combotimer = 0f;
					}
					
				}//end punk1
				
				//sstrike p2
				if (otherObject.tag == "Enemy2")
				{
					Punk2 enemyscript = (Punk2)otherObject.gameObject.GetComponent("Punk2");
					if (playerscript.dbzmode)
						playerscript.dbzmodetimer = 0;
						playerscript.dbzmode = true;
					if (enemyscript.dbzmode)
						enemyscript.dbzmodetimer = 0;
						enemyscript.dbzmode = true;
						audio.PlayOneShot(smacksound2);
						audio.PlayOneShot(linesound);
						playerscript.hits++;
						playerscript.balls += 0.25f;
						enemyscript.Takedamage(playerscript.attackdmg);
						playerscript.linkwindow = true;	
						playerscript.linktimer = 0f;
						string rune = QuerySstrike();
						if (rune == "yellow")
						{
							enemyscript.bombed = true;
							Instantiate(smackred, 
							new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
						}
						else if (rune == "green")
						{
							enemyscript.poisoned = true;
							Instantiate(smackgreen, 
							new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
						}
						else if (rune == "red")
						{
							playerscript.Healthup(playerscript.attackdmg);
							audio.PlayOneShot(heal);
							Instantiate(smackred, 
							new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
						}
						else
						Instantiate(smackanimu, 
						new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
						sstrikeready = false;
					
					
					obox.comboing = true;
					if (obox.comboing)
					{
						obox.combocounter++;
						obox.combotimer = 0f;
					}
					
				}//end punk2
				
				//sstrike p3
				if (otherObject.tag == "Enemy3")
				{
					Punk3 enemyscript = (Punk3)otherObject.gameObject.GetComponent("Punk3");
					if (playerscript.dbzmode)
						playerscript.dbzmodetimer = 0;
						playerscript.dbzmode = true;
					if (enemyscript.dbzmode)
						enemyscript.dbzmodetimer = 0;
						enemyscript.dbzmode = true;
						audio.PlayOneShot(smacksound2);
						audio.PlayOneShot(linesound);
						playerscript.hits++;
						playerscript.balls += 0.25f;
						enemyscript.Takedamage(playerscript.attackdmg);
						playerscript.linkwindow = true;	
						playerscript.linktimer = 0f;
						string rune = QuerySstrike();
						if (rune == "yellow")
						{
							enemyscript.bombed = true;
							Instantiate(smackred, 
							new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
						}
						else if (rune == "green")
						{
							enemyscript.poisoned = true;
							Instantiate(smackgreen, 
							new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
						}
						else if (rune == "red")
						{
							playerscript.Healthup(playerscript.attackdmg);
							audio.PlayOneShot(heal);
							Instantiate(smackred, 
							new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
						}
						else
						Instantiate(smackanimu, 
						new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
						sstrikeready = false;
					
					
					obox.comboing = true;
					if (obox.comboing)
					{
						obox.combocounter++;
						obox.combotimer = 0f;
					}
					
				}//end punk3
				
				//sstrike ninja
				if (otherObject.tag == "Ninja2")
				{
					Ninja2 enemyscript = (Ninja2)otherObject.gameObject.GetComponent("Ninja2");
					if (playerscript.dbzmode)
						playerscript.dbzmodetimer = 0;
						playerscript.dbzmode = true;
						enemyscript.dbzmode = true;
						audio.PlayOneShot(smacksound2);
						audio.PlayOneShot(linesound);
						playerscript.hits++;
						playerscript.balls += 0.25f;
						enemyscript.Takedamage(playerscript.attackdmg);
						playerscript.linkwindow = true;	
						playerscript.linktimer = 0f;
						string rune = QuerySstrike();
						if (rune == "yellow")
						{
							enemyscript.bombed = true;
							Instantiate(smackred, 
							new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
						}
						else if (rune == "green")
						{
							enemyscript.poisoned = true;
							Instantiate(smackgreen, 
							new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
						}
						else if (rune == "red")
						{
							playerscript.Healthup(playerscript.attackdmg);
							audio.PlayOneShot(heal);
							Instantiate(smackred, 
							new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
						}
						else
						Instantiate(smackanimu, 
						new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
						sstrikeready = false;
					
					
					obox.comboing = true;
					if (obox.comboing)
					{
						obox.combocounter++;
						obox.combotimer = 0f;
					}
					
				}//end Ninja
				
				//sstrike wrestler
				if (otherObject.tag == "Hardcore")
				{
					EnemyWrestler enemyscript = (EnemyWrestler)otherObject.gameObject.GetComponent("EnemyWrestler");
					if (playerscript.dbzmode)
						playerscript.dbzmodetimer = 0;
						playerscript.dbzmode = true;
						if (enemyscript.dbzmode)
							enemyscript.dbzmodetimer = 0;
						enemyscript.dbzmode = true;
						audio.PlayOneShot(smacksound2);
						audio.PlayOneShot(linesound);
						playerscript.hits++;
						playerscript.balls += 0.25f;
						enemyscript.TakeDamage(playerscript.attackdmg);
						playerscript.linkwindow = true;	
						playerscript.linktimer = 0f;
						string rune = QuerySstrike();
						if (rune == "yellow")
						{
							enemyscript.bombed = true;
							Instantiate(smackred, 
							new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
						}
						else if (rune == "green")
						{
							enemyscript.poisoned = true;
							Instantiate(smackgreen, 
							new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
						}
						else if (rune == "red")
						{
							playerscript.Healthup(playerscript.attackdmg);
							audio.PlayOneShot(heal);
							Instantiate(smackred, 
							new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
						}
						else
						Instantiate(smackanimu, 
						new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
						sstrikeready = false;
					
					
					obox.comboing = true;
					if (obox.comboing)
					{
						obox.combocounter++;
						obox.combotimer = 0f;
					}
					
				}//end W1
				
				//sstrike w2
				if (otherObject.tag == "Hardcore2")
				{
					EnemyWrestler2 enemyscript = (EnemyWrestler2)otherObject.gameObject.GetComponent("EnemyWrestler2");
					if (playerscript.dbzmode)
						playerscript.dbzmodetimer = 0;
						playerscript.dbzmode = true;
						if (enemyscript.dbzmode)
							enemyscript.dbzmodetimer = 0;
						enemyscript.dbzmode = true;
						audio.PlayOneShot(smacksound2);
						audio.PlayOneShot(linesound);
						playerscript.hits++;
						playerscript.balls += 0.25f;
						enemyscript.TakeDamage(playerscript.attackdmg);
						playerscript.linkwindow = true;	
						playerscript.linktimer = 0f;
						string rune = QuerySstrike();
						if (rune == "yellow")
						{
							enemyscript.bombed = true;
							Instantiate(smackred, 
							new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
						}
						else if (rune == "green")
						{
							enemyscript.poisoned = true;
							Instantiate(smackgreen, 
							new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
						}
						else if (rune == "red")
						{
							playerscript.Healthup(playerscript.attackdmg);
							audio.PlayOneShot(heal);
							Instantiate(smackred, 
							new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
						}
						else
						Instantiate(smackanimu, 
						new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
						sstrikeready = false;
					
					
					obox.comboing = true;
					if (obox.comboing)
					{
						obox.combocounter++;
						obox.combotimer = 0f;
					}
					
				}//end W2
				
				//sstrike w3
				if (otherObject.tag == "Hardcore3")
				{
					EnemyWrestler3 enemyscript = (EnemyWrestler3)otherObject.gameObject.GetComponent("EnemyWrestler3");
					if (playerscript.dbzmode)
						playerscript.dbzmodetimer = 0;
						playerscript.dbzmode = true;
						if (enemyscript.dbzmode)
							enemyscript.dbzmodetimer = 0;
						enemyscript.dbzmode = true;
						audio.PlayOneShot(smacksound2);
						audio.PlayOneShot(linesound);
						playerscript.hits++;
						playerscript.balls += 0.25f;
						enemyscript.TakeDamage(playerscript.attackdmg);
						playerscript.linkwindow = true;	
						playerscript.linktimer = 0f;
						string rune = QuerySstrike();
						if (rune == "yellow")
						{
							enemyscript.bombed = true;
							Instantiate(smackred, 
							new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
						}
						else if (rune == "green")
						{
							enemyscript.poisoned = true;
							Instantiate(smackgreen, 
							new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
						}
						else if (rune == "red")
						{
							playerscript.Healthup(playerscript.attackdmg);
							audio.PlayOneShot(heal);
							Instantiate(smackred, 
							new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
						}
						else
						Instantiate(smackanimu, 
						new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
						sstrikeready = false;
					
					
					obox.comboing = true;
					if (obox.comboing)
					{
						obox.combocounter++;
						obox.combotimer = 0f;
					}
					
				}//end W3
				
				//sstrike zz
				if (otherObject.tag == "Bomb")
				{
					zoomzoom enemyscript = (zoomzoom)otherObject.gameObject.GetComponent("zoomzoom");
					if (playerscript.dbzmode)
						playerscript.dbzmodetimer = 0;
						playerscript.dbzmode = true;
						enemyscript.flyaway();
						audio.PlayOneShot(smacksound2);
						audio.PlayOneShot(linesound);
						playerscript.hits++;
						playerscript.balls += 0.25f;
						enemyscript.Takedamage(1);
						playerscript.linkwindow = true;	
						playerscript.linktimer = 0f;
						string rune = QuerySstrike();
//						if (rune == "green")
//						{
//							enemyscript.poisoned = true;
//							Instantiate(smackgreen, 
//							new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -12), otherObject.transform.rotation);
//						}
						if (rune == "red")
						{
							playerscript.Healthup(playerscript.attackdmg);
							audio.PlayOneShot(heal);
							Instantiate(smackred, 
							new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
						}
						else
						Instantiate(smackanimu, 
						new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
						sstrikeready = false;
					
					
					obox.comboing = true;
					if (obox.comboing)
					{
						obox.combocounter++;
						obox.combotimer = 0f;
					}
					
				}//end W3
				
			}//end startuptimer
			
		}// end conditionals
		
		
	}//end Ontriggerstay
	
	IEnumerator Resetthebutton(float time)
	{	
		yield return new WaitForSeconds(time);
			
		buttonpressed = false;
		
	}
	
	//not used
	public void increaseatkcounter()
	{
		if (playerscript.attackcounter < 3 || playerscript.attackcounter == 0)
			playerscript.attackcounter++;
		if (playerscript.attackcounter >= 3)
			playerscript.attackcounter = 0;
		
	}
	
	//displacement function 
	void Changezposition()
	{
		displaced = true;
		karateman.transform.position = new Vector3(karateman.transform.position.x, karateman.transform.position.y, -200);
		
		Instantiate(blackscreen, new Vector3(0, 0, -120), Quaternion.Euler(new Vector3(270, 0, 0)));
		if (!playerscript.facingright)
		Instantiate(specialline, new Vector3(1280, karateman.transform.position.y, -150), Quaternion.identity);
		
		if (playerscript.facingright)
		{
			GameObject thisline = (GameObject)Instantiate(specialline, new Vector3(-1280, karateman.transform.position.y, -150), Quaternion.identity);
			movedirection linemove = (movedirection)thisline.GetComponent("movedirection");
			linemove.direction = "right";
			
		}
		
	}
	
	//tackle refresh
	void tacklerefresh()
	{
		if (PlayerPrefs.GetString("TackleT1") == "white" && !playerscript.tackleready)
			playerscript.tackleCD += 0.2f;	
			
	}
	
	//sstrike rune query
	string QuerySstrike()
	{
		return PlayerPrefs.GetString("Sstrikerune");
	}
	
	//smack animu
	void makesmack(GameObject otherObject)
	{
		float yoffset = Random.Range(-40, 41);
		float xoffset = 25;
		if (karateman.transform.position.x > otherObject.gameObject.transform.position.x)
			xoffset = -20;
		ParticleSystem thissmack = (ParticleSystem)Instantiate(smackanimu, 
		new Vector3(otherObject.transform.position.x + xoffset, otherObject.transform.position.y + yoffset, -120), otherObject.transform.rotation);
		
		//if karateman on right
		if (karateman.transform.position.x > otherObject.gameObject.transform.position.x)
			thissmack.startRotation = 60;
		
	}
	
//	void OnGUI()
//	{
//		GUI.Label(new Rect(0, 120, 200, 100), "DEV SHIT SONS");
//		GUI.TextArea(new Rect(0, 160, 120, 200), " ");
//				
//			GUI.Label(new Rect(0, 160, 200, 100), "Startup == " + sstrikestartup.ToString());
//			GUI.Label(new Rect(0, 180, 200, 100), "startimer: " + startuptimer.ToString());	
//		
//	}
	
	
}
