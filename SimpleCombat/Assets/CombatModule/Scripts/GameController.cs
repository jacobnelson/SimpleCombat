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
	ChooseDefenseType
}

public class GameController : MonoBehaviour {
	
	public Profile player1;
	public Profile player2;
	Profile attacker;
	Profile defender;
	
	//the card that is passed into the upgrade scene to be upgraded
	static public PlayerCard temp;
	
	AttackOption currentAttack;
	DefendOption currentDefend;
	
	public Transform player1Object;
	public Transform player2Object;
	
	public Texture2D healthBar;
	public Texture2D enduranceBar;
	public float healthBar1Width;
	public float healthBar2Width;
	public float enduranceBar1Height;
	public float enduranceBar2Height;
	
	bool targetHealthReached = false;
	bool targetEnduranceReached = false;
	
	
	public PlayerIndex playerTurn = PlayerIndex.One;
	
	public TurnStage turnStage = TurnStage.PlayerStart;
	
	public float turnCounter = 0;
	public float turnRate = 4;
	
	//run this once to pass stats from title to game
	public static void getPlayer(PlayerCard importPlayer){
		temp = importPlayer;
		
		
	}
	
	// Use this for initialization
	void Start () {
		//Randomly decide which player goes first
		//if (Random.Range(1,3) == 2) playerTurn = PlayerIndex.One;
		//else playerTurn = PlayerIndex.Two;
		
		
		
		player1 = new Player();
		
		if (temp == null)
		{
			temp = GameObject.Find("default1").GetComponent<PlayerCard>();
		}
		
		PassStats((Player)player1, out player1);
		
		player2 = new Player();
		player2.AddAttackOption("Kick", AttackMethod.Attack, ActionGroup.Group1, 3, 1, "Kick ya in the pants!");
		player2.AddDefendOption("Mommy", ActionGroup.Group1, 1, 1, "Awww.. needs his mommy to protect him.");
		
		
		
		healthBar1Width = healthBar.width;
		healthBar2Width = healthBar.width;
		enduranceBar1Height = 0;
		enduranceBar2Height = 0;
		
	}
	
	//takes information from card and gives it to player
	void PassStats(Player playerIn, out Profile playerOut){
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

		
		
		playerOut = playerIn;
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
		//Create the two hp bars for each player
		float hpRatio = (float)player1.hpCurrent / (float)player1.hpMax;
		float targetBarWidth = healthBar.width * hpRatio;
		float deltaValue = targetBarWidth - healthBar1Width;
		healthBar1Width += deltaValue / 8;
		if (Mathf.Abs(deltaValue) < .02f) healthBar1Width = targetBarWidth;
		if (healthBar1Width == targetBarWidth && playerTurn == PlayerIndex.Two) targetHealthReached = true;
		GUI.DrawTexture(new Rect(10, Screen.height - healthBar.height - 20, healthBar1Width, healthBar.height), healthBar, ScaleMode.StretchToFill);
	
		hpRatio = (float)player2.hpCurrent / (float)player2.hpMax;
		targetBarWidth = healthBar.width * hpRatio;
		healthBar2Width += (targetBarWidth - healthBar2Width) / 8;
		if (Mathf.Abs(targetBarWidth - healthBar2Width) < .02f) healthBar2Width = targetBarWidth;
		if (healthBar1Width == targetBarWidth && playerTurn == PlayerIndex.One) targetHealthReached = true;
		GUI.DrawTexture(new Rect(Screen.width - healthBar2Width - 10, Screen.height - healthBar.height - 20, healthBar2Width, healthBar.height), healthBar, ScaleMode.StretchToFill);

		float enduranceRatio = (float)player1.enduranceCurrent / (float)player1.enduranceMax;
		float barHeight = enduranceBar.height * enduranceRatio;
		enduranceBar1Height += (barHeight - enduranceBar1Height) / 8;
		if (Mathf.Abs(barHeight - enduranceBar1Height) < .02f) enduranceBar1Height =  barHeight;
		if (enduranceBar1Height == barHeight && playerTurn == PlayerIndex.One) targetEnduranceReached = true;
		GUI.DrawTexture(new Rect(10, Screen.height - enduranceBar1Height - 60, enduranceBar.width, enduranceBar1Height), enduranceBar, ScaleMode.StretchToFill);

		enduranceRatio = (float)player2.enduranceCurrent / (float)player2.enduranceMax;
		barHeight = enduranceBar.height * enduranceRatio;
		enduranceBar2Height += (barHeight - enduranceBar2Height) / 8;
		if (Mathf.Abs(barHeight - enduranceBar2Height) < .02f) enduranceBar2Height = barHeight;
		if (enduranceBar2Height == barHeight && playerTurn == PlayerIndex.Two) targetEnduranceReached = true;
		GUI.DrawTexture(new Rect(Screen.width - enduranceBar.width - 10, Screen.height - enduranceBar2Height - 60, enduranceBar.width, enduranceBar2Height), enduranceBar, ScaleMode.StretchToFill);
		
		
		switch (turnStage)
		{
			
		case TurnStage.PlayerStart:
			
			string playerString = "";
			if (playerTurn == PlayerIndex.One) playerString = "Player 1";
			else playerString = "Player 2";
			if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2, 100, 20), playerString + " Start!"))
			{
				turnStage = TurnStage.RecoverEndurance;
			}
			
			
			break;
			
		case TurnStage.RecoverEndurance:
			
			GUI.Label(new Rect(Screen.width / 2 - 100, 50, 200, 20), "Recovery Stage");
			
			break;
			
		case TurnStage.ChooseAttack:
			
			GUI.Label(new Rect(Screen.width / 2 - 100, 50, 200, 20), "Choose Attack");
			
			for (int i = 0; i < attacker.attackList.Count; i++)
			{
				AttackOption atk = attacker.attackList[i];
				float cellHeight = 60;
				float yValue = (Screen.height / 2 - (float)attacker.attackList.Count / 2 * cellHeight) + (i * cellHeight) + 10;
				if (GUI.Button(new Rect(Screen.width / 2 - 200, yValue, 400, 40), "|" + atk.attackName + "| " + atk.attackGroup.ToString() + " " + atk.attackMethod.ToString() + " Power: " + atk.basePower + " Cost: " + atk.enduranceCost + "\n" + atk.attackDescription))
				{
					currentAttack = atk;
					turnStage = TurnStage.ChooseAttackType;
				}
			}
			
			
			break;
			
		case TurnStage.ChooseAttackType:
			
			
			break;
			
			
		case TurnStage.ChooseDefense:
			
			
			break;
			
			
		case TurnStage.ChooseDefenseType:
			
			
			
			break;
			
			
			
		}
	}
	
	void ExecuteTurn()
	{
		//Execute the turn based on which stage we're in
		switch (turnStage)
		{
			
		case TurnStage.RecoverEndurance:
			
			if (turnCounter == 0)
			{
				attacker.RecoverEndurance(attacker.enduranceRegen);
				targetEnduranceReached = false;
				turnCounter++;
			}
			
			if (targetEnduranceReached)
			{
				turnStage = TurnStage.ChooseAttack;
			}
			
			
			break;
			
		case TurnStage.ExecuteAttack:
			
			
			
			
			break;
			
			
		}
	}
	
	
	
	
}
