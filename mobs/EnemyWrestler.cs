using UnityEngine;
using System.Collections;

public class EnemyWrestler : MonoBehaviour {
	
	private float age;
	
	public float health = 3, maxhealth = 3;
	public bool tutorial = false;
	public bool facingright = true;
	public bool dbzmode = false, stunned = false, balloon = false, gravitypulled = false, gotdmgbydkick = false, falling = false, otg = false
		, grabbed = false, gotdmgbyforce = false, gotdmgbytackle = false, gotdmgbybomb = false;
	private float dmgtypereset = 0, otgtimer = 0, balloontimer = 0;
	public float dbzmodetimer = 0;
	public exSprite mywrestler;
	public exSpriteAnimation wrestleranim;
	
	pausemenu pausescript;
	
	public bool bombed = false;
	public bool poisoned = false;
	private int poisonticks = 0;
	private float bombtimer = 0, poisontimer = 0, wallbouncetimer = 0;
	public bool wallbounce = false;
	
	public int comboedcount = 0;
	public float comboedreset = 0;
	private int highestcombo = 0;
	
	public bool doonce = false;
	
	public GameObject Karateman;
	public Player playerscript;
	
	public Karateoboxnew obox;
	
	public GameObject spawner;
	private bool spawnonce = false;
	
	private float velocityX, velocityY;
	public float gravity = -23;
	
	public AudioClip gsmashsound;
	public GameObject debris;
	
	
	//items
	private int itemroll;
	public GameObject sushi, money, chiball, life, redscroll, explosion;


	void Start () {
		
		SetCurrent();
		Karateman = GameObject.FindGameObjectWithTag("Player");
		pausescript = (pausemenu)Camera.main.GetComponent("pausemenu");
		playerscript = (Player)Karateman.GetComponent("Player");
		
		if (GameObject.FindGameObjectWithTag("Tutorial") != null)
			tutorial = true;
	
	}
	
	void FixedUpdate ()
	{
		if (!pausescript.playerpause)
		{
			//movement
			if (!dbzmode && !falling & !otg && !balloon && !stunned && !gotdmgbyforce)
			gameObject.rigidbody.velocity = new Vector3(velocityX, velocityY, 0);
		}
		
	}
	
