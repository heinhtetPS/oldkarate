using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EndlessClear : MonoBehaviour {
	
	public Font headerfont, contentfont;
	public GUIStyle myStyle;
	
	private float counter;
	private bool calculationsdone = false, doonce = false;
	private int currentlevel;
	
	//boxes
	private Rect gratslabel = new Rect(Screen.width / 2 - 200, 60, 500, 150);
	private Rect defeatedlabel = new Rect(220, 120, 300, 100);
	private Rect progresslabel = new Rect(220, 220, 300, 100);
	private Rect acclabel = new Rect(220, 290, 300, 100);
	private Rect combolabel = new Rect(220, 360, 300, 100);
	private Rect finallabel = new Rect(220, 450, 300, 100);
	
	
	private Rect ranklabel = new Rect(Screen.width / 2 + 200, 110, 100, 50);
	private Rect Rankbox = new Rect(Screen.width / 2 + 220, 140, 100, 100);
		
	private Rect rewardslabel = new Rect(Screen.width / 2 + 200, Screen.height / 2 - 90, 250, 50);
	private Rect spoilslabel = new Rect(Screen.width / 2 + 200, Screen.height / 2 + 40, 250, 50);
	
	private Rect redrect = new Rect(Screen.width / 2 + 200, Screen.height / 2 + 100 , 12, 44);
	private Rect greenrect = new Rect(Screen.width / 2 + 200, Screen.height / 2 + 140, 12, 44);
	private Rect bluerect = new Rect(Screen.width / 2 + 200, Screen.height / 2 + 180, 12, 44);
	private Rect redamount = new Rect(Screen.width / 2 + 250, Screen.height / 2 + 110 , 150, 50);
	private Rect greenamount = new Rect(Screen.width / 2 + 250, Screen.height / 2 + 150, 150, 50);
	private Rect blueamount = new Rect(Screen.width / 2 + 250, Screen.height / 2 + 190, 150, 50);
	
	//bottom buttons
	private Rect restartbutton = new Rect(50, Screen.height - 90, 100, 25);
	private Rect dojobutton = new Rect(50, Screen.height - 60, 100, 25);
	private Rect Nextlevel = new Rect(Screen.width - 150, Screen.height - 90, 100, 25);
	private Rect levelselect = new Rect(Screen.width - 150, Screen.height - 60, 100, 25);
	
	public int enemiesdefeated, allenemies, wavesdefeated, highestcombo, moneys, finalscore, rank, maxair, checkpointkilled;
	
	public float hitscore, spacebar, survivaltime, xpgain;
	
	public float defeatedscore, accpercent, accscore, survivalscore, checkpointbonus, comboB, airscore;
	
	public Texture2D redscroll, greenscroll, bluescroll, checkmark;
	
	private Color grayblue = new Color(0, 0.8f, 0.8f, 1);
	public AudioClip moneyching;
	private bool demopicon = false;
	
	private string[] Endlessrewards = new string[]
	{
		" ",
		" Skill Unlock - Tackle",
		" $1,000",
		" $500"
	};
	
	void Start () {
		
		currentlevel = PlayerPrefs.GetInt("Currentlevel");
		
		allenemies = PlayerPrefs.GetInt("PunksDefeated") + PlayerPrefs.GetInt("WrestlersDefeated") + PlayerPrefs.GetInt("GroundsDefeated") +
			PlayerPrefs.GetInt("ThrowersDefeated") + PlayerPrefs.GetInt("ZZsdefeated") + PlayerPrefs.GetInt("Ninjasdefeated");
		
		
		
		doonce = false;
		
	
	}
	
	// Update is called once per frame
	void Update () {
		
		//devshit
		if (Input.GetKeyDown(KeyCode.M))
		{
			Debug.Log("CurrentLevel: " + PlayerPrefs.GetInt("Currentlevel").ToString());
			Debug.Log("Did I get S treasure? " + PlayerPrefs.GetInt("Level_" + PlayerPrefs.GetInt("Currentlevel") + "_S"));
			Debug.Log("Did I get A treasure? " + PlayerPrefs.GetInt("Level_" + PlayerPrefs.GetInt("Currentlevel") + "_A"));
			Debug.Log("Did I get B treasure? " + PlayerPrefs.GetInt("Level_" + PlayerPrefs.GetInt("Currentlevel") + "_B"));
			Debug.Log("This level rank: " + PlayerPrefs.GetInt("Level_" + PlayerPrefs.GetInt("Currentlevel") + "_Rank").ToString());
		}
			
		
		counter += Time.deltaTime;
		
		//making the numbers count up
		if (counter >= 0.01f)
		{
		
			//enemie count
			if (enemiesdefeated < allenemies)
					enemiesdefeated++;
			if (wavesdefeated < PlayerPrefs.GetInt("Endlesswavesdefeated"))
					wavesdefeated++;
				
			//survival
			if (survivaltime < PlayerPrefs.GetInt("Endlesssurvival"))
					survivaltime += 2;
				
			//accuracy
			if (hitscore < PlayerPrefs.GetFloat("Hitscore"))
					hitscore += 1.5f;
			if (spacebar < PlayerPrefs.GetFloat("Spacebar"))
					spacebar += 1.5f;
				
			//combo, airtime
			if (highestcombo < PlayerPrefs.GetInt("HighestCombo"))
					highestcombo++;	
			if (maxair < PlayerPrefs.GetInt("Maxairtime"))
					maxair++;
			
			//moneys & xp
			if (moneys < PlayerPrefs.GetInt("Moneythisround"))
				moneys++;
			if (xpgain < PlayerPrefs.GetFloat("XPgained"))
			xpgain += 1.5f;
				
				counter = 0;
			
		}
		
		
		//how to finish the counting
		if (spacebar >= PlayerPrefs.GetFloat("Hitscore") &&
			moneys >= PlayerPrefs.GetInt("Moneythisround") &&
			hitscore >= PlayerPrefs.GetFloat("Hitscore"))
			calculationsdone = true;
		
		
		if (calculationsdone && !doonce)
		{
			//make sure the values are correct before the real shit
			enemiesdefeated = allenemies;
			survivaltime = PlayerPrefs.GetInt("Endlesssurvival");
			wavesdefeated = PlayerPrefs.GetInt("Endlesswavesdefeated");
			hitscore = PlayerPrefs.GetFloat("Hitscore");
			spacebar = PlayerPrefs.GetFloat("Spacebar");;
			highestcombo = PlayerPrefs.GetInt("HighestCombo");
			maxair = PlayerPrefs.GetInt("Maxairtime");
			moneys = PlayerPrefs.GetInt("Moneythisround");
			checkpointkilled = PlayerPrefs.GetInt("Checkpointskilled");
			checkpointbonus = checkpointkilled * 10000;
			xpgain = PlayerPrefs.GetFloat("XPgained");
			
			//THE REAL SHIT
			
			defeatedscore = enemiesdefeated * 10;
			
			survivalscore = survivaltime * 10;
			
			accpercent = ((hitscore/spacebar * 100));
			accuracyscore();
		

			comboB = (highestcombo*100) *4;
			airscore = maxair * 100;
			
		
			finalscore = Mathf.RoundToInt((defeatedscore + accpercent + comboB + airscore + survivalscore));	
			
			//new highscore?
			if (PlayerPrefs.GetInt("Level_" + PlayerPrefs.GetInt("Currentlevel") + "_Highscore") == 0 ||
			PlayerPrefs.GetInt("Level_" + PlayerPrefs.GetInt("Currentlevel") + "_Highscore") < finalscore)
			PlayerPrefs.SetInt("Level_" + PlayerPrefs.GetInt("Currentlevel") + "_Highscore", finalscore);
			
			//new highest combo?
			if (PlayerPrefs.GetInt("Level_" + PlayerPrefs.GetInt("Currentlevel") + "_Combo") == 0 ||
			PlayerPrefs.GetInt("Level_" + PlayerPrefs.GetInt("Currentlevel") + "_Combo") < highestcombo)
			PlayerPrefs.SetInt("Level_" + PlayerPrefs.GetInt("Currentlevel") + "_Combo", highestcombo);
			
			//new Rank?
			Gradingtorankconversion();
			if (PlayerPrefs.GetInt("Level_" + PlayerPrefs.GetInt("Currentlevel") + "_Rank") == 0 ||
			PlayerPrefs.GetInt("Level_" + PlayerPrefs.GetInt("Currentlevel") + "_Rank") < rank)
			PlayerPrefs.SetInt("Level_" + PlayerPrefs.GetInt("Currentlevel") + "_Rank", rank);
			
			//give you your shit
			
			//LOOTZ
			PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + (int)(Mathf.Round(finalscore / 10) + moneys));
			PlayerPrefs.SetInt("Redscroll", PlayerPrefs.GetInt("Redscroll") + PlayerPrefs.GetInt("Redpickup"));
			PlayerPrefs.SetInt("Greenscroll", PlayerPrefs.GetInt("Greenscroll") + PlayerPrefs.GetInt("Greenpickup"));
			PlayerPrefs.SetInt("Bluescroll", PlayerPrefs.GetInt("Bluescroll") + PlayerPrefs.GetInt("Bluepickup"));
			
			//XP
			DistributeXP();
			
			//ranked
			Rankrewards();
			
			//CHING CHONG NING NONG
			StartCoroutine( Moneyching () );
			
			doonce = true;
		}
		
		
	
	}
	
	void OnGUI () {
		
    	myStyle.font = headerfont;
		
		myStyle.fontSize = 50;
		GUI.color = Color.yellow;
		GUI.Label(gratslabel, "CONGRATULATIONS! ", myStyle);
		GUI.color = Color.white;
		

		//DEFEATED LABEL
		myStyle.fontSize = 18;
		GUI.color = Color.yellow;
		GUI.Label(defeatedlabel, "Defeated: ", myStyle);
		GUI.color = Color.white;
		myStyle.font = contentfont;
		myStyle.fontSize = 16;
		
//		GUI.Label(new Rect(defeatedlabel.x + 30, 135, 400, 100),
//			"Punks: " + punkdefeated.ToString() +  "  x 100 = " + 
//			(punkdefeated * 100).ToString(), myStyle);
//		GUI.Label(new Rect(defeatedlabel.x + 30, 150, 400, 100),
//			"Throwers: " + throwersdefeated.ToString() + "  x 200 = " + 
//			(throwersdefeated * 200).ToString(), myStyle);
//		GUI.Label(new Rect(defeatedlabel.x + 30, 165, 400, 100),
//			"Goos: " + grounddefeated.ToString() + "  x 200 = " + 
//			(grounddefeated * 200).ToString(), myStyle);
//		GUI.Label(new Rect(defeatedlabel.x + 30, 180, 400, 100),
//			"Wrestlers: " + wrestlerdefeated.ToString() + "  x 500 = " + 
//			(wrestlerdefeated * 500).ToString(), myStyle);
		
		
		GUI.Label(new Rect(defeatedlabel.x + 30, 135, 400, 100),
			"Waves Conquered: " + wavesdefeated.ToString(), myStyle);
		GUI.Label(new Rect(defeatedlabel.x + 30, 150, 400, 100),
			"Enemies defeated: " + enemiesdefeated.ToString(), myStyle);
		GUI.Label(new Rect(defeatedlabel.x + 30, 165, 400, 100),
			"Special enemies defeated: 0" , myStyle);
		GUI.Label(new Rect(defeatedlabel.x + 30, 180, 400, 100), "Score: " + defeatedscore.ToString(), myStyle);
		
//	if (punkdefeated >= maxpunks &&
//		grounddefeated >= maxshits &&
//		wrestlerdefeated >= maxwrestlers)
//		GUI.Label(new Rect(250, 195, 350, 100), "Full kill bonus : 50000!" , myStyle);
		
		//ACCURACY LABEL
		myStyle.fontSize = 18;
		myStyle.font = headerfont;
		GUI.color = Color.yellow;
		GUI.Label(acclabel, "Accuracy: " , myStyle);
		GUI.color = Color.white;
		myStyle.font = contentfont;
		myStyle.fontSize = 16;
		GUI.Label(new Rect(acclabel.x + 30, acclabel.y + 15, 350, 100), 
			"Hits: " + Mathf.Round(hitscore).ToString() + " / Total: " + Mathf.Round(spacebar) + 
			" = " + Mathf.Round(accpercent).ToString() + "%", myStyle);
		GUI.Label(new Rect(acclabel.x + 30, acclabel.y + 30, 300, 100), "Accuracy Bonus: " + accscore, myStyle);
		
		//COMBO LABEL
		myStyle.fontSize = 18;
		myStyle.font = headerfont;
		GUI.color = Color.yellow;
		GUI.Label(combolabel, "Combo: " , myStyle);
		GUI.color = Color.white;
		myStyle.font = contentfont;
		myStyle.fontSize = 16;
		GUI.Label(new Rect(combolabel.x + 30, combolabel.y + 15, 300, 100), "Highest Combo: " + highestcombo.ToString(), myStyle );
		GUI.Label(new Rect(combolabel.x + 30, combolabel.y + 30, 300, 100), "Max Airtime: " + maxair.ToString() + " seconds", myStyle );
		if (highestcombo >= 10)
		GUI.Label(new Rect(combolabel.x + 30, combolabel.y + 45, 300, 100), "Combo Bonus:  " + comboB.ToString(), myStyle );
		
		//PROGRESS LABEL
		myStyle.fontSize = 18;
		myStyle.font = headerfont;
		GUI.color = Color.yellow;
		GUI.Label(progresslabel, "Progress: ", myStyle );
		GUI.color = Color.white;
		myStyle.font = contentfont;
		myStyle.fontSize = 16;
		GUI.Label(new Rect(progresslabel.x + 30, progresslabel.y + 15, 400, 100), 
			"Survival Time: " + survivaltime.ToString() + " seconds x 10 = " 
			+ survivalscore.ToString(), myStyle);
		
		GUI.Label(new Rect(progresslabel.x + 30, progresslabel.y + 30, 300, 100), "Checkpoint Bonus: " + checkpointbonus.ToString(), myStyle );
		
		
		//FINAL LABEL
		myStyle.fontSize = 18;
		myStyle.font = headerfont;
		GUI.color = Color.yellow;
		GUI.Label(finallabel, "Total Score: ", myStyle);
		GUI.color = Color.white;
		myStyle.font = contentfont;
		myStyle.fontSize = 16;
		GUI.Label(new Rect(finallabel.x + 30, finallabel.y + 20, 500, 100), 
			"Total: " + defeatedscore.ToString() + " + "
			+ Mathf.Round(survivalscore + checkpointbonus).ToString() + " + "
			+ accscore.ToString() + " + " +
			comboB.ToString() + " = " +
			Mathf.Round(finalscore).ToString(), myStyle); 
		GUI.Label(new Rect(finallabel.x + 30, finallabel.y + 35, 500, 100), "Money conversion: $" + (Mathf.Round(finalscore / 10) + moneys).ToString(), myStyle);
		
		//ranking Section
		myStyle.fontSize = 28;
		myStyle.font = headerfont;
		GUI.color = Color.yellow;
		GUI.Label (ranklabel, "Rank", myStyle);
		myStyle.fontSize = 58;
		
		GUI.color = Color.white;
		if (calculationsdone)
		GUI.Label (Rankbox, Gradingscale(), myStyle);
		
		//Rank rewards
		myStyle.fontSize = 28;
		GUI.color = Color.yellow;
		GUI.Label (rewardslabel, "Rank Rewards", myStyle);
		Hilightrewards();
		
		//Spoils section
		myStyle.fontSize = 28;
		myStyle.font = headerfont;
		GUI.color = Color.yellow;
		GUI.Label (spoilslabel, "Additional Loot", myStyle);
		
		myStyle.font = contentfont;
		GUI.color = Color.white;
		myStyle.fontSize = 16;
		GUI.Label(new Rect(spoilslabel.x, spoilslabel.y + 30, 300, 50), "Extra change: $" + moneys, myStyle);
		GUI.Label(new Rect(spoilslabel.x, spoilslabel.y + 45, 300, 50), "XP gained: " + Mathf.Round (xpgain), myStyle);
		GUI.DrawTexture(redrect, redscroll);
		GUI.DrawTexture(greenrect, greenscroll);
		GUI.DrawTexture(bluerect, bluescroll);
		
		GUI.Label(redamount, " x  " + PlayerPrefs.GetInt("Redpickup"), myStyle);
		GUI.Label(greenamount, " x  " + PlayerPrefs.GetInt("Greenpickup"), myStyle);
		GUI.Label(blueamount, " x  " + PlayerPrefs.GetInt("Bluepickup"), myStyle);
			
		
		
		//TRY AGAIN BOXES
		myStyle.fontSize = 18;
		myStyle.font = contentfont;
		GUI.color = Color.white;
		
		if (GUI.Button(restartbutton, "< Retry Level"))
		{
			Application.LoadLevel("Endless");
		}
		
		if (GUI.Button(dojobutton, "To Dojo"))
		{
			Application.LoadLevel("Dojo");
		}
		
//		if (GUI.Button(Nextlevel, "Next Level >"))
//		{
//			if (PlayerPrefs.GetInt("Currentlevel") == 1)
//				Application.LoadLevel("Comicpg4");
//			if (PlayerPrefs.GetInt("Currentlevel") == 2)
//				Application.LoadLevel("Comicpg5");
//			if (PlayerPrefs.GetInt("Currentlevel") == 3)
//				Application.LoadLevel("Level4");
//			if (PlayerPrefs.GetInt("Currentlevel") == 4)
//			{
//				demopicon = true;
//			}
//		}
		
		if (GUI.Button(levelselect, "Level Select"))
		{
			Application.LoadLevel("LevelSelect");	
		}
		
		
		
	}//END | ONGUI
	
	
	#region Ranking rewards functions--------------------------------------------------
	void Rankrewards()
	{
		
		
	}
	
	void accuracyscore()
	{
		if (accpercent < 60)
			accscore = 0;
		if (accpercent >= 60)
			accscore = 1200;
		if (accpercent >= 70)
			accscore = 2500;
		if (accpercent >= 80)
			accscore = 5000;
		if (accpercent >= 90)
			accscore = 10000;
		if (accpercent >= 100)
			accscore = 25000;
		
		
		
		
		
		
	}

	string Gradingscale()
	{
		if (finalscore < 10000)
			return "F";
		if (finalscore >= 10000 && finalscore < 20000)
			return "D";
		if (finalscore >= 20000 && finalscore < 30000)
			return "C";
		if (finalscore >= 30000 && finalscore < 40000)
			return "B";
		if (finalscore >= 40000 && finalscore < 50000)
			return "A";
		if (finalscore >= 50000)
			return "S";
		
		return null;
		
	}

	void Hilightrewards()
	{
		
		
		
		
		
	}

	void Gradingtorankconversion()
	{
		if (Gradingscale() == "S")
		rank = 3;
		
		if (Gradingscale() == "A")
		rank = 2;
		
		if (Gradingscale() == "B")
		rank = 1;
		
		if (Gradingscale() != "S" && Gradingscale() != "A" && Gradingscale() != "B")
		rank = 0;
	}
	
	int timebonuscalc()
	{
		if (survivaltime > 500)
			return 10000;
		if (survivaltime > 400)
			return 7500;	
		if (survivaltime > 300)
			return 5000;
		if (survivaltime > 200)
			return 1000;
		
			
				
				
				
		return 0;
	}
	
	void DistributeXP()
	{
		PlayerPrefs.SetFloat("XPtonextlevel", PlayerPrefs.GetFloat("XPtonextlevel") - xpgain);
		
		if (PlayerPrefs.GetFloat("XPtonextlevel") < 0)
		{
			PlayerPrefs.SetFloat("Placeholder", PlayerPrefs.GetFloat("XPtonextlevel") * -1);
			
			PlayerPrefs.SetInt("Playerlevel", PlayerPrefs.GetInt("Playerlevel") + 1);
			newlevelrequirements();
			
			PlayerPrefs.SetFloat("XPtonextlevel", PlayerPrefs.GetFloat("XPtonextlevel") - PlayerPrefs.GetFloat("Placeholder"));
			
		}
		
		
		
	}
	
	void newlevelrequirements()
	{
		if (PlayerPrefs.GetInt("Playerlevel") == 2)
			PlayerPrefs.SetFloat("XPtonextlevel", 600);
		
		if (PlayerPrefs.GetInt("Playerlevel") == 3)
			PlayerPrefs.SetFloat("XPtonextlevel", 1200);
		
		if (PlayerPrefs.GetInt("Playerlevel") == 3)
			PlayerPrefs.SetFloat("XPtonextlevel", 1800);
		
		if (PlayerPrefs.GetInt("Playerlevel") == 3)
			PlayerPrefs.SetFloat("XPtonextlevel", 2400);
		
		if (PlayerPrefs.GetInt("Playerlevel") == 4)
			PlayerPrefs.SetFloat("XPtonextlevel", 3000);
		
		if (PlayerPrefs.GetInt("Playerlevel") == 5)
			PlayerPrefs.SetFloat("XPtonextlevel", 3600);
		
		if (PlayerPrefs.GetInt("Playerlevel") == 6)
			PlayerPrefs.SetFloat("XPtonextlevel", 4200);
		
		if (PlayerPrefs.GetInt("Playerlevel") == 7)
			PlayerPrefs.SetFloat("XPtonextlevel", 4800);
		
		if (PlayerPrefs.GetInt("Playerlevel") == 8)
			PlayerPrefs.SetFloat("XPtonextlevel", 5400);
		
		if (PlayerPrefs.GetInt("Playerlevel") == 9)
			PlayerPrefs.SetFloat("XPtonextlevel", 6000);
		
		if (PlayerPrefs.GetInt("Playerlevel") == 10)
			PlayerPrefs.SetFloat("XPtonextlevel", 6600);
		
		if (PlayerPrefs.GetInt("Playerlevel") == 11)
			PlayerPrefs.SetFloat("XPtonextlevel", 7200);
		
		
	}

	IEnumerator Moneyching()
	{
		if (PlayerPrefs.GetInt("Level_" + PlayerPrefs.GetInt("Currentlevel") + "_Rank") > 0)
		audio.PlayOneShot(moneyching);
		
		yield return new WaitForSeconds(0.2f);
		
		if (PlayerPrefs.GetInt("Level_" + PlayerPrefs.GetInt("Currentlevel") + "_Rank") > 1)
		audio.PlayOneShot(moneyching);
		
		yield return new WaitForSeconds(0.4f);
		
		if (PlayerPrefs.GetInt("Level_" + PlayerPrefs.GetInt("Currentlevel") + "_Rank") > 2)
		audio.PlayOneShot(moneyching);
		
	}
	#endregion
}
