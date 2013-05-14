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
	
	
	public int hpCurrent = 30;
	public int _hpMax = 30;
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
	
	public List<AttackOption> attackList = new List<AttackOption>();
	public List<DefendOption> defendList = new List<DefendOption>();
	
	
	public void AddAttackOption(string attackName, AttackMethod attackMethod, ActionGroup attackGroup, int baseDamage, int enduranceCost, string attackDescription)
	{
		AttackOption atkOpt = new AttackOption(attackName, attackMethod, attackGroup, baseDamage, enduranceCost, attackDescription);
		attackList.Add(atkOpt);
	}
	
	public void AddAttackOption(AttackOption attopt){
		attackList.Add (attopt);
		
	}
	
	public void AddDefendOption(string defendName, ActionGroup defendGroup, int baseDefense, int enduranceCost, string defendDescription)
	{
		DefendOption defOpt = new DefendOption(defendName, defendGroup, baseDefense, enduranceCost, defendDescription);
		defendList.Add(defOpt);
	}
	
	public void AddDefendOption(DefendOption defopt){
		defendList.Add (defopt);
		
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
	
	
	
}
