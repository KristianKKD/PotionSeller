using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RewardType {
    Item,
    Payout,
}

[CreateAssetMenu(fileName = "Reward", menuName = "Quests/Reward", order = 1)]
public class Reward : ScriptableObject {
    public string rewardText;

    public RewardType rewardType;
    public Step itemPayout;
    public int itmeQuantity = 10;
    public int payout = 1000;
}
