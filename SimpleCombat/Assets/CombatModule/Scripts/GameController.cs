using UnityEngine;
using System.Collections;


public enum PlayerIndex //enumeration to keep track of player
{
	One,
	Two
}

public enum TurnStage
{
	PlayerStart,
	RecoverEndurance,
	ChooseAttack,
	ChooseAttackType,
	ExecuteAttack,
	ChooseDefense,
	ChooseDefenseType,
	EndOfBattle
}

public class GameController : MonoBehaviour {
	
	public bool battleEnd = false;
	
	public Profile player1;
	public Profile player2;
	Profile attacker;
	Profile defender;
	Profile losingPlayer;
	
	//the card that is passed into the game from title
	static public PlayerCard temp1;
	static public PlayerCard temp2;
	
	//the players passed from the upgrade to bttle again
	static public Player playTemp1;
	static public Player playTemp2;
	
	//holds information for the current attack and defense being used
	AttackOption currentAttack;
	DefendOption currentDefend;
	
	//holdes the current attack and defense action type (rock paper o scissors
	ActionType actionAttack;
	ActionType actionDefend;
	
	//Objects that represent the players visually
	public Transform player1Object;
	public Transform player2Object;
	
	//Health and endurance bar variables and textures
	public Texture2D healthBar;
	public Texture2D enduranceBar;
	public Texture2D healthBack;
	public Texture2D enduranceBack;
	public float healthBar1Width;
	public float healthBar2Width;
	public float enduranceBar1Height;
	public float enduranceBar2Height;
	
	//Variables used to determine the animating of the bars
	bool targetHealthReached = false;
	bool targetHealthReached2 = false;
	bool targetEnduranceReached = false;
	
	
	public PlayerIndex playerTurn = PlayerIndex.One; //Who's turn it is
	
	public TurnStage turnStage = TurnStage.PlayerStart; //What stage the turn is currently in
	
	//Generic counter for doing turn timing
	public float turnCounter = 0; 
	public float turnRate = 4;
	
	#region Effects
	//These are all the particle effects being used!
	public Transform enduranceParticle;
	public Transform attack1Particle;
	public Transform attack2Particle;
	public Transform attack3Particle;
	public Transform defend1Particle;
	public Transform defend2Particle;
	public Transform defend3Particle;
	public Transform dyingFlames;
	
	#endregion
	
	static bool fromUpdate = false;
	//run this once to pass stats from title to game
	public static void getPlayer1(PlayerCard importPlayer){
		temp1 = importPlayer;
		fromUpdate = false;
	}
	public static void getPlayer2(PlayerCard importPlayer){
		temp2 = importPlayer;
		fromUpdate = false;
	}
	
	public static void getPlayer1(Player importPlayer){
		playTemp1 = importPlayer;
		fromUpdate = true;
	}
	public static void getPlayer2(Player importPlayer){
		playTemp2 = importPlayer;
		fromUpdate = true;
	}
	
	// Use this for initialization
	void Start () {
		//Randomly decide which player goes first
		if (Random.Range(1,3) == 2) playerTurn = PlayerIndex.One;
		else playerTurn = PlayerIndex.Two;
		
		
		
		player1 = new Player();
		player2 = new Player();
		
		if(!fromUpdate){
			if (temp1 == null)
			{
				temp1 = GameObject.Find("default1").GetComponent<PlayerCard>();
			}
			
			PassStats((Player)player1, temp1, out player1);
			
			
			
			if (temp2 == null)
			{
				temp2 = GameObject.Find("default2").GetComponent<PlayerCard>();
			}
			PassStats((Player)player2, temp2, out player2);
		}
		else{
			passPlayer ((Player)playTemp1, out player1);
			passPlayer ((Player)playTemp2, out player2);
		}
		player1.playerIndex = PlayerIndex.One;
		player2.playerIndex = PlayerIndex.Two;
		player1.RecoverHP(player1.hpMax);
		player2.RecoverHP(player2.hpMax);
		player1.UseEndurance(player1.enduranceMax);
		player2.UseEndurance(player2.enduranceMax);
		
		player1.profileObject = player1Object;
		player2.profileObject = player2Object;
		
		healthBar1Width = healthBar.width;
		healthBar2Width = healthBar.width;
		enduranceBar1Height = 0;
		enduranceBar2Height = 0;
		
		
	}
	
