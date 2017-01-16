using UnityEngine;
using System.Collections;

public class Teledoor : MonoBehaviour {
	
	public exSpriteAnimation dooranim;
	public bool T1black = false, T2black = false, T1white = false, T2white = false, 
				CCready = true, Eatready = true, buffready = true, santaready = true;
	private float CCcooldown, Eatcooldown, buffcooldown, santacooldown, generaltimer, activetimer;
	public GameObject karateman, sushi, ball, cash;
	public AudioClip ccsound, eatsound;
	
	public float Karatelocation;
	
	
	// Use this for initialization
	void Start () {
		
		karateman = GameObject.FindGameObjectWithTag("Player");
		Karatelocation = karateman.transform.position.y;
		
		if (PlayerPrefs.GetString("TeledoorT1") == "black")
			T1black = true;
		if (PlayerPrefs.GetString("TeledoorT1") == "white")
			T1white = true;
		if (PlayerPrefs.GetString("TeledoorT2") == "black")
			T2black = true;
		if (PlayerPrefs.GetString("TeledoorT2") == "white")
			T2white = true;
		
		
		if (T2black)
			Eatready = true;
		if (T1black)
			CCready = true;
		if (T1white)
			santaready = true;
		if (T2white)
			buffready = true;
	
	}
	
	// Update is called once per frame
	void Update () {
		
		
		
//		//coming down 
//		if (transform.position.y > Karatelocation && transform.position.y >= -210)
//			transform.Translate(new Vector3(0, -600, 0) * Time.deltaTime);
		
		//general uptime and cleanup
		generaltimer += Time.deltaTime;
		if (generaltimer - Time.deltaTime >= 8)
			Destroy(this.gameObject);
		
		if (T1black)
		{	
			if (!CCready)
			{
				CCcooldown += Time.deltaTime;
				if (CCcooldown - Time.deltaTime >= 3)
				{
					CCready = true;
					CCcooldown = 0;
				}
			}
		}
		
		if (T1white)
		{	
			if (!santaready)
			{
				santacooldown += Time.deltaTime;
				if (santacooldown - Time.deltaTime >= 3)
				{
					santaready = true;
					santacooldown = 0;
				}
			}
			
		}
		
		if (T2black)
		{
			if (!Eatready)
			{
				Eatcooldown += Time.deltaTime;
				if (Eatcooldown - Time.deltaTime >= 4)
				{
					Eatready = true;	
					Eatcooldown = 0;
				}
				
			}
			
		}
		
		
		if (T2white)
		{
			if (!buffready)
			{
				buffcooldown += Time.deltaTime;
				if (buffcooldown - Time.deltaTime >= 2)
				{
					buffready = true;
					buffcooldown = 0;
				}
			}
			
		}
		
	
	}
	
