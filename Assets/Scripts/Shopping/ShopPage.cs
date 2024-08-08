using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPage : MonoBehaviour {

    public TMP_Text title;
    public TMP_Text description;
    public TMP_Text properties;
    public Image indicator;

    private void Awake() {
        Step s = GetComponent<Purchase>().ingredient;
        if (s != null) {
            title.text = s.name + " x10";
            description.text = s.description;
            properties.text = IngredientDisplay.CollectPropertyText(s.propertiesApplied);
        }

        if (indicator != null) {
            Color c = s.colourMix;
            c.a = 1;
            indicator.color = c;
        }
    }

}
