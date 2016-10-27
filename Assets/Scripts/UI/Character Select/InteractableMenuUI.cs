using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InteractableMenuUI : MonoBehaviour {
    public bool selected;
    public GameObject highlighter;
    public Transform contextPanel;

    private int j = 0;
    void Update() {
        if (selected) {
            if (j < 1) {
                j++;
                Instantiate(highlighter, GetComponent<RectTransform>().position, Quaternion.identity);
            }
        }
    }

    public void Recieve(string input, bool isDualshock, out bool ret) {
        if (!isDualshock) {

        }
        else {
            if (Input.GetButtonDown(input)) {
                ret = true;
            }
        }
        ret = false;
    }


}
