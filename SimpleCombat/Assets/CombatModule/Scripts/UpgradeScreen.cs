using UnityEngine;
using System.Collections;

public class UpgradeScreen : MonoBehaviour {
	
	//windows rectangles
	Rect mainWindow = new Rect(0,0,Screen.width,Screen.height);
	Rect statsWindow = new Rect(25,50,400,500);
	Rect chiWindow = new Rect(Screen.width - 325, 50, 300, 200);
	
	
	//the card that is passed into the upgrade scene to be upgraded
	static public PlayerCard temp;
	public PlayerCard player;
	
	//run this when loading this screen to pass player to be upgraded into temp
	public static void getPlayer(PlayerCard importPlayer){
		temp = importPlayer;
		
		
	}
	
	
	// Use this for initialization
	void Start () {
		 SetStats();
	}
	
	//assigns the stats from temp to player
	void SetStats(){
		player.attack_1 = temp.attack_1;
		player.attack_2 = temp.attack_2;
		player.attack_3 = temp.attack_3;
		player.defend_1 = temp.defend_1;
		player.defend_2 = temp.defend_2;
		player.defend_3 = temp.defend_3;
		player.meditate_1 = temp.meditate_1;
		player.meditate_2 = temp.meditate_2;
		player.meditate_3 = temp.meditate_3;
		player.health = temp.health;
		player.maxStamina = temp.maxStamina;
		player.stamRegen = temp.stamRegen;
		player.level = temp.level;
		player.name = temp.name;
	}
	
	
	// Update is called once per frame
	void Update () {
		
	}
	
	
	//draws the three windows
	void OnGUI(){
		mainWindow = GUI.Window(0,mainWindow, DoMainWindow, "Upgrade");
		statsWindow = GUI.Window(1,statsWindow, DoStatsWindow, "Stats: " + player.name);
		chiWindow = GUI.Window(2,chiWindow, DoChiWindow, "Chi Level");
		
		
		//keep main window in back
		GUI.BringWindowToBack(0);
	}
	
	
	//main window function
	void DoMainWindow(int windowID){
		if(GUI.Button(new Rect(Screen.width - 150, 600,100,30), "next fight")){
			Application.LoadLevel("MenuScene");
			
		}
	}
	
	
	//stats windows and upgrading
	void DoStatsWindow(int windowID){
		
		GUI.TextArea(new Rect(20, 25, 150, 25), "Attack 1");
		GUI.TextArea(new Rect(20, 55 , 150, 25), "Attack 2");
		GUI.TextArea(new Rect(20, 85 , 150, 25), "Attack 3");
		GUI.TextArea(new Rect(20, 115, 150, 25), "Defence 1");
		GUI.TextArea(new Rect(20, 145, 150, 25), "Defence 2");
		GUI.TextArea(new Rect(20, 175, 150, 25), "Defense 3");
		GUI.TextArea(new Rect(20, 205, 150, 25), "Meditate 1");
		GUI.TextArea(new Rect(20, 235, 150, 25), "Meditate 2");
		GUI.TextArea(new Rect(20, 265, 150, 25), "Meditate 3");
		GUI.TextArea(new Rect(20, 295, 150, 25), "Health");
		GUI.TextArea(new Rect(20, 325, 150, 25), "Stamina");
		GUI.TextArea(new Rect(20, 355, 150, 25), "Stamina Regen Rate");
		
		GUI.TextArea(new Rect(200, 25 , 100, 25), player.attack_1.ToString());
		GUI.TextArea(new Rect(200, 55 , 100, 25), player.attack_2.ToString());
		GUI.TextArea(new Rect(200, 85 , 100, 25), player.attack_3.ToString());
		GUI.TextArea(new Rect(200, 115, 100, 25), player.defend_1.ToString());
		GUI.TextArea(new Rect(200, 145, 100, 25), player.defend_2.ToString());
		GUI.TextArea(new Rect(200, 175, 100, 25), player.defend_3.ToString());
		GUI.TextArea(new Rect(200, 205, 100, 25), player.meditate_1.ToString());
		GUI.TextArea(new Rect(200, 235, 100, 25), player.meditate_2.ToString());
		GUI.TextArea(new Rect(200, 265, 100, 25), player.meditate_3.ToString());
		GUI.TextArea(new Rect(200, 295, 100, 25), player.health.ToString());
		GUI.TextArea(new Rect(200, 325, 100, 25), player.maxStamina.ToString());
		GUI.TextArea(new Rect(200, 355, 100, 25), player.stamRegen.ToString());
		
		//upgrade buttons for upgrading the player
		// upgrades the stats and the player chi level
		if (GUI.Button(new Rect(310, 25 , 75, 25), "UPGRADE")) {player.attack_1.basePower++;       player.level++;}
		if (GUI.Button(new Rect(310, 55 , 75, 25), "UPGRADE")) {player.attack_2.basePower++;       player.level++;}
		if (GUI.Button(new Rect(310, 85 , 75, 25), "UPGRADE")) {player.attack_3.basePower++;       player.level++;}
		if (GUI.Button(new Rect(310, 115, 75, 25), "UPGRADE")) {player.defend_1.baseDefense++;       player.level++;}
		if (GUI.Button(new Rect(310, 145, 75, 25), "UPGRADE")) {player.defend_2.baseDefense++;       player.level++;}
		if (GUI.Button(new Rect(310, 175, 75, 25), "UPGRADE")) {player.defend_3.baseDefense++;       player.level++;}
		if (GUI.Button(new Rect(310, 205, 75, 25), "UPGRADE")) {player.meditate_1.basePower++;     player.level++;}
		if (GUI.Button(new Rect(310, 235, 75, 25), "UPGRADE")) {player.meditate_2.basePower++;     player.level++;}
		if (GUI.Button(new Rect(310, 265, 75, 25), "UPGRADE")) {player.meditate_3.basePower++;     player.level++;}
		if (GUI.Button(new Rect(310, 295, 75, 25), "UPGRADE")) {player.health += 10;     player.level++;}
		if (GUI.Button(new Rect(310, 325, 75, 25), "UPGRADE")) {player.maxStamina += 10; player.level++;}
		if (GUI.Button(new Rect(310, 355, 75, 25), "UPGRADE")) {player.stamRegen++;      player.level++;}
		
	}
	
	
	//draws the chi level
	//only has level but drawn bigger for future graphics
	void DoChiWindow(int windowID){
		GUI.TextArea(new Rect(150,20,50,30), player.level.ToString());
	}
}
