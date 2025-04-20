using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GolemBase : MonoBehaviour {

    public enum State {
        Idle,
        Ingredient,
        Bench,
        ExportingPotion,
        Error
    }

    GameObject target;

    public State stateTarget = State.Idle;

    NavMeshAgent agent;
    Rigidbody rb;

    ReadRecipe myRecipe;
    int recipeIndex = 0;

    public bool grounded = true;
    public bool isPaused = false;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        agent.enabled = false;
        grounded = true;
    }

    private void FixedUpdate() {
        if (myRecipe == null || !grounded || stateTarget == State.Error || !agent.enabled || isPaused)
            return;


        if (stateTarget == State.Idle) {
            if (recipeIndex == myRecipe.savedPotion.currentSteps.Count) { //finished potion
                print("Done recipe, exporting...");
                stateTarget = State.ExportingPotion;
                CollectItem(References.r.bottle);
            } else {
                Debug.Log("Searching for:" + myRecipe.savedPotion.currentSteps[recipeIndex]);
                stateTarget = State.Ingredient;
                CollectItem(myRecipe.savedPotion.currentSteps[recipeIndex]);
                StartCoroutine(Pause());
            }

            return;
        }

        if (agent.remainingDistance > agent.stoppingDistance)
            return;
        
        if (stateTarget == State.Ingredient) {
            Debug.Log("Found:" + myRecipe.savedPotion.currentSteps[recipeIndex]);
            recipeIndex++;
            stateTarget = State.Idle;

            Shelf s = target.GetComponent<Shelf>();
            if (s != null) {
                s.Interact(false);
                s = null;
            } else
                Destroy(target.gameObject); //destroy ground object

        } else if (stateTarget == State.ExportingPotion) {
            Potion p = target.GetComponent<Potion>();
            if (p != null) {
                foreach (Step s in myRecipe.savedPotion.currentSteps)
                    p.AddStep(s);
                //p.Copy(myRecipe.savedPotion);
            }
            recipeIndex = 0;
            stateTarget = State.Idle;
        } else {

        }

        StartCoroutine(Pause());
        target = null;
    }

    public void PickedUp() {
        agent.enabled = false;
        grounded = false;
    }

    public void Dropped() {
        grounded = true;
        rb.isKinematic = false;
    }

    void GiveTask(ReadRecipe r) {
        if (r.savedPotion.currentSteps.Count == 0)
            return;

        myRecipe = r;
        //r.gameObject.SetActive(false);
        stateTarget = State.Idle;

        recipeIndex = 0;
    }

    void CollectItem(Step s) {
        target = null;

        Shelf[] shelves = FindObjectsOfType<Shelf>();
        foreach (Shelf sh in shelves) {
            if (sh.ingredient == s && sh.quantityHeld > 0) {
                target = sh.gameObject;
                break;
            }
        }

        if (target == null) {
            Ingredient[] items = FindObjectsOfType<Ingredient>();
            foreach (Ingredient i in items) {
                if (i.ingredientStep == s) {
                    if (i.ingredientStep == References.r.bottle && i.gameObject.GetComponent<Potion>().currentSteps.Count > 0)
                        continue;
                    target = i.gameObject;
                    break;
                }
            }
        }

        if (target == null) {
            Debug.Log("Failed to find: " + s.name);
            stateTarget = State.Error;
            return;
        }

        agent.SetDestination(target.transform.position);
    }

    private IEnumerator Pause() {
        isPaused = true;
        yield return new WaitForSeconds(0.5f);
        isPaused = false;
    }

    private void OnTriggerEnter(Collider other) {
        ReadRecipe r = other.gameObject.GetComponent<ReadRecipe>();
        if (r != null && !r.GetComponent<Rigidbody>().isKinematic) {
            Debug.Log("found recipe");
            agent.enabled = true;
            rb.isKinematic = true;
            GiveTask(r);
            Instantiate(References.r.rp.gameObject, References.r.respawnPotionParent);
        } else if (!grounded) {
            agent.enabled = true;
            rb.isKinematic = true;
            grounded = true;

            stateTarget = State.Idle;

            if (target != null && stateTarget == State.Ingredient || stateTarget == State.ExportingPotion)
                agent.SetDestination(target.transform.position);
        }
    }
}
