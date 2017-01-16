using UnityEngine;
using System.Collections;

public class karatemanmirror : MonoBehaviour {

	public pausemenu pausescript;
	private int health;
	private float lifetime = 0;
	private float randomatkreset;
	private bool gotrandom = false;
	
	public exSprite karatesprite;
	public exSpriteAnimation karateanim;
	public GameObject realkarateman;
	public Player karatescript;
	public Fakeobox obox;
	public karatemanmirror otherclone;
	
	private float jumpbounce = 0, gravity = 0;
	private float regulargrav = -23f, regularjb = 1120;
	public bool dbzmode = false, gettinghit = false, timedout = false;
	public float dbzmodetimer, hittimer;
	
	public float scalereducer = 0;
	
	public bool leftside = false, rightside = false, topside = false, botside = false, facingright = true;
	public bool mousetotheright;
	
	public bool atkready;
	private float atkCD;
	private Color ghost = new Color(0, 0, 0, 0.4f);
	private Color ghost2 = new Color(0, 0, 0, 0.9f);
	
	public int attackcounter = 1;
	public GameObject smackanimu, enemysmack, blackdeath;
	public AudioClip smacksound;
	
	void Start () {
		
		gravity = regulargrav;
		jumpbounce = regularjb;
		pausescript = (pausemenu)GameObject.FindGameObjectWithTag("MainCamera").GetComponent("pausemenu");
		realkarateman = GameObject.FindGameObjectWithTag("Player");
		karatescript = (Player)realkarateman.GetComponent("Player");
		
		health = (int)karatescript.maxHealth / 2;
		
		if (PlayerPrefs.GetString("CloneT2") == "white")
		{
			if (!karatescript.rightclone && !karatescript.leftclone && !karatescript.topclone && !karatescript.botclone ||
				karatescript.leftclone && !karatescript.rightclone)
			{
				rightside = true;
				karatescript.rightclone = true;
				return;
			}
			if (karatescript.rightclone && !karatescript.leftclone && !karatescript.topclone && !karatescript.botclone)
			{
				leftside = true;
				karatescript.leftclone = true;
				return;
			}
			if (karatescript.leftclone && karatescript.rightclone && !karatescript.topclone && !karatescript.botclone)
			{
				botside = true;
				karatescript.botclone = true;
				return;
			}
			if (karatescript.botclone && karatescript.leftclone && karatescript.rightclone && !karatescript.topclone)
			{
				topside = true;
				karatescript.topclone = true;
				return;
			}
			
		}
	
	}
	
	void FixedUpdate () {
		
		if (!pausescript.pausemenud && !pausescript.paused && !pausescript.playerpause)
		{
			if (PlayerPrefs.GetString("CloneT2") != "white")
			{
				//jumpbounce
				if (transform.position.y <= -325)
				{
					gameObject.rigidbody.velocity = new Vector3(Random.Range(-300, 300), jumpbounce, 0);	
				}
				
				//gravity
				if (transform.position.y > -325)
					gameObject.rigidbody.velocity += new Vector3(0, gravity, 0);
			
			}
			//DBZ mode---------------------------------------------------
				if (dbzmode)
				{
					dbzmodetimer += Time.fixedDeltaTime;
					Vector3 tempstore = rigidbody.velocity;
					rigidbody.velocity = new Vector3(0, -6.8f, 0);
					gravity = -6.8f;
				
//					if (attackcounter > 3)
//					{
//						StartCoroutine( CancelDBZwithtime(1, tempstore) );
//					}
					
					if (dbzmodetimer - Time.fixedDeltaTime > 0.5f)
					{
						CancelDBZ(tempstore);
						
					}
			
				}
		}//!paused	
		
		
	}

