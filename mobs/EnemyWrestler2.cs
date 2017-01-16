using UnityEngine;
using System.Collections;

public class EnemyWrestler2 : MonoBehaviour {
	
	public float health = 12, maxhealth = 12;
	public bool tutorial = false;
	public bool facingright = true;
	public bool firsttime = true;
	public bool secondtime = false;
	private bool doonce = false;
	
	public bool slowed = false, gotdmgbybomb = false, gotdmgbydkick = false, falling = false, dbzmode = false, gravitypulled = false, 
	otg = false, balloon = false, stunned = false, grabbed = false, gotdmgbyforce = false, gotdmgbytackle = false;
	private float slowtimer = 0, dmgtypereset = 0, otgtimer = 0, balloontimer = 0;
	public float dbzmodetimer = 0;
	private bool spawnonce = false;
	
	public bool bombed = false;
	public bool poisoned = false;
	private int poisonticks = 0;
	private float bombtimer = 0, poisontimer = 0, wallbouncetimer = 0;
	public bool wallbounce = false;
	
	pausemenu pausescript;
	public exSprite mywrestler;
	public exSpriteAnimation wrestleranim;
	
	public GameObject Karateman;
	public Player playerscript;
	
	public Karateoboxnew obox;
	
	public GameObject spawner, debris;
	
	private float velocityX, velocityY;
	public float gravity = -23;
	
	public AudioClip gsmashsound;
	
	
	//items
	private int itemroll;
	public GameObject sushi, money, chiball, life, redscroll, explosion;


	void Start () {
		
		SetCurrent();
		Karateman = GameObject.FindGameObjectWithTag("Player");
		playerscript = (Player)Karateman.GetComponent("Player");
		pausescript = (pausemenu)Camera.main.GetComponent("pausemenu");
		
		if (GameObject.FindGameObjectWithTag("Tutorial") != null)
			tutorial = true;
	
	}
	
	void FixedUpdate ()
	{
		//movement
		if (!pausescript.playerpause)
		{
			if (!dbzmode)
			{
				if (!slowed && !dbzmode && !falling & !otg && !balloon && !stunned)
				gameObject.rigidbody.velocity = new Vector3(velocityX, velocityY, 0);
				
				if (slowed && !dbzmode && !falling & !otg && !balloon && !stunned)
				gameObject.rigidbody.velocity = new Vector3(velocityX / 2, velocityY / 2, 0);
				
			}
		}
		
	}
	
