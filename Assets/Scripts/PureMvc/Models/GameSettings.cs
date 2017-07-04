using UnityEngine;

//[CreateAssetMenu(fileName = "GameSettings", menuName = "Svitla/Create GameSettings", order = 1)]
public class GameSettings : ScriptableObject
{
	public int TotalItems; // Total amount of starting items
	public int TakePerTurn; // Max items per turn
	public bool IsLastWinner; // Defines if last player is winner or loser
}
