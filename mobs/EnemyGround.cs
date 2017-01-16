using UnityEngine;
using System.Collections;

public class EnemyGround : MonoBehaviour {
	
//	private int downcount = 0;
	private float age;
	public float ttg = 20;
	private bool doonce = false, checkedagain = false;
	
	public int health = 4;
	
	
	public bool tutorial = false, turnon = false, spawned = false;
	
	private float minRotatespeed = 0, maxRotatespeed = 360;
	private float currentRotate;
	
	public GameObject spawner;
	public Player playerscript;
	
	private float velocityX, velocityY;
	public float gravity = -22, jumpbounce;
	
	public AudioClip fuck;
	
	public Karateoboxnew obox;
	
	public GameObject sushi, chiball, debris, debris2, debris3, debris4, debris5, debris6, explo;
	
	public GameObject[] debrisbox;
	
	private int itemroll;
	pausemenu pausescript;


	void Start () {
		
		SetCurrent();
		collider.enabled = false;
		StartCoroutine (turniton () );
		playerscript = (Player)GameObject.FindGameObjectWithTag("Player").GetComponent("Player");
		pausescript = (pausemenu)Camera.main.GetComponent("pausemenu");
		
		if (GameObject.FindGameObjectWithTag("Tutorial") != null)
			tutorial = true;
	
		debrisbox = new GameObject[] {debris, debris2, debris3, debris4, debris5, debris6};
	}
	
	void FixedUpdate ()
	{		
		
		//falling down
		if (transform.position.y > -300 && !pausescript.playerpause)
		gameObject.rigidbody.velocity = new Vector3(velocityX, velocityY, 0);
		
	}
	
	void Update () {
		
		
		if (!pausescript.playerpause)
		{
			if (!checkedagain)
			{
				if (GameObject.FindGameObjectWithTag("Tutorial") == null)
				tutorial = false;	
				
			}
			
			//variables
			age += Time.deltaTime;
			float rotationspeed = currentRotate * Time.deltaTime;
			
			//spinning
			transform.Rotate(new Vector3(0,0,1) * rotationspeed);
			
			//restrictions & trash removal
			if (GameObject.FindGameObjectWithTag("Enemy") == null && GameObject.FindGameObjectWithTag("Enemy2") == null
				&& GameObject.FindGameObjectWithTag("Enemy3") == null)
			{
				transform.Translate(new Vector3(4,0,0) * Time.deltaTime);
				if (transform.position.x < -600)
				Destroy(this.gameObject);
			}
			
			if (age > ttg)
			{
				Instantiate(explo, transform.position, Quaternion.identity);
				Die ();
			}
			
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
				
			if (transform.position.y > -300)
					gameObject.rigidbody.velocity += new Vector3(0, gravity, 0);
			
			if (transform.position.y <= -300)
			{
					transform.rotation = Quaternion.Euler(Vector3.zero);
					if (transform.position.x > 0)
					gameObject.rigidbody.velocity = new Vector3(Random.Range (-50,50), 0, 0);
					if (transform.position.x < 0)
					gameObject.rigidbody.velocity = new Vector3(Random.Range (-50,60), 0, 0);
			}
			
			if (turnon)
				collider.enabled = true;
			
			if (health <= 0)
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
		
		//random spawn (initial pos is closer)
		if (transform.position.x > 0 && transform.position.x < 800)
		//right side
		{
			velocityX = Random.Range (-204, -680);
			velocityY = Random.Range (-340, -34);
		}
		
		if (transform.position.x < 0 && transform.position.x > -800 )
		//left side	
		{
			velocityX = Random.Range (204, 680);
			velocityY = Random.Range (-340, -34);
		}
		
		//spawning from another shit
		if (transform.position.y < 0)
		{
			currentRotate = -900;
			velocityX = Random.Range(-200, 200);
			velocityY = Random.Range(20, 40);
			StartCoroutine (stopgoingup () );
		}
		
	}
	
	public void Die ()
	{
		if (!doonce)
		{
			if (age < 15)
			{
				itemroll = Random.Range(1,6);
				if (itemroll == 1)
				Instantiate(chiball, transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));
				if (itemroll == 2 || itemroll == 3)
				Instantiate(sushi, transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));	
			}
			
			int debrisroll = Random.Range(1, 4);
			if (debrisroll == 1)
			{
				Instantiate(debrisbox[Random.Range(0,6)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
				Instantiate(debrisbox[Random.Range(0,6)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
				Instantiate(debrisbox[Random.Range(0,6)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
				Instantiate(debrisbox[Random.Range(0,6)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
			}
			if (debrisroll == 2)
			{
				Instantiate(debrisbox[Random.Range(0,6)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
				Instantiate(debrisbox[Random.Range(0,6)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
				Instantiate(debrisbox[Random.Range(0,6)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
				Instantiate(debrisbox[Random.Range(0,6)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
				Instantiate(debrisbox[Random.Range(0,6)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
			}
			if (debrisroll == 3)
			{
				Instantiate(debrisbox[Random.Range(0,6)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
				Instantiate(debrisbox[Random.Range(0,6)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
				Instantiate(debrisbox[Random.Range(0,6)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
				Instantiate(debrisbox[Random.Range(0,6)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
				Instantiate(debrisbox[Random.Range(0,6)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
				Instantiate(debrisbox[Random.Range(0,6)], new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
			}
			
			if (!tutorial && health <= 0 && Application.loadedLevelName != "devmode")
			{
				playerscript.grounddefeated++;
				playerscript.XPgained += 10;	
			}
			
			doonce = true;
			Destroy(this.gameObject);
		
		}
	}
	
	IEnumerator turniton()
	{
		yield return new WaitForSeconds(2);
		
		turnon = true;
		
	}
	
	IEnumerator stopgoingup()
	{
		yield return new WaitForSeconds(0.5f);
		
		velocityY = -90;
		
	}
	
}