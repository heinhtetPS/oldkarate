using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
	
	public enum State
	{
		Playing,
		Explosion,
		Invincible
	}
	
	//karateman basic status
	public State state = State.Playing;
	private float rotationspeed = 16;
	public int score = 0; 
	public int lives = 2;
	public float balls = 5, maxballs = 5;
	public float ballsbar1 = 1, ballsbar2 = 1, ballsbar3 = 1, ballsbar4 = 1, ballsbar5 = 1;
	public float hits = 0, spacebar = 0;
	public int gothit = 0;
	public bool endtime = false, stagetime = true;
	private float PlayerSpeed, invincibility = 1;
	public float yellowhealth = 10;
	public float redhealth = 10;
	public float maxHealth = 10;
	public float greenhealth = 0;
	public float bonushealth = 10;
	public float gravity, jumpbounce;
	public bool shielded = false;
	public float shieldhit, shieldlimit;
	private bool speedbuff = false;
	public bool dbzmode = false;
	public bool targetacquired = false;
	public int attackdmg = 1;
	public int heavydmg = 2;
	private float leftdashtimer = 0f, rightdashtimer = 0f;
	private bool doubledashwindow = false;
	private bool moving = false;
	public bool dashing = false;
	public bool leftdashbool = false, rightdashbool = false;
	private float dashCD = 0f;
	public ParticleSystem enemysmack;
	private Vector3 prevpos;
	public float gsmashCDbase = 2, HurrCDbase = 3, tackleCDbase = 2, forceCDbase = 3, LazerCDbase = 15, serenCDbase = 4;
	public float LazerCDreal = 15;
	public bool falling = false, stunned = false;
	private float stuntimer = 0, fallingx;
	public bool attackbuff = false;
	public float bufftimer = 0;
	private bool specialready = true;
	private float specialCD;
	public bool blocking = false, perfectguard = false;
	private float blockbar = 5, maxblockbar = 5, blockbarregen;
	private bool showblockbar = false;
	private float bbtimer = 0;
	public bool guardbreak = false;
	private string Lastmove;
	private bool HPshake = false, ballshake = false, blockshake = false;
	public float HPoffset = 0, HPoffset2 = 0, ballsoffset = 0; 
	private float blockoffset = 0, blockoffset2 = 0;
	public int moneythisround = 0, redpickup = 0, greenpickup = 0, bluepickup = 0;
	public int heavycounter = 0;
	public bool dashready = true;
	private float dashcooldown = 0;
	private float airtime, maxairtime;
	string lastkeyboardinput = null;
	public bool dynastystyle = false;
	private bool axisdelay = false;
	private float axdelaytimer;
	private bool dashticket = true;
	private float protest = 0.5f;
	private int counterstrike = 0;

	
	//actions and animation
	public int highestcombo = 0;
	public bool animating = false;
	public float animationtimer = 0;
	public bool groundsmash = false, grabbing = false, caughtone = false, gSmashready = true, shining = false;
	public bool hurricane = false, hurriready = true, barrierready = true;
	public bool beamready = true;
	public bool tackleon = false, tackleready = true, forceon = false, forceready = true, hfistready = true,
	hfiston = false, finalbodyready = true, finalbodyon = false, warudoready = true, warudoon = false;
	public int attackcounter = 1;
	public int hadocounter = 1;
	private bool atktimer;
	private float atkreset;
	private float atkCD;
	public bool atkready;
	private float frameX, frameY;
	private float frametimer;
	private float animudelay;
	private bool frame1played = false, frame2played = false, frame3played = false;
	public bool targetdash = false, doingheavy = false;
	private float heavytimer;
	public GameObject currenttarget;
	public float dbzmodetimer = 0;
	public float gSmashCD, hurriCD, beamCD, barrierCD, tackleCD, forceCD;
	private float blinkrate = 0.1f;
	private int blinks = 6, blinkcount = 0;
	private int whichground;
	private Vector2 currentanimation;
	public bool linkwindow = false;
	public float linktimer = 0f;
	public bool leftkick = false, rightkick = false;
	public bool junklin = false;
	private float junkletimer = 0;
	private float upsidedowntimer = 0;
	public bool additionalairtime = false;
	public float airtimer = 0;
	private bool bump = false;
	private float bumptimer;
	private float blocktimer = 0, perfectgtimer = 0;
	private bool parrying = false;
	private float parrytimer = 0;
	public bool gettinghit = false;
	private Quaternion basic = new Quaternion(0,0,0,0);
	public bool itembounce = false;
	private float ibouncereset = 0;
	public bool rightclone = false, leftclone = false, topclone = false, botclone = false;
	public bool piledrive = false;
	private bool shootingbeam = false;
	public int numberforlinks = 0;
	private float lunknumberreset;
	public float XPgained = 0;
	private int dashcombocount = 0;
	private int bounceupondeath = 0;
	private float deathtimer = 0;

	public GameObject lastenemy;
	
	
	public exSpriteAnimation karateanim;
	public exSprite karatesprite;
