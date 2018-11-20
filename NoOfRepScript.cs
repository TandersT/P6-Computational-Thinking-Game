using UnityEngine;
using UnityEngine.UI;

public class NoOfRepScript : MonoBehaviour {

    void Update() {
        int number;
        string keyPressed = Input.inputString;
        if (int.TryParse(keyPressed, out number)) {
            if (keyPressed.Length > 1) {
                keyPressed = keyPressed.Substring(0, 1);
            }
            if (keyPressed == "0") {
                return;
            }
            InputField iField = GetComponent<InputField>();
            iField.text = keyPressed;
        }
    }
}

