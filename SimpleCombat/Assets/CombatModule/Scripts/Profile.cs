using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ActionType
{
	Rock,
	Paper,
	Scissors
}

public class Profile {
	
	
	public Transform profileObject;
	
	public PlayerIndex playerIndex;
	
	public int hpCurrent = 50;
	public int _hpMax = 50;
	public int hpMax
	{
		get { return _hpMax; }
		set 
		{
			_hpMax = value;
			hpCurrent = value;
		}
	}
	
	public int enduranceCurrent = 0;
	public int enduranceMax = 20;
	public int enduranceRegen = 2;
	
	public int chiLevel = 1;
	
	public int chiPoints = 0;
	public bool victor = false;
	
	public string name = "noName";
	
	public List<AttackOption> attackList = new List<AttackOption>();
	public List<DefendOption> defendList = new List<DefendOption>();
	
	
	public void AddAttackOption(string attackName, AttackMethod attackMethod, ActionGroup attackGroup, int baseDamage, int enduranceCost, string attackDescription)
	{
		AttackOption atkOpt = new AttackOption(attackName, attackMethod, attackGroup, baseDamage, enduranceCost, attackDescription);
		attackList.Add(atkOpt);
	}
	
	public void AddAttackOption(AttackOption attopt){
		AttackOption atkOpt = new AttackOption(attopt.attackName,attopt.attackMethod,attopt.attackGroup, attopt.basePower, attopt.enduranceCost, attopt.attackDescription);
		attackList.Add (atkOpt);
		
	}
	
	public void AddDefendOption(string defendName, ActionGroup defendGroup, int baseDefense, int enduranceCost, string defendDescription)
	{
		DefendOption defOpt = new DefendOption(defendName, defendGroup, baseDefense, enduranceCost, defendDescription);
		defendList.Add(defOpt);
	}
	
	public void AddDefendOption(DefendOption defopt){
		DefendOption defOpt = new DefendOption(defopt.defendName, defopt.defendGroup, defopt.baseDefense, defopt.enduranceCost, defopt.defendDescription);
		defendList.Add (defOpt);
		
	}
	
	public void Update()
	{
	}
	
	public virtual void ChooseAttack()
	{
		
	}
	
	public virtual void ChooseDefense()
	{
		
	}
	
	public virtual void RecoverEndurance(int amount)
	{
		enduranceCurrent += amount;
		if (enduranceCurrent > enduranceMax) 
		{
			enduranceCurrent = enduranceMax;
		}
	}
	
	public virtual void UseEndurance(int amount)
	{
		enduranceCurrent -= amount;
		if (enduranceCurrent < 0) 
		{
			enduranceCurrent = 0;
		}
	}
	
	public virtual void RecoverHP(int amount)
	{
		hpCurrent += amount;
		if (hpCurrent > hpMax) 
		{
			hpCurrent = hpMax;
		}
	}
	
	public virtual void LoseHP(int amount)
	{
		hpCurrent -= amount;
		if (hpCurrent < 0) 
		{
			hpCurrent = 0;
		}
	}
	
	
	
}
