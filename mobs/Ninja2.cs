using UnityEngine;
using System.Collections;

public class Ninja2 : MonoBehaviour {
	
	public GameObject parentshit, naltoknife, tinyshu, shugeneric, Karateman;
	public Player playerscript;
	
	//stats
	public exSprite ninjasprite;
	public exSpriteAnimation ninanim;
	public int itemroll, chancetodrop;
	public float health = 5, maxhealth = 5;
	float deadonfloor, dbzmodetimer;
	public bool dbzmode, facingleft = true, gotdmgbybomb = false, gotdmgbyforce = false, gotdmgbytackle = false, canhityou = true;
	private float dmgtypereset = 0;
	
	public bool bombed = false, tackled = false;
	public bool poisoned = false;
	private int poisonticks = 0;
	private float bombtimer = 0, poisontimer = 0, wallbouncetimer = 0;
	public bool wallbounce = false;
	
	public bool taunted = false, tauntable = true;
	private float taunttimer, tauntCD;
	
	//shooting
	private int randomforshoot;
	private bool gotrandomforshoot = false, purpleready = true;
	private float shootCD, purpleCD;
	
	//running
	private bool gotrandomforrun = false;
	private int randomforrun;
	private float moveawaytimer = 0;
	
	//behaviors
	private bool gotrandomforloc = false;
	public bool gothitbydkick = false;
	private int randomforloc;
	private Vector3 gravity = new Vector3(0, -6, 0);
	private Vector3 regulargrav = new Vector3(0, -6, 0);
	private Vector3 gravity2 = new Vector3(0, -8, 0);
	private Vector3 jumpbounce = new Vector3(0, 550, 0);
	private bool escapeready = true;
	private float escapeCD;
	bool doingaoe = false;
	
	//other
	public GameObject chiball, sushi, coin, explosion;
	public AudioClip tele;
	private Vector3[] telelocations = new Vector3[7]
									{ new Vector3(0, 100, -100),
									  new Vector3(-594, 210, -100), 
									  new Vector3(550, 210, -100),
									  new Vector3(-594, 0, -100),
									  new Vector3(550, 0, -100),
									  new Vector3(-594, 0, -100),
									  new Vector3(550, 0, -100),	};
	
	//item
	private bool spawnonce = false;
	
	// Use this for initialization
	void Start () {
		
		Karateman = GameObject.FindGameObjectWithTag("Player");
		playerscript = (Player)Karateman.GetComponent("Player");
		
//		defup();
//		defdown();
	}
	
	void FixedUpdate () {
		
		
		//semi intelligent tele evade
		if (!gotrandomforrun)
			getrandomforrun();
		if (tag == "Ninja2" && moveawaytimer - Time.deltaTime >= randomforrun && !doingaoe)
		{
			teleevade();
		}	
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!doingaoe && !dbzmode)
		{
			shootCD += Time.deltaTime;
			moveawaytimer += Time.deltaTime;
		}
		
		if (!gotrandomforshoot)
			getrandomforshoot();
		if (shootCD > randomforshoot && tag == "Ninja2" && !dbzmode && !doingaoe)
		{
			chooseattack();	
			shootCD = 0;
			gotrandomforshoot = false;
		}
		
			//jumpbounce
			if (transform.position.y <= -325)
				teleevade();
			
			
			
			//gravity
			if (transform.position.y > -325 && transform.position.y < 450)
				gameObject.rigidbody.velocity = gravity;
				
			if (transform.position.y >= 450)
				gameObject.rigidbody.velocity = gravity2;
		
		//vertical
		if (transform.position.y <= -600 || transform.position.y >= 600)
			Destroy(this.gameObject);
		
		
		//fuck 3d
			if (transform.position.z != -100)
				transform.position = new Vector3(transform.position.x, transform.position.y, -100);
		
		//facing
			if (transform.position.x > 0 && !facingleft)
			{
				ninjasprite.HFlip();
				facingleft = true;
			}
			if (transform.position.x < 0 && facingleft)
			{
				ninjasprite.HFlip();
				facingleft = false;
			}
		
