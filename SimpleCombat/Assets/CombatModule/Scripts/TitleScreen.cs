using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour {
	
	enum GameStates{
		TITLE,
		PLAYERSELECT
	}
	
	//window rectangles
	Rect titleWindow = new Rect(0,0,Screen.width,Screen.height);
	Rect playerSelectWindow = new Rect(100, 50,400, 400);
	Rect playerStatsWindow = new Rect(Screen.width - 500, 50, 400, 400);
	
	//button rectangles
	Rect startButton = new Rect(100,Screen.height - 200 ,100,50);
	Rect quitOrBack = new Rect(Screen.width - 200, Screen.height - 200 ,100,50);
	Rect gameStart = new Rect(Screen.width/2 -50,  Screen.height - 200 ,100,50);
	
	
	//menu states
	int currentState = 0;
	
	//selected player
	int selectedPlayer1 = 0;
	int selectedPlayer2 = 0;
	
	bool player1Selected = false;
	
	
	//determines what window is in focus
	int windowFucus = 0;
	
	//holds all the player crads to choose from as a reference
	public PlayerCard[] cards = new PlayerCard[16];
	
	
	
	
	// Use this for initialization
	void Start () {
		foreach(PlayerCard card in cards){
			card.Assign();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	// draws the GUI
	void OnGUI(){
		titleWindow = GUI.Window(0,titleWindow, MainWindow,"Simple Combat");
		if (currentState == (int)GameStates.PLAYERSELECT){
			playerSelectWindow = GUI.Window(1,playerSelectWindow, PlayerSelectWindow, "Player Select");
			playerStatsWindow = GUI.Window(2,playerStatsWindow, PlayerStatsWindow, "Player Stats");
		}
	}
	
	
	//draws everything for the main window
	void MainWindow(int windowID){
		
		GUI.BringWindowToBack(0);
		
		if(currentState == (int)GameStates.TITLE){
			
			if(GUI.Button(startButton,"Start")){
				currentState = (int)GameStates.PLAYERSELECT;
			}
			if(GUI.Button(quitOrBack,"Quit")){
				Application.Quit();
			}
		}
		
		if(currentState == (int)GameStates.PLAYERSELECT){
			if(GUI.Button(quitOrBack,"back")){
				if(player1Selected) player1Selected = false;
				else currentState = (int)GameStates.TITLE;
			}
			if(!player1Selected){
				if(selectedPlayer1 > 0){
					if(GUI.Button(gameStart,"Select")){
						player1Selected = true;
						
						
					}
				}
			}
			else{
				if(selectedPlayer2 > 0){
					if(GUI.Button(gameStart,"Begin Battle")){
						//pass the selected player information to the next
						GameController.getPlayer1(cards[selectedPlayer1-1]);
						GameController.getPlayer2(cards[selectedPlayer2-1]);
						//load the next screen
						Application.LoadLevel("BattleScene");
					}
				}
			}
			
		}
		
		//checks for focus on this window
		if((Event.current.button == 0) && (Event.current.type == EventType.MouseDown)) windowFucus = windowID;
	}
	
	//draws everything for the player select window
	void PlayerSelectWindow(int windowID){
		if(!player1Selected){
			//column 1
			if(GUI.Button(new Rect(20 , 40 , 150,30),cards[0 ].name)) selectedPlayer1 = 1;
			if(GUI.Button(new Rect(20 , 80 , 150,30),cards[1 ].name)) selectedPlayer1 = 2;
			if(GUI.Button(new Rect(20 , 120, 150,30),cards[2 ].name)) selectedPlayer1 = 3;
			if(GUI.Button(new Rect(20 , 160, 150,30),cards[3 ].name)) selectedPlayer1 = 4;
			if(GUI.Button(new Rect(20 , 200, 150,30),cards[4 ].name)) selectedPlayer1 = 5;
			if(GUI.Button(new Rect(20 , 240, 150,30),cards[5 ].name)) selectedPlayer1 = 6;
			if(GUI.Button(new Rect(20 , 280, 150,30),cards[6 ].name)) selectedPlayer1 = 7;
			if(GUI.Button(new Rect(20 , 320, 150,30),cards[7 ].name)) selectedPlayer1 = 8;
																					
			//column 2															  
			if(GUI.Button(new Rect(230, 40 , 150,30),cards[8 ].name)) selectedPlayer1 = 9 ;
			if(GUI.Button(new Rect(230, 80 , 150,30),cards[9 ].name)) selectedPlayer1 = 10;
			if(GUI.Button(new Rect(230, 120, 150,30),cards[10].name)) selectedPlayer1 = 11;
			if(GUI.Button(new Rect(230, 160, 150,30),cards[11].name)) selectedPlayer1 = 12;
			if(GUI.Button(new Rect(230, 200, 150,30),cards[12].name)) selectedPlayer1 = 13;
			if(GUI.Button(new Rect(230, 240, 150,30),cards[13].name)) selectedPlayer1 = 14;
			if(GUI.Button(new Rect(230, 280, 150,30),cards[14].name)) selectedPlayer1 = 15;
			if(GUI.Button(new Rect(230, 320, 150,30),cards[15].name)) selectedPlayer1 = 16;
			//clear selection
			if(Input.GetMouseButtonDown(0) && windowFucus == windowID) selectedPlayer1 = 0;
			
		}
		else {
			//column 1
			if(GUI.Button(new Rect(20 , 40 , 150,30),cards[0 ].name)) selectedPlayer2 = 1;
			if(GUI.Button(new Rect(20 , 80 , 150,30),cards[1 ].name)) selectedPlayer2 = 2;
			if(GUI.Button(new Rect(20 , 120, 150,30),cards[2 ].name)) selectedPlayer2 = 3;
			if(GUI.Button(new Rect(20 , 160, 150,30),cards[3 ].name)) selectedPlayer2 = 4;
			if(GUI.Button(new Rect(20 , 200, 150,30),cards[4 ].name)) selectedPlayer2 = 5;
			if(GUI.Button(new Rect(20 , 240, 150,30),cards[5 ].name)) selectedPlayer2 = 6;
			if(GUI.Button(new Rect(20 , 280, 150,30),cards[6 ].name)) selectedPlayer2 = 7;
			if(GUI.Button(new Rect(20 , 320, 150,30),cards[7 ].name)) selectedPlayer2 = 8;
																					
			//column 2															    
			if(GUI.Button(new Rect(230, 40 , 150,30),cards[8 ].name)) selectedPlayer2 = 9 ;
			if(GUI.Button(new Rect(230, 80 , 150,30),cards[9 ].name)) selectedPlayer2 = 10;
			if(GUI.Button(new Rect(230, 120, 150,30),cards[10].name)) selectedPlayer2 = 11;
			if(GUI.Button(new Rect(230, 160, 150,30),cards[11].name)) selectedPlayer2 = 12;
			if(GUI.Button(new Rect(230, 200, 150,30),cards[12].name)) selectedPlayer2 = 13;
			if(GUI.Button(new Rect(230, 240, 150,30),cards[13].name)) selectedPlayer2 = 14;
			if(GUI.Button(new Rect(230, 280, 150,30),cards[14].name)) selectedPlayer2 = 15;
			if(GUI.Button(new Rect(230, 320, 150,30),cards[15].name)) selectedPlayer2 = 16;
			//clear selection
			if(Input.GetMouseButtonDown(0) && windowFucus == windowID) selectedPlayer2 = 0;
		}
		//checks for focus on this window
		if((Event.current.button == 0) && (Event.current.type == EventType.MouseDown)) windowFucus = windowID;
		
		
		
	}
	
	//draws the stats in the stats window
	void PlayerStatsWindow(int windowID){
		
		if(!player1Selected){
		
			if(selectedPlayer1 > 0){
				GUI.TextArea(new Rect(20, 25 , 150, 25), cards[selectedPlayer1 - 1].attack_1.attackName);
				GUI.TextArea(new Rect(20, 55 , 150, 25), cards[selectedPlayer1 - 1].attack_2.attackName);
				GUI.TextArea(new Rect(20, 85 , 150, 25), cards[selectedPlayer1 - 1].attack_3.attackName);
				GUI.TextArea(new Rect(20, 115, 150, 25), cards[selectedPlayer1 - 1].defend_1.defendName);
				GUI.TextArea(new Rect(20, 145, 150, 25), cards[selectedPlayer1 - 1].defend_2.defendName);
				GUI.TextArea(new Rect(20, 175, 150, 25), cards[selectedPlayer1 - 1].defend_3.defendName);
				GUI.TextArea(new Rect(20, 205, 150, 25), cards[selectedPlayer1 - 1].meditate_1.attackName);
				GUI.TextArea(new Rect(20, 235, 150, 25), cards[selectedPlayer1 - 1].meditate_2.attackName);
				GUI.TextArea(new Rect(20, 265, 150, 25), cards[selectedPlayer1 - 1].meditate_3.attackName);
				GUI.TextArea(new Rect(20, 295, 150, 25), "Health");
				GUI.TextArea(new Rect(20, 325, 150, 25), "Stamina");
				GUI.TextArea(new Rect(20, 355, 150, 25), "Stamina Regen Rate");
			
			
				GUI.TextArea(new Rect(230, 25 , 150, 25), cards[selectedPlayer1-1].attack_1.basePower.ToString());
				GUI.TextArea(new Rect(230, 55 , 150, 25), cards[selectedPlayer1-1].attack_2.basePower.ToString());
				GUI.TextArea(new Rect(230, 85 , 150, 25), cards[selectedPlayer1-1].attack_3.basePower.ToString());
				GUI.TextArea(new Rect(230, 115, 150, 25), cards[selectedPlayer1-1].defend_1.baseDefense.ToString());
				GUI.TextArea(new Rect(230, 145, 150, 25), cards[selectedPlayer1-1].defend_2.baseDefense.ToString());
				GUI.TextArea(new Rect(230, 175, 150, 25), cards[selectedPlayer1-1].defend_3.baseDefense.ToString());
				GUI.TextArea(new Rect(230, 205, 150, 25), cards[selectedPlayer1-1].meditate_1.basePower.ToString());
				GUI.TextArea(new Rect(230, 235, 150, 25), cards[selectedPlayer1-1].meditate_2.basePower.ToString());
				GUI.TextArea(new Rect(230, 265, 150, 25), cards[selectedPlayer1-1].meditate_3.basePower.ToString());
				GUI.TextArea(new Rect(230, 295, 150, 25), cards[selectedPlayer1-1].health.ToString());
				GUI.TextArea(new Rect(230, 325, 150, 25), cards[selectedPlayer1-1].maxStamina.ToString());
				GUI.TextArea(new Rect(230, 355, 150, 25), cards[selectedPlayer1-1].stamRegen.ToString());
			}
			
		}
		
		else{
			if(selectedPlayer2 > 0){
				GUI.TextArea(new Rect(20, 25 , 150, 25), cards[selectedPlayer2 - 1].attack_1.attackName);
				GUI.TextArea(new Rect(20, 55 , 150, 25), cards[selectedPlayer2 - 1].attack_2.attackName);
				GUI.TextArea(new Rect(20, 85 , 150, 25), cards[selectedPlayer2 - 1].attack_3.attackName);
				GUI.TextArea(new Rect(20, 115, 150, 25), cards[selectedPlayer2 - 1].defend_1.defendName);
				GUI.TextArea(new Rect(20, 145, 150, 25), cards[selectedPlayer2 - 1].defend_2.defendName);
				GUI.TextArea(new Rect(20, 175, 150, 25), cards[selectedPlayer2 - 1].defend_3.defendName);
				GUI.TextArea(new Rect(20, 205, 150, 25), cards[selectedPlayer2 - 1].meditate_1.attackName);
				GUI.TextArea(new Rect(20, 235, 150, 25), cards[selectedPlayer2 - 1].meditate_2.attackName);
				GUI.TextArea(new Rect(20, 265, 150, 25), cards[selectedPlayer2 - 1].meditate_3.attackName);
				GUI.TextArea(new Rect(20, 295, 150, 25), "Health");
				GUI.TextArea(new Rect(20, 325, 150, 25), "Stamina");
				GUI.TextArea(new Rect(20, 355, 150, 25), "Stamina Regen Rate");
			
			
				GUI.TextArea(new Rect(230, 25 , 150, 25), cards[selectedPlayer2-1].attack_1.basePower.ToString());
				GUI.TextArea(new Rect(230, 55 , 150, 25), cards[selectedPlayer2-1].attack_2.basePower.ToString());
				GUI.TextArea(new Rect(230, 85 , 150, 25), cards[selectedPlayer2-1].attack_3.basePower.ToString());
				GUI.TextArea(new Rect(230, 115, 150, 25), cards[selectedPlayer2-1].defend_1.baseDefense.ToString());
				GUI.TextArea(new Rect(230, 145, 150, 25), cards[selectedPlayer2-1].defend_2.baseDefense.ToString());
				GUI.TextArea(new Rect(230, 175, 150, 25), cards[selectedPlayer2-1].defend_3.baseDefense.ToString());
				GUI.TextArea(new Rect(230, 205, 150, 25), cards[selectedPlayer2-1].meditate_1.basePower.ToString());
				GUI.TextArea(new Rect(230, 235, 150, 25), cards[selectedPlayer2-1].meditate_2.basePower.ToString());
				GUI.TextArea(new Rect(230, 265, 150, 25), cards[selectedPlayer2-1].meditate_3.basePower.ToString());
				GUI.TextArea(new Rect(230, 295, 150, 25), cards[selectedPlayer2-1].health.ToString());
				GUI.TextArea(new Rect(230, 325, 150, 25), cards[selectedPlayer2-1].maxStamina.ToString());
				GUI.TextArea(new Rect(230, 355, 150, 25), cards[selectedPlayer2-1].stamRegen.ToString());
			}
			
		}
		
		
		//checks for focus on this window
		if((Event.current.button == 0) && (Event.current.type == EventType.MouseDown)) windowFucus = windowID;
	}
}
