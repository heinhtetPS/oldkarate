using UnityEngine;
using System.Collections;

public class EnemyGround3 : MonoBehaviour {
	
//	private int downcount = 0;
	private float age;
	private bool doonce;
	
	public bool tutorial = false, turnon = false;
	
	private float minRotatespeed = 0, maxRotatespeed = 360;
	private float currentRotate;
	
	public int health = 12;
	
	public GameObject spawner;
	public Player playerscript;
	
	private float velocityX, velocityY;
	public float gravity = 22, jumpbounce;
	
	public AudioClip fuck;
	public Animation shitsanimation;
	public exSpriteAnimation shitsanime;
	
	private float rushCD = 0f, rushingtimer = 0, expandCD = 0;
	private int randomforrush;
	private bool gotrandomforrush = false, rushing = false, expanding = false;
	
	private int randomforexpand;
	private bool gotrandomforexpand = false;
	
	public GameObject Karateman;

	
	public GameObject sushi, chiball, debris, debris2, debris3, debris4, debris5, debris6, shits1;
	
	public GameObject[] debrisbox;
	
	private int itemroll;
	pausemenu pausescript;


	void Start () {
		
		SetCurrent();
		collider.enabled = false;
		
		playerscript = (Player)GameObject.FindGameObjectWithTag("Player").GetComponent("Player");
		pausescript = (pausemenu)Camera.main.GetComponent("pausemenu");
		
		if (GameObject.FindGameObjectWithTag("Tutorial") != null)
			tutorial = true;
		
		Karateman = GameObject.FindGameObjectWithTag("Player");
		
		debrisbox = new GameObject[] {debris, debris2, debris3, debris4, debris5, debris6, shits1};
	
	}
	
	void FixedUpdate ()
	{	
		if (!pausescript.playerpause)
		{
			//falling down
			if (transform.position.y > -300)
			gameObject.rigidbody.velocity = new Vector3(velocityX, velocityY, 0);
			
			
			//semi-intelligent chase
			if (rushing && transform.position.y <= -300 && tag == "Ground2")
			{
				rigidbody.velocity = new Vector3(Getdiff().x * 0.5f, 0, 0);
			}
			if (rushing && transform.position.y <= -300 && tag == "Ground3")
			{
				rigidbody.velocity = new Vector3(Getdiff().x, 0, 0);
			}
		}
		
	}
	
	void Update () {
		
		if (!pausescript.playerpause)
		{
			//variables
			age += Time.deltaTime;
			float rotationspeed = currentRotate * Time.deltaTime;
			
			//spinning
			if (transform.position.x > -300)
			transform.Rotate(new Vector3(0,0,1) * rotationspeed);
			
			//restrictions & trash removal
	//		if (age >= 40)
	//		{
	//			transform.Translate(new Vector3(4,0,0) * Time.deltaTime);
	//			if (transform.position.x < -20)
	//			Destroy(this.gameObject);
	//		}
			
	//		if (age < 40)
	//		{
	//			if (transform.position.x <= -13f)
	//				transform.position = new Vector3(15.4f, transform.position.y, transform.position.z);
	//			else if (transform.position.x >= 15.5f)
	//				transform.position = new Vector3(-12.9f, transform.position.y, transform.position.z);
	//		}
			
			//destroy 
			if (transform.position.x > 850)
					Destroy(this.gameObject);
			if (transform.position.x < -850)
					Destroy(this.gameObject);
			
			//fuck 3d
			if (transform.position.z != -100)
				transform.position = new Vector3(transform.position.x, transform.position.y, -100);
			
			
			//screen wrap
			if (transform.position.x <= -680)
				transform.position = new Vector3(669, transform.position.y, transform.position.z);
			else if (transform.position.x >= 670)
				transform.position = new Vector3(-679, transform.position.y, transform.position.z);
			
			//gravity, no bounce	
			if (transform.position.y <= -300)
					transform.position = new Vector3 (transform.position.x, -300, -100);
			
			
			if (transform.position.y <= -300 && !rushing)
			{
					transform.rotation = Quaternion.Euler(Vector3.zero);
					if (transform.position.x > 0)
					gameObject.rigidbody.velocity = new Vector3(Random.Range (-50,50), 0, 0);
					if (transform.position.x < 0)
					gameObject.rigidbody.velocity = new Vector3(Random.Range (-50,60), 0, 0);
					collider.enabled = true;
					rushCD += Time.deltaTime;
			}
			
			//semi intelligent following
			if (!gotrandomforrush)
				Getrushrandom();
			
			if (transform.position.y <= -300 && rushCD - Time.deltaTime >= randomforrush)
			{
				rushing = true;
				rushCD = 0f;
			}
			
			if (rushing)
			{
				transform.rotation = Quaternion.Euler(Vector3.zero);
				rushingtimer += Time.deltaTime;	
				if (rushingtimer >= 2)
				{
					rushing = false;	
					shitsanime.Stop();
					rushingtimer = 0;
					gotrandomforrush = false;
				}
			}
			if (rushing && transform.position.y < -320)
			{
				playanimation("Shitsmoving");
			}
	//		if (rigidbody.velocity.x <= 100 && transform.position.y < -300)
	//			shitsanime.Play("Shitsdefault");
			
			
			
			//expand and contract
			if (!gotrandomforexpand)
				Getexpandrandom();
			if (!expanding)
			{
				expandCD += Time.deltaTime;
				if (expandCD >= randomforexpand)
				{
					StartCoroutine( ExpandContract () );
					expandCD = 0;
				}
			}
			
			
			
			if (turnon)
				collider.enabled = true;
			
			if (tag == "Ground2" && health <= 4)
				Die();
			
			if (tag == "Ground3" && health <= 0)
				Die();
			
		}
		
		if (pausescript.playerpause)
			rigidbody.velocity = Vector3.zero;
	}
	
