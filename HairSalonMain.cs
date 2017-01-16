using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HairSalonMain : MonoBehaviour {

	public int health, strength, speed, weight, chiballs;
	
	//window stuff
	private bool AfroOn = false;
	private Rect UpgradeWindow = new Rect(15, 15, 935, 615);
	private Rect Chipswindow = new Rect(950, 15, 75, 562);
	private Rect afrobg = new Rect(5, 0, 800, 600);
	private Rect afrobgdefault = new Rect(5, 25, 768, 512);
	private Rect messages = new Rect(5, 40, 200, 50);
	private Rect messages2 = new Rect(500, 40, 200, 50);
	private Vector3 mspos;
	
	public GUIStyle mystyle;
	
	
	
	//actions
	private bool square1textureon = false;
	private bool speedon = false, strengthon = false, viton = false;
	
	//import media
	public Texture2D afrohead, up, down, left, right, spd, str, vit, spdwave, spdwaveright, spdwaveup, spdwaveleft, strwave, vitwave, gen;
	private Texture2D textureinsidemouse, blank;
	private Texture2D [] texturestoapply = new Texture2D[50];
	
	
	//other
	private bool helpmsg = false, wavemsg = false, removemsg = false;
	private string clickedon;
	private Vector2 nodeinquestion;
	private int texturenumber;
	private Color transparent = new Color(255, 255, 255, 0.7f);
	private string[,] gridcontent = new string[9,11];
	private Rect[,] invisiblewaverects = new Rect[9,11];
	private List<Vector2> spdnodes = new List<Vector2>(), strnodes = new List<Vector2>(), vitnodes = new List<Vector2>();
	// it goes (columns, rows);
	
	
	void Start () {
		
		health = PlayerPrefs.GetInt("Health");
		strength = PlayerPrefs.GetInt("Strength");
		speed = PlayerPrefs.GetInt("Speed");
		
		//declaring static grid content
		setupstaticgridnodes();
		
		
		//generate table for invisirects
		generateinvisiblerects();
		
	
	}
	
	// Update is called once per frame
	void Update () {
		
	
//		if (Input.GetButtonDown("fight"))
//		{
//			if (rdytoputshiton)
//				rdytoputshiton = false;
//		}
		
	}
	
	void OnGUI ()
	{
		GUI.color = Color.black;
		GUI.Label(new Rect(0, 160, 200, 100), "3,1 content == " + gridcontent[3,1]);
//		GUI.Label(new Rect(0, 180, 200, 100), "spdnodeslist:  " + spdnodes[0].ToString());
		GUI.Label(new Rect(0, 200, 200, 100), "square1 == " + square1textureon.ToString());
//		GUI.Label(new Rect(0, 180, 200, 100), "mouselocation == " + Input.mousePosition.ToString());
		GUI.Label(new Rect(Screen.width / 2 - 200, 0, 300, 100), "Well hello there, Ricky! Welcome back!\n What kind of treatment would" +
			"you like today, hmm?", "box");
//		GUI.Label(new Rect(Screen.width / 2 - 185, 20, 200, 150), "What kind of treatment would you like today, hmm?", "box");
		
//		GUI.Label(new Rect(Screen.width / 2 - 100, 200, 200, 150), "Health: " + health.ToString());
//		GUI.Label(new Rect(Screen.width / 2 - 100, 230, 200, 150), "Strength: " + strength.ToString());
		
		
		GUI.color = Color.black;
//		//Vit UPGRADES
//		if (GUI.Button(new Rect(Screen.width / 2 - 270, 200, 150, 25), "+ 1 Health"))
//		{
//			health++;
//			PlayerPrefs.SetInt("Health", health);
//		}
//		
//		//STR upgrades
//		if (GUI.Button(new Rect(Screen.width / 2 - 270, 230, 150, 25), "+ 1 Strength"))
//		{
//			strength++;
//			PlayerPrefs.SetInt("Strength", strength);
//		}
		
		if (GUI.Button(new Rect(Screen.width / 2 - 200, 100, 170, 25), "Configure Hair Style"))
		{
			AfroOn = !AfroOn;
		}
		
		if (AfroOn)
		{
			GUI.color = Color.white;
			GUI.Window(1, UpgradeWindow, AfroUpgrade, "Afro Upgrade");
			
		}
		
		
		GUI.color = Color.white;
		if (GUI.Button(new Rect(10, Screen.height - 70, 120, 25), "Level Select"))
		{
			Application.LoadLevel("LevelSelect");
		}
		
		if (GUI.Button(new Rect(10, Screen.height - 40, 120, 25), "Visit Dojo"))
		{
			Application.LoadLevel("Dojo");
		}
		
		if (GUI.Button(new Rect(Screen.width - 140, Screen.height - 40, 120, 25), "> Next Level"))
		{
			Application.LoadLevel("Level4");
		}
		
		
	}
			
	void AfroUpgrade(int windowID)
	{
		GUI.color = Color.white;
		GUI.DrawTexture(afrobg, afrohead, ScaleMode.ScaleToFit, true, 0f);		
		GUI.Window (2, Chipswindow, Chips, "Inventory");	
		
		thegrid();
		
		
		
		if (helpmsg)
		{
			wavemsg = false;
			GUI.color = Color.black;
			GUI.Label(messages, "Click where you like to put " + clickedon + " node on your face");
		}
		
		if (wavemsg)
		{
			helpmsg = false;
			GUI.color = Color.black;
			GUI.Label(messages, "Click where you like to modify power wave to go " + clickedon + " on your face");
		}
		
		if (removemsg)
		{
			helpmsg = false;
			wavemsg = false;
			GUI.color = Color.black;
			GUI.Label(messages2, "Remove this stat chip?");
			if (GUI.Button(new Rect(500, 60, 50, 25), "Yes"))
			{
				removemsg = false;
				gridcontent[(int)nodeinquestion.x, (int)nodeinquestion.y] = null;
				texturestoapply[texturenumber] = null;
			}
			
			if (GUI.Button(new Rect(560, 60, 50, 25), "Yes"))
			{
				removemsg = false;
				
			}
		}
			
		
		GUI.color = Color.white;
		GUI.Button(new Rect(393, 125, 23, 23), gen, mystyle);
		
		
		
		//inputs
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			AfroOn = false;	
		}
		
		if (Input.GetAxis("Vertical") > 0)
		{
			afrobg.width++;
			afrobg.height++;
		}
		
		if (Input.GetAxis("Vertical") < 0)
		{
			afrobg.width--;
			afrobg.height--;

			if (afrobg.width < 768)
				afrobg.width = 768;
			if (afrobg.height < 512)
				afrobg.height = 512;
		}
		
		if (Input.GetButtonDown("fight2"))
		{
			Clearallmouseshit();
		}
	
		graphicsforwave();
		
				
	}
	
	void Chips(int windowID)
	{
		GUI.color = Color.white;
		
		if (GUI.Button(new Rect(5, 20, 52, 52), spd))
		{
			textureinsidemouse = spd;
			speedon = true;
			clickedon = "spd";
			helpmsg = true;
		}
		
		if (GUI.Button(new Rect(5, 72, 52, 52), str))
		{
			textureinsidemouse = str;
			strengthon = true;
			clickedon = "STR";
			helpmsg = true;
		}
		
		if (GUI.Button(new Rect(5, 124, 52, 52), vit))
		{
			textureinsidemouse = vit;
			viton = true;
			clickedon = "VIT";
			helpmsg = true;
		}
		
		if (GUI.Button(new Rect(5, 176, 52, 52), up))
		{
			clickedon = "upwards";
			wavemsg = true;
		}
		
		if (GUI.Button(new Rect(5, 228, 52, 52), down))
		{
			clickedon = "downwards";
			wavemsg = true;
		}
		
		if (GUI.Button(new Rect(5, 280, 52, 52), left))
		{
			clickedon = "leftwards";
			wavemsg = true;
		}
		
		if (GUI.Button(new Rect(5, 332, 52, 52), left))
		{
			clickedon = "rightwards";
			wavemsg = true;
		}
		
//		GUI.DrawTexture(new Rect(5, 120, 52, 52), right, ScaleMode.ScaleToFit, true, 0f);
				
	}
	
	void thegrid()
	{
		if (GUI.Button(new Rect(314, 126, 22, 22), texturestoapply[0], mystyle))
		{
			if (Event.current.button == 1 && texturestoapply[0] != null)
			{
				Storepotential(new Vector2(1,1), 0);
				removemsg = true;
			}
			if (clickedon == null)
				return;
			if (clickedon == "upwards" || clickedon == "downwards" || clickedon == "leftwards" || clickedon == "rightwards")
			{
				Changewave(clickedon, 1, 1);
				updateAdjacentnodes(1,1);
				Clearallmouseshit();
				return;	
			}
			texturestoapply[0] = textureinsidemouse;
			gridcontent[1,1] = clickedon;
			checkAdjacentnodes(1, 1);
			Chainreaction();
		}
		if (GUI.Button(new Rect(341, 126, 22, 22), texturestoapply[1], mystyle))
		{
			if (Event.current.button == 1 && texturestoapply[1] != null)
			{
				Storepotential(new Vector2(2,1), 1);
				removemsg = true;
			}
			if (clickedon == null)
				return;
			if (clickedon == "upwards" || clickedon == "downwards" || clickedon == "leftwards" || clickedon == "rightwards")
			{
				Changewave(clickedon, 2, 1);
				updateAdjacentnodes(2,1);
				Clearallmouseshit();
				return;	
			}
			texturestoapply[1] = textureinsidemouse;
			gridcontent[2,1] = clickedon;
			checkAdjacentnodes(2, 1);
			Chainreaction();
		}
		if (GUI.Button(new Rect(368, 126, 22, 22), texturestoapply[2], mystyle))
		{
			if (Event.current.button == 1 && texturestoapply[2] != null)
			{
				Storepotential(new Vector2(3,1), 2);
				removemsg = true;
			}
			if (clickedon == null)
				return;
			if (clickedon == "upwards" || clickedon == "downwards" || clickedon == "leftwards" || clickedon == "rightwards")
			{
				Changewave(clickedon, 3, 1);
				updateAdjacentnodes(3,1);
				Clearallmouseshit();
				return;	
			}
			texturestoapply[2] = textureinsidemouse;
			gridcontent[3,1] = clickedon;
			checkAdjacentnodes(3, 1);
			Chainreaction();
		}
		if (GUI.Button(new Rect(421, 126, 22, 22), texturestoapply[3], mystyle))
		{
			if (Event.current.button == 1 && texturestoapply[3] != null)
			{
				Storepotential(new Vector2(5,1), 3);
				removemsg = true;
			}
			if (clickedon == null)
				return;
			if (clickedon == "upwards" || clickedon == "downwards" || clickedon == "leftwards" || clickedon == "rightwards")
			{
				Changewave(clickedon, 5, 1);
				updateAdjacentnodes(5,1);
				Clearallmouseshit();
				return;	
			}
			texturestoapply[3] = textureinsidemouse;
			gridcontent[5,1] = clickedon;
			checkAdjacentnodes(5, 1);
		}
		if (GUI.Button(new Rect(447, 126, 22, 22), texturestoapply[4], mystyle))
		{
			if (Event.current.button == 1 && texturestoapply[4] != null)
			{
				Storepotential(new Vector2(6,1), 4);
				removemsg = true;
			}
			if (clickedon == null)
				return;
			if (clickedon == "upwards" || clickedon == "downwards" || clickedon == "leftwards" || clickedon == "rightwards")
			{
				Changewave(clickedon, 6, 1);
				updateAdjacentnodes(6,1);
				Clearallmouseshit();
				return;	
			}
			texturestoapply[4] = textureinsidemouse;
			gridcontent[6,1] = clickedon;
			checkAdjacentnodes(6, 1);
		}
		if (GUI.Button(new Rect(474, 126, 22, 22), texturestoapply[5], mystyle))
		{
			if (Event.current.button == 1 && texturestoapply[5] != null)
			{
				Storepotential(new Vector2(7,1), 5);
				removemsg = true;
			}
			if (clickedon == null)
				return;
			if (clickedon == "upwards" || clickedon == "downwards" || clickedon == "leftwards" || clickedon == "rightwards")
			{
				Changewave(clickedon, 7, 1);
				updateAdjacentnodes(7,1);
				Clearallmouseshit();
				return;	
			}
			texturestoapply[5] = textureinsidemouse;
			gridcontent[7,1] = clickedon;
			checkAdjacentnodes(7, 1);
		}
		
		if (GUI.Button(new Rect(341, 153, 22, 22), texturestoapply[6], mystyle))
		{
			if (Event.current.button == 1 && texturestoapply[6] != null)
			{
				Storepotential(new Vector2(2,2), 6);
				removemsg = true;
			}
			if (clickedon == null)
				return;
			if (clickedon == "upwards" || clickedon == "downwards" || clickedon == "leftwards" || clickedon == "rightwards")
			{
				Changewave(clickedon, 2, 2);
				updateAdjacentnodes(2,2);
				Clearallmouseshit();
				return;	
			}
			texturestoapply[6] = textureinsidemouse;
			gridcontent[2,2] = clickedon;
			checkAdjacentnodes(2, 2);
		}
		
		if (GUI.Button(new Rect(368, 153, 22, 22), texturestoapply[7], mystyle))
		{
			if (Event.current.button == 1 && texturestoapply[7] != null)
			{
				Storepotential(new Vector2(3,2), 7);
				removemsg = true;
			}
			if (clickedon == null)
				return;
			if (clickedon == "upwards" || clickedon == "downwards" || clickedon == "leftwards" || clickedon == "rightwards")
			{
				Changewave(clickedon, 3, 2);
				updateAdjacentnodes(3,2);
				Clearallmouseshit();
				return;	
			}
			texturestoapply[7] = textureinsidemouse;
			gridcontent[3,2] = clickedon;
			checkAdjacentnodes(3, 2);
		}
		
		if (GUI.Button(new Rect(394, 153, 22, 22), texturestoapply[8], mystyle))
		{
			if (Event.current.button == 1 && texturestoapply[8] != null)
			{
				Storepotential(new Vector2(4,2), 8);
				removemsg = true;
			}
			if (clickedon == null)
				return;
			if (clickedon == "upwards" || clickedon == "downwards" || clickedon == "leftwards" || clickedon == "rightwards")
			{
				Changewave(clickedon, 4, 2);
				updateAdjacentnodes(4,2);
				Clearallmouseshit();
				return;	
			}
			texturestoapply[8] = textureinsidemouse;
			gridcontent[4,2] = clickedon;
			checkAdjacentnodes(4, 2);
		}
		
		if (GUI.Button(new Rect(421, 153, 22, 22), texturestoapply[9], mystyle))
		{
			if (Event.current.button == 1 && texturestoapply[9] != null)
			{
				Storepotential(new Vector2(5,2), 9);
				removemsg = true;
			}
			if (clickedon == null)
				return;
			if (clickedon == "upwards" || clickedon == "downwards" || clickedon == "leftwards" || clickedon == "rightwards")
			{
				Changewave(clickedon, 5, 2);
				updateAdjacentnodes(5,2);
				Clearallmouseshit();
				return;	
			}
			texturestoapply[9] = textureinsidemouse;
			gridcontent[5,2] = clickedon;
			checkAdjacentnodes(5, 2);
		}
		
		if (GUI.Button(new Rect(447, 153, 22, 22), texturestoapply[10], mystyle))
		{
			if (Event.current.button == 1 && texturestoapply[10] != null)
			{
				Storepotential(new Vector2(6,2), 10);
				removemsg = true;
			}
			if (clickedon == null)
				return;
			if (clickedon == "upwards" || clickedon == "downwards" || clickedon == "leftwards" || clickedon == "rightwards")
			{
				Changewave(clickedon, 6, 2);
				updateAdjacentnodes(6,2);
				Clearallmouseshit();
				return;	
			}
			texturestoapply[10] = textureinsidemouse;
			gridcontent[6,2] = clickedon;
			checkAdjacentnodes(6, 2);
		}
		
	}
	
	
	void checkAdjacentnodes(int gridlocationx, int gridlocationy)
	{
		//desclaring adjacent nodes
		string abovenode, rightnode, belownode, leftnode;
		Vector2 aboveloc, rightloc, belowloc, leftloc;
		
		//finding the adjacent nodes
		abovenode = gridcontent[gridlocationx, gridlocationy - 1];
		aboveloc = new Vector2 (gridlocationx, gridlocationy - 1);
		rightnode = gridcontent[gridlocationx + 1, gridlocationy];
		rightloc = new Vector2 (gridlocationx + 1, gridlocationy);
		belownode = gridcontent[gridlocationx, gridlocationy + 1];
		belowloc = new Vector2 (gridlocationx, gridlocationy + 1);
		leftnode = gridcontent[gridlocationx - 1, gridlocationy];
		leftloc = new Vector2 (gridlocationx - 1, gridlocationy);
		
		//Find out the current wave direction through gridcontent
		string currentwave = gridcontent[gridlocationx, gridlocationy];
		//Find out what the stat is and clears the mouse "clipboard"
		string currentstat = Queryforstats(gridlocationx, gridlocationy);
		
		//generator check
		if (abovenode == "generator")
		{
			gridcontent[gridlocationx, gridlocationy] = "powerwavedown";
		}
		
		if (rightnode == "generator")
		{
			gridcontent[gridlocationx, gridlocationy] = "powerwaveleft";
		}
		
		if (belownode == "generator")
		{
			gridcontent[gridlocationx, gridlocationy] = "powerwaveup";
		}
		
		if (leftnode == "generator")
		{
			gridcontent[gridlocationx, gridlocationy] = "powerwaveright";
		}
		
		
		
		if (currentstat == "spd")
		{
			//check for adjacent active powerwaves
			if (abovenode == "powerwavedown")
			{
				//if its the same stat then you can continue the wave
				if (spdnodes.Contains(aboveloc))
				{
					gridcontent[gridlocationx, gridlocationy] = "powerwavedown";
				}
			}
			
			if (rightnode == "powerwaveleft")
			{
				//if its the same stat then you can continue the wave
				if (spdnodes.Contains(rightloc))
				{
					gridcontent[gridlocationx, gridlocationy] = "powerwaveleft";
				
				}
				
				
			}
			
			if (belownode == "powerwaveup")
			{
				//if its the same stat then you can continue the wave
				if (spdnodes.Contains(belowloc))
				{
					gridcontent[gridlocationx, gridlocationy] = "powerwaveup";
				}
			}
			
			if (leftnode == "powerwaveright")
			{
				//if its the same stat then you can continue the wave
				if (spdnodes.Contains(leftloc))
				{
					gridcontent[gridlocationx, gridlocationy] = "powerwaveright";
				}
			}
			
			//check for adjacent nodes of just the same thing but no waves
			if (leftnode == "spd")
				gridcontent[(int)leftloc.x, (int)leftloc.y] = currentwave;
			if (rightnode == "spd")
				gridcontent[(int)leftloc.x, (int)leftloc.y] = currentwave;
			if (leftnode == "spd")
				gridcontent[(int)leftloc.x, (int)leftloc.y] = currentwave;
			if (leftnode == "spd")
				gridcontent[(int)leftloc.x, (int)leftloc.y] = currentwave;
		}
		
		
	}
	
	
	void updateAdjacentnodes(int gridlocationx, int gridlocationy)
	{
		//finding adjacent nodes'n contact 'n location
		string abovenode, rightnode, belownode, leftnode;
		Vector2 aboveloc, rightloc, belowloc, leftloc;
		
		abovenode = gridcontent[gridlocationx, gridlocationy - 1];
		aboveloc = new Vector2 (gridlocationx, gridlocationy - 1);
		rightnode = gridcontent[gridlocationx + 1, gridlocationy];
		rightloc = new Vector2 (gridlocationx + 1, gridlocationy);
		belownode = gridcontent[gridlocationx, gridlocationy + 1];
		belowloc = new Vector2 (gridlocationx, gridlocationy + 1);
		leftnode = gridcontent[gridlocationx - 1, gridlocationy];
		leftloc = new Vector2 (gridlocationx - 1, gridlocationy);
		
		//Find out the current wave direction through gridcontent
		string currentwave = gridcontent[gridlocationx, gridlocationy];
		//Find out what the old stat is by cross checking the lists
		string currentstat = Findoldstat(gridlocationx, gridlocationy);
		

			//check for adjacent square with same stat and continue wave
			if (abovenode == currentstat && currentwave == "powerwaveup")
			{
				gridcontent[(int)aboveloc.x, (int)aboveloc.y] = "powerwaveup";

			}
			
			if (rightnode == currentstat && currentwave == "powerwaveright")
			{
					gridcontent[(int)rightloc.x, (int)rightloc.y] = "powerwaveright";
			}
			
			if (belownode == currentstat && currentwave == "powerwavedown")
			{
					gridcontent[(int)belowloc.x, (int)belowloc.y] = "powerwavedown";
			}
			
			if (leftnode == currentstat && currentwave == "powerwaveleft")
			{
					gridcontent[(int)leftloc.x, (int)leftloc.y] = "powerwaveleft";
			}
		
			//discontinue unconnected waves
			if (leftnode != currentwave && leftnode != "powerwaveright")
				gridcontent[(int)leftloc.x, (int)leftloc.y] = currentstat;
			if (rightnode != currentwave && rightnode != "powerwaveleft")
				gridcontent[(int)leftloc.x, (int)leftloc.y] = currentstat;
			if (belownode != currentwave && belownode != "powerwavedown")
				gridcontent[(int)leftloc.x, (int)leftloc.y] = currentstat;
			if (abovenode != currentwave && abovenode != "powerwaveup")
				gridcontent[(int)leftloc.x, (int)leftloc.y] = currentstat;
		
		
		
	}
	
	void ChainAdjacentnodes(int gridlocationx, int gridlocationy)
	{
		//declaring adjacent nodes
		string abovenode, rightnode, belownode, leftnode;
		Vector2 aboveloc, rightloc, belowloc, leftloc;
		
		//finding the adjacent nodes
		abovenode = gridcontent[gridlocationx, gridlocationy - 1];
		aboveloc = new Vector2 (gridlocationx, gridlocationy - 1);
		rightnode = gridcontent[gridlocationx + 1, gridlocationy];
		rightloc = new Vector2 (gridlocationx + 1, gridlocationy);
		belownode = gridcontent[gridlocationx, gridlocationy + 1];
		belowloc = new Vector2 (gridlocationx, gridlocationy + 1);
		leftnode = gridcontent[gridlocationx - 1, gridlocationy];
		leftloc = new Vector2 (gridlocationx - 1, gridlocationy);
		
		//Find out the current wave direction through gridcontent
		string currentwave = gridcontent[gridlocationx, gridlocationy];
		//Find out what the stat is and clears the mouse "clipboard"
		string currentstat = Queryforstats(gridlocationx, gridlocationy);
		
		
		if (currentstat == "spd")
		{
			//check for adjacent active powerwaves
			if (abovenode == "powerwavedown")
			{
				//if its the same stat then you can continue the wave
				if (spdnodes.Contains(aboveloc))
				{
					gridcontent[gridlocationx, gridlocationy] = "powerwavedown";
				}
			}
			
			if (rightnode == "powerwaveleft")
			{
				//if its the same stat then you can continue the wave
				if (spdnodes.Contains(rightloc))
				{
					gridcontent[gridlocationx, gridlocationy] = "powerwaveleft";
				
				}
				
				
			}
			
			if (belownode == "powerwaveup")
			{
				//if its the same stat then you can continue the wave
				if (spdnodes.Contains(belowloc))
				{
					gridcontent[gridlocationx, gridlocationy] = "powerwaveup";
				}
			}
			
			if (leftnode == "powerwaveright")
			{
				//if its the same stat then you can continue the wave
				if (spdnodes.Contains(leftloc))
				{
					gridcontent[gridlocationx, gridlocationy] = "powerwaveright";
				}
			}
			
			//check for adjacent nodes of just the same thing but no waves
			if (leftnode == "spd")
				gridcontent[(int)leftloc.x, (int)leftloc.y] = currentwave;
			if (rightnode == "spd")
				gridcontent[(int)leftloc.x, (int)leftloc.y] = currentwave;
			if (leftnode == "spd")
				gridcontent[(int)leftloc.x, (int)leftloc.y] = currentwave;
			if (leftnode == "spd")
				gridcontent[(int)leftloc.x, (int)leftloc.y] = currentwave;
		}
		
		
	}
	
	void Chainreaction()
	{

		foreach (Vector2 gridloc in spdnodes)
		{
			updateAdjacentnodes((int)gridloc.x, (int)gridloc.y);	
			
		}
		
		foreach (Vector2 gridloc in strnodes)
		{
			updateAdjacentnodes((int)gridloc.x, (int)gridloc.y);	
		}
		
		foreach (Vector2 gridloc in vitnodes)
		{
			updateAdjacentnodes((int)gridloc.x, (int)gridloc.y);	
		}
		
		Debug.Log("firstsquare: " + gridcontent[1,1].ToString() + "\n secondsquare: " + gridcontent[2,1].ToString());
		
	}
	
	void graphicsforwave()
	{
		
		for (int v1 = 0; v1 <= 8; v1++)
		{
			for (int v2 = 0; v2 <= 10; v2++)
			{
				if (gridcontent[v1, v2] == "powerwavedown")
				{
					GUI.color = transparent;
					GUI.DrawTexture(invisiblewaverects[v1,v2], spdwave);
				}
				
				if (gridcontent[v1, v2] == "powerwaveleft")
				{
					GUI.color = transparent;
					GUI.DrawTexture(invisiblewaverects[v1,v2], spdwaveleft);
				}
				
				if (gridcontent[v1, v2] == "powerwaveup")
				{
					GUI.color = transparent;
					GUI.DrawTexture(invisiblewaverects[v1,v2], spdwaveup);
				}
				
				if (gridcontent[v1, v2] == "powerwaveright")
				{
					GUI.color = transparent;
					GUI.DrawTexture(invisiblewaverects[v1,v2], spdwaveright);
				}
				
//				if (gridcontent[v1, v2] == "spd" || gridcontent[v1, v2] == "str" || gridcontent[v1, v2] == "vit")
//					ChainAdjacentnodes(v1, v2);
				
			}
		}
		
		
		
//		foreach (string content in gridcontent)
//		{
//			if (content == "powerwavedown")
//			{
//				int contentx = System.Array.IndexOf(gridcontent, content);
//				GUI.DrawTexture(invisiblewaverects[0,0], spdwave);
//			}
//			
//		}
//		if (gridcontent[(int)gridlocation.x, (int)gridlocation.y] == "powerwavedown")	
//		{
//			
//			
//		}
//		
//		if (gridcontent[(int)gridlocation.x, (int)gridlocation.y] == "powerwaveleft")	
//		{
//			
//			
//		}
//		
//		if (gridcontent[(int)gridlocation.x, (int)gridlocation.y] == "powerwaveup")	
//		{
//			
//			
//		}
//		
//		if (gridcontent[(int)gridlocation.x, (int)gridlocation.y] == "powerwaveright")	
//		{
//			
//			
//		}
		
	}
	
	string Queryforstats(int gridX, int gridY)
	{
		if (speedon)
		{
			spdnodes.Add(new Vector2(gridX, gridY));
			Clearallmouseshit();
			return "spd";
		}
		
		if (strengthon)
		{
			strnodes.Add(new Vector2(gridX, gridY));
			Clearallmouseshit();
			return "STR";
		}
		
		if (viton)
		{
			vitnodes.Add(new Vector2(gridX, gridY));
			Clearallmouseshit();
			return "vit";
		}
		
		return null;
		
	}
	
	string Findoldstat(int gridX, int gridY)
	{
		if (spdnodes.Contains(new Vector2(gridX, gridY)))
		{
			return "spd";	
			
		}
		
		if (strnodes.Contains(new Vector2(gridX, gridY)))
		{
			return "STR";	
			
		}
		
		if (vitnodes.Contains(new Vector2(gridX, gridY)))
		{
			return "vit";	
			
		}
		
//		if (spdnodes.Contains(new Vector2(gridX, gridY)))
//		{
//			
//			
//		}
//		
		return null;	
	}
	
	void Clearallmouseshit()
	{
		textureinsidemouse = null;
		clickedon = null;
		speedon = false;
		strengthon = false;
		viton = false;
		helpmsg = false;
		wavemsg = false;	
		
	}
	
	
	
	void setupstaticgridnodes()
	{
		//GEN
		gridcontent[4,1] = "generator";
		
		//all of 0th row
		gridcontent[0,0] = "null";
		gridcontent[1,0] = "null";
		gridcontent[2,0] = "null";
		gridcontent[3,0] = "null";
		gridcontent[4,0] = "null";
		gridcontent[5,0] = "null";
		gridcontent[6,0] = "null";
		gridcontent[7,0] = "null";
		gridcontent[8,0] = "null";
		
		//two sides for 1st row
		gridcontent[0,1] = "null";
		gridcontent[8,1] = "null";
		
		//bite off sides 2nd and 3rd row
		gridcontent[1,2] = "null";
		gridcontent[1,3] = "null";
		gridcontent[7,2] = "null";
		gridcontent[7,3] = "null";
		
		//far left column 5 rows
		gridcontent[0,4] = "null";
		gridcontent[0,5] = "null";
		gridcontent[0,6] = "null";
		gridcontent[0,7] = "null";
		gridcontent[0,8] = "null";
		
		//far right column 5 rows
		gridcontent[8,4] = "null";
		gridcontent[8,5] = "null";
		gridcontent[8,6] = "null";
		gridcontent[8,7] = "null";
		gridcontent[8,8] = "null";
		
		//2 forehead slots row 8
		gridcontent[2,8] = "null";
		gridcontent[6,8] = "null";
		
		//all of 9th row except middle
		gridcontent[1,9] = "null";
		gridcontent[2,9] = "null";
		gridcontent[3,9] = "null";
		gridcontent[5,9] = "null";
		gridcontent[6,9] = "null";
		gridcontent[7,9] = "null";
		
		//nose node
		gridcontent[4,10] = "null";
		
	}
	
	void generateinvisiblerects()
	{
		//first row
		invisiblewaverects[1,1] = new Rect(314, 126, 22, 22);
		invisiblewaverects[2,1] = new Rect(341, 126, 22, 22);
		invisiblewaverects[3,1] = new Rect(368, 126, 22, 22);
		//gen goes here
		invisiblewaverects[5,1] = new Rect(421, 126, 22, 22);
		invisiblewaverects[6,1] = new Rect(447, 126, 22, 22);
		invisiblewaverects[7,1] = new Rect(474, 126, 22, 22);
		
		//second row
		invisiblewaverects[2,2] = new Rect(341, 153, 22, 22);
		invisiblewaverects[3,2] = new Rect(368, 153, 22, 22);
		invisiblewaverects[4,2] = new Rect(394, 153, 22, 22);
		invisiblewaverects[5,2] = new Rect(421, 153, 22, 22);
		invisiblewaverects[6,2] = new Rect(447, 153, 22, 22);
		
	}
	
	void Changewave(string direction, int gridx, int gridy)
	{
		if (gridcontent[gridx, gridy] != null)
		{
			if (direction == "downwards")
			{
				gridcontent[gridx, gridy] = "powerwavedown";
				
			}
			
			if (direction == "upwards")
			{
				gridcontent[gridx, gridy] = "powerwaveup";
				
			}
			
			if (direction == "rightwards")
			{
				gridcontent[gridx, gridy] = "powerwaveright";
				
			}
			
			if (direction == "leftwards")
			{
				gridcontent[gridx, gridy] = "powerwaveleft";
				
			}
//			checkAdjacentnodes(gridx, gridy);
		}
		
	}
	
	void Storepotential(Vector2 gridloc, int nodetexture)
	{
		nodeinquestion = gridloc;	
		texturenumber = nodetexture;
	}
		
	
}
