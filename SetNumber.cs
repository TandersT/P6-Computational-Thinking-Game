using UnityEngine;
using UnityEngine.UI;

public class SetNumber : MonoBehaviour {

    public Transform parent;
    Text numberText;
    public int number;

    void Start() {
        //number = Random.Range(0, 6);
        numberText = parent.GetComponentInChildren<Text>();
        //numberText.text = number.ToString();
    }
}
