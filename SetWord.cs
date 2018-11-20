using UnityEngine;
using UnityEngine.UI;

public class SetWord : MonoBehaviour {

    public Transform parent;
    Text wordText;
    public string word;

    void Start() {
        wordText = parent.GetComponentInChildren<Text>();
        wordText.text = word;
    }
}
