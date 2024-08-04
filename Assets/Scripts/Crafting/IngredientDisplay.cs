using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class IngredientDisplay : MonoBehaviour {

    public TMP_Text title;
    public TMP_Text subtitle;
    public TMP_Text description;
    public TMP_Text properties;

    public Image img;

    private void Awake() {
        ResetDisplay();
    }

    void ResetDisplay() {
        title.text = "";
        description.text = "";
        properties.text = "";
        img.enabled = false;
    }

    public void UpdateDisplay(Step s) {
        ResetDisplay();

        if (s == null)
            return;

        img.enabled = true;
        Color c = s.colourMix;
        img.color = new Color(c.r, c.g, c.b, 1);
        title.text = s.name;
        subtitle.text = s.text;
        description.text = s.description;

        List<Property> seen = new List<Property>();
        List<int> counts = new List<int>();

        for (int i = 0; i < s.propertiesApplied.Count; i++) {
            Property p = s.propertiesApplied[i];
            if (seen.Contains(p))
                counts[seen.IndexOf(p)] += 1;
            else {
                counts.Add(1);
                seen.Add(p);
            }
        }

        for (int i = 0; i < counts.Count; i++) {
            int num = counts[i];
            string numString = " x" + num.ToString();
            if (num <= 1)
                numString = "";

            properties.text += seen[i].name + numString + "\n";
        }

    }

}
