using UnityEngine;
using System.Collections;

public class beam2body : MonoBehaviour {
	
	public float offsetter, stretcher, boxoffsetter, boxstretcher;
	public BoxCollider collider1;
	
	public exSprite beam;
	
	public Karateoboxnew obox;
	public bool blackt2 = false, whitet2 = false;
	private bool alreadydid = false;
	public float beamdmgdelay;
	
	void Start () {
	
		obox = (Karateoboxnew)GameObject.FindGameObjectWithTag("Offense").GetComponent("Karateoboxnew");
		
		if (PlayerPrefs.GetString("LazerT2") == "black")
			blackt2 = true;
		if (PlayerPrefs.GetString("LazerT2") == "white")
			whitet2 = true;
	}
	
	// Update is called once per frame
	void Update () {
		
		beamdmgdelay += Time.deltaTime;
		offsetter += 0.5f;
		stretcher += 0.001f;
		boxoffsetter += 1;
		boxstretcher += 0.3f;
		
		if (beam.offset.x <= 1640)
		beam.offset = new Vector2 (beam.offset.x + offsetter, beam.offset.y);
		if (beam.scale.x < 3.2f)
		beam.scale = new Vector2 (beam.scale.x + stretcher, beam.scale.y);
		if (collider1.center.x <= 700)
		collider1.center = new Vector3 (collider1.center.x + boxoffsetter, collider1.center.y, collider1.center.z);
		if (collider1.size.x <= 1400)
		collider1.size = new Vector3 (collider1.size.x + boxstretcher, collider1.size.y, collider1.size.z);
		
	}
	
