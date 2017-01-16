using UnityEngine;
using System.Collections;

public class PlayerInfo : MonoBehaviour {
	
	public GameObject player;
	public Player playerscript;
	
	//Stat modifications
	public int healthupgrade, strengthupgrade;
	
	//ability toggles
	public bool Hurricane = false, Groundsmash = false, Lazer = false, Spiritbomb = false, 
	Serenity = false, Mirrorimage = false, Zawarudo = false;
	
	void Start () {
		
		player = this.gameObject;
		playerscript = (Player)this.gameObject.GetComponent("Player");
		
		#region Stat Modifications-------------------------
		
		healthupgrade = PlayerPrefs.GetInt("Health");
		strengthupgrade = PlayerPrefs.GetInt("Strength");
		
		#endregion
		
		#region Ability Toggles------------------------------------------
//		if (PlayerPrefs.GetInt("HurricaneEnabled") == 1)
//			Hurricane = true;
//		if (PlayerPrefs.GetInt("HurricaneEnabled") == 0)
//			Hurricane = false;
//			
//		if (PlayerPrefs.GetInt("GroundsmashEnabled") == 1)
//			Groundsmash = true;
//		else Groundsmash = false;
//
//		if (PlayerPrefs.GetInt("LazerEnabled") == 1)
//			Lazer = true;
//		else Lazer = false;
//
//		if (PlayerPrefs.GetInt("SpiritbombEnabled") == 1)
//			Spiritbomb = true;
//		else Spiritbomb = false;
//
//		if (PlayerPrefs.GetInt("SerenityEnabled") == 1)
//			Serenity = true;
//		else Serenity = false;

		#endregion	
	
	}
	
	// Update is called once per frame
	void Update () {
		
		

		
		

		
	}
}
