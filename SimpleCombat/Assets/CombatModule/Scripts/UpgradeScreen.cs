using UnityEngine;
using System.Collections;

public class UpgradeScreen : MonoBehaviour {
	
	//windows rectangles
	Rect mainWindow = new Rect(0,0,Screen.width,Screen.height);
	Rect statsWindow = new Rect(25,50,400,500);
	Rect chiWindow = new Rect(Screen.width - 325, 50, 300, 200);
	
	int pointsEarned = 1;
	
	//the card that is passed into the upgrade scene to be upgraded
	static public Profile temp1;
	static public Profile temp2;

	//run this when loading this screen to pass player to be upgraded into temp
	public static void getPlayer(Player importPlayer, out Profile temp){
		temp = importPlayer;
	}
	
	bool player1Went = false;
	
	// Use this for initialization
	void Start () {
		if(!player1Went) pointsEarned = temp1.chiPoints;
		else pointsEarned = temp2.chiPoints;
		
	}
	
	
	// Update is called once per frame
	void Update () {
		temp1.chiLevel = SetLevel (temp1);
		
		temp2.chiLevel = SetLevel (temp2);
		
	}

	static int SetLevel (Profile player)
	{
		int totalPoints = 0;
		float attackPoints = 0;
		float defendPoints = 0;
		float meditatePoints = 0;
		for (int i = 0; i < 3; i++){
			attackPoints += player.attackList[i].basePower;
		}
		attackPoints /= 6;
		for (int i = 3; i < player.attackList.Count; i++){
			meditatePoints += player.attackList[i].basePower;
		}
		meditatePoints /= 6;
		
		for (int i = 0; i < player.defendList.Count; i++){
			defendPoints += player.defendList[i].baseDefense;
		}
		defendPoints /= 6;
		totalPoints = (int)(attackPoints + defendPoints + meditatePoints);
		totalPoints += player.hpMax / 5;
		totalPoints += player.enduranceMax / 5;
		totalPoints += player.enduranceRegen;
		return totalPoints;
	}
	
	
	//draws the three windows
	void OnGUI(){
		string currentName = "";
		if(!player1Went)currentName = temp1.name;
		else currentName = temp2.name;
		mainWindow = GUI.Window(0,mainWindow, DoMainWindow, "Upgrade");
		statsWindow = GUI.Window(1,statsWindow, DoStatsWindow, "Stats: " + currentName);
		chiWindow = GUI.Window(2,chiWindow, DoChiWindow, "Chi Level");
		
		
		//keep main window in back
		GUI.BringWindowToBack(0);
	}
	
	
	//main window function
	void DoMainWindow(int windowID){
		if(GUI.Button(new Rect(Screen.width - 150, 600,100,30), "next fight")){
			if(!player1Went){
				temp1.chiPoints = pointsEarned;
				pointsEarned = temp2.chiPoints;
				player1Went = true;
			}
			else {
				temp2.chiPoints = pointsEarned;
				GameController.getPlayer1((Player)temp1);
				GameController.getPlayer2((Player)temp2);
				Application.LoadLevel("BattleScene");
			}
			
		}
	}
	
	
	//stats windows and upgrading
	void DoStatsWindow(int windowID){
		
		if(!player1Went) DrawStats (temp1);
		else DrawStats(temp2);


	}

