using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class References : MonoBehaviour {

    public static References r;

    public GameObject player;
    public Cabinets c;

    public QuestManager qm;
    public ReadRecipe pt;
    public PlayerUnlocks pu;
    public IngredientDisplay id;
    public MessageManager mm;
    public PotionPage pp;
    public ShopBook sb;
    public ReadRecipe rp;
    public XRInteractionManager xrc;

    public GameObject templateIngredientPrefab;
    public GameObject questPagePrefab;
    public GameObject potionPrefab;

    public Transform itemSpawnParent;
    public Transform respawnPotionParent;

    public Step bottle;
    public Property elemental;

    [HideInInspector]
    public Pickup p;

    private void Awake() {
        p = player.GetComponentInChildren<Pickup>();
        r = this;
    }

}
