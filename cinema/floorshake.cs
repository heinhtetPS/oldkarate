using UnityEngine;
using System.Collections;

public class floorshake : MonoBehaviour {
	
	public exSprite floorsprite;
	
	public bool shaking = false;
	private float Xoffset, shaketimer;
	
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (shaking)
		{
			shaketimer += Time.deltaTime;
			float duration = 0.1f;
			float lerp = Mathf.PingPong (Time.time, duration) / duration;
			Xoffset = Mathf.Lerp(-4, 4, lerp);
			floorsprite.offset = new Vector2(0 + Xoffset, 0);
			
			if (shaketimer >= 0.5f)
			{
				shaking = false;
				shaketimer = 0;
			}
		}
	
		if (!shaking)
			floorsprite.offset = Vector2.zero;
	}
}