	void SetCurrent () {
		
		//variables
		currentRotate = Random.Range(minRotatespeed, maxRotatespeed);
		
		
		//only for specific spawn (initial pos strict)
		if (transform.position.x >= 800)
		//right side
		{
			velocityX = -270;
			velocityY = -680;
			if (transform.position.y > 100)
			{
				velocityX = -540;
				velocityY = -970;
				if (transform.position.y > 200)
				{
					velocityX = -810;
					velocityY = -1280;
					if (transform.position.y > 300)
					{
						velocityX = -1080;
						velocityY = -1490;
						if (transform.position.y > 400)
						{
							velocityX = -1350;
							velocityY = -1600;
						}
					}	
				}
			}
			return;
		}
		
		if (transform.position.x <= -800)
		//left side
		{
			velocityX = 270;
			velocityY = -680;
			if (transform.position.y > 100)
			{
				velocityX = 540;
				velocityY = -970;
				if (transform.position.y > 200)
				{
					velocityX = 810;
					velocityY = -1280;
					if (transform.position.y > 300)
					{
						velocityX = 1080;
						velocityY = -1490;
						if (transform.position.y > 400)
						{
							velocityX = 1350;
							velocityY = -1600;
						}
					}	
				}
			}
			return;
		}
		
		//random spawn (initial pos is closer
		if (transform.position.x > 0 && transform.position.x < 800)
		//right side
		{
			turnon = true;
			velocityX = Random.Range (-204, -680);
			velocityY = Random.Range (-340, -34);
		}
		
		if (transform.position.x < 0 && transform.position.x > -800 )
		//left side	
		{
			turnon = true;
			velocityX = Random.Range (204, 680);
			velocityY = Random.Range (-340, -34);
		}
		

		
	}
	
	public void Die ()
	{
		if (!doonce)
		{
			itemroll = Random.Range(1,6);
			if (itemroll == 1)
			Instantiate(chiball, transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));
			if (itemroll == 2 || itemroll == 3)
			Instantiate(sushi, transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));	
			
			int debrisroll = Random.Range(1, 4);
			if (debrisroll == 1)
			{
				Instantiate(debrisbox[Random.Range(0,7)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
				Instantiate(debrisbox[Random.Range(0,7)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
				Instantiate(debrisbox[Random.Range(0,7)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
				Instantiate(debrisbox[Random.Range(0,7)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
			}
			if (debrisroll == 2)
			{
				Instantiate(debrisbox[Random.Range(0,7)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
				Instantiate(debrisbox[Random.Range(0,7)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
				Instantiate(debrisbox[Random.Range(0,7)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
				Instantiate(debrisbox[Random.Range(0,7)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
				if (tag == "Ground3")
				Instantiate(shits1, new Vector3(transform.position.x, transform.position.y + 50, -100), transform.rotation);
			}
			if (debrisroll == 3)
			{
				Instantiate(debrisbox[Random.Range(0,7)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
				Instantiate(debrisbox[Random.Range(0,7)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
				Instantiate(debrisbox[Random.Range(0,7)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
				Instantiate(debrisbox[Random.Range(0,7)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
				if (tag == "Ground3")
				{
					Instantiate(shits1, new Vector3(transform.position.x + 100, transform.position.y + 50, -100), transform.rotation);
					Instantiate(shits1, new Vector3(transform.position.x - 100, transform.position.y + 50, -100), transform.rotation);
				}
				Instantiate(debrisbox[Random.Range(0,7)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
			}
			
			if (!tutorial && health <= 0  && Application.loadedLevelName != "devmode")
			{
				playerscript.grounddefeated++;
				if (tag == "Ground2")
				playerscript.XPgained += 20;
				if (tag == "Ground3")
				playerscript.XPgained += 30;
			}
			
			doonce = true;
			Destroy(this.gameObject);
			
		}
	}
	
	public Vector3 Getdiff()
	{
		Vector3 diff = Karateman.transform.position - transform.position; 
		return diff;
	}
	
	void playanimation(string name)
	{
		if (!shitsanime.IsPlaying("Shitsmoving"))	
		shitsanime.Play(name);
	}
	
	void Getrushrandom()
	{
		randomforrush = Random.Range (2, 10);	
		gotrandomforrush = true;
	}
	
	void Getexpandrandom()
	{
		randomforexpand = Random.Range (5, 10);	
		gotrandomforexpand = true;
	}
	
	IEnumerator ExpandContract()
	{
		shitsanimation.Play("expand");
		expanding = true;
		
		yield return new WaitForSeconds(6.5f);
		
		shitsanimation.Play("contract");
		expanding = false;
		gotrandomforexpand = false;
	}
	
//	void OnGUI () 
//	{
//		GUI.color = Color.red;
//		GUI.Label(new Rect(0, 160, 200, 100), "random == " + randomforrush.ToString());
//		GUI.Label(new Rect(0, 180, 200, 100), "rushing:" + rushing.ToString());
//		if (shitsanime.GetCurrentAnimation() != null)
//		GUI.Label(new Rect(0, 200, 200, 100), "anime:" + shitsanime.GetCurrentAnimation().name.ToString());
//		GUI.Label(new Rect(0, 220, 200, 100), "gsmashtimer:" + gsmashtimer.ToString());
//		GUI.Label(new Rect(0, 240, 200, 100), "gsmashactive:" + gsmashactive.ToString());
//
//
//		
//	}	
	
}