using UnityEngine;
using System.Collections;

public class Fakeobox : MonoBehaviour {

	public GameObject Karateman, anotherclone;
	public karatemanmirror fakeman;
	public Player playerscript;
	
	public exAtlas karatemanatlas;
	
	private int combocounter = 0;
	private bool comboonCD = false;
	private float comboCD, idletimer;
	
	private bool juxtaposeready = true;
	private float juxtCD;
	
	
	public bool dmgready = true;
	private float dmgCD;
	
	public ParticleSystem smackanimu;
	public AudioClip smacksound;
	
	void Start () {
		
		playerscript = (Player)GameObject.FindGameObjectWithTag("Player").GetComponent("Player");
		
	
	}
	

	void Update () {
		
		transform.position = Karateman.transform.position;
		
		if (PlayerPrefs.GetString("CloneT2") != "white")
		{
			if (fakeman.atkready)
			dmgready = true;
			
		}
		
		
		if (PlayerPrefs.GetString("CloneT2") == "white")
		{
			//delay after LAST attack
			if (dmgready && fakeman.attackcounter == 1)
			{
				dmgCD += Time.deltaTime;
				//working one is .33
			if (dmgCD >= 0.33f)
				{
					dmgready = false;
					dmgCD = 0;
				}
			}
			
			//delay after FIRST attack
			if (dmgready && fakeman.attackcounter == 2)
			{
				dmgCD += Time.deltaTime;
				//working one is .3
			if (dmgCD >= 0.3f)
				{
					dmgready = false;
					dmgCD = 0;
				}
			}
			
			//delay after SECOND attack
			if (dmgready && fakeman.attackcounter == 3)
			{
				dmgCD += Time.deltaTime;
				//working one is .35
			if (dmgCD >=0.35f)
				{
					dmgready = false;
					dmgCD = 0;
				}
			}
		}
		
		if (combocounter > 0)
		{
			idletimer += Time.deltaTime;
			if (idletimer > 2)
			{
				combocounter = 0;	
				idletimer = 0;
			}
			
		}
		
		if (combocounter > 4)
		{
			comboonCD = true;
			combocounter = 0;	
		}
		
		if (comboonCD)
		{
			comboCD += Time.deltaTime;
			if (comboCD >= 0.5f)
			{
				comboonCD = false;	
				comboCD = 0;
			}
			
			
		}
		
		if (!juxtaposeready)
		{
			juxtCD += Time.deltaTime;	
			if (juxtCD > 1)
			{
				juxtaposeready = true;
				juxtCD = 0;
			}
			
		}
		
	
	}
	
