using UnityEngine;

public class AlienCController : MonoBehaviour {
    [SerializeField]
    GameObject startPanel;
    public void StartPanel() {
        startPanel.SetActive(false);
    }
    public void WinPanel() {
        RandomSceneChooser.SceneChooser();
    }
}
