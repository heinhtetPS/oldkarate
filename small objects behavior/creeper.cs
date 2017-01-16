using UnityEngine;
using System.Collections;

public class creeper : MonoBehaviour {

	public int numFrames;
	public int CurFrame;
	private float screenreset;
	private bool gotrandom;
	private float walkspeed;
	
	public bool slowed = false;
	private float slowtimer = 0;
	
	public BoxCollider basket;
	public BoxCollider head;
	
	private bool chillin = false;
	private float framedelay;
	
	void Start () {
		
		renderer.material.mainTextureOffset = new Vector2(0f, 0f);
		Getrandom();
	
	}
	
	
	void Update () {
		
		//variables
		framedelay += Time.deltaTime;
		float offset = 1.0f/numFrames;
		
		if (!chillin)
		walkspeed = Random.Range (75, 200);
		
		//walking
		if (!slowed)
		transform.Translate(new Vector3(walkspeed,0,0) * Time.deltaTime);
		
		if (slowed)
		transform.Translate(new Vector3(walkspeed / 2,0,0) * Time.deltaTime);
		
		//fuck 3d
		if (transform.position.z != -110)
			transform.position = new Vector3(transform.position.x, transform.position.y, -110);
		
		//animution
		if (!chillin)
		{
			if (framedelay - Time.deltaTime >= 0.1f)
			{
				
				renderer.material.mainTextureOffset = new Vector2(
					(float)renderer.material.mainTextureOffset.x + offset, 
					0f);
				CurFrame++;
				framedelay = 0f;
				if (CurFrame > 3)
				{
					renderer.material.mainTextureOffset = new Vector2(0f, 0f);
					CurFrame= 1;
				}
			}
		}
		
		//status effects
		if (slowed)
		{
			slowtimer += Time.deltaTime;
			renderer.material.color = Color.yellow;
			
			if (slowtimer > 10)
			{
				slowed = false;
				slowtimer = 0;
				renderer.material.color = Color.white;
			}
			
		}
		
		//get screen reset
		if (transform.position.x < -750)
		{
			Getrandom();
		}
		
		//restrictions
		if (transform.position.x <= screenreset)
		{
			transform.position = new Vector3(800 , transform.position.y, transform.position.z);
			gotrandom = false;
		}
		
		
		
	
		if (Input.GetKeyDown(KeyCode.K))
			walkspeed = 0;
		
	}//end update
	
	
	
	IEnumerator StopandSmile (int timetostop)
	{
		chillin = true;
		walkspeed = 0;
		renderer.material.mainTextureOffset = new Vector2(0.75f, 0);
		
		yield return new WaitForSeconds(timetostop);
		
		renderer.material.mainTextureOffset = Vector2.zero;
		CurFrame = 1;
		if (slowed)
		{
			slowed = false;
			slowtimer = 0;
			renderer.material.color = Color.white;
		}
		chillin = false;
	}
	
	IEnumerator Stopforawhile (int timetostop)
	{
		chillin = true;
		walkspeed = 0;
		
		yield return new WaitForSeconds(timetostop);
		
		chillin = false;
	}
	
	public void stoproutine(int timett)
	{
		StartCoroutine( Stopforawhile(timett) );	
	}
	
	public void smileroutine(int timett)
	{
		StartCoroutine( StopandSmile(timett) );	
	}
	
	void Getrandom()
	{
		screenreset = Random.Range (-800, -900);	
		gotrandom = true;
	}
}
