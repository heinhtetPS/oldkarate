using UnityEngine;
using System.Collections;

public class Backgroundgeneral : MonoBehaviour {

	float xoffset;
	bool shaking;
	
	public exSprite thisbackg;
	pausemenu pausescript;
	
	void Start () {
		
		pausescript = (pausemenu)GameObject.FindGameObjectWithTag("MainCamera").GetComponent("pausemenu");
	
//		transform.position = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!pausescript.pausemenud)
		{

			thisbackg.offset = new Vector2(thisbackg.offset.x + xoffset, 0);
			
			if (shaking)
			{
				float duration = 0.1f;
	   			float lerp = Mathf.PingPong (Time.time, duration) / duration;
	 			xoffset = Mathf.Lerp(-4, 4, lerp);
			}
		}
	
	}
	
	public void shake()
	{
		StartCoroutine( Toggleshake () );	
	}
	
	IEnumerator Toggleshake()
	{
		shaking = true;
		
		yield return new WaitForSeconds(0.5f);
		
		shaking = false;
		xoffset = 0;
		thisbackg.offset = Vector2.zero;
	}
}
