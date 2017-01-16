using UnityEngine;
using System.Collections;

public class barrier : MonoBehaviour {
	
	private float destroydelay;
	public AudioClip shine, smack;
	public GameObject player;
	public Player playerscript;
	
	public Collider searingwind;
	public ParticleSystem smackanimu;
	private bool dmgready = false;
	private float dmgCD;

	void Start () {
		
		searingwind.enabled = false;
		player = GameObject.FindGameObjectWithTag("Player");
		playerscript = (Player)GameObject.FindGameObjectWithTag("Player").GetComponent("Player");
		audio.PlayOneShot(shine);
		
		if (PlayerPrefs.GetString("SerenityT1") == "black")
			playerscript.shieldlimit = 6;
		else 
			playerscript.shieldlimit = 3;
		
		if (PlayerPrefs.GetString("SerenityT2") == "black")
			searingwind.enabled = true;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, -50);
		transform.Rotate(new Vector3(0,0,1) * 1);
		
		if (PlayerPrefs.GetString("SerenityT1") != "white" && playerscript.shieldhit >= playerscript.shieldlimit)
		{
			playerscript.shieldhit = 0;
			playerscript.shielded = false;
			Destroy(this.gameObject);
		}
		
		if (PlayerPrefs.GetString("SerenityT1") == "white")
		{
			destroydelay += Time.deltaTime;
			if (destroydelay - Time.deltaTime > 5f)
			{
				playerscript.shielded = false;
				Destroy(this.gameObject);
			}
		}
		
		if (PlayerPrefs.GetString("SerenityT2") == "black")
		{
			if (!dmgready)
			{
				dmgCD += Time.deltaTime;
				if (dmgCD - Time.deltaTime >= 0.25f)
				{
					dmgready = true;	
					dmgCD = 0;
				}
				
			}
			
			
		}
		
	
	}//update

	void OnTriggerStay(Collider otherObject)
	{
		if (dmgready)
		{
			if (otherObject.tag == "Enemy")
			{
				Punk1 enemyscript = (Punk1)otherObject.gameObject.GetComponent("Punk1");
				enemyscript.Takedamage(0.5f);
				audio.PlayOneShot(smack);
				Instantiate(smackanimu, 
				new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
				dmgready = false;
			}
			
			if (otherObject.tag == "Enemythrower")
			{
				Punkthrower enemyscript = (Punkthrower)otherObject.gameObject.GetComponent("Punkthrower");
				enemyscript.Takedamage(0.5f);
				audio.PlayOneShot(smack);
				Instantiate(smackanimu, 
				new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
				dmgready = false;
			}
			
			if (otherObject.tag == "Enemy2")
			{
				Punk2 enemyscript = (Punk2)otherObject.gameObject.GetComponent("Punk2");
				enemyscript.Takedamage(0.5f);
				audio.PlayOneShot(smack);
				Instantiate(smackanimu, 
				new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
				dmgready = false;
			}
			
			if (otherObject.tag == "Enemy3")
			{
				Punk3 enemyscript = (Punk3)otherObject.gameObject.GetComponent("Punk3");
				enemyscript.Takedamage(0.5f);
				audio.PlayOneShot(smack);
				Instantiate(smackanimu, 
				new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
				dmgready = false;
			}
			
			if (otherObject.tag == "Hardcore")
			{
				EnemyWrestler enemyscript = (EnemyWrestler)otherObject.gameObject.GetComponent("EnemyWrestler");
				enemyscript.FlyAway();
				audio.PlayOneShot(smack);
				Instantiate(smackanimu, 
				new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
				dmgready = false;
			}
			
			if (otherObject.tag == "Hardcore2")
			{
				EnemyWrestler2 enemyscript = (EnemyWrestler2)otherObject.gameObject.GetComponent("EnemyWrestler2");
				enemyscript.TakeDamage(0.5f);
				audio.PlayOneShot(smack);
				Instantiate(smackanimu, 
				new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
				dmgready = false;
			}
			
			if (otherObject.tag == "Hardcore3")
			{
				EnemyWrestler3 enemyscript = (EnemyWrestler3)otherObject.gameObject.GetComponent("EnemyWrestler3");
				enemyscript.TakeDamage(0.5f);
				audio.PlayOneShot(smack);
				Instantiate(smackanimu, 
				new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
				dmgready = false;
			}
			
			if (otherObject.tag == "Ground")
			{
				EnemyGround enemyscript = (EnemyGround)otherObject.gameObject.GetComponent("EnemyGround");
				enemyscript.Die();
				audio.PlayOneShot(smack);
				Instantiate(smackanimu, 
				new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
				dmgready = false;
			}
			
			if (otherObject.tag == "Ground2")
			{
				EnemyGround2 enemyscript = (EnemyGround2)otherObject.gameObject.GetComponent("EnemyGround2");
				enemyscript.Die();
				audio.PlayOneShot(smack);
				Instantiate(smackanimu, 
				new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
				dmgready = false;
			}
			
			if (otherObject.tag == "Ground3")
			{
				EnemyGround3 enemyscript = (EnemyGround3)otherObject.gameObject.GetComponent("EnemyGround3");
				enemyscript.Die();
				audio.PlayOneShot(smack);
				Instantiate(smackanimu, 
				new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
				dmgready = false;
			}
			
			if (otherObject.tag == "Ninja1")
			{
				Ninja1 enemyscript = (Ninja1)otherObject.gameObject.GetComponent("Ninja1");
				enemyscript.TakeDamage(0.5f);
				audio.PlayOneShot(smack);
				Instantiate(smackanimu, 
				new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
				dmgready = false;
			}
			
			if (otherObject.tag == "Ninja2")
			{
				Ninja2 enemyscript = (Ninja2)otherObject.gameObject.GetComponent("Ninja2");
				enemyscript.TakeDamage(0.5f);
				audio.PlayOneShot(smack);
				Instantiate(smackanimu, 
				new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
				dmgready = false;
			}
			
			if (otherObject.tag == "Bomb")
			{
				zoomzoom zzscript = (zoomzoom)otherObject.gameObject.GetComponent("zoomzoom");
				if (zzscript.isActive)
						zzscript.health--;
					zzscript.flyaway();
				audio.PlayOneShot(smack);
				Instantiate(smackanimu, 
				new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
				dmgready = false;
			}
			
			if (otherObject.tag == "Boss")
			{
				Boss1 bossscript = (Boss1)otherObject.gameObject.GetComponent("Boss1");
				bossscript.GetHit(1);
				audio.PlayOneShot(smack);
				Instantiate(smackanimu, 
				new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
				dmgready = false;
			}
			
			if (otherObject.tag == "Minion")
			{
				Boss1 boss1script = (Boss1)otherObject.transform.parent.gameObject.GetComponent("Boss1");
				boss1script.m1GetHit(1);
				audio.PlayOneShot(smack);
				Instantiate(smackanimu, 
				new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
				dmgready = false;
			}
			
			if (otherObject.tag == "Minion2")
			{
				Boss1 boss1script = (Boss1)otherObject.transform.parent.gameObject.GetComponent("Boss1");
				boss1script.m2GetHit(1);
				audio.PlayOneShot(smack);
				Instantiate(smackanimu, 
				new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
				dmgready = false;
			}
			
			
			
			
		}
	}

}
