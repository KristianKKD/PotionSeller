using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class References : MonoBehaviour {

    public static References r;

    public GameObject player;
    public Pickup p;
    public QuestManager qm;
    public ReadRecipe playerTracking;
    public PlayerUnlocks playerUnlocks;
    public IngredientDisplay id;
    public MessageManager mm;

    public GameObject templateIngredientPrefab;
    public GameObject questPagePrefab;

    public Transform itemSpawnParent;

    public Step bottle;

    private void Awake() {
        p = player.GetComponentInChildren<Pickup>();
        r = this;
    }

}