	void OnTriggerEnter (Collider otherObject) 
	{
		if (whitet2 && otherObject.tag == "Player" && !alreadydid)
		{
			Player playerscript = (Player)otherObject.gameObject.GetComponent("Player");
			playerscript.Healthup(5);
			alreadydid = true;
		}
		
		if (otherObject.tag == "Bullet")
			{
				Destroy(otherObject.gameObject);
			}
		
		if (otherObject.tag == "Enemy")
			{
				Punk1 enemyscript = (Punk1)otherObject.gameObject.GetComponent("Punk1");
				enemyscript.FlyAway();
				if (obox.comboing)
				{
					obox.combocounter++;
					obox.combotimer = 0f;
				}
			}
		
		if (otherObject.tag == "Enemythrower")
			{
				Punkthrower enemyscript = (Punkthrower)otherObject.gameObject.GetComponent("Punkthrower");
				enemyscript.FlyAway();
				Debug.Log("eef");
				if (obox.comboing)
				{
					obox.combocounter++;
					obox.combotimer = 0f;
				}
			}
		if (otherObject.tag == "Enemy2")
			{
				Punk2 enemy2script = (Punk2)otherObject.gameObject.GetComponent("Punk2");
				if (beamdmgdelay > 0)
				{
					enemy2script.dbzmode = true;
					enemy2script.health-=5;
					beamdmgdelay = 0;
					if (obox.comboing)
				{
					obox.combocounter++;
					obox.combotimer = 0f;
				}
				
				}
			}
		if (otherObject.tag == "Enemy3")
			{
				Punk3 enemy3script = (Punk3)otherObject.gameObject.GetComponent("Punk3");
				if (beamdmgdelay > 0)
				{
					enemy3script.dbzmode = true;
					enemy3script.health-=5;
					beamdmgdelay = 0;
					if (obox.comboing)
				{
					obox.combocounter++;
					obox.combotimer = 0f;
				}
				}
			}
		
			if (otherObject.tag == "Ninja1")
			{
				Ninja1 nin1script = (Ninja1)otherObject.gameObject.GetComponent("Ninja1");
				if (beamdmgdelay > 0)
				{
					nin1script.dbzmode = true;
					nin1script.health-=3;
					beamdmgdelay = 0;
					if (obox.comboing)
				{
					obox.combocounter++;
					obox.combotimer = 0f;
				}
				}
				alreadydid = true;
			}
		
			if (otherObject.tag == "Ninja2")
			{
				Ninja2 nin2script = (Ninja2)otherObject.gameObject.GetComponent("Ninja2");
				if (beamdmgdelay > 0)
				{
					nin2script.dbzmode = true;
					nin2script.health-=3;
					beamdmgdelay = 0;
					if (obox.comboing)
				{
					obox.combocounter++;
					obox.combotimer = 0f;
				}
				}
				alreadydid = true;
			}
		
			if (otherObject.tag == "ATM")
			{
				atmachine atmscript = (atmachine)otherObject.gameObject.GetComponent("atmachine");
				if (beamdmgdelay > 0)
				{
					otherObject.audio.Play();
					atmscript.shake();
					if (atmscript.health <= 1)
						obox.Makelines();
					atmscript.health -= 1;
					atmscript.ttg += 0.2f;
					atmscript.releasecoins();
					beamdmgdelay = 0;
					if (obox.comboing)
				{
					obox.combocounter++;
					obox.combotimer = 0f;
				}
				}
				alreadydid = true;
			}
			if (otherObject.tag == "Hardcore")
			{
				EnemyWrestler wrestscript = (EnemyWrestler)otherObject.gameObject.GetComponent("EnemyWrestler");
				wrestscript.FlyAway();
					if (obox.comboing)
				{
					obox.combocounter++;
					obox.combotimer = 0f;
				}
			}
		
			if (otherObject.tag == "Hardcore2")
			{
				EnemyWrestler2 wrest2script = (EnemyWrestler2)otherObject.gameObject.GetComponent("EnemyWrestler2");
				if (beamdmgdelay > 0f)
				{
					wrest2script.FlyAway();
					if (obox.comboing)
				{
					obox.combocounter++;
					obox.combotimer = 0f;
				}
					beamdmgdelay = 0;
				}
			}
		
			if (otherObject.tag == "Hardcore3")
			{
				EnemyWrestler3 wrest3script = (EnemyWrestler3)otherObject.gameObject.GetComponent("EnemyWrestler3");
				if (beamdmgdelay > 0f)
				{
					wrest3script.FlyAway();
					if (obox.comboing)
				{
					obox.combocounter++;
					obox.combotimer = 0f;
				}
					beamdmgdelay = 0;
				}
			}
		
			if (otherObject.tag == "Ground")
					{
						EnemyGround gscript = (EnemyGround)otherObject.gameObject.GetComponent("EnemyGround");
						gscript.Die();
						if (obox.comboing)
						{
							obox.combocounter++;
							obox.combotimer = 0f;
						}
					}
		
			if (otherObject.tag == "Ground2")
					{
						EnemyGround3 gscript = (EnemyGround3)otherObject.gameObject.GetComponent("EnemyGround3");
						gscript.Die();
						if (obox.comboing)
						{
							obox.combocounter++;
							obox.combotimer = 0f;
						}
					}
		
			if (otherObject.tag == "Ground3")
					{
						EnemyGround3 gscript = (EnemyGround3)otherObject.gameObject.GetComponent("EnemyGround3");
						gscript.Die();
						if (obox.comboing)
						{
							obox.combocounter++;
							obox.combotimer = 0f;
						}
					}
		
			if (otherObject.tag == "Boss")
					{
						Boss1 boss1script = (Boss1)otherObject.gameObject.GetComponent("Boss1");
						boss1script.GetHit(5);
						if (obox.comboing)
						{
							obox.combocounter++;
							obox.combotimer = 0f;
						}
					}
		
			if (otherObject.tag == "Minion")
					{
						Boss1 boss1script = (Boss1)otherObject.transform.parent.gameObject.GetComponent("Boss1");
						boss1script.m1GetHit(5);
						if (obox.comboing)
						{
							obox.combocounter++;
							obox.combotimer = 0f;
						}
					}
		
			if (otherObject.tag == "Minion2")
					{
						Boss1 boss1script = (Boss1)otherObject.transform.parent.gameObject.GetComponent("Boss1");
						boss1script.m2GetHit(5);
						if (obox.comboing)
						{
							obox.combocounter++;
							obox.combotimer = 0f;
						}
					}
		
			if (PlayerPrefs.GetString("SpiritbombT1") == "white" || PlayerPrefs.GetString("SpiritbombT2") == "white")
			{
				if (otherObject.tag == "Sbomb")
				{
					Debug.Log("What");
					SpiritBomb sbscript = (SpiritBomb)otherObject.gameObject.GetComponent("SpiritBomb");	
						sbscript.TriggerExplosion();
				}
			
			}
	}
}
