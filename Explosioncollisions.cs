using UnityEngine;
using System.Collections;

public class Explosioncollisions : MonoBehaviour {
	
	private float explosiondelay;
	public Karateoboxnew obox;
	public Player playerscript;
	
	private bool alreadyhealed = false;
	
	private bool alreadycomboed = false;
	
	//if it came from sbomb
	public bool Sbomb = false, t2black = false, t1white = false, t2white = false;
	
	

	void Start () {
		
		obox = (Karateoboxnew)GameObject.FindGameObjectWithTag("Offense").GetComponent("Karateoboxnew");
		playerscript = (Player)GameObject.FindGameObjectWithTag("Player").GetComponent("Player");
		
		if (Sbomb)
		{
			if (PlayerPrefs.GetString("SpiritbombT1") == "white")
				t1white = true;
			if (PlayerPrefs.GetString("SpiritbombT2") == "white")
				t2white = true;
			if (PlayerPrefs.GetString("SpiritbombT2") == "black")
				t2black = true;
		}
	
	}
	

	void Update () {
		
		explosiondelay += Time.deltaTime;
	
	}
	
	void OnTriggerStay (Collider otherObject) {
		
		if (explosiondelay >= 0.1f)
		{
			if (otherObject.tag == "Enemy")
			{
				Punk1 enemyscript = (Punk1)otherObject.gameObject.GetComponent("Punk1");
				if (!enemyscript.gotdmgbybomb)
				{
					if (!t2black)
						{
							enemyscript.FlyAway();
						}
						if (t2black)
						{
							enemyscript.health -= 4;
							otherObject.gameObject.rigidbody.velocity = Getdiff(otherObject.transform.position) * 0.6f	;
							if (otherObject.gameObject.transform.position.x - transform.position.x > 50)
							otherObject.rigidbody.velocity = Vector3.zero;
							enemyscript.gravitypulled = true;
							enemyscript.dbzmode = true;
						}
	
					if (obox.comboing && !alreadycomboed)
					{
						obox.combocounter++;
						obox.combotimer = 0f;
						enemyscript.comboedcount++;
						enemyscript.Bleedcash();
						alreadycomboed = true;
					}
					enemyscript.gotdmgbybomb = true;
				}
				
			}
			if (otherObject.tag == "Enemythrower")
			{
				Punkthrower enemyscript = (Punkthrower)otherObject.gameObject.GetComponent("Punkthrower");
				if (!enemyscript.gotdmgbybomb)
				{
					if (!t2black)
						{
							enemyscript.FlyAway();
						}
						if (t2black)
						{
							enemyscript.health -= 4;
							otherObject.gameObject.rigidbody.velocity = Getdiff(otherObject.transform.position) * 0.6f	;
							if (otherObject.gameObject.transform.position.x - transform.position.x > 50)
							otherObject.rigidbody.velocity = Vector3.zero;
							enemyscript.gravitypulled = true;
							enemyscript.dbzmode = true;
						}
	
					if (obox.comboing && !alreadycomboed)
					{
						obox.combocounter++;
						obox.combotimer = 0f;
						enemyscript.comboedcount++;
						enemyscript.Bleedcash();
						alreadycomboed = true;
					}
					enemyscript.gotdmgbybomb = true;
				}
				
			}
			if (otherObject.tag == "Enemy2")
			{
				Punk2 enemy2script = (Punk2)otherObject.gameObject.GetComponent("Punk2");
				if (!enemy2script.gotdmgbybomb)
				{

					if (!t2black)
					{
						if (enemy2script.health <= 4)
							obox.Makelines();
						enemy2script.health -= 4;
						enemy2script.dbzmode = true;
					}
					if (t2black)
					{
						enemy2script.health -= 4;	
						
						otherObject.gameObject.rigidbody.velocity = Getdiff(otherObject.transform.position) * 0.4f;
						enemy2script.gravitypulled = true;
						enemy2script.dbzmode = true;
					}
					if (obox.comboing && !alreadycomboed)
					{
						obox.combocounter++;
						obox.combotimer = 0f;
						enemy2script.comboedcount++;
						enemy2script.comboedreset = 0;
						enemy2script.Bleedcash();
						alreadycomboed = true;
						
					}
					enemy2script.gotdmgbybomb = true;
				}

			}
			if (otherObject.tag == "Enemy3")
			{
				Punk3 enemy3script = (Punk3)otherObject.gameObject.GetComponent("Punk3");
				if (!enemy3script.gotdmgbybomb)
				{

					if (!t2black)
					{
						if (enemy3script.health <= 4)
							obox.Makelines();
						enemy3script.health -= 4;	
						enemy3script.dbzmode = true;
					}
					if (t2black)
					{
						enemy3script.health -= 4;	
						otherObject.gameObject.rigidbody.velocity = Getdiff(otherObject.transform.position) * 0.3f;
						enemy3script.gravitypulled = true;
						enemy3script.dbzmode = true;
					}
					if (obox.comboing && !alreadycomboed)
					{
						obox.combocounter++;
						obox.combotimer = 0f;
						enemy3script.comboedcount++;
						enemy3script.comboedreset = 0;
						enemy3script.Bleedcash();
						alreadycomboed = true;
					}
					enemy3script.gotdmgbybomb = true;
				}

			}
			
			if (otherObject.tag == "Ninja1")
			{
				Ninja1 nin1script = (Ninja1)otherObject.gameObject.GetComponent("Ninja1");
				nin1script.FlyAway();
				
				if (obox.comboing && !alreadycomboed)
				{
					obox.combocounter++;
					obox.combotimer = 0f;
					alreadycomboed = true;
				}
			}
			
			if (otherObject.tag == "Ninja2")
			{
				Ninja2 nin2script = (Ninja2)otherObject.gameObject.GetComponent("Ninja2");
				if (!nin2script.gotdmgbybomb)
				{
					if (!t2black)
					{
						nin2script.health-=4;
						nin2script.dbzmode = true;
					}
					if (t2black)
					{
						nin2script.health-= 4;	
						otherObject.gameObject.rigidbody.velocity = Getdiff(otherObject.transform.position) * 2;
						nin2script.dbzmode = true;
					}
				
					if (obox.comboing && !alreadycomboed)
					{
						obox.combocounter++;
						obox.combotimer = 0f;
						alreadycomboed = true;
					}
					nin2script.gotdmgbybomb = true;
				}
			}
			
			if (otherObject.tag == "Hardcore")
			{
				EnemyWrestler wrestscript = (EnemyWrestler)otherObject.gameObject.GetComponent("EnemyWrestler");
				wrestscript.FlyAway();
					if (obox.comboing && !alreadycomboed)
				{
					obox.combocounter++;
					obox.combotimer = 0f;
					alreadycomboed = true;
				}
			}
			
			if (otherObject.tag == "Hardcore2")
			{
				EnemyWrestler2 wrest2script = (EnemyWrestler2)otherObject.gameObject.GetComponent("EnemyWrestler2");
				if (!wrest2script.gotdmgbybomb)
				{
					if (!t2black)
					{
						wrest2script.health-=4;
					}
					if (t2black)
					{
						wrest2script.health-= 4;	
						wrest2script.dbzmode = true;
						wrest2script.gravitypulled = true;
						otherObject.gameObject.rigidbody.velocity = Getdiff(otherObject.transform.position);
					}
				
					if (obox.comboing && !alreadycomboed)
					{
						obox.combocounter++;
						obox.combotimer = 0f;
						alreadycomboed = true;
					}
					wrest2script.gotdmgbybomb = true;
				}
			}
			
			if (otherObject.tag == "Hardcore3")
			{
				EnemyWrestler3 wrest3script = (EnemyWrestler3)otherObject.gameObject.GetComponent("EnemyWrestler3");
				if (!wrest3script.gotdmgbybomb)
				{
					if (!t2black)
					{
						wrest3script.health-=4;
					}
					if (t2black)
					{
						wrest3script.health-= 4;	
						wrest3script.dbzmode = true;
						wrest3script.gravitypulled = true;
						otherObject.gameObject.rigidbody.velocity = Getdiff(otherObject.transform.position);
					}
				
					if (obox.comboing && !alreadycomboed)
					{
						obox.combocounter++;
						obox.combotimer = 0f;
						alreadycomboed = true;
					}
					wrest3script.gotdmgbybomb = true;
				}
			}
			
			if (otherObject.tag == "Ground")
			{
				EnemyGround enemyscript = (EnemyGround)otherObject.gameObject.GetComponent("EnemyGround");
				
				enemyscript.Die();
				
			}
			
			if (otherObject.tag == "Ground3" || otherObject.tag == "Ground2")
			{
				EnemyGround3 enemyscript = (EnemyGround3)otherObject.gameObject.GetComponent("EnemyGround3");
				
				enemyscript.Die();
				
			}
			
			//redo these sometime later
			if (otherObject.tag == "Boss")
			{
				Boss1 bossscript = (Boss1)otherObject.gameObject.GetComponent("Boss1");
				bossscript.GetHit(4);

					if (obox.comboing && !alreadycomboed)
				{
					obox.combocounter++;
					obox.combotimer = 0f;
					alreadycomboed = true;
				}

			}
			
			if (otherObject.tag == "Minion")
			{
				Boss1 bossscript = (Boss1)otherObject.transform.parent.gameObject.GetComponent("Boss1");
				bossscript.m1GetHit(4);

					if (obox.comboing && !alreadycomboed)
				{
					obox.combocounter++;
					obox.combotimer = 0f;
					alreadycomboed = true;
				}

			}
			
			if (otherObject.tag == "Minion2")
			{
				Boss1 bossscript = (Boss1)otherObject.transform.parent.gameObject.GetComponent("Boss1");
				bossscript.m2GetHit(4);

					if (obox.comboing && !alreadycomboed)
				{
					obox.combocounter++;
					obox.combotimer = 0f;
					alreadycomboed = true;
				}

			}
			if (otherObject.tag == "Creep")
			{
				creeper creepscript = (creeper)otherObject.gameObject.GetComponent("creeper");
				creepscript.stoproutine(2);
				if (obox.comboing && !alreadycomboed)
				{
					obox.combocounter++;
					obox.combotimer = 0f;
					alreadycomboed = true;
				}
			}
			
			if (Sbomb)
			{
				if (otherObject.tag == "Player" && t1white && !alreadyhealed)
				{
					Player playerscript = (Player)otherObject.gameObject.GetComponent("Player");
					playerscript.Healthup(5);	
					alreadyhealed = true;
				}
				
			}
			
			if (otherObject.tag == "ATM")
			{
				atmachine atmscript = (atmachine)otherObject.gameObject.GetComponent("atmachine");
				if (!atmscript.hitbyexplo)
				{
					
					otherObject.audio.Play();
					atmscript.shake();
					if (atmscript.health <= playerscript.heavydmg)
						obox.Makelines();
					atmscript.health -= playerscript.heavydmg;
					atmscript.ttg += 0.5f;
					atmscript.releasecoins();
					
					
					if (obox.comboing && !alreadycomboed)
					{
						obox.combocounter++;
						obox.combotimer = 0f;
						alreadycomboed = true;
						
					}
					atmscript.hitbyexplo = true;
				}

			}
			
			
			
			
		}
	}
	
		public Vector3 Getdiff(Vector3 enemyposition)
	{
		Vector3 diff = transform.position - enemyposition; 
		return diff;
	}
}
