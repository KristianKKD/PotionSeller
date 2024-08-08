using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class ShopBook : MonoBehaviour {

    public GameObject ingredientsTab;
    public GameObject golemsTab;

    //prefabs
    public Purchase basicGolem;

    public GameObject purchaseButton;
    public TMP_Text purchaseText;
    public GameObject notEnoughMoney;
    public TMP_Text notEnoughMoneyText;

    public TMP_Text currentMoney;

    List<GameObject> tabs;
    public List<Purchase> pages;

    Button lastButton;

    public GameObject buyPrefab;
    public Step buyIngredient;
    public int buyPrice;

    private void Awake() {
        tabs = new List<GameObject>() { ingredientsTab, golemsTab };
        pages = new List<Purchase>(GetComponentsInChildren<Purchase>(true));
        ResetTabs();
        ResetPages();

        GetComponent<Canvas>().worldCamera = Camera.main;
    }

    private void FixedUpdate() {
        currentMoney.text = "Money\n$" + References.r.pu.money.ToString();
    }

    void ResetTabs() {
        foreach (GameObject t in tabs)
            t.gameObject.SetActive(false);
    }

    void ResetPages() {
        foreach (Purchase p in pages)
            p.gameObject.SetActive(false);
        TogglePurchaseButton();
    }

    public void TogglePurchaseButton() {
        if (buyPrefab == null && buyIngredient == null) {
            purchaseButton.SetActive(false);
            notEnoughMoney.SetActive(false);
            return;
        }

        if (References.r.pu.money >= buyPrice) {
            purchaseText.text = "Purchase ($" + buyPrice.ToString() + ")";

            purchaseButton.SetActive(true);
            notEnoughMoney.SetActive(false);
        } else {
            notEnoughMoneyText.text = "Not Enough Money ($" + buyPrice.ToString() + ")";

            purchaseButton.SetActive(false);
            notEnoughMoney.SetActive(true);
        }
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

    public void ClickedPurchase() {
        if (buyPrefab == null && buyIngredient == null) {
            Debug.Log("PREFAB NULL");
            return;
        }
        
        References.r.pu.money -= buyPrice;

        if (buyPrefab != null && buyPrefab.GetComponent<GolemBase>() != null) {
            GameObject go = Instantiate(buyPrefab, References.r.itemSpawnParent);
            go.transform.position = Vector3.up;
        } else if (buyPrefab == null && buyIngredient != null) {
            References.r.c.AddShelf(buyIngredient, 10);
        }
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

    public void PressedPageTab(Button b, GameObject page) {
        ResetPages();
        page.gameObject.SetActive(true);
        ChangeColour(b);
        TogglePurchaseButton();
    }

}