	void DrawStats (Profile player)
	{
		GUI.TextArea(new Rect(20, 25 , 150, 25), "Attack 1");
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
		
		GUI.TextArea(new Rect(200, 25 , 100, 25), player.attackList[0].basePower.ToString());
		GUI.TextArea(new Rect(200, 55 , 100, 25), player.attackList[1].basePower.ToString());
		GUI.TextArea(new Rect(200, 85 , 100, 25), player.attackList[2].basePower.ToString());
		GUI.TextArea(new Rect(200, 115, 100, 25), player.defendList[0].baseDefense.ToString());
		GUI.TextArea(new Rect(200, 145, 100, 25), player.defendList[1].baseDefense.ToString());
		GUI.TextArea(new Rect(200, 175, 100, 25), player.defendList[2].baseDefense.ToString());
		GUI.TextArea(new Rect(200, 205, 100, 25), player.attackList[3].basePower.ToString());
		GUI.TextArea(new Rect(200, 235, 100, 25), player.attackList[4].basePower.ToString());
		GUI.TextArea(new Rect(200, 265, 100, 25), player.attackList[5].basePower.ToString());
		GUI.TextArea(new Rect(200, 295, 100, 25), player.hpMax.ToString());
		GUI.TextArea(new Rect(200, 325, 100, 25), player.enduranceMax.ToString());
		GUI.TextArea(new Rect(200, 355, 100, 25), player.enduranceRegen.ToString());
		
		//upgrade buttons for upgrading the player
		// upgrades the stats and the player chi level
		if(pointsEarned < 2) GUI.color = Color.red;
		else GUI.color = Color.green;
		
		if (GUI.Button(new Rect(310, 25 , 75, 75), "UPGRADE")) {
			if(pointsEarned > 1){
				player.attackList[0].basePower += 1;  
				player.attackList[1].basePower += 2; 
				player.attackList[2].basePower += 3; 
				pointsEarned -= 2;
			}
		}
		
		if (GUI.Button(new Rect(310, 115, 75, 75), "UPGRADE")) {
			if(pointsEarned > 1){
				player.defendList[0].baseDefense += 1;  
				player.defendList[1].baseDefense += 2;  
				player.defendList[2].baseDefense += 3;  
				pointsEarned -= 2;
			}
		}
		if(pointsEarned < 4) GUI.color = Color.red;
		else GUI.color = Color.green;
		
		
		if (GUI.Button(new Rect(310, 205, 75, 75), "UPGRADE")) {
			if(pointsEarned > 3){
				player.attackList[3].basePower += 1;
				player.attackList[4].basePower += 2;
				player.attackList[5].basePower += 3;
				pointsEarned -= 4;
			}
		}
		
		if(pointsEarned < 5) GUI.color = Color.red;
		else GUI.color = Color.green;
		
		if (GUI.Button(new Rect(310, 295, 75, 25), "UPGRADE")) {
			if(pointsEarned > 4){
		
		if (GUI.Button(new Rect(310, 25 , 75, 75), "UPGRADE")) {
			if(pointsEarned > 1){
				player.attackList[0].basePower += 1;  
				player.attackList[1].basePower += 2; 
				player.attackList[2].basePower += 3; 
				pointsEarned -= 2;
			}
		}
		
		if (GUI.Button(new Rect(310, 115, 75, 75), "UPGRADE")) {
			if(pointsEarned > 1){
				player.defendList[0].baseDefense += 1;  
				player.defendList[1].baseDefense += 2;  
				player.defendList[2].baseDefense += 3;  
				pointsEarned -= 2;
			}
		}
		if(pointsEarned < 4) GUI.color = Color.red;
		else GUI.color = Color.green;
		
		
		if (GUI.Button(new Rect(310, 205, 75, 75), "UPGRADE")) {
			if(pointsEarned > 3){
				player.attackList[3].basePower += 1;
				player.attackList[4].basePower += 2;
				player.attackList[5].basePower += 3;
				pointsEarned -= 4;
			}
		}
		
				pointsEarned -= 5;
			}
		}
		if (GUI.Button(new Rect(310, 325, 75, 25), "UPGRADE")) {
			if(pointsEarned > 4){
				player.enduranceMax += 5;  
				pointsEarned -= 5;
			}
		}
		
		if (GUI.Button(new Rect(310, 355, 75, 25), "UPGRADE")) {
			if(pointsEarned > 4){
				player.enduranceRegen++;      
				pointsEarned -= 5;
			}
		}
	}
	
	
	
	//draws the chi level
	//only has level but drawn bigger for future graphics
	void DoChiWindow(int windowID){
		GUI.TextArea(new Rect(25,20,125,30), "Chi Level");
		if(!player1Went) GUI.TextArea(new Rect(150,20,50,30), temp1.chiLevel.ToString());
		else GUI.TextArea(new Rect(150,20,50,30), temp2.chiLevel.ToString());
		
		GUI.TextArea(new Rect(150,50,100,30), pointsEarned.ToString());
		GUI.TextArea(new Rect(25,50,125,30), "Chi Points Earned");
	}
}
