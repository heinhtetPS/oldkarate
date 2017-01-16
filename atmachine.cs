using UnityEngine;
using System.Collections;

public class atmachine : MonoBehaviour {

	private bool shaking = false, flickeron = false;
	public bool hitbydkick = false, hitbyexplo = false;
	private float dmgreset;
	public float health = 12;
	
	public exSprite atmsprite;
	public exSpriteAnimation atmanimu;
	
	private float xoffset;
	private bool alreadylanded = false;
	
	public float ttg = 12;
	floorshake floorscript;
	
	public AudioClip attention;
	
	public GameObject coin, explosion, gbreak, redscroll, greenscroll, bluescroll;
	private Backgroundgeneral bgscript;
	
	void Start () {
		
//		bgscript = (Backgroundgeneral)GameObject.FindGameObjectWithTag("Backg").GetComponent("Backgroundgeneral");
		
	
	}
	
	// Update is called once per frame
	void Update () {
		
		ttg -= Time.deltaTime;
		
		//coming down (different on different maps) 
		fallingscript();
		mapspecificrestrictions();
		
		transform.position = new Vector3 (transform.position.x + xoffset, transform.position.y, -90);
		
		if (shaking)
		{
			float duration = 0.05f;
   			float lerp = Mathf.PingPong (Time.time, duration) / duration;
 			xoffset = Mathf.Lerp(-3, 3, lerp);
		}
		
		if (health <= 0)
		{
			Coinexplosion();	
		}
		
		if (ttg <= 5)
		{
			if (!flickeron)	
			StartCoroutine( flicker() );
		}
		
		if (ttg <= 0)
			Destroy(this.gameObject);
		
		
		//reset
		if (hitbydkick || hitbyexplo)
		{
			dmgreset += Time.deltaTime;
			if (dmgreset >= 0.3f)
			{
				hitbydkick = false;	
				hitbyexplo = false;
				dmgreset = 0;
			}
			
		}
	
	}
	
	IEnumerator Toggleshake()
	{
		shaking = true;
		atmanimu.Play("atmcash");
		
		yield return new WaitForSeconds(0.2f);
		
		shaking = false;
		xoffset = 0;
		
	}
	