	void Update () {
		
		if (!pausescript.playerpause)
		{
			//variables
			float currentRotate = 720;
			float rotationspeed = currentRotate * Time.deltaTime;
			
			if (gameObject.tag == "Dead")
			{
				transform.Rotate(new Vector3(0, 0,-1) * rotationspeed);
			}
			
			//status effects
			if (slowed)
			{
				slowtimer += Time.deltaTime;
				mywrestler.color = Color.yellow;
				
				if (slowtimer > 10)
				{
					slowed = false;
					slowtimer = 0;
					mywrestler.color = Color.white;
				}
				
			}
			
			//DBZ mode
				if (dbzmode)
				{
					dbzmodetimer += Time.deltaTime;
					Vector3 tempstore = rigidbody.velocity;
					wrestleranim.PlayDefault();
	//				shaking();
	//				canhityou = true;
					transform.rotation = Quaternion.Euler(Vector3.zero);
					if (!gravitypulled)
					rigidbody.velocity = Vector3.zero;
					gravity = -0.34f;	
					
					if (gravitypulled && dbzmodetimer - Time.deltaTime > 2f)
					{
						CancelDBZ(tempstore);
					}
				
					if (stunned && dbzmodetimer - Time.deltaTime > 3f)
					{
						CancelDBZ(tempstore);
					}
					
					if (!stunned && !gravitypulled && dbzmodetimer - Time.deltaTime > 1f)
					{
						CancelDBZ(tempstore);
					}
				}
			
			if (!dbzmode)
				wrestleranim.Stop();
			
			if (bombed)
			{
				bombtimer += Time.deltaTime;
				mywrestler.color = Color.red;
				
				if (bombtimer >= 6)
				{
					bombed = false;
					bombtimer = 0;
					mywrestler.color = Color.white;
				}
				
			}
			
			if (poisoned)
			{
				poisontimer += Time.deltaTime;
				mywrestler.color = Color.green;
				
				if (poisontimer >= 1)
				{
					poisonticks++;	
	//				punkanim.Play("g2stomach");
					health--;
					poisontimer = 0;
				}
				
				if (poisonticks >= 5)
				{
					poisoned = false;
					poisontimer = 0;
					poisonticks = 0;
					mywrestler.color = Color.white;
				}
				
			}
			
			//gsmash & bounce
			if (transform.position.y <= -325 && falling && tag == "Hardcore2")
			{
				falling = false;
				otg = true;
				audio.PlayOneShot(gsmashsound);
				Instantiate(debris, transform.position, transform.rotation);
				gameObject.rigidbody.velocity = new Vector3(0, 800, 0);
				TakeDamage(playerscript.attackdmg);
			}
			
			//regular axekick effect: OTG
			if (gameObject.tag == "Hardcore2" && otg)
			{
				otgtimer += Time.deltaTime;
	//			punkanim.Play("Stomachache");
				if (otgtimer - Time.deltaTime >= 1.5f)
				{
					otg = false;
					otgtimer = 0;
					if (transform.position.x > 0 && facingright)
					{
						velocityX *= -1;
						mywrestler.HFlip();
						facingright = false;
					}
					if (transform.position.x < 0 && !facingright)
					{
						velocityX *= -1;
						mywrestler.HFlip();
						facingright = true;
					}
						
				}
			}
			
			//junkled disable
			if (tag == "Hardcore2" && balloon)
			{
				balloontimer += Time.deltaTime;
	//			punkanim.Play("Stomacheache");
				transform.Rotate(new Vector3(0,0,-1) * 12);
				if (balloontimer - Time.deltaTime > 3f)
				{
					balloon = false;
					transform.rotation = new Quaternion(0,0,0,0);
					balloontimer = 0f;
				}
			}
			
			if (grabbed)
			{
				transform.position = Karateman.transform.position;	
				transform.rotation = Quaternion.Euler(new Vector3(0,0, 90));
				if (transform.position.y <= -300)
				{
					grabbed = false;
					transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
					health -= maxhealth/2;
				}
			}
			
			if (health <= 0)
				FlyAway();
			
			if (gravitypulled)
				mywrestler.color = Color.black;
			else mywrestler.color = Color.white;
			
			//restrictions & trash removal
			if (transform.position.y > 325)
				rigidbody.velocity += new Vector3 (0, -28, 0);
			
			if (transform.position.x < -900  || transform.position.x > 900 || transform.position.y < -600)
			{
				Destroy(this.gameObject);
			}
			
			
			//screen bounce
			if (firsttime || secondtime)
			{
				if (transform.position.x <= -645)
					{
						transform.position = new Vector3(-644, transform.position.y, transform.position.z);
						velocityX = 340;
						mywrestler.HFlip();
						facingright = true;
						firsttime = false;
					}
				if (transform.position.x >= 633)
					{
						transform.position = new Vector3(632, transform.position.y, transform.position.z);
						velocityX = -340;
						mywrestler.HFlip();
						facingright = false;
						firsttime = false;
					}
					
			}
			
			//fuck 3d
			if (transform.position.z != -100)
				transform.position = new Vector3(transform.position.x, transform.position.y, -100);
			
			
			//gravity, bounce when status
			if (transform.position.y > -7.0f && tag != "Dead")
					gameObject.rigidbody.velocity += new Vector3(0, gravity, 0);
			
			if (transform.position.y <= -325 && balloon)
				rigidbody.velocity = new Vector3(Random.Range (-200, 200), 800, 0);
				
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
				if (dmgtypereset > 0.5f)
				{
					gotdmgbyforce = false;	
					dmgtypereset = 0;
				}
				
			}
			
		}//pause
		
