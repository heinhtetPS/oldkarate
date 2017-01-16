using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Punk3 : MonoBehaviour {
	
	public exSprite punksprite;
	public exSpriteAnimation punkanim;
	private float minRotatespeed = 120, maxRotatespeed = 340;
	private float currentRotate;
	public bool dbzmode = false, gravitypulled = false;
	public float dbzmodetimer = 0;
	private bool firsttime = true;
	public bool canhityou = true;
	public bool grabbed = false;
	public bool tutorial = false;
	public bool facingleft = true;
	public bool upsidedown = false;
	public bool followup = false, followupready = false, justdidamove = false;
	private float followupdisabletime, fcanceltimer;
	public bool nocontact = true;
	public bool slowed = false;
	private float slowtimer = 0;
	public bool attacking = false;
	public int comboedcount = 0;
	public float comboedreset = 0;
	private int highestcombo = 0;
	private int moneyinpocket = 100;
	
	pausemenu pausescript;
	
	private bool recoverplayed = false, KDplayed = false;
	public int sametarget = 0;
	public float targetreset = 0;
	
	public bool gsmashready = false, gotrandom = false;
	private float gsmashCD = 0, gsmashtimer = 0;
	private int gsmashwait, gsmashactive;
	
	//animutions
	private bool animating = false;
	 
	private float velocityX, velocityY;
	public float gravity, jumpbounce;
	
	public float health = 13, maxhealth = 13;
	
	//states
	private float scalereducer = 0;
	private bool frozen = false;
	public bool tornado = false;
	public bool falling = false;
	public bool otg = false;
	public float otgtimer;
	private float deadonfloor = 0f;
	private float deadgroundoffset = 0;
	public bool stunned = false;
	public bool bombed = false;
	public bool poisoned = false;
	private int poisonticks = 0;
	private float bombtimer = 0, poisontimer = 0, wallbouncetimer = 0, tackledtimer = 0;
	public bool wallbounce = false, tackled = false;
	public bool getknocked = false;
	public float knockdowntimer;
	private Vector3 storedvelo;
	private bool justpaused = false;
	private float pausedtimer = 0;
	
	public float movetowardstimer = 0f;
	private int randomforrush;
	private bool gotrandomforrush = false;
	
	private float dmgtypereset = 0;
	public bool gotdmgbybomb = false, gotdmgbydkick = false, gotdmgbyforce = false, gotdmgbytackle = false, gotdmgbyclone = false;
	
	public Karateoboxnew obox;
	public GameObject Karateman, smackanimu;
	public Player playerscript;
	public Spawnerv2 spawnscript;
	public bool isTarget = false;
	private float targettime;
	
	public bool taunted = false, tauntable = true;
	public float taunttimer, tauntCD;
	
	public AudioClip death1, death2, death3, gsmash, woosh, hit, smacksound, otwsound;
	
	public AudioClip[] deathsounds;
	
	//itemdrop
	private int chancetodrop;
	private bool spawnonce = false;
	private int itemroll;
	public GameObject sushi, money, chiball, redscroll, greenscroll, bluescroll, explosion;
	public ParticleSystem debris;

	void Start () {
		
		SetCurrent();
		Karateman = GameObject.FindGameObjectWithTag("Player");
		obox = (Karateoboxnew)GameObject.FindGameObjectWithTag("Offense").GetComponent("Karateoboxnew");
		playerscript = (Player)GameObject.FindGameObjectWithTag("Player").GetComponent("Player");
//		spawnscript = (Spawnerv2)GameObject.FindGameObjectWithTag("Spawner").GetComponent("Spawnerv2");
		pausescript = (pausemenu)Camera.main.GetComponent("pausemenu");
		collider.enabled = false;
		
		if (GameObject.FindGameObjectWithTag("Tutorial") != null)
			tutorial = true;
		
		deathsounds = new AudioClip[] {death1, death2, death3};
	}
	
	//this is for things that apply physics. especially rigidbody.velocity
	void FixedUpdate ()
	{
		if (!pausescript.playerpause)
		{
			//gravity
			if (transform.position.y > -300 && !frozen && tag != "Dead" && !dbzmode && !getknocked)
					gameObject.rigidbody.velocity += new Vector3(0, gravity, 0);
			
			//pause velocity return
			if (justpaused)
			{
				rigidbody.velocity = storedvelo;	
				justpaused = false;
			}
			
			//bounce
			if (transform.position.y <= -300 && !frozen && !otg && !getknocked && tag == "Enemy3")
				gameObject.rigidbody.velocity = new Vector3(Random.Range (-204,204), jumpbounce, 0);
			
			//gsmash & bounce
				if (transform.position.y <= -300 && !frozen && falling && tag == "Enemy3")
				{
					falling = false;
					otg = true;
					if (upsidedown)
					{
						punksprite.VFlip();
						upsidedown = false;
					}
					punkanim.Play("g3axed");
					audio.PlayOneShot(gsmash);
					Instantiate(debris, transform.position, transform.rotation);
					gameObject.rigidbody.velocity = new Vector3(0, jumpbounce, 0);
				}
			 
			// regular dying 
			if (gameObject.tag == "Enemy3" && health <= 0 && !frozen && !tornado && !falling)
			{
				FlyAway();
			}
			
			//semi intelligent chase
			if (!gotrandomforrush)
				Getrushrandom();
			if (taunted)
			{
				GameObject Fakeman = GameObject.FindGameObjectWithTag("Fake");
				if (tag == "Enemy3" && !tornado && !getknocked && !tackled && !gravitypulled && !otg && movetowardstimer - Time.deltaTime >= randomforrush)
				{
					if (!slowed)
					rigidbody.velocity += Getdiff("no", Fakeman) * 3;
					if (slowed)
					rigidbody.velocity += Getdiff("no", Fakeman) * 0.8f;
					punkanim.Play("g3lunge");
					movetowardstimer = 0f;
					gotrandomforrush = false;
				}
			}
			if (!taunted)
			{
				if (tag == "Enemy3" && !tornado && !getknocked && !tackled && !gravitypulled && !otg && movetowardstimer - Time.deltaTime >= randomforrush)
				{
					if (!slowed)
					rigidbody.velocity += Getdiff() * 3;
					if (slowed)
					rigidbody.velocity += Getdiff() * 0.8f;
					punkanim.Play("g3lunge");
					movetowardstimer = 0f;
					gotrandomforrush = false;
				}
			}
			
			if (followup)
			{
				fcanceltimer += Time.deltaTime;
				punkanim.Play("g3lunge");	
				rigidbody.velocity = Getdiff() * 3;
				if (fcanceltimer >= 5)
				{
					followup = false;	
					justdidamove = true;
					fcanceltimer = 0;
				}
			}
			
		}//pause
		
	}
	
	void Update () {
		
		//activate
			if (firsttime)
				StartCoroutine( Startup () );
		
		if (!pausescript.playerpause)
		{
			//variables
			float rotationspeed = currentRotate * Time.deltaTime;
				
			//spinning
			if (transform.position.y > -300 && tag == "Dead")
			{
				animating = false;
				punkanim.Play("g3dead");
				transform.Rotate(new Vector3(0,0,-1) * rotationspeed);
			}
			
			if (GameObject.FindGameObjectWithTag("Fake") != null && tauntable)
				taunted = true;
			if (GameObject.FindGameObjectWithTag("Fake") == null)
			{
				taunted = false;
				taunttimer = 0;
				tauntCD = 0;
			}
			
			//general animutinos
			if (!animating)
				{
					//low going down (crab)
					if (transform.position.y < -150f && rigidbody.velocity.y < 0 && tag == "Enemy3")
						punkanim.Play("g3crab");
				}
				
				//restrictions & trash removal
				//side screen shit
				if (transform.position.x <= -624 && !tackled)
					{	
						if (gameObject.tag != "Dead")
						{
							transform.position = new Vector3(-623,
							transform.position.y, transform.position.z);
							gameObject.rigidbody.velocity += new Vector3 (170,0,0);
						}
					}
				if (transform.position.x >= 571 && !tackled)
					{	
						if (gameObject.tag != "Dead")
						{
							transform.position = new Vector3(570,
							transform.position.y, transform.position.z);
							gameObject.rigidbody.velocity += new Vector3 (-170,0,0);
						}
					
					}
					if (tag == "Enemy3")
					{
						if (transform.position.y > 400)
						gameObject.rigidbody.velocity += new Vector3 (0,-100,0);
					}
					if (transform.position.y <= -600 || transform.position.y >= 600)
					{
						if (tag == "Dead")
						Destroy(this.gameObject);
					}
			
				//color
				if (gravitypulled)
					punksprite.color = Color.black;
				else punksprite.color = Color.white;
				
				//fuck 3d
				if (transform.position.z != -100)
					transform.position = new Vector3(transform.position.x, transform.position.y, -100);
			
			
			
			
	#region different states------------------------------------------------------------------
			if (gameObject.tag == "Enemy3")
			{
				//look at afro always
				if (!falling & !frozen && !tornado && !getknocked && !tackled)
				{
					if (!taunted)
					{
						if (transform.position.x < Karateman.transform.position.x && facingleft)
						{
							punksprite.HFlip();
							facingleft = false;	
						}
					
						if (transform.position.x >= Karateman.transform.position.x && !facingleft)
						{
							punksprite.HFlip();
							facingleft = true;	
						}
					}
					
					//or aggro to the puppet
					if (taunted && GameObject.FindGameObjectWithTag("Fake") != null)
					{
						GameObject Fakeman = GameObject.FindGameObjectWithTag("Fake");
						if (transform.position.x < Fakeman.transform.position.x && facingleft)
						{
							punksprite.HFlip();
							facingleft = false;	
						}
					
						if (transform.position.x >= Fakeman.transform.position.x && !facingleft)
						{
							punksprite.HFlip();
							facingleft = true;	
						}
					}
				}
					
			}
			
				//DBZ mode
				if (dbzmode)
				{
					dbzmodetimer += Time.deltaTime;
					Vector3 tempstore = rigidbody.velocity;
					punksprite.color = Color.red;
					canhityou = false;
					transform.rotation = Quaternion.Euler(Vector3.zero);
					if (gravitypulled || stunned || gotdmgbybomb)
					{
						punkanim.Play("g3hurt");
						shaking();
					}
					if (!gravitypulled)
					rigidbody.velocity = Vector3.zero;
					gravity = -0.34f;	
					
					if (gravitypulled && dbzmodetimer - Time.deltaTime > 2f)
					{
						CancelDBZ(tempstore);
						punkanim.Stop();
					}
				
					if (stunned && dbzmodetimer - Time.deltaTime > 2.5f)
					{
						CancelDBZ(tempstore);
						punkanim.Stop();
					}
					
					if (!stunned && !gravitypulled && dbzmodetimer - Time.deltaTime > 0.5f)
					{
						CancelDBZ(tempstore);
						punkanim.Stop();
					}
				}
			
			if (isTarget)
			{
				targettime+= Time.deltaTime;
				if (targettime - Time.deltaTime > 3)
					isTarget = false;
			}
			
			
			// spinning axekick rune effect
			if (gameObject.tag == "Enemy3" && tornado)
			{
				animating = true;
				canhityou = false;
				gravity = -0.01f;
				transform.Rotate(new Vector3(0,0, -1) * 100);
				
				if (transform.position.y <= -300)
					rigidbody.velocity = new Vector3(Random.Range(-400, 400), rigidbody.velocity.y * -1, 0);
				if (transform.position.y > 299)
					rigidbody.velocity = new Vector3(Random.Range(-400, 400), rigidbody.velocity.y * -1, 0);
				if (transform.position.x < -650)
					rigidbody.velocity = new Vector3(rigidbody.velocity.x * -1, Random.Range(-400, 400), 0);
				if (transform.position.x > 600)
					rigidbody.velocity = new Vector3(rigidbody.velocity.x * -1, Random.Range(-400, 400), 0);
			}
			
			//regular axekick effect: OTG
			if (gameObject.tag == "Enemy3" && otg)
			{
				otgtimer += Time.deltaTime;
				animating = true;
				canhityou = false;
				
				punkanim.Play("g3axed");
				gravity = 0;
				if (transform.position.y <= -300 && !frozen)
					gameObject.rigidbody.velocity = new Vector3(0, 1224, 0);
				if (otgtimer - Time.deltaTime >= 2)
				{
					if (upsidedown)
					{
						punksprite.VFlip();
						upsidedown = false;
					}
					otg = false;
					gravity = -27;
					jumpbounce = 930;
					animating = false;
					canhityou = true;
					otgtimer = 0;
				}
			}
			
			//otw
			if (gameObject.tag == "Enemy3" && tackled)
			{
				punkanim.Play("g3hurt");
				storedvelo = rigidbody.velocity;
				canhityou = false;
				tackledtimer += Time.deltaTime;
				if (transform.position.x >= 600 || transform.position.x <= -600)
				{
					if (transform.position.x >= 600)
					{
						Instantiate(debris, new Vector3(600, transform.position.y, transform.position.z), Quaternion.Euler(new Vector3(0, 0, 90)));
						rigidbody.velocity = new Vector3(Getdiff().x, 600, 0);
						storedvelo = rigidbody.velocity;
						audio.PlayOneShot(otwsound);
						Bleedcash();
					}
					
					if (transform.position.x <= -600)
					{
						Instantiate(debris, new Vector3(-600, transform.position.y, transform.position.z), Quaternion.Euler(new Vector3(0, 0, -90)));
						rigidbody.velocity = new Vector3(Getdiff().x, 600, 0);	
						storedvelo = rigidbody.velocity;
						audio.PlayOneShot(otwsound);
						Bleedcash();
					}
				}
				
				if (tackledtimer >= 3)
				{
					tackled = false;
					canhityou = true;
					tackledtimer = 0;
				}
				
			}
			
			//knockdown
		if (tag == "Enemy3" && getknocked)
		{	
			animating = true;
			canhityou = false;

			if (knockdowntimer < 4 && !KDplayed)
				punkgetknocked();
			
			//pretty fucking high
			if (transform.position.y > -315f && transform.position.y <= 150)
			{
				rigidbody.velocity += new Vector3 (0, -8, 0);	
				
			}
			
			//in the air generally
			if (transform.position.y > 150)
			{
				rigidbody.velocity += new Vector3 (0, -16, 0);	
			
			}
				
			//hit the ground	
			if (transform.position.y <= -315f && knockdowntimer < 4.6f)
			{
				rigidbody.velocity = Vector3.zero;
				transform.rotation = Quaternion.Euler(Vector3.zero);
					if (falling)
					{
						falling = false;
						gravity = -27;
						jumpbounce = 1122;
						audio.PlayOneShot(gsmash);
						Instantiate(debris, transform.position, transform.rotation);	
	
					}
					
					knockdowntimer += Time.deltaTime;
					rigidbody.velocity = Vector3.zero;
					punkanim.Play("g3KO");
			}
				
			//about to get up, play getting up anim and cancel all shits
			if (knockdowntimer > 4.6f && knockdowntimer < 5f && tag != "Dead")
			{
				knockdowntimer += Time.deltaTime;
				if (upsidedown)
				{
					punksprite.VFlip();	
					upsidedown = false;
				}
				if (!recoverplayed)
				punkrecover();
			}		
			
			if (knockdowntimer > 5f && tag != "Dead")
			{
				getknocked = false;
				punkanim.Stop();
				knockdowntimer = 0;
				canhityou = true;
				animating = false;
			}

		}
			
			//enemy falling from axekick
			if (gameObject.tag == "Enemy3" && falling)
			{
				jumpbounce = 153;
				if (!upsidedown)
				{
					punksprite.VFlip();
					upsidedown = true;
				}
			}
			
			//states
			
			if (taunted && tag != "Dead")
			{
				punksprite.color = Color.magenta;
				taunttimer += Time.deltaTime;
				if (taunttimer > 3)
				{
					taunted = false;
					taunttimer = 0;
					tauntable = false;
					punksprite.color = Color.white;
				}
			}
			
			if (!tauntable)
			{
				tauntCD += Time.deltaTime;
				if (tauntCD > 6)
				{
					tauntable = true;
					tauntCD = 0;
				}	
			}
			
			if (bombed)
			{
				bombtimer += Time.deltaTime;
				punksprite.color = Color.red;
				
				if (bombtimer >= 6)
				{
					bombed = false;
					bombtimer = 0;
					punksprite.color = Color.white;
				}
				
			}
			
			if (poisoned)
			{
				poisontimer += Time.deltaTime;
				punksprite.color = Color.green;
				
				if (poisontimer >= 1)
				{
					poisonticks++;	
					punkanim.Play("g3stomach");
					health--;
					poisontimer = 0;
				}
				
				if (poisonticks >= 5)
				{
					poisoned = false;
					poisontimer = 0;
					poisonticks = 0;
					punksprite.color = Color.white;
				}
				
			}
			
			//slow from hurri rune
			if (slowed)
			{
				slowtimer += Time.deltaTime;
				punksprite.color = Color.yellow;
				
				gravity = -8;
				jumpbounce = 661;
				
				if (slowtimer > 10)
				{
					slowed = false;
					slowtimer = 0;
					Recoverfromslow();
				}
				
			}
			
			if (grabbed)
			{
				transform.position = Karateman.transform.position;	
				if (!upsidedown)
				{
					punksprite.VFlip();
					upsidedown = true;	
				}
				if (transform.position.y <= -240)
				{
					grabbed = false;
					health -= maxhealth/2;
					punksprite.VFlip();
					upsidedown = false;
				}
			}
			
			if (wallbounce)
			{
				wallbouncetimer += Time.deltaTime;
				tornado = true;
				(gameObject.collider as SphereCollider).radius = 65;
				if (wallbouncetimer > 4)
				{
					tornado = false;	
					wallbounce = false;
					transform.rotation = new Quaternion(0,0,0,0);
					(gameObject.collider as SphereCollider).radius = 65;
					wallbouncetimer = 0;
				}
			}
			
			//dead people
			if (gameObject.tag == "Dead")
			{
				renderer.material.color = Color.white;
				//fly away now scale control
				if (transform.position.y > -300)
				{
					scalereducer -= 0.001f;
					if (rigidbody.velocity.y > -50)
					{
					transform.localScale = new Vector3 (transform.localScale.x + scalereducer,
						transform.localScale.y + scalereducer, transform.localScale.z);
					if (transform.localScale.x < 0)
						Destroy(this.gameObject);
					}
					if (rigidbody.velocity.y <= -50)
					{
						transform.localScale = new Vector3 (transform.localScale.x - scalereducer,
						transform.localScale.y - scalereducer, transform.localScale.z);
					if (transform.localScale.x > 7)
							Destroy(this.gameObject);
						
					}
				}
				
				//on ground sleep now
				if (transform.position.y <= -300)
				{
					punkanim.Play("g3dead");
					deadgroundoffset = Random.Range(0,4);
					deadonfloor += Time.deltaTime;
					transform.position = new Vector3(transform.position.x,
													 transform.position.y, -90);
					transform.rotation = Quaternion.Euler(Vector3.zero);
					transform.localScale = new Vector3 (1.4f, 1.4f, 1);
					gameObject.rigidbody.velocity = Vector3.zero;
					gameObject.collider.enabled = false;
					punksprite.color = Color.gray;
					rotationspeed = 0;
					if (!spawnonce)
					SpawnPowerup();
					if (deadonfloor - Time.deltaTime >= 30)
						Destroy(this.gameObject);
					if (transform.localScale.x > 2)
						Destroy(this.gameObject);
				}
			}
			
			if (!tornado)
				gravity = -27;
			
			if (tag == "Enemy3" && !tornado && !otg && !getknocked && followupready)
			{
				followup = true;
				followupready = false;
			}
			
			#endregion
			
			if (!gsmashready)
			{
				gsmashCD += Time.deltaTime;
				if (!gotrandom)
				getrandom();
				if (gsmashCD >= gsmashwait)
					{
						gsmashready = true;	
						gsmashCD = 0;
						gotrandom = false;
					}
			}
			if (gsmashready)
			{
				if (nocontact)
				punkanim.Play("g3threat");
				gsmashtimer += Time.deltaTime;
				if (!gotrandom)
				getrandom();
				if (gsmashtimer >= gsmashactive)
					{
						gsmashready = false;	
						gsmashtimer = 0;
						gotrandom = false;
					}
			}
			
			if (justdidamove)
			{
				followup = false;
				followupready = false;
				followupdisabletime+= Time.deltaTime;
				if (followupdisabletime > 3)
				{
					justdidamove = false;	
					followupdisabletime = 0;
				}
				
			}
			
			if (playerscript.stunned && !justdidamove)
				followupready = true;
			
			if (gsmashready && followupready)
				gsmashready = false;
			
			
			//DMG cooldowns
			
			if (gotdmgbybomb)
			{
				dmgtypereset += Time.deltaTime;	
				if (dmgtypereset > 0.9f)
				{
					gotdmgbybomb = false;	
					dmgtypereset = 0;
				}
				
			}
			
			if (gotdmgbydkick)
			{
				dmgtypereset += Time.deltaTime;	
				if (dmgtypereset > 0.5f)
				{
					gotdmgbydkick = false;	
					dmgtypereset = 0;
				}
				
			}
			
			if (gotdmgbytackle)
			{
				dmgtypereset += Time.deltaTime;	
				if (dmgtypereset > 0.5f)
				{
					gotdmgbytackle = false;	
					dmgtypereset = 0;
				}
				
			}
			
			if (gotdmgbyforce)
			{
				dmgtypereset += Time.deltaTime;	
				if (dmgtypereset > 0.6f)
				{
					gotdmgbyforce = false;	
					dmgtypereset = 0;
				}
				
			}
			
			if (gotdmgbyclone)
			{
				dmgtypereset += Time.deltaTime;	
				if (dmgtypereset > 0.3f)
				{
					gotdmgbyclone = false;	
					dmgtypereset = 0;
				}
				
			}
			
			
			
			//combo reset
			if (comboedcount > 0)
			{
				comboedreset += Time.deltaTime;
				if (comboedreset > 3)
				{
					comboedcount = 0;	
					comboedreset = 0;
				}
			}
			
			if (sametarget > 0)
			{
				targetreset += Time.deltaTime;
				
				if (targetreset > 1f)
				{
					sametarget = 0;	
					targetreset = 0;
				}
			}
			
			if (highestcombo < comboedcount)
				highestcombo = comboedcount;
	
			movetowardstimer+= Time.deltaTime;
		
		}//nopause
		
		if (pausescript.playerpause)
		{
			rigidbody.velocity = Vector3.zero;
			if (dbzmode || tackled)
				punkanim.Play("g3hurt");
			justpaused = true;
			
		}
	}
	
	void SetCurrent () {
		
		//variables
		currentRotate = Random.Range(minRotatespeed, maxRotatespeed);
		
		if (transform.position.x > 0)
		//right side
		{
		velocityX = Random.Range (-340, -510);
		velocityY = 0f;
		}
		
		if (transform.position.x < 0)
		//left side	
		{
		velocityX = Random.Range (340, 510);
		velocityY = 0f;
		punksprite.HFlip();
		facingleft = false;
		}
		
		//movement
		gameObject.rigidbody.velocity = new Vector3(velocityX, velocityY, 0);
		
	}
	
	void getrandom()
	{
		gsmashwait = Random.Range (3,8);
		gsmashactive = Random.Range (3,5);
		gotrandom = true;
	}
	
	public void FlyAway () 
	{
//		PlayRandom();
		if (gameObject.tag != "Dead" && !tutorial && Application.loadedLevelName != "devmode")
			{
				playerscript.enemydefeated += 1;
				playerscript.XPgained += 64;
			}
		punkanim.Play("g3dead");
		currentRotate = 920;
		chancetodrop = Random.Range (1,5);
		if (chancetodrop == 1 && !spawnonce)
		SpawnPowerup();
		if (bombed && tag != "Dead")
			Instantiate (explosion, transform.position, transform.rotation);
		gameObject.tag = "Dead";
		isTarget = false;
		gravity = -12.6f;
		Vector3 newvelo = new Vector3 
				(Random.Range(-450,450), Random.Range(-100,340), 0);
		CancelDBZ(newvelo);
//		playerscript.CancelDBZ(Vector3.zero);
		if (playerscript.currenttarget == this.gameObject)
			playerscript.currenttarget = null;
	}
	
	IEnumerator DelayedFlyAway ()
	{
		yield return new WaitForSeconds(1);
		
		FlyAway();
	}
	
	public void DelayedFly()
	{
		rigidbody.velocity = Vector3.zero;
		canhityou = false;
		frozen = true;
		StartCoroutine( DelayedFlyAway () );
	}
	
	public void Takedamage(float amount)
	{
		health -= amount; 	
		
	}
	
	IEnumerator GoTowards()
	{
		renderer.material.color = Color.black;
		rigidbody.velocity += Getdiff();	
		
		yield return new WaitForSeconds(1.5f);
		
		renderer.material.color = Color.white;
		movetowardstimer = 0f;


	}
	
	IEnumerator shakee()
	{
		Vector3 pospos = punksprite.offset;
		
		punksprite.offset = new Vector3(pospos.x + 1, pospos.y, pospos.z);
		
		yield return new WaitForSeconds(0.05f);
		
		punksprite.offset = new Vector3(pospos.x - 1, pospos.y, pospos.z);
		
		yield return new WaitForSeconds(0.05f);
		
		punksprite.offset = new Vector3(pospos.x + 1, pospos.y, pospos.z);
		
		yield return new WaitForSeconds(0.05f);
		
		punksprite.offset = new Vector3(pospos.x - 1, pospos.y, pospos.z);
		
		yield return new WaitForSeconds(0.05f);
		
		punksprite.offset = new Vector3(pospos.x, pospos.y, pospos.z);
		
		
	}
	
	public void shaking()
	{
		StartCoroutine ( shakee () );
	}
	
	
	public void Punkattack()
	{
		if (!punkanim.IsPlaying("g3attack"))
		punkanim.Play("g3attack");
	}
	
	public void Gsmash()
	{
		punkanim.Play("g3gsmash");
	}
	
	
//	IEnumerator StomacheAcheMain()
//	{
//		animating = true;
//		renderer.material.mainTextureOffset = stomachache1;
//		
//		yield return new WaitForSeconds(0.1f);
//		
//		renderer.material.mainTextureOffset = stomachache3;
//		
//		yield return new WaitForSeconds(0.1f);
//		
//		renderer.material.mainTextureOffset = stomachache2;
//		
//		yield return new WaitForSeconds(0.1f);
//		
//		renderer.material.mainTextureOffset = stomachache3;
//		animating = false;
//		
//	}
	
//	public void HitStun()
//	{
//		StartCoroutine ( StomacheAcheMain () );	
//	}
	
	IEnumerator Startup()
	{
		Karateman = GameObject.FindGameObjectWithTag("Player");
		
		yield return new WaitForSeconds(1);	
		collider.enabled = true;
		firsttime = false;
	}
	
	IEnumerator Gsmashattack()
	{
		punkanim.Play("g3threat");
		
		
		yield return new WaitForSeconds(0.5f);
		
		if (!falling && !stunned && !dbzmode && !getknocked && !tackled && !grabbed)
		{
			rigidbody.velocity = Vector3.Normalize(Getdiff()) * 400;
			punkanim.Play("g3gsmash");
			attacking = true;
		}
		
		yield return new WaitForSeconds(0.3f);
		
		attacking = false;
		
	}
	
	IEnumerator Lungeattack()
	{
		animating = true;
		punkanim.Play("g3lunge");
		
		
		yield return new WaitForSeconds(0.3f);
		
		if (!falling && !stunned && !dbzmode && !getknocked && !tackled && !grabbed)
		{
			rigidbody.velocity = Vector3.Normalize(Getdiff()) * 400;
			punkanim.Play("g3attack");
			attacking = true;
		}
		
		yield return new WaitForSeconds(0.3f);
		
		attacking = false;
		animating = false;
	}
	
	void punkrecover()
	{
		if (!punkanim.IsPlaying("g3recover") && !recoverplayed)	
		{
			punkanim.Play("g3recover");	
			recoverplayed = true;
		}
	}
	
	void punkgetknocked()
	{
		if (!punkanim.IsPlaying("g3KD") && !KDplayed)	
		{
			punkanim.Play("g3KD");	
			KDplayed = true;
		}
	}
	
	public void normalattack()
	{	
		StartCoroutine ( Lungeattack () );	
	}
	
	public void gsmashattack()
	{	
		StartCoroutine( Gsmashattack () );	
	}
	
	public void Bleedcash()
	{
		if (moneyinpocket >= 1)
		{
			if (tackled && PlayerPrefs.GetString("TackleT2") == "black")
			{
				Instantiate(money, transform.position, Quaternion.Euler(Vector3.zero));
				Instantiate(money, transform.position, Quaternion.Euler(Vector3.zero));
				Instantiate(money, transform.position, Quaternion.Euler(Vector3.zero));
				moneyinpocket -= 3;
			}
			
			if (comboedcount == 3)
			{
				Instantiate(money, transform.position, Quaternion.Euler(Vector3.zero));
				moneyinpocket--;
			}
			if (comboedcount == 4)
			{
				Instantiate(money, transform.position, Quaternion.Euler(Vector3.zero));
				moneyinpocket--;
			}
			
			if (comboedcount == 5)
			{
				Instantiate(money, transform.position, Quaternion.Euler(Vector3.zero));
				Instantiate(money, transform.position, Quaternion.Euler(Vector3.zero));
				Instantiate(money, transform.position, Quaternion.Euler(Vector3.zero));
				moneyinpocket -= 3;
			}
			if (comboedcount == 6)
			{
				Instantiate(money, transform.position, Quaternion.Euler(Vector3.zero));
				Instantiate(money, transform.position, Quaternion.Euler(Vector3.zero));
				Instantiate(money, transform.position, Quaternion.Euler(Vector3.zero));
				moneyinpocket -= 3;
			}
			if (comboedcount == 7)
			{
				Instantiate(money, transform.position, Quaternion.Euler(Vector3.zero));
				Instantiate(money, transform.position, Quaternion.Euler(Vector3.zero));
				Instantiate(money, transform.position, Quaternion.Euler(Vector3.zero));
				Instantiate(money, transform.position, Quaternion.Euler(Vector3.zero));
				moneyinpocket -= 4;
			}
			if (comboedcount == 8)
			{
				Instantiate(money, transform.position, Quaternion.Euler(Vector3.zero));
				Instantiate(money, transform.position, Quaternion.Euler(Vector3.zero));
				Instantiate(money, transform.position, Quaternion.Euler(Vector3.zero));
				Instantiate(money, transform.position, Quaternion.Euler(Vector3.zero));
				Instantiate(money, transform.position, Quaternion.Euler(Vector3.zero));
				moneyinpocket -= 5;
			}
		}
		
	}
	
	void SpawnPowerup () 
	{
		
		itemroll = Random.Range(1,14);
		int scrollroll = Random.Range(1,4);
		
		if (itemroll < 6)
		{
			if (scrollroll == 1)
			Instantiate(redscroll, new Vector3 (transform.position.x, transform.position.y + 90, -110), Quaternion.identity);
			if (scrollroll == 2)
			Instantiate(greenscroll, new Vector3 (transform.position.x, transform.position.y + 90, -110), Quaternion.identity);
			if (scrollroll == 3)
			Instantiate(bluescroll, new Vector3 (transform.position.x, transform.position.y + 90, -110), Quaternion.identity);	
			
		}
		
		if (itemroll < 3 && moneyinpocket >= 1)
		{
			if (highestcombo <= 2)	
				Instantiate(money, transform.position, Quaternion.identity);
			if (highestcombo >= 3 && highestcombo < 5)
			{
				Instantiate(money, transform.position, Quaternion.identity);
				Instantiate(money, transform.position, Quaternion.identity);
			}
			if (highestcombo >= 5 && highestcombo < 8)
			{
				Instantiate(money, transform.position, Quaternion.identity);
				Instantiate(money, transform.position, Quaternion.identity);
				Instantiate(money, transform.position, Quaternion.identity);
			}
			if (highestcombo >= 8 && highestcombo < 12)
			{
				Instantiate(money, transform.position, Quaternion.identity);
				Instantiate(money, transform.position, Quaternion.identity);
				Instantiate(money, transform.position, Quaternion.identity);
				Instantiate(money, transform.position, Quaternion.identity);
			}
			if (highestcombo >= 12)
			{
				for (int g = 0; g < 20; g++)
				{
					Instantiate(money, new Vector3 (transform.position.x, transform.position.y + 90, -110), Quaternion.identity);
				}
			}
		}
		if (itemroll >= 3 && itemroll < 6)
		{
			Instantiate(sushi, transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));
		}
		if (itemroll >= 6)
		{
			Instantiate(chiball, transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));
		}
		
		spawnonce = true;
		
	}
	
	public void CancelDBZ (Vector3 tt)
	{
		dbzmode = false;
		gravitypulled = false;
		canhityou = true;
		dbzmodetimer = 0f;
		gravity = -27;
		jumpbounce = 1122;
		rigidbody.velocity = tt;
		punksprite.color = Color.white;
		
	}
	
	void Getrushrandom()
	{
		randomforrush = Random.Range (2, 8);	
		gotrandomforrush = true;
	}
	
	
	public Vector3 Getdiff()
	{
		Vector3 diff = new Vector3();
		if (Karateman.transform.position.x > transform.position.x)
		diff = new Vector3 (Karateman.transform.position.x + 100, Karateman.transform.position.y, Karateman.transform.position.z) - transform.position; 
		if (Karateman.transform.position.x <= transform.position.x)
		diff = new Vector3 (Karateman.transform.position.x - 100, Karateman.transform.position.y, Karateman.transform.position.z) - transform.position; 
		return diff;
	}
	
	public Vector3 Getdiff(string yesorno, GameObject target)
	{
		Vector3 diff = new Vector3();
		if (target.transform.position.x > transform.position.x)
		diff = new Vector3 (target.transform.position.x + 100, target.transform.position.y, target.transform.position.z) - transform.position; 
		if (target.transform.position.x <= transform.position.x)
		diff = new Vector3 (target.transform.position.x - 100, target.transform.position.y, target.transform.position.z) - transform.position; 
		
		if (yesorno == "yes")
			diff = Vector3.Normalize(diff);
		
		return diff;
	}
	
	
	
	public Vector3 Getnegativediff()
	{
		Vector3 diff = transform.position - Karateman.transform.position; 
		return diff;
	}
	
	void  PlayRandom ()
	{ 
	    if (audio.isPlaying) 
			return;
	    audio.clip = deathsounds[Random.Range(0,deathsounds.Length)];
	    audio.Play();
	}
	
	IEnumerator resetcontactgg()
	{
		yield return new WaitForSeconds(2);	
		nocontact = true;
	}
	
	void Recoverfromslow()
	{
		gravity = -27;	
		jumpbounce = 1122;
		punksprite.color = Color.white;
		
	}
	
	public void resetcontact()
	{
		StartCoroutine ( resetcontactgg () );	
	}
	
	void OnTriggerEnter(Collider otherObject)
	{
		if (falling && transform.position.y < -250 || PlayerPrefs.GetString("TackleT2") == "white" && tackled)
		{
			if (otherObject.tag == "Ground")
			{
				EnemyGround enemyscript = (EnemyGround)otherObject.gameObject.GetComponent("EnemyGround");
				Instantiate(smackanimu, 
				new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
				audio.PlayOneShot(smacksound);
				enemyscript.Die();
				
				if (obox.comboing)
				{
					obox.combocounter += 2;
					obox.combotimer = 0;
				}
				
			}
			
			if (otherObject.tag == "Enemy")
			{
				Punk1 enemyscript = (Punk1)otherObject.gameObject.GetComponent("Punk1");	
				Instantiate(smackanimu, 
				new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
				audio.PlayOneShot(smacksound);
				if (falling)
					enemyscript.Takedamage(playerscript.attackdmg);
				else
				enemyscript.Takedamage(0.2f);
				
				if (obox.comboing)
				{
					obox.combocounter += 2;
					obox.combotimer = 0;
				}
				
			}
			
			if (otherObject.tag == "Enemythrower")
			{
				Punkthrower enemyscript = (Punkthrower)otherObject.gameObject.GetComponent("Punkthrower");	
				Instantiate(smackanimu, 
				new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
				audio.PlayOneShot(smacksound);
				if (falling)
					enemyscript.Takedamage(playerscript.attackdmg);
				else
				enemyscript.Takedamage(0.2f);
				
				if (obox.comboing)
				{
					obox.combocounter += 2;
					obox.combotimer = 0;
				}
				
			}
			
			if (otherObject.tag == "Enemy2")
			{
				Punk2 enemyscript = (Punk2)otherObject.gameObject.GetComponent("Punk2");	
				Instantiate(smackanimu, 
				new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
				audio.PlayOneShot(smacksound);
				if (falling)
					enemyscript.Takedamage(playerscript.attackdmg);
				else
				enemyscript.Takedamage(0.8f);
				
				if (obox.comboing)
				{
					obox.combocounter += 2;
					obox.combotimer = 0;
				}
				
			}
			
			if (otherObject.tag == "Enemy3")
			{
				Punk3 enemyscript = (Punk3)otherObject.gameObject.GetComponent("Punk3");	
				Instantiate(smackanimu, 
				new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
				audio.PlayOneShot(smacksound);
				if (falling)
					enemyscript.Takedamage(playerscript.attackdmg);
				else
				enemyscript.Takedamage(1f);
				
				if (obox.comboing)
				{
					obox.combocounter += 2;
					obox.combotimer = 0;
				}
				
			}
			
		}
		
		if (wallbounce)
		{
			if (otherObject.tag == "Enemy")
			{
				Punk1 enemyscript = (Punk1)otherObject.gameObject.GetComponent("Punk1");	
				Instantiate(smackanimu, 
				new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
				audio.PlayOneShot(smacksound);
				enemyscript.Takedamage(playerscript.attackdmg);
				
			}
			
			if (otherObject.tag == "Enemythrower")
			{
				Punkthrower enemyscript = (Punkthrower)otherObject.gameObject.GetComponent("Punkthrower");	
				Instantiate(smackanimu, 
				new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
				audio.PlayOneShot(smacksound);
				enemyscript.Takedamage(playerscript.attackdmg);
				
			}
			
			if (otherObject.tag == "Enemy2")
			{
				Punk2 enemyscript = (Punk2)otherObject.gameObject.GetComponent("Punk2");	
				Instantiate(smackanimu, 
				new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
				audio.PlayOneShot(smacksound);
				enemyscript.Takedamage(playerscript.attackdmg);
				
			}
			
			if (otherObject.tag == "Enemy3")
			{
				Punk3 enemyscript = (Punk3)otherObject.gameObject.GetComponent("Punk3");	
				Instantiate(smackanimu, 
				new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
				audio.PlayOneShot(smacksound);
				enemyscript.Takedamage(playerscript.attackdmg);
				
			}
			
			if (otherObject.tag == "Ground")
			{
				EnemyGround enemyscript = (EnemyGround)otherObject.gameObject.GetComponent("EnemyGround");
				Instantiate(smackanimu, 
				new Vector3(otherObject.transform.position.x, otherObject.transform.position.y, -120), otherObject.transform.rotation);
				audio.PlayOneShot(smacksound);
				enemyscript.health -= playerscript.attackdmg;
			}
			
			if (otherObject.tag == "Ground2" || otherObject.tag == "Ground3")
			{
				EnemyGround3 enemyscript = (EnemyGround3)otherObject.gameObject.GetComponent("EnemyGround3");
				Instantiate(smackanimu, 
				new Vector3(otherObject.transform.position.x, otherObject.transform.position.y, -120), otherObject.transform.rotation);
				audio.PlayOneShot(smacksound);
				enemyscript.health -= playerscript.attackdmg;
			}
			
			if (otherObject.tag == "Hardcore")
			{
				EnemyWrestler enemyscript = (EnemyWrestler)otherObject.gameObject.GetComponent("EnemyWrestler");	
				Instantiate(smackanimu, 
				new Vector3(otherObject.transform.position.x, otherObject.transform.position.y, -120), otherObject.transform.rotation);
				audio.PlayOneShot(smacksound);
				enemyscript.TakeDamage(playerscript.attackdmg);
				
			}
			
			if (otherObject.tag == "Hardcore2")
			{
				EnemyWrestler2 enemyscript = (EnemyWrestler2)otherObject.gameObject.GetComponent("EnemyWrestler2");	
				Instantiate(smackanimu, 
				new Vector3(otherObject.transform.position.x, otherObject.transform.position.y, -120), otherObject.transform.rotation);
				audio.PlayOneShot(smacksound);
				enemyscript.TakeDamage(playerscript.attackdmg);
				
			}
			
			if (otherObject.tag == "Hardcore3")
			{
				EnemyWrestler3 enemyscript = (EnemyWrestler3)otherObject.gameObject.GetComponent("EnemyWrestler3");	
				Instantiate(smackanimu, 
				new Vector3(otherObject.transform.position.x, otherObject.transform.position.y, -120), otherObject.transform.rotation);
				audio.PlayOneShot(smacksound);
				enemyscript.TakeDamage(playerscript.attackdmg);
				
			}
			
			if (otherObject.tag == "Ninja2")
			{
				Ninja2 enemyscript = (Ninja2)otherObject.gameObject.GetComponent("Ninja2");	
				Instantiate(smackanimu, 
				new Vector3(otherObject.transform.position.x, otherObject.transform.position.y, -120), otherObject.transform.rotation);
				audio.PlayOneShot(smacksound);
				enemyscript.TakeDamage(playerscript.attackdmg);
			}
		}
		
		
	}
	
	void OnGUI () 
	{
		GUI.color = Color.red;
//		GUI.Label(new Rect(0, 160, 200, 100), "gsmashready == " + gsmashready.ToString());
//		GUI.Label(new Rect(0, 180, 200, 100), "gsmashCD:" + gsmashCD.ToString());
//		GUI.Label(new Rect(0, 200, 200, 100), "highestcombno:" + highestcombo.ToString());
//		GUI.Label(new Rect(0, 220, 200, 100), "gsmashtimer:" + gsmashtimer.ToString());
//		GUI.Label(new Rect(0, 240, 200, 100), "gsmashactive:" + gsmashactive.ToString());
//		GUI.Label(new Rect(0, 160, 200, 100), "random == " + randomforrush.ToString());
//		GUI.Label(new Rect(0, 180, 200, 100), "timer:" + movetowardstimer.ToString());

		
	}	
	
}