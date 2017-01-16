using UnityEngine;
using System.Collections;

public class GUIicons : MonoBehaviour {
	
	//import shit
	public Texture2D mask, skillsbar, skillsbackg, beamicon, gsmashicon, hurricon, sbombicon, sereniticon, tdooricon,
	tackleicon, forceicon, cloneicon, wallicon, sapecialicon;
	public Player playerscript;
	public PlayerInfo infoscript;
	private pausemenu pausescript;
	
	//positions
	private Rect skillsbarloc = new Rect(Screen.width - 275, 20, 246, 58);
	private Rect firstslot = new Rect(Screen.width - 263, 25, 48, 48);
	private Rect secondslot = new Rect(Screen.width - 205, 25, 48, 48);
	private Rect biggggslot = new Rect(Screen.width / 2 - 76, 160, 96, 96);
	private Rect thirdslot = new Rect(Screen.width - 147, 25, 48, 48);
	private Rect fourthslot = new Rect(Screen.width - 89, 25, 48, 48);
	
	public string Slot1content, Slot2content, Slot3content, Slot4content;
	public bool Slot1cooldown = false, Slot2cooldown = false, Slot3cooldown = false, Slot4cooldown = false, 
	S1CDfound = false, S2CDfound = false, S3CDfound = false, S4CDfound = false, 
	Slot1insuf = false, Slot2insuf = false, Slot3insuf = false, Slot4insuf = false;
	
	private float Slot1CD, Slot2CD, Slot3CD, Slot4CD;
	
	private Color cooldown = new Color(0.2f, 0.2f, 0.2f, 0.5f), 
			  insufficient = new Color(0.1f, 0.1f, 0.1f, 0.75f);
	
	public bool tutorialevent1 = false, tutorialevent2 = false, flashing = false;
	private float flashtimer;
	
	// Use this for initialization
	void Start () {
		
		Slot1content = PlayerPrefs.GetString("Slot1");
		Slot2content = PlayerPrefs.GetString("Slot2");
		Slot3content = PlayerPrefs.GetString("Slot3");
		Slot4content = PlayerPrefs.GetString("Slot4");
		
		playerscript = (Player)GameObject.FindGameObjectWithTag("Player").GetComponent("Player");
		pausescript = (pausemenu)GameObject.FindGameObjectWithTag("MainCamera").GetComponent("pausemenu");
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Slot1cooldown)
		{
			Slot1CD -= Time.deltaTime;	
			if (Slot1CD <= 0)
			{
				Slot1CD = 0;
				Slot1cooldown = false;
			}
		}
		
		if (Slot2cooldown)
		{
			Slot2CD -= Time.deltaTime;	
			if (Slot2CD <= 0)
			{
				Slot2CD = 0;
				Slot2cooldown = false;
			}
		}
		
		if (Slot3cooldown)
		{
			Slot3CD -= Time.deltaTime;	
			if (Slot3CD <= 0)
			{
				Slot3CD = 0;
				Slot3cooldown = false;
			}
		}
		
		if (Slot4cooldown)
		{
			Slot4CD -= Time.deltaTime;	
			if (Slot4CD <= 0)
			{
				Slot4CD = 0;
				Slot4cooldown = false;
			}
		}
		
		if (tutorialevent1 || tutorialevent2)
		{
			flashtimer += Time.deltaTime;
			
			if (flashtimer > 0.2f)
			{
				flashing = !flashing;	
				flashtimer = 0;
			}
			
		}
		
	
	}
	
	void OnGUI () 
	{
//		GUI.Label(new Rect(0, 100, 200, 100), "S1insuf == " + Slot1insuf.ToString());
//		GUI.Label(new Rect(0, 120, 200, 100), "s4insuf: " + Slot3insuf.ToString());
//		GUI.Label(new Rect(0, 180, 200, 100), "s4insuf: " + Slot4insuf.ToString());
		
		if (!playerscript.endtime && !playerscript.stagetime && !pausescript.pausemenud)
		{	
		
		//skillsbar
//		GUI.color = insufficient;
//		GUI.DrawTexture(skillsbarloc, skillsbackg);
		GUI.color = Color.white;
		GUI.DrawTexture(skillsbarloc, skillsbar);
		
		
		
		//Slot 1 Content
		if (Slot1content == null)
		{
			
		}
			
		if (Slot1content == "Groundsmash")
		{
			checkGsmash(1);
			if (tutorialevent2)
				{
					if (flashing)
					GUI.DrawTexture(biggggslot, gsmashicon);
					
				}
			else
			{
				if (!Slot1cooldown)
				{
					GUI.color = Color.white;
					GUI.DrawTexture(firstslot, gsmashicon);	
				}
				if (Slot1cooldown)
				{
					GUI.color = cooldown;
					GUI.DrawTexture(firstslot, gsmashicon);
					GUI.color = Color.white;
					GUI.Label(new Rect(firstslot.x + 16, firstslot.y + 12, firstslot.width, firstslot.height), Mathf.Round(Slot1CD).ToString());
				}
			}
		}
		if (Slot1content == "Tackle")
		{
			checkTackle(1);
				if (!Slot1cooldown)
				{
					GUI.color = Color.white;
					GUI.DrawTexture(firstslot, tackleicon);	
				}
				if (Slot1cooldown)
				{
					GUI.color = cooldown;
					GUI.DrawTexture(firstslot, tackleicon);
					GUI.color = Color.white;
					GUI.Label(new Rect(firstslot.x + 16, firstslot.y + 12, firstslot.width, firstslot.height), Mathf.Round(Slot1CD).ToString());
				}
		}
		if (Slot1content == "Force")
		{
			checkForce(1);
				if (!Slot1cooldown)
				{
					GUI.color = Color.white;
					GUI.DrawTexture(firstslot, forceicon);	
				}
				if (Slot1cooldown)
				{
					GUI.color = cooldown;
					GUI.DrawTexture(firstslot, forceicon);
					GUI.color = Color.white;
					GUI.Label(new Rect(firstslot.x + 16, firstslot.y + 12, firstslot.width, firstslot.height), Mathf.Round(Slot1CD).ToString());
				}
		}
		if (Slot1content == "Hurricane")
		{
			checkHurri(1);
			if (Slot1insuf)
			{
				GUI.color = insufficient;
				GUI.DrawTexture(firstslot, hurricon);
			}
			if (!Slot1cooldown && !Slot1insuf)
			{
				GUI.color = Color.white;
				GUI.DrawTexture(firstslot, hurricon);	
			}
			if (Slot1cooldown)
			{
				GUI.color = cooldown;
				GUI.DrawTexture(firstslot, hurricon);
				GUI.color = Color.white;
				GUI.Label(new Rect(firstslot.x + 16, firstslot.y + 12, firstslot.width, firstslot.height), Mathf.Round(Slot1CD).ToString());
			}
			
		}
		if (Slot1content == "Lazer")
		{
			checklazer(1);
			if (Slot1insuf)
			{
				GUI.color = insufficient;
				GUI.DrawTexture(firstslot, beamicon);
			}
			if (!Slot1cooldown && !Slot1insuf)
			{
				GUI.color = Color.white;
				GUI.DrawTexture(firstslot, beamicon);	
			}
			if (Slot1cooldown)
			{
				GUI.color = cooldown;
				GUI.DrawTexture(firstslot, beamicon);
				GUI.color = Color.white;
				GUI.Label(new Rect(firstslot.x + 16, firstslot.y + 12, firstslot.width, firstslot.height), Mathf.Round(Slot1CD).ToString());
			}
		}
		if (Slot1content == "Spiritbomb")
		{
			checkSbomb(1);
			if (Slot1insuf)
			{
				GUI.color = insufficient;
				GUI.DrawTexture(firstslot, sbombicon);
			}
			if (!Slot1insuf)
			{
				GUI.color = Color.white;
				GUI.DrawTexture(firstslot, sbombicon);
			}
		}
		if (Slot1content == "Serenity")
		{
			checkSeren(1);
				if (Slot1insuf)
				{
					GUI.color = insufficient;
					GUI.DrawTexture(firstslot, sereniticon);
				}
				if (!Slot1cooldown && !Slot1insuf)
				{
					GUI.color = Color.white;
					GUI.DrawTexture(firstslot, sereniticon);	
				}
				if (Slot1cooldown)
				{
					GUI.color = cooldown;
					GUI.DrawTexture(firstslot, sereniticon);
					GUI.color = Color.white;
					GUI.Label(new Rect(firstslot.x + 16, firstslot.y + 12, firstslot.width, firstslot.height), Mathf.Round(Slot1CD).ToString());
				}
		}
		if (Slot1content == "Teledoor")
		{
				checktdoor(1);
				if (Slot1insuf)
				{
					GUI.color = insufficient;
					GUI.DrawTexture(firstslot, tdooricon);
				}
				if (!Slot1insuf)
				{
					GUI.color = Color.white;
					GUI.DrawTexture(firstslot, tdooricon);	
				}
//				if (Slot1cooldown)
//				{
//					GUI.color = cooldown;
//					GUI.DrawTexture(firstslot, tdooricon);
//					GUI.color = Color.white;
//					GUI.Label(new Rect(firstslot.x + 16, firstslot.y + 12, firstslot.width, firstslot.height), Mathf.Round(Slot1CD).ToString());
//				}
		}
		
		if (Slot1content == "Clone")
		{
				checkclone(1);
				if (Slot1insuf)
				{
					GUI.color = insufficient;
					GUI.DrawTexture(firstslot, cloneicon);
				}
				if (!Slot1cooldown && !Slot1insuf)
				{
					GUI.color = Color.white;
					GUI.DrawTexture(firstslot, cloneicon);	
				}
				if (Slot1cooldown)
				{
					GUI.color = cooldown;
					GUI.DrawTexture(firstslot, cloneicon);
					GUI.color = Color.white;
					GUI.Label(new Rect(firstslot.x + 16, firstslot.y + 12, firstslot.width, firstslot.height), Mathf.Round(Slot1CD).ToString());
				}
		}
		
		if (Slot1content == "Wall")
		{
				checkwall(1);
				if (!Slot1cooldown)
				{
					GUI.color = Color.white;
					GUI.DrawTexture(firstslot, wallicon);	
				}
				if (Slot1cooldown)
				{
					GUI.color = cooldown;
					GUI.DrawTexture(firstslot, wallicon);
					GUI.color = Color.white;
					GUI.Label(new Rect(firstslot.x + 16, firstslot.y + 12, firstslot.width, firstslot.height), Mathf.Round(Slot1CD).ToString());
				}
		}
		
		
		//Slot 2 Content
		if (Slot2content == null)
		{
			
		}
		if (Slot2content == "Groundsmash")
		{
			checkGsmash(2);
				if (!Slot2cooldown)
				{
					GUI.color = Color.white;
					GUI.DrawTexture(secondslot, gsmashicon);	
				}
				if (Slot2cooldown)
				{
					GUI.color = cooldown;
					GUI.DrawTexture(secondslot, gsmashicon);
					GUI.color = Color.white;
					GUI.Label(new Rect(secondslot.x + 16, secondslot.y + 12, secondslot.width, secondslot.height), Mathf.Round(Slot2CD).ToString());
				}
		}
		if (Slot2content == "Tackle")
		{
			checkTackle(2);
				if (!Slot2cooldown)
				{
					GUI.color = Color.white;
					GUI.DrawTexture(secondslot, tackleicon);	
				}
				if (Slot2cooldown)
				{
					GUI.color = cooldown;
					GUI.DrawTexture(secondslot, tackleicon);
					GUI.color = Color.white;
					GUI.Label(new Rect(secondslot.x + 16, secondslot.y + 12, secondslot.width, secondslot.height), Mathf.Round(Slot2CD).ToString());
				}
			
		}
		if (Slot2content == "Force")
		{
			checkForce(2);
				if (!Slot2cooldown)
				{
					GUI.color = Color.white;
					GUI.DrawTexture(secondslot, forceicon);	
				}
				if (Slot2cooldown)
				{
					GUI.color = cooldown;
					GUI.DrawTexture(secondslot, forceicon);
					GUI.color = Color.white;
					GUI.Label(new Rect(secondslot.x + 16, secondslot.y + 12, secondslot.width, secondslot.height), Mathf.Round(Slot2CD).ToString());
				}
		}
		if (Slot2content == "Hurricane")
		{
			checkHurri(2);
			if (tutorialevent1)
			{
				GUI.depth = 0;
				if (flashing)
				GUI.DrawTexture(biggggslot, hurricon);
			}
				
			else
			{
				if (Slot2insuf)
				{
					GUI.color = insufficient;
					GUI.DrawTexture(secondslot, hurricon);
				}
				if (!Slot2cooldown && !Slot2insuf)
				{
					GUI.color = Color.white;
					GUI.DrawTexture(secondslot, hurricon);	
				}
				if (Slot2cooldown)
				{
					GUI.color = cooldown;
					GUI.DrawTexture(secondslot, hurricon);
					GUI.color = Color.white;
					GUI.Label(new Rect(secondslot.x + 16, secondslot.y + 12, secondslot.width, secondslot.height), Mathf.Round(Slot2CD).ToString());
				}
			}
		}
		if (Slot2content == "Lazer")
		{
			checklazer(2);
			if (Slot2insuf)
			{
				GUI.color = insufficient;
				GUI.DrawTexture(secondslot, beamicon);
			}
			if (!Slot2cooldown && !Slot2insuf)
			{
				GUI.color = Color.white;
				GUI.DrawTexture(secondslot, beamicon);	
			}
			if (Slot2cooldown)
			{
				GUI.color = cooldown;
				GUI.DrawTexture(secondslot, beamicon);
				GUI.color = Color.white;
				GUI.Label(new Rect(secondslot.x + 16, secondslot.y + 12, secondslot.width, secondslot.height), Mathf.Round(Slot2CD).ToString());
			}
		}
		if (Slot2content == "Spiritbomb")
		{
			checkSbomb(2);
			if (Slot2insuf)
			{
				GUI.color = insufficient;
				GUI.DrawTexture(secondslot, sbombicon);
			}
			if (!Slot2insuf)
			{
				GUI.color = Color.white;
				GUI.DrawTexture(secondslot, sbombicon);
			}
		}
		if (Slot2content == "Serenity")
		{
			checkSeren(2);
				if (Slot2insuf)
				{
					GUI.color = insufficient;
					GUI.DrawTexture(secondslot, sereniticon);
				}
				if (!Slot2cooldown && !Slot2insuf)
				{
					GUI.color = Color.white;
					GUI.DrawTexture(secondslot, sereniticon);	
				}
				if (Slot2cooldown)
				{
					GUI.color = cooldown;
					GUI.DrawTexture(secondslot, sereniticon);
					GUI.color = Color.white;
					GUI.Label(new Rect(secondslot.x + 16, secondslot.y + 12, secondslot.width, secondslot.height), Mathf.Round(Slot2CD).ToString());
				}
		}
		if (Slot2content == "Teledoor")
		{
				checktdoor(2);
				if (Slot2insuf)
				{
					GUI.color = insufficient;
					GUI.DrawTexture(secondslot, tdooricon);
				}
				if (!Slot2insuf)
				{
					GUI.color = Color.white;
					GUI.DrawTexture(secondslot, tdooricon);	
				}
//				if (Slot2cooldown)
//				{
//					GUI.color = cooldown;
//					GUI.DrawTexture(secondslot, tdooricon);
//					GUI.color = Color.white;
//					GUI.Label(new Rect(secondslot.x + 16, secondslot.y + 12, secondslot.width, secondslot.height), Mathf.Round(Slot2CD).ToString());
//				}
		}
		
		if (Slot2content == "Clone")
		{
				checkclone(2);
				if (Slot2insuf)
				{
					GUI.color = insufficient;
					GUI.DrawTexture(secondslot, cloneicon);
				}
				if (!Slot2cooldown && !Slot2insuf)
				{
					GUI.color = Color.white;
					GUI.DrawTexture(secondslot, cloneicon);	
				}
				if (Slot2cooldown)
				{
					GUI.color = cooldown;
					GUI.DrawTexture(secondslot, cloneicon);
					GUI.color = Color.white;
					GUI.Label(new Rect(secondslot.x + 16, secondslot.y + 12, secondslot.width, secondslot.height), Mathf.Round(Slot2CD).ToString());
				}
		}
		
		if (Slot2content == "Wall")
		{
				checkwall(2);
				if (!Slot2cooldown)
				{
					GUI.color = Color.white;
					GUI.DrawTexture(secondslot, wallicon);	
				}
				if (Slot2cooldown)
				{
					GUI.color = cooldown;
					GUI.DrawTexture(secondslot, wallicon);
					GUI.color = Color.white;
					GUI.Label(new Rect(secondslot.x + 16, secondslot.y + 12, secondslot.width, secondslot.height), Mathf.Round(Slot2CD).ToString());
				}
		}
		
		
		//Slot 3 Content
		if (Slot3content == null)
		{
			
		}
		if (Slot3content == "Groundsmash")
		{
			checkGsmash(3);
			if (!Slot3cooldown)
			{
				GUI.color = Color.white;
				GUI.DrawTexture(thirdslot, gsmashicon);	
			}
			if (Slot3cooldown)
			{
				GUI.color = cooldown;
				GUI.DrawTexture(thirdslot, gsmashicon);
				GUI.color = Color.white;
				GUI.Label(new Rect(thirdslot.x + 16, thirdslot.y + 12, thirdslot.width, thirdslot.height), Mathf.Round(Slot3CD).ToString());
			}
		}
		if (Slot3content == "Tackle")
		{
			checkTackle(3);
				if (!Slot3cooldown)
				{
					GUI.color = Color.white;
					GUI.DrawTexture(thirdslot, tackleicon);	
				}
				if (Slot3cooldown)
				{
					GUI.color = cooldown;
					GUI.DrawTexture(thirdslot, tackleicon);
					GUI.color = Color.white;
					GUI.Label(new Rect(thirdslot.x + 16, thirdslot.y + 12, thirdslot.width, thirdslot.height), Mathf.Round(Slot3CD).ToString());
				}
			
		}
		if (Slot3content == "Force")
		{
			checkForce(3);
				if (!Slot3cooldown)
				{
					GUI.color = Color.white;
					GUI.DrawTexture(thirdslot, forceicon);	
				}
				if (Slot3cooldown)
				{
					GUI.color = cooldown;
					GUI.DrawTexture(thirdslot, forceicon);
					GUI.color = Color.white;
					GUI.Label(new Rect(thirdslot.x + 16, thirdslot.y + 12, thirdslot.width, thirdslot.height), Mathf.Round(Slot3CD).ToString());
				}
		}
		if (Slot3content == "Hurricane")
		{
			checkHurri(3);
			if (Slot3insuf)
			{
				GUI.color = insufficient;
				GUI.DrawTexture(thirdslot, hurricon);
			}
			if (!Slot3cooldown && !Slot3insuf)
			{
				GUI.color = Color.white;
				GUI.DrawTexture(thirdslot, hurricon);	
			}
			if (Slot3cooldown)
			{
				GUI.color = cooldown;
				GUI.DrawTexture(thirdslot, hurricon);
				GUI.color = Color.white;
				GUI.Label(new Rect(thirdslot.x + 16, thirdslot.y + 12, thirdslot.width, thirdslot.height), Mathf.Round(Slot3CD).ToString());
			}
		}
		if (Slot3content == "Lazer")
		{
			checklazer(3);
			if (Slot3insuf)
			{
				GUI.color = insufficient;
				GUI.DrawTexture(thirdslot, beamicon);
			}
			if (!Slot3cooldown && !Slot3insuf)
			{
				GUI.color = Color.white;
				GUI.DrawTexture(thirdslot, beamicon);	
			}
			if (Slot3cooldown)
			{
				GUI.color = cooldown;
				GUI.DrawTexture(thirdslot, beamicon);
				GUI.color = Color.white;
				GUI.Label(new Rect(thirdslot.x + 16, thirdslot.y + 12, thirdslot.width, thirdslot.height), Mathf.Round(Slot3CD).ToString());
			}
			
		}
		if (Slot3content == "Spiritbomb")
		{
			checkSbomb(3);
			if (Slot3insuf)
			{
				GUI.color = insufficient;
				GUI.DrawTexture(thirdslot, sbombicon);
			}
			if (!Slot3insuf)
			{
				GUI.color = Color.white;
				GUI.DrawTexture(thirdslot, sbombicon);
			}
		}
		if (Slot3content == "Serenity")
		{
			checkSeren(3);
				if (Slot3insuf)
				{
					GUI.color = insufficient;
					GUI.DrawTexture(thirdslot, sereniticon);
				}
				if (!Slot3cooldown && !Slot3insuf)
				{
					GUI.color = Color.white;
					GUI.DrawTexture(thirdslot, sereniticon);	
				}
				if (Slot3cooldown)
				{
					GUI.color = cooldown;
					GUI.DrawTexture(thirdslot, sereniticon);
					GUI.color = Color.white;
					GUI.Label(new Rect(thirdslot.x + 16, thirdslot.y + 12, thirdslot.width, thirdslot.height), Mathf.Round(Slot3CD).ToString());
				}
		}
		if (Slot3content == "Teledoor")
		{
				checktdoor(3);
				if (Slot3insuf)
				{
					GUI.color = insufficient;
					GUI.DrawTexture(thirdslot, tdooricon);
				}
				if (!Slot3insuf)
				{
					GUI.color = Color.white;
					GUI.DrawTexture(thirdslot, tdooricon);	
				}
		}
		
		if (Slot3content == "Clone")
		{
				checkclone(3);
				if (Slot3insuf)
				{
					GUI.color = insufficient;
					GUI.DrawTexture(thirdslot, cloneicon);
				}
				if (!Slot3cooldown && !Slot3insuf)
				{
					GUI.color = Color.white;
					GUI.DrawTexture(thirdslot, cloneicon);	
				}
				if (Slot3cooldown)
				{
					GUI.color = cooldown;
					GUI.DrawTexture(thirdslot, cloneicon);
					GUI.color = Color.white;
					GUI.Label(new Rect(thirdslot.x + 16, thirdslot.y + 12, thirdslot.width, thirdslot.height), Mathf.Round(Slot3CD).ToString());
				}
		}
		
		if (Slot3content == "Wall")
		{
				checkwall(3);
				if (!Slot3cooldown)
				{
					GUI.color = Color.white;
					GUI.DrawTexture(thirdslot, wallicon);	
				}
				if (Slot3cooldown)
				{
					GUI.color = cooldown;
					GUI.DrawTexture(thirdslot, wallicon);
					GUI.color = Color.white;
					GUI.Label(new Rect(thirdslot.x + 16, thirdslot.y + 12, thirdslot.width, thirdslot.height), Mathf.Round(Slot3CD).ToString());
				}
		}
		
		
		//Slot 4 Content
		if (Slot4content == null)
		{
			
		}
		if (Slot4content == "Groundsmash")
		{
			checkGsmash(4);
				if (!Slot4cooldown)
				{
					GUI.color = Color.white;
					GUI.DrawTexture(fourthslot, gsmashicon);	
				}
				if (Slot4cooldown)
				{
					GUI.color = cooldown;
					GUI.DrawTexture(fourthslot, gsmashicon);
					GUI.color = Color.white;
					GUI.Label(new Rect(fourthslot.x + 16, fourthslot.y + 12, fourthslot.width, fourthslot.height), Mathf.Round(Slot4CD).ToString());
				}	
		}
		if (Slot4content == "Tackle")
		{
			checkTackle(4);
				if (!Slot4cooldown)
				{
					GUI.color = Color.white;
					GUI.DrawTexture(fourthslot, tackleicon);	
				}
				if (Slot4cooldown)
				{
					GUI.color = cooldown;
					GUI.DrawTexture(fourthslot, tackleicon);
					GUI.color = Color.white;
					GUI.Label(new Rect(fourthslot.x + 16, fourthslot.y + 12, fourthslot.width, fourthslot.height), Mathf.Round(Slot4CD).ToString());
				}
			
		}
		if (Slot4content == "Force")
		{
			checkForce(4);
				if (!Slot4cooldown)
				{
					GUI.color = Color.white;
					GUI.DrawTexture(fourthslot, forceicon);	
				}
				if (Slot4cooldown)
				{
					GUI.color = cooldown;
					GUI.DrawTexture(fourthslot, forceicon);
					GUI.color = Color.white;
					GUI.Label(new Rect(fourthslot.x + 16, fourthslot.y + 12, fourthslot.width, fourthslot.height), Mathf.Round(Slot4CD).ToString());
				}
		}
		if (Slot4content == "Hurricane")
		{
			checkHurri(4);
			if (Slot4insuf)
			{
				GUI.color = insufficient;
				GUI.DrawTexture(fourthslot, hurricon);
			}
			if (!Slot4cooldown && !Slot4insuf)
			{
				GUI.color = Color.white;
				GUI.DrawTexture(fourthslot, hurricon);	
			}
			if (Slot4cooldown)
			{
				GUI.color = cooldown;
				GUI.DrawTexture(fourthslot, hurricon);
				GUI.color = Color.white;
				GUI.Label(new Rect(fourthslot.x + 16, fourthslot.y + 12, fourthslot.width, fourthslot.height), Mathf.Round(Slot4CD).ToString());
			}
		}
		if (Slot4content == "Lazer")
		{
			checklazer(4);
			if (Slot4insuf)
			{
				GUI.color = insufficient;
				GUI.DrawTexture(fourthslot, beamicon);
			}
			if (!Slot4cooldown && !Slot4insuf)
			{
				GUI.color = Color.white;
				GUI.DrawTexture(fourthslot, beamicon);	
			}
			if (Slot4cooldown)
			{
				GUI.color = cooldown;
				GUI.DrawTexture(fourthslot, beamicon);
				GUI.color = Color.white;
				GUI.Label(new Rect(fourthslot.x + 16, fourthslot.y + 12, fourthslot.width, fourthslot.height), Mathf.Round(Slot4CD).ToString());
			}
		}
		if (Slot4content == "Spiritbomb")
		{
			checkSbomb(4);
			if (Slot4insuf)
			{
				GUI.color = insufficient;
				GUI.DrawTexture(fourthslot, sbombicon);
			}
			if (!Slot4insuf)
			{
				GUI.color = Color.white;
				GUI.DrawTexture(fourthslot, sbombicon);
			}
		}
		if (Slot4content == "Serenity")
		{
			checkSeren(4);
				if (Slot4insuf)
				{
					GUI.color = insufficient;
					GUI.DrawTexture(fourthslot, sereniticon);
				}
				if (!Slot4cooldown && !Slot4insuf)
				{
					GUI.color = Color.white;
					GUI.DrawTexture(fourthslot, sereniticon);	
				}
				if (Slot4cooldown)
				{
					GUI.color = cooldown;
					GUI.DrawTexture(fourthslot, sereniticon);
					GUI.color = Color.white;
					GUI.Label(new Rect(fourthslot.x + 16, fourthslot.y + 12, fourthslot.width, fourthslot.height), Mathf.Round(Slot4CD).ToString());
				}
		}
		if (Slot4content == "Teledoor")
		{
				checktdoor(4);
				if (Slot4insuf)
				{
					GUI.color = insufficient;
					GUI.DrawTexture(fourthslot, tdooricon);
				}
				if (!Slot4insuf)
				{
					GUI.color = Color.white;
					GUI.DrawTexture(fourthslot, tdooricon);	
				}
//				if (Slot4cooldown)
//				{
//					GUI.color = cooldown;
//					GUI.DrawTexture(fourthslot, tdooricon);
//					GUI.color = Color.white;
//					GUI.Label(new Rect(fourthslot.x + 16, fourthslot.y + 12, fourthslot.width, fourthslot.height), Mathf.Round(Slot4CD).ToString());
//				}
		}
		
		if (Slot4content == "Clone")
		{
				checkclone(4);
				if (Slot4insuf)
				{
					GUI.color = insufficient;
					GUI.DrawTexture(fourthslot, cloneicon);
				}
				if (!Slot4cooldown && !Slot4insuf)
				{
					GUI.color = Color.white;
					GUI.DrawTexture(fourthslot, cloneicon);	
				}
				if (Slot4cooldown)
				{
					GUI.color = cooldown;
					GUI.DrawTexture(fourthslot, cloneicon);
					GUI.color = Color.white;
					GUI.Label(new Rect(fourthslot.x + 16, fourthslot.y + 12, fourthslot.width, fourthslot.height), Mathf.Round(Slot4CD).ToString());
				}
		}
		
		if (Slot4content == "Wall")
		{
				checkwall(4);
				if (!Slot4cooldown)
				{
					GUI.color = Color.white;
					GUI.DrawTexture(fourthslot, wallicon);	
				}
				if (Slot4cooldown)
				{
					GUI.color = cooldown;
					GUI.DrawTexture(fourthslot, wallicon);
					GUI.color = Color.white;
					GUI.Label(new Rect(fourthslot.x + 16, fourthslot.y + 12, fourthslot.width, fourthslot.height), Mathf.Round(Slot4CD).ToString());
				}
			}

		}//playerscript
		
	}// END ONGUI
	
	void checkGsmash (int slotnumber)
	{
		if (playerscript.gSmashready)
		{
			if (slotnumber == 1)
			{
				Slot1cooldown = false;
				S1CDfound = false;
			}
			if (slotnumber == 2)
			{
				Slot2cooldown = false;
				S2CDfound = false;
			}
			if (slotnumber == 3)
			{
				Slot3cooldown = false;
				S3CDfound = false;
			}
			if (slotnumber == 4)
			{
				Slot4cooldown = false;
				S4CDfound = false;
			}
		}
		if (!playerscript.gSmashready)
		{
			if (slotnumber == 1)
			{
				if (!S1CDfound)
				FindSlot(slotnumber, playerscript.tackleCDbase);
			}
			if (slotnumber == 2)
			{
				if (!S2CDfound)
				FindSlot(slotnumber, playerscript.tackleCDbase);
			}
			if (slotnumber == 3)
			{
				if (!S3CDfound)
				FindSlot(slotnumber, playerscript.tackleCDbase);
			}
			if (slotnumber == 4)
			{
				if (!S4CDfound)
				FindSlot(slotnumber, playerscript.tackleCDbase);
			}
			
		}		
	}
	
	void checkTackle (int slotnumber)
	{
		if (playerscript.tackleready)
		{
			if (slotnumber == 1)
			{
				Slot1cooldown = false;
				S1CDfound = false;
			}
			if (slotnumber == 2)
			{
				Slot2cooldown = false;
				S2CDfound = false;
			}
			if (slotnumber == 3)
			{
				Slot3cooldown = false;
				S3CDfound = false;
			}
			if (slotnumber == 4)
			{
				Slot4cooldown = false;
				S4CDfound = false;
			}
		}
		if (!playerscript.tackleready)
		{
			if (slotnumber == 1)
			{
				if (!S1CDfound)
				FindSlot(slotnumber, playerscript.tackleCDbase);
			}
			if (slotnumber == 2)
			{
				if (!S2CDfound)
				FindSlot(slotnumber, playerscript.tackleCDbase);
			}
			if (slotnumber == 3)
			{
				if (!S3CDfound)
				FindSlot(slotnumber, playerscript.tackleCDbase);
			}
			if (slotnumber == 4)
			{
				if (!S4CDfound)
				FindSlot(slotnumber, playerscript.tackleCDbase);
			}
			
		}		
	}
	
	void checkForce (int slotnumber)
	{
		if (playerscript.forceready)
		{
			if (slotnumber == 1)
			{
				Slot1cooldown = false;
				S1CDfound = false;
			}
			if (slotnumber == 2)
			{
				Slot2cooldown = false;
				S2CDfound = false;
			}
			if (slotnumber == 3)
			{
				Slot3cooldown = false;
				S3CDfound = false;
			}
			if (slotnumber == 4)
			{
				Slot4cooldown = false;
				S4CDfound = false;
			}
		}
		if (!playerscript.forceready)
		{
			if (slotnumber == 1)
			{
				if (!S1CDfound)
				FindSlot(slotnumber, playerscript.forceCDbase);
			}
			if (slotnumber == 2)
			{
				if (!S2CDfound)
				FindSlot(slotnumber, playerscript.forceCDbase);
			}
			if (slotnumber == 3)
			{
				if (!S3CDfound)
				FindSlot(slotnumber, playerscript.forceCDbase);
			}
			if (slotnumber == 4)
			{
				if (!S4CDfound)
				FindSlot(slotnumber, playerscript.forceCDbase);
			}
			
		}		
	}
	
	void checkHurri (int slotnumber)
	{
		if (playerscript.balls < 3)
		{
			if (slotnumber == 1)
			{
				Slot1insuf = true;
			}
			if (slotnumber == 2)
			{
				Slot2insuf = true;
			}
			if (slotnumber == 3)
			{
				Slot3insuf = true;
			}
			if (slotnumber == 4)
			{
				Slot4insuf = true;
			}

		}
		if (playerscript.hurriready && playerscript.balls >= 3)
		{
			if (slotnumber == 1)
			{
				Slot1insuf = false;
				Slot1cooldown = false;
				S1CDfound = false;
			}
			if (slotnumber == 2)
			{
				Slot2insuf = false;
				Slot2cooldown = false;
				S2CDfound = false;
			}
			if (slotnumber == 3)
			{
				Slot3insuf = false;
				Slot3cooldown = false;
				S3CDfound = false;
			}
			if (slotnumber == 4)
			{
				Slot4insuf = false;
				Slot4cooldown = false;
				S4CDfound = false;
			}
		}
		if (!playerscript.hurriready)
		{
			if (slotnumber == 1)
			{
				if (!S1CDfound)
				FindSlot(slotnumber, playerscript.HurrCDbase);
			}
			if (slotnumber == 2)
			{
				if (!S2CDfound)
				FindSlot(slotnumber, playerscript.HurrCDbase);
			}
			if (slotnumber == 3)
			{
				if (!S3CDfound)
				FindSlot(slotnumber, playerscript.HurrCDbase);
			}
			if (slotnumber == 4)
			{
				if (!S4CDfound)
				FindSlot(slotnumber, playerscript.HurrCDbase);
			}
			
		}	
	}
	
	void checklazer(int slotnumber)
	{
		if (playerscript.balls < 4)
		{
			if (slotnumber == 1)
			{
				Slot1insuf = true;
			}
			if (slotnumber == 2)
			{
				Slot2insuf = true;
			}
			if (slotnumber == 3)
			{
				Slot3insuf = true;
			}
			if (slotnumber == 4)
			{
				Slot4insuf = true;
			}
		}
		if (playerscript.beamready && playerscript.balls >= 4)
		{
			if (slotnumber == 1)
			{
				Slot1insuf = false;
				Slot1cooldown = false;
				S1CDfound = false;
			}
			if (slotnumber == 2)
			{
				Slot2insuf = false;
				Slot2cooldown = false;
				S2CDfound = false;
			}
			if (slotnumber == 3)
			{
				Slot3insuf = false;
				Slot3cooldown = false;
				S3CDfound = false;
			}
			if (slotnumber == 4)
			{
				Slot4insuf = false;
				Slot4cooldown = false;
				S4CDfound = false;
			}
		}
		if (!playerscript.beamready)
		{
			if (slotnumber == 1)
			{
				if (!S1CDfound)
				FindSlot(slotnumber, playerscript.LazerCDreal);
			}
			if (slotnumber == 2)
			{
				if (!S2CDfound)
				FindSlot(slotnumber, playerscript.LazerCDreal);
			}
			if (slotnumber == 3)
			{
				if (!S3CDfound)
				FindSlot(slotnumber, playerscript.LazerCDreal);
			}
			if (slotnumber == 4)
			{
				if (!S4CDfound)
				FindSlot(slotnumber, playerscript.LazerCDreal);
			}
			
		}
	}
	
	void checkSbomb(int slotnumber)
	{
		if (playerscript.balls < 1)
		{
			if (slotnumber == 1)
				Slot1insuf = true;
			
			if (slotnumber == 2)
				Slot2insuf = true;
		
			if (slotnumber == 3)
				Slot3insuf = true;
			
			if (slotnumber == 4)
				Slot4insuf = true;
		
		}
		if (playerscript.balls >= 1)
		{
			if (slotnumber == 1)
				Slot1insuf = false;
			
			if (slotnumber == 2)
				Slot2insuf = false;
			
			if (slotnumber == 3)
				Slot3insuf = false;
			
			if (slotnumber == 4)
				Slot4insuf = false;
		}
	}
	
	void checkSeren(int slotnumber)
	{
		if (playerscript.balls < 4 || GameObject.FindGameObjectWithTag("Barrier") != null)
		{
			if (slotnumber == 1)
				Slot1insuf = true;
			
			if (slotnumber == 2)
				Slot2insuf = true;
		
			if (slotnumber == 3)
				Slot3insuf = true;
			
			if (slotnumber == 4)
				Slot4insuf = true;
		
		}
		
		if (playerscript.barrierready && playerscript.balls >= 4)
		{
			if (slotnumber == 1)
			{
				Slot1cooldown = false;
				S1CDfound = false;
				Slot1insuf = false;
			}
			if (slotnumber == 2)
			{
				Slot2cooldown = false;
				S2CDfound = false;
				Slot2insuf = false;
			}
			if (slotnumber == 3)
			{
				Slot3cooldown = false;
				S3CDfound = false;
				Slot3insuf = false;
			}
			if (slotnumber == 4)
			{
				Slot4cooldown = false;
				S4CDfound = false;
				Slot4insuf = false;
			}
		}
		if (!playerscript.barrierready)
		{
			if (slotnumber == 1)
			{
				if (!S1CDfound)
				FindSlot(slotnumber, playerscript.serenCDbase);
			}
			if (slotnumber == 2)
			{
				if (!S2CDfound)
				FindSlot(slotnumber, playerscript.serenCDbase);
			}
			if (slotnumber == 3)
			{
				if (!S3CDfound)
				FindSlot(slotnumber, playerscript.serenCDbase);
			}
			if (slotnumber == 4)
			{
				if (!S4CDfound)
				FindSlot(slotnumber, playerscript.serenCDbase);
			}
		}
	
	}
	
	void checktdoor(int slotnumber)
	{
		if (playerscript.balls < 1)
		{
			if (slotnumber == 1)
				Slot1insuf = true;
			
			if (slotnumber == 2)
				Slot2insuf = true;
		
			if (slotnumber == 3)
				Slot3insuf = true;
			
			if (slotnumber == 4)
				Slot4insuf = true;
		
		}
		
		if (playerscript.balls >= 1)
		{
			if (slotnumber == 1)
			{
				Slot1cooldown = false;
				S1CDfound = false;
				Slot1insuf = false;
			}
			if (slotnumber == 2)
			{
				Slot2cooldown = false;
				S2CDfound = false;
				Slot2insuf = false;
			}
			if (slotnumber == 3)
			{
				Slot3cooldown = false;
				S3CDfound = false;
				Slot3insuf = false;
			}
			if (slotnumber == 4)
			{
				Slot4cooldown = false;
				S4CDfound = false;
				Slot4insuf = false;
			}
		}
	
	}
	
	void checkclone(int slotnumber)
	{
		if (playerscript.balls < 1.25f)
		{
			if (slotnumber == 1)
				Slot1insuf = true;
			
			if (slotnumber == 2)
				Slot2insuf = true;
		
			if (slotnumber == 3)
				Slot3insuf = true;
			
			if (slotnumber == 4)
				Slot4insuf = true;
		
		}
		if (playerscript.balls >= 1.25f)
		{
			if (slotnumber == 1)
			{
				Slot1cooldown = false;
				S1CDfound = false;
				Slot1insuf = false;
			}
			if (slotnumber == 2)
			{
				Slot2cooldown = false;
				S2CDfound = false;
				Slot2insuf = false;
			}
			if (slotnumber == 3)
			{
				Slot3cooldown = false;
				S3CDfound = false;
				Slot3insuf = false;
			}
			if (slotnumber == 4)
			{
				Slot4cooldown = false;
				S4CDfound = false;
				Slot4insuf = false;
			}
		}
	
	}
	
	void checkwall(int slotnumber)
	{
		if (playerscript.balls > 0)
		{
			if (slotnumber == 1)
			{
				Slot1cooldown = false;
				S1CDfound = false;
			}
			if (slotnumber == 2)
			{
				Slot2cooldown = false;
				S2CDfound = false;
			}
			if (slotnumber == 3)
			{
				Slot3cooldown = false;
				S3CDfound = false;
			}
			if (slotnumber == 4)
			{
				Slot4cooldown = false;
				S4CDfound = false;
			}
		}
		if (playerscript.balls <= 0)
		{
			if (slotnumber == 1)
				Slot1insuf = true;
			
			if (slotnumber == 2)
				Slot2insuf = true;
		
			if (slotnumber == 3)
				Slot3insuf = true;
			
			if (slotnumber == 4)
				Slot4insuf = true;
		}
	
	}
	
	void checkSstrike(int slotnumber)
	{
		if (playerscript.balls > 0)
		{
			if (slotnumber == 1)
			{
				Slot1cooldown = false;
				S1CDfound = false;
			}
			if (slotnumber == 2)
			{
				Slot2cooldown = false;
				S2CDfound = false;
			}
			if (slotnumber == 3)
			{
				Slot3cooldown = false;
				S3CDfound = false;
			}
			if (slotnumber == 4)
			{
				Slot4cooldown = false;
				S4CDfound = false;
			}
		}
		if (playerscript.balls <= 0)
		{
			if (slotnumber == 1)
				Slot1insuf = true;
			
			if (slotnumber == 2)
				Slot2insuf = true;
		
			if (slotnumber == 3)
				Slot3insuf = true;
			
			if (slotnumber == 4)
				Slot4insuf = true;
		}
	
	}
	
	void FindSlot(int slotnumber, float CDassign)
	{
		if (slotnumber == 1)
		{
			Slot1CD = CDassign;
			Slot1cooldown = true;
			S1CDfound = true;
		}
		
		if (slotnumber == 2)
		{
			Slot2CD = CDassign;
			Slot2cooldown = true;
			S2CDfound = true;
		}
		
		if (slotnumber == 3)
		{
			Slot3CD = CDassign;
			Slot3cooldown = true;
			S3CDfound = true;
		}
		
		if (slotnumber == 4)
		{
			Slot4CD = CDassign;
			Slot4cooldown = true;
			S4CDfound = true;
		}
	}
	
	IEnumerator Zoomzoom()
	{
		tutorialevent1 = true;
		
		yield return new WaitForSeconds(4);
		
		tutorialevent1 = false;
		
	}
	
	IEnumerator Zoomzoom2()
	{
		tutorialevent2 = true;
		
		yield return new WaitForSeconds(4);
		
		tutorialevent2 = false;
		
	}
	
	public void Zoomevent()
	{
		StartCoroutine ( Zoomzoom () );	
	}
	
	public void Zoomevent2()
	{
		StartCoroutine ( Zoomzoom2 () );	
	}
	
	
}
