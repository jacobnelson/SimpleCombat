using UnityEngine;
using System.Collections;

public class PlayerCard : MonoBehaviour{
	
	//card stats assigned in inspector for now
	
	//card chi level
	//starts at 1 and is leveled up
	public int level = 1;
	
	//for assignment purposes:
	public string att1Name = "Punch";
	public string att1Discript = "punch ya";
	public int att1Power = 10;
	
	public string att2Name = "kick";
	public string att2Discript = "kick you in the pants!";
	public int att2Power = 20;
	
	public string att3Name = "Sword";
	public string att3Discript = "swish swish klang klang";
	public int att3Power = 30;
	
	public string def1Name = "Mommy";
	public string def1Discript = "aww... someone needs his mommy";
	public int def1Power = 10;
	
	public string def2Name = "block";
	public string def2Discript = "wax on, wax off";
	public int def2Power = 20;
	
	public string def3Name = "shield";
	public string def3Discript = "Made of wood";
	public int def3Power = 30;
	
	public string med1Name = "Calm Mind";
	public string med1Discript = "clear the mind you must.";
	public int med1Power = 1;
	
	public string med2Name = "relaxing tea";
	public string med2Discript = "mmmmmm...tea";
	public int med2Power = 2;
	
	public string med3Name = "sleep";
	public string med3Discript = "westful wewaxation at wast";
	public int med3Power = 3;
	
	//the three attack styles
	public AttackOption attack_1 = new AttackOption("Punch", AttackMethod.Attack, ActionGroup.Group1, 1, 1, "punch ya");
	public AttackOption attack_2 = new AttackOption("kick", AttackMethod.Attack, ActionGroup.Group2, 2, 2, "kick ya");
	public AttackOption attack_3 = new AttackOption("sword", AttackMethod.Attack, ActionGroup.Group3, 3, 3, "swish swish clang clang");
	
	
	//the three defense styles
	public DefendOption defend_1 = new DefendOption("mommy", ActionGroup.Group1, 1, 1 ,"Aww you need your mommy..");
	public DefendOption defend_2 = new DefendOption("Block", ActionGroup.Group1, 2, 2 ,"Block with arm");
	public DefendOption defend_3 = new DefendOption("Shield", ActionGroup.Group1, 3, 3 ,"made of wood");
	
	
	//the three meditate styles
	public AttackOption meditate_1 = new AttackOption("calm mind", AttackMethod.Attack, ActionGroup.Group1, 1, 0, "calms the mind");
	public AttackOption meditate_2 = new AttackOption("restful tea", AttackMethod.Attack, ActionGroup.Group1, 2, 0, "mmmmmm...tea");
	public AttackOption meditate_3 = new AttackOption("sleep", AttackMethod.Attack, ActionGroup.Group1, 3, 0, "west and wewaxation at wast!");
	
	
	//health stamina and stamina regen rate
	public int health = 50;
	public int maxStamina = 50;
	public int stamRegen = 3;
	
	public void Assign(){
		Debug.Log ("i did it");
		attack_1.attackName = att1Name;
		attack_1.attackDescription = att1Discript;
		attack_1.basePower = att1Power;
		
		attack_2.attackName = att2Name;
		attack_2.attackDescription = att2Discript;
		attack_2.basePower = att2Power;
		
		attack_3.attackName = att3Name;
		attack_3.attackDescription = att3Discript;
		attack_3.basePower = att3Power;
		
		defend_1.defendName = def1Name;
		defend_1.defendDescription = def1Discript;
		defend_1.baseDefense = def1Power;
		
		defend_2.defendName = def2Name;
		defend_2.defendDescription = def2Discript;
		defend_2.baseDefense = def2Power;
		
		defend_3.defendName = def3Name;
		defend_3.defendDescription = def3Discript;
		defend_3.baseDefense = def3Power;
		
		meditate_1.attackName = med1Name;
		meditate_1.attackDescription = med1Discript;
		meditate_1.basePower = med1Power;
		
		meditate_2.attackName = med2Name;
		meditate_2.attackDescription = med2Discript;
		meditate_2.basePower = med2Power;
		
		meditate_3.attackName = med3Name;
		meditate_3.attackDescription = med3Discript;
		meditate_3.basePower = med3Power;
	}
	
}
