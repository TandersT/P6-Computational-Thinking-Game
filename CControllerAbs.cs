using UnityEngine;
using UnityEngine.UI;

public class CControllerAbs : MonoBehaviour {

    GameManagerAbs gM;
    [SerializeField]
    Transform matchZone;
    [SerializeField]
    GameObject startScreen;
    [SerializeField]
    GameObject winScreen;

    void Start() {
        gM = GetComponent<GameManagerAbs>();
    }

    public void StartButton() {
        startScreen.SetActive(false);
    }

    public void ContinueButton() {
        RandomSceneChooser.SceneChooser();
    }

    public void CheckOptions() {
        int correctNr = 0;
        SetWord[] wordsToCheck = matchZone.GetComponentsInChildren<SetWord>();
        Debug.Log(wordsToCheck.Length);
        for (int i = 0; i < wordsToCheck.Length; i++) {
            Debug.Log(wordsToCheck[i].word);
            if (gM.abstractedWords.Contains(wordsToCheck[i].word)) {
                wordsToCheck[i].GetComponentInParent<DraggableAbs>().graphics.GetComponent<Image>().color = Color.green;
                correctNr++;
                if (correctNr == gM.wordsToGuess) {
                    winScreen.SetActive(true);
                }
            } else {
                correctNr--;
                wordsToCheck[i].GetComponentInParent<DraggableAbs>().graphics.GetComponent<Image>().color = Color.red;
            }

        }
    }
}
