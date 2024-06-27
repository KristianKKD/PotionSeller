using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class References : MonoBehaviour {

    public static References r;

    public GameObject player;
    public Pickup p;

    public GameObject templateIngredientPrefab;

    public Potion basePotion;
    public Potion customPotion;

    private void Awake() {
        p = player.GetComponentInChildren<Pickup>();
        r = this;
    }

}
