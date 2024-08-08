using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Data;

public class IngredientDisplay : MonoBehaviour {

    public TMP_Text title;
    public TMP_Text description;
    public TMP_Text properties;
    public TMP_Text mutations;
    public TMP_Text counters;

    public List<GameObject> titles = new List<GameObject>();

    public Image img;

    private void Awake() {
        for (int i = 0; i < titles.Count; i++)
            titles[i].SetActive(false);
        ResetDisplay();
    }

    void ResetDisplay() {
        title.text = "";
        counters.text = "";
        description.text = "";
        properties.text = "";
        mutations.text = "";
        img.enabled = false;
    }

    public void UpdateDisplay(Step s) {
        ResetDisplay();

        if (s == null)
            return;

        for (int i = 0; i < titles.Count; i++)
            titles[i].SetActive(true);

        img.enabled = true;
        Color c = s.colourMix;
        img.color = new Color(c.r, c.g, c.b, 1);
        title.text = s.name;
        description.text = s.description;
        properties.text = CollectPropertyText(s.propertiesApplied);

        List<Property> counterProperties = new List<Property>();
        for (int i = 0; i < s.propertiesApplied.Count; i++)
            counterProperties.AddRange(s.propertiesApplied[i].counters);
        counters.text = CollectPropertyText(counterProperties);

        List<Property> possibleMutations = new List<Property>();
        for (int i = 0; i < s.propertiesApplied.Count; i++)
            possibleMutations.AddRange(s.propertiesApplied[i].chanceApplied);
        mutations.text = CollectPropertyText(possibleMutations);
    }

    public static string CollectPropertyText(List<Property> props) {
        string propertyText = "";

        List<Property> seen = new List<Property>();
        List<int> counts = new List<int>();

        for (int i = 0; i < props.Count; i++) {
            Property prop = props[i];
            if (seen.Contains(prop))
                counts[seen.IndexOf(prop)] += 1;
            else {
                counts.Add(1);
                seen.Add(prop);
            }
        }

        for (int i = 0; i < counts.Count; i++) {
            int num = counts[i];
            string numString = " x" + num.ToString();
            if (num <= 1 || seen[i] == References.r.elemental)
                numString = "";

            propertyText += "-" + seen[i].name + numString + "\n";
        }

        return propertyText;
    }

}