	void OnTriggerEnter (Collider otherObject) 
	{
		if (T1black && CCready && !Eatready)
		{	
			if (otherObject.tag == "Enemy")
			{
				audio.PlayOneShot(ccsound);
				Punk1 enemyscript = (Punk1)otherObject.gameObject.GetComponent("Punk1");
				enemyscript.dbzmode = true;
				enemyscript.stunned = true;
				CCready = false;
			}
			
			if (otherObject.tag == "Enemythrower")
			{
				audio.PlayOneShot(ccsound);
				Punkthrower enemyscript = (Punkthrower)otherObject.gameObject.GetComponent("Punkthrower");
				enemyscript.dbzmode = true;
				enemyscript.stunned = true;
				CCready = false;
			}
			
			if (otherObject.tag == "Enemy2")
			{
				audio.PlayOneShot(ccsound);
				Punk2 enemyscript = (Punk2)otherObject.gameObject.GetComponent("Punk2");
				enemyscript.dbzmode = true;
				enemyscript.stunned = true;
				CCready = false;
			}
			
			if (otherObject.tag == "Enemy3")
			{
				audio.PlayOneShot(ccsound);
				Punk3 enemyscript = (Punk3)otherObject.gameObject.GetComponent("Punk3");
				enemyscript.dbzmode = true;
				enemyscript.stunned = true;
				CCready = false;
			}
			
			if (otherObject.tag == "Hardcore")
			{
				audio.PlayOneShot(ccsound);
				EnemyWrestler enemyscript = (EnemyWrestler)otherObject.gameObject.GetComponent("EnemyWrestler");
				enemyscript.stunned = true;
				CCready = false;
			}
			
			if (otherObject.tag == "Hardcore2")
			{
				audio.PlayOneShot(ccsound);
				EnemyWrestler2 enemyscript = (EnemyWrestler2)otherObject.gameObject.GetComponent("EnemyWrestler2");
				enemyscript.stunned = true;
				CCready = false;
			}
			
			if (otherObject.tag == "Hardcore3")
			{
				audio.PlayOneShot(ccsound);
				EnemyWrestler3 enemyscript = (EnemyWrestler3)otherObject.gameObject.GetComponent("EnemyWrestler3");
				enemyscript.stunned = true;
				CCready = false;
			}
			
			if (otherObject.tag == "Bomb")
			{
				audio.PlayOneShot(ccsound);
				zoomzoom enemyscript = (zoomzoom)otherObject.gameObject.GetComponent("zoomzoom");
				enemyscript.flyaway();
				CCready = false;
			}
			
		}
		
		if (T2black && Eatready)
		{
			if (otherObject.tag == "Enemy")
			{
				dooranim.PlayDefault();
				audio.PlayOneShot(eatsound);
				Punk1 enemyscript = (Punk1)otherObject.gameObject.GetComponent("Punk1");
				enemyscript.dbzmode = true;
				enemyscript.stunned = true;
				enemyscript.PlayRandom();
				StartCoroutine ( DelayedDestroy (otherObject.gameObject) );
				Eatready = false;
				StartCoroutine ( DelayedDestroy (this.gameObject) );
			}
			
			if (otherObject.tag == "Enemythrower")
			{
				dooranim.PlayDefault();
				audio.PlayOneShot(eatsound);
				Punkthrower enemyscript = (Punkthrower)otherObject.gameObject.GetComponent("Punkthrower");
				enemyscript.dbzmode = true;
				enemyscript.stunned = true;
				StartCoroutine ( DelayedDestroy (otherObject.gameObject) );
				Eatready = false;
				StartCoroutine ( DelayedDestroy (this.gameObject) );
			}
			
			if (otherObject.tag == "Enemy2")
			{
				dooranim.PlayDefault();
				audio.PlayOneShot(eatsound);
				Punk2 enemyscript = (Punk2)otherObject.gameObject.GetComponent("Punk2");
				enemyscript.dbzmode = true;
				enemyscript.stunned = true;
				StartCoroutine ( DelayedDestroy (otherObject.gameObject) );
				Eatready = false;
				StartCoroutine ( DelayedDestroy (this.gameObject) );
			}
			
			if (otherObject.tag == "Enemy3")
			{
				dooranim.PlayDefault();
				audio.PlayOneShot(eatsound);
				Punk3 enemyscript = (Punk3)otherObject.gameObject.GetComponent("Punk3");
				enemyscript.dbzmode = true;
				enemyscript.stunned = true;
				StartCoroutine ( DelayedDestroy (otherObject.gameObject) );
				Eatready = false;
				StartCoroutine ( DelayedDestroy (this.gameObject) );
			}

			if (otherObject.tag == "Hardcore")
			{
				dooranim.PlayDefault();
				audio.PlayOneShot(eatsound);
				EnemyWrestler enemyscript = (EnemyWrestler)otherObject.gameObject.GetComponent("EnemyWrestler");
				enemyscript.dbzmode = true;
				enemyscript.stunned = true;
				StartCoroutine ( DelayedDestroy (otherObject.gameObject) );
				Eatready = false;
				StartCoroutine ( DelayedDestroy (this.gameObject) );
			}
			
			if (otherObject.tag == "Hardcore2")
			{
				dooranim.PlayDefault();
				audio.PlayOneShot(eatsound);
				EnemyWrestler2 enemyscript = (EnemyWrestler2)otherObject.gameObject.GetComponent("EnemyWrestler2");
				enemyscript.dbzmode = true;
				enemyscript.stunned = true;
				StartCoroutine ( DelayedDestroy (otherObject.gameObject) );
				Eatready = false;
				StartCoroutine ( DelayedDestroy (this.gameObject) );
			}
		
			if (otherObject.tag == "Hardcore3")
			{
				dooranim.PlayDefault();
				audio.PlayOneShot(eatsound);
				EnemyWrestler3 enemyscript = (EnemyWrestler3)otherObject.gameObject.GetComponent("EnemyWrestler3");
				enemyscript.dbzmode = true;
				enemyscript.stunned = true;
				StartCoroutine ( DelayedDestroy (otherObject.gameObject) );
				Eatready = false;
				StartCoroutine ( DelayedDestroy (this.gameObject) );
			}
			
			if (otherObject.tag == "Ninja1")
			{
				dooranim.PlayDefault();
				audio.PlayOneShot(eatsound);
				Ninja1 enemyscript = (Ninja1)otherObject.gameObject.GetComponent("Ninja1");
				enemyscript.dbzmode = true;
				StartCoroutine ( DelayedDestroy (otherObject.gameObject) );
				Eatready = false;
				StartCoroutine ( DelayedDestroy (this.gameObject) );
			}
			
			if (otherObject.tag == "Ninja2")
			{
				dooranim.PlayDefault();
				audio.PlayOneShot(eatsound);
				Ninja2 enemyscript = (Ninja2)otherObject.gameObject.GetComponent("Ninja2");
				enemyscript.dbzmode = true;
				StartCoroutine ( DelayedDestroy (otherObject.gameObject) );
				Eatready = false;
				StartCoroutine ( DelayedDestroy (this.gameObject) );
			}
			
			if (otherObject.tag == "Bomb")
			{
				dooranim.PlayDefault();
				audio.PlayOneShot(eatsound);
				zoomzoom enemyscript = (zoomzoom)otherObject.gameObject.GetComponent("zoomzoom");
				enemyscript.isActive = false;
				StartCoroutine ( DelayedDestroy (otherObject.gameObject) );
				Eatready = false;
				StartCoroutine ( DelayedDestroy (this.gameObject) );
			}
			
			
			
		}
		
		
		if (otherObject.tag == "Player")
		{
			Player playerscript = (Player)otherObject.gameObject.GetComponent("Player");
			
			if (T1white && santaready)
			{
				Randomitemspawn(otherObject.gameObject);
				Destroy(this.gameObject);
			}
			
			if (T2white && buffready)
			{
				if (playerscript.attackbuff)
					playerscript.bufftimer = 0;
				playerscript.attackbuff = true;
				buffready = false;
				Destroy(this.gameObject);
			}
			
		}
		
		
	}
	
