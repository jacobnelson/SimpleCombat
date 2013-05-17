using UnityEngine;
using System.Collections;

public class DefendOption {

	public ActionGroup defendGroup; //What group level this belongs to
	public ActionType defendType; //A container for what type this attack will be at any given stage. Rock, paper, or scissors
	
	public int baseDefense; //Base defense amount
	public int enduranceCost; //How much endurance this option costs
	
	public string defendName; //Name of defense option
	public string defendDescription; //Description of defend option
	
	
	public DefendOption(string defendName, ActionGroup defendGroup, int baseDefense, int enduranceCost, string defendDescription)
	{
		this.defendName = defendName;
		this.defendGroup = defendGroup;
		this.baseDefense = baseDefense;
		this.enduranceCost = enduranceCost;
		this.defendDescription = defendDescription;
	}
}