//	public exSpriteAnimState karatestate;
	public bool facingright = true, upsidedown = false;
	
	
	
	private Vector2 junkleright = new Vector2(0,0);
	private Vector2 junkleleft = new Vector2(0.5f,0.25f);
	private Vector2 junkleboth = new Vector2(0.25f,0.75f);
	
	private Vector3 regularscale = new Vector3(1.2f, 1.2f, 1);
	private Vector3 hurrscale =  new Vector3 (1.4f, 1.4f, 1);
	private Vector3 firekickscale = new Vector3(13, 1, 7.6f);
	//original is -23, 1020
	private float regulargrav = -23f, regularjb = 1020;
	
	
	//mouse stuff
	public bool mouserotating = false, shortrotate = false;
	private float mouseX, mouseY, diffX, diffY, cameraDif, rotatetimer;
	private Vector3 mWorldPos, mainPos;
	private bool wallrotate = false;
	private bool Usemousecontrols = true;
	
	//grabbing from other objects
	public Karateoboxnew obox;
	public targettingsphere targetbox;
	public leftrightbox hitbox;
	public GameObject enemy, explosion, beam, biggerbeam, vertbeam, groundbreak1, groundbreak2, spiritb, blocked, tdoor, backg, shadow, dashemit, 
	clonegeneric, Serenity, spear, blackspear, club, blackclub, shadowflip, supshine, greyscreen, coin;
	public GameObject[] gbreak;
	public GameObject[] bloods;
	public Powerups powerup;
	public Level1Spawn spawner1;
	public Level2Spawn spawner2;
	public Level3Spawn spawner3;
	public Level4Spawn spawner4;
	public pausemenu pausescript;
	public PlayerInfo infoscript;
	public barrier serenityscript;
	
	
	//time & space
	private int randomdelay = 0;
	private float timenow = 0f;
	private float hitdelay;
	private float dotdmgdelay;
	private float scalereducer;
	
	
	//stats
	public int enemydefeated = 0, grounddefeated = 0, wrestlerdefeated = 0, throwersdefeated = 0, ZZdefeated = 0, ninjadefeated = 0, specialdefeated = 0;
	public int lvlenemies, lvlgrounds, lvlwrestlers;
	
	
	//GUI stuff
	private GUITexture icon1, icon2, icon3;
	public bool devshitison = false;
	private Color trans1 = new Color(255,255,255, 0.5f);
	private Color trans2 = new Color(255,255,255, 0.8f);
	
	
	//other assets
	public AudioClip death, smack1, blocksmack, whoosh1, whoosh2, shit, boom, lazer, wind, pguardsound, teleport, heal, voice, breaksound, forcewoosh, super, deathsmack, deathbounce;
	public Texture2D healthred, healthyellow, healthgreen, afrohead, chiball, healthframe, graybar, wavesphere, beamicon,
	gsicon;
	
	
	void Start () 
	{

		SetDefaults();
		InitializeStats();
		enemydefeated = 0; grounddefeated = 0; wrestlerdefeated = 0;
		gbreak = new GameObject[] {groundbreak1, groundbreak2};
//		balls = maxballs;
		pausescript = (pausemenu)GameObject.FindGameObjectWithTag("MainCamera").GetComponent("pausemenu");
		infoscript = (PlayerInfo)this.gameObject.GetComponent("PlayerInfo");
		backg = GameObject.FindGameObjectWithTag("Backg");
		GetSpawner();
		
		StartCoroutine ( Stagestarteup (4) );
		
		
		//optional mods
		if (PlayerPrefs.GetInt("Mousecontrols") == 0)
			Usemousecontrols = false;
		if (PlayerPrefs.GetInt("Mousecontrols") == 1)
			Usemousecontrols = true;
		if (PlayerPrefs.GetInt("Autocombo") == 0)
			dynastystyle = false;
		if (PlayerPrefs.GetInt("Autocombo") == 1)
			dynastystyle = true;
		
		//ability modifications
		if (PlayerPrefs.GetString("LazerT1") == "white")
			LazerCDreal = LazerCDbase - 5;
		
		
	}
	
	void FixedUpdate ()
	{
		
		if (!pausescript.pausemenud || !pausescript.paused || !endtime)
		{
			//jumpbounce
			if (transform.position.y <= -300 && !falling && !blocking && !piledrive && state != State.Explosion)
			{
				gameObject.rigidbody.velocity = new Vector3(0, jumpbounce, 0);	
				Resetheavies();
				doingheavy = false;
				airtime = 0;
			}
			
			//piledrive jb
			if (transform.position.y <= -240 && piledrive)
			{
				gameObject.rigidbody.velocity = new Vector3(-400, 200, 0);
				piledrive = false;
			}
			
			//blocking jb
			if (transform.position.y <= -300 && !falling && blocking && !piledrive)
			{
				gameObject.rigidbody.velocity = new Vector3(0, 950, 0);	
			}
			
			
			
			//gravity
			if (transform.position.y > -300 && !rightkick && !leftkick && !additionalairtime && !grabbing && !caughtone && !tackleon && !forceon && state != State.Explosion)
				gameObject.rigidbody.velocity += new Vector3(0, gravity, 0);
			
			//up dash
			if (Input.GetButtonDown("Ascent") && !stunned && !doingheavy && !junklin && !caughtone && !groundsmash && !shining && !hurricane && !shootingbeam ||
				Input.GetAxis("Vertical") > 0 && !stunned && !doingheavy && !junklin && !caughtone && !groundsmash && !shining && !hurricane && !shootingbeam)
			{
				
				CancelAllStates();
				karateanim.Play("Doublejump");
				if (transform.position.y < 500)
				rigidbody.velocity = new Vector3(0, 680, 0);
			}
			
			//down dash
			if (Input.GetButtonDown("Descent") && !stunned && !caughtone && !junklin && !obox.heavyon && !shining && !hurricane && !shootingbeam ||
				Input.GetAxis("Vertical") < 0 && !stunned && !doingheavy && !junklin && !caughtone && !groundsmash && !shining && !hurricane && !shootingbeam)
			{
				CancelAllStates();
				karateanim.Play("Baby");
				if (transform.position.y > -325)
				rigidbody.velocity = new Vector3(0, -1600, 0);
			}
			
			//DBZ mode---------------------------------------------------
				if (dbzmode && !stunned && !falling && !guardbreak)
				{
					dbzmodetimer += Time.fixedDeltaTime;
					Vector3 tempstore = rigidbody.velocity;
					PlayerSpeed = 170;
					rigidbody.velocity = new Vector3(0, -6.8f, 0);
					gravity = -6.8f;
				
					
					if (dbzmodetimer - Time.fixedDeltaTime > 0.5f && !shining)
					{
						CancelDBZ(tempstore);
					}
			
				}
			
				//lost in space
				if (transform.position.y >= 550)
				{
					gameObject.rigidbody.velocity -= new Vector3(0, 340, 0);
					junklin = false;
					obox.heavyon = false;
					doingheavy = false;
				}
			
		}//!paused
	}
	

	void Update () {
		
		if (!pausescript.pausemenud || !pausescript.paused)
		{
			
		//death anim and time states	
		if (state == State.Explosion)
		{
			deathtimer += 0.1f;	
			//deathanim, blood and movement	
			if (transform.position.y > -320)
			{
				karateanim.Play("Death");
				blooddrops();
				gameObject.rigidbody.velocity += new Vector3(0, -20, 0);	
					
				//timescale changing upon descent
				if (lives < 0)
				{
					if (deathtimer > 0 && deathtimer < 4)
					Time.timeScale = 0.2f;
					
					if (deathtimer >= 4 && deathtimer < 8)
					Time.timeScale = 0.3f;
						
					if (deathtimer >= 8 && deathtimer < 10)
					Time.timeScale = 0.4f;
						
					if (deathtimer >= 10)
					Time.timeScale = 0.8f;
				}
			}
				
				
			//deathbounce	
			if (transform.position.y <= -320)
			{
				if (bounceupondeath < 3 && rigidbody.velocity.y < 0)
				{
					audio.PlayOneShot(deathbounce);
					gameObject.rigidbody.velocity = new Vector3(Random.Range(200, 300), Random.Range(700, 800), 0);		
					bounceupondeath++;
				}
				
				//finisher	
				if (bounceupondeath >= 2)
				{
					karateanim.Play("Deathfloor");
					transform.position = new Vector3(transform.position.x, -320, transform.position.z);
					rigidbody.velocity = Vector3.zero;
					if (Time.timeScale < 1)
					Time.timeScale += 0.1f;
				}
			}
				
		}
		
		//whenever you're not dying
		if (state != State.Explosion && !endtime)
			{	
				
				//variables--------------------------------------------------------
				timenow += Time.deltaTime;
				randomdelay = Random.Range(5, 9);
				

				if (!speedbuff && !hurricane && !Input.GetKey(KeyCode.LeftShift) && !dbzmode)
					PlayerSpeed = 442;
				
				if (hurricane)
					PlayerSpeed = 800;
				
				//for xbox versions of movement
				float amtToMove = 0;
				if (Input.GetAxisRaw("Xbox_horizontal") != 0)
					amtToMove = Input.GetAxisRaw("Xbox_horizontal") * PlayerSpeed * Time.deltaTime;
				if (Input.GetAxisRaw("Horizontal") != 0)
					amtToMove = Input.GetAxisRaw("Horizontal") * PlayerSpeed * Time.deltaTime;
				
				//airtime calc
				if (transform.position.y > -290)
					airtime += Time.deltaTime;
				
				
				//some mathemathael bullshit for health and bars UI
				if (redhealth > 10)
					redhealth = 10;
				if (yellowhealth > 10)
					yellowhealth = 10;
				if (yellowhealth < 0)
					yellowhealth = 0;
				if (greenhealth > 10)
					greenhealth = 10;
				if (balls > maxballs)
					balls = maxballs;
				
				//small bar increments
				ballsbar5 = balls - 4;
				if (ballsbar5 > 1)
					ballsbar5 = 1;
				if (ballsbar5 < 0)
					ballsbar5 = 0;
				ballsbar4 = balls - 3;
				if (ballsbar4 > 1)
					ballsbar4 = 1;
				if (ballsbar4 < 0)
					ballsbar4 = 0;
				ballsbar3 = balls - 2;
				if (ballsbar3 > 1)
					ballsbar3 = 1;
				if (ballsbar3 < 0)
					ballsbar3 = 0;
				ballsbar2 = balls - 1;
				if (ballsbar2 > 1)
					ballsbar2 = 1;
				if (ballsbar2 < 0)
					ballsbar2 = 0;
				ballsbar1 = balls - 0;
				if (ballsbar1 > 1)
					ballsbar1 = 1;
				if (ballsbar1 < 0)
					ballsbar1 = 0;
				
				#region Animutions
				if (!hurricane && !groundsmash && !rightkick && !leftkick && !stunned && !falling && !guardbreak && !tackleon && !forceon && !shining && !shootingbeam)
				{
					//regular LMB
					if  (Input.GetButtonDown("fight") || Input.GetKeyDown(KeyCode.LeftArrow)) 
					{
						if (Usemousecontrols)
						{
							if (MousetotheRight())
							{
								if (!facingright)
								{
									karatesprite.HFlip();
									facingright = true;
								}
							}
							if (!MousetotheRight())
							{
								if (facingright)
								{
									karatesprite.HFlip();
									facingright = false;
								}
							}
						}
						
						//wii-style
						if (!Usemousecontrols)
						{
							if (lastkeyboardinput == "right")
							{
								if (!facingright)
								{
									karatesprite.HFlip();
									facingright = true;
								}
							}
							if (lastkeyboardinput == "left")
							{
								if (facingright)
								{
									karatesprite.HFlip();
									facingright = false;
								}
							}
						}
					
					//LMB animations
					if (atkready)
					{	
							if (!blocking && !hurricane && !groundsmash && !rightkick && !leftkick && !junklin && !gettinghit && !tackleon)
							{
								if (attackcounter >= 3)
								{
//									animating = true;
									karateanim.Play("Afroattack3");
									atktimer = true;
									atkreset = 0;
									spacebar++;
									atkready = false;
									attackcounter = 0;
								}
								if (attackcounter == 2)
								{
//									animating = true;
									karateanim.Play("Afroattack2");
									atktimer = true;
									atkreset = 0;
									spacebar++;
									atkready = false;
									attackcounter++;
								}
								if (attackcounter == 1)
								{
//									animating = true;
									karateanim.Play("Afroattack1");
									atktimer = true;
									atkreset = 0;
									spacebar++;
									atkready = false;
									attackcounter++;
										
								}
								if (attackcounter == 0)
								{
									attackcounter++;	
										
								}
							}
					}
				}
				
					
			
		//heavy attack animutions
		//removed !DBZMODE from here
		if (obox.heavyon && !blocking && !stunned && !guardbreak && !dashing && !tackleon && !forceon && !shining)
		{
			doingheavy = true;
						
			//ADDED THIS HEAVY COUNTER THING IN TO PREVENT ANIMS EXECUTING SEVERAL TIMES
			// BUT THEY STILL DO :(
			
			if (!dynastystyle)
			{		
				//pressing down Axekick
				if (Input.GetAxis("Vertical") < 0 && heavycounter > 0 && obox.axekickready ||
					Input.GetButton("Descent") && heavycounter > 0 && obox.axekickready)
				{
					if (Usemousecontrols)
					{
						if (MousetotheRight())
						{
							if(!facingright)
							{
								karatesprite.HFlip();
								facingright = true;
							}
							if (linkwindow && !animating)
								quickheavydown();
							if (!linkwindow && !animating)
								HeavyDown();
						}
						
						if (!MousetotheRight())
						{
							if(facingright)
							{
								karatesprite.HFlip();
								facingright = false;
							}
							if (linkwindow && !animating)
								quickheavydown();
							if (!linkwindow && !animating)
								HeavyDown();
						}	
					}
				
					if (!Usemousecontrols)
					{
						if (lastkeyboardinput == "right")
						{
							if(!facingright)
							{
								karatesprite.HFlip();
								facingright = true;
							}
							if (linkwindow && !animating)
								quickheavydown();
							if (!linkwindow && !animating)
								HeavyDown();
						}	
						if (lastkeyboardinput == "left")
						{
							if(facingright)
							{
								karatesprite.HFlip();
								facingright = false;
							}
							if (linkwindow && !animating)
								quickheavydown();
							if (!linkwindow && !animating)
								HeavyDown();
							
						}
						
					}
								
					
				}//end axekick
						
						
				//pressing left Dkick
				if (Input.GetAxis("Horizontal") < 0 && heavycounter > 0 && obox.Dkickready ||
					Input.GetAxis("Xbox_horizontal") < 0 && heavycounter > 0 && obox.Dkickready)
				{
					if (facingright)
					{
						karatesprite.HFlip();	
						facingright = false;
					}
					gravity = 0;
					if (linkwindow && !leftkick)
						quickleft();
					if (!linkwindow && !animating && !leftkick)
						StartCoroutine ( heavyleft () );
				}
				//pressing right Dkick
				if (Input.GetAxis("Horizontal") > 0 && heavycounter > 0 && obox.Dkickready ||
					Input.GetAxis("Xbox_horizontal") > 0 && heavycounter > 0 && obox.Dkickready)
				{
					if (!facingright)
					{
						karatesprite.HFlip();	
						facingright = true;
					}
					gravity = 0;
					if (linkwindow && !rightkick)
						quickright();
					if (!linkwindow && !animating && !rightkick)
						StartCoroutine ( heavyright () );
				}
					
				//junklin
				if (Input.GetAxis("Vertical") > 0 && heavycounter > 0 && obox.junklinready ||
					Input.GetButton("Ascent") && heavycounter > 0 && obox.junklinready)
				{
					rigidbody.velocity = Vector3.zero;
					if (linkwindow && !junklin)
						quickheavyup();
					if (!linkwindow && !animating)
						StartCoroutine ( heavyup() );
				}	
						
			}//!dynasty
						
			if (dynastystyle)
			{
				//shoryu
				if (attackcounter == 2)
				{
					//junklin
					if (heavycounter > 0 && obox.junklinready)
					{
						rigidbody.velocity = Vector3.zero;
						if (linkwindow && !junklin)
							quickheavyup();
						if (!linkwindow && !animating)
							StartCoroutine ( heavyup() );
					}		
					
					StartCoroutine ( resetdoingheavywithtime (0.5f) );
					obox.junklinready = false;
					return;
				}
				
				//dkick
				if (attackcounter == 3)
				{
					if (heavycounter > 0 && obox.Dkickready)
					{
						//mouse rightkick
						if (Usemousecontrols)
						{
							if (MousetotheRight())
							{
								if(!facingright)
								{
									karatesprite.HFlip();
									facingright = true;
								}
								if (linkwindow && !animating)
									quickright();
								if (!linkwindow && !animating)
									heavyright();
							}
							//mouse leftkick
							if (!MousetotheRight())
							{
								if(facingright)
								{
									karatesprite.HFlip();
									facingright = false;
								}
								if (linkwindow && !animating)
									quickleft();
								if (!linkwindow && !animating)
									heavyleft();
							}	
						}
				
						if (!Usemousecontrols)
						{
							//!mouse rightkick
							if (lastkeyboardinput == "right")
							{
								if(!facingright)
								{
									karatesprite.HFlip();
									facingright = true;
								}
								if (linkwindow && !animating)
									quickright();
								if (!linkwindow && !animating)
									heavyright();
							}	
							//!mouse leftkick
							if (lastkeyboardinput == "left")
							{
								if(facingright)
								{
									karatesprite.HFlip();
									facingright = false;
								}
								if (linkwindow && !animating)
									quickleft();
								if (!linkwindow && !animating)
									heavyleft();
								
							}
						
						}			
						StartCoroutine ( resetdoingheavywithtime (0.5f) );		
					}
					return;
				}
							
				//axekick		
				if (attackcounter == 1 && linkwindow)
				{
					if (heavycounter > 0 && obox.axekickready)
					{
						if (Usemousecontrols)
						{
							if (MousetotheRight())
							{
								if(!facingright)
								{
									karatesprite.HFlip();
									facingright = true;
								}
								if (linkwindow && !animating)
									quickheavydown();
								if (!linkwindow && !animating)
									HeavyDown();
							}
						
							if (!MousetotheRight())
							{
								if(facingright)
								{
									karatesprite.HFlip();
									facingright = false;
								}
								if (linkwindow && !animating)
									quickheavydown();
								if (!linkwindow && !animating)
									HeavyDown();
							}	
						}
				
						if (!Usemousecontrols)
						{
							if (lastkeyboardinput == "right")
							{
								if(!facingright)
								{
									karatesprite.HFlip();
									facingright = true;
								}
								if (linkwindow && !animating)
									quickheavydown();
								if (!linkwindow && !animating)
									HeavyDown();
							}
										
							if (lastkeyboardinput == "left")
							{
								if(facingright)
								{
									karatesprite.HFlip();
									facingright = false;
								}
								if (linkwindow && !animating)
									quickheavydown();
								if (!linkwindow && !animating)
									HeavyDown();
							
							}
						
						}
									
						StartCoroutine ( resetdoingheavywithtime (0.5f) );;			
					}//end axekick			
								
								
				}			
							
			}//end dynasty
			
			//make sure he doesnt do multiple anims by making him constipate
			if (karateanim.GetCurrentAnimation() == null)
			{
				karateanim.Play("constipate");
			}
						
		}//end obox
					
		
					
		//baby anims
		if (Input.GetButtonDown("left") && !blocking && atkready && !dashing && !hurricane && !groundsmash && !stunned && !falling && !doingheavy && !animating && !tackleon && !shining||
			Input.GetButtonDown("right") && !blocking && atkready && !dashing && !hurricane && !groundsmash && !stunned && !falling && !doingheavy && !animating && !tackleon && !shining)
			karateanim.Play("Baby");
					
			if (karateanim.IsPlaying("Baby"))
			{
				if (Input.GetButtonUp("left") || Input.GetButtonUp("right"))
				karateanim.Stop();
			}
					
			//DEFAULT ANIMATIONS
			DefaultAnimation();
					
					
		}//end of animution conditionals
				
				//dkick stuff
				if (rightkick && !falling)
				{
					if (!facingright)
							{
								karatesprite.HFlip();	
								facingright = true;
							}
					
					StartCoroutine ( DragonDash (new Vector3 (114, 34, 0), 0.2f) );
					
					StartCoroutine ( Cancelkicks () );
				}
					
				if (leftkick && !falling)
				{
					if (facingright)
							{
								karatesprite.HFlip();	
								facingright = false;
							}
					
					StartCoroutine ( DragonDash (new Vector3 (-114, 34, 0), 0.2f) );
					
					StartCoroutine ( Cancelkicks () );
				}
				
				//bump for axekick
				if (bump && !falling)
				{
					bumptimer += Time.deltaTime;
					
					if (Usemousecontrols)
					{
						if (MousetotheRight()) 
						transform.position += new Vector3(68, 136, 0) * Time.deltaTime;
						if (!MousetotheRight())
						transform.position += new Vector3(-68, 136, 0) * Time.deltaTime;
					}
					
					if (!Usemousecontrols)
					{
						if (lastkeyboardinput == "right") 
						transform.position += new Vector3(68, 136, 0) * Time.deltaTime;
						if (lastkeyboardinput == "left")
						transform.position += new Vector3(-68, 136, 0) * Time.deltaTime;
					}
					
					if (bumptimer - Time.deltaTime > 0.5)
					{
						bump = false;
						bumptimer = 0;
					}
				}
				
				//dashing state
				if (dashing && !stunned && !falling && !guardbreak)
				{
					
					if (!hurricane)
					{
						animating = true;
						karateanim.Play("Doubledash");	
					}
//					if (obox.heavyon || hurricane)
//					karateanim.Play("Dashcancel");
					gravity = -0.8f;
					dashCD += Time.deltaTime;
					if (dashCD - Time.deltaTime > 0.3f)
					{
						dashing = false;
						dashCD = 0;
						rigidbody.velocity = Vector3.zero;
					}
					
				}
				
				//piledriver anims
				if (caughtone)
				{
					if (transform.position.y >= 200)
					karateanim.Play("Grabbed1");
					
					if (transform.position.y < 200 && transform.position.y > -100)
					karateanim.Play("Grabbed2");
					
					if (transform.position.y <= -100)
					karateanim.Play("Grabbed3");
						
				}
				
				//regular gravity
				if (!dashing && !dbzmode && !animating && !additionalairtime && !blocking && !piledrive)
				gravity = regulargrav;
				
				//regular rotation
				if (transform.rotation != basic && !stunned)
					transform.rotation = basic;
				
				
				#endregion
				
					
				
				if (targetdash)
				{
					Vector3 targetpos = currenttarget.transform.position;
					StartCoroutine ( DragonDash ((targetpos - transform.position) * 1.5f, 0.2f) );
				}
				
				//some resets
				if (junklin)
				{
					junkletimer += Time.deltaTime;
					
					if (junkletimer >= 1)
					{
						junklin = false;
						animating = false;
						junkletimer = 0;
					}
				}
				
				//combo link or not
				if (linkwindow)
				{
					linktimer+= Time.deltaTime;
					if (linktimer - Time.deltaTime > 1.5f)
					{
						linkwindow = false;
						linktimer = 0f;
					}
				}
				
				//additional air time from random stuff
				if (additionalairtime)
				{
					gravity = -6.8f;
//					rigidbody.velocity = Vector3.zero;
					airtimer += Time.deltaTime;
					if (airtimer - Time.deltaTime > 1f)
					{
						additionalairtime = false;
						airtimer = 0;
					}
				}
					
				
				#region Restrictions, Reactions & states
				//screen wrap
				if (transform.position.x <= -645)
					transform.position = new Vector3(632, transform.position.y, transform.position.z);
				else if (transform.position.x >= 633)
					transform.position = new Vector3(-644, transform.position.y, transform.position.z);
				
				//regular facing
				if (!karateanim.IsPlaying("Special4") && !tackleon && !doingheavy)
				{
					if (Usemousecontrols)
					{
						if (MousetotheRight() && !rightkick && !leftkick && !junklin && !tackleon)
							{
								if (!facingright)
								{
									karatesprite.HFlip();
									facingright = true;
								}
							}
						if (!MousetotheRight() && !rightkick && !leftkick && !junklin && !tackleon)
						{
							if (facingright)
							{
								karatesprite.HFlip();
								facingright = false;
							}
						}
					}
					
					if (!Usemousecontrols)
					{
						if (lastkeyboardinput == "right" && !rightkick && !leftkick && !junklin && !tackleon)
							{
								if (!facingright)
								{
									karatesprite.HFlip();
									facingright = true;
								}
							}
						if (lastkeyboardinput == "left" && !rightkick && !leftkick && !junklin && !tackleon)
						{
							if (facingright)
							{
								karatesprite.HFlip();
								facingright = false;
							}
						}
						
						
					}
				}
				
				
				//fuck 3d yo
				if (transform.position.z != -100 && !hitbox.displaced && !shining)
					transform.position = new Vector3(transform.position.x, transform.position.y, -100);
				
				
				
				//gsmashed
				if (transform.position.y >= -290 && falling)
				{
					fallingx = rigidbody.velocity.x;
				}
				
				//bounce after gsmashed
				if (transform.position.y <= -290 && falling)
				{
					falling = false;
					stunned = true;
					audio.PlayOneShot(boom);
					Instantiate(groundbreak2, transform.position, transform.rotation);
//					gameObject.rigidbody.velocity = new Vector3(-1900, jumpbounce * 0.85f, 0);
				}
				
			
				
				//itembounce
				if (PlayerPrefs.GetString("GroundsmashT1") == "white" && groundsmash && transform.position.y <= -300)
				{
					itembounce = true;	
				}
				
				//mouse rotation
				if (mouserotating)
				{
					rotatetimer += Time.deltaTime;
					//old mouserotate stuff
					cameraDif = Camera.main.transform.position.y - transform.position.y;
					mouseX = Input.mousePosition.x;
					mouseY = Input.mousePosition.y;
					mWorldPos = Camera.main.ScreenToWorldPoint( new Vector3(mouseX, mouseY, cameraDif));
					
					diffX = mWorldPos.x - transform.position.x;
				    diffY = mWorldPos.y  - transform.position.y;
			
					float angle = Mathf.Atan2(diffY, diffX) * Mathf.Rad2Deg;
					
					
					if (MousetotheRight())
		    		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
					if (!MousetotheRight())
		    		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 180));
					
					if (rotatetimer >= 3)
					{
						mouserotating = false;	
						rotatetimer = 0;
					}
					
					if (rotatetimer >= 1 && shortrotate)
					{
						mouserotating = false;	
						rotatetimer = 0;
					}
					
				}
				
				//stunned state
				if (stunned)
				{
					gameObject.rigidbody.velocity = new Vector3(fallingx * 0.15f, jumpbounce * 0.2f, 0);
					stuntimer += Time.deltaTime;
					rotationspeed += Time.deltaTime;
					transform.Rotate(new Vector3(0,0,-1) * rotationspeed);
					karateanim.Play("Damage");
					if (stuntimer - Time.deltaTime >= 2)
					{
						stunned = false;	
						stuntimer = 0;
						rotationspeed = 16;
						transform.rotation = basic;
						rigidbody.velocity = Vector3.zero;
					}
				}
				
				//GB state
				if (guardbreak)
				{
					blockbar = 0.1f;
					stuntimer += Time.deltaTime;
					rigidbody.velocity = Vector3.zero;
					karateanim.Play("Damage");
					if (stuntimer - Time.deltaTime >= 1.5f)
					{
						stunned = false;	
						stuntimer = 0;
						guardbreak = false;
					}
				}
				
				
				#endregion
			
				
				#region Player Inputs
				
				//dash for xbox controls
				if (Input.GetAxis("Xbox_horizontal") == 0)
					dashticket = true;
				
				
		//movement-----------------------------------------------------------------------------------------
				
			if (!obox.heavyon && !stunned && !guardbreak && !forceon && !shining)
			{
					//general movement left
					if (Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Xbox_horizontal") < 0)
					{		
						if (!obox.staticheavystartup && !rightkick && !leftkick && !stunned && !falling && !shining)
						{
							transform.Translate(Vector3.right * amtToMove, Space.World);
							if (facingright && !karateanim.IsPlaying("Special4"))
								{
									karatesprite.HFlip();
									facingright = false;
								}
						}
						moving = true;
						lastkeyboardinput = "left";
					}
					
				//left dash		
				if (Input.GetButtonDown("left") || Input.GetAxis("Xbox_horizontal") < 0 && dashticket && !axisdelay)
					{		
						axisdelay = true;
						leftdashbool = true;
						rightdashbool = false;
						dashticket = false;
						counterstrike ++;
						
						//double click left
						if (Input.GetButtonDown("left") && leftdashtimer > 0.05f && leftdashtimer < 0.2f &&
							moving && !grabbing && !groundsmash && !caughtone && !blocking && !doingheavy && dashready)
							{
								dashcombocount = (int)hits;
								rightdashbool = false;
								dashing = true;
								if (facingright)
								{
									karatesprite.HFlip();
									facingright = false;
								}
								StartCoroutine ( Dashparticles ("left") );
								StartCoroutine ( DragonDash (new Vector3(-340, 34, 0), 0.2f) );
								leftdashtimer = 0f;
								dashready = false;
							}
						lastkeyboardinput = "left";
					}
					
					 
					//general movement right
					if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Xbox_horizontal") > 0)
					{		
						if (!obox.staticheavystartup && !rightkick && !leftkick && !stunned && !falling && !shining)
						{
							transform.Translate(Vector3.right * amtToMove, Space.World);
							if (!facingright && !karateanim.IsPlaying("Special4"))
								{
									karatesprite.HFlip();
									facingright = true;
								}
						}
						moving = true;
						lastkeyboardinput = "right";
					}
					
				//right dash	
				if (Input.GetButtonDown("right") || Input.GetAxis("Xbox_horizontal") > 0 && dashticket && !axisdelay)
					{		
						rightdashbool = true;
						leftdashbool = false;
						
						//double click right
						if (Input.GetButtonDown("right") && rightdashtimer > 0.05f && rightdashtimer < 0.2f &&
							moving && !grabbing && !groundsmash && !caughtone && !blocking && !doingheavy && dashready)
							{
								dashcombocount = (int)hits;
								leftdashbool = false;
								dashing = true;
								if (!facingright)
								{
									karatesprite.HFlip();
									facingright = true;
								}
								StartCoroutine ( DragonDash (new Vector3(340, 34, 0), 0.2f) );
								StartCoroutine ( Dashparticles ("right") );
								rightdashtimer = 0f;
								dashready = false;
							}
						lastkeyboardinput = "right";
					}
					
					
			
			}//end movement conditionals
				
			//some movement resets	
			if (leftdashbool)
			{
				rightdashbool = false;
				leftdashtimer += Time.deltaTime;
				if (leftdashtimer > 0.25f)
				{
					leftdashbool = false;	
					leftdashtimer = 0;
				}
				
			}	
				
			if (rightdashbool)
			{
				leftdashbool = false;
				rightdashtimer += Time.deltaTime;
				if (rightdashtimer > 0.25f)
				{
					rightdashbool = false;	
					rightdashtimer = 0;
				}
				
			}
		
				
		//Ability cast conditionals		
		if (!stunned && !rightkick && !leftkick && !grabbing && !groundsmash && !tackleon && !caughtone && !guardbreak && !blocking && !forceon && !shining)
				{
			//Slot 1 ability cast--------------------------------------------
					if (Input.GetButtonDown("ability1") || Input.GetAxis ("Xbox_triggers") < 0)
					{
						QuerySlot("Slot1");
					}
	
					
			//Slot 2 ability cast------------------------------------------------------
					if (Input.GetButtonDown("ability2") || Input.GetAxis ("Xbox_triggers") > 0)
					{
						QuerySlot("Slot2");
					}
	
					
			//Slot 3 ability cast---------------------------------
					if (Input.GetButtonDown("ability3") || Input.GetKeyDown(KeyCode.F))
					{
						QuerySlot("Slot3");
					}
				
		
			//Slot 4 ability cast-------------------------------------------------------
					if (Input.GetButtonDown("ability4") || Input.GetKeyDown(KeyCode.C))
					{
						QuerySlot("Slot4");
					}
					
			//Slot 5 special strike animations-------------------------------------------
					if (Input.GetButtonDown("fight3") && PlayerPrefs.GetInt("Sstrikeabilitystate") > 0)
					{
						if (balls >= 0.5f)
						{
							if (Usemousecontrols)
							{
								if (MousetotheRight())
								{
									if (!facingright)
									{
										karatesprite.HFlip();
										facingright = true;
									}
								}
								if (!MousetotheRight())
								{
									if (facingright)
									{
										karatesprite.HFlip();
										facingright = false;
									}
								}
							}
							if (!Usemousecontrols)
							{
								if (lastkeyboardinput == "right")
								{
									if (!facingright)
									{
										karatesprite.HFlip();
										facingright = true;
									}
								}
								if (lastkeyboardinput == "left")
								{
									if (facingright)
									{
										karatesprite.HFlip();
										facingright = false;
									}
								}
							}
							
							if (PlayerPrefs.GetString("Sstrikerune") != "blue")
							{
								animating = true;
								karateanim.Play("Special1");
								specialready = false;
							}
							if (PlayerPrefs.GetString("Sstrikerune") == "blue")
							{
									if (hadocounter >= 2)
									{
										animating = true;
										karateanim.Play("Special2");
										hadocounter = 0;
									}
									if (hadocounter == 1)
									{
										animating = true;
										karateanim.Play("Special3");
										hadocounter++;	
									}
									if (hadocounter == 0)
										hadocounter++;
						
								specialready = false;
	//							animating = true;
								mouserotating = true;
								shortrotate = true;
							}
							balls -= 0.5f;
						}
					}
					
				}//END ability cast conditionals
				
				
				//Blocking
				if (Input.GetButton("block") && !stunned && !guardbreak && !rightkick && !leftkick && !doingheavy && !forceon)
				{
					showblockbar = true;
						
					if (Usemousecontrols)
					{
						if (MousetotheRight())
						{
							if (!facingright)
							{
								karatesprite.HFlip();
								facingright = true;
							}
						}
						if (!MousetotheRight())
						{
							if (facingright)
							{
								karatesprite.HFlip();
								facingright = false;
							}
						}
					}
					if (!Usemousecontrols)
					{
						if (lastkeyboardinput == "right")
						{
							if (!facingright)
							{
								karatesprite.HFlip();
								facingright = true;
							}
						}
						if (lastkeyboardinput == "left")
						{
							if (facingright)
							{
								karatesprite.HFlip();
								facingright = false;
							}
						}
					}
					
					blocking = true;
					(gameObject.collider as SphereCollider).radius = 60;
					PlayerSpeed = 221;
					gravity = -20;
					jumpbounce = 650;
//					if (!parrying)
					karateanim.Play("Block");	
				}
				else
				{
					blocking = false;
					(gameObject.collider as SphereCollider).radius = 15;
					PlayerSpeed = 442;
					gravity = regulargrav;
					jumpbounce = regularjb;
				}
				
				if (Input.GetButtonUp("block"))
				{
					showblockbar = false;
					perfectguard = false;
					perfectgtimer = 0;	
				}
				if (Input.GetButtonDown("block"))
					perfectguard = true;
				
				
				
				#endregion
				
				
			}//end state 
			
			//UI Stuff---------------------------------------------------------
			if (Input.GetKeyDown(KeyCode.Pause))
				devshitison = !devshitison;
			
			//shaking
			if (HPshake)
			{
				float duration = 0.05f;
   				float lerp = Mathf.PingPong (Time.time, duration) / duration;
 				HPoffset = Mathf.Lerp(-5, 5, lerp);
				HPoffset2 = Mathf.Lerp(-5, 5, lerp);
			}
			if (ballshake)
			{
				float duration = 0.1f;
   				float lerp = Mathf.PingPong (Time.time, duration) / duration;
 				ballsoffset = Mathf.Lerp(-2, 2, lerp);
			}
			if (blockshake)
			{
				float duration = 0.1f;
   				float lerp = Mathf.PingPong (Time.time, duration) / duration;
 				blockoffset = Mathf.Lerp(-5, 5, lerp);
				blockoffset2 = Mathf.Lerp(-3, 3, lerp);
			}
			
					
			//cooldowns & switches------------------------------------------------
			#region Turning off anims
			if (animating)
			{
				animationtimer += Time.deltaTime;
				
				
				//each reset is for the named anims. You can tell by the names.
				if (karateanim.GetCurrentAnimation() != null && karateanim.GetCurrentAnimation().name == "Block")
				{
					if (Input.GetButtonUp("block"))
					{
						animating = false;
						animationtimer = 0;
					}
					
				} 
				
				
				if (karateanim.GetCurrentAnimation() != null && karateanim.GetCurrentAnimation().name == "Afroattack1")
				{
					if (animationtimer >= 0.3f)
					{
						animating = false;
						animationtimer = 0;
					}
					
				}
				
				if (karateanim.GetCurrentAnimation() != null && karateanim.GetCurrentAnimation().name == "Afroattack2")
				{
					if (animationtimer >= 0.35f)
					{
						animating = false;
						animationtimer = 0;
					}
					
				}
				
				if (karateanim.GetCurrentAnimation() != null && karateanim.GetCurrentAnimation().name == "Afroattack3")
				{
					if (animationtimer >= 0.33f)
					{
						animating = false;
						animationtimer = 0;
					}
					
				}
				
				if (karateanim.IsPlaying("Newtornado"))
				{
					if (animationtimer >= 1.25f)
					{
						animating = false;
						animationtimer = 0;
					}
					
				}
				
				if (karateanim.IsPlaying("Damage"))
				{
					if (animationtimer >= 0.7f)
					{
						animating = false;
						animationtimer = 0;
					}
					
				}
				
				if (karateanim.IsPlaying("Special1"))
				{
					if (animationtimer >= 0.45f)
					{
						animating = false;
						animationtimer = 0;
					}
					
				}
				
				if (karateanim.IsPlaying("Special2") || karateanim.IsPlaying("Special3"))
				{
					if (animationtimer >= 0.4f)
					{
						animating = false;
						animationtimer = 0;
					}
					
				}
				
				if (karateanim.GetCurrentAnimation() != null && karateanim.GetCurrentAnimation().name == "Doubledash")
				{
					if (animationtimer >= 0.1f)
					{
						animating = false;
						animationtimer = 0;
					}
					
				}
				
				if (karateanim.IsPlaying("Axekick"))
				{
					if (animationtimer >= 0.8f)
					{
						animating = false;
						animationtimer = 0;
					}
					
				}
				
				if (karateanim.IsPlaying("Quickaxe"))
				{
					if (animationtimer >= 0.2f)
					{
						animating = false;
						animationtimer = 0;
					}
					
				}
				
				if (karateanim.IsPlaying("Dragonkick"))
				{
					if (animationtimer >= 1f)
					{
						animating = false;
						animationtimer = 0;
					}
					
				}
				
				if (karateanim.IsPlaying("Quickdkick"))
				{
					if (animationtimer >= 0.6f)
					{
						animating = false;
						animationtimer = 0;
					}
					
				}
				
				if (karateanim.IsPlaying("Junklinlong"))
				{
					if (animationtimer >= 1.1f)
					{
						animating = false;
						animationtimer = 0;
					}
					
				}
				
				if (karateanim.IsPlaying("Junklinshort"))
				{
					if (animationtimer >= 1.2f)
					{
						animating = false;
						animationtimer = 0;
					}
					
				}
				
				if (karateanim.IsPlaying("Baby"))
				{
					if (animationtimer >= 1f)
					{
						animating = false;
						animationtimer = 0;
						karateanim.Stop();
					}
					
				}
				
//				if (karateanim.IsPlaying("constipate"))
//				{
//					if (animationtimer > 0.7f)
//					{
//						animating = false;
//						animationtimer = 0;
//						karateanim.Stop();
//					}
//					
//				}
				
//				if (karateanim.IsPlaying("Parry"))
//				{		
//					
//					
//				}
				
						
				
			}
			#endregion
			
			//GENERAL RESETS AND COOLDOWNS
			
			if (doingheavy)
			{
				heavytimer += Time.deltaTime;
				if (obox.direction == 1)
				{
					if (heavytimer >= 1.2f)
					{
						doingheavy = false;	
						heavytimer = 0;
						return;
					}
					
				}
				if (obox.direction == 3 || obox.direction == 4)
				{
					if (heavytimer >= 1.2f)
					{
						doingheavy = false;
						heavytimer = 0;
						return;
					}
					
				}
				
				if (obox.direction == 2)
				{
					if (heavytimer >= 0.7f)
					{
						doingheavy = false;
						heavytimer = 0;
						return;
					}
					
				}
				
				
			}
			
			if (blockbar < maxblockbar)
			{
				blockbarregen += Time.deltaTime;
				if (blockbarregen >= 0.5f)
				{
					if (blockbar < maxblockbar / 2)
					blockbar += 0.2f;
					else 
					blockbar += 0.1f;
					blockbarregen = 0;
				}
				
			}
			
			if (blockbar <= 0)
			{
				audio.PlayOneShot(breaksound);
				blocking = false;
				guardbreak = true;
			}
			
			if (atktimer)
			{
				atkreset += Time.deltaTime;
				
				if (atkreset - Time.deltaTime > 0.8f)
				{
					atktimer = false;
					attackcounter = 1;
					atkreset = 0;
					atkCD = 0;
					hitbox.dmgCD = 0;
				}
				
			}
			
			if (hits - dashcombocount == 3)
			{	
				dashcombocount = 0;
				if (!dashready)
				{
					dashready = true;	
					dashcooldown = 0;
				}
				
			}
		
			
			//Delay after LAST attack
			if (!atkready && attackcounter == 1)
			{
				atkCD += Time.deltaTime;
				//working one is .23
			if (atkCD >= 0.23f)
				{
					atkready = true;
					atkCD = 0;
				}
			}
			
			//Delay after FIRST attack
			if (!atkready && attackcounter == 2)
			{
				atkCD += Time.deltaTime;
				//working one is .2
			if (atkCD >= 0.2f)
				{
					atkready = true;
					atkCD = 0;
				}
			}
			
			//Delay after SECOND attack
			if (!atkready && attackcounter == 3)
			{
				atkCD += Time.deltaTime;
				//working one is .25
			if (atkCD >= 0.25f)
				{
					atkready = true;
					atkCD = 0;
				}
			}
			
//			if (!atkready && attackcounter > 3)
//			{
//				atkCD += Time.deltaTime;
//			if (atkCD >= 0.1f)
//				{
//					atkready = true;
//					atkCD = 0;
//				}
//			}
			
			if (attackbuff)
			{
				bufftimer += Time.deltaTime;
				karatesprite.color = Color.red;
				attackdmg = 4;
				if (bufftimer >= 3)
				{
					attackbuff = false;
					attackdmg = 1;
					karatesprite.color = Color.white;
					bufftimer = 0;
				}
			}
			
			if (!gSmashready)
				gSmashCD += Time.deltaTime;
			if (gSmashCD >= gsmashCDbase)
				{
					gSmashready = true;	
					gSmashCD = 0;	
				}
			
			if (!tackleready)
				tackleCD += Time.deltaTime;
			if (tackleCD >= tackleCDbase)
				{
					tackleready = true;	
					tackleCD = 0;	
				}
			
			if (!forceready)
				forceCD += Time.deltaTime;
			if (forceCD >= forceCDbase)
				{
					forceready = true;	
					forceCD = 0;	
				}
			
			if (!hurriready)
				hurriCD += Time.deltaTime;
			if (hurriCD >= HurrCDbase)
				{
					hurriready = true;	
					hurriCD = 0;	
				}
			
			if (!beamready)
				beamCD += Time.deltaTime;
			if (beamCD >= LazerCDreal)
			{
				beamready = true;	
				beamCD = 0;	
			}
			else if (beamCD >= LazerCDbase)
				{
					beamready = true;	
					beamCD = 0;	
				}
			
			if (!barrierready)
				barrierCD += Time.deltaTime;
			if (barrierCD >= serenCDbase)
				{
					barrierready = true;	
					barrierCD = 0;	
				}
			
			if (numberforlinks > 0)
			{
				lunknumberreset += Time.deltaTime;	
				if (lunknumberreset > 1)
				{	
					numberforlinks = 0; 
					lunknumberreset = 0;
				}
				
			}
			
			if (blocking)
			{
				blocktimer += Time.deltaTime;
				perfectgtimer += Time.deltaTime;
				animating = true;
				if (perfectgtimer - Time.deltaTime >= 0.5f)
				{
					perfectguard = false;	
					perfectgtimer = 0;
				}
			}
			
			if (parrying)
			{
				StartCoroutine( parrycolor () );
				parrytimer += Time.deltaTime;
				if (parrytimer >= 0.7f)
				{
					parrying = false;	
					parrytimer = 0;
				}
				
				
			}
			
			if (!dashready)
			{
				dashcooldown += Time.deltaTime;	
				if (dashcooldown > 2)
				{
					dashready = true;	
					dashcooldown = 0;
				}
				
			}
			
			if (!specialready && PlayerPrefs.GetString("Sstrikerune") != "blue")
			{
				specialCD += Time.deltaTime;
				if (specialCD >= 1f)
				{
					specialready = true;	
					specialCD = 0;
					animating = false;
				}
				
			}
			
			if (!specialready && PlayerPrefs.GetString("Sstrikerune") == "blue")
			{
				specialCD += Time.deltaTime;
				if (specialCD >= 5f)
				{
					specialready = true;	
					specialCD = 0;
					animating = false;
				}
				
			}
			
			if (itembounce)
			{
				ibouncereset += Time.deltaTime;
				if (ibouncereset > 0.6f)
				{
					itembounce = false;
					ibouncereset = 0;
				}
				
			}
			
			if (axisdelay)
			{
				axdelaytimer += Time.deltaTime;
				if (axdelaytimer > 0.25f)
				{
					axisdelay = false;
					axdelaytimer = 0;
					counterstrike = 0;
				}
			}
			
			//targetting reset 
			if (currenttarget != null)
			{
				if (currenttarget.tag == "Dead" || currenttarget.transform.position.x > 571
					|| currenttarget.transform.position.x < -624)
					currenttarget = null;
			}
			
			//airtime max setter
			if (airtime > maxairtime)
			{
				maxairtime = airtime;	
			}
			
		}//!paused|end
		
		//devshit
		if (Input.GetKeyDown(KeyCode.Alpha0))
		{
			Debug.Log("Currentlevel" + PlayerPrefs.GetInt("Currentlevel").ToString());
			Debug.Log("Mousecontrols: " + Usemousecontrols.ToString());
			balls = maxballs;
		}
		
		if (Input.GetKeyDown(KeyCode.Delete))
		{
			TranscribeStats();
			Application.LoadLevel("LevelClear");
		}
		
		if (Input.GetKeyDown(KeyCode.G))
			killall();
		
		if (Input.GetKeyDown(KeyCode.Backspace))
		combocheat();
		
		
		
		
	}//update|end
	
	
	
	//POST UPDATE FUNCTIONS 
	

	void Blockeffect()
	{
		Instantiate(blocked, transform.position, transform.rotation);
		blockbar--;
		StartCoroutine ( Toggleblockshake () );
		
	}
	
	//body collision code-----------------------------------------------------------
	void OnTriggerEnter(Collider otherObject)
	{
		if (state != State.Invincible && !gettinghit)
		{
			//wrestler 1
			if (otherObject.tag == "Hardcore" && state == State.Playing && !hurricane && !grabbing)
			{
				Vector3 newvelo = new Vector3(otherObject.rigidbody.velocity.x, -1190, 0);
				EnemyWrestler enemyscript = (EnemyWrestler)otherObject.gameObject.GetComponent("EnemyWrestler");
				if (shielded)
				{
					shieldhit++;
					audio.PlayOneShot(blocksmack);
					return;
				}
				if (blocking)
				{	
					if (perfectguard && Enemyinfront(otherObject.gameObject.transform.position))
							{
								enemyscript.dbzmode = true;
								dbzmode = true;
								linkwindow = true;
								parrying = true;
								audio.PlayOneShot(pguardsound);
								balls++;
								if (GameObject.FindGameObjectWithTag("Tutorial") != null  && PlayerPrefs.GetInt("Currentlevel") == 2)
								{
									Tutorialshit2 tutscript = (Tutorialshit2)GameObject.FindGameObjectWithTag("MainCamera").GetComponent("Tutorialshit2");
									tutscript.parrycount++;
								}
								return;
							}
					Blockeffect();
					CancelDBZ(newvelo);
					return;
				}
				if (!enemyscript.dbzmode && !enemyscript.falling && !enemyscript.otg && !enemyscript.balloon)
				{
					gothit++;
					CancelDBZ(newvelo);
					audio.PlayOneShot(smack1);
					Turntofacedmgsource(otherObject.gameObject.transform.position);
					StartCoroutine( GetHit (otherObject.gameObject.transform.position.x) );
					if (greenhealth > 0)
					greenhealth = 0;
					if (yellowhealth < 10)
					{
						redhealth = yellowhealth;
					}	
				}
				
			}
			
			//wrestler 2
			if (otherObject.tag == "Hardcore2" && state == State.Playing && !hurricane && !grabbing)
			{
				EnemyWrestler2 enemyscript = (EnemyWrestler2)otherObject.gameObject.GetComponent("EnemyWrestler2");
				Vector3 newvelo = new Vector3(otherObject.rigidbody.velocity.x, -1190, 0);
				if (shielded)
				{
					shieldhit++;
					audio.PlayOneShot(blocksmack);
					return;
				}
				if (blocking)
				{	
					if (perfectguard && Enemyinfront(otherObject.gameObject.transform.position))
							{
								enemyscript.dbzmode = true;
								dbzmode = true;
								linkwindow = true;
								parrying = true;
								audio.PlayOneShot(pguardsound);
								balls++;
								
								return;
							}
					Blockeffect();
					CancelDBZ(newvelo);
					return;
				}
				if (!enemyscript.dbzmode && !enemyscript.falling && !enemyscript.otg && !enemyscript.balloon)
				{
					gothit++;
					CancelDBZ(newvelo);
					audio.PlayOneShot(smack1);
					Turntofacedmgsource(otherObject.gameObject.transform.position);
					StartCoroutine( GetHit (otherObject.gameObject.transform.position.x) );
					if (greenhealth > 0)
					greenhealth = 0;
					if (yellowhealth <= 10)
					{
						loseyellowhealth(2);
					}	
					redhealth = yellowhealth;
					if (yellowhealth <= 0)
					{
						StartCoroutine(Death ());	
					}
				}
				
			}
			
			//w3
			if (otherObject.tag == "Hardcore3" && state == State.Playing && !hurricane && !grabbing)
			{
				EnemyWrestler3 enemyscript = (EnemyWrestler3)otherObject.gameObject.GetComponent("EnemyWrestler3");
				Vector3 newvelo = new Vector3(otherObject.rigidbody.velocity.x, -1190, 0);
				if (shielded)
				{
					shieldhit++;
					audio.PlayOneShot(blocksmack);
					return;
				}
				if (blocking)
				{	
					if (perfectguard && Enemyinfront(otherObject.gameObject.transform.position))
							{
								enemyscript.dbzmode = true;
								dbzmode = true;
								linkwindow = true;
								parrying = true;
								audio.PlayOneShot(pguardsound);
								balls++;
								return;
							}
					Blockeffect();
					CancelDBZ(newvelo);
					return;
				}
				if (!enemyscript.dbzmode && !enemyscript.falling && !enemyscript.otg && !enemyscript.balloon)
				{
					gothit++;
					CancelDBZ(newvelo);
					audio.PlayOneShot(smack1);
					Turntofacedmgsource(otherObject.gameObject.transform.position);
					StartCoroutine( GetHit (otherObject.gameObject.transform.position.x) );
					if (greenhealth > 0)
					greenhealth = 0;
					if (yellowhealth <= 10)
					{
						loseyellowhealth(2);
					}	
					redhealth = yellowhealth;
					if (yellowhealth <= 0)
					{
						StartCoroutine(Death ());	
					}
				}
				
			}
			
			//stepping on shits
			if (otherObject.tag == "Ground" && state == State.Playing)
			{
				if (!groundsmash && !hurricane)
				{
					if (shielded)
					{
						shieldhit++;
						audio.PlayOneShot(blocksmack);
						return;
					}
					gothit++;
					gameObject.rigidbody.velocity += new Vector3(otherObject.rigidbody.velocity.x, -1190, 0);
					audio.PlayOneShot(shit);
					StartCoroutine( GetHit (otherObject.gameObject.transform.position.x) );
						loseHealth(2);
				}
			}
			
			//advanced shits
			if (otherObject.tag == "Ground2" && state == State.Playing ||
				otherObject.tag == "Ground3" && state == State.Playing)
			{
				if (!groundsmash && !hurricane)
				{
					if (shielded)
					{
						shieldhit++;
						audio.PlayOneShot(blocksmack);
						return;
					}
					gothit++;
					gameObject.rigidbody.velocity += new Vector3(otherObject.rigidbody.velocity.x, -1190, 0);
					audio.PlayOneShot(shit);
					StartCoroutine( GetHit (otherObject.gameObject.transform.position.x) );
						loseHealth(3);
				}
			}
			
			//Projectiles
			if (otherObject.tag == "Bullet" && state == State.Playing && !hurricane)
			{
					if (shielded)
					{
						shieldhit++;
						audio.PlayOneShot(blocksmack);
						Destroy(otherObject.gameObject);
						return;
					}
					if (blocking)
					{	
						Blockeffect();
						Destroy(otherObject.gameObject);
						if (GameObject.FindGameObjectWithTag("Tutorial") != null  && PlayerPrefs.GetInt("Currentlevel") == 2)
						{
							Tutorialshit2 tutscript = (Tutorialshit2)GameObject.FindGameObjectWithTag("MainCamera").GetComponent("Tutorialshit2");
							tutscript.blockcount++;
						}
						return;
					}
					gothit++;
					audio.PlayOneShot(smack1);
					StartCoroutine( GetHit (otherObject.gameObject.transform.position.x) );
						if (greenhealth > 0)
						{
							greenhealth-=1;
								if (greenhealth < 0)
							{
								greenhealth = 0;		
							}
							return;	
						}
					yellowhealth-=1;
						if (yellowhealth <= 0)
						{
							StartCoroutine(Death ());	
						}
				Destroy(otherObject.gameObject);
				
			}
			
			//Punk1 melee
			if (otherObject.tag == "punkhitbox" && state == State.Playing && !hurricane && !leftkick && !rightkick && !grabbing && !junklin)
			{
				Punk1 enemyscript = (Punk1)otherObject.transform.parent.GetComponent("Punk1");
				hitdelay += Time.deltaTime;
				if (enemyscript.attacking && !enemyscript.getknocked)
				{
					if (shielded)
					{
						shieldhit++;
						audio.PlayOneShot(blocksmack);
						return;
					}
					if (blocking)
					{	
						if (Enemyinfront(otherObject.transform.parent.transform.position))
						{
							if (perfectguard)
							{
								enemyscript.dbzmode = true;
								dbzmode = true;
								linkwindow = true;
								parrying = true;
								audio.PlayOneShot(pguardsound);
								balls++;
								if (GameObject.FindGameObjectWithTag("Tutorial") != null  && PlayerPrefs.GetInt("Currentlevel") == 2)
								{
									Tutorialshit2 tutscript = (Tutorialshit2)GameObject.FindGameObjectWithTag("MainCamera").GetComponent("Tutorialshit2");
									tutscript.parrycount++;
								}
								return;
							}
							
							Blockeffect();
							if (GameObject.FindGameObjectWithTag("Tutorial") != null  && PlayerPrefs.GetInt("Currentlevel") == 2)
							{
								Tutorialshit2 tutscript = (Tutorialshit2)GameObject.FindGameObjectWithTag("MainCamera").GetComponent("Tutorialshit2");
								tutscript.blockcount++;
							}
							return;
						}
					}
					gothit++;
					if (greenhealth > 0)
					{
						if (greenhealth < 0)
						{
							greenhealth = 0;		
						}
						greenhealth--;
						return;	
					}
					Turntofacedmgsource(otherObject.transform.parent.transform.position);
					StartCoroutine( GetHit (otherObject.gameObject.transform.position.x) );
					yellowhealth--;
					
					if (yellowhealth <= 0)
					{
						deathvelocity(otherObject.gameObject.transform.position);
						StartCoroutine(Death ());	
					}
					
					hitdelay = 0f;
				}
				
				
			}
			
			//punk2 melee
			if (otherObject.tag == "punkhitbox2" && state == State.Playing && !hurricane && !leftkick && !rightkick && !grabbing && !junklin)
			{
				Punk2 Punk2script = (Punk2)otherObject.transform.parent.GetComponent("Punk2");
				hitdelay += Time.deltaTime;
				if (Punk2script.attacking)
				{
					if (shielded)
					{
						audio.PlayOneShot(blocksmack);
						shieldhit++;
						return;
					}
					if (blocking)
					{	
						if (Enemyinfront(otherObject.transform.parent.transform.position))
						{
							if (perfectguard)
							{
								Punk2script.dbzmode = true;
								dbzmode = true;
								linkwindow = true;
								parrying = true;
								audio.PlayOneShot(pguardsound);
								balls++;
								return;
							}
							Blockeffect();
							knockback(otherObject.transform.parent.transform.position);
							return;
						}
					}
					gothit++;
					if (greenhealth > 0)
					{
						if (greenhealth < 0)
						{
							greenhealth = 0;		
						}
						greenhealth--;
						return;	
					}
					knockback(otherObject.transform.parent.transform.position);
					StartCoroutine( GetHit (otherObject.gameObject.transform.position.x) );
					yellowhealth--;
					
					if (yellowhealth <= 0)
					{
						deathvelocity(otherObject.gameObject.transform.position);
						StartCoroutine(Death ());	
					}
				}
				
				
			}
			
			//punk3 melee
			if (otherObject.tag == "punkhitbox3" && state == State.Playing && !hurricane && !leftkick && !rightkick && !grabbing && !junklin)
			{
				Punk3 Punk3script = (Punk3)otherObject.transform.parent.GetComponent("Punk3");
				hitdelay += Time.deltaTime;
				Punk3script.nocontact = false;
				if (hitdelay > 0.05f && Punk3script.attacking)
				{
					if (shielded)
					{
						if (Punk3script.followup)
						{
							Punk3script.followup = false;
							Punk3script.justdidamove = true;
						}
						shieldhit++;
						audio.PlayOneShot(blocksmack);
						return;
					}
					if (blocking)
					{	
						if (Enemyinfront(otherObject.gameObject.transform.position))
						{
							if (Punk3script.followup)
							{
								Punk3script.followup = false;
								Punk3script.justdidamove = true;
							}
							if (perfectguard)
							{
								Punk3script.dbzmode = true;
								dbzmode = true;
								linkwindow = true;
								parrying = true;
								audio.PlayOneShot(pguardsound);
								balls++;
								return;
							}
							Blockeffect();
							knockback(otherObject.transform.position);
							return;
						}
					}
					gothit++;
					if (Punk3script.followup)
					{
						Punk3script.followup = false;
						Punk3script.justdidamove = true;
					}
					if (Punk3script.gsmashready)
					{
						Punk3script.Gsmash();
						Instantiate(enemysmack, transform.position, transform.rotation);
						audio.PlayOneShot(smack1);
						if (otherObject.transform.position.x > transform.position.x)
						CancelDBZ(new Vector3(-1200, -1500, 0));
						if (otherObject.transform.position.x <= transform.position.x)
						CancelDBZ(new Vector3(1200, -1500, 0));
						Punk3script.justdidamove = true;
						Punk3script.movetowardstimer = 0;
						falling = true;
					}
					
					if (greenhealth > 0)
					{
						if (greenhealth < 0)
						{
							greenhealth = 0;		
						}
						greenhealth--;
						return;	
					}
					if (!Punk3script.gsmashready)
					knockback(otherObject.transform.position);
					
					StartCoroutine( GetHit (otherObject.gameObject.transform.position.x) );
					yellowhealth--;
					hitdelay = 0f;
					Punk3script.resetcontact();
					if (yellowhealth <= 0)
					{
						deathvelocity(otherObject.gameObject.transform.position);	
						StartCoroutine(Death ());	
					}
					
					
				}
				
			}
			
			
			//zz and other explosive shit
			if (otherObject.tag == "Bomb" && state == State.Playing && !hurricane && !leftkick && !rightkick)
			{
				zoomzoom zzscript = (zoomzoom)otherObject.gameObject.GetComponent("zoomzoom");
				if (zzscript.isActive)
				{
					if (shielded)
					{
						zzscript.flyaway();
						shieldhit++;
						audio.PlayOneShot(blocksmack);
						return;
					}
					if (blocking)
					{	
						Instantiate(blocked, transform.position, transform.rotation);
						zzscript.explode();
						if (blockbar == 1)
							blockbar = 0;
						if (blockbar > 1)
						blockbar = 1;
						gothit++;
						return;
					}
				zzscript.explode();
				gothit++;
					
				}		
				
			}
			
			//environmaent collisions
			if (otherObject.tag == "Deathbox" && state == State.Playing && !hurricane)
			{
				StartCoroutine ( Death () );
			}
			
			if (otherObject.tag == "Wall" && rigidbody.velocity.y < 0 && !falling && !blocking)
			{
				gameObject.rigidbody.velocity = new Vector3(0, jumpbounce, 0);	
				Resetheavies();
				doingheavy = false;
				
			}
			
		}//invincible check
		
	}//ontriggerenter | End
	
	void OnTriggerStay (Collider otherObject) {
		
		if (state != State.Invincible)
		{
			if (otherObject.tag == "Hurtbox" && state == State.Playing && !hurricane)
			{
				dotdmgdelay += Time.deltaTime;
				if (dotdmgdelay > 0.4f)
				{
					if (shielded)
					{
						shieldhit++;
						dotdmgdelay = 0;
						audio.PlayOneShot(blocksmack);
						return;
					}
					loseyellowhealth(1);
					gothit++;
					audio.PlayOneShot(smack1);
					StartCoroutine ( GetHit (otherObject.gameObject.transform.position.x) );
					dotdmgdelay = 0;
				}
			}
		
		
		}
		
		
	}
	
	//death and reset---------------------------------------
	IEnumerator Death()
	{
		state = State.Explosion;
		collider.enabled = false;
		audio.PlayOneShot(death);
		if (lives != 0 && Application.loadedLevelName != "Endless")
		Sanicdrops();
		lives--;
		
		
		yield return new WaitForSeconds(3);
		
		while (blinkcount < blinks)
		{
			gameObject.renderer.enabled = !gameObject.renderer.enabled;
			
			if (gameObject.renderer.enabled == true)
				blinkcount++;
			
			yield return new WaitForSeconds(blinkrate);
		}
		
		gameObject.renderer.enabled = false;
		CancelAllStates();
		transform.position = new Vector3(0f, 300, transform.position.z);
		state = State.Invincible;
		
		if (lives < 0 && Application.loadedLevelName == "Endless")
		{
			TranscribeStats();
			Application.LoadLevel("EndlessClear");
		}
		else if (lives < 0)
		{
			
			Application.LoadLevel("GameOver");
			
		}
		
		yield return new WaitForSeconds(invincibility);
		
		gameObject.renderer.enabled = true;
		blinkcount = 0;
		animating = false;
		DefaultAnimation();
		
		while (blinkcount < blinks)
		{
			gameObject.renderer.enabled = !gameObject.renderer.enabled;
			
			if (gameObject.renderer.enabled == true)
				blinkcount++;
			
			yield return new WaitForSeconds(blinkrate);
		}
		
		
		yellowhealth = 10;
		redhealth = 10;
		if (balls > 0)
			balls = 3;
		else balls = 0;
		SetDefaults();
		obox.comboing = false;
		obox.combocounter = 0;
		
	
		
		blinkcount = 0;
		bounceupondeath = 0;
		deathtimer = 0;
		state = State.Playing;
		collider.enabled = true;
		
	}
	
	void parryanim()
	{
		karateanim.Stop();
		
		if (!karateanim.IsPlaying("Parry"))
			karateanim.Play("Parry");
		
		Debug.Log("Does this even work?");
		
	}
	
	//getting hit-----------------------------------------------
	IEnumerator GetHit (float enemyx)
	{
		if (!gettinghit)
		{
			obox.comboing = false;
			obox.combocounter = 0;
			gettinghit = true;
			
			float yoffset = Random.Range(-40, 41);
			ParticleSystem newsmack = (ParticleSystem)Instantiate (enemysmack, new Vector3(transform.position.x, transform.position.y + yoffset, -120), Quaternion.identity);
			
			if (enemyx > transform.position.x)
				newsmack.startRotation = 60;
			
			if (enemyx < transform.position.x)
			
			if (yellowhealth > 0)
			{
				StartCoroutine ( shakkee () );
				audio.PlayOneShot(smack1);
			}
			if (yellowhealth <= 0)
			{
				StartCoroutine ( shakkee () );
				audio.PlayOneShot(whoosh2);
				audio.PlayOneShot(deathsmack);
				audio.PlayOneShot(death);
			}
			StartCoroutine ( ToggleHPshake () );

			
			karateanim.Play("Damage");
			animating = true;
		}
		
		yield return new WaitForSeconds(0.7f);
		
		gettinghit = false;
		
		
	}
	
	IEnumerator GetHitnoshake ()
	{
		obox.comboing = false;
		obox.combocounter = 0;
		gettinghit = true;
		Instantiate (enemysmack, transform.position, transform.rotation);;
		audio.PlayOneShot(smack1);
		karateanim.Play("Damage");
		
		yield return new WaitForSeconds(0.7f);
		
		gettinghit = false;
		
		
	}
	
	public void Tutorialhurricane()
	{
		StartCoroutine( Supershinehur () );
	}
	
	public void TutorialGsmash()
	{
		StartCoroutine( Supershinesmash() );
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
	
	void Knockdownall()
	{
		GameObject[] enemiesonscreen = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject[] g2onscreen = GameObject.FindGameObjectsWithTag("Enemy2");
		GameObject[] g3onscreen = GameObject.FindGameObjectsWithTag("Enemy3");
		GameObject[] throweronsreen = GameObject.FindGameObjectsWithTag("Enemythrower");
		
		foreach (GameObject g in enemiesonscreen)
		{
			Punk1 enemyscript = (Punk1)g.GetComponent("Punk1");
			if (g.transform.position.y <= -150 && !enemyscript.getknocked)
			enemyscript.getknocked = true;
		}
		
		foreach (GameObject t in throweronsreen)
		{
			Punkthrower enemyscript = (Punkthrower)t.GetComponent("Punkthrower");
			if (t.transform.position.y <= -150)
			enemyscript.dbzmode = true;
		}
		
		foreach (GameObject g2 in g2onscreen)
		{
			Punk2 enemyscript2 = (Punk2)g2.GetComponent("Punk2");	
			if (g2.transform.position.y <= -150 && !enemyscript2.getknocked)
			enemyscript2.getknocked = true;
		}
		
		foreach (GameObject g3 in g3onscreen)
		{
			Punk3 enemyscript3 = (Punk3)g3.GetComponent("Punk3");
			if (g3.transform.position.y <= -150 && !enemyscript3.getknocked)
			enemyscript3.getknocked = true;
		}
		
	}
	
	//idle anim frames for just stayin still
	void DefaultAnimation()
	{
		//going up and less than -200 (Jump1)
		if (transform.position.y < -200 && rigidbody.velocity.y > 0 && !dbzmode && !gettinghit && !animating
			&& !junklin && !grabbing && !doingheavy && atkready && !karateanim.IsPlaying("Baby")) 
			karateanim.Play("Jump1");
		//going up between -200 and -50
		if (transform.position.y > -200 && transform.position.y < -50 && rigidbody.velocity.y > 0  && !animating
			&& !dbzmode && !gettinghit && !junklin && !grabbing && !doingheavy && atkready && !karateanim.IsPlaying("Baby")) 
			karateanim.Play("Jump2");
		//going up between 0 and 200 (Jump3)
		if (transform.position.y > -50 && transform.position.y < 100 && rigidbody.velocity.y > 0  && !animating
			&& !dbzmode && !gettinghit && !junklin && !grabbing && !doingheavy && atkready && !karateanim.IsPlaying("Baby")) 
			karateanim.Play("Jump3");
		//going up less than 300 (Jump4)
		if (transform.position.y > 100 && transform.position.y < 150 && rigidbody.velocity.y > 0  && !animating
			&& !dbzmode && !gettinghit && !junklin && !grabbing && !doingheavy && atkready && !karateanim.IsPlaying("Baby")) 
			karateanim.Play("Jump4");
		
		//going down and less than -200 (crab)
		if (transform.position.y < -200 && rigidbody.velocity.y < 0  && !animating && 
			!dbzmode && !gettinghit  && !junklin && !grabbing && !doingheavy && atkready && !karateanim.IsPlaying("Baby")) 
			karateanim.Play("Crab");
		//going down between -200 and 0 (Jump6)
		if (transform.position.y > -200 && transform.position.y < 0 && rigidbody.velocity.y < 0  && !animating 
			&& !dbzmode && !gettinghit  && !junklin && !grabbing && !doingheavy && atkready && !karateanim.IsPlaying("Baby")) 
			karateanim.Play("Jump6");
		//going down between 0 and 300 (Jump5)
		if (transform.position.y > 0 && transform.position.y < 150 && rigidbody.velocity.y < 0  && !animating
			&& !dbzmode && !gettinghit && !junklin && !grabbing && !doingheavy && atkready && !karateanim.IsPlaying("Baby")) 
			karateanim.Play("Jump5");
		
	}
	
	public void Gethitfunc (float otherobjectx)
	{
		StartCoroutine( GetHit (otherobjectx) );	
	}
	
	public void loseHealth (int healthamount) 
	{
		if (greenhealth > 0)
			{
				losegreenhealth(healthamount);
				return;
			}
		
			if (redhealth > yellowhealth)
			redhealth = yellowhealth;
		
			yellowhealth-= healthamount;	
			
	}
	
	public void losegreenhealth (int healthamount) 
	{
		if (greenhealth > 0)
				greenhealth -= healthamount;
		
		if (greenhealth <= 0)
			{
				greenhealth = 0;
				yellowhealth -= healthamount;
			}
	
	}
	
	public void loseyellowhealth (int healthamount) 
	{
		if (yellowhealth > 0)
				yellowhealth -= healthamount;
		
		if (yellowhealth <= 0)
			{
				yellowhealth = 0;
				redhealth -= healthamount;
			}
	
	}
	
	public void loseredhealth (int healthamount) 
	{
		if (redhealth > yellowhealth)
				redhealth -= healthamount;
		
		if (redhealth <= yellowhealth)
			{
				yellowhealth -= healthamount;
				redhealth = yellowhealth;
			}
	
	}
	
	public void knockback(Vector3 dmgposition)
	{
		//from right
		if (dmgposition.x > transform.position.x)
		{
			CancelDBZ(new Vector3(-550, 0, 0));
		}
		
		//from left
		if (dmgposition.x < transform.position.x)
		{
			CancelDBZ(new Vector3(-550, 0, 0));
		}
		Turntofacedmgsource(dmgposition);
	}
	
	private void Turntofacedmgsource(Vector3 dmgposition)
	{	
		//if facing left and dmg is coming from right
		if (!facingright && dmgposition.x > transform.position.x)
		{
			karatesprite.HFlip();
			facingright = true;
		}

		//if facing right and dmg is coming from left
		if (facingright && dmgposition.x < transform.position.x)
		{
			karatesprite.HFlip();	
			facingright = false;
		}
		
	}
	
	void deathvelocity(Vector3 dmgsource)
	{
		Turntofacedmgsource(dmgsource);
		
		if (dmgsource.x > transform.position.x)
			rigidbody.velocity = new Vector3 (-400, 800, 0);
		
		if (dmgsource.x <= transform.position.x)
			rigidbody.velocity = new Vector3 (400, 800, 0);
	}
	
	IEnumerator Dashparticles(string direction)
	{
		if (direction == "right")
		{
			Instantiate(dashemit, new Vector3( transform.position.x + 200, transform.position.y, -100), Quaternion.Euler(new Vector3(0, -90, 0)));
			
			yield return new WaitForSeconds(0.2f);
			
			Instantiate(dashemit, new Vector3( transform.position.x - 30, transform.position.y, -100), Quaternion.Euler(new Vector3(0, -90, 0)));
		}
		
		if (direction == "left")
		{
			Instantiate(dashemit, new Vector3( transform.position.x - 200, transform.position.y, -100), Quaternion.Euler(new Vector3(0, 90, 0)));
			
			yield return new WaitForSeconds(0.2f);
			
			Instantiate(dashemit, new Vector3( transform.position.x + 30, transform.position.y, -100), Quaternion.Euler(new Vector3(0, 90, 0)));
		}
		
		
	}
	
	
	//item effects, buffs & abilities--------------------------------------------------
	IEnumerator Speedbuff ()
	{
		renderer.material.color = Color.blue;
		speedbuff = true;
		
		yield return new WaitForSeconds(2);
		
		renderer.material.color = Color.white;
	}
	
	public void Speedup ()
	{
		StartCoroutine( Speedbuff () );	
		
	}
	
	IEnumerator Healthrestore (int amttorestore)
	{
		renderer.material.color = Color.yellow;
		yellowhealth += amttorestore;
		if (yellowhealth > redhealth)
		{
			if (yellowhealth >= 10  && redhealth >= 10)
				{
				 greenhealth += 1;	
				}
			yellowhealth = redhealth;
		}
		
		
		yield return new WaitForSeconds(2);
		renderer.material.color = Color.white;
	}
	
	public void Healthup (int amttorestore)
	{	
		
	StartCoroutine (Healthrestore (amttorestore) );	
		
	}
	
	public void Sanicdrops()
	{
		int moneytolose = moneythisround / 2;
		
		for (int g = 0; g < moneytolose; g++)
		{
			Instantiate(coin, transform.position, transform.rotation);
		}
		
		moneythisround = moneytolose;
		
	}
	
	void blooddrops()
	{
		Instantiate(bloods[Random.Range(0, 3)], transform.position, Quaternion.identity);	
		
	}
	
	
	
	#region Ability co-Routines & functions----------------------------------------------
	public IEnumerator GSmash ()
	{
		animating = false;
		rightkick = false;
		leftkick = false;
		groundsmash = true;
		gSmashready = false;
		obox.resetobox(0.1f);
//		animating = true;
		karateanim.Play("Baby");
		Vector3 Bounce = new Vector3(0, -2550, 0);
		CancelDBZ(Bounce);
		audio.PlayOneShot(boom);
		if (obox.combocounter > 10)
		Bouncebg();
		Object thisshit;
		
		
		thisshit = Instantiate(gbreak[Random.Range(0,2)], 
		new Vector3 (transform.position.x, -350, -150), Quaternion.Euler(new Vector3(-90,0,0)));
		hitbox.dmgReady = false;
			
		yield return new WaitForSeconds(0.75f);
			
		if (PlayerPrefs.GetString("GroundsmashT2") == "white")
		{
			Knockdownall();
		}
		groundsmash = false;
		animating = false;
		renderer.material.color = Color.white;
		Destroy(thisshit);
		
		
	}
	
	public IEnumerator Gbounce ()
	{
		rightkick = false;
		leftkick = false;
		groundsmash = true;
		gSmashready = false;
		obox.resetobox(0.1f);
//		animating = true;
		karateanim.Play("Baby");
		Vector3 Bounce = new Vector3(0, -2550, 0);
		CancelDBZ(Bounce);
		
		hitbox.dmgReady = false;
		
		yield return new WaitForSeconds(0.75f);
		
		groundsmash = false;
		animating = false;

		
	}
	
	public IEnumerator GPiledriver ()
	{
		//prep and grabbing
		animating = false;
		rightkick = false;
		leftkick = false;
		grabbing = true;
		gSmashready = false;
		obox.resetobox(0.1f);
//		animating = true;
		karateanim.Play("Doubledash");
		audio.PlayOneShot(blocksmack);
		if (Usemousecontrols)
		{
			if (MousetotheRight())
			StartCoroutine ( DragonDash (new Vector3 (100, 0, 0), 0.3f) );
			if (!MousetotheRight())
			StartCoroutine ( DragonDash (new Vector3 (-100, 0, 0), 0.3f) );
		}
		
		if (!Usemousecontrols)
		{
			if (lastkeyboardinput == "right")
			StartCoroutine ( DragonDash (new Vector3 (100, 0, 0), 0.3f) );
			if (lastkeyboardinput == "left")
			StartCoroutine ( DragonDash (new Vector3 (-100, 0, 0), 0.3f) );
			
		}
		
		yield return new WaitForSeconds(0.2f);
		
		//grab check and falling
		grabbing = false;
		if (!caughtone)
		{
			animating = false;
			yield break;
		}
		if (caughtone)
		{
			groundsmash = true;
			piledrive = true;
		}
		Vector3 Bounce = new Vector3(0, -1800, 0);
		CancelDBZ(Bounce);
		audio.PlayOneShot(boom);
		whichground = Random.Range(0,2);
		Object thisshit;
		
		//different groundsmash particles and ending
		if (whichground == 0)
		{
			thisshit = Instantiate(groundbreak1, 
			new Vector3 (transform.position.x, -350, -150), Quaternion.Euler(new Vector3(-90,0,0)));
			hitbox.dmgReady = false;
			
			yield return new WaitForSeconds(0.75f);
			
			karateanim.Play("Awyea");
			groundsmash = false;
			caughtone = false;
			animating = false;
			Destroy(thisshit);
		}
		if (whichground == 1)
		{
			thisshit = Instantiate(groundbreak2, 
			new Vector3 (transform.position.x, -350, -150), Quaternion.Euler(new Vector3(-90,0,0)));
			hitbox.dmgReady = false;
		
		yield return new WaitForSeconds(0.75f);
		
		karateanim.Play("Awyea");
		groundsmash = false;
		animating = false;
		caughtone = false;
		Destroy(thisshit);
		}
	}
	
	IEnumerator Tacklesmash ()
	{
		rigidbody.velocity = Vector3.zero;
		if (dbzmode)
		{
			dbzmode = false;
			dbzmodetimer = 0;
		}
		tackleon = true;
		animating = true;
		tackleready = false;
		
		karateanim.Play("Tackle");
		audio.PlayOneShot(whoosh2);
		
		
		if (Input.GetButton("left"))
			rigidbody.velocity = new Vector3(-1200, 0, 0);
		else if (Input.GetButton("right"))
			rigidbody.velocity = new Vector3(1200, 0, 0);
		
		else if (!Input.GetButton("left") && !Input.GetButton("right"))
		{
			if (Usemousecontrols)
			{
				if (MousetotheRight())
				rigidbody.velocity = new Vector3(1200, 0, 0);
				if (!MousetotheRight())
				rigidbody.velocity = new Vector3(-1200, 0, 0);
			}
			
			if (!Usemousecontrols)
			{
				if (lastkeyboardinput == "right")
				rigidbody.velocity = new Vector3(1200, 0, 0);
				if (lastkeyboardinput == "left")
				rigidbody.velocity = new Vector3(-1200, 0, 0);
			}
			
		}

		
		yield return new WaitForSeconds(0.2f);
		
		if (rigidbody.velocity.x > 0)
			rigidbody.velocity = new Vector3 (-200, 0, 0);
		
		if (rigidbody.velocity.x < 0)
			rigidbody.velocity = new Vector3 (200, 0, 0);
		
		if (PlayerPrefs.GetString("TackleT1") != "black")
		{
			animating = false;
			tackleon = false;
			dbzmode = true;
			bump = true;
			
		}
		
		if (PlayerPrefs.GetString("TackleT1") == "black")
		{
			yield return new WaitForSeconds(0.2f);
			
			animating = false;
			tackleon = false;
			dbzmode = true;
			bump = true;
		}
		
	}
	
	IEnumerator Forcepush()
	{
		forceon = true;
		forceready = false;
		animating = true;
		audio.PlayOneShot(forcewoosh);
		karateanim.Play("Force");
		dbzmode = true;
		
		yield return new WaitForSeconds(0.4f);
		
		animating = false;
		forceon = false;
		
	}

	public IEnumerator Hurricane ()
	{
		hurricane = true;
		hurriready = false;
		audio.PlayOneShot(wind);
		animating = true;
		karateanim.Play("Newtornado");
		
		PlayerSpeed = 221;
		rigidbody.velocity = Vector3.zero;
		gravity = -3.4f;
		
		yield return new WaitForSeconds(1.3f);
		
		hurricane = false;
		SetDefaults();

		
	}
	
	public IEnumerator invincible (float time)
	{
		karatesprite.color = Color.yellow;
		state = State.Invincible;
		
		yield return new WaitForSeconds(time);
		
		karatesprite.color = Color.white;
		state = State.Playing;
		
	}

	void Spiritbomb ()
	{
		if (PlayerPrefs.GetString("SpiritbombT2") == "black" && GameObject.FindGameObjectWithTag("Sbomb") != null)
		{
			SpiritBomb sbscript = (SpiritBomb)GameObject.FindGameObjectWithTag("Sbomb").GetComponent("SpiritBomb");
			sbscript.TriggerExplosion();
			return;
		}
			
			
		if (balls > 0 && PlayerPrefs.GetString("SpiritbombT2") != "black")
		{
			Instantiate(spiritb, transform.position, transform.rotation);
			balls -= 1;
		}
		
		if (balls >= 2 && PlayerPrefs.GetString("SpiritbombT2") == "black")
		{
			Instantiate(spiritb, transform.position, transform.rotation);
			balls -= 2;
		}
	}
	
	void teledoor ()
	{
		if (GameObject.FindGameObjectWithTag("Door") != null)
		{
			Teledoor tscript = (Teledoor)GameObject.FindGameObjectWithTag("Door").GetComponent("Teledoor");
			tscript.dooranim.PlayDefault();
			teleporttodoor();
		}
		
		if (GameObject.FindGameObjectWithTag("Door") == null && balls >= 1)
		{
			if (PlayerPrefs.GetString("TeledoorT1") == "white" || PlayerPrefs.GetString("TeledoorT2") == "white")
			{
				cameraDif = Camera.main.transform.position.y - transform.position.y;
				mouseX = Input.mousePosition.x;
				mouseY = Input.mousePosition.y;
				mWorldPos = Camera.main.ScreenToWorldPoint( new Vector3(mouseX, mouseY, cameraDif));
				Instantiate(tdoor, new Vector3 (mWorldPos.x, mWorldPos.y,
				-80), transform.rotation);
			
				balls -= 1;
				return;
			}
			
			if (PlayerPrefs.GetString("TeledoorT1") != "white" && PlayerPrefs.GetString("TeledoorT2") != "white" && balls >= 1)
			{
				Instantiate(tdoor, new Vector3 (transform.position.x, transform.position.y,
					-80), transform.rotation);
				
				balls -=1;
			}
		}
		
		
		
	}
	
	void teleporttodoor()
	{
		GameObject thisdoor = GameObject.FindGameObjectWithTag("Door");
		
		StartCoroutine ( invincible (0.5f) );
		StartCoroutine ( delayedappear (thisdoor.transform.position) );
		
	}
	
	void SpawnClonegeneric()
	{
		karatemanmirror fakescript;
		
		GameObject newclone = (GameObject)Instantiate(clonegeneric, transform.position, transform.rotation);	
		if (PlayerPrefs.GetString("CloneT2") == "white")
		{
			fakescript = (karatemanmirror)newclone.GetComponent("karatemanmirror");	
			if (Usemousecontrols)
			{
				if (MousetotheRight())
					fakescript.mousetotheright = true;
				if (!MousetotheRight())
					fakescript.mousetotheright = false;
			}
			
			if (!Usemousecontrols)
			{
				if (lastkeyboardinput == "left")
					fakescript.mousetotheright = true;
				if (lastkeyboardinput == "right")
					fakescript.mousetotheright = false;	
				
			}
		}
		
	}
	
	void SpawnClonedouble()
	{
		Instantiate(clonegeneric, new Vector3( transform.position.x - 100, transform.position.y, transform.position.z), transform.rotation);
		Instantiate(clonegeneric, new Vector3( transform.position.x + 100, transform.position.y, transform.position.z), transform.rotation);
		
	}
	
	IEnumerator delayedappear(Vector3 newpos)
	{
		renderer.enabled = false;
		
		yield return new WaitForSeconds(0.4f);
		
		audio.PlayOneShot(teleport);
		transform.position = newpos;
		dbzmode = true;
		renderer.enabled = true;
		
	}
	
//	void  InvertAllMaterialColors ()
//	{
//     	renderers = FindObjectsOfType(typeof(Renderer));
//		
//   	 	foreach (render in renderers) 
//		{
//      		 if (render.material.HasProperty("_Color")) 
//				{
//        		 	render.material.color = InvertColor (render.material.color);
//      			}
//    	}
//	}
	
	Color InvertColor ( Color color  )
	{
   		 return new Color (1.0f - color.r, 1.0f - color.g, 1.0f - color.b);
	}
	

	IEnumerator ShootBeam ()
	{
		animating = true;
		if (PlayerPrefs.GetString("LazerT2") == "white")
			karateanim.Play("centurion");
		else
		karateanim.Play("Special4");
		
		
//		if (PlayerPrefs.GetString("LazerT2") == "black")
//			mouserotating = true;
		
		yield return new WaitForSeconds(0.2f);
		
		if (PlayerPrefs.GetString("LazerT2") == "white")
		{
			Instantiate(vertbeam, new Vector3(transform.position.x, 1350, -90), Quaternion.identity);
			audio.PlayOneShot(heal);
			dbzmode = true;
			yield break;	
		}
		
		if (PlayerPrefs.GetString("LazerT1") == "black")
		{
			if (Usemousecontrols)
			{
				if (MousetotheRight())
				{
					Instantiate(biggerbeam, 
						new Vector3(transform.position.x + 40, transform.position.y + 30, transform.position.z), 
						transform.rotation);
				}
			
				if (!MousetotheRight())
				{
					Instantiate(biggerbeam, 
						new Vector3(transform.position.x - 40, transform.position.y + 30, transform.position.z), 
						Quaternion.Euler(new Vector3( 0, 0, -180)));
				}
			}
			
			if (!Usemousecontrols)
			{
				if (lastkeyboardinput == "right")
				{
					Instantiate(biggerbeam, 
						new Vector3(transform.position.x + 40, transform.position.y + 30, transform.position.z), 
						transform.rotation);
				}
			
				if (lastkeyboardinput == "left")
				{
					Instantiate(biggerbeam, 
						new Vector3(transform.position.x - 40, transform.position.y + 30, transform.position.z), 
						Quaternion.Euler(new Vector3( 0, 0, -180)));
				}
				
			}
			
		}
		
		
		else
			
		{
			if (Usemousecontrols)
			{
				if (MousetotheRight())
				{
					Instantiate(beam, 
						transform.position, 
						transform.rotation);
				}
				
				if (!MousetotheRight())
				{
					Instantiate(beam, 
						transform.position, 
						Quaternion.Euler(new Vector3( 0, 0, -180)));
				}
			}
			
			if (!Usemousecontrols)
			{
				if (lastkeyboardinput == "right")
				{
					Instantiate(beam, 
						transform.position, 
						transform.rotation);
				}
				
				if (lastkeyboardinput == "left")
				{
					Instantiate(beam, 
						transform.position, 
						Quaternion.Euler(new Vector3( 0, 0, -180)));
				}
				
				
			}
		}
		
		
	}
	
	IEnumerator Hundredfistcast()
	{
		
		yield return new WaitForSeconds(1);
		
	}
	
	IEnumerator Finalbodycast()
	{
		
		yield return new WaitForSeconds(1);
		
	}
	
	void serenity()
	{
		
			Instantiate(Serenity, new Vector3(transform.position.x, transform.position.y, -50), transform.rotation);	
			shielded = true;
			if (PlayerPrefs.GetString("SerenityT2") == "white")
				Healthup(5);
	}
	
	void SpawnWall()
	{
		//t2 white smartcast small weapons
		if (PlayerPrefs.GetString("WallT2") == "white" && PlayerPrefs.GetString("WallT1") != "black")
		{
			cameraDif = Camera.main.transform.position.y - transform.position.y;
			mouseX = Input.mousePosition.x;
			mouseY = Input.mousePosition.y;
			mWorldPos = Camera.main.ScreenToWorldPoint( new Vector3(mouseX, mouseY, cameraDif));
			Instantiate(spear, new Vector3(mWorldPos.x, mWorldPos.y, -100), transform.rotation);
			return;
		}
		
		//t2 white smartcast big weapons
		if (PlayerPrefs.GetString("WallT2") == "white" && PlayerPrefs.GetString("WallT1") == "black")
		{
			cameraDif = Camera.main.transform.position.y - transform.position.y;
			mouseX = Input.mousePosition.x;
			mouseY = Input.mousePosition.y;
			mWorldPos = Camera.main.ScreenToWorldPoint( new Vector3(mouseX, mouseY, cameraDif));
			Instantiate(blackspear, new Vector3(mWorldPos.x, mWorldPos.y, -100), transform.rotation);
			return;
		}
		
		//regular cast dependant on mouse
		if (Usemousecontrols)
		{
			if (MousetotheRight())
			{
				if (PlayerPrefs.GetString("WallT1") == "black" && PlayerPrefs.GetString("WallT2") != "black")
				{
					Instantiate(blackspear, new Vector3(transform.position.x + 100, 500, -100), transform.rotation);
					return;
				}
				
				if (PlayerPrefs.GetString("WallT2") == "black" && PlayerPrefs.GetString("WallT1") != "black")
				{
					Instantiate(club, new Vector3(transform.position.x + 100, 500, -100), transform.rotation);
					return;
				}
				
				if (PlayerPrefs.GetString("WallT2") == "black" && PlayerPrefs.GetString("WallT2") == "black")
				{
					Instantiate(blackclub, new Vector3(transform.position.x + 100, 500, -100), transform.rotation);
					return;
				}
				
				Instantiate(spear, new Vector3(transform.position.x + 100, 500, -100), transform.rotation);
	
				
			}
			
			if (!MousetotheRight())
			{
				if (PlayerPrefs.GetString("WallT1") == "black" && PlayerPrefs.GetString("WallT2") != "black")
				{
					Instantiate(blackspear, new Vector3(transform.position.x - 100, 500, -100), transform.rotation);
					return;
				}
				
				if (PlayerPrefs.GetString("WallT2") == "black" && PlayerPrefs.GetString("WallT1") != "black")
				{
					Instantiate(club, new Vector3(transform.position.x - 100, 500, -100), transform.rotation);
					return;
				}
				
				if (PlayerPrefs.GetString("WallT2") == "black" && PlayerPrefs.GetString("WallT2") == "black")
				{
					Instantiate(blackclub, new Vector3(transform.position.x - 100, 500, -100), transform.rotation);
					return;
				}
				
				
			  	Instantiate(spear, new Vector3(transform.position.x - 100, 500, -100), transform.rotation);
					
				
			}
		}
		
		if (!Usemousecontrols)
		{
			if (lastkeyboardinput == "right")
			{
				if (PlayerPrefs.GetString("WallT1") == "black" && PlayerPrefs.GetString("WallT2") != "black")
				{
					Instantiate(blackspear, new Vector3(transform.position.x + 100, 500, -100), transform.rotation);
					return;
				}
				
				if (PlayerPrefs.GetString("WallT2") == "black" && PlayerPrefs.GetString("WallT1") != "black")
				{
					Instantiate(club, new Vector3(transform.position.x + 100, 500, -100), transform.rotation);
					return;
				}
				
				if (PlayerPrefs.GetString("WallT2") == "black" && PlayerPrefs.GetString("WallT2") == "black")
				{
					Instantiate(blackclub, new Vector3(transform.position.x + 100, 500, -100), transform.rotation);
					return;
				}
				
				Instantiate(spear, new Vector3(transform.position.x + 100, 500, -100), transform.rotation);
			}
			
			if (lastkeyboardinput == "left")
			{
				if (PlayerPrefs.GetString("WallT1") == "black" && PlayerPrefs.GetString("WallT2") != "black")
				{
					Instantiate(blackspear, new Vector3(transform.position.x - 100, 500, -100), transform.rotation);
					return;
				}
				
				if (PlayerPrefs.GetString("WallT2") == "black" && PlayerPrefs.GetString("WallT1") != "black")
				{
					Instantiate(club, new Vector3(transform.position.x - 100, 500, -100), transform.rotation);
					return;
				}
				
				if (PlayerPrefs.GetString("WallT2") == "black" && PlayerPrefs.GetString("WallT2") == "black")
				{
					Instantiate(blackclub, new Vector3(transform.position.x - 100, 500, -100), transform.rotation);
					return;
				}
				
				
			  	Instantiate(spear, new Vector3(transform.position.x - 100, 500, -100), transform.rotation);
					
				
			}
		}
	}
	
	#endregion
	
	
	
//	#region Animations: light attack 3 hit combo----------------------------------
//	IEnumerator PlayFrame1 ()
//	{
//		animating = true;
//					audio.PlayOneShot(whoosh1);
//					StartCoroutine( attack1 () );
//					currentanimation = new Vector2(0.25f, 0.5f);
//					frame1played = true;
//						
//					yield return new WaitForSeconds(0.2f);
//					
//					animating = false;
//		
//	}
//
//	IEnumerator PlayFrame2 ()
//	{
//		animating = true;
//					audio.PlayOneShot(whoosh1);
//					StartCoroutine( attack2 () );
//					currentanimation = new Vector2(0.75f, 0.5f);
//					frame2played = true;
//						
//					yield return new WaitForSeconds(0.2f);
//					
//					animating = false;
//		
//	}
//
//	IEnumerator PlayFrame3 ()
//	{
//		animating = true;
//					audio.PlayOneShot(whoosh1);
//					StartCoroutine( attack3 () );
//					currentanimation = new Vector2(0.75f, 0.25f);
//					frame3played = true;
//						
//					yield return new WaitForSeconds(0.2f);
//					
//					animating = false;
//		
//	}
//
//	IEnumerator attack1()
//	{
//		renderer.material.mainTextureOffset = new Vector2 (0f, 0.5f);
//		
//		yield return new WaitForSeconds(0.1f);
//		
//		renderer.material.mainTextureOffset = new Vector2 (0.25f, 0.5f);
//		
//	}
//
//	IEnumerator attack2()
//	{
//		renderer.material.mainTextureOffset = new Vector2 (0.5f, 0.5f);
//		
//		yield return new WaitForSeconds(0.1f);
//		
//		renderer.material.mainTextureOffset = new Vector2 (0.75f, 0.5f);
//		
//	}
//
//	IEnumerator attack3()
//	{
//		renderer.material.mainTextureOffset = new Vector2 (0.75f, 0.25f);
//		
//		yield return new WaitForSeconds(0.1f);
//		
//		renderer.material.mainTextureOffset = new Vector2 (0.75f, 0.25f);
//		
//	}
//	#endregion
	
	IEnumerator heavyup()
	{
		spacebar++;
		doingheavy = true;
		animating = true;
//		transform.rotation = Quaternion.Euler(new Vector3(270, 270, 90));
//		StartCoroutine (Junklinz () );
////		rigidbody.velocity = new Vector3(0, 9 ,0);
//		
//		yield return new WaitForSeconds(0.6f);
//		
//		animating = false;
//		transform.rotation = Quaternion.Euler(new Vector3(90, 270, 90));
//		animating = true;
		dbzmode = true;
		karateanim.Play("Junklinlong");
		
		yield return new WaitForSeconds(0.3f);
		
		if (Usemousecontrols)
		{
			if (MousetotheRight()) 
			{
				StartCoroutine ( DragonDash( new Vector3 (100, 300, 0), 0.1f));
			}
			if (!MousetotheRight())
			{
				StartCoroutine ( DragonDash( new Vector3 (-100, 300, 0), 0.1f));
			}
		}
		if (!Usemousecontrols)
		{
			if (lastkeyboardinput == "right") 
			{
				StartCoroutine ( DragonDash( new Vector3 (100, 300, 0), 0.1f));
			}
			if (lastkeyboardinput == "left")
			{
				StartCoroutine ( DragonDash( new Vector3 (-100, 300, 0), 0.1f));
			}
		}
			
		junklin = true;
		StartCoroutine ( shadowtrail () );
		
	}
	
	IEnumerator shadowtrail()
	{
		if (facingright)
		{
			Instantiate(shadow, new Vector3(transform.position.x - 50, transform.position.y - 50, -90), transform.rotation);
			
			yield return new WaitForSeconds(0.05f);
				
			Instantiate(shadow, new Vector3(transform.position.x - 50, transform.position.y - 50, -90), transform.rotation);
			
			yield return new WaitForSeconds(0.05f);
				
			Instantiate(shadow, new Vector3(transform.position.x - 50, transform.position.y - 50, -90), transform.rotation);
			
			yield return new WaitForSeconds(0.05f);
				
			Instantiate(shadow, new Vector3(transform.position.x - 50, transform.position.y - 50, -90), transform.rotation);
			
		}
		
		if (!facingright)
		{
			Instantiate(shadowflip, new Vector3(transform.position.x + 50, transform.position.y - 50, -90), transform.rotation);
			
			yield return new WaitForSeconds(0.05f);
				
			Instantiate(shadowflip, new Vector3(transform.position.x + 50, transform.position.y - 50, -90), transform.rotation);
			
			yield return new WaitForSeconds(0.05f);
				
			Instantiate(shadowflip, new Vector3(transform.position.x + 50, transform.position.y - 50, -90), transform.rotation);
			
			yield return new WaitForSeconds(0.05f);
				
			Instantiate(shadowflip, new Vector3(transform.position.x + 50, transform.position.y - 50, -90), transform.rotation);
			
		}
		
		
		
		
	}
	
	void quickheavyup()
	{
		heavycounter = 0;
		spacebar++;
		doingheavy = true;
//		transform.rotation = Quaternion.Euler(new Vector3(270, 270, 90));
//		StartCoroutine (Junklinz () );
////		rigidbody.velocity = new Vector3(0, 9 ,0);
//		
//		yield return new WaitForSeconds(0.6f);
//		
//		animating = false;
//		transform.rotation = Quaternion.Euler(new Vector3(90, 270, 90));
//		animating = true;
		junklin = true;
		karateanim.Play("Junklinshort");
			
		if (Usemousecontrols)
		{
			if (MousetotheRight()) 
			{
				StartCoroutine ( DragonDash( new Vector3 (80, 240, 0), 0.1f));
			}
			if (!MousetotheRight())
			{
				StartCoroutine ( DragonDash( new Vector3 (-80, 240, 0), 0.1f));
			}
		}
			
		if (!Usemousecontrols)
		{
			if (lastkeyboardinput == "right") 
			{
				StartCoroutine ( DragonDash( new Vector3 (80, 240, 0), 0.1f));
			}
			if (lastkeyboardinput == "left")
			{
				StartCoroutine ( DragonDash( new Vector3 (-80, 240, 0), 0.1f));
			}
		}
					
		StartCoroutine ( shadowtrail () );
		
		
	}
	
	IEnumerator heavyright()
	{
		heavycounter = 0;
		dbzmode = true;
		spacebar++;
		animating = true;
		doingheavy = true;
		karateanim.Play("Dragonkick");
		
		
		yield return new WaitForSeconds(0.3f);
		
		rightkick = true;
		
	}
	
	void quickright()
	{
		heavycounter = 0;
		spacebar++;
		doingheavy = true;
//		rightkick = true;
////		renderer.material.mainTextureOffset = new Vector2 (0.75f, 0.25f);
//		renderer.material = firekick;
//		transform.localScale = firekickscale;
//		
//		yield return new WaitForSeconds(0.5f);
//		
//		renderer.material = regular;
//		transform.localScale = regularscale;
//		gravity = regulargrav;
//		rightkick = false;
//		animating = false;
		
			animating = true;
			rightkick = true;
			karateanim.Play ("Quickdkick");
		
	}
	
	IEnumerator heavyleft()
	{
		heavycounter = 0;
		dbzmode = true;
		spacebar++;
		animating = true;
		doingheavy = true;
		karateanim.Play ("Dragonkick");
		
		yield return new WaitForSeconds(0.3f);
		
		leftkick = true;
		
	}
	
	void quickleft()
	{
		heavycounter = 0;
		spacebar++;
		doingheavy = true;
//		leftkick = true;
//		renderer.material = firekickL;
//		transform.localScale = firekickscale;
//		
//		yield return new WaitForSeconds(0.5f);
//		
//		gravity = regulargrav;
//		renderer.material = regular;
//		transform.localScale = regularscale;
//		leftkick = false;
//		animating = false;
		
		animating = true;
		leftkick = true;
		karateanim.Play ("Quickdkick");
		
	}
	
	void HeavyDown ()
	{
		heavycounter = 0;
		spacebar++;
		doingheavy = true;
//		bump = true;
//		if (direction == "left")
//			renderer.material = mirror; 
//		StartCoroutine ( Axekick () );
//		
//		yield return new WaitForSeconds(0.7f);
//		
//		if (direction == "left")
//			renderer.material = regular;
//		animating = false;
		
//		animating = true;
		bump = true;
		animating = true;
		karateanim.Play("Axekick");
		
	}
	
	void quickheavydown ()
	{
		heavycounter = 0;
		spacebar++;
		doingheavy = true;
		
//		bump = true;
//		if (direction == "left")
//			renderer.material = mirror;
//		StartCoroutine ( quickAxekick () );
//		yield return new WaitForSeconds(0.3f);
//		
//		if (direction == "left")
//			renderer.material = regular;
//		linkwindow = true;
//		linktimer = 0;
//		animating = false;
		
		animating = true;
		bump = true;
		if (!karateanim.IsPlaying("Quickaxe"))
		karateanim.Play("Quickaxe");
		
	}
	
	public IEnumerator DragonDash (Vector3 offset, float divider)
	{
// 	 0fastest to 1 slowest
  	 Vector3 org = transform.position; 
  	 Vector3 dest = org+offset;
 	 float t = 0;
  	 while (t < 1)
		{ 
	    	 t += Time.deltaTime/ divider;
	   		 transform.position = Vector3.Lerp(org, dest, t);
	   		 yield return null; 
 		}

	}
	
	// other shit------------------------------------------------------
	IEnumerator OpenDashWindow()
	{
//		SelectTarget();
		targetdash = true;
		
		yield return new WaitForSeconds(2);
		
		targetdash = false;
		
	}
	
	IEnumerator OpenDDashWindow()
	{
		doubledashwindow = true;
		
		yield return new WaitForSeconds(0.5f);
		
		doubledashwindow = false;
		
	}

	
	IEnumerator shakkee()
	{
		Vector3 pospos = karatesprite.offset;
		
		karatesprite.offset = new Vector3(pospos.x + 15, pospos.y, pospos.z);
		
		yield return new WaitForSeconds(0.05f);
		
		karatesprite.offset = new Vector3(pospos.x - 15, pospos.y, pospos.z);
		
		yield return new WaitForSeconds(0.05f);
		
		karatesprite.offset = new Vector3(pospos.x + 15, pospos.y, pospos.z);
		
		yield return new WaitForSeconds(0.05f);
		
		karatesprite.offset = new Vector3(pospos.x - 15, pospos.y, pospos.z);
		
		yield return new WaitForSeconds(0.05f);
		
		karatesprite.offset = new Vector3(pospos.x + 15, pospos.y, pospos.z);
		
		yield return new WaitForSeconds(0.05f);
		
		karatesprite.offset = Vector3.zero;
		
	}
	
	IEnumerator parrycolor()
	{
		
		karatesprite.color = Color.yellow;
		
		yield return new WaitForSeconds(0.05f);
		
		karatesprite.color = Color.white;
		
		yield return new WaitForSeconds(0.05f);
		
		karatesprite.color = Color.yellow;
		
		yield return new WaitForSeconds(0.05f);
		
		karatesprite.color = Color.white;
		
		yield return new WaitForSeconds(0.05f);
		
		karatesprite.color = Color.yellow;
		
		yield return new WaitForSeconds(0.05f);
		
		karatesprite.color = Color.white;
		
		yield return new WaitForSeconds(0.05f);
		
		karatesprite.color = Color.yellow;
		
		yield return new WaitForSeconds(0.05f);
		
		karatesprite.color = Color.white;
		
	}
	
	void ResetBuffs ()
	{
		SetDefaults();
		
	}
	
	
	public void SetDefaults ()
	{
		PlayerSpeed = 442;
		dbzmode = false;
		gravity = regulargrav;
		jumpbounce = regularjb;
		stunned = false;
		falling = false;
		
	}
	
	public void CancelDBZ (Vector3 tt)
	{
		dbzmode = false;
		dbzmodetimer = 0f;
		PlayerSpeed = 442;
//		transform.localScale = new Vector3(0.34f, 1f, 0.34f);
		gravity = regulargrav;
		jumpbounce = regularjb;
		rigidbody.velocity = tt;
		
	}
	
	public void CancelAllStates()
	{
//		animating = false;
		dbzmode = false;	
		dbzmodetimer = 0;
		additionalairtime = false;
		airtimer = 0;
//		hurricane = false;
		groundsmash = false;
		blocking = false;
		parrying = false;
		showblockbar = false;
		dashing = false;
		stunned = false;
		guardbreak = false;
		targetdash = false;
		doingheavy = false;
		leftkick = false;
		rightkick = false;
		
	}
	
//	void SelectTarget()
//	{
//		if (currenttarget == null)
//		{
//		currenttarget = targetbox.SelectTarget();
//		currenttarget.renderer.material.color  = Color.yellow;
//		}
//	}
	
	IEnumerator CancelDBZwithtime(float time, Vector3 returnvelo)
	{
		yield return new WaitForSeconds(time);
		
		CancelDBZ(returnvelo);
		
	}
	
	IEnumerator Cancelkicks()
	{
		yield return new WaitForSeconds(0.6f);
			
		if (rightkick)
			rightkick = false;
		if (leftkick)
			leftkick = false;
	}
	
	public bool MousetotheRight()
	{	
		cameraDif = Camera.main.transform.position.y - transform.position.y;
		mouseX = Input.mousePosition.x;
		mouseY = Input.mousePosition.y;
		mWorldPos = Camera.main.ScreenToWorldPoint( new Vector3(mouseX, mouseY, cameraDif));
	
		diffX = mWorldPos.x - transform.position.x;
		diffY = mWorldPos.y - transform.position.y;
			
		if (diffX < 0)
		{
			return false;
		}
		
		else return true;
	
		
	}
	
	public string mousemoved()
	{
		if (Input.GetAxis("MouseX") > 0.75f)
			return "right";
		if (Input.GetAxis("MouseX") < -0.75f)
			return "left";
		
		return null;
	}
	
	public bool Enemyinfront(Vector3 enemyposition)
	{
		if (facingright && enemyposition.x > transform.position.x)
			return true;
		
		if (facingright && enemyposition.x < transform.position.x)
			return false;
			
		if (!facingright && enemyposition.x < transform.position.x)
			return true;
		
		if (!facingright && enemyposition.x > transform.position.x)
			return false;
		
		return true;
	}
	
	
	
	private void QuerySlot(string slot)
	{
		string thisslot = PlayerPrefs.GetString(slot);
		
		//BODY CATAGORY
		if (thisslot == "Groundsmash")
		{
			if (gSmashready)
			{
				if (PlayerPrefs.GetString("GroundsmashT2") == "black")
				StartCoroutine ( GPiledriver () );
				else if (PlayerPrefs.GetString("GroundsmashT2") == "white")
				StartCoroutine (GSmash () );
				else
				StartCoroutine (GSmash () );
			}
		}
		
		if (thisslot == "Tackle")
		{
			if (tackleready)
			{
				StartCoroutine( Tacklesmash () );
			}
		}
		
		if (thisslot == "Force")
		{
			if (forceready)
			{
				StartCoroutine( Forcepush () );
			}
		}
		
		if (thisslot == "Hundredfist")
		{
			if (hfistready)
			{
				StartCoroutine ( Hundredfistcast () );
			}
		}
		
		//replace with final body skill
		if (thisslot == "Hundredfist")
		{
			if (finalbodyready)
			{
				StartCoroutine ( Hundredfistcast () );
			}
		}
		
		//MIND CATAGORY
		if (thisslot == "Hurricane")
		{
			if (hurriready && balls >=3)
			{
				StartCoroutine( Supershinehur () );
				balls -= 3;
				
				if (balls < 0)
						balls = 0;
			}
		}
		
		if (thisslot == "Lazer")
		{
			if (beamready && balls >= 4)
			{
				StartCoroutine( Supershinelaz () );
				beamready = false;
			
				balls -= 4;
			}
		}
		
		//replace with new mind ability
		if (thisslot == "Lazer")
		{
			if (beamready && balls >= 4)
			{
				StartCoroutine( Supershinelaz () );
				beamready = false;
			
				balls -= 4;
			}
		}
		
		if (thisslot == "Serenity")
		{
			if (barrierready && balls >= 4 && GameObject.FindGameObjectWithTag("Barrier") == null)
			{
				StartCoroutine( Supershineser () );	
				barrierready = false;
				balls -= 4;
			}
		}
		
		if (thisslot == "Warudo")
		{
			if (beamready && balls >= 4)
			{
				StartCoroutine( Supershinelaz () );
				warudoready = false;
			
			}
		}
		
		
		//TECH CATAGORY
		if (thisslot == "Spiritbomb")
		{
			Spiritbomb();	
		}
		
		if (thisslot == "Teledoor")
		{
			teledoor();
		}
		
		if (thisslot == "Wall")
		{
			if (balls >= 1)
			{
				SpawnWall();
				balls -= 1;
			}
		}
		
		if (thisslot == "Clone")
		{
			if (balls >= 1.25f && PlayerPrefs.GetString("CloneT2") != "black")
			{
				SpawnClonegeneric();
				balls -= 1.25f;
			}
			
			if (PlayerPrefs.GetString("CloneT2") == "black" && balls >= 2.5f)
			{
				SpawnClonedouble();
				balls -= 2.5f;
			}
		}
		
		if (thisslot == "Vplate")
		{
				
			
		}
		
		if (thisslot == "null")
		{
			Debug.Log("This slot is empty");
		}
	}
	
	IEnumerator Supershinehur()
	{
		transform.position = new Vector3(transform.position.x, transform.position.y, -200);
		state = State.Invincible;
		Instantiate(supshine, new Vector3(transform.position.x, transform.position.y, -210), Quaternion.identity);
		Instantiate(greyscreen, new Vector3(0,0, -180), Quaternion.Euler(new Vector3(270, 0, 0)));
		shining = true;
		dbzmode = true;
		obox.combotimer = 0;
		pausescript.playerpause = true;
		audio.PlayOneShot(super);
		
		yield return new WaitForSeconds(0.8f);
		
		pausescript.playerpause = false;
		shining = false;
		state = State.Playing;
		transform.position = new Vector3(transform.position.x, transform.position.y, -100);
		
		StartCoroutine( Hurricane () );
		
	}
	
	IEnumerator Supershinesmash()
	{
		transform.position = new Vector3(transform.position.x, transform.position.y, -200);
		state = State.Invincible;
		Instantiate(supshine, new Vector3(transform.position.x, transform.position.y, -210), Quaternion.identity);
		Instantiate(greyscreen, new Vector3(0,0, -180), Quaternion.Euler(new Vector3(270, 0, 0)));
		shining = true;
		dbzmode = true;
		obox.combotimer = 0;
		pausescript.playerpause = true;
		audio.PlayOneShot(super);
		
		yield return new WaitForSeconds(0.8f);
		
		pausescript.playerpause = false;
		shining = false;
		state = State.Playing;
		transform.position = new Vector3(transform.position.x, transform.position.y, -100);
		
		StartCoroutine ( GSmash () );
		
	}
	
	IEnumerator Supershineser()
	{
		transform.position = new Vector3(transform.position.x, transform.position.y, -200);
		state = State.Invincible;
		Instantiate(supshine, new Vector3(transform.position.x, transform.position.y, -210), Quaternion.identity);
		Instantiate(greyscreen, new Vector3(0,0, -180), Quaternion.Euler(new Vector3(270, 0, 0)));
		shining = true;
		dbzmode = true;
		obox.combotimer = 0;
		pausescript.playerpause = true;
		audio.PlayOneShot(super);
		
		yield return new WaitForSeconds(0.8f);
		
		pausescript.playerpause = false;
		shining = false;
		state = State.Playing;
		transform.position = new Vector3(transform.position.x, transform.position.y, -100);
		
		serenity();
		
	}
	
	IEnumerator Supershinelaz()
	{
		transform.position = new Vector3(transform.position.x, transform.position.y, -200);
		state = State.Invincible;
		Instantiate(supshine, new Vector3(transform.position.x, transform.position.y, -210), Quaternion.identity);
		Instantiate(greyscreen, new Vector3(0,0, -180), Quaternion.Euler(new Vector3(270, 0, 0)));
		shining = true;
		dbzmode = true;
		obox.combotimer = 0;
		pausescript.playerpause = true;
		audio.PlayOneShot(super);
		
		yield return new WaitForSeconds(0.8f);
		
		pausescript.playerpause = false;
		shining = false;
		state = State.Playing;
		transform.position = new Vector3(transform.position.x, transform.position.y, -100);
		
		StartCoroutine( ShootBeam () );
		
	}
	
	IEnumerator Supershinewarudo()
	{
		transform.position = new Vector3(transform.position.x, transform.position.y, -200);
		state = State.Invincible;
		Instantiate(supshine, new Vector3(transform.position.x, transform.position.y, -210), Quaternion.identity);
		Instantiate(greyscreen, new Vector3(0,0, -180), Quaternion.Euler(new Vector3(270, 0, 0)));
		shining = true;
		dbzmode = true;
		obox.combotimer = 0;
		pausescript.playerpause = true;
		audio.PlayOneShot(super);
		
		yield return new WaitForSeconds(0.8f);
		
		pausescript.playerpause = false;
		shining = false;
		state = State.Playing;
		transform.position = new Vector3(transform.position.x, transform.position.y, -100);
		
		StartCoroutine( ShootBeam () );
		
	}
	
	public void Resetheavies()
	{
		if (!obox.junklinready)
			obox.junklinready = true;
		
		if (!obox.axekickready)
			obox.axekickready = true;
		
		if (!obox.Dkickready)
			obox.Dkickready = true;
		
		if (!dashready)
		{
			dashready = true;	
			dashcooldown = 0;
		}
		
	}
	
	//made this because doing heavy was not resetting during dynasty mode
	IEnumerator resetdoingheavywithtime(float time)
	{
		yield return new WaitForSeconds(time);
		
		doingheavy = false;	
		
	}
	
	public void GetSpawner()
	{
		if (PlayerPrefs.GetInt("Currentlevel") == 1)
		{
			spawner1 = (Level1Spawn)GameObject.FindGameObjectWithTag("Spawner").GetComponent("Level1Spawn");
		}
		
		if (PlayerPrefs.GetInt("Currentlevel") == 2)
		{
			spawner2 = (Level2Spawn)GameObject.FindGameObjectWithTag("Spawner").GetComponent("Level2Spawn");
		}
		
		if (PlayerPrefs.GetInt("Currentlevel") == 3)
		{
			spawner3 = (Level3Spawn)GameObject.FindGameObjectWithTag("Spawner").GetComponent("Level3Spawn");
		}
		
		if (PlayerPrefs.GetInt("Currentlevel") == 4)
		{
			spawner4 = (Level4Spawn)GameObject.FindGameObjectWithTag("Spawner").GetComponent("Level4Spawn");
		}
		
	}
	
	public void endPose ()
	{
//		spawner = (Spawnerv2)GameObject.FindGameObjectWithTag("Spawner").GetComponent("Spawnerv2");
//		spawner.MaxEnemies = lvlenemies;
//		spawner.Maxshits = lvlgrounds;
//		spawner.MaxWrestler = lvlwrestlers;
		TranscribeStats();
		Vector3 endposition = new Vector3(0, 0, -100);
		transform.Translate(Vector3.Normalize(endposition)
			* 2 * Time.deltaTime,Space.World);
		
	}
	
	public void TranscribeStats()
	{	
		//enemies
		PlayerPrefs.SetInt("PunksDefeated", enemydefeated);
		PlayerPrefs.SetInt("WrestlersDefeated", wrestlerdefeated);
		PlayerPrefs.SetInt("GroundsDefeated", grounddefeated);
		PlayerPrefs.SetInt("ThrowersDefeated", throwersdefeated);
		PlayerPrefs.SetInt("ZZsdefeated", ZZdefeated);
		PlayerPrefs.SetInt("Ninjasdefeated", ninjadefeated);
		
		//XP
		//if next level is unlocked or not
		if (PlayerPrefs.GetInt("Level" + (PlayerPrefs.GetInt("Currentlevel") + 1) + "Unlock") == 1)
			PlayerPrefs.SetFloat("XPgained", XPgained / 2);
		if (PlayerPrefs.GetInt("Level" + (PlayerPrefs.GetInt("Currentlevel") + 1) + "Unlock") == 0)
			PlayerPrefs.SetFloat("XPgained", XPgained);
			
		//accuracy
		PlayerPrefs.SetFloat("Hitscore", hits);
		PlayerPrefs.SetFloat("Spacebar", spacebar);
		
		//dmg taken
		PlayerPrefs.SetFloat("DmgTaken", gothit);
		
		//combo
		PlayerPrefs.SetInt("HighestCombo", highestcombo);
		PlayerPrefs.SetInt("Maxairtime", (int)Mathf.Round(maxairtime));
		
		//moneys
		PlayerPrefs.SetInt("Moneythisround", moneythisround);
		PlayerPrefs.SetInt("Redpickup", redpickup);
		PlayerPrefs.SetInt("Greenpickup", greenpickup);
		PlayerPrefs.SetInt("Bluepickup", bluepickup);
		
		//Endless specific
		if (Application.loadedLevelName == "Endless")
		{
			Spawner spawnscript = (Spawner)GameObject.FindGameObjectWithTag("Left").GetComponent("Spawner");	
			PlayerPrefs.SetInt("Endlesssurvival", (int)spawnscript.survivaltimer);
			PlayerPrefs.SetInt("Endlesswavesdefeated", spawnscript.wavesdefeated);
		}
		
	}
	
	public void InitializeStats()
	{
		//Level
		PlayerPrefs.SetInt("Currentlevel", Application.loadedLevel);
		

		//enemies
		PlayerPrefs.SetInt("PunksDefeated", 0);
		PlayerPrefs.SetInt("WrestlersDefeated", 0);
		PlayerPrefs.SetInt("GroundsDefeated", 0);

			
		//accuracy
		PlayerPrefs.SetFloat("Hitscore", 0);
		PlayerPrefs.SetFloat("Spacebar", 0);
		
		//dmg taken
		PlayerPrefs.SetFloat("DmgTaken", 0);
		
		//combo && airtime
		PlayerPrefs.SetInt("HighestCombo", 0);
		PlayerPrefs.SetInt("Maxairtime", 0);
		
		//moneys && xp
		PlayerPrefs.SetInt("Moneythisround", 0);
		PlayerPrefs.SetFloat("XPgained", 0);
		PlayerPrefs.SetInt("Redpickup", 0);
		PlayerPrefs.SetInt("Greenpickup", 0);
		PlayerPrefs.SetInt("Bluepickup", 0);
		
		//
		PlayerPrefs.SetInt("Checkpointskilled", 0);
		
	}
	
	IEnumerator Stagestarteup(float waittime)
	{
		yield return new WaitForSeconds(waittime);
		
		stagetime = false;
		
	}
	
	void Bouncebg()
	{
		GameObject[] backgstuff = GameObject.FindGameObjectsWithTag("bgbump");
		
		foreach (GameObject bb in backgstuff)
		{
			bb.rigidbody.velocity = new Vector3 (0, 500, 0);	
			
		}
		
	}

	
	void OnGUI () {
		
	GUI.contentColor = Color.white;
		
	if (!endtime && !stagetime && !pausescript.pausemenud)
	{	
			
		#region devshit
		if (devshitison)
			{
				GUI.Label(new Rect(0, 140, 200, 100), "DEV SHIT SONS");
				GUI.Box(new Rect(0, 160, 120, 380), " ");
				
				GUI.Label(new Rect(0, 160, 200, 100), "animating == " + animating.ToString());
				GUI.Label(new Rect(0, 180, 200, 100), "timer: " + animationtimer.ToString());
				if (karateanim.GetCurrentAnimation() != null)
				GUI.Label(new Rect(0, 200, 200, 100), "animclip: " + karateanim.GetCurrentAnimation().name.ToString());
				GUI.Label(new Rect(0, 220, 200, 100), "Direction == " + obox.direction.ToString());
				GUI.Label(new Rect(0, 240, 200, 100), "Dirreceived: " + obox.directionreceived.ToString());
				GUI.Label(new Rect(0, 260, 200, 100), "heavyon: " + obox.heavyon.ToString());
				GUI.Label(new Rect(0, 280, 200, 100), "doingheavy: " + doingheavy.ToString());
				GUI.Label(new Rect(0, 300, 200, 100), "Hcounter: " + heavycounter.ToString());
				
				GUI.Label(new Rect(0, 320, 200, 100), "Shoready: " + obox.junklinready.ToString());
				GUI.Label(new Rect(0, 340, 200, 100), "Axeready: " + obox.axekickready.ToString());
				GUI.Label(new Rect(0, 360, 200, 100), "DKready: " + obox.Dkickready.ToString());
				
				GUI.Label(new Rect(0, 380, 200, 100), "doingaxe: " + obox.doingaxe.ToString());
				GUI.Label(new Rect(0, 400, 200, 100), "junkcombo: " + obox.junkcombo.ToString());
				
				GUI.Label(new Rect(0, 420, 200, 100), "axecombo: " + obox.axecombo.ToString());
				GUI.Label(new Rect(0, 440, 200, 100), "LDB: " + leftdashbool.ToString());
				GUI.Label(new Rect(0, 460, 200, 100), "axisde: " + axisdelay.ToString());
				GUI.Label(new Rect(0, 480, 200, 100), "LDT: " + leftdashtimer.ToString());
				GUI.Label(new Rect(0, 500, 200, 100), "dashticket: " + dashticket.ToString());
				GUI.Label(new Rect(0, 520, 200, 100), "Time: " + Time.timeScale.ToString());
				GUI.Label(new Rect(0, 540, 200, 100), "dtimer: " + deathtimer.ToString());
				
			}
			
		#endregion
			
			//healthbars------------------------------------------------------------------
			GUI.color = Color.white;
			
			GUI.DrawTexture(new Rect(134 + HPoffset, 
				38 + HPoffset2, 346, 12), graybar, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect(134 + HPoffset, 
				38 + HPoffset2, 346 * (redhealth / maxHealth), 12), healthred, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect(134 + HPoffset, 
				38 + HPoffset2, 346 * (yellowhealth / maxHealth), 12), healthyellow, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect(134 + HPoffset, 
				38 + HPoffset2, 346 * (greenhealth / bonushealth), 12), healthgreen, ScaleMode.StretchToFill);	
			
			
			//frame
			GUI.DrawTexture(new Rect(0 + HPoffset, 
				0 + HPoffset2, 492, 140), healthframe, ScaleMode.StretchToFill);
			
			
			//CHI bar
			#region Balls bar
			
			if (balls >= 0 && balls < 1)
			{
				GUI.DrawTexture(new Rect(146 + HPoffset , 60 + HPoffset2, 22 * (ballsbar1 / 1), 8), chiball, ScaleMode.StretchToFill);
			}
			
			if (balls >= 1 && balls < 2)
			{
				GUI.DrawTexture(new Rect(146 + HPoffset , 60 + HPoffset2, 22 * (ballsbar1 / 1), 8), chiball, ScaleMode.StretchToFill);
				GUI.DrawTexture(new Rect(174 + HPoffset , 60 + HPoffset2, 22 * (ballsbar2 / 1), 8), chiball, ScaleMode.StretchToFill);
			}

			if (balls >= 2 && balls < 3)
			{
				GUI.DrawTexture(new Rect(146 + HPoffset , 60 + HPoffset2, 22 * (ballsbar1 / 1), 8), chiball, ScaleMode.StretchToFill);
				GUI.DrawTexture(new Rect(174 + HPoffset , 60 + HPoffset2, 22 * (ballsbar2 / 1), 8), chiball, ScaleMode.StretchToFill);
				GUI.DrawTexture(new Rect(202 + HPoffset , 60 + HPoffset2, 22 * (ballsbar3 / 1), 8), chiball, ScaleMode.StretchToFill);
			}

			if (balls >= 3 && balls < 4)
			{
				GUI.DrawTexture(new Rect(146 + HPoffset , 60 + HPoffset2, 22 * (ballsbar1 / 1), 8), chiball, ScaleMode.StretchToFill);
				GUI.DrawTexture(new Rect(174 + HPoffset , 60 + HPoffset2, 22 * (ballsbar2 / 1), 8), chiball, ScaleMode.StretchToFill);
				GUI.DrawTexture(new Rect(202 + HPoffset , 60 + HPoffset2, 22 * (ballsbar3 / 1), 8), chiball, ScaleMode.StretchToFill);
				GUI.DrawTexture(new Rect(230 + HPoffset , 60 + HPoffset2, 22 * (ballsbar4 / 1), 8), chiball, ScaleMode.StretchToFill);
			}

			if (balls >= 4)
			{
				GUI.DrawTexture(new Rect(146 + HPoffset , 60 + HPoffset2, 22 * (ballsbar1 / 1), 8), chiball, ScaleMode.StretchToFill);
				GUI.DrawTexture(new Rect(174 + HPoffset , 60 + HPoffset2, 22 * (ballsbar2 / 1), 8), chiball, ScaleMode.StretchToFill);
				GUI.DrawTexture(new Rect(202 + HPoffset , 60 + HPoffset2, 22 * (ballsbar3 / 1), 8), chiball, ScaleMode.StretchToFill);
				GUI.DrawTexture(new Rect(230 + HPoffset , 60 + HPoffset2, 22 * (ballsbar4 / 1), 8), chiball, ScaleMode.StretchToFill);
				GUI.DrawTexture(new Rect(258 + HPoffset , 60 + HPoffset2, 22 * (ballsbar5 / 1), 8), chiball, ScaleMode.StretchToFill);
			}

//			if (balls >= 5)
//			{
//				GUI.DrawTexture(new Rect(142 + HPoffset + ballsoffset, 56 + HPoffset2, 25 * (ballsbar1 / 1), 11), chiball, ScaleMode.StretchToFill, true, 10);
//				GUI.DrawTexture(new Rect(171 + HPoffset + ballsoffset, 56 + HPoffset2, 25 * (ballsbar2 / 1), 11), chiball, ScaleMode.StretchToFill, true, 10);
//				GUI.DrawTexture(new Rect(200 + HPoffset + ballsoffset, 56 + HPoffset2, 25 * (ballsbar3 / 1), 11), chiball, ScaleMode.StretchToFill, true, 10);
//				GUI.DrawTexture(new Rect(229 + HPoffset + ballsoffset, 56 + HPoffset2, 25 * (ballsbar4 / 1), 11), chiball, ScaleMode.StretchToFill, true, 10);
//				GUI.DrawTexture(new Rect(258 + HPoffset + ballsoffset, 56 + HPoffset2, 25 * (ballsbar5 / 1), 11), chiball, ScaleMode.StretchToFill, true, 10);
//			}
			#endregion
			
			
			//block bar
			
			if (showblockbar)
			{
				Vector3 mainpos = Camera.main.WorldToScreenPoint(transform.position);
				GUI.color = trans1;
				if (blockbar < maxblockbar)
				GUI.DrawTexture(new Rect(mainpos.x + blockoffset, 
										 Screen.height - mainpos.y - 60 + blockoffset2, 
										 50 *(blockbar / maxblockbar), 5), healthred, ScaleMode.StretchToFill, true, 10);
				if (blockbar == maxblockbar)
				GUI.DrawTexture(new Rect(mainpos.x + blockoffset, 
										 Screen.height - mainpos.y - 60 + blockoffset2, 
										 50 *(blockbar / maxblockbar), 5), healthyellow, ScaleMode.StretchToFill, true, 10);
			}
			
			
			//off screen pointer
			if (transform.position.y >= 320)
			{
				float cameraDif = Camera.main.transform.position.y - transform.position.y;
				Vector3 pos = Camera.main.WorldToScreenPoint(new Vector3
					(transform.position.x, transform.position.y, cameraDif)); 
				GUI.color = Color.white;
				GUI.DrawTexture(new Rect(pos.x, 
					10, 30, 30), afrohead, ScaleMode.StretchToFill, true, 0f);
				
			}
			
			#region Afuroheads
			GUI.color = Color.white;
	
			if (lives == 1)
			{	
				GUI.DrawTexture(new Rect(140, 
					75, 20, 20), afrohead, ScaleMode.StretchToFill, true, 0f);
			}
	
			if (lives == 2)
			{
				GUI.DrawTexture(new Rect(140, 
					75, 20, 20), afrohead, ScaleMode.StretchToFill, true, 0f);
				GUI.DrawTexture(new Rect(160, 
					75, 20, 20), afrohead, ScaleMode.StretchToFill, true, 0f);
			}
			#endregion
			
			
		}//not endtime|End
	}//END ON GUI
	
	
	IEnumerator ToggleHPshake()
	{
		HPshake = true;
		
		yield return new WaitForSeconds(0.8f);
		
		HPshake = false;
		HPoffset = 0;
		HPoffset2 = 0;
		
	}
	
	IEnumerator Toggleballshake()
	{
		ballshake = true;
		
		yield return new WaitForSeconds(0.8f);
		
		ballshake = false;
		ballsoffset = 0;
		
	}
	
	IEnumerator Toggleblockshake()
	{
		blockshake = true;
		
		yield return new WaitForSeconds(0.8f);
		
		blockshake = false;
		blockoffset = 0;
		blockoffset2 = 0;
		
	}
	
	void combocheat()
	{
		obox.combocounter++;	
		obox.combotimer = 0;
	}
	
	
}//END ALL