	void Update () {
		
		//time out
		lifetime += Time.deltaTime;
		if (lifetime >= 15 && PlayerPrefs.GetString("CloneT1") != "white")
			timedout = true;
		
		if (timedout)
		{
			cancelkaratebools();
			scalereducer -= 0.0001f;
				transform.localScale = new Vector3 (transform.localScale.x + scalereducer,
					transform.localScale.y + scalereducer, transform.localScale.z);
				if (transform.localScale.x < 0)
					Destroy(this.gameObject);
		
		}
		
		//death
		if (health <= 0 && PlayerPrefs.GetString("CloneT1") != "black")
		{
			cancelkaratebools();
			Instantiate(blackdeath, transform.position, transform.rotation);
			Destroy(this.gameObject);
		}
		
		//Karateman dies
		if (karatescript.yellowhealth <= 0)
		{
			cancelkaratebools();
			Instantiate(blackdeath, transform.position, transform.rotation);
			Destroy(this.gameObject);
		}
		
		//stage ends
		if (karatescript.endtime)
		{
			cancelkaratebools();	
			Instantiate(blackdeath, transform.position, transform.rotation);
			Destroy(this.gameObject);
		}
		
		
		//attack anims
			if (atkready && !gettinghit && PlayerPrefs.GetString("CloneT2") != "white")
			{	
				if (attackcounter >= 3)
					{
						atkready = false;
						attackcounter = 0;
					}
					if (attackcounter == 2)
					{
						karateanim.Play("Afroattack2");
						attackcounter++;
						atkready = false;
					}
					if (attackcounter == 1)
					{
						karateanim.Play("Afroattack1");
						attackcounter++;
						atkready = false;
							
					}
					if (attackcounter == 0)
					{
						karateanim.Play("Afroattack3");
						attackcounter++;
						atkready = false;	
					}

			}
		
		//restrictions && states
		
		//color
		if (PlayerPrefs.GetString("CloneT1") == "black")
			karatesprite.color = ghost2;
		if (PlayerPrefs.GetString("CloneT1") != "black")
			karatesprite.color = ghost;
		
		//fuck 3d
			if (transform.position.z != -100)
				transform.position = new Vector3(transform.position.x, transform.position.y, -100);
		
		//side screen shit
			if (transform.position.x <= -624)
				{	
					if (gameObject.tag != "Dead")
					{
						transform.position = new Vector3(-623,
						transform.position.y, transform.position.z);
						gameObject.rigidbody.velocity += new Vector3 (170,0,0);
					}
				}
			if (transform.position.x >= 571 )
				{	
					if (gameObject.tag != "Dead")
					{
						transform.position = new Vector3(570,
						transform.position.y, transform.position.z);
						gameObject.rigidbody.velocity += new Vector3 (-170,0,0);
					}
				
				}
		
		if (gettinghit)
		{
			hittimer += Time.deltaTime;
			if (hittimer > 0.7f)
			{
				gettinghit = false;
				hittimer = 0;	
			}
			
		}
		
		//attack cooldowns
		
		//manual attack
		if (PlayerPrefs.GetString("CloneT2") == "white")
		{
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
		}
		
		if (!gotrandom)
		{
			randomatkreset = Random.Range(1,4);
			gotrandom = true;
		}
		
		//Clone auto attack
		if (PlayerPrefs.GetString("CloneT2") != "white")
		{
				//Delay after LAST attack
			if (!atkready && attackcounter == 1)
			{
				atkCD += Time.deltaTime;
			if (atkCD >= randomatkreset)
				{
					atkready = true;
					atkCD = 0;
					gotrandom = false;
				}
			}
			
			//Delay after FIRST attack
			if (!atkready && attackcounter == 2)
			{
				atkCD += Time.deltaTime;
			if (atkCD >= 0.4f)
				{
					atkready = true;
					atkCD = 0;
				}
			}
			
			//Delay after SECOND attack
			if (!atkready && attackcounter == 3)
			{
				atkCD += Time.deltaTime;
			if (atkCD >= 0.5f)
				{
					atkready = true;
					atkCD = 0;
				}
			}
			
			
		}
			
		
		//T2 white's clone
		if (PlayerPrefs.GetString("CloneT2") == "white")
		{
			if (leftside)
			{
				transform.position = new Vector3
					(realkarateman.transform.position.x - 150, realkarateman.transform.position.y, realkarateman.transform.position.z);
				if (mousetotheright && !facingright)
				{
					karatesprite.HFlip();	
					facingright = true;
				}
				if (!mousetotheright && facingright)
				{
					karatesprite.HFlip();	
					facingright = false;
				}
				
			}
			
			if (rightside)
			{
				transform.position = new Vector3
					(realkarateman.transform.position.x + 150, realkarateman.transform.position.y, realkarateman.transform.position.z);
				if (mousetotheright && !facingright)
				{
					karatesprite.HFlip();	
					facingright = true;
				}
				if (!mousetotheright && facingright)
				{
					karatesprite.HFlip();	
					facingright = false;
				}
			}
			
			if (botside)
			{
				transform.position = new Vector3
					(realkarateman.transform.position.x, realkarateman.transform.position.y - 150, realkarateman.transform.position.z);
				if (mousetotheright && !facingright)
				{
					karatesprite.HFlip();	
					facingright = true;
				}
				if (!mousetotheright && facingright)
				{
					karatesprite.HFlip();	
					facingright = false;
				}
			}
			
			if (topside)
			{
				transform.position = new Vector3
					(realkarateman.transform.position.x, realkarateman.transform.position.y + 150, realkarateman.transform.position.z);
				if (mousetotheright && !facingright)
				{
					karatesprite.HFlip();	
					facingright = true;
				}
				if (!mousetotheright && facingright)
				{
					karatesprite.HFlip();	
					facingright = false;
				}
			}
			
			
			if (Input.GetButtonDown("fight") && atkready && !gettinghit)
			{
				if (attackcounter >= 3)
					{
						attackcounter = 0;
						atkready = false;
						obox.dmgready = true;
					}
					if (attackcounter == 2)
					{
						karateanim.Play("Afroattack2");
						attackcounter++;
						atkready = false;
						obox.dmgready = true;
					}
					if (attackcounter == 1)
					{
						karateanim.Play("Afroattack1");
						attackcounter++;
						atkready = false;
						obox.dmgready = true;	
					}
					if (attackcounter == 0)
					{
						karateanim.Play("Afroattack3");
						attackcounter++;
						atkready = false;	
						obox.dmgready = true;
					}
				
			}
		}
	
	}
	
