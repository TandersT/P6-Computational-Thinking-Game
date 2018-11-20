using UnityEngine;
using UnityEngine.UI;

public class CommandController : MonoBehaviour {

    [HideInInspector]
    public string dir;
    [HideInInspector]
    public int rep;

    [SerializeField]
    GameObject command;
    [SerializeField]
    Transform commandList;

    PlayerController pCon;

    Button btn;

    [SerializeField]
    Sprite[] arrows = new Sprite[4];
    Sprite currentArrow;
    [SerializeField]
    Sprite[] numbers = new Sprite[10];
    Sprite currentNumber;

    GameManager gM;
    GameObject player;

    [SerializeField]
    GameObject startPanel;

    bool h_isAxisInUse;
    bool v_isAxisInUse;

    void Start() {
        gM = GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        pCon = (PlayerController)player.GetComponent(typeof(PlayerController));
    }

    void Update() {
        if (!pCon._GameRunning) {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Return)) {
            startButton();
        }

        float hAxis = Input.GetAxisRaw("Horizontal");
        if (hAxis != 0) {
            if (!h_isAxisInUse) {
                if (hAxis == 1) {
                    dir = "right";
                } else if (hAxis == -1) {
                    dir = "left";
                }
                OnArrowPress();
                h_isAxisInUse = true;
            }
        }
        if (hAxis == 0) {
            h_isAxisInUse = false;
        }
        float vAxis = Input.GetAxisRaw("Vertical");
        if (vAxis != 0) {
            if (!v_isAxisInUse) {
                if (vAxis == 1) {
                    dir = "up";
                } else if (vAxis == -1) {
                    dir = "down";
                }
                OnArrowPress();
                v_isAxisInUse = true;
            }
        }
        if (vAxis == 0) {
            v_isAxisInUse = false;
        }

    }

    public void repButton(string _rep) {
        if (!pCon._GameRunning) {
            return;
        }
        rep = int.Parse(_rep);

    }

    public void StartPanel() {
        startPanel.SetActive(false);
        pCon._GameRunning = true;
    }

    public void startButton() {
        pCon.start = true;
        foreach (DraggableBear _child in commandList.GetComponentsInChildren<DraggableBear>()) {
            pCon.rep.Add(_child.rep);
            pCon.dir.Add(_child.dir);
        }
    }

    void OnArrowPress() {
        if (rep == 0) {
            return;
        }
        GameObject tempCommand = Instantiate(command, commandList);
        if (dir == "up") {
            currentArrow = arrows[0];
        } else if (dir == "right") {
            currentArrow = arrows[1];
        } else if (dir == "down") {
            currentArrow = arrows[2];
        } else if (dir == "left") {
            currentArrow = arrows[3];
        }

        tempCommand.GetComponentsInChildren<Image>()[1].sprite = currentArrow;
        tempCommand.GetComponentsInChildren<Image>()[3].sprite = numbers[rep];

        tempCommand.GetComponent<DraggableBear>().dir = dir;
        tempCommand.GetComponent<DraggableBear>().rep = rep;
    }

    public void RestartButton() {
        pCon._GameRunning = true;
        player.transform.position = gM.startPos;
        gM.restartScreen.SetActive(false);
    }

    public void ContinueButton() {
        RandomSceneChooser.SceneChooser();
    }

}