		if (pausescript.playerpause)
			rigidbody.velocity = Vector3.zero;
	
	}//update
	
	void SetCurrent () {
		
		//only for horizontal strict spawn (initial pos further)
		if (transform.position.x >= 800)
		//right side
		{
			velocityX = Random.Range (-340, -510);
			velocityY = 0;
			if (facingright)
				{
					mywrestler.HFlip();
					facingright = false;
				}
			return;
		}
		
		if (transform.position.x <= -800)
		//left side	
		{
			velocityX = Random.Range (340, 510);
			velocityY = 0;
			if (!facingright)
				{
					mywrestler.HFlip();
					facingright = true;
				}
			return;
		}
		

		//for vertical movement
		if (transform.position.y >= 425)
		{
			velocityX = 0;	
			velocityY = Random.Range (-340, -510);
			transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
			return;
		}
		
		
		//for diagonal/random movement (initial pos is closer)
		if (transform.position.x > 0 && transform.position.x < 800)
		//right side
		{
		velocityX = Random.Range (-340, -510);
		velocityY = Random.Range (-100, 30);
		if (facingright)
				{
					mywrestler.HFlip();
					facingright = false;
				}
		}
		
		if (transform.position.x < 0 && transform.position.x > -800)
		//left side	
		{
		velocityX = Random.Range (340, 510);
		velocityY = Random.Range (-100, 30);
		if (!facingright)
				{
					mywrestler.HFlip();
					facingright = true;
				}
		}
		
		
		
	}
	
	public void TakeDamage(float amount)
	{
		health -= amount;
		
	}
	
	public void FlyAway () 
	{
		if (!tutorial && !doonce  && Application.loadedLevelName != "devmode")
		{
			playerscript.wrestlerdefeated++;
			playerscript.XPgained += 40;
			doonce = true;
		}
		if (bombed && tag != "Dead")
			Instantiate (explosion, transform.position, transform.rotation);
		gameObject.tag = "Dead";
		if (!spawnonce)
		SpawnPowerup();
		gravity = 0;
		gameObject.rigidbody.velocity += new Vector3 
				(Random.Range(-850,850), 510, 0);
		
		
	}
	
	void SpawnPowerup () 
	{
		
		itemroll = Random.Range(1,11);
		int scrollroll = Random.Range(1,4);
		
		if (itemroll < 5)
		{
			Instantiate(sushi, transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));
		}
		if (itemroll >= 5)
		{
			Instantiate(money, transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));
			Instantiate(money, transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));
			Instantiate(money, transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));
			Instantiate(money, transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));
			if (scrollroll == 2)
				Instantiate(redscroll, transform.position, Quaternion.identity);
		}
			Instantiate(chiball, transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));

		
		spawnonce = true;
		
	}
	
	public void shaking()
	{
		StartCoroutine ( shakee () );
	}
	
	IEnumerator shakee()
	{
		Vector3 pospos = mywrestler.offset;
		
		mywrestler.offset = new Vector3(pospos.x + 3, pospos.y, pospos.z);
		
		yield return new WaitForSeconds(0.05f);
		
		mywrestler.offset = new Vector3(pospos.x - 3, pospos.y, pospos.z);
		
		yield return new WaitForSeconds(0.05f);
		
		mywrestler.offset = new Vector3(pospos.x + 3, pospos.y, pospos.z);
		
		yield return new WaitForSeconds(0.05f);
		
		mywrestler.offset = new Vector3(pospos.x - 3, pospos.y, pospos.z);
		
		yield return new WaitForSeconds(0.05f);
		
		mywrestler.offset = new Vector3(pospos.x, pospos.y, pospos.z);
		
		
	}
	
	public void CancelDBZ (Vector3 tt)
	{
		dbzmode = false;
		gravitypulled = false;
		dbzmodetimer = 0f;
		gravity = -23;
		rigidbody.velocity = tt;
		
	}
	
}