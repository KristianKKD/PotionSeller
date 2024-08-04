using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadRotate : MonoBehaviour {

    public GameObject head;

    public float camSens = 150F;
    public float camSmooth = 0.2F;

    float vertRot = 0f;
    float horiRot = 0f;

    float oldVertRot;

    private void Awake() {
        //camSens = FindObjectOfType<Settings>().sensitivity;
        //AudioListener.volume = (FindObjectOfType<Settings>().volume + 0.01f) / 100f;
    }

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() {
        if (References.r.p.rotating)
            return;

        float inpX = Input.GetAxis("Mouse X") * camSens * Time.deltaTime;
        float inpY = Input.GetAxis("Mouse Y") * camSens * Time.deltaTime;

        vertRot -= inpY;
        vertRot = Mathf.Lerp(vertRot, oldVertRot, camSmooth);
        vertRot = Mathf.Clamp(vertRot, -90, 90);

        horiRot += inpX;
    }

    private void LateUpdate() {
        head.transform.rotation = Quaternion.Euler(vertRot, horiRot, 0);
        oldVertRot = vertRot;
    }
}
