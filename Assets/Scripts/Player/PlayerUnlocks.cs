using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnlocks : MonoBehaviour {

    public int money = 0;

    public List<Quest> availableQuests = new List<Quest>();


    private void Update() {
        if (Input.GetKey(KeyCode.LeftShift)) {
            if (Input.GetKeyDown(KeyCode.Equals))
                money += 1000;
            if (Input.GetKeyDown(KeyCode.P))
                Instantiate(References.r.potionPrefab, References.r.respawnPotionParent);
        }

    }
}