	//takes information from card and gives it to player
	void PassStats(Player playerIn, PlayerCard temp, out Profile playerOut){
		playerIn.hpMax = temp.health;
		playerIn.enduranceMax = temp.maxStamina;



		playerIn.enduranceRegen = temp.stamRegen;
		playerIn.AddAttackOption(temp.attack_1);
		playerIn.AddAttackOption(temp.attack_2);
		playerIn.AddAttackOption(temp.attack_3);
		playerIn.AddAttackOption(temp.meditate_1);
		playerIn.AddAttackOption(temp.meditate_2);
		playerIn.AddAttackOption(temp.meditate_3);
		
		playerIn.AddDefendOption(temp.defend_1);
		playerIn.AddDefendOption(temp.defend_2);
		playerIn.AddDefendOption(temp.defend_3);


		
		playerIn.name = temp.name;
		playerIn.chiPoints = temp.level;
		
		playerOut = playerIn;
	}
	void passPlayer(Player playerIn, out Profile PlayerOut){
		Player temp = playerIn;
		PlayerOut = temp;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (playerTurn == PlayerIndex.One) //Determine who is the attacker, and who is the defender
		{
			attacker = player1;
			defender = player2;
		}
		else
		{
			attacker = player2;
			defender = player1;
		}
		
		ExecuteTurn(); 
		
		
	}
	
