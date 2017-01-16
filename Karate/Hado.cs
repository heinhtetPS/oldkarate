using UnityEngine;
using System.Collections;

public class Hado : MonoBehaviour {
	
	public exSprite hadwo;
	private bool gotrandom = false;
	public Player playerscript;
	public Karateoboxnew obox;
	private float speed = 500;
	private float mouseX, mouseY, diffX, diffY, cameraDif;
	
	public ParticleSystem smackanimu;
	public AudioClip smack, finalsmack;

	
	private float lifetime;
	
	// Use this for initialization
	void Start () {
		
		playerscript = (Player)GameObject.FindGameObjectWithTag("Player").GetComponent("Player");
		obox = (Karateoboxnew)GameObject.FindGameObjectWithTag("Offense").GetComponent("Karateoboxnew");
		
//		if (playerscript.MousetotheRight())
//		rigidbody.velocity = new Vector3 (300, 0, 0) * speed;
//		
//		if (!playerscript.MousetotheRight())
//		rigidbody.velocity = new Vector3 (-300, 0, 0) * speed;
		
		//mouserotate
		cameraDif = Camera.main.transform.position.y - transform.position.y;
		mouseX = Input.mousePosition.x;
		mouseY = Input.mousePosition.y;
		Vector3 mWorldPos = Camera.main.ScreenToWorldPoint( new Vector3(mouseX, mouseY, cameraDif));
		
		diffX = mWorldPos.x - transform.position.x;
	    diffY = mWorldPos.y  - transform.position.y;

		float angle = Mathf.Atan2(diffY, diffX) * Mathf.Rad2Deg;
		
		rigidbody.velocity = Vector3.Normalize( new Vector3(diffX, diffY, 0)) * speed;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
		
		
		
		
	
	}

	
	// Update is called once per frame
	void Update () 
	{
		
		if (transform.position.z != -100)
			transform.position = new Vector3(transform.position.x, transform.position.y, -100);
		
		if (transform.position.x >= 800 || transform.position.x <= -900 || transform.position.y >= 600)
			Destroy(this.gameObject);
	}
	
	
	void OnTriggerEnter (Collider otherObject) {
		
		if (otherObject.tag == "Enemy")
		{
			Punk1 enemyscript = (Punk1)otherObject.gameObject.GetComponent("Punk1");	
			enemyscript.dbzmode = true;
			enemyscript.comboedcount++;
			enemyscript.comboedreset = 0;
			enemyscript.Bleedcash();
			if (enemyscript.health <= playerscript.attackdmg)
			{
				obox.Makelines();
				audio.PlayOneShot(finalsmack);
			}
			enemyscript.health -= playerscript.attackdmg;
			playerscript.balls += 0.125f;
			Instantiate(smackanimu, 
			new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
			audio.PlayOneShot(smack);
			
			if (obox.comboing)
			{
				obox.combocounter++;	
				obox.combotimer = 0f;
			}
			StartCoroutine( DelayedDestroy () );
		}
		
		if (otherObject.tag == "Enemy2")
		{
			Punk2 enemyscript = (Punk2)otherObject.gameObject.GetComponent("Punk2");	
			enemyscript.dbzmode = true;
			enemyscript.comboedcount++;
			enemyscript.comboedreset = 0;
			enemyscript.Bleedcash();
			if (enemyscript.health <= playerscript.attackdmg)
			{
				obox.Makelines();
				audio.PlayOneShot(finalsmack);
			}
			enemyscript.health -= playerscript.attackdmg;
			playerscript.balls += 0.125f;
			Instantiate(smackanimu, 
			new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
			audio.PlayOneShot(smack);
			
			if (obox.comboing)
			{
				obox.combocounter++;	
				obox.combotimer = 0f;
			}
			StartCoroutine( DelayedDestroy () );
		}
		
		if (otherObject.tag == "Enemy3")
		{
			Punk3 enemyscript = (Punk3)otherObject.gameObject.GetComponent("Punk3");	
			enemyscript.dbzmode = true;
			enemyscript.comboedcount++;
			enemyscript.comboedreset = 0;
			enemyscript.Bleedcash();
			if (enemyscript.health <= playerscript.attackdmg)
			{
				obox.Makelines();
				audio.PlayOneShot(finalsmack);
			}
			enemyscript.health -= playerscript.attackdmg;
			playerscript.balls += 0.125f;
			Instantiate(smackanimu, 
			new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
			audio.PlayOneShot(smack);
			
			if (obox.comboing)
			{
				obox.combocounter++;	
				obox.combotimer = 0f;
			}
			StartCoroutine( DelayedDestroy () );
		}
		
		if (otherObject.tag == "Hardcore")
		{
			EnemyWrestler enemyscript = (EnemyWrestler)otherObject.gameObject.GetComponent("EnemyWrestler");	
			enemyscript.dbzmode = true;
			if (enemyscript.health <= playerscript.attackdmg)
			{
				obox.Makelines();
				audio.PlayOneShot(finalsmack);
			}
			enemyscript.health -= playerscript.attackdmg;
			playerscript.balls += 0.125f;
			Instantiate(smackanimu, 
			new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
			audio.PlayOneShot(smack);
			
			if (obox.comboing)
			{
				obox.combocounter++;	
				obox.combotimer = 0f;
			}
			StartCoroutine( DelayedDestroy () );
		}
		
		if (otherObject.tag == "Hardcore2")
		{
			EnemyWrestler2 enemyscript = (EnemyWrestler2)otherObject.gameObject.GetComponent("EnemyWrestler2");	
			enemyscript.dbzmode = true;
			if (enemyscript.health <= playerscript.attackdmg)
			{
				obox.Makelines();
				audio.PlayOneShot(finalsmack);
			}
			enemyscript.health -= playerscript.attackdmg;
			playerscript.balls += 0.125f;
			Instantiate(smackanimu, 
			new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
			audio.PlayOneShot(smack);
			
			if (obox.comboing)
			{
				obox.combocounter++;	
				obox.combotimer = 0f;
			}
			StartCoroutine( DelayedDestroy () );
		}
		
		if (otherObject.tag == "Hardcore3")
		{
			EnemyWrestler3 enemyscript = (EnemyWrestler3)otherObject.gameObject.GetComponent("EnemyWrestler3");	
			enemyscript.dbzmode = true;
			if (enemyscript.health <= playerscript.attackdmg)
			{
				obox.Makelines();
				audio.PlayOneShot(finalsmack);
			}
			enemyscript.health -= playerscript.attackdmg;
			playerscript.balls += 0.125f;
			Instantiate(smackanimu, 
			new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
			audio.PlayOneShot(smack);
			
			if (obox.comboing)
			{
				obox.combocounter++;	
				obox.combotimer = 0f;
			}
			StartCoroutine( DelayedDestroy () );
		}
		
		if (otherObject.tag == "Ground")
		{
			EnemyGround enemyscript = (EnemyGround)otherObject.gameObject.GetComponent("EnemyGround");	
			enemyscript.Die();
			playerscript.balls += 0.125f;
			Instantiate(smackanimu, 
			new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
			audio.PlayOneShot(smack);
			
			if (obox.comboing)
			{
				obox.combocounter++;	
				obox.combotimer = 0f;
			}
			StartCoroutine( DelayedDestroy () );
		}
		
		if (otherObject.tag == "Ground2")
		{
			EnemyGround2 enemyscript = (EnemyGround2)otherObject.gameObject.GetComponent("EnemyGround2");	
			enemyscript.Die();
			playerscript.balls += 0.125f;
			Instantiate(smackanimu, 
			new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
			audio.PlayOneShot(smack);
			
			if (obox.comboing)
			{
				obox.combocounter++;	
				obox.combotimer = 0f;
			}
			StartCoroutine( DelayedDestroy () );
		}
		
		if (otherObject.tag == "Ground3")
		{
			EnemyGround3 enemyscript = (EnemyGround3)otherObject.gameObject.GetComponent("EnemyGround3");	
			enemyscript.Die();
			playerscript.balls += 0.125f;
			Instantiate(smackanimu, 
			new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
			audio.PlayOneShot(smack);
			
			if (obox.comboing)
			{
				obox.combocounter++;	
				obox.combotimer = 0f;
			}
			StartCoroutine( DelayedDestroy () );
		}
		
		if (otherObject.tag == "Ninja1")
		{
			Ninja1 enemyscript = (Ninja1)otherObject.gameObject.GetComponent("Ninja1");	
			if (enemyscript.health <= playerscript.attackdmg)
			{
				obox.Makelines();
				audio.PlayOneShot(finalsmack);
			}
			enemyscript.health -= playerscript.attackdmg;
			playerscript.balls += 0.125f;
			Instantiate(smackanimu, 
			new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
			audio.PlayOneShot(smack);
			
			if (obox.comboing)
			{
				obox.combocounter++;	
				obox.combotimer = 0f;
			}
			StartCoroutine( DelayedDestroy () );
		}
		
		if (otherObject.tag == "Ninja2")
		{
			Ninja2 enemyscript = (Ninja2)otherObject.gameObject.GetComponent("Ninja2");	
			if (enemyscript.health <= playerscript.attackdmg)
			{
				obox.Makelines();
				audio.PlayOneShot(finalsmack);
			}
			enemyscript.health -= playerscript.attackdmg;
			playerscript.balls += 0.125f;
			Instantiate(smackanimu, 
			new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
			audio.PlayOneShot(smack);
			
			if (obox.comboing)
			{
				obox.combocounter++;	
				obox.combotimer = 0f;
			}
			StartCoroutine( DelayedDestroy () );
		}
		
		if (otherObject.tag == "Bomb")
		{
			zoomzoom enemyscript = (zoomzoom)otherObject.gameObject.GetComponent("zoomzoom");	
			if (enemyscript.isActive)
			{
				enemyscript.health -= playerscript.attackdmg;
				enemyscript.flyaway();
			}
			playerscript.balls += 0.125f;
			Instantiate(smackanimu, 
			new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
			audio.PlayOneShot(smack);
			
			if (obox.comboing)
			{
				obox.combocounter++;	
				obox.combotimer = 0f;
			}
			StartCoroutine( DelayedDestroy () );
		}
		
		if (otherObject.tag == "Boss")
		{
			Boss1 enemyscript = (Boss1)otherObject.transform.parent.GetComponent("Boss1");	
			enemyscript.GetHit(playerscript.attackdmg);
			playerscript.balls += 0.125f;
			Instantiate(smackanimu, 
			new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
			audio.PlayOneShot(smack);
			
			if (obox.comboing)
			{
				obox.combocounter++;	
				obox.combotimer = 0f;
			}
			StartCoroutine( DelayedDestroy () );
		}
		
		if (otherObject.tag == "Minion")
		{
			Boss1 enemyscript = (Boss1)otherObject.transform.parent.gameObject.GetComponent("Boss1");	
			enemyscript.m1GetHit(playerscript.attackdmg);
			playerscript.balls += 0.125f;
			Instantiate(smackanimu, 
			new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
			audio.PlayOneShot(smack);
			
			if (obox.comboing)
			{
				obox.combocounter++;	
				obox.combotimer = 0f;
			}
			StartCoroutine( DelayedDestroy () );
		}
		
		if (otherObject.tag == "Minion2")
		{
			Boss1 enemyscript = (Boss1)otherObject.transform.parent.gameObject.GetComponent("Boss1");	
			enemyscript.m2GetHit(playerscript.attackdmg);
			playerscript.balls += 0.125f;
			Instantiate(smackanimu, 
			new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
			audio.PlayOneShot(smack);
			
			if (obox.comboing)
			{
				obox.combocounter++;	
				obox.combotimer = 0f;
			}
			StartCoroutine( DelayedDestroy () );
		}
		
		if (otherObject.tag == "Sbomb")
		{
			SpiritBomb sbscript = (SpiritBomb)otherObject.gameObject.GetComponent("SpiritBomb");	
			if (sbscript.whitet1 || sbscript.whitet2)
			{
				Instantiate(smackanimu, 
				new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
				audio.PlayOneShot(smack);
				
				if (obox.comboing)
				{
					obox.combocounter++;	
					obox.combotimer = 0f;
				}
				sbscript.TriggerExplosion();
				playerscript.balls += 0.125f;
				StartCoroutine( DelayedDestroy () );
			}
		}
		
		if (otherObject.tag == "ATM")
		{
			atmachine atmscript = (atmachine)otherObject.gameObject.GetComponent("atmachine");
			Instantiate(smackanimu, 
				new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
				audio.PlayOneShot(smack);
			otherObject.audio.Play();
					atmscript.shake();
					if (atmscript.health <= playerscript.attackdmg)
						obox.Makelines();
					atmscript.health -= playerscript.attackdmg;
					atmscript.ttg += 0.3f;
					atmscript.releasecoins();
			
			if (obox.comboing)
			{
				obox.combocounter++;	
				obox.combotimer = 0f;
			}
			playerscript.balls += 0.125f;
			StartCoroutine( DelayedDestroy () );
		}
		
		
		
		
	}
	
	IEnumerator DelayedDestroy()
	{
		renderer.enabled = false;
		
		yield return new WaitForSeconds(0.2f);
		
		Destroy(this.gameObject);
		
	}
		
}
