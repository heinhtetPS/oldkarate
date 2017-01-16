using UnityEngine;
using System.Collections;

public class Checkpointthing : MonoBehaviour {
	
	public int health = 4;
	bool shownotification = false, doonce = false;
	
	// Use this for initialization
	void Start () {
		
		if (transform.position.x > 0)
			rigidbody.velocity = new Vector3 (-75, Random.Range(-50, 50), 0);
		
		if (transform.position.x <= 0)
			rigidbody.velocity = new Vector3 (75, Random.Range(-50, 50), 0);
	
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.Rotate(new Vector3(0, 0, 1) * 8);
	
		//goodbye
		if (transform.position.x > 700 || transform.position.x < -700 || transform.position.y > 500)
			Destroy(this.gameObject);
		
		if (health <= 0)
			StartCoroutine ( Grantcheckpoint() );
	}
	
	IEnumerator Grantcheckpoint()
	{
		if (!doonce)
		{
			rigidbody.velocity = Vector3.zero;
			if (!audio.isPlaying)
			audio.Play();
			renderer.enabled = false;
			collider.enabled = false;
			shownotification = true;
			Docheckpointstuff();
			PlayerPrefs.SetInt("Checkpointskilled", PlayerPrefs.GetInt("Checkpointskilled") + 1);
			doonce = true;
		}
		
		yield return new WaitForSeconds(4);
		
		Destroy(this.gameObject);
		
		
	}
	
	void Docheckpointstuff()
	{
		if (Application.loadedLevelName == "Endless")
		{
			Spawner spawnscript = (Spawner)GameObject.FindGameObjectWithTag("Left").GetComponent("Spawner");
			if (spawnscript.enemyCount >= 100 && spawnscript.enemyCount < 200)
			{
				PlayerPrefs.SetInt("Endlesscheckpoint", 1);
				PlayerPrefs.SetInt("Endlessmax", 1);
			}
			
			if (spawnscript.enemyCount >= 200 && spawnscript.enemyCount < 300)
			{
				PlayerPrefs.SetInt("Endlesscheckpoint", 2);
				PlayerPrefs.SetInt("Endlessmax", 2);
			}
			
		}
		
	}
	
	void OnGUI()
	{
		if (shownotification)
		{
			GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 300, 100), "CHECKPOINT REACHED!");	
			
		}
		
	}
}
