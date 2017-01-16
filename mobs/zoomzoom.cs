using UnityEngine;
using System.Collections;

public class zoomzoom : MonoBehaviour {
	
	public GameObject Karateman;
	Player playerscript;
	public exSprite zz;
	public exSpriteAnimation zzanim;
	public bool isActive = true, alreadyexploded = false;
	float recovery = 0;
	
	public float health = 2, maxhealth = 2;
	public float fuse = 0, speed = 0.5f;
	
	public bool slowed = false;
	private float slowtimer = 0;
	
	private bool facingright = true;
	
	public bool taunted = false, tauntable = true;
	private float taunttimer, tauntCD;
	
	public GameObject explosion;
	public SphereCollider exploradius;
	public AudioClip boom;
	
	// Use this for initialization
	void Start () {
		
		Karateman = GameObject.FindGameObjectWithTag("Player");
		playerscript = (Player)Karateman.GetComponent("Player");
		
	}
	
	void FixedUpdate () {
		
		if (isActive)
		{
			if (!taunted)
			{
				if (!slowed)
				rigidbody.velocity = Getdiff () * speed;
				
				if (slowed)
				rigidbody.velocity = Getdiff () * speed / 2;
				
			}
			
			if (taunted && GameObject.FindGameObjectWithTag("Fake") != null)
			{
				GameObject Fakeman = GameObject.FindGameObjectWithTag("Fake");
				if (!slowed)
				rigidbody.velocity = Getdiff ("no", Fakeman) * speed;
				
				if (slowed)
				rigidbody.velocity = Getdiff ("no", Fakeman) * speed / 2;
				
			}
			
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (isActive)
		{
		 	Getangle();
			alwayslookatafro();
			fuse += Time.deltaTime;
			
			//taunt
			if (GameObject.FindGameObjectWithTag("Fake") != null && tauntable)
				taunted = true;
			if (GameObject.FindGameObjectWithTag("Fake") == null && taunted)
			{
				taunted = false;
				taunttimer = 0;
				tauntCD = 0;
			}
		}
		
		if (!isActive)
		{
			transform.Rotate(new Vector3(0,0,-1) * 12);
			recovery += Time.deltaTime;
			if (recovery > 4)
			{
				if (health <= 0)
					explode();
				isActive = true;	
				recovery = 0;
				taunted = false;
				taunttimer = 0;
				tauntCD = 0;
			}
		}
		
		if (taunted && isActive)
			{
				zz.color = Color.magenta;
				taunttimer += Time.deltaTime;
				if (taunttimer > 3)
				{
					taunted = false;
					taunttimer = 0;
					tauntable = false;
					zz.color = Color.white;
				}
			}
			
			if (!tauntable)
			{
				tauntCD += Time.deltaTime;
				if (tauntCD > 6)
				{
					tauntable = true;
					tauntCD = 0;
				}	
			}
		
		if (slowed)
		{
			slowtimer += Time.deltaTime;
			zz.color = Color.yellow;
			
			if (slowtimer > 7)
			{
				slowed = false;
				slowtimer = 0;
				zz.color = Color.white;
			}
			
		}
		
		if (transform.position.y <= -325 || fuse >= 10)
			explode();
		
		if (fuse >= 5)
		{
			speed = 1;
			zzanim.Play("zzfaster");
		}
		if (fuse >= 8)
		{
			speed = 2;
			zzanim.Play("zzhorror");	
		}
		
		if (transform.position.z != -100)
				transform.position = new Vector3(transform.position.x, transform.position.y, -100);
	
	}
	
	void Getangle()
	{
		if (taunted)
		{
			GameObject Fakeman = GameObject.FindGameObjectWithTag("Fake");
			float diffX = Fakeman.transform.position.x - transform.position.x;
			float diffY = Fakeman.transform.position.y - transform.position.y;
		
			float angle = Mathf.Atan2(diffY, diffX) * Mathf.Rad2Deg;
			
			if (facingright)
			transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));	
			
			if (!facingright)
			transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 180));	
			
		}
		
		if (!taunted)
		{
			float diffX = Karateman.transform.position.x - transform.position.x;
			float diffY = Karateman.transform.position.y - transform.position.y;
		
			float angle = Mathf.Atan2(diffY, diffX) * Mathf.Rad2Deg;
			
			if (facingright)
			transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));	
			
			if (!facingright)
			transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 180));	
			
		}
	}
	
	Vector3 Getdiff()
	{
		Vector3 diff = Vector3.zero;
		if (Karateman.transform.position.x > transform.position.x)
		diff = new Vector3 (Karateman.transform.position.x + 100, Karateman.transform.position.y, Karateman.transform.position.z) - transform.position; 
		if (Karateman.transform.position.x <= transform.position.x)
		diff = new Vector3 (Karateman.transform.position.x - 100, Karateman.transform.position.y, Karateman.transform.position.z) - transform.position; 
		
		return diff;
	}
	
	public Vector3 Getdiff(string yesorno, GameObject target)
	{
		Vector3 diff = Vector3.zero;
		if (target.transform.position.x > transform.position.x)
		diff = new Vector3 (target.transform.position.x + 100, target.transform.position.y, target.transform.position.z) - transform.position; 
		if (Karateman.transform.position.x <= transform.position.x)
		diff = new Vector3 (target.transform.position.x - 100, target.transform.position.y, target.transform.position.z) - transform.position; 
		
		if (yesorno == "yes")
			diff = Vector3.Normalize(diff);
		
		return diff;
	}
	
	public void flyaway()
	{
		isActive = false;
		rigidbody.velocity = Getdiff() * -1;
	}
	
	public void Takedamage(float amount)
	{
		health -= amount; 	
		
	}
	
	IEnumerator exploding()
	{
		Instantiate(explosion, transform.position, transform.rotation);
		exploradius.enabled = true;
		renderer.enabled = false;
		audio.PlayOneShot(boom);
		alreadyexploded = true;
		
		yield return new WaitForSeconds(0.5f);
		
		Destroy(this.gameObject);
	}
	
	void alwayslookatafro()
	{
		if (taunted)
		{
			GameObject Fakeman = GameObject.FindGameObjectWithTag("Fake");
			if (transform.position.x < Fakeman.transform.position.x && !facingright)
			{
				zz.HFlip();
				facingright = true;	
			}
				
			if (transform.position.x >= Fakeman.transform.position.x && facingright)
			{
				zz.HFlip();
				facingright = false;	
			}	
		}
		
		if (!taunted)
		{
			if (transform.position.x < Karateman.transform.position.x && !facingright)
			{
				zz.HFlip();
				facingright = true;	
			}
				
			if (transform.position.x >= Karateman.transform.position.x && facingright)
			{
				zz.HFlip();
				facingright = false;	
			}	
		}
		
	}
	
	public void explode()
	{
		
		if (!alreadyexploded)
		{
			if (health <= 0)
			{
				playerscript.ZZdefeated++;
				playerscript.XPgained += 30;
			}
			
			StartCoroutine (exploding () );	
			
		}
	}
}
