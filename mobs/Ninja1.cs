using UnityEngine;
using System.Collections;

public class Ninja1 : MonoBehaviour {
	
	public GameObject parentshit, shuriken, shugeneric, tinyshu, Karateman;
	public Player playerscript;
	
	//stats
	public exSprite ninjasprite;
	public exSpriteAnimation ninanim;
	public float health = 4, maxhealth = 4;
	public int itemroll, chancetodrop;
	float deadonfloor, dbzmodetimer;
	public bool dbzmode;
	bool facingleft = true;
	
	//shooting
	private int randomforshoot;
	private bool gotrandomforshoot = false;
	private float shootCD;
	
	//running
	private bool gotrandomforrun = false;
	private int randomforrun;
	private float moveawaytimer = 0;
	
	//behaviors
	private bool gotrandomforloc = false;
	public bool gothitbydkick = false;
	private int randomforloc;
	private Vector3 gravity = new Vector3(0, -6, 0);
	private Vector3 jumpbounce = new Vector3(0, 1100, 0);
	
	//other
	public GameObject chiball, sushi;
	public AudioClip tele;
	private Vector3[] telelocations = new Vector3[4]
									{ new Vector3(-594, 280, -10), 
									  new Vector3(550, 300, -10),
									  new Vector3(-594, 0, -10),
									  new Vector3(550, 0, -10),	};
	
	//item
	private bool spawnonce = false;
	
	// Use this for initialization
	void Start () {
		
		Karateman = GameObject.FindGameObjectWithTag("Player");
		playerscript = (Player)Karateman.GetComponent("Player");
	
	}
	
	void FixedUpdate () {
		
		
		//semi intelligent tele evade
		if (!gotrandomforrun)
			getrandomforrun();
		if (tag == "Ninja1" && moveawaytimer - Time.deltaTime >= randomforrun)
		{
			teleevade();
		}	
		
	}
	
	// Update is called once per frame
	void Update () {
		
		shootCD += Time.deltaTime;
		moveawaytimer += Time.deltaTime;
		
		if (!gotrandomforshoot)
			getrandomforshoot();
		if (shootCD > randomforshoot && tag == "Ninja1" && !dbzmode)
		{
			shootmult();	
			shootCD = 0;
			gotrandomforshoot = false;
		}
		
			//jumpbounce
			if (transform.position.y <= -325)
				teleevade();
			
			//gravity
			if (transform.position.y > -325)
				gameObject.rigidbody.velocity = gravity;
		
		
		//vertical
		if (transform.position.y <= -600 || transform.position.y >= 600)
			Destroy(this.gameObject);
		
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
		
		//fuck 3d
			if (transform.position.z != -10)
				transform.position = new Vector3(transform.position.x, transform.position.y, -10);
		
		
		//DBZ mode
			if (dbzmode)
			{
				dbzmodetimer += Time.deltaTime;
				Vector3 tempstore = rigidbody.velocity;
				renderer.material.color = Color.red;
//				punkanim.Play("Stomachache");
				shaking();
//				transform.rotation = Quaternion.Euler(Vector3.zero);
				rigidbody.velocity = Vector3.zero;
					
				if (dbzmodetimer - Time.deltaTime > 0.2f)
				{
					CancelDBZ(tempstore);
				}
			}
		
		if (!dbzmode)
			renderer.material.color = Color.white;
		
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
	
	}
	
	public Vector3 Getdiff()
	{
		Vector3 diff = transform.position - Karateman.transform.position; 
		return diff;
	}
	
	void getrandomforshoot()
	{
		randomforshoot = Random.Range(4, 6);
		gotrandomforshoot = true;
	}
	
	void getrandomforrun()
	{
		randomforrun = Random.Range(4, 9);
		gotrandomforrun = true;
	}
	
	void getrandomforloc()
	{
		randomforloc = Random.Range(0, 4);
		gotrandomforloc = true;
	}
	
	public void FlyAway () 
	{
		playerscript.enemydefeated+=1;
		playerscript.targetdash = false;
		chancetodrop = Random.Range (1,5);
		if (chancetodrop == 1 && tag != "Dead")
		SpawnPowerup();
		gameObject.tag = "Dead";
		rigidbody.velocity = new Vector3 
				(Random.Range(-450,450), Random.Range(-100,340), 0);

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
		int choice = Random.Range(1,3);
		if (choice == 1)
			shootone();
		if (choice == 2)
			shootmult();
	}
	
	void SpawnPowerup () 
	{
		
		itemroll = Random.Range(1,11);
		

		if (itemroll < 3)
		{
			Instantiate(sushi, transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));
		}
		if (itemroll >= 7)
		{
			Instantiate(chiball, transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));
		}
		
		
		
	}
	
	public void teleport(Vector3 newloc)
	{	
		StartCoroutine ( Teleportanim (newloc) );	
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
	
	IEnumerator delayoff()
	{
		yield return new WaitForSeconds(0.28f);
		renderer.enabled = false;
	}
	
	
	public void CancelDBZ (Vector3 tt)
	{
		dbzmode = false;
		dbzmodetimer = 0f;
		
	}
	
	void shaking()
	{
//		float fefe = Mathf.PingPong(Time.time, 10);
//		punksprite.offset = new Vector2 (fefe, 0);
		
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
	
	void teleevade()
	{
		audio.PlayOneShot(tele);
		if (!gotrandomforloc)
			getrandomforloc();
		teleport(telelocations[randomforloc]);
		StartCoroutine ( defensiveshoot() );
		moveawaytimer = 0f;
		gotrandomforrun = false;
		gotrandomforloc = false;
	}
	
	void shootone()
	{
		ninanim.Play("ninthrow");
		Instantiate(shuriken, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
	}
	
	public void TakeDamage(float amount)
	{
		health -= amount;	
		
	}
	
	void shootmult()
	{
		ninanim.Play("ninthrow");
		if (transform.position.x > 0)
		{
			GameObject botleft = (GameObject)Instantiate(shugeneric, transform.position, transform.rotation);
			botleft.rigidbody.velocity = new Vector3 (-400, -400, 0);
		
			GameObject leftleft = (GameObject)Instantiate(shugeneric, transform.position, transform.rotation);
			leftleft.rigidbody.velocity = new Vector3 (-400, 0, 0);
			
			GameObject topleft = (GameObject)Instantiate(shugeneric, transform.position, transform.rotation);
			topleft.rigidbody.velocity = new Vector3 (-400, 400, 0); 
			
		}
		if (transform.position.x <= 0)
		{
			GameObject topright = (GameObject)Instantiate(shugeneric, transform.position, transform.rotation);
			topright.rigidbody.velocity = new Vector3 (200, 200, 0);
		
			GameObject rightright = (GameObject)Instantiate(shugeneric, transform.position, transform.rotation);
			rightright.rigidbody.velocity = new Vector3 (400, 0, 0);
		
			GameObject rightbot = (GameObject)Instantiate(shugeneric, transform.position, transform.rotation);
			rightbot.rigidbody.velocity = new Vector3 (400, -400, 0);
				
		}
	}
	
	IEnumerator defensiveshoot()
	{
		yield return new WaitForSeconds(0.5f);
		
		if (transform.position.y > 0)
			defdown();
		if (transform.position.y <= 0)
			defup();
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
		
	}
	
}