		//taunt
			if (GameObject.FindGameObjectWithTag("Fake") != null && tauntable)
				taunted = true;
			if (GameObject.FindGameObjectWithTag("Fake") == null)
			{
				taunted = false;
				taunttimer = 0;
				tauntCD = 0;
			}
		
		
		//DBZ mode
			if (dbzmode)
			{
				if (health != 0 && health <= 3 && escapeready)
				DBZescape();
			
				dbzmodetimer += Time.deltaTime;
				Vector3 tempstore = rigidbody.velocity;
//				punkanim.Play("Stomachache");
				shaking();
//				transform.rotation = Quaternion.Euler(Vector3.zero);
				rigidbody.velocity = Vector3.zero;
			
				if (dbzmodetimer - Time.deltaTime > 0.5f)
				{
					CancelDBZ(tempstore);
				}
			}
		

		
		if (taunted && tag != "Dead")
			{
				ninjasprite.color = Color.magenta;
				taunttimer += Time.deltaTime;
				if (taunttimer > 3)
				{
					taunted = false;
					taunttimer = 0;
					tauntable = false;
					ninjasprite.color = Color.white;
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
			ninjasprite.color = Color.red;
			
			if (bombtimer >= 6)
			{
				bombed = false;
				bombtimer = 0;
				ninjasprite.color = Color.white;
			}
			
		}
		
		if (poisoned)
		{
			poisontimer += Time.deltaTime;
			ninjasprite.color = Color.green;
			
			if (poisontimer >= 1)
			{
				poisonticks++;	
				health--;
				poisontimer = 0;
			}
			
			if (poisonticks >= 5)
			{
				poisoned = false;
				poisontimer = 0;
				poisonticks = 0;
				ninjasprite.color = Color.white;
			}
			
		}
		
		//death and flyaway
		if (health <= 0)
			FlyAway();
		if (tag == "Dead")
		{
			transform.Rotate(new Vector3(0,0,1) * 400);
			if (transform.position.y <= -300)
			{
				deadonfloor += Time.deltaTime;
				transform.position = new Vector3(transform.position.x,
												 transform.position.y, -5);
				transform.rotation = Quaternion.Euler(new Vector3(
									0, 0, 90));
				gameObject.rigidbody.velocity = Vector3.zero;
				gameObject.collider.enabled = false;
				if (deadonfloor - Time.deltaTime >= 30)
					Destroy(this.gameObject);
				if (transform.localScale.x > 2)
					Destroy(this.gameObject);
			}
		}
		
		if (!escapeready)
		{
			escapeCD += Time.deltaTime;	
			if (escapeCD >= 4)
			{
				escapeready = true;
				escapeCD = 0;
			}
		}
		
		if (!purpleready)
		{
			purpleCD += Time.deltaTime;
			if (purpleCD >= 2)
			{
				purpleready = true;	
				purpleCD = 0;
			}
		}
		
		//DMG type reset
		if (gotdmgbybomb)
		{
			dmgtypereset += Time.deltaTime;	
			if (dmgtypereset > 0.9f)
			{
				gotdmgbybomb = false;	
				dmgtypereset = 0;
			}
			
		}
		
		if (gotdmgbyforce)
		{
			dmgtypereset += Time.deltaTime;	
			if (dmgtypereset > 0.5f)
			{
				gotdmgbyforce = false;	
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
	
	}//end update
	
	public Vector3 Getdiff()
	{
		Vector3 diff = transform.position - Karateman.transform.position; 
		return diff;
	}
	
	public void Takedamage(float amount)
	{
		health -= amount; 	
		
	}
	
	void getrandomforshoot()
	{
		randomforshoot = Random.Range(4, 6);
		gotrandomforshoot = true;
	}
	
	void getrandomforrun()
	{
		randomforrun = Random.Range(6, 9);
		gotrandomforrun = true;
	}
	
	void getrandomforloc()
	{
		randomforloc = Random.Range(1, 7);
		gotrandomforloc = true;
	}
	
	public void FlyAway () 
	{
		if (tag != "Dead" && Application.loadedLevelName != "devmode")
		{
			playerscript.ninjadefeated++;
			playerscript.XPgained += 50;
		}

		chancetodrop = Random.Range (1,5);
		if (chancetodrop == 1 && tag != "Dead")
		SpawnPowerup();
		if (bombed)
			Instantiate (explosion, transform.position, transform.rotation);
		gameObject.tag = "Dead";
		rigidbody.velocity = new Vector3 
				(Random.Range(-550,550), Random.Range(-200,350), 0);

	}
	
	IEnumerator DelayedFlyAway ()
	{
		yield return new WaitForSeconds(1);
		
		FlyAway();
	}
	
	public void DelayedFly()
	{
		rigidbody.velocity = Vector3.zero;
		StartCoroutine( DelayedFlyAway () );
	}
	
	void chooseattack()
	{
		int choice = Random.Range(1,2);
		if (choice <= 2)
			shootone(800);
		if (choice >= 4)
			shootmult();
		if (choice == 3)
			aoemove();
	}
	
	public void Bleedcash()
	{
		int roll = Random.Range(1, 3);
		
		if (tackled && PlayerPrefs.GetString("TackleT2") == "black")
		{
			Instantiate(coin, transform.position, Quaternion.Euler(Vector3.zero));
			Instantiate(coin, transform.position, Quaternion.Euler(Vector3.zero));
			Instantiate(coin, transform.position, Quaternion.Euler(Vector3.zero));
		}
		
		if (roll == 1)
		Instantiate(coin, transform.position, Quaternion.Euler(Vector3.zero));
		
		if (roll == 2)
		{
			Instantiate(coin, transform.position, Quaternion.Euler(Vector3.zero));
			Instantiate(coin, transform.position, Quaternion.Euler(Vector3.zero));
		}
		
		if (roll == 3)
		{
			Instantiate(coin, transform.position, Quaternion.Euler(Vector3.zero));
			Instantiate(coin, transform.position, Quaternion.Euler(Vector3.zero));
			Instantiate(coin, transform.position, Quaternion.Euler(Vector3.zero));
		}
		
	}
	
	void SpawnPowerup () 
	{
		
		itemroll = Random.Range(1,10);
		

		if (itemroll < 3)
		{
			Instantiate(sushi, transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));
		}
		if (itemroll >= 6)
		{
			Instantiate(chiball, transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));
		}
		
		
		
	}
	
