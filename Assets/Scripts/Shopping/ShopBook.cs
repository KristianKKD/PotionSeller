using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopBook : MonoBehaviour {


    public GameObject ingredientsTab;
    public GameObject golemsTab;

    public GameObject chefPage;

    public TMP_Text money;

    List<GameObject> tabs;
    List<GameObject> pages;

    Button lastButton;

    public bool hovering = false;

    private void Awake() {
        tabs = new List<GameObject>() { ingredientsTab, golemsTab };
        pages = new List<GameObject>() { chefPage };
        ResetTabs();
        ResetPages();

        GetComponent<Canvas>().worldCamera = Camera.main;
    }

    private void Update() {
        money.text = "Money\n$" + References.r.playerUnlocks.money.ToString();
    }

    void ResetTabs() {
        foreach (GameObject t in tabs)
            t.gameObject.SetActive(false);
    }

    void ResetPages() {
        foreach (GameObject p in pages)
            p.gameObject.SetActive(false);
    }

    public void ChangeColour(Button b) {
        EventSystem.current.SetSelectedGameObject(null);

        Color offset = new Color(-0.2f, -0.2f, -0.2f);

        ColorBlock cb = b.colors;
        cb.normalColor += offset;
        b.colors = cb;

        if (lastButton != null) {
            ColorBlock lb = lastButton.colors;
            lb.normalColor -= offset;
            lastButton.colors = lb;
        }

        lastButton = b;
    }

    public void ClickedIngredientsTab(Button b) {
        ResetTabs();
        ingredientsTab.SetActive(true);
        ChangeColour(b);
    }

    public void ClickedGolemsTab(Button b) {
        ResetTabs();
        golemsTab.SetActive(true);
        ChangeColour(b);
    }

    public void ClickedChefPage(Button b) {
        ResetPages();
        chefPage.SetActive(true);
        ChangeColour(b);
    }

}