	public void releasecoins()
	{
		int randomizer = Random.Range(1,5);
		int morerandom = Random.Range(1, 10);
		int evenmore = Random.Range(1,4);
		
		if (randomizer == 1)
		{
			Instantiate(coin, new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
		}
		if (randomizer == 2)
		{
			Instantiate(coin, new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
			Instantiate(coin, new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
			Instantiate(coin, new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
		}
		if (randomizer == 3)
		{
			Instantiate(coin, new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
			Instantiate(coin, new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
			Instantiate(coin, new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
			Instantiate(coin, new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
			if (morerandom == 5)
			{
				if (evenmore == 1)
					Instantiate(redscroll, new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation); 
				if (evenmore == 2)
					Instantiate(greenscroll, new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
				if (evenmore == 3)
					Instantiate(bluescroll, new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
			}
		}
		
		if (randomizer == 4)
		{
			Instantiate(coin, new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
			Instantiate(coin, new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
			Instantiate(coin, new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
			Instantiate(coin, new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
			
		}
		
		
		
	}
	
	public void Coinexplosion()
	{
		for (int i = 0; i < 50; i++)
		{
			Instantiate(coin, new Vector3(transform.position.x, transform.position.y + 50, -10), transform.rotation);
			
		}
		Instantiate(explosion, transform.position, transform.rotation);
		Destroy(this.gameObject);
	}
	
	public void shake()
	{
		StartCoroutine( Toggleshake () );	
	}
	
	void Bouncebg()
	{
		GameObject[] backgstuff = GameObject.FindGameObjectsWithTag("bgbump");
		
		foreach (GameObject bb in backgstuff)
		{
			bb.rigidbody.velocity = new Vector3 (0, 500, 0);	
			
		}
		
		if (GameObject.FindGameObjectWithTag("Floor") != null)
		{
			floorscript = (floorshake)GameObject.FindGameObjectWithTag("Floor").GetComponent("floorshake");
			floorscript.shaking = true;
		}
	}
	
	void fallingscript()
	{
		if (PlayerPrefs.GetInt("Currentlevel") == 1)
		{
			if (transform.position.y > -200)
			transform.Translate(new Vector3(0, -600, 0) * Time.deltaTime);
			
			if (transform.position.y <= -200 && !alreadylanded)
			{
				audio.PlayOneShot(attention);
				Instantiate(gbreak, new Vector3( transform.position.x, transform.position.y -50, -90), Quaternion.identity);
				Bouncebg();
				atmanimu.Play("atm1");
				alreadylanded = true;
			}
			
		}
		
		if (PlayerPrefs.GetInt("Currentlevel") == 2)
		{
			if (transform.position.y > -225)
			transform.Translate(new Vector3(0, -600, 0) * Time.deltaTime);
			
			if (transform.position.y <= -225 && !alreadylanded)
			{
				audio.PlayOneShot(attention);
				Instantiate(gbreak, new Vector3( transform.position.x, transform.position.y -50, -90), Quaternion.identity);
				Bouncebg();
				atmanimu.Play("atm1");
				alreadylanded = true;
			}
		}
		
		if (PlayerPrefs.GetInt("Currentlevel") == 3)
		{
			if (transform.position.y > -200)
			transform.Translate(new Vector3(0, -600, 0) * Time.deltaTime);
			
			if (transform.position.y <= -200 && !alreadylanded)
			{
				audio.PlayOneShot(attention);
				Instantiate(gbreak, new Vector3( transform.position.x, transform.position.y -50, -90), Quaternion.identity);
				Bouncebg();
				atmanimu.Play("atm1");
				alreadylanded = true;
			}
		}
		
		if (PlayerPrefs.GetInt("Currentlevel") == 4)
		{
			if (transform.position.y > -150)
			transform.Translate(new Vector3(0, -600, 0) * Time.deltaTime);
			
			if (transform.position.y <= -150 && !alreadylanded)
			{
				audio.PlayOneShot(attention);
				Instantiate(gbreak, new Vector3( transform.position.x, transform.position.y -50, -90), Quaternion.identity);
				Bouncebg();
				atmanimu.Play("atm1");
				alreadylanded = true;
			}
		}
		
		if (Application.loadedLevelName == "Endless" || Application.loadedLevelName == "devmode")
		{
			if (transform.position.y > -240)
			transform.Translate(new Vector3(0, -600, 0) * Time.deltaTime);
			
			if (transform.position.y <= -240 && !alreadylanded)
			{
				audio.PlayOneShot(attention);
				Instantiate(gbreak, new Vector3( transform.position.x, transform.position.y -50, -90), Quaternion.identity);
				Bouncebg();
				atmanimu.Play("atm1");
				alreadylanded = true;
			}
		}
		
		
		
	}
	
	void mapspecificrestrictions()
	{
		if (PlayerPrefs.GetInt("Currentlevel") == 4)
		{
			if (transform.position.x != 295)
			transform.position = new Vector3(295, transform.position.y, transform.position.z);
			
		}
		
		if (Application.loadedLevelName == "Endless" || Application.loadedLevelName == "devmode")
		{
			if (transform.position.x > 400)
			transform.position = new Vector3(400, transform.position.y, transform.position.z);
			
			if (transform.position.x < -430)
			transform.position = new Vector3(-430, transform.position.y, transform.position.z);
			
		}
	}
	
	IEnumerator flicker()
	{
		flickeron = true;
		
		while (ttg <= 5)
		{
			renderer.enabled = !renderer.enabled;	
			
			yield return new WaitForSeconds(0.1f);
		}
		
		
	}
	
//	void OnGUI()
//	{
//		GUI.color = Color.black;
//		GUI.Label(new Rect(0, 160, 200, 100), "ttg == " + ttg.ToString());	
//		
//	}
}
