using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelClear : MonoBehaviour {
	
	public Font headerfont, contentfont;
	public GUIStyle thestyle;
	public GUISkin notice, notice2;
	
	private float counter;
	private bool calculationsdone = false, doonce = false;
	private int currentlevel;
	
	//boxes
	private Rect gratslabel = new Rect(Screen.width / 2 - 200, 60, 500, 150);
	private Rect defeatedlabel = new Rect(220, 120, 300, 100);
	private Rect acclabel = new Rect(220, 295, 300, 100);
	private Rect combolabel = new Rect(220, 360, 300, 100);
	private Rect dmgdlabel = new Rect(220, 455, 300, 100);
	private Rect finallabel = new Rect(220, 520, 300, 100);
	private Rect newshitavailable = new Rect(50, Screen.height / 2 + 60, 96, 98);
	
	
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
	private Rect restartbutton = new Rect(40, Screen.height - 100, 130, 35);
	private Rect dojobutton = new Rect(40, Screen.height - 60, 130, 35);
	private Rect Nextlevel = new Rect(Screen.width - 160, Screen.height - 100, 130, 35);
	private Rect levelselect = new Rect(Screen.width - 160, Screen.height - 60, 130, 35);
	
	public int punkdefeated, maxpunks, wrestlerdefeated, maxwrestlers, grounddefeated, maxshits, highestcombo, moneys, finalscore, rank, maxair;
	
	public float hitscore, spacebar, dmgtaken, xpgain;
	
	public float defeatedscore, accscore, dmgscore, nodmgbonus, comboB, airscore;
	
	public Texture2D demo, redscroll, greenscroll, bluescroll, checkmark, newshinytext, left, right, rb1, rb2, rb3, rb4, rb5, rb6;
	
	private bool shinymessage, flashing, goingup, goingdown = true;
	private int rainbownumber;
	private float Yoffset, flashtimer, rainbowtimer;
	
	private Color grayblue = new Color(0, 0.7f, 0.7f, 1);
	public AudioClip moneyching;
	private bool demopicon = false;
	
	#region Rewardsstrings--------------------------------------------
	private string[] level1rewards = new string[]
	{
		" ",
		" Skill Unlock - Tackle",
		" $1,000",
		" $500"
	};

	private string[] level2rewards = new string[]
	{
		" ",
		" Hurricane Rune: T1white",
		" Blackscroll x 1",
		" Greenscroll x 5"
	};

	private string[] level3rewards = new string[]
	{
		" ",
		" Groundsmash Rune: T1white",
		" $1,000",
		" Blackscroll x 1"
	};

	private string[] level4rewards = new string[]
	{
		" ",
		" Skill Unlock: SpiritBomb",
		" $4,000",
		" Whitescroll x 1"
	};
	#endregion
	
	void Start () {
		
		currentlevel = PlayerPrefs.GetInt("Currentlevel");
		
		//enemy score
		maxpunks = PlayerPrefs.GetInt("MaxPunks");
		maxwrestlers = PlayerPrefs.GetInt("MaxWrestlers");
		maxshits = PlayerPrefs.GetInt("MaxGrounds");

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
			Debug.Log("dojo: " + PlayerPrefs.GetInt("Dojotutorial").ToString());
		}
			
		
		counter += Time.deltaTime;
		
		//making the numbers count up
		if (counter >= 0.01f)
		{
		
		//enemie count
		if (punkdefeated < PlayerPrefs.GetInt("PunksDefeated"))
			punkdefeated++;
		if (wrestlerdefeated < PlayerPrefs.GetInt("WrestlersDefeated"))
			wrestlerdefeated++;	
		if (grounddefeated < PlayerPrefs.GetInt("GroundsDefeated"))
			grounddefeated++;	
			
		//accuracy
		if (hitscore < PlayerPrefs.GetFloat("Hitscore"))
				hitscore++;
		if (spacebar < PlayerPrefs.GetFloat("Spacebar"))
				spacebar += 1.2f;
			
		//dmg, combo, airtime
		if (dmgtaken < PlayerPrefs.GetFloat("DmgTaken"))
				dmgtaken++;
		if (highestcombo < PlayerPrefs.GetInt("HighestCombo"))
				highestcombo++;	
		if (maxair < PlayerPrefs.GetInt("Maxairtime"))
				maxair++;
		
		//moneys && xp
		if (moneys < PlayerPrefs.GetInt("Moneythisround"))
			moneys++;
		if (xpgain < PlayerPrefs.GetFloat("XPgained"))
			xpgain += 1.5f;
			
			counter = 0;
			
		}
		
		
		//how to finish the counting
		if (spacebar >= PlayerPrefs.GetFloat("Spacebar") &&
			moneys >= PlayerPrefs.GetInt("Moneythisround") &&
			hitscore >= PlayerPrefs.GetFloat("Hitscore"))
			calculationsdone = true;
		
		
		if (calculationsdone && !doonce)
		{
			//make sure the values are correct before the real shit
			punkdefeated = PlayerPrefs.GetInt("PunksDefeated");
			wrestlerdefeated = PlayerPrefs.GetInt("WrestlersDefeated");
			grounddefeated = PlayerPrefs.GetInt("GroundsDefeated");
			hitscore = PlayerPrefs.GetFloat("Hitscore");
			spacebar = PlayerPrefs.GetFloat("Spacebar");
			dmgtaken = PlayerPrefs.GetFloat("DmgTaken");
			highestcombo = PlayerPrefs.GetInt("HighestCombo");
			maxair = PlayerPrefs.GetInt("Maxairtime");
			moneys = PlayerPrefs.GetInt("Moneythisround");
			xpgain = PlayerPrefs.GetFloat("XPgained");
			
			
			//THE REAL SHIT
			
			defeatedscore = (punkdefeated * 100) + (grounddefeated * 200) + 
			(wrestlerdefeated * 500);
		
			accscore = ((hitscore/spacebar * 100));
			if (accscore < 2000f)
				accscore = 0;
		
			dmgscore = (dmgtaken * 200);
			comboB = (highestcombo*100) *4;
			if (highestcombo < 20)
				comboB = 0;
			airscore = maxair * 100;
			if (maxair < 5)
				airscore = 0;
		
			finalscore = Mathf.RoundToInt((defeatedscore + accscore + comboB + airscore + moneys) - dmgscore);	
			
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
			PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + PlayerPrefs.GetInt("Moneythisround"));
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
		
		if (shinymessage)
		{
			flashtimer += Time.deltaTime;
			rainbowtimer += Time.deltaTime;
			
			if (flashtimer > 0.1f)
			{
				flashing = !flashing;	
				flashtimer = 0;
			}
			
			if (rainbowtimer > 0.02f)
			{
				rainbownumber++;
				if (rainbownumber > 6)
					rainbownumber = 1;
				rainbowtimer = 0;
			}
			
			if (goingup)
			{
				Yoffset -= 2;
				if (Yoffset < -20)
				{
					goingup = false;
					goingdown = true;
				}
				
			}
			
			if (goingdown)
			{
				Yoffset += 2;
				if (Yoffset > 20)
				{
					goingdown = false;
					goingup = true;
				}
				
			}
			
		}
	
	}
	
	void OnGUI () {
		
    	thestyle.font = headerfont;
		
		thestyle.fontSize = 40;
		GUI.color = Color.yellow;
		GUI.Label(gratslabel, "CONGRATULATIONS! ", thestyle);
		GUI.color = Color.white;
		

		//DEFEATED LABEL
		thestyle.fontSize = 18;
		GUI.color = Color.yellow;
		GUI.Label(defeatedlabel, "Defeated: ", thestyle);
		GUI.color = Color.white;
		thestyle.font = contentfont;
		thestyle.fontSize = 16;
		
		GUI.Label(new Rect(defeatedlabel.x + 30, 135, 400, 100),
			"Punks: " + punkdefeated.ToString() +  "  x 100 = " + 
			(punkdefeated * 100).ToString(), thestyle);
		GUI.Label(new Rect(defeatedlabel.x + 30, 150, 400, 100),
			"Goos: " + grounddefeated.ToString() + "  x 200 = " + 
			(grounddefeated * 200).ToString(), thestyle);
		GUI.Label(new Rect(defeatedlabel.x + 30, 165, 400, 100),
			"Wrestlers: " + wrestlerdefeated.ToString() + "  x 500 = " + 
			(wrestlerdefeated * 500).ToString(), thestyle);
		GUI.Label(new Rect(defeatedlabel.x + 30, 180, 400, 100), "Score: " + defeatedscore.ToString(), thestyle);
		
	if (punkdefeated >= maxpunks &&
		grounddefeated >= maxshits &&
		wrestlerdefeated >= maxwrestlers)
		GUI.Label(new Rect(250, 195, 350, 100), "Full kill bonus : 5000!" , thestyle);
		
		//ACCURACY LABEL
		thestyle.font = headerfont;
		thestyle.fontSize = 18;
		GUI.color = Color.yellow;
		GUI.Label(acclabel, "Accuracy: " , thestyle);
		GUI.color = Color.white;
		thestyle.font = contentfont;
		thestyle.fontSize = 16;
		GUI.Label(new Rect(acclabel.x + 30, acclabel.y + 15, 350, 100), 
			"Hits: " + hitscore.ToString() + " / Total: " + Mathf.Round(spacebar) + 
			" = " + Mathf.Round(hitscore/spacebar * 100).ToString() + "%", thestyle);
		GUI.Label(new Rect(acclabel.x + 30, acclabel.y + 30, 300, 100), "Accuracy Bonus: " + Mathf.Round (accscore), thestyle);
		
		//COMBO LABEL
		thestyle.font = headerfont;
		thestyle.fontSize = 18;
		GUI.color = Color.yellow;
		GUI.Label(combolabel, "Combo: " , thestyle);
		GUI.color = Color.white;
		thestyle.font = contentfont;
		thestyle.fontSize = 16;
		GUI.Label(new Rect(combolabel.x + 30, combolabel.y + 15, 300, 100), "Highest Combo: " + highestcombo.ToString(), thestyle );
		GUI.Label(new Rect(combolabel.x + 30, combolabel.y + 30, 300, 100), "Max Airtime: " + maxair.ToString() + " seconds", thestyle );
		GUI.Label(new Rect(combolabel.x + 30, combolabel.y + 45, 300, 100), "Combo Bonus:  " + comboB.ToString(), thestyle );
		GUI.Label(new Rect(combolabel.x + 30, combolabel.y + 60, 300, 100), "Airtime Bonus: " + airscore.ToString(), thestyle );
		
		//DAMAGE LABEL
		thestyle.font = headerfont;
		thestyle.fontSize = 18;
		GUI.color = Color.yellow;
		GUI.Label(dmgdlabel, "Damage Taken: ", thestyle );
		GUI.color = Color.white;
		thestyle.font = contentfont;
		thestyle.fontSize = 16;
		GUI.Label(new Rect(dmgdlabel.x + 30, dmgdlabel.y + 15, 400, 100), 
			"Damage Received: " + dmgtaken.ToString() + " x 200 = " 
			+ dmgscore.ToString(), thestyle);
		
		GUI.Label(new Rect(dmgdlabel.x + 30, dmgdlabel.y + 30, 300, 100), "No Damage Bonus: " + nodmgbonus.ToString(), thestyle );
		
		
		//FINAL LABEL
		thestyle.font = headerfont;
		thestyle.fontSize = 18;
		GUI.color = Color.yellow;
		GUI.Label(finallabel, "Total Score: ", thestyle);
		GUI.color = Color.white;
		thestyle.font = contentfont;
		thestyle.fontSize = 16;
		GUI.Label(new Rect(finallabel.x + 30, finallabel.y + 20, 500, 100), 
			defeatedscore.ToString() + " + " + Mathf.Round(accscore).ToString() + " + " + comboB.ToString() + 
			" + " + airscore.ToString() + " - " + dmgscore.ToString() + " = " + Mathf.Round(finalscore).ToString(), thestyle);
		
		//ranking Section
		thestyle.font = headerfont;
		thestyle.fontSize = 28;
		GUI.color = Color.yellow;
		GUI.Label (ranklabel, "Rank", thestyle);
		thestyle.fontSize = 58;
		
		GUI.color = Color.white;
		GUI.Label (Rankbox, Gradingscale(), thestyle);
		
		//Rank rewards
		thestyle.font = headerfont;
		thestyle.fontSize = 28;
		GUI.color = Color.yellow;
		GUI.Label (rewardslabel, "Rank Rewards", thestyle);
		Hilightrewards();
		
		//Spoils section
		thestyle.font = headerfont;
		thestyle.fontSize = 28;
		GUI.color = Color.yellow;
		GUI.Label (spoilslabel, "Additional Loot", thestyle);
		
		GUI.color = Color.white;
		thestyle.font = contentfont;
		thestyle.fontSize = 16;
		GUI.Label(new Rect(spoilslabel.x, spoilslabel.y + 30, 300, 50), "Extra change: $" + moneys, thestyle);
		GUI.Label(new Rect(spoilslabel.x, spoilslabel.y + 45, 300, 50), "XP gained: " + Mathf.Round (xpgain), thestyle);
		GUI.DrawTexture(redrect, redscroll);
		GUI.DrawTexture(greenrect, greenscroll);
		GUI.DrawTexture(bluerect, bluescroll);
		
		GUI.Label(redamount, " x  " + PlayerPrefs.GetInt("Redpickup"), thestyle);
		GUI.Label(greenamount, " x  " + PlayerPrefs.GetInt("Greenpickup"), thestyle);
		GUI.Label(blueamount, " x  " + PlayerPrefs.GetInt("Bluepickup"), thestyle);
			
		
		
		//BOTTOM BUTTONS
		thestyle.fontSize = 18;
		GUI.color = Color.white;
		GUI.skin = notice;
		
		//3 buttons besides dojo
	if (PlayerPrefs.GetInt("Dojotutorial") > 0 || PlayerPrefs.GetInt("Currentlevel") == 1 && PlayerPrefs.GetInt("Dojotutorial") == 0)
		{
			//retry button
			if (GUI.Button(restartbutton, "Retry Level"))
			{
				Application.LoadLevel(PlayerPrefs.GetInt("Currentlevel"));
			}
			GUI.DrawTexture(new Rect(restartbutton.x - 28, restartbutton.y + 2, 32, 32), left);
			
			//next level
			if (GUI.Button(Nextlevel, "Next Level"))
			{
				if (PlayerPrefs.GetInt("Currentlevel") == 1)
					Application.LoadLevel("Comicpg4");
				if (PlayerPrefs.GetInt("Currentlevel") == 2)
					Application.LoadLevel("Comicpg5");
				if (PlayerPrefs.GetInt("Currentlevel") == 3)
					Application.LoadLevel("Level4");
				if (PlayerPrefs.GetInt("Currentlevel") == 4)
				{
					demopicon = true;
				}
			}
			GUI.DrawTexture(new Rect(Nextlevel.x + 126, Nextlevel.y + 2, 32, 32), right);
			
			//level select
			if (GUI.Button(levelselect, "Level Select"))
			{
				Application.LoadLevel("LevelSelect");	
			}
		}
		
		
		//dojo button
		if (GUI.Button(dojobutton, "To Dojo"))
		{
			Application.LoadLevel("Dojo");
		}
		//shiny shit on dojo button
		if (PlayerPrefs.GetInt("Newabilitynotice") > 0 || PlayerPrefs.GetInt("Newpurchaseoptions") > 0)
		{
			if (rainbownumber == 1)
			GUI.DrawTexture(new Rect(dojobutton.x + 100, dojobutton.y + 10, 52, 17), rb1);
			
			if (rainbownumber == 2)
			GUI.DrawTexture(new Rect(dojobutton.x + 100, dojobutton.y + 10, 52, 17), rb2);
			
			if (rainbownumber == 3)
			GUI.DrawTexture(new Rect(dojobutton.x + 100, dojobutton.y + 10, 52, 17), rb3);
			
			if (rainbownumber == 4)
			GUI.DrawTexture(new Rect(dojobutton.x + 100, dojobutton.y + 10, 52, 17), rb4);
			
			if (rainbownumber == 5)
			GUI.DrawTexture(new Rect(dojobutton.x + 100, dojobutton.y + 10, 52, 17), rb5);
			
			if (rainbownumber == 6)
			GUI.DrawTexture(new Rect(dojobutton.x + 100, dojobutton.y + 10, 52, 17), rb6);
			
		}
		
		
		//for demo end
		if (demopicon)
		{
			GUI.DrawTexture(new Rect(0,0, Screen.width, Screen.height), demo);
			if (Input.anyKeyDown)
				demopicon = false;
			
		}
		
		
		//shiny message on left
		if (PlayerPrefs.GetInt("Newabilitynotice") > 0)
		{
			shinymessage = true;
			if (flashing)
			GUI.skin = notice;
			if (!flashing)
			GUI.skin = notice2;
			GUI.Label(new Rect(newshitavailable.x, newshitavailable.y + Yoffset, newshitavailable.width, newshitavailable.height), "new skill available!!");
			
		}
		
		if (PlayerPrefs.GetInt("Onetimedojonotice") > 0 && PlayerPrefs.GetInt("Currentlevel") == 2)
			GUI.DrawTexture(newshitavailable, newshinytext);
		
		
		GUI.skin = null;
	}//END | ONGUI
	
	
	#region Ranking rewards functions--------------------------------------------------
	void Rankrewards()
	{
		if (currentlevel == 1)
		{
			if (Gradingscale() == "S" && PlayerPrefs.GetInt("Level_1_S") == 0)
			{
				if (PlayerPrefs.GetInt("Tackleabilitystate") < 1)
				PlayerPrefs.SetInt("Tackleabilitystate", 1);
				if (PlayerPrefs.GetInt("Level_1_A") == 0)
				PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + 1000);
				if (PlayerPrefs.GetInt("Level_1_B") == 0)
				PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + 500);
				PlayerPrefs.SetInt("Level_1_S", 1);
				PlayerPrefs.SetInt("Level_1_A", 1);
				PlayerPrefs.SetInt("Level_1_B", 1);
				PlayerPrefs.SetInt("Newabilitynotice", 1);
			}
			
			if (Gradingscale() == "A" && PlayerPrefs.GetInt("Level_1_A") == 0)
			{
				PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + 1000);
				if (PlayerPrefs.GetInt("Level_1_B") == 0)
				PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + 500);
				PlayerPrefs.SetInt("Level_1_A", 1);
				PlayerPrefs.SetInt("Level_1_B", 1);
			}
			
			if (Gradingscale() == "B"  && PlayerPrefs.GetInt("Level_1_B") == 0)
			{
				PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + 500);
				PlayerPrefs.SetInt("Level_1_B", 1);
			}
			
		}
		
		if (currentlevel == 2)
		{
			if (Gradingscale() == "S" && PlayerPrefs.GetInt("Level_2_S") == 0)
			{
				if (PlayerPrefs.GetInt("Hurricaneabilitystate") < 3)
				PlayerPrefs.SetInt("Hurricaneabilitystate", 3);
				if (PlayerPrefs.GetInt("Level_2_A") == 0)
				PlayerPrefs.SetInt("Blackscroll", PlayerPrefs.GetInt("Blackscroll") + 1);
				if (PlayerPrefs.GetInt("Level_2_B") == 0)
				PlayerPrefs.SetInt("Greenscroll", PlayerPrefs.GetInt("Greenscroll") + 5);
				PlayerPrefs.SetInt("Level_2_S", 1);
				PlayerPrefs.SetInt("Level_2_A", 1);
				PlayerPrefs.SetInt("Level_2_B", 1);
				PlayerPrefs.SetInt("Newabilitynotice", 26);
			}
			
			if (Gradingscale() == "A" && PlayerPrefs.GetInt("Level_2_A") == 0)
			{
				PlayerPrefs.SetInt("Blackscroll", PlayerPrefs.GetInt("Blackscroll") + 1);
				if (PlayerPrefs.GetInt("Level_2_B") == 0)
				PlayerPrefs.SetInt("Greenscroll", PlayerPrefs.GetInt("Greenscroll") + 5);
				PlayerPrefs.SetInt("Level_2_A", 1);
				PlayerPrefs.SetInt("Level_2_B", 1);
			}
			
			if (Gradingscale() == "B" && PlayerPrefs.GetInt("Level_2_B") == 0)
			{
				PlayerPrefs.SetInt("Greenscroll", PlayerPrefs.GetInt("Greenscroll") + 5);
				PlayerPrefs.SetInt("Level_2_B", 1);
			}
			
		} 
		
		if (currentlevel == 3)
		{
			if (Gradingscale() == "S" && PlayerPrefs.GetInt("Level_3_S") == 0)
			{
				if (PlayerPrefs.GetInt("Groundsmashabilitystate") < 3)
				PlayerPrefs.SetInt("Groundsmashabilitystate", 3);
				if (PlayerPrefs.GetInt("Level_3_A") == 0)
				PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + 1000);
				if (PlayerPrefs.GetInt("Level_3_B") == 0)
				PlayerPrefs.SetInt("Blackscroll", PlayerPrefs.GetInt("Blackscroll") + 1);
				PlayerPrefs.SetInt("Level_3_S", 1);
				PlayerPrefs.SetInt("Level_3_A", 1);
				PlayerPrefs.SetInt("Level_3_B", 1);
				PlayerPrefs.SetInt("Newabilitynotice", 22);
			}
			
			if (Gradingscale() == "A" && PlayerPrefs.GetInt("Level_3_A") == 0)
			{
				PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + 1000);
				if (PlayerPrefs.GetInt("Level_3_B") == 0)
				PlayerPrefs.SetInt("Blackscroll", PlayerPrefs.GetInt("Blackscroll") + 1);
				PlayerPrefs.SetInt("Level_3_A", 1);
				PlayerPrefs.SetInt("Level_3_B", 1);
			}
			
			if (Gradingscale() == "B" && PlayerPrefs.GetInt("Level_3_B") == 0)
			{
				PlayerPrefs.SetInt("Blackscroll", PlayerPrefs.GetInt("Blackscroll") + 1);
				PlayerPrefs.SetInt("Level_3_B", 1);
			}
			
			
		} 
		
		if (currentlevel == 4)
		{
			if (Gradingscale() == "S" && PlayerPrefs.GetInt("Level_4_S") == 0)
			{
				if (PlayerPrefs.GetInt("Spiritbombabilitystate") < 1)
				PlayerPrefs.SetInt("Spiritbombabilitystate", 1); 
				if (PlayerPrefs.GetInt("Level_4_A") == 0)
				PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + 4000);
				if (PlayerPrefs.GetInt("Level_4_B") == 0)
				PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") + 1);
				PlayerPrefs.SetInt("Level_4_S", 1);
				PlayerPrefs.SetInt("Level_4_A", 1);
				PlayerPrefs.SetInt("Level_4_B", 1);
				PlayerPrefs.SetInt("Newabilitynotice", 3);
			}
			
			if (Gradingscale() == "A" && PlayerPrefs.GetInt("Level_4_A") == 0)
			{
				PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + 4000);
				if (PlayerPrefs.GetInt("Level_4_B") == 0)
				PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") + 1);
				PlayerPrefs.SetInt("Level_4_A", 1);
				PlayerPrefs.SetInt("Level_4_B", 1);
			}
			
			if (Gradingscale() == "B" && PlayerPrefs.GetInt("Level_4_B") == 0)
			{
				PlayerPrefs.SetInt("Whitescroll", PlayerPrefs.GetInt("Whitescroll") + 1);
				PlayerPrefs.SetInt("Level_4_B", 1);
			}
			
		} 
		
	}

	string Gradingscale()
	{
		if (currentlevel == 1)
		{
			if (finalscore < 5000)
				return "F";
			if (finalscore >= 5000 && finalscore < 10000)
				return "D";
			if (finalscore >= 10000 && finalscore < 20000)
				return "C";
			if (finalscore >= 20000 && finalscore < 30000)
				return "B";
			if (finalscore >= 30000 && finalscore < 48000)
				return "A";
			if (finalscore >= 48000)
				return "S";
		}
		
		if (currentlevel == 2)
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
		}
		
		if (currentlevel == 3)
		{
			if (finalscore < 20000)
				return "F";
			if (finalscore >= 20000 && finalscore < 30000)
				return "D";
			if (finalscore >= 30000 && finalscore < 40000)
				return "C";
			if (finalscore >= 40000 && finalscore < 50000)
				return "B";
			if (finalscore >= 50000 && finalscore < 60000)
				return "A";
			if (finalscore >= 60000)
				return "S";
		}
		
		if (currentlevel == 4)
		{
			if (finalscore < 20000)
				return "F";
			if (finalscore >= 20000 && finalscore < 30000)
				return "D";
			if (finalscore >= 30000 && finalscore < 40000)
				return "C";
			if (finalscore >= 40000 && finalscore < 50000)
				return "B";
			if (finalscore >= 50000 && finalscore < 60000)
				return "A";
			if (finalscore >= 60000)
				return "S";
		}
		
		
		
		return null;
		
	}

	void Hilightrewards()
	{
		thestyle.font = contentfont;
		//S rank
		if (Gradingscale() == "S")
		{
			//already claimed rewards
			if (PlayerPrefs.GetInt("Level_" + PlayerPrefs.GetInt("Currentlevel") + "_S") == 1)
			{
				GUI.color = grayblue;
				thestyle.fontSize = 16;
				GUI.Label(new Rect(rewardslabel.x, rewardslabel.y + 30, 300, 25), "S Rank: " + whichlevelrewards()[1], thestyle);
				GUI.Label(new Rect(rewardslabel.x, rewardslabel.y + 60, 250, 25), "A Rank: " + whichlevelrewards()[2], thestyle);
				GUI.Label(new Rect(rewardslabel.x, rewardslabel.y + 90, 250, 25), "B Rank: " + whichlevelrewards()[3], thestyle);
				
				GUI.color = Color.white;
				GUI.DrawTexture(new Rect(rewardslabel.x - 40, rewardslabel.y + 30, 30, 30), checkmark);
				GUI.DrawTexture(new Rect(rewardslabel.x - 40, rewardslabel.y + 60, 30, 30), checkmark);
				GUI.DrawTexture(new Rect(rewardslabel.x - 40, rewardslabel.y + 90, 30, 30), checkmark);
				
			}
			
			//new rewards!
			if (PlayerPrefs.GetInt("Level_" + PlayerPrefs.GetInt("Currentlevel") + "_S") == 0)
			{
				GUI.color = Color.cyan;
				thestyle.fontSize = 16;
				GUI.Label(new Rect(rewardslabel.x, rewardslabel.y + 30, 300, 25), "S Rank: " + whichlevelrewards()[1], thestyle);
				GUI.Label(new Rect(rewardslabel.x, rewardslabel.y + 60, 250, 25), "A Rank: " + whichlevelrewards()[2], thestyle);
				GUI.Label(new Rect(rewardslabel.x, rewardslabel.y + 90, 250, 25), "B Rank: " + whichlevelrewards()[3], thestyle);
				
				GUI.color = Color.white;
				GUI.DrawTexture(new Rect(rewardslabel.x - 40, rewardslabel.y + 30, 30, 30), checkmark);
				GUI.DrawTexture(new Rect(rewardslabel.x - 40, rewardslabel.y + 60, 30, 30), checkmark);
				GUI.DrawTexture(new Rect(rewardslabel.x - 40, rewardslabel.y + 90, 30, 30), checkmark);
			}
		}
		
		//A rank
		if (Gradingscale() == "A")
		{
			if (PlayerPrefs.GetInt("Level_" + PlayerPrefs.GetInt("Currentlevel") + "_A") == 1)
			{
				GUI.color = Color.white;
				thestyle.fontSize = 16;
				GUI.Label(new Rect(rewardslabel.x, rewardslabel.y + 30, 300, 25), "S Rank: " + whichlevelrewards()[1], thestyle);
				GUI.color = grayblue;
				GUI.Label(new Rect(rewardslabel.x, rewardslabel.y + 60, 250, 25), "A Rank: " + whichlevelrewards()[2], thestyle);
				GUI.Label(new Rect(rewardslabel.x, rewardslabel.y + 90, 250, 25), "B Rank: " + whichlevelrewards()[3], thestyle);
				
				GUI.color = Color.white;
				GUI.DrawTexture(new Rect(rewardslabel.x - 40, rewardslabel.y + 60, 30, 30), checkmark);
				GUI.DrawTexture(new Rect(rewardslabel.x - 40, rewardslabel.y + 90, 30, 30), checkmark);
			}
			
			if (PlayerPrefs.GetInt("Level_" + PlayerPrefs.GetInt("Currentlevel") + "_A") == 0)
			{
				GUI.color = Color.white;
				thestyle.fontSize = 16;
				GUI.Label(new Rect(rewardslabel.x, rewardslabel.y + 30, 300, 25), "S Rank: " + whichlevelrewards()[1], thestyle);
				GUI.color = Color.cyan;
				GUI.Label(new Rect(rewardslabel.x, rewardslabel.y + 60, 250, 25), "A Rank: " + whichlevelrewards()[2], thestyle);
				GUI.Label(new Rect(rewardslabel.x, rewardslabel.y + 90, 250, 25), "B Rank: " + whichlevelrewards()[3], thestyle);
				
				GUI.color = Color.white;
				GUI.DrawTexture(new Rect(rewardslabel.x - 40, rewardslabel.y + 60, 30, 30), checkmark);
				GUI.DrawTexture(new Rect(rewardslabel.x - 40, rewardslabel.y + 90, 30, 30), checkmark);
				
			}
		}
		
		//B rank
		if (Gradingscale() == "B")
		{
			if (PlayerPrefs.GetInt("Level_" + PlayerPrefs.GetInt("Currentlevel") + "_B") == 1)
			{
				GUI.color = Color.white;
				thestyle.fontSize = 16;
				GUI.Label(new Rect(rewardslabel.x, rewardslabel.y + 30, 300, 25), "S Rank: " + whichlevelrewards()[1], thestyle);
				GUI.Label(new Rect(rewardslabel.x, rewardslabel.y + 60, 250, 25), "A Rank: " + whichlevelrewards()[2], thestyle);
				GUI.color = grayblue;
				GUI.Label(new Rect(rewardslabel.x, rewardslabel.y + 90, 250, 25), "B Rank: " + whichlevelrewards()[3], thestyle);
				
				GUI.color = Color.white;
				GUI.DrawTexture(new Rect(rewardslabel.x - 40, rewardslabel.y + 90, 30, 30), checkmark);
			}
			
			if (PlayerPrefs.GetInt("Level_" + PlayerPrefs.GetInt("Currentlevel") + "_B") == 0)
			{
				GUI.color = Color.white;
				thestyle.fontSize = 16;
				GUI.Label(new Rect(rewardslabel.x, rewardslabel.y + 30, 300, 25), "S Rank: " + whichlevelrewards()[1], thestyle);
				GUI.Label(new Rect(rewardslabel.x, rewardslabel.y + 60, 250, 25), "A Rank: " + whichlevelrewards()[2], thestyle);
				GUI.color = Color.cyan;
				GUI.Label(new Rect(rewardslabel.x, rewardslabel.y + 90, 250, 25), "B Rank: " + whichlevelrewards()[3], thestyle);
				
				GUI.color = Color.white;
				GUI.DrawTexture(new Rect(rewardslabel.x - 40, rewardslabel.y + 90, 30, 30), checkmark);
			}
		}
		
		//no rank
		if (Gradingscale() != "S" && Gradingscale() != "A" && Gradingscale() != "B")
		{
			GUI.color = Color.white;
			thestyle.fontSize = 16;
			GUI.Label(new Rect(rewardslabel.x, rewardslabel.y + 30, 300, 25), "S Rank: " + whichlevelrewards()[1], thestyle);
			GUI.Label(new Rect(rewardslabel.x, rewardslabel.y + 60, 250, 25), "A Rank: " + whichlevelrewards()[2], thestyle);
			GUI.Label(new Rect(rewardslabel.x, rewardslabel.y + 90, 250, 25), "B Rank: " + whichlevelrewards()[3], thestyle);
		}
		
		
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

	string[] whichlevelrewards()
	{
		if (currentlevel == 1)
		return level1rewards;
		
		if (currentlevel == 2)
		return level2rewards;
		
		if (currentlevel == 3)
		return level3rewards;
		
		if (currentlevel == 4)
		return level4rewards;
		
		return null;
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
