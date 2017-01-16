using UnityEngine;
using System.Collections;

public class SpiritBomb : MonoBehaviour {
	
	public enum State
	{
		Arming,
		Armed,
		Exploding,
		Other
	}
	public State state = State.Arming;
	public GameObject explosion100, explosion150;
	public bool exploded = false;
	
	public bool alreadydiddmg = false, alreadyheald = false, blackt1 = false, blackt2 = false, whitet1 = false, whitet2 = false;
	
	private float armingdelay, destroydelay, explosiondelay;
	public AudioClip smallexplo, smallnoise;
	public Karateoboxnew obox;
	
	
	// Use this for initialization
	void Start () {
		
		obox = (Karateoboxnew)GameObject.FindGameObjectWithTag("Offense").GetComponent("Karateoboxnew");
		if (PlayerPrefs.GetString("SpiritbombT1") == "black")
			blackt1 = true;
		if (PlayerPrefs.GetString("SpiritbombT2") == "black")
			blackt2 = true;
		if (PlayerPrefs.GetString("SpiritbombT1") == "white")
			whitet1 = true;
		if (PlayerPrefs.GetString("SpiritbombT2") == "white")
			whitet2 = true;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (transform.position.z != -100)
				transform.position = new Vector3(transform.position.x, transform.position.y, -100);
		
		if (state == State.Arming)
		{
			armingdelay += Time.deltaTime;		
			
		}
		
		if (armingdelay - Time.deltaTime >= 0.1f)
		{
			state = State.Armed;
			audio.PlayOneShot(smallnoise);
			armingdelay = 0;
		}
		
		if (state == State.Armed)
		{
			
			
			
		}
		
		if (state == State.Exploding)
		{
			renderer.enabled = false;
			
			if (blackt1 && !exploded)
			{
				(gameObject.collider as SphereCollider).radius = 150f;
				GameObject thisexplo = (GameObject)Instantiate(explosion150, transform.position, transform.rotation);
				Explosioncollisions exploscript = (Explosioncollisions)thisexplo.GetComponent("Explosioncollisions");
				exploscript.Sbomb = true;
				exploded = true;
			}
			else
			if (!blackt1 && !exploded)
			{
				(gameObject.collider as SphereCollider).radius = 100f;
				GameObject thisexplo = (GameObject)Instantiate(explosion100, transform.position, transform.rotation);
				Explosioncollisions exploscript = (Explosioncollisions)thisexplo.GetComponent("Explosioncollisions");
				exploscript.Sbomb = true;
				exploded = true;
			}
			
			destroydelay += Time.deltaTime;
			explosiondelay += Time.deltaTime;
			
		}
		
		if (destroydelay - Time.deltaTime > 0.5f)
			Destroy(this.gameObject);
	
	}
	
	void OnTriggerEnter (Collider otherObject) 
	{
		if (state == State.Armed && !whitet2 && !whitet1 && !blackt2)
		{
			if (otherObject.tag == "Enemy" ||
				otherObject.tag == "Enemy2" ||
				otherObject.tag == "Enemy3" ||
				otherObject.tag == "Ground" ||
				otherObject.tag == "Ground2" ||
				otherObject.tag == "Ground3" ||
				otherObject.tag == "Hardcore" ||
				otherObject.tag == "Hardcore2" ||
				otherObject.tag == "Hardcore3" ||
				otherObject.tag == "Ninja1" ||
				otherObject.tag == "Ninja2" ||
				otherObject.tag == "Bomb" ||
				otherObject.tag == "Boss" ||
				otherObject.tag == "Minion" ||
				otherObject.tag == "Minion2" ||
				otherObject.tag == "Creep")
				TriggerExplosion();
		}
		
	}
	
	public void TriggerExplosion ()
	{
		state = State.Exploding;
		
	}
	
	public void DelayedExplosion (float time)
	{
		StartCoroutine ( delayedexplo (time) );
	}
	
	IEnumerator delayedexplo(float time)
	{
		yield return new WaitForSeconds(time);
		
		TriggerExplosion();
		
	}
	
	public Vector3 Getdiff(Vector3 enemyposition)
	{
		Vector3 diff = transform.position - enemyposition; 
		return diff;
	}
}