	void OnGUI()
	{
		//Player 1's hp bar
		float hpRatio = (float)player1.hpCurrent / (float)player1.hpMax;
		float targetBarWidth = healthBar.width * hpRatio;
		float deltaValue = targetBarWidth - healthBar1Width;
		healthBar1Width += deltaValue / 8;
		if (Mathf.Abs(deltaValue) < .02f) healthBar1Width = targetBarWidth;
		if (healthBar1Width == targetBarWidth) targetHealthReached = true;
		GUI.DrawTexture(new Rect(10, Screen.height - healthBack.height - 20, healthBack.width, healthBack.height), healthBack, ScaleMode.StretchToFill);
		GUI.DrawTexture(new Rect(10, Screen.height - healthBar.height - 20, healthBar1Width, healthBar.height), healthBar, ScaleMode.StretchToFill);
		GUI.Label(new Rect(10, Screen.height - 20, 100, 25), player1.hpCurrent + "/" + player1.hpMax);
	
		//Player 2's hp bar
		hpRatio = (float)player2.hpCurrent / (float)player2.hpMax;
		targetBarWidth = healthBar.width * hpRatio;
		healthBar2Width += (targetBarWidth - healthBar2Width) / 8;
		if (Mathf.Abs(targetBarWidth - healthBar2Width) < .02f) healthBar2Width = targetBarWidth;
		if (healthBar1Width == targetBarWidth) targetHealthReached2 = true;
		GUI.DrawTexture(new Rect(Screen.width - healthBack.width - 10, Screen.height - healthBack.height - 20, healthBack.width, healthBack.height), healthBack, ScaleMode.StretchToFill);
		GUI.DrawTexture(new Rect(Screen.width - healthBar2Width - 10, Screen.height - healthBar.height - 20, healthBar2Width, healthBar.height), healthBar, ScaleMode.StretchToFill);
		GUI.Label(new Rect(Screen.width - 40, Screen.height - 20, 50, 25), player2.hpCurrent + "/" + player2.hpMax);
		
		//Player 1's endurance bar
		float enduranceRatio = (float)player1.enduranceCurrent / (float)player1.enduranceMax;
		float barHeight = enduranceBar.height * enduranceRatio;
		enduranceBar1Height += (barHeight - enduranceBar1Height) / 8;
		if (Mathf.Abs(barHeight - enduranceBar1Height) < .02f) enduranceBar1Height =  barHeight;
		if (enduranceBar1Height == barHeight && playerTurn == PlayerIndex.One) targetEnduranceReached = true;
		GUI.DrawTexture(new Rect(10, Screen.height - enduranceBack.height - 60, enduranceBack.width, enduranceBack.height), enduranceBack, ScaleMode.StretchToFill);
		GUI.DrawTexture(new Rect(10, Screen.height - enduranceBar1Height - 60, enduranceBar.width, enduranceBar1Height), enduranceBar, ScaleMode.StretchToFill);
		GUI.Label(new Rect(15 + enduranceBar.width, Screen.height - 85, 100, 25), player1.enduranceCurrent + "/" + player1.enduranceMax);
		
		//Player 2's endurance bar
		enduranceRatio = (float)player2.enduranceCurrent / (float)player2.enduranceMax;
		barHeight = enduranceBar.height * enduranceRatio;
		enduranceBar2Height += (barHeight - enduranceBar2Height) / 8;
		if (Mathf.Abs(barHeight - enduranceBar2Height) < .02f) enduranceBar2Height = barHeight;
		if (enduranceBar2Height == barHeight && playerTurn == PlayerIndex.Two) targetEnduranceReached = true;
		GUI.DrawTexture(new Rect(Screen.width - enduranceBack.width - 10, Screen.height - enduranceBack.height - 60, enduranceBack.width, enduranceBack.height), enduranceBack, ScaleMode.StretchToFill);
		GUI.DrawTexture(new Rect(Screen.width - enduranceBar.width - 10, Screen.height - enduranceBar2Height - 60, enduranceBar.width, enduranceBar2Height), enduranceBar, ScaleMode.StretchToFill);
		GUI.Label(new Rect(Screen.width - enduranceBar.width - 45, Screen.height - 85, 100, 25), player2.enduranceCurrent + "/" + player2.enduranceMax);
		
		switch (turnStage) //Depending on the turn stage, the GUI does different things
		{
			
		case TurnStage.PlayerStart:
			
			//Player must hit button to start their turn
			string playerString = "";
			if (playerTurn == PlayerIndex.One) playerString = "Player 1";
			else playerString = "Player 2";
			if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2, 100, 20), playerString + " Start!"))
			{
				turnStage = TurnStage.RecoverEndurance;
			}
			
			
			break;
			
		case TurnStage.RecoverEndurance:
			//Labels the stage
			GUI.Label(new Rect(Screen.width / 2 - 100, 50, 200, 20), "Recovery Stage");
			
			break;
			
		case TurnStage.ChooseAttack:
			//Labels the stage
			GUI.Label(new Rect(Screen.width / 2 - 100, 50, 200, 20), "Choose Attack");
			
			for (int i = 0; i < attacker.attackList.Count; i++) //Goes through all the attack options for the current attacker
			{
				//Creates a button for each attack option
				AttackOption atk = attacker.attackList[i];
				float cellHeight = 60;
				//If player has enough endurance, button is green. If not, button is red
				if (attacker.enduranceCurrent < atk.enduranceCost) GUI.color = Color.red;
				else GUI.color = Color.green;
				float yValue = (Screen.height / 2 - (float)attacker.attackList.Count / 2 * cellHeight) + (i * cellHeight) + 10;
				if (GUI.Button(new Rect(Screen.width / 2 - 200, yValue, 400, 40), "|" + atk.attackName + "| " + atk.attackGroup.ToString() + " " + atk.attackMethod.ToString() + " Power: " + atk.basePower + " Cost: " + atk.enduranceCost + "\n" + atk.attackDescription))
				{
					if(attacker.enduranceCurrent >= atk.enduranceCost){
						currentAttack = atk;
						turnStage = TurnStage.ChooseAttackType;
					}
				}
			}
			//Player can choose to not attack
			GUI.color = Color.green;
			if(GUI.Button (new Rect(Screen.width / 2 - 100, Screen.height / 2 + 200, 200, 40), "Don't Attack")){
				currentAttack = null;
				turnStage = TurnStage.ChooseDefense;
			}
			
			
			break;
			
		case TurnStage.ChooseAttackType:
			
			//Player can choose either rock, paper, or scissors for their attack type
			//Creates a button for each
			GUI.Label(new Rect(Screen.width / 2 - 100, 50, 200, 20), "Jan-ken Attack!");
			float cellHeight2 = 60;
			float yValue2 = (Screen.height / 2 - 3f / 2 * cellHeight2) + (0 * cellHeight2) + 10;
			if (GUI.Button(new Rect(Screen.width / 2 - 200, yValue2, 400, 40), "ROCK"))
			{
				actionAttack = ActionType.Rock; 
				turnStage = TurnStage.ExecuteAttack;
			}
			
			cellHeight2 = 60;
			yValue2 = (Screen.height / 2 - 3f / 2 * cellHeight2) + (1 * cellHeight2) + 10;
			if (GUI.Button(new Rect(Screen.width / 2 - 200, yValue2, 400, 40), "PAPER"))
			{
				actionAttack = ActionType.Paper; 
				turnStage = TurnStage.ExecuteAttack;
			}
			
			cellHeight2 = 60;
			yValue2 = (Screen.height / 2 - 3f / 2 * cellHeight2) + (2 * cellHeight2) + 10;
			if (GUI.Button(new Rect(Screen.width / 2 - 200, yValue2, 400, 40), "SCISSORS"))
			{
				actionAttack = ActionType.Scissors; 
				turnStage = TurnStage.ExecuteAttack;
			}
			
			
			break;
			
			
		case TurnStage.ChooseDefense:
			//Labels the stage
			GUI.Label(new Rect(Screen.width / 2 - 100, 50, 200, 20), "Choose Defense");
			
			for (int i = 0; i < attacker.defendList.Count; i++) //Goes through all the defend options for the current player
			{
				//Creates a button for each defend option
				DefendOption def = attacker.defendList[i];
				float cellHeight = 60;
				//If player has enough endurance, button is green, if not, button is red
				if (attacker.enduranceCurrent < def.enduranceCost) GUI.color = Color.red;
				else GUI.color = Color.green;
				float yValue = (Screen.height / 2 - (float)attacker.attackList.Count / 2 * cellHeight) + (i * cellHeight) + 10;
				if (GUI.Button(new Rect(Screen.width / 2 - 200, yValue, 400, 40), "|" + def.defendName + "| " + def.defendGroup.ToString() + " "  + " Defense strength: " + def.baseDefense + " Cost: " + def.enduranceCost + "\n" + def.defendDescription))
				{
					if(attacker.enduranceCurrent >= def.enduranceCost){
						currentDefend = def;
						turnStage = TurnStage.ChooseDefenseType;
						attacker.UseEndurance(currentDefend.enduranceCost);
						
						
					}
				}
			}
			//Player can choose to not defend
			GUI.color = Color.green;
			if(GUI.Button (new Rect(Screen.width / 2 - 100, Screen.height / 2 + 100, 200, 40), "Don't Defend")){
				currentDefend = null;
				turnStage = TurnStage.ChooseDefenseType;
			}
			
			break;
			
			
		case TurnStage.ChooseDefenseType:
			
			//If the player has chosen to defend, they can choose rock, paper, or scissors for defend type
			if(currentDefend != null){
				
				GUI.Label(new Rect(Screen.width / 2 - 100, 50, 200, 20), "Jan-ken Attack!");
				cellHeight2 = 60;
				yValue2 = (Screen.height / 2 - 3f / 2 * cellHeight2) + (0 * cellHeight2) + 10;
				if (GUI.Button(new Rect(Screen.width / 2 - 200, yValue2, 400, 40), "ROCK"))
				{
					actionDefend = ActionType.Rock; 
					turnStage = TurnStage.PlayerStart;
					if(playerTurn == PlayerIndex.One) playerTurn = PlayerIndex.Two;
					else playerTurn = PlayerIndex.One;
					DefendParticleGo();
					
				}
				
				cellHeight2 = 60;
				yValue2 = (Screen.height / 2 - 3f / 2 * cellHeight2) + (1 * cellHeight2) + 10;
				if (GUI.Button(new Rect(Screen.width / 2 - 200, yValue2, 400, 40), "PAPER"))
				{
					actionDefend = ActionType.Paper; 
					turnStage = TurnStage.PlayerStart;
					if(playerTurn == PlayerIndex.One) playerTurn = PlayerIndex.Two;
					else playerTurn = PlayerIndex.One;
					DefendParticleGo();
				}
				
				cellHeight2 = 60;
				yValue2 = (Screen.height / 2 - 3f / 2 * cellHeight2) + (2 * cellHeight2) + 10;
				if (GUI.Button(new Rect(Screen.width / 2 - 200, yValue2, 400, 40), "SCISSORS"))
				{
					actionDefend = ActionType.Scissors; 
					turnStage = TurnStage.PlayerStart;
					if(playerTurn == PlayerIndex.One) playerTurn = PlayerIndex.Two;
					else playerTurn = PlayerIndex.One;
					DefendParticleGo();
				}
				
				
			}
			else //If there was no defend chosen, then we just skip ahead
			{
				turnStage = TurnStage.PlayerStart;
				if(playerTurn == PlayerIndex.One) playerTurn = PlayerIndex.Two;
				else playerTurn = PlayerIndex.One;
			}
			break;
			
		case TurnStage.EndOfBattle:
			
			//Displays which player won and provides a button that will take players to the upgrade screen
			playerString = "";
			if (losingPlayer.playerIndex == PlayerIndex.Two) playerString = "Player 1";
			else playerString = "Player 2";
			if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2, 100, 20), playerString + " Wins!"))
			{
				battleEnd = true;
			}
			break;
			
		}
	}
	
	void DefendParticleGo() //Method that creates particles after player chooses a defense
	{
		switch (currentDefend.defendGroup) //Intensity of particles increases with higher level defends
		{
		case ActionGroup.Group1:
			Instantiate(defend1Particle, attacker.profileObject.transform.position, Quaternion.identity);
			break;
		case ActionGroup.Group2:
			Instantiate(defend2Particle, attacker.profileObject.transform.position, Quaternion.identity);
			break;
		case ActionGroup.Group3:
			Instantiate(defend3Particle, attacker.profileObject.transform.position, Quaternion.identity);
			break;
		}
	}
	
	//Does turn stage stuff
	void ExecuteTurn()
	{
		//Execute the turn based on which stage we're in
		switch (turnStage)
		{
			
		case TurnStage.RecoverEndurance:
			
			if (turnCounter == 0) //Recover endurance and start particle effect
			{
				attacker.RecoverEndurance(attacker.enduranceRegen);
				targetEnduranceReached = false;
				Instantiate(enduranceParticle, attacker.profileObject.transform.position, Quaternion.identity);
				turnCounter++;
			}
			
			if (targetEnduranceReached) //Once the endurance bar has reached its target value, we move on
			{
				turnStage = TurnStage.ChooseAttack;
				turnCounter = 0;
			}
			
			
			break;
			
		case TurnStage.ExecuteAttack:
			
			//We now attack based on the attack option and type previously selected
			if(turnCounter == 0){
				
				targetHealthReached = false;
				targetEnduranceReached = false;
				if ( currentAttack.attackMethod == AttackMethod.Attack){
					attacker.UseEndurance(currentAttack.enduranceCost);
					//check for defense up
					int damage = currentAttack.basePower;
					
					switch (currentAttack.attackGroup)
					{
					case ActionGroup.Group1:
						Instantiate(attack1Particle, defender.profileObject.transform.position, Quaternion.identity);
						break;
					case ActionGroup.Group2:
						Instantiate(attack2Particle, defender.profileObject.transform.position, Quaternion.identity);
						break;
					case ActionGroup.Group3:
						Instantiate(attack3Particle, defender.profileObject.transform.position, Quaternion.identity);
						break;
					}
					
					if(currentDefend != null){
						float defence = currentDefend.baseDefense;
						//rock paper scissors
						//attacker wins, defense 50%
						//tie, defense 100%
						//defender win, defense 150%
						
						float efffectiveness = 1.0f;
						//a tie
						if(actionAttack == actionDefend) efffectiveness = 1.0f;
						//cases when attacker wins
						if(actionAttack == ActionType.Rock && actionDefend == ActionType.Scissors) efffectiveness = 0.5f;
						if(actionAttack == ActionType.Paper && actionDefend == ActionType.Rock) efffectiveness = 0.5f;
						if(actionAttack == ActionType.Scissors && actionDefend == ActionType.Paper) efffectiveness = 0.5f;
						//cases defender wins
						if(actionDefend == ActionType.Rock && actionAttack == ActionType.Scissors) efffectiveness = 1.5f;
						if(actionDefend == ActionType.Paper && actionAttack == ActionType.Rock) efffectiveness = 1.5f;
						if(actionDefend == ActionType.Scissors && actionAttack == ActionType.Paper) efffectiveness = 1.5f;
						
						defence *= efffectiveness;
						Debug.Log (efffectiveness);
						Debug.Log (defence);
						damage -= (int)defence;
					}
					
					if (damage < 0) {
						if(-damage > attacker.hpCurrent) damage = -attacker.hpCurrent;
						attacker.LoseHP(-damage);
						defender.chiPoints += damage / 10;
					}
					else {
						if(damage > defender.hpCurrent) damage = defender.hpCurrent;
						defender.LoseHP(damage);
						attacker.chiPoints += damage / 10;
					}
					
					
					
				}
				//If meditated instead of attacked
				else if (currentAttack.attackMethod == AttackMethod.Meditation){
					
					attacker.RecoverEndurance(currentAttack.basePower);
					Instantiate(enduranceParticle, attacker.profileObject.transform.position, Quaternion.identity);
					currentDefend = new DefendOption("gimp", currentAttack.attackGroup, -((int)currentAttack.attackGroup)*5, 0, "gimped by meditation");
					
				}
				turnCounter++;
				
			}
			
			if(targetEnduranceReached){
				//If ether player has reached zero HP, then the battle is over
				if(player1.hpCurrent <= 0 || player2.hpCurrent <= 0) 
				{
					//Set the losing player
					if (player1.hpCurrent <= 0) 
					{
						losingPlayer = player1;
					}
					else
					{
						losingPlayer = player2;
					}
					turnStage = TurnStage.EndOfBattle;
					turnCounter = 0;
					break;
					
				}
				
				if (currentAttack.attackMethod == AttackMethod.Attack) turnStage = TurnStage.ChooseDefense;
				else {
					turnStage = TurnStage.ChooseDefenseType;
				}
				turnCounter = 0;
			}
			
			break;
			
		case TurnStage.EndOfBattle:
			//Battle is over, set the loser on fire and wait for someone to hit the button
			if (turnCounter == 0)
			{
				Instantiate(dyingFlames, losingPlayer.profileObject.transform.position, Quaternion.identity);
				turnCounter++;
			}
			
			
			break;
			
			
			
		}
		if( battleEnd) EndBattle();
	}
	
	public void EndBattle(){
		if(player1.hpCurrent <=0){
			
			player1.victor = false;
			player2.victor = true;
			
			
		}
		
		else{
			
			player2.victor = false;
			player1.victor = true;
			
		}
		UpgradeScreen.getPlayer((Player)player1, out UpgradeScreen.temp1);
		UpgradeScreen.getPlayer((Player)player2, out UpgradeScreen.temp2);
		Application.LoadLevel("UpgradeScreen");
		
		
	}
	
	
	
}