	void Randomitemspawn(GameObject you)
	{
		int roll = Random.Range(1,4);
		
		if (roll == 1)
		{
			GameObject thisitem = (GameObject)Instantiate(sushi, you.transform.position, you.transform.rotation);
			thisitem.rigidbody.velocity = new Vector3(Random.Range(-300, 300), Random.Range(0, 400), 0);
		}
		if (roll == 2)
		{
			GameObject thisitem = (GameObject)Instantiate(ball, you.transform.position, you.transform.rotation);
			thisitem.rigidbody.velocity = new Vector3(Random.Range(-300, 300), Random.Range(0, 400), 0);
		}
		if (roll == 3)
		{
			GameObject thisitem = (GameObject)Instantiate(cash, you.transform.position, you.transform.rotation);
			thisitem.rigidbody.velocity = new Vector3(Random.Range(-300, 300), Random.Range(0, 400), 0);
		}
		
		santaready = false;
	}
	
//	void OnGUI()
//	{
//		GUI.color = Color.black;
//		GUI.Label(new Rect(0, 160, 200, 100), "ccready:" + CCready.ToString());
//		GUI.Label(new Rect(0, 180, 200, 100), "eatready: " + Eatready.ToString());
//		GUI.Label(new Rect(0, 200, 200, 100), "santaready: " + santaready.ToString());
//		GUI.Label(new Rect(0, 220, 200, 100), "buffreadyu: " + buffready.ToString());
//		
//	}
	
	IEnumerator DelayedDestroy(GameObject victim)
	{
		yield return new WaitForSeconds(0.6f);
		
		Destroy(victim);
		
	}
	
}