	void OnTriggerEnter (Collider otherObject)
	{
		if (otherObject.tag == "Enemy")
			{
				Punk1 enemyscript = (Punk1)otherObject.gameObject.GetComponent("Punk1");
				if (enemyscript.canhityou)
				{
					enemyscript.punkattack();
					GetHit();
					health--;
				}
			}
		
		if (otherObject.tag == "Enemy2")
			{
				Punk2 enemyscript = (Punk2)otherObject.gameObject.GetComponent("Punk2");
				if (enemyscript.canhityou)
				{
					
					GetHit();
					health--;
				}
				
				
			}
		
		if (otherObject.tag == "Enemy3")
			{
				Punk3 enemyscript = (Punk3)otherObject.gameObject.GetComponent("Punk3");
				if (enemyscript.canhityou)
				{
					enemyscript.Punkattack();
					GetHit();
					health--;
				}
				
				
			}
		
		if (otherObject.tag == "Bullet")
		{
			GetHit();
			health--;	
			Destroy(otherObject.gameObject);
		}
		
		if (otherObject.tag == "Bomb")
		{
			zoomzoom enemyscript = (zoomzoom)otherObject.gameObject.GetComponent("zoomzoom");
			enemyscript.explode();
			
		}
		
		if (otherObject.tag == "Hardcore" || otherObject.tag == "Hardcore2" || otherObject.tag == "Hardcore3" ||
			otherObject.tag == "Hurtbox")
			{
				GetHit();
				health--;
			}
		
		
		
	}
	
	void GetHit()
	{
		if (!gettinghit)
		gettinghit = true;
		Instantiate (enemysmack, transform.position, transform.rotation);
		StartCoroutine ( shakkee () );
		audio.PlayOneShot(smacksound);
		karateanim.Play("Damage");
		
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
		
		karatesprite.offset = new Vector3(pospos.x, pospos.y, pospos.z);
		
	}
	
	IEnumerator Opendmgwindow(float time)
	{
		obox.dmgready = true;
		
		yield return new WaitForSeconds(time);
		
		obox.dmgready = false;
		
	}
	
	public void CancelDBZ (Vector3 tt)
	{
		dbzmode = false;
		dbzmodetimer = 0f;
//		transform.localScale = new Vector3(0.34f, 1f, 0.34f);
		gravity = regulargrav;
		jumpbounce = regularjb;
		rigidbody.velocity = tt;
		
	}
	
	void cancelkaratebools()
	{
		if (leftside)
			karatescript.leftclone = false;
		if (rightside) 
			karatescript.rightclone = false;
		if (topside)
			karatescript.topclone = false;
		if (botside)
			karatescript.botclone = false;
		
	}
	
	IEnumerator CancelDBZwithtime(float time, Vector3 returnvelo)
	{
		yield return new WaitForSeconds(time);
		
		CancelDBZ(returnvelo);
		
	}
	
	void OnGUI()
	{
		GUI.color = Color.red;
		GUI.Label(new Rect(0, 240, 200, 100), "atkready: " + atkready.ToString());
		GUI.Label(new Rect(0, 260, 200, 100), "dmgready: " + obox.dmgready.ToString());
		GUI.Label(new Rect(0, 280, 200, 100), "gettinghit: " + gettinghit.ToString());
		
	}
}