	void Update () {
		
		if (!pausescript.playerpause)
		{
		
			//variables
			age += Time.deltaTime;
			float currentRotate = 720;
			float rotationspeed = currentRotate * Time.deltaTime;
			
			if (gameObject.tag == "Dead")
			{
				transform.Rotate(new Vector3(0, 0,-1) * rotationspeed);
			}
			
			
			//DBZ mode
				if (dbzmode)
				{
					dbzmodetimer += Time.deltaTime;
					Vector3 tempstore = rigidbody.velocity;
					wrestleranim.Play("w1dbz");
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
			if (transform.position.y <= -325 && falling && tag == "Hardcore")
			{
				falling = false;
				otg = true;
				audio.PlayOneShot(gsmashsound);
				Instantiate(debris, transform.position, transform.rotation);
				gameObject.rigidbody.velocity = new Vector3(0, 800, 0);
				TakeDamage(playerscript.attackdmg);
			}
			
			//regular axekick effect: OTG
			if (gameObject.tag == "Hardcore" && otg)
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
			if (tag == "Hardcore" && balloon)
			{
				balloontimer += Time.deltaTime;
	//			punkanim.Play("Stomacheache");
				transform.Rotate(new Vector3(0,0,-1) * 12);
				if (balloontimer - Time.deltaTime > 5f)
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
					FlyAway();
				}
			}
			
			if (gravitypulled)
				mywrestler.color = Color.black;
			else mywrestler.color = Color.white;
			
			if (health <= 0)
				FlyAway();
			
			//restrictions & trash removal
			if (transform.position.y > 325)
				rigidbody.velocity += new Vector3 (0, -28, 0);
			
			if (transform.position.x < -900  || transform.position.x > 900 || transform.position.y < -600)
			{
				Destroy(this.gameObject);
			}
			
			//fuck 3d
			if (transform.position.z != -100)
				transform.position = new Vector3(transform.position.x, transform.position.y, -100);
			
			//gravity, bounce when status
			if (transform.position.y > -7.0f)
					gameObject.rigidbody.velocity += new Vector3(0, gravity, 0);
			
			if (transform.position.y <= -325 && balloon)
				rigidbody.velocity = new Vector3(Random.Range (-200, 200), 800, 0);
			
			
			//dmg reserts
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
				if (dmgtypereset > 0.7f)
				{
					gotdmgbyforce = false;	
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
		
		}
		
		if (pausescript.playerpause)
			rigidbody.velocity = Vector3.zero;
		
	}//end | update
	
//	void OnTriggerEnter(Collider otherObject)
//	{
//		if (otherObject.tag == "Beam")
//		{
//			FlyAway();
//		}
//		
//		
//	}
	
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
			playerscript.XPgained += 20;
			doonce = true;
		}
		if (bombed && tag != "Dead")
			Instantiate (explosion, transform.position, transform.rotation);
		gameObject.tag = "Dead";
		dbzmode = false;
		otg = false;
		stunned = false;
		if (!spawnonce)
		SpawnPowerup();
		gravity = 0;
		gameObject.rigidbody.velocity += new Vector3 
				(Random.Range(-850,850), 510, 0);
		
		
	}
	
	public void Bleedcash()
	{
		if (comboedcount == 3)
			Instantiate(money, transform.position, Quaternion.Euler(Vector3.zero));
		if (comboedcount == 4)
			Instantiate(money, transform.position, Quaternion.Euler(Vector3.zero));
		if (comboedcount == 5)
		{
			Instantiate(money, transform.position, Quaternion.Euler(Vector3.zero));
			Instantiate(money, transform.position, Quaternion.Euler(Vector3.zero));
			Instantiate(money, transform.position, Quaternion.Euler(Vector3.zero));
		}
		
		
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
	
	void SpawnPowerup () 
	{
		
		itemroll = Random.Range(1,11);
		int scrollroll = Random.Range(1,5);
		
		if (itemroll < 5)
		{
			Instantiate(sushi, transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));
		}
		if (itemroll >= 5)
		{
			if (highestcombo <= 1)	
			Instantiate(money, transform.position, transform.rotation);
			if (highestcombo == 2)
			{
				Instantiate(money, transform.position, transform.rotation);
				Instantiate(money, transform.position, transform.rotation);
			}
			if (highestcombo == 3)
			{
				Instantiate(money, transform.position, transform.rotation);
				Instantiate(money, transform.position, transform.rotation);
				Instantiate(money, transform.position, transform.rotation);
			}
			if (highestcombo > 3)
			{
				Instantiate(money, transform.position, transform.rotation);
				Instantiate(money, transform.position, transform.rotation);
				Instantiate(money, transform.position, transform.rotation);
				Instantiate(money, transform.position, transform.rotation);
				Instantiate(money, transform.position, transform.rotation);
			}
			if (scrollroll == 2)
				Instantiate(redscroll, transform.position, transform.rotation);
		}
			Instantiate(chiball, transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));

		
		spawnonce = true;
		
	}
	
	public void CancelDBZ (Vector3 tt)
	{
		mywrestler.spanim.Stop();
		dbzmode = false;
		gravitypulled = false;
		dbzmodetimer = 0f;
		rigidbody.velocity = tt;
		
	}
	
//	void OnGUI () 
//	{
//		
//		GUI.color = Color.black;
//		if (tag != "Dead" && mywrestler.spanim.GetCurrentAnimation() != null)
//		GUI.Label(new Rect(0, 160, 200, 100), "animu == " + mywrestler.spanim.GetCurrentAnimation().name.ToString());
//		GUI.Label(new Rect(0, 180, 200, 100), "dbz == " + dbzmode.ToString());
//
//		
//	}
}