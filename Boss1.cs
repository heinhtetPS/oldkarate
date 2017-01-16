using UnityEngine;
using System.Collections;

public class Boss1 : MonoBehaviour {

	//stats
	public float healthmainred, healthmainyellow, healthminion1, healthminion2, Maxhealthmain, Maxhealthminion1, Maxhealthminion2;
	public bool maintargetted = false, leftmin = false, rightmin = false;
	private bool shockwave = false, gotshocknumber = false, MainlazerON = false, m1lazerON = false, m2lazerON = false,
	m1alive = true, m2alive = true, minionsalive = true;
	
	
	//timers and patterns shit
	private float shockwavetimer, shockwavedelay, shockwavetime, targettimer, Mainlazertimer, MainlazerCD,
	m1lazertimer, m2lazertimer, m1lazerCD, m2lazerCD, timetoshoot;
	public float gothitdelay, randomshoottimer;
	private bool doingapattern = false, mainguntime = false, gotshootrandom = false, panicpicked = false;
	private float t1timer, t2timer, m1recharge, m2recharge;
	
	//positions
	private Transform minion1default, minion2default;
	private Rect namelabel = new Rect(100, Screen.height - 80, 200, 100);
	private Rect healthbarframe = new Rect(1,1,1,1);
	private Vector3 gun1pos, gun2pos, m1gleft, m1gright, m2gleft, m2gright;
	
	
	//media
	public Texture2D healthframe, healthyellow, healthred;
	public ParticleSystem beam, tinybeam, shock, death, chargin, m1chargin, m2chargin;
	public Animation bossanim;
	public GameObject minion1, minion2, bulletgeneric;
	public SphereCollider shockbox;
	private Player playerscript;

	
	void Start () {
		
		minion1default = minion1.transform;
		minion2default = minion2.transform;
		gun1pos = new Vector3(transform.position.x -184,transform.position.y - 130, -12);
		gun2pos = new Vector3(transform.position.x + 184,transform.position.y - 130, -12);
		m1gleft = new Vector3(minion1.transform.position.x - 70, minion1.transform.position.y - 53, -12);
		m1gright = new Vector3(minion1.transform.position.x + 70,minion1.transform.position.y - 53, -12);
		m2gleft = new Vector3(minion2.transform.position.x - 70, minion2.transform.position.y - 53, -12);
		m2gright = new Vector3(minion2.transform.position.x + 70, minion2.transform.position.y - 53, -12);
		shockbox.enabled = false;
	
	}
	

	void Update () {
		
		gothitdelay += Time.deltaTime;
		
		if (healthminion1 <= 0)
			m1alive = false;
		if (healthminion2 <=0)
			m2alive = false;
		if (!m1alive || !m2alive)
			minionsalive = false;
		if (m1alive && m2alive)
			minionsalive = true;
		
		if (maintargetted || leftmin || rightmin)
		{
			targettimer += Time.deltaTime;	
			if (targettimer - Time.deltaTime > 4)
			{
				maintargetted = false;
				leftmin = false;
				rightmin = false;
				targettimer = 0;
			}
		}
		
		if (Input.GetKeyDown(KeyCode.Y))
			mswitchsides();
		if (Input.GetKeyDown(KeyCode.U))
			StartCoroutine( minionspin () );
		if (Input.GetKeyDown(KeyCode.I))
			M2movetoleft();
		if (Input.GetKeyDown(KeyCode.O))
			M1movetoright();
		if (Input.GetKeyDown(KeyCode.P))
			minionsdefault();
		if (Input.GetKeyDown(KeyCode.L))
			everyoneshoot();
		
		
		#region Cooldowns
		
		//Pattern1
		if (healthmainyellow > 0 && minionsalive)
		{
			t1timer += Time.deltaTime;
			if (t1timer > 5)
			{
				dot1pattern();		
				t1timer = 0;
			}
			
		}
		
		//Pattern2
		if (healthmainyellow <= 0 && minionsalive)
		{
			t2timer += Time.deltaTime;
			if (t2timer > 5)
			{
				dot2pattern();		
				t2timer = 0;
			}
		}
		
		//SHOCKWAVE----------------------------------------------------------------------------
		if (!shockwave)
		{
			shockwavedelay += Time.deltaTime;
			if (!gotshocknumber)
			Getshockrandom();
		}
		if (shockwavedelay - Time.deltaTime >= shockwavetime)
		{
			Shockwave();
			shockwavedelay = 0;	
			gotshocknumber = false;
		}

		if (shockwave)
		{
			shockwavetimer += Time.deltaTime;
			if (shockwavetimer - Time.deltaTime >= 5f)
			{
				shockwave = false;
				shock.Stop();
				shockbox.enabled = false;
				shockwavetimer = 0;
			}
		}
		
		//RANDOM SHOOTING-------------------------------------------------------
		
		if (!mainguntime)
		{
			randomshoottimer += Time.deltaTime;
			if (!gotshootrandom)
				Getshootrandom();
			if (randomshoottimer >= timetoshoot)
			{
				StartCoroutine ( shoot3times () );
				randomshoottimer = 0;
				gotshootrandom = false;
			}
			
		}

		//MAIN LAZER---------------------------------------------------------------------
		if (!MainlazerON)
		{
			MainlazerCD += Time.deltaTime;
			if (MainlazerCD - Time.deltaTime > 10)
			{
				MainLazer();
				MainlazerCD = 0;
			}
		}

		if (MainlazerON)
		{
			Mainlazertimer += Time.deltaTime;	
			if (Mainlazertimer - Time.deltaTime > 3)
			{
				MainLazerShoot();	
				Mainlazertimer = 0;
				MainlazerON = false;
			}
		}
		
		//MINION RECHARGE----------------------------------------------------
		if (!m1alive)
		{
			m1recharge += Time.deltaTime;	
			if (m1recharge - Time.deltaTime > 20)
			{
				m1alive = true;	
				minion1.renderer.enabled = true;
				healthminion1 = 10;
				m1recharge = 0;
			}	
		}
		
		if (!m2alive)
		{
			m2recharge += Time.deltaTime;	
			if (m2recharge - Time.deltaTime > 20)
			{
				m2alive = true;	
				minion2.renderer.enabled = true;
				healthminion2 = 10;
				m2recharge = 0;
			}	
		}

		//Minion lazers------------------------------------------------------------
		if (!m1lazerON)
		{
			m1lazerCD += Time.deltaTime;
			if (m1lazerCD - Time.deltaTime > 10)
			{
				M1lazer();
			}
		}
		if (!m2lazerON)
		{
			m2lazerCD += Time.deltaTime;
			if (m2lazerCD - Time.deltaTime > 10)
			{
				M2lazer();
			}
		}

		if (m1alive && m1lazerON)
		{
			m1lazertimer += Time.deltaTime;	
			if (m1lazertimer - Time.deltaTime > 2)
			{
				M1lazershoot();	
				m1lazertimer = 0;
				m1lazerON = false;
			}
			
		}
		if (m2alive && m2lazerON)
		{
			m2lazertimer += Time.deltaTime;	
			if (m2lazertimer - Time.deltaTime > 2)
			{
				M2lazershoot();	
				m2lazertimer = 0;
				m2lazerON = false;
			}
		}
		#endregion
		
		if (!m1alive)
		{
			minion1.renderer.material.color = Color.gray;	
			minion1.collider.enabled = false;
		}
		if (m1alive)
		{
			minion1.renderer.material.color = Color.white;	
			minion1.collider.enabled = true;
		}
		
		if (!m2alive)
		{
			minion2.renderer.material.color = Color.gray;	
			minion2.collider.enabled = false;
		}
		if (m2alive)
		{
			minion2.renderer.material.color = Color.white;	
			minion2.collider.enabled = true;
		}
		
		if (minionsalive)
		{
			collider.enabled = false;
			panicpicked = false;	
		}
		
		if (!m1alive && !m2alive && !panicpicked)
		{
			collider.enabled = true;
			pickpanic();
		}
	
	}//update END
	
	
	
	
	void Shockwave()
	{
		shock.transform.position = transform.position;
		shockwave = true;
		shockbox.enabled = true;
		shock.Play();
	}
	
	void Getshockrandom()
	{
		shockwavetime = Random.Range (5, 15);	
		gotshocknumber = true;
	}
	
	void Getshootrandom()
	{
		timetoshoot = Random.Range (3, 8);	
		gotshootrandom = true;
	}
	
	public void GetHit(int dmg)
	{
		if (gothitdelay > 0.1f)
		{
			maintargetted = true;
			rightmin = false;
			leftmin = false;
			TakeDmg(dmg);
			gothitdelay = 0;
		}
		
	}
	
	public void m1GetHit (int dmg)
	{
		if (gothitdelay > 0.1f)
		{
			leftmin = true;
			maintargetted = false;
			rightmin = false;
			m1TakeDmg(dmg);
			gothitdelay = 0;
		}
		
	}
	
	public void m2GetHit (int dmg)
	{
		if (gothitdelay > 0.1f)
		{
			rightmin = true;
			maintargetted = false;
			leftmin = false;
			m2TakeDmg(dmg);
			gothitdelay = 0;
		}
		
	}
	
	void TakeDmg(int dmg)
	{
		if (healthmainyellow > 0)
		healthmainyellow -= dmg;	
		if (healthmainyellow <= 0)
		healthmainred -= dmg;
		if (healthmainred <=0)
			Death();
		
	}
	
	void m1TakeDmg(int dmg)
	{
		healthminion1 -= dmg;
	}
	
	void m2TakeDmg(int dmg)
	{
		healthminion2 -= dmg;
		
	}
	
	void Death()
	{
		Instantiate(death, transform.position, transform.rotation);
		Destroy(this.gameObject);
	}
	
	//MAIN lazer-------------------------------------------
	
	void MainLazer()
	{
		chargin.Play();
		MainlazerON = true;

	}
	
	void MainLazerShoot()
	{
		Instantiate(beam, transform.position, Quaternion.Euler(new Vector3(90, 180, 90)));	
	}
	
	//minion lazers------------------------------------------------------------
	
	void M1lazer()
	{
		if (m1alive)
		{
			m1chargin.Play();
			m1lazerCD = 0;
			m1lazerON = true;
		}

	}
	
	void M1lazershoot()
	{
		Instantiate(tinybeam, minion1default.position, Quaternion.Euler(new Vector3(90, 180, 90)));	
	}
	
	void M2lazer()
	{
		if (m2alive)
		{
			m2chargin.Play();
			m2lazerCD = 0;
			m2lazerON = true;
		}

	}
	
	void M2lazershoot()
	{
		Instantiate(tinybeam, minion2default.position, Quaternion.Euler(new Vector3(90, 180, 90)));	
	}
	
	//Bullet shots
	
	void Bossmainshoot()
	{
		gun1pos = new Vector3(transform.position.x -184,transform.position.y - 130, -12);
		gun2pos = new Vector3(transform.position.x + 184,transform.position.y - 130, -12);
		
		GameObject left = (GameObject)Instantiate(bulletgeneric, gun1pos, transform.rotation);
		left.rigidbody.velocity = new Vector3 (-300, -300, 0); 
		
		GameObject right = (GameObject)Instantiate(bulletgeneric, gun2pos, transform.rotation);
		Bulletgeneric bulletscript = (Bulletgeneric)right.GetComponent("Bulletgeneric");
		bulletscript.otherside = true;
		right.rigidbody.velocity = new Vector3 (300, -300, 0);
		
	}
	
	IEnumerator shoot3times()
	{
		Bossmainshoot();
		
		yield return new WaitForSeconds(0.5f);
			
		Bossmainshoot();
		
		yield return new WaitForSeconds(0.5f);
			
		Bossmainshoot();
		
		
	}
	
	void m1gun ()
	{
		m1gleft = new Vector3(minion1.transform.position.x - 70, minion1.transform.position.y - 53, -12);
		m1gright = new Vector3(minion1.transform.position.x + 70,minion1.transform.position.y - 53, -12);
		
		GameObject left = (GameObject)Instantiate(bulletgeneric, m1gleft, transform.rotation);
		left.rigidbody.velocity = new Vector3 (-300, -300, 0); 
		
		GameObject right = (GameObject)Instantiate(bulletgeneric, m1gright, transform.rotation);
		Bulletgeneric bulletscript = (Bulletgeneric)right.GetComponent("Bulletgeneric");
		bulletscript.otherside = true;
		right.rigidbody.velocity = new Vector3 (300, -300, 0);
		
	}
	
	
	void m2gun ()
	{
		m2gleft = new Vector3(minion2.transform.position.x - 70, minion2.transform.position.y - 53, -12);
		m2gright = new Vector3(minion2.transform.position.x + 70, minion2.transform.position.y - 53, -12);
		
		GameObject left = (GameObject)Instantiate(bulletgeneric, m2gleft, transform.rotation);
		left.rigidbody.velocity = new Vector3 (-300, -300, 0); 
		
		GameObject right = (GameObject)Instantiate(bulletgeneric, m2gright, transform.rotation);
		Bulletgeneric bulletscript = (Bulletgeneric)right.GetComponent("Bulletgeneric");
		bulletscript.otherside = true;
		right.rigidbody.velocity = new Vector3 (300, -300, 0);
		
	}
	
	void minionsshoot()
	{
		StartCoroutine ( mshoots () );
	}
	
	IEnumerator mshoots()
	{
		m1gun();
		m2gun();
		
		yield return new WaitForSeconds(0.2f);
		
		m1gun();
		m2gun();
		
		yield return new WaitForSeconds(0.2f);
		
		m1gun();
		m2gun();
		
	}
	
	void everyoneshoot()
	{
		StartCoroutine ( allshoot () );
	}
	
	IEnumerator allshoot()
	{
		Bossmainshoot();	
		minionsshoot();
		
		yield return new WaitForSeconds(0.2f);
		
		Bossmainshoot();	
		minionsshoot();
		
		yield return new WaitForSeconds(0.2f);
		
		Bossmainshoot();	
		minionsshoot();
		
	}
	
	//SPECIFIC ANIMATION PATTERNS
	
	void mswitchsides()
	{
		bossanim.Play("boss1minions");		
	}
	
	void M1movetoright()
	{
		bossanim.Play("minion1do");
	}
	
	void M2movetoleft()
	{
		bossanim.Play("minion2do");
	}
	
	IEnumerator minionspin()
	{
		bossanim.Play("minionscome");
		
		yield return new WaitForSeconds(1);
		
		bossanim.Play("boss1mspin");
		
		
	}
	
	void minionsdefault()
	{
		bossanim.Play("minionsgo");	
	}
	
	void dot1pattern()
	{
		int pick = Random.Range(1,4);
		
		if (pick == 1)
			M1movetoright();
		
		if (pick == 2)
			M2movetoleft();
		
		if (pick == 3)
			mswitchsides();
		
	}
	
	
	void dot2pattern()
	{
		StartCoroutine ( minionspin() );
		
	}
	
	void pickpanic()
	{
		minion1.renderer.enabled = false;
		minion2.renderer.enabled = false;
		int pick = Random.Range(1,3);	
		
		if (pick == 1)
			bossanim.Play("boss1panic");
		
		if (pick == 2)
			bossanim.Play("boss1panic2");
		
		panicpicked = true;
	}
	
	void OnGUI ()
	{
//		GUI.Label(new Rect(0, 120, 200, 100), "DEV SHIT SONS");
//		GUI.TextArea(new Rect(0, 80, 140, 200), " ");
		GUI.color = Color.black;
		GUI.Label(new Rect(0, 160, 200, 100), "t2timer: " + t2timer.ToString());
		GUI.Label(new Rect(0, 180, 200, 100), "minionsaliv == " + minionsalive.ToString());
//		GUI.Label(new Rect(0, 200, 200, 100), "thebool: " + shockwave.ToString());
//		GUI.Label(new Rect(0, 220, 200, 100), "shockwavetimer: " + shockwavetimer.ToString());
//		GUI.Label(new Rect(0, 240, 200, 100), "airtime == " + additionalairtime.ToString());
//		GUI.Label(new Rect(0, 260, 200, 100), "dbzmode == " + dbzmode.ToString());
		
		
		
		
		//healthbars------------------------------------------------------------------
		if (maintargetted)
		{
			GUI.Label(namelabel, "Boss Name"); 
//			GUI.DrawTexture(new Rect(0, 
//				8f, 384, 50), healthframe, ScaleMode.ScaleToFit, true, 0f);
			GUI.DrawTexture(new Rect(100, Screen.height - 50,
				900 * (healthmainred / Maxhealthmain), 14), healthred, ScaleMode.StretchToFill, true, 10.0f);
			GUI.DrawTexture(new Rect(100, Screen.height - 50,
				900 * (healthmainyellow / Maxhealthmain), 14), healthyellow, ScaleMode.StretchToFill, true, 10.0f);
		}
		
		if (leftmin)
		{
			GUI.Label(namelabel, "Left Orb"); 
//			GUI.DrawTexture(new Rect(0, 
//				8f, 384, 50), healthframe, ScaleMode.ScaleToFit, true, 0f);
			GUI.DrawTexture(new Rect(100, Screen.height - 50,
				900 * (healthminion1 / Maxhealthminion1), 14), healthred, ScaleMode.StretchToFill, true, 10.0f);
		}
		
		if (rightmin)
		{
			GUI.Label(namelabel, "Right Orb"); 
//			GUI.DrawTexture(new Rect(0, 
//				8f, 384, 50), healthframe, ScaleMode.ScaleToFit, true, 0f);
			GUI.DrawTexture(new Rect(100, Screen.height - 50,
				900 * (healthminion2 / Maxhealthminion2), 14), healthred, ScaleMode.StretchToFill, true, 10.0f);
			
		}
	
		
	}
}
