using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class References : MonoBehaviour {

    public static References r;

    public GameObject player;
    public Pickup p;
    public QuestManager q;

    public GameObject templateIngredientPrefab;
    public GameObject questPagePrefab;

    public Transform itemSpawnParent;

    private void Awake() {
        p = player.GetComponentInChildren<Pickup>();
        r = this;
        q = GetComponent<QuestManager>();
    }

}