	public void CancelDBZ (Vector3 tt)
	{
		dbzmode = false;
		dbzmodetimer = 0f;
		
	}
	
	public void DBZescape()
	{
		dbzmode = false;
		dbzmodetimer = 0;
		audio.PlayOneShot(tele);
		
		teleport(telelocations[whichescape()]);
		
		StartCoroutine( delayshoot() );
		StartCoroutine( defensiveshoot() );
		escapeready = false;
	}
	
	void shaking()
	{
		StartCoroutine ( shakee () );
	}
	
	IEnumerator shakee()
	{
		Vector3 pospos = ninjasprite.offset;
		
		ninjasprite.offset = new Vector3(pospos.x + 1, pospos.y, pospos.z);
		
		yield return new WaitForSeconds(0.05f);
		
		ninjasprite.offset = new Vector3(pospos.x - 1, pospos.y, pospos.z);
		
		yield return new WaitForSeconds(0.05f);
		
		ninjasprite.offset = new Vector3(pospos.x + 1, pospos.y, pospos.z);
		
		yield return new WaitForSeconds(0.05f);
		
		ninjasprite.offset = new Vector3(pospos.x - 1, pospos.y, pospos.z);
		
		yield return new WaitForSeconds(0.05f);
		
		ninjasprite.offset = new Vector3(pospos.x + 1, pospos.y, pospos.z);
		
		yield return new WaitForSeconds(0.05f);
		
		ninjasprite.offset = new Vector3(pospos.x, pospos.y, pospos.z);
		
		
	}
	
	int whichescape()
	{
		if (transform.position.x > 0)
			return 1;
		if (transform.position.x < 0)
			return 2;
			
		return 0;
	}
	
	public void teleevade()
	{
		telelocations[5] = new Vector3(-594, Karateman.transform.position.y, -100);
		telelocations[6] = new Vector3(550, Karateman.transform.position.y, -100);
		if (!gotrandomforloc)
			getrandomforloc();
		
		teleport(telelocations[randomforloc]);
		StartCoroutine ( defensiveshoot() );
		moveawaytimer = 0f;
		gotrandomforrun = false;
		gotrandomforloc = false;
	}
	
