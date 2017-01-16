using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {
	
	public float stamina, timetogo;
	
	GameObject Karateman;
	Karateoboxnew obox;
	private Player playerscript;
	
	//rotation stuff
	public bool wallrotate = false, stack = false;
	private float mouseX, mouseY, diffX, diffY, cameraDif, rotatetimer;
	private Vector3 mWorldPos, mainPos;
	
	public AudioClip smacksound;
	public GameObject smackanimu;
	
	bool initialdmg = false; 
	float initialdmgtimer = 0;
	
	bool launching = false;
	float launchtimer = 0;
	
	bool doonce = false;
	
	// Use this for initialization
	void Start () {
		
		Karateman = GameObject.FindGameObjectWithTag("Player");
		playerscript = (Player)Karateman.GetComponent("Player");
		obox = (Karateoboxnew)GameObject.FindGameObjectWithTag("Offense").GetComponent("Karateoboxnew");
		
		if (PlayerPrefs.GetString("WallT1") == "black")
			stamina = 20;
		
		if (PlayerPrefs.GetString("WallT2") == "white")
		{
			if (PlayerPrefs.GetString("WallT1") == "black")
			{
				StartCoroutine (delayedwallrotate () );	
				return;
			}
			wallrotate = true;
			
		}
		
		if (PlayerPrefs.GetString("WallT2") != "white")
		initialdmg = true;
	}
	
	// Update is called once per frame
	void Update () {
		
		//falling from sky
		
		
			if (PlayerPrefs.GetString("WallT2") != "white")
			{
				if (!stack)
				{
					//long spear
					if (PlayerPrefs.GetString("WallT1") == "black" && PlayerPrefs.GetString("WallT2") != "black")
					{
						if (transform.position.y >= -160)
							transform.Translate(new Vector3(0, -1220, 0) * Time.deltaTime, Space.World);
					}
					//long club
					if (PlayerPrefs.GetString("WallT1") == "black" && PlayerPrefs.GetString("WallT2") == "black")
					{
						if (transform.position.y >= -148)
							transform.Translate(new Vector3(0, -1220, 0) * Time.deltaTime, Space.World);
					}
					//regular club
					if (PlayerPrefs.GetString("WallT1") != "black" && PlayerPrefs.GetString("WallT2") == "black")
					{
						if (transform.position.y >= -148)
							transform.Translate(new Vector3(0, -1220, 0) * Time.deltaTime, Space.World);
					}
					//regular spear
					if (PlayerPrefs.GetString("WallT2") != "white" && PlayerPrefs.GetString("WallT1") != "black")
					{
						if (transform.position.y >= -200)
							transform.Translate(new Vector3(0, -1220, 0) * Time.deltaTime, Space.World);
					}
					
				}
				
			}
		
			
		
		if (transform.position.z != -100)
				transform.position = new Vector3(transform.position.x, transform.position.y, -100);
		
		if (wallrotate)
		{
			cameraDif = Camera.main.transform.position.y - transform.position.y;
			mouseX = Input.mousePosition.x;
			mouseY = Input.mousePosition.y;
			mWorldPos = Camera.main.ScreenToWorldPoint( new Vector3(mouseX, mouseY, cameraDif));
			
			diffX = mWorldPos.x - transform.position.x;
		    diffY = mWorldPos.y  - transform.position.y;
	
			float angle = Mathf.Atan2(diffY, diffX) * Mathf.Rad2Deg;
			
			transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
			
			if (Input.GetButtonDown("fight"))
				wallrotate = false;
		}
		
		if (launching)
		{
			transform.Translate(Vector3.down * 900 * Time.deltaTime, Space.Self);	
			initialdmg = true;
			
			if (transform.position.y <= -260 || transform.position.x <= -600 | transform.position.x >= 600)
				launching = false;
			
			if (transform.position.y >= 450)
				Destroy(this.gameObject);
			
		}
		
		timetogo += Time.deltaTime;
		initialdmgtimer += Time.deltaTime;
		if (PlayerPrefs.GetString("WallT2") == "white" && wallrotate == false && !doonce)
			launchtimer += Time.deltaTime;
		
		if (initialdmgtimer > 1)
		{
			initialdmg = false;	
			initialdmgtimer = 0;
		}
		
		if (launchtimer > 2)
		{
			launching = true;
			launchtimer  = 0;
			doonce = true;
		}
		
		if (timetogo > stamina)
			Destroy(this.gameObject);
		
		
	
	}//END update
	
	
	IEnumerator delayedwallrotate()
	{
		yield return new WaitForSeconds(0.4f);
		
		wallrotate = true;
		
	}
	
	public Vector3 Getdiff()
	{
		Vector3 diff = Karateman.transform.position - transform.position; 
		return diff;
	}
	
	void OnTriggerEnter(Collider otherObject)
	{
		//damage from t2black or summoning
		if (PlayerPrefs.GetString("WallT2") == "black" || initialdmg)
		{
			if (otherObject.tag == "Enemy")
			{
				Punk1 enemyscript = (Punk1)otherObject.gameObject.GetComponent("Punk1");
				Instantiate(smackanimu, otherObject.transform.position, Quaternion.identity);
				audio.PlayOneShot(smacksound);
				if (initialdmg)
					enemyscript.Takedamage(4);
				else
				enemyscript.Takedamage(1);
				
				if (enemyscript.tackled)
				{
					otherObject.rigidbody.velocity = new Vector3(Getdiff().x, 600, 0);
					enemyscript.Bleedcash();
					obox.combocount();
				}
			}
			
			if (otherObject.tag == "Enemythrower")
			{
				Punkthrower enemyscript = (Punkthrower)otherObject.gameObject.GetComponent("Punkthrower");
				Instantiate(smackanimu, otherObject.transform.position, Quaternion.identity);
				audio.PlayOneShot(smacksound);
				if (initialdmg)
					enemyscript.Takedamage(4);
				else
				enemyscript.Takedamage(1);
				
				if (enemyscript.tackled)
				{
					otherObject.rigidbody.velocity = new Vector3(Getdiff().x, 600, 0);
					enemyscript.Bleedcash();
					obox.combocount();
				}
			}
			
			if (otherObject.tag == "Enemy2")
			{
				Punk2 enemyscript = (Punk2)otherObject.gameObject.GetComponent("Punk2");
				Instantiate(smackanimu, otherObject.transform.position, Quaternion.identity);
				audio.PlayOneShot(smacksound);
				if (initialdmg)
					enemyscript.Takedamage(4);
				else
				enemyscript.Takedamage(1);
				
				if (enemyscript.tackled)
				{
					otherObject.rigidbody.velocity = new Vector3(Getdiff().x, 600, 0);
					enemyscript.Bleedcash();
					obox.combocount();
				}
			}
			
			if (otherObject.tag == "Enemy3")
			{
				Punk3 enemyscript = (Punk3)otherObject.gameObject.GetComponent("Punk3");
				Instantiate(smackanimu, otherObject.transform.position, Quaternion.identity);
				audio.PlayOneShot(smacksound);
				if (initialdmg)
					enemyscript.Takedamage(4);
				else
				enemyscript.Takedamage(1);
				
				if (enemyscript.tackled)
				{
					otherObject.rigidbody.velocity = new Vector3(Getdiff().x, 600, 0);
					enemyscript.Bleedcash();
					obox.combocount();
				}
			}
			
			if (otherObject.tag == "Hardcore")
			{
				EnemyWrestler enemyscript = (EnemyWrestler)otherObject.gameObject.GetComponent("EnemyWrestler");
				Instantiate(smackanimu, otherObject.transform.position, Quaternion.identity);
				audio.PlayOneShot(smacksound);
				if (initialdmg)
					enemyscript.TakeDamage(4);
				else
				enemyscript.TakeDamage(1);	
			}
			
			if (otherObject.tag == "Hardcore2")
			{
				EnemyWrestler2 enemyscript = (EnemyWrestler2)otherObject.gameObject.GetComponent("EnemyWrestler2");
				Instantiate(smackanimu, otherObject.transform.position, Quaternion.identity);
				audio.PlayOneShot(smacksound);
				if (initialdmg)
					enemyscript.TakeDamage(4);
				else
				enemyscript.TakeDamage(1);	
			}
			
			if (otherObject.tag == "Hardcore3")
			{
				EnemyWrestler3 enemyscript = (EnemyWrestler3)otherObject.gameObject.GetComponent("EnemyWrestler3");
				Instantiate(smackanimu, otherObject.transform.position, Quaternion.identity);
				audio.PlayOneShot(smacksound);
				if (initialdmg)
					enemyscript.TakeDamage(4);
				else
				enemyscript.TakeDamage(1);	
			}
			
			if (otherObject.tag == "Ground")
			{
				EnemyGround enemyscript = (EnemyGround)otherObject.gameObject.GetComponent("EnemyGround");
				Instantiate(smackanimu, otherObject.transform.position, Quaternion.identity);
				audio.PlayOneShot(smacksound);
				enemyscript.Die();
			}
			
			if (otherObject.tag == "Ground2" || otherObject.tag == "Ground3")
			{
				EnemyGround3 enemyscript = (EnemyGround3)otherObject.gameObject.GetComponent("EnemyGround3");
				Instantiate(smackanimu, otherObject.transform.position, Quaternion.identity);
				audio.PlayOneShot(smacksound);
				enemyscript.Die();
			}
			
			
		}
		
		//regular wall effects
		if (otherObject.tag == "Enemy")
		{
			Punk1 enemyscript = (Punk1)otherObject.gameObject.GetComponent("Punk1");
			if (enemyscript.tackled)
			otherObject.rigidbody.velocity = new Vector3(Getdiff().x, 600, 0);
			else
			otherObject.rigidbody.velocity *= -0.3f;
		}
		
		if (otherObject.tag == "Enemythrower")
		{
			Punkthrower enemyscript = (Punkthrower)otherObject.gameObject.GetComponent("Punkthrower");
			if (enemyscript.tackled)
			otherObject.rigidbody.velocity = new Vector3(Getdiff().x, 600, 0);
			else
			otherObject.rigidbody.velocity *= -0.3f;
		}
		
		if (otherObject.tag == "Enemy2")
		{
			Punk2 enemyscript = (Punk2)otherObject.gameObject.GetComponent("Punk2");
			if (enemyscript.tackled)
			otherObject.rigidbody.velocity = new Vector3(Getdiff().x, 600, 0);
			else
			otherObject.rigidbody.velocity *= -0.3f;
		}
		
		if (otherObject.tag == "Enemy3")
		{
			Punk3 enemyscript = (Punk3)otherObject.gameObject.GetComponent("Punk3");
			if (enemyscript.tackled)
			otherObject.rigidbody.velocity = new Vector3(Getdiff().x, 600, 0);
			else
			otherObject.rigidbody.velocity *= -0.3f;
		}
		
		if (otherObject.tag == "Hardcore")
		{
			EnemyWrestler enemyscript = (EnemyWrestler)otherObject.gameObject.GetComponent("EnemyWrestler");
			enemyscript.dbzmode = true;
		}
		
		if (otherObject.tag == "Hardcore2")
		{
			EnemyWrestler2 enemyscript = (EnemyWrestler2)otherObject.gameObject.GetComponent("EnemyWrestler2");
			enemyscript.dbzmode = true;
		}
		
		if (otherObject.tag == "Hardcore3")
		{
			EnemyWrestler3 enemyscript = (EnemyWrestler3)otherObject.gameObject.GetComponent("EnemyWrestler3");
			enemyscript.dbzmode = true;
		}
		
		if (otherObject.tag == "Bomb")
		{
			zoomzoom zzscript = (zoomzoom)otherObject.gameObject.GetComponent("zoomzoom");
			zzscript.flyaway();
			
		}
		
		if (otherObject.tag == "Bullet")
			Destroy(otherObject.gameObject);
		
		if (PlayerPrefs.GetString("WallT1") == "white")
		{
			if (otherObject.tag == "Player")
			{
				if (playerscript.attackbuff)
					playerscript.bufftimer = 0;
				playerscript.attackbuff = true;	
				
			}
			
		}
		
		if (otherObject.tag == "Wall")
		{
			stack = true;
		}
		
	}// END TRIGGERENTER
	
	void OnTriggerStay(Collider otherObject)
	{
		if (otherObject.tag == "Wall")
		{
			stack = true;
		}
	
	}
	
//	void OnGUI()
//	{
//				GUI.color = Color.red;
//		GUI.Label(new Rect(0, 180, 200, 100), "Stack: " + stack.ToString());	
//		
//	}
}
