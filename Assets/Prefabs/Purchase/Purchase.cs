using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Purchase : MonoBehaviour {

    public GameObject prefab;
    public Step ingredient;
    public int price;

    public void Activate(Button b) {
        References.r.sb.buyPrefab = prefab;
        References.r.sb.buyIngredient = ingredient;
        References.r.sb.buyPrice = price;
        References.r.sb.PressedPageTab(b, this.gameObject);
    }

}