	public void teleport(Vector3 newloc)
	{	
		StartCoroutine ( Teleportanim (newloc) );	
	}
	
	public void teleportafterAOE(Vector3 newloc)
	{	
		StartCoroutine ( Teleportanim2 (newloc) );	
	}
	
	IEnumerator Teleportanim(Vector3 newlocation)
	{
		ninanim.Play("nindisappear");
		collider.enabled = false;
		StartCoroutine (delayoff ());
		
		yield return new WaitForSeconds(0.5f);
		
		transform.position = newlocation;
		ninanim.Play("ninappear");
		renderer.enabled = true;
		collider.enabled = true;
		
		
	}
	
	IEnumerator Teleportanim2(Vector3 newlocation)
	{
		ninanim.Play("nindisappear2");
		collider.enabled = false;
		StartCoroutine (delayoff ());
		
		yield return new WaitForSeconds(0.5f);
		
		transform.position = newlocation;
		ninanim.Play("ninappear");
		renderer.enabled = true;
		collider.enabled = true;
		
		
	}
	
	IEnumerator delayoff()
	{
		yield return new WaitForSeconds(0.28f);
		renderer.enabled = false;
	}
	
	
	void shootone(float speed)
	{
		ninanim.Play("ninthrow");
		GameObject fast = (GameObject)Instantiate(naltoknife, 
			new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
		Naltoknife knifescript = (Naltoknife)fast.gameObject.GetComponent("Naltoknife");
		knifescript.parent = this.gameObject;
		knifescript.speed = speed;
	}
	
	IEnumerator delayshoot()
	{
		yield return new WaitForSeconds(0.6f);
		shootone(2000);
	}
	
	void shootmult()
	{
		ninanim.Play("ninthrow");
		if (Karateman.transform.position.x < transform.position.x)
		{
			GameObject botleft = (GameObject)Instantiate(shugeneric, transform.position, transform.rotation);
			botleft.rigidbody.velocity = new Vector3 (-400, -400, 0);
		
			GameObject leftleft = (GameObject)Instantiate(shugeneric, transform.position, transform.rotation);
			leftleft.rigidbody.velocity = new Vector3 (-400, 0, 0);
			
			GameObject topleft = (GameObject)Instantiate(shugeneric, transform.position, transform.rotation);
			topleft.rigidbody.velocity = new Vector3 (-400, 400, 0); 
			
		}
		if (Karateman.transform.position.x >= transform.position.x)
		{
			GameObject topright = (GameObject)Instantiate(shugeneric, transform.position, transform.rotation);
			topright.rigidbody.velocity = new Vector3 (400, 400, 0);
		
			GameObject rightright = (GameObject)Instantiate(shugeneric, transform.position, transform.rotation);
			rightright.rigidbody.velocity = new Vector3 (400, 0, 0);
		
			GameObject rightbot = (GameObject)Instantiate(shugeneric, transform.position, transform.rotation);
			rightbot.rigidbody.velocity = new Vector3 (400, -400, 0);
				
		}
	}
	
	public void aoemove()
	{
		StartCoroutine ( aoeshot () );		
	}
	
	public void TakeDamage(float amount)
	{
		health -= amount;	
		
	}
	
	IEnumerator aoeshot()
	{
		doingaoe = true;
		Vector3 anchor = transform.position;
		Vector3 oldvelo = rigidbody.velocity;
		
		
		teleport(telelocations[0]);

		audio.PlayOneShot(tele);
		rigidbody.velocity = Vector3.zero;
		
		yield return new WaitForSeconds(0.5f);
		
		ninanim.Play("ninAOE");
		
		yield return new WaitForSeconds(0.7f);
		
		shootAOE();
		
		yield return new WaitForSeconds(0.5f);
		
		
		teleportafterAOE(anchor);
		rigidbody.velocity = oldvelo;
		doingaoe = false;
	}
	
	void shootAOE()
	{
		GameObject topleft = (GameObject)Instantiate(shugeneric, transform.position, transform.rotation);
		topleft.rigidbody.velocity = new Vector3 (-300, 300, 0); 
		
		GameObject toptop = (GameObject)Instantiate(shugeneric, transform.position, transform.rotation);
		toptop.rigidbody.velocity = new Vector3 (0, 300, 0);
		
		GameObject topright = (GameObject)Instantiate(shugeneric, transform.position, transform.rotation);
		topright.rigidbody.velocity = new Vector3 (300, 300, 0);
		
		GameObject rightright = (GameObject)Instantiate(shugeneric, transform.position, transform.rotation);
		rightright.rigidbody.velocity = new Vector3 (300, 0, 0);
		
		GameObject rightbot = (GameObject)Instantiate(shugeneric, transform.position, transform.rotation);
		rightbot.rigidbody.velocity = new Vector3 (300, -300, 0);
		
		GameObject botbot = (GameObject)Instantiate(shugeneric, transform.position, transform.rotation);
		botbot.rigidbody.velocity = new Vector3 (0, -300, 0);
		
		GameObject botleft = (GameObject)Instantiate(shugeneric, transform.position, transform.rotation);
		botleft.rigidbody.velocity = new Vector3 (-300, -300, 0);
		
		GameObject leftleft = (GameObject)Instantiate(shugeneric, transform.position, transform.rotation);
		leftleft.rigidbody.velocity = new Vector3 (-300, 0, 0);
		
	}
	
	IEnumerator defensiveshoot()
	{
		yield return new WaitForSeconds(0.5f);
		
		if (purpleready)
		{
			if (transform.position.y > 0 )
				defdown();
			if (transform.position.y <= 0)
				defup();
		}
	}
	
	void defup()
	{
		if (transform.position.x > 0)
		{
			GameObject topleft = (GameObject)Instantiate(tinyshu, transform.position, transform.rotation);
			topleft.rigidbody.velocity = new Vector3 (-70, 70, 0); 
			
			GameObject toptop = (GameObject)Instantiate(tinyshu, transform.position, transform.rotation);
			toptop.rigidbody.velocity = new Vector3 (0, 70, 0);
			
			GameObject middle = (GameObject)Instantiate(tinyshu, transform.position, transform.rotation);
			middle.rigidbody.velocity = new Vector3 (-35, 70, 0);
		}
		
		if (transform.position.x < 0)
		{
			GameObject topleft = (GameObject)Instantiate(tinyshu, transform.position, transform.rotation);
			topleft.rigidbody.velocity = new Vector3 (-70, 70, 0); 
			
			GameObject toptop = (GameObject)Instantiate(tinyshu, transform.position, transform.rotation);
			toptop.rigidbody.velocity = new Vector3 (0, 70, 0);
			
			GameObject middle = (GameObject)Instantiate(tinyshu, transform.position, transform.rotation);
			middle.rigidbody.velocity = new Vector3 (35, 70, 0);
		}
		purpleready = false;
	}
	
	void defdown()
	{
		if (transform.position.x > 0)
		{
			GameObject botleft = (GameObject)Instantiate(tinyshu, transform.position, transform.rotation);
			botleft.rigidbody.velocity = new Vector3 (-70, -70, 0);
			
			GameObject botbot = (GameObject)Instantiate(tinyshu, transform.position, transform.rotation);
			botbot.rigidbody.velocity = new Vector3 (0, -70, 0);
			
			GameObject middle = (GameObject)Instantiate(tinyshu, transform.position, transform.rotation);
			middle.rigidbody.velocity = new Vector3 (-35, -70, 0);
		}
		
		if (transform.position.x < 0)
		{
			GameObject middle = (GameObject)Instantiate(tinyshu, transform.position, transform.rotation);
			middle.rigidbody.velocity = new Vector3 (35, -70, 0);
			
			GameObject botbot = (GameObject)Instantiate(tinyshu, transform.position, transform.rotation);
			botbot.rigidbody.velocity = new Vector3 (0, -70, 0);
			
			GameObject rightbot = (GameObject)Instantiate(tinyshu, transform.position, transform.rotation);
			rightbot.rigidbody.velocity = new Vector3 (70, -70, 0);
		}
		
		purpleready = false;
	}
	
}
