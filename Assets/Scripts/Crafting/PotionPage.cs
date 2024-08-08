using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PotionPage : MonoBehaviour {

    public TMP_Text propertiesText;
    public Image indicator;

    public Potion p;
    Color emptyColor;

    private void Awake() {
        emptyColor = indicator.color;
    }

    public void UpdateDisplay() {
        propertiesText.text = "";
        indicator.color = emptyColor;

        propertiesText.text = IngredientDisplay.CollectPropertyText(p.currentProperties);

        indicator.color = p.indicatorColor;
    }

}