	void OnTriggerStay(Collider otherObject)
	{
		if (dmgready)
		{
				if (otherObject.tag == "Enemy")
				{
					Punk1 enemyscript = (Punk1)otherObject.gameObject.GetComponent("Punk1");

					if (fakeman.dbzmode)
						fakeman.dbzmodetimer = 0;
					fakeman.dbzmode = true;
					if (enemyscript.dbzmode)
						enemyscript.dbzmodetimer = 0;
					enemyscript.dbzmode = true;
					
					
					if (!enemyscript.gotdmgbyclone)
					{
						enemyscript.Bleedcash();
						Instantiate(smackanimu, 
						new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
						audio.PlayOneShot(smacksound);
						enemyscript.health -= (int)playerscript.attackdmg;
						enemyscript.gotdmgbyclone = true;
					}
					dmgready = false;	
					combocounter++;
					if (PlayerPrefs.GetString("CloneT2") == "black" && juxtaposeready)
					juxtapose();
				
				}
				
				if (otherObject.tag == "Enemythrower")
				{
					Punkthrower enemyscript = (Punkthrower)otherObject.gameObject.GetComponent("Punkthrower");

					if (fakeman.dbzmode)
						fakeman.dbzmodetimer = 0;
					fakeman.dbzmode = true;
					if (enemyscript.dbzmode)
						enemyscript.dbzmodetimer = 0;
					enemyscript.dbzmode = true;
					
					if (!enemyscript.gotdmgbyclone)
					{
						enemyscript.Bleedcash();
						Instantiate(smackanimu, 
						new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
						audio.PlayOneShot(smacksound);
						enemyscript.health -= (int)playerscript.attackdmg;
						enemyscript.gotdmgbyclone = true;
					}
					dmgready = false;	
					combocounter++;
				
				}
				
				if (otherObject.tag == "Enemy2")
				{
					Punk2 enemyscript = (Punk2)otherObject.gameObject.GetComponent("Punk2");

					if (fakeman.dbzmode)
						fakeman.dbzmodetimer = 0;
					fakeman.dbzmode = true;
					if (enemyscript.dbzmode)
						enemyscript.dbzmodetimer = 0;
					enemyscript.dbzmode = true;
					
					if (!enemyscript.gotdmgbyclone)
					{
						enemyscript.Bleedcash();
						Instantiate(smackanimu, 
						new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
						audio.PlayOneShot(smacksound);
						enemyscript.health -= (int)playerscript.attackdmg;
						enemyscript.gotdmgbyclone = true;
					}
					dmgready = false;	
					combocounter++;
				
				}
				if (otherObject.tag == "Enemy3")
				{
					Punk3 enemyscript = (Punk3)otherObject.gameObject.GetComponent("Punk3");

					if (fakeman.dbzmode)
						fakeman.dbzmodetimer = 0;
					fakeman.dbzmode = true;
					if (enemyscript.dbzmode)
						enemyscript.dbzmodetimer = 0;
					enemyscript.dbzmode = true;
					
					if (!enemyscript.gotdmgbyclone)
					{
						enemyscript.Bleedcash();
						Instantiate(smackanimu, 
						new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
						audio.PlayOneShot(smacksound);
						enemyscript.health -= (int)playerscript.attackdmg;
						enemyscript.gotdmgbyclone = true;
					}
					dmgready = false;	
					combocounter++;
				
				}
				if (otherObject.tag == "Ground")
				{
					EnemyGround enemyscript = (EnemyGround)otherObject.gameObject.GetComponent("EnemyGround");

					if (fakeman.dbzmode)
						fakeman.dbzmodetimer = 0;
					fakeman.dbzmode = true;
					Instantiate(smackanimu, 
					new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
					audio.PlayOneShot(smacksound);
					enemyscript.Die();
					dmgready = false;	
					combocounter++;
				
				}
				if (otherObject.tag == "Ground2")
				{
					EnemyGround3 enemyscript = (EnemyGround3)otherObject.gameObject.GetComponent("EnemyGround3");

					if (fakeman.dbzmode)
						fakeman.dbzmodetimer = 0;
					fakeman.dbzmode = true;
					Instantiate(smackanimu, 
					new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
					audio.PlayOneShot(smacksound);
					enemyscript.Die();
					dmgready = false;	
					combocounter++;
				
				}
				if (otherObject.tag == "Ground3")
				{
					EnemyGround3 enemyscript = (EnemyGround3)otherObject.gameObject.GetComponent("EnemyGround3");

					if (fakeman.dbzmode)
						fakeman.dbzmodetimer = 0;
					fakeman.dbzmode = true;
					Instantiate(smackanimu, 
					new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
					audio.PlayOneShot(smacksound);
					enemyscript.Die();
					dmgready = false;	
					combocounter++;
				
				}
				
				if (otherObject.tag == "Ninja2")
				{
					Ninja2 enemyscript = (Ninja2)otherObject.gameObject.GetComponent("Ninja2");

					if (fakeman.dbzmode)
						fakeman.dbzmodetimer = 0;
					fakeman.dbzmode = true;
					enemyscript.dbzmode = true;
					Instantiate(smackanimu, 
					new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
					audio.PlayOneShot(smacksound);
					enemyscript.TakeDamage(playerscript.attackdmg);
					dmgready = false;	
					combocounter++;
				
				}
				if (otherObject.tag == "Bomb")
				{
					zoomzoom enemyscript = (zoomzoom)otherObject.gameObject.GetComponent("zoomzoom");

					if (fakeman.dbzmode)
						fakeman.dbzmodetimer = 0;
					fakeman.dbzmode = true;
					if (enemyscript.isActive)
						enemyscript.flyaway();
					Instantiate(smackanimu, 
					new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
					audio.PlayOneShot(smacksound);
					enemyscript.health --;
					dmgready = false;	
					combocounter++;
				
				}
			
			
			
			
			if (otherObject.tag == "ATM")
				{
					atmachine atmscript = (atmachine)otherObject.gameObject.GetComponent("atmachine");

					if (fakeman.dbzmode)
						fakeman.dbzmodetimer = 0;
					fakeman.dbzmode = true;
					Instantiate(smackanimu, 
					new Vector3(otherObject.transform.position.x + 45, otherObject.transform.position.y, -120), otherObject.transform.rotation);
					audio.PlayOneShot(smacksound);
					otherObject.audio.Play();
					atmscript.shake();
					atmscript.health -= playerscript.attackdmg;
					atmscript.ttg += 0.3f;
					atmscript.releasecoins();
					dmgready = false;	
					combocounter++;
				}
		
		
	}
}
	
	void juxtapose()
	{
		int roll = Random.Range(1, 101);
		
		if (roll < 10 && Checkclonesonscreen(5))
		{
			GameObject newman = (GameObject)Instantiate(anotherclone, transform.position, Quaternion.identity);
			karatemanmirror clonescript = (karatemanmirror)newman.GetComponent("karatemanmirror");
			clonescript.karatesprite.SetSprite(karatemanatlas, 83, true);
			juxtaposeready = false;
		}
		
		
	}
	
	bool Checkclonesonscreen(int max)
	{
		GameObject[] clonesonscreeen = GameObject.FindGameObjectsWithTag("Fake");
		
		if (clonesonscreeen.Length <= max)
			return true;
		
		else
		return false;
	}
	
}
