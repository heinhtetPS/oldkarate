using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Punk1 : MonoBehaviour {
	
	public exSprite punksprite;
	public exSpriteAnimation punkanim;
	private float minRotatespeed = 120, maxRotatespeed = 340;
	private float currentRotate;
	public bool dbzmode = false, gravitypulled = false;
	public float dbzmodetimer = 0;
	private bool firsttime = true;
	public bool canhityou = true;
	public Collider searchrange;
	public int comboedcount = 0;
	public int sametarget = 0;
	public float targetreset = 0;
	public float comboedreset = 0;
	private int highestcombo = 0;
	private int moneyinpocket = 25;
	
	int maincolor;
	public exAtlas generic, blue, red, green;
	
	pausemenu pausescript;
	
	public bool displaced = false;
	private float displacementtimer = 0;
	
	public bool attacking = false, getknocked = false;
	private float dmgCD = 0;
	
	private bool spawnonce = false;

	public bool tutorial = false;
	
	//animutions
	private bool animating = false;
	private bool recoverplayed = false, KDplayed = false;
	private float animreset = 0;
	
	private float velocityX, velocityY;
	public float gravity, jumpbounce;
	
	public float health = 4, maxhealth = 4;
	
	//states
	private float scalereducer = 0;
	private bool frozen = false;
	public bool tornado = false;
	public bool falling = false;
	public bool otg = false, otw = false;
	public bool grabbed = false, taunted = false, tauntable = true;
	public float otgtimer, knockdowntimer, taunttimer, tauntCD;
	private float deadonfloor = 0f;
	private float deadgroundoffset = 0;
	public bool stunned = false;
	public bool facingleft = true;
	public bool upsidedown = false;
	public bool bombed = false;
	public bool poisoned = false;
	private int poisonticks = 0;
	private float bombtimer = 0, poisontimer = 0, wallbouncetimer = 0, tackledtimer = 0;
	public bool wallbounce = false, tackled = false;
	private Vector3 storedvelo;
	private bool justpaused = false;
	private float pausedtimer = 0;
	
	public bool gotdmgbydkick = false, gotdmgbytackle = false, gotdmgbyforce = false, gotdmgbybomb =  false, gotdmgbyclone = false;
	private float dmgtypereset = 0;
	
	private float movetowardstimer = 0f;
	private int randomforrush;
	private bool gotrandomforrush = false;
	
	public Karateoboxnew obox;
	public GameObject Karateman, smackanimu;
	public Player playerscript;
	public Spawnerv2 spawnscript;
	public bool isTarget = false;
	private float targettime;
	
	public AudioClip death1, death2, death3, gsmash, woosh, smacksound, otwsound;
	
	public AudioClip[] deathsounds;
	
	public exSpriteAnimClip[] redanims, blueanims, greenanims;
	
	//itemdrop
	private int chancetodrop;
	private int itemroll;
	public GameObject sushi, money, chiball, explosion;
	public ParticleSystem debris;

	void Start () {
		
		SetCurrent();
		Karateman = GameObject.FindGameObjectWithTag("Player");
		obox = (Karateoboxnew)GameObject.FindGameObjectWithTag("Offense").GetComponent("Karateoboxnew");
		playerscript = (Player)Karateman.GetComponent("Player");
		pausescript = (pausemenu)Camera.main.GetComponent("pausemenu");
		
		collider.enabled = false;
		
		if (GameObject.FindGameObjectWithTag("Tutorial") != null)
			tutorial = true;
		
		deathsounds = new AudioClip[] {death1, death2, death3};
		
		if (maincolor == 0)
		{
			
			
		}
		
		if (maincolor == 1)
		{
			punksprite.SetSprite(red, 6, true);
			punkanim.animations.Clear();
			foreach (exSpriteAnimClip clip in redanims)
			{
				punkanim.AddAnimation(clip);	
			}

			
		}
		
		if (maincolor == 2)
		{
			punksprite.SetSprite(green, 6, true);
			punkanim.animations.Clear();
			foreach (exSpriteAnimClip clip in greenanims)
			{
				punkanim.AddAnimation(clip);	
			}	
			
		}
		
		if (maincolor == 3)
		{
			punksprite.SetSprite(blue, 6, true);
			punkanim.animations.Clear();
			foreach (exSpriteAnimClip clip in blueanims)
			{
				punkanim.AddAnimation(clip);	
			}
			
		}
	}
	
	//this is for things that apply physics. especially rigidbody.velocity
	void FixedUpdate ()
	{
		if (!pausescript.playerpause)
		{
			//tutorial
			if (GameObject.FindGameObjectWithTag("Tutorial") != null && PlayerPrefs.GetInt("Currentlevel") == 1)
				canhityou = false;
			
			//pause velocity return
			if (justpaused)
			{
				rigidbody.velocity = storedvelo;	
				justpaused = false;
			}
			
			//gravity
			if (transform.position.y > -300 && !frozen  && tag != "Dead" && !dbzmode && !getknocked)
					gameObject.rigidbody.velocity += new Vector3(0, gravity, 0);
			
			//bounce
			if (transform.position.y <= -300 && !frozen && !otg  && !getknocked && tag == "Enemy")
				gameObject.rigidbody.velocity = new Vector3(Random.Range (-204,204), jumpbounce, 0);
			
			//gsmash & bounce
				if (transform.position.y <= -300 && !frozen && !tornado && falling && tag == "Enemy")
				{
					falling = false;
					otg = true;
					if (upsidedown)
					{
						punksprite.VFlip();
						upsidedown = false;
					}
					punkaxed();
					audio.PlayOneShot(gsmash);
					Instantiate(debris, transform.position, transform.rotation);
					gameObject.rigidbody.velocity = new Vector3(0, jumpbounce, 0);
					if (obox.combocounter > 10)
					Bouncebg();
				}
			 
			// regular dying 
			if (gameObject.tag == "Enemy" && health <= 0 && !frozen && !tornado && !falling)
			{
				FlyAway();
			}
			
			//semi intelligent chase
			if (!gotrandomforrush)
				Getrushrandom();
			if (taunted && GameObject.FindGameObjectWithTag("Fake") != null)
			{
				GameObject Fakeman = GameObject.FindGameObjectWithTag("Fake");
				if (tag == "Enemy" && !tornado && !otg && !getknocked && !gravitypulled && !falling && !tackled && movetowardstimer - Time.deltaTime >= randomforrush)
				{
					rigidbody.velocity += Getdiff("no", Fakeman);
					movetowardstimer = 0f;
					gotrandomforrush = false;
				}
			}
			
			if (!taunted)
			{
				if (tag == "Enemy" && !tornado && !otg && !getknocked && !gravitypulled && !falling && !tackled && movetowardstimer - Time.deltaTime >= randomforrush)
				{
					rigidbody.velocity += Getdiff();
					movetowardstimer = 0f;
					gotrandomforrush = false;
				}
			}
			
			
		}
	}
	
	void Update () {
		
		if (!pausescript.playerpause)
		{
		
			//variables
			float rotationspeed = currentRotate * Time.deltaTime;
			
			//activate
			if (firsttime)
				StartCoroutine( Startup () );
				
			//spinning
			if (transform.position.y > -300 && tag == "Dead")
			{
				animating = false;
				punkdead();
				transform.Rotate(new Vector3(0,0,-1) * rotationspeed);
			}
			
			//taunt
			if (GameObject.FindGameObjectWithTag("Fake") != null && tauntable)
				taunted = true;
			if (GameObject.FindGameObjectWithTag("Fake") == null && taunted)
			{
				taunted = false;
				taunttimer = 0;
				tauntCD = 0;
			}
			
			//general animutinos
			if (!animating)
				{
					//low going down (crab)
					if (transform.position.y < -150f && rigidbody.velocity.y < 0 && tag == "Enemy" && !getknocked)
						punkcrab();
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
					if (tag == "Enemy")
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
				if (!gravitypulled)
				    punksprite.color = Color.white;
				
				
				//fuck 3d
				if (transform.position.z != -100 && !displaced)
					transform.position = new Vector3(transform.position.x, transform.position.y, -100);
			
				if (displaced)
					transform.position = new Vector3(transform.position.x, transform.position.y, -200);
			
			
	#region different states------------------------------------------------------------------
			if (gameObject.tag == "Enemy")
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
//					punkhurt();
					if (dbzmodetimer < 0.5f)
					shaking();
					punksprite.color = Color.red;
					transform.rotation = Quaternion.Euler(Vector3.zero);
					if (!gravitypulled)
					rigidbody.velocity = Vector3.zero;
					gravity = -0.34f;	
					
					if (stunned && dbzmodetimer - Time.deltaTime > 3f)
					{
						CancelDBZ(tempstore);
						punkanim.Stop();
					}
				
					if (gravitypulled && dbzmodetimer - Time.deltaTime > 2f)
					{
						CancelDBZ(tempstore);
						punkanim.Stop();
					}
					
					if (!stunned && !gravitypulled && dbzmodetimer - Time.deltaTime > 0.7f)
					{
						CancelDBZ(tempstore);
						punkanim.Stop();
					}
				}
			
			if (dbzmode || !canhityou)
				StopCoroutine ( "Lungeattack" );
			
				
			
			if (isTarget)
			{
				targettime+= Time.deltaTime;
				if (targettime - Time.deltaTime > 3)
					isTarget = false;
			}
			
			
			// spinning hurricane rune effect
			if (gameObject.tag == "Enemy" && tornado)
			{
				animating = true;
				canhityou = false;
				gravity = -0.01f;
				transform.Rotate(new Vector3(0, 0, -1) * 40);
				
				if (transform.position.y <= -300)
					rigidbody.velocity = new Vector3(Random.Range(-400, 400), rigidbody.velocity.y * -1, 0);
				if (transform.position.y > 290)
					rigidbody.velocity = new Vector3(Random.Range(-400, 400), rigidbody.velocity.y * -1, 0);
				if (transform.position.x < -650)
					rigidbody.velocity = new Vector3(rigidbody.velocity.x * -1, Random.Range(-400, 400), 0);
				if (transform.position.x > 600)
					rigidbody.velocity = new Vector3(rigidbody.velocity.x * -1, Random.Range(-400, 400), 0);
			}
			
			//enemy falling from axekick
			if (gameObject.tag == "Enemy" && falling)
			{
				jumpbounce = 153;
				if (!upsidedown)
				{
					punksprite.VFlip();
					upsidedown = true;
				}
			}
			
			//regular tacklesmash effect: OTW
			if (gameObject.tag == "Enemy" && tackled)
			{
				punkhurt();
				storedvelo = rigidbody.velocity;
				canhityou = false;
				tackledtimer += Time.deltaTime;
				if (transform.position.x >= 600 || transform.position.x <= -600)
				{
					if (transform.position.x >= 600)
					{
						Instantiate(debris, new Vector3(600, transform.position.y, transform.position.z), Quaternion.Euler(new Vector3(0, 0, 90)));
						audio.PlayOneShot(otwsound);
						Bleedcash();
						rigidbody.velocity = new Vector3(Getdiff().x, 600, 0);	
						storedvelo = rigidbody.velocity;
					}
					
					if (transform.position.x <= -600)
					{
						Instantiate(debris, new Vector3(-600, transform.position.y, transform.position.z), Quaternion.Euler(new Vector3(0, 0, -90)));
						audio.PlayOneShot(otwsound);
						Bleedcash();
						rigidbody.velocity = new Vector3(Getdiff().x, 600, 0);	
						storedvelo = rigidbody.velocity;
					}
				}
				
				if (tackledtimer >= 3)
				{
					tackled = false;	
					canhityou = true;
					tackledtimer = 0;
				}
				
			}
			
			//regular axekick effect: OTG
			if (gameObject.tag == "Enemy" && otg)
			{
				otgtimer += Time.deltaTime;
				animating = true;
				canhityou = false;
				punkaxed();
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
			
			//knockdown
			if (tag == "Enemy" && getknocked)
			{
				animating = true;
				canhityou = false;

				if (knockdowntimer < 4 && !KDplayed)
				punkgetKD();
				
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
					if (falling)
					{
						falling = false;
						gravity = -27;
						jumpbounce = 1122;
						audio.PlayOneShot(gsmash);
						Instantiate(debris, transform.position, transform.rotation);	
						if (GameObject.FindGameObjectWithTag("Tutorial") != null && PlayerPrefs.GetInt("Currentlevel") == 1)
						{
							FlyAway();	
						}
					}
					
					knockdowntimer += Time.deltaTime;
					rigidbody.velocity = Vector3.zero;
					punkKO();
					if (GameObject.FindGameObjectWithTag("Tutorial") != null && PlayerPrefs.GetInt("Currentlevel") == 1)
						{
							FlyAway();	
						}
					
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
				
				//just got up cancel anim 
				if (knockdowntimer >= 5f && tag != "Dead")
				{
					punkanim.Stop();
					getknocked = false;
					knockdowntimer = 0;
					canhityou = true;
					animating = false;
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
					punkstomach();
					health--;
					poisontimer = 0;
				}
				
				if (poisonticks >= 5)
				{
					poisoned = false;
					poisontimer = 0;
					punksprite.color = Color.white;
				}
				
			}
			
			if (!tornado)
				gravity = -27;
			
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
					FlyAway();	
				}
			}
			
			if (wallbounce)
			{
				wallbouncetimer += Time.deltaTime;
				tornado = true;
				punkfall();
				(gameObject.collider as SphereCollider).radius = 65;
				if (wallbouncetimer > 4)
				{
					tornado = false;	
					wallbounce = false;
					animating = false;
					canhityou = true;
					transform.rotation = new Quaternion(0,0,0,0);
					(gameObject.collider as SphereCollider).radius = 65;
					wallbouncetimer = 0;
					punkanim.Stop();
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
					punkdeadx();
					searchrange.enabled = false;
					deadgroundoffset = Random.Range(0,4);
					deadonfloor += Time.deltaTime;
					transform.position = new Vector3(transform.position.x,
													 transform.position.y, -90);
					transform.rotation = Quaternion.Euler(Vector3.zero);
					transform.localScale = new Vector3 (1.2f, 1.2f, 1);
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
			
			#endregion
			
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
			
			if (attacking)
			{
				dmgCD += Time.deltaTime;
				if (dmgCD >= 0.6f)
				{
					attacking = false;	
					dmgCD = 0;
				}
				
			}
			
			//damage resets
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
			
			//anim reset
			if (recoverplayed || KDplayed)
			{
				animreset += Time.deltaTime;	
				if (animreset >= 3)
				{
					if (recoverplayed)	
						recoverplayed = false;
					if (KDplayed)
						KDplayed = false;
					animreset = 0;
				}
				
			}
			
			if (displaced)
			{	
				displacementtimer += Time.deltaTime;
				if (displacementtimer >= 1f)
				{
					displaced = false;
					displacementtimer = 0;
				}
				
			}
	
			
			
			//timer for Semi intelligent chasing
			movetowardstimer+= Time.deltaTime;
			
			if (highestcombo < comboedcount)
				highestcombo = comboedcount;
			
		}//END PLAYERPAUSE
		
		if (pausescript.playerpause)
		{
			rigidbody.velocity = Vector3.zero;
			if (dbzmode || tackled)
				punkhurt();
			justpaused = true;
		}
		
	}//UPDATE | END
	
	
	
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
		
		//choose color
		maincolor = Random.Range(0, 4);
		if (tutorial && PlayerPrefs.GetInt("Currentlevel") == 1)
		{
			maincolor = 0;
			Debug.Log("doin it");
		}
	
	}
	
	public void FlyAway () 
	{
		if (transform.position.y <= -315)
		{
//			PlayRandom();	
			punkhurt();
			chancetodrop = Random.Range (1,5);
			if (chancetodrop == 1 && !spawnonce)
			SpawnPowerup();
			if (bombed)
			Instantiate (explosion, transform.position, transform.rotation);
			if (gameObject.tag != "Dead" && !tutorial  && Application.loadedLevelName != "devmode")
			{
				playerscript.enemydefeated += 1;
				playerscript.XPgained += 16;
			}
			gameObject.tag = "Dead";
			return;
		}
		
//		PlayRandom();
		if (gameObject.tag != "Dead" && !tutorial)
		{
			playerscript.enemydefeated += 1;
			playerscript.XPgained += 16;
		}
		punkdead();
		currentRotate = 920;
		chancetodrop = Random.Range (1,5);
		if (chancetodrop == 1 && !spawnonce)
		SpawnPowerup();
		if (bombed && tag != "Dead")
			Instantiate (explosion, transform.position, transform.rotation);
		gameObject.tag = "Dead";
		isTarget = false;
		gravity = -12.6f;
		searchrange.enabled = false;
		Vector3 newvelo = new Vector3 
				(Random.Range(-650,650), Random.Range(-100,340), 0);
		CancelDBZ(newvelo);
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
	
	void Getrushrandom()
	{
		randomforrush = Random.Range (4, 10);	
		gotrandomforrush = true;
	}
	
	IEnumerator Lungeattack()
	{
		animating = true;
		punklunge();
		
		
		yield return new WaitForSeconds(0.3f);
		
		if (!falling && !stunned && !dbzmode && !getknocked && !tackled && !grabbed)
		{
			punkattack();
			rigidbody.velocity = Vector3.Normalize(Getdiff()) * 400;
			attacking = true;
			if (dbzmode || canhityou == false)
			{
				attacking = false;
				punkhurt();
			}
		}
		
		yield return new WaitForSeconds(0.3f);
		
		attacking = false;
		animating = false;
		
	}
	
	public void normalattack()
	{	
		StartCoroutine ( Lungeattack () );	
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
				moneyinpocket --;
			}
			if (comboedcount == 4)
			{
				Instantiate(money, transform.position, Quaternion.Euler(Vector3.zero));
				moneyinpocket --;
			}
			if (comboedcount == 5)
			{
				Instantiate(money, transform.position, Quaternion.Euler(Vector3.zero));
				Instantiate(money, transform.position, Quaternion.Euler(Vector3.zero));
				Instantiate(money, transform.position, Quaternion.Euler(Vector3.zero));
				moneyinpocket -= 3;
			}
		}
		
	}
	
	void SpawnPowerup () 
	{
		
		itemroll = Random.Range(1,14);
		
		if (itemroll < 3 && moneyinpocket >= 1)
		{
			if (highestcombo <= 2)	
			Instantiate(money, transform.position, Quaternion.Euler(Vector3.zero));
			if (highestcombo == 3)
			{
				Instantiate(money, transform.position, Quaternion.identity);
				Instantiate(money, transform.position, Quaternion.identity);
			}
			if (highestcombo == 4)
			{
				Instantiate(money, transform.position, Quaternion.identity);
				Instantiate(money, transform.position, Quaternion.identity);
				Instantiate(money, transform.position, Quaternion.identity);
			}
			if (highestcombo > 4)
			{
				Instantiate(money, transform.position, Quaternion.identity);
				Instantiate(money, transform.position, Quaternion.identity);
				Instantiate(money, transform.position, Quaternion.identity);
				Instantiate(money, transform.position, Quaternion.identity);
				Instantiate(money, transform.position, Quaternion.identity);
			}
		}
		
		if (itemroll >= 3 && itemroll < 6)
		{
			Instantiate(sushi, transform.position, transform.rotation);
		}
		if (itemroll >= 6 && itemroll < 9)
		{
			Instantiate(chiball, transform.position, transform.rotation);
		}
		
		spawnonce = true;
		
	}
	
	public void CancelDBZ (Vector3 tt)
	{
		dbzmode = false;
		gravitypulled = false;
		dbzmodetimer = 0f;
		gravity = -27;
		jumpbounce = 1122;
		rigidbody.velocity = tt;
		canhityou = true;
		punksprite.color = Color.white;
		
	}
	
	void shaking()
	{
		StartCoroutine ( shakee () );
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
	
	public void Takedamage(float amount)
	{
		health -= amount; 	
		
	}
	
	
	public Vector3 Getdiff()
	{
		Vector3 diff = Karateman.transform.position - transform.position; 
		return diff;
	}
	
	public Vector3 Getdiff(string yesorno, GameObject target)
	{
		Vector3 diff = target.transform.position - transform.position;
		
		if (yesorno == "yes")
			diff = Vector3.Normalize(diff);
		
		return diff;
	}
	
	public void  PlayRandom ()
	{ 
	    if (audio.isPlaying) 
			return;
	    audio.clip = deathsounds[Random.Range(0,deathsounds.Length)];
	    audio.Play();
	}
	
	void Bouncebg()
	{
		GameObject[] backgstuff = GameObject.FindGameObjectsWithTag("bgbump");
		
		foreach (GameObject bb in backgstuff)
		{
			bb.rigidbody.velocity = new Vector3 (0, 500, 0);	
			
		}
		
	}
	
	public void GetHit()
	{
		//combo count (non KD state)
		if (!getknocked)
		{
			if (dbzmode)
				dbzmodetimer = 0;
			dbzmode = true;
			comboedcount++;
			comboedreset = 0;
		}
		
		//increasing same target counting and bleedmoney effect
		if (sametarget > 0)
			targetreset = 0;
			sametarget++;
			Bleedcash();
		
		//visual feedback
		punkhurt();
//		shaking();
		
		//damage stuff
		//1/2 scaling. Anything beyond 6 will automatically do no dmg because there's no code for it
		if (sametarget >= 4 && sametarget <= 6)
			Takedamage(playerscript.attackdmg / 2);
		
		
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
				enemyscript.Takedamage(0.2f);
				
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
				enemyscript.Takedamage(0.2f);
				
				if (obox.comboing)
				{
					obox.combocounter += 2;
					obox.combotimer = 0;
				}
				
			}
			
			if (otherObject.tag == "Hardcore")
			{
				EnemyWrestler enemyscript = (EnemyWrestler)otherObject.gameObject.GetComponent("EnemyWrestler");	
				Instantiate(smackanimu, 
				new Vector3(otherObject.transform.position.x, otherObject.transform.position.y, -120), otherObject.transform.rotation);
				audio.PlayOneShot(smacksound);
				enemyscript.FlyAway();
				
			}
			
			if (otherObject.tag == "Hardcore2")
			{
				EnemyWrestler2 enemyscript = (EnemyWrestler2)otherObject.gameObject.GetComponent("EnemyWrestler2");	
				Instantiate(smackanimu, 
				new Vector3(otherObject.transform.position.x, otherObject.transform.position.y, -120), otherObject.transform.rotation);
				audio.PlayOneShot(smacksound);
				enemyscript.dbzmode = true;
				enemyscript.TakeDamage(playerscript.attackdmg);
				
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
				new Vector3(otherObject.transform.position.x, otherObject.transform.position.y, -120), otherObject.transform.rotation);
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
	
	#region Anims for different colors--------------------------------------------------------------------
	//animations functions

	void punkcrab()
	{
		if (maincolor == 0)
		{
			punkanim.Play("Punkcrab");
		}
		
		if (maincolor == 1)
		{
			punkanim.Play("Punkcrabaltred");
		}
		
		if (maincolor == 2)
		{
			punkanim.Play("Punkcrabaltgreen");	
		}
		
		if (maincolor == 3)
		{
			punkanim.Play("Punkcrabaltblue");	
			
		}
		
	}

	void punkdead()
	{
		if (maincolor == 0)
		{
			punkanim.Play("Punkdead");
		}
		
		if (maincolor == 1)
		{
			punkanim.Play("Punkdeadaltred");
		}
		
		if (maincolor == 2)
		{
			punkanim.Play("Punkdeadaltgreen");
			
		}
		
		if (maincolor == 3)
		{
			punkanim.Play("Punkdeadaltblue");
			
		}
		
	}

	void punkstomach()
	{
		if (maincolor == 0)
		{
			punkanim.Play("Punkstomach");
		}
		
		if (maincolor == 1)
		{
			punkanim.Play("Punkstomachaltred");
		}
		
		if (maincolor == 2)
		{
			punkanim.Play("Punkstomachaltgreen");
			
		}
		
		if (maincolor == 3)
		{
			punkanim.Play("Punkstomachaltblue");
			
		}
		
	}

	public void punkattack()
	{
		if (maincolor == 0)
		{
			punkanim.Play("Punkattack");
		}
		
		if (maincolor == 1)
		{
			punkanim.Play("Punkattackaltred");
		}
		
		if (maincolor == 2)
		{
			punkanim.Play("Punkattackaltgreen");
			
		}
		
		if (maincolor == 3)
		{
			punkanim.Play("Punkattackaltblue");
			
		}
		
	}

	void punklunge()
	{
		if (maincolor == 0)
		{
			punkanim.Play("Punklunge");
		}
		
		if (maincolor == 1)
		{
			punkanim.Play("Punklungealtred");
			
		}
		
		if (maincolor == 2)
		{
			punkanim.Play("Punklungealtgreen");
			
			
		}
		
		if (maincolor == 3)
		{
			punkanim.Play("Punklungealtblue");
			
			
		}
		
	}

	void punkgetKD()
	{
		if (maincolor == 0)
		{
			if (!punkanim.IsPlaying("punkKD") && !KDplayed)	
			{
				punkanim.Play("punkKD");	
				KDplayed = true;
			}
		}
		
		if (maincolor == 1)
		{
			if (!punkanim.IsPlaying("punkKDaltred") && !KDplayed)	
			{
				punkanim.Play("punkKDaltred");	
				KDplayed = true;
			}
		}
		
		if (maincolor == 2)
		{
			if (!punkanim.IsPlaying("punkKDaltgreen") && !KDplayed)	
			{
				punkanim.Play("punkKDaltgreen");	
				KDplayed = true;
			}
		}
		
		if (maincolor == 3)
		{
			if (!punkanim.IsPlaying("punkKDaltblue") && !KDplayed)	
			{
				punkanim.Play("punkKDaltblue");	
				KDplayed = true;
			}
		}
	}

	void punkKO()
	{
		if (maincolor == 0)
		{
			punkanim.Play("punkKO");
		}
		
		if (maincolor == 1)
		{
			punkanim.Play("punkKOaltred");
		}
		
		if (maincolor == 2)
		{
			punkanim.Play("punkKOaltgreen");
			
		}
		
		if (maincolor == 3)
		{
			punkanim.Play("punkKOaltblue");
			
		}
		
	}

	void punkfall()
	{
		if (maincolor == 0)
		{
			punkanim.Play("punkfall");
		}
		
		if (maincolor == 1)
		{
			punkanim.Play("punkfallaltred");
		}
		
		if (maincolor == 2)
		{
			punkanim.Play("punkfallaltgreen");
			
		}
		
		if (maincolor == 3)
		{
			punkanim.Play("punkfallaltblue");
			
		}
		
	}

	void punkrecover()
	{
		if (maincolor == 0)
		{
			if (!punkanim.IsPlaying("punkrecover") && !recoverplayed)	
			{
				punkanim.Play("punkrecover");	
				recoverplayed = true;
			}
			
		}
		
		if (maincolor == 1)
		{
			if (!punkanim.IsPlaying("punkrecoveraltred") && !recoverplayed)	
			{
				punkanim.Play("punkrecoveraltred");	
				recoverplayed = true;
			}
			
		}
		
		if (maincolor == 2)
		{
			if (!punkanim.IsPlaying("punkrecoveraltgreen") && !recoverplayed)	
			{
				punkanim.Play("punkrecoveraltgreen");	
				recoverplayed = true;
			}
			
		}
		
		if (maincolor == 3)
		{
			if (!punkanim.IsPlaying("punkrecoveraltblue") && !recoverplayed)	
			{
				punkanim.Play("punkrecoveraltblue");	
				recoverplayed = true;
			}
			
		}
	}

	void punkaxed()
	{
		if (maincolor == 0)
		{
			punkanim.Play("Punkaxed");
		}
		
		if (maincolor == 1)
		{
			punkanim.Play("Punkaxedaltred");
		}
		
		if (maincolor == 2)
		{
			punkanim.Play("Punkaxedaltgreen");
			
		}
		
		if (maincolor == 3)
		{
			punkanim.Play("Punkaxedaltblue");
			
		}
		
	}

	void punkhurt()
	{
		if (maincolor == 0)
		{
			punkanim.Play("punkhurt");
		}
		
		if (maincolor == 1)
		{
			punkanim.Play("punkhurtaltred");
		}
		
		if (maincolor == 2)
		{
			punkanim.Play("punkhurtaltgreen");
			
		}
		
		if (maincolor == 3)
		{
			punkanim.Play("punkhurtaltblue");
			
		}
		
	}

	void punkdeadx()
	{
		if (maincolor == 0)
		{
			punkanim.Play("Punkdeadx");
		}
		
		if (maincolor == 1)
		{
			punkanim.Play("Punkdeadxaltred");
		}
		
		if (maincolor == 2)
		{
			punkanim.Play("Punkdeadxaltgreen");
			
		}
		
		if (maincolor == 3)
		{
			punkanim.Play("Punkdeadxaltblue");
			
		}
		
	}
	
	//----------------------------------------------------------------------------------------------
	#endregion
	

	
	
	
//	void OnGUI () 
//	{
//		GUI.color = Color.red;
////		if (tag != "Dead" && punkanim.GetCurrentAnimation() != null)
////		GUI.Label(new Rect(0, 160, 200, 100), "animu == " + punkanim.GetCurrentAnimation().name.ToString());
////		GUI.Label(new Rect(0, 180, 260, 100), "velo: " + rigidbody.velocity.ToString());
//		
//	}	
	
}