using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Quests/Quest", order = 2)]
public class Quest : ScriptableObject {
    public string title;
    public string description;
    public List<Reward> rewards = new List<Reward>();
    public List<Potion> deliverables = new List<Potion>();
    public int payout;
}
