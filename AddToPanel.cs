using UnityEngine;
using UnityEngine.UI;

public class AddToPanel : MonoBehaviour {
    [HideInInspector]
    public string dir;
    [HideInInspector]
    public int rep;

    [SerializeField]
    Text command;
    [SerializeField]
    Transform commandList;

    PlayerController pCon;

    int amountAdded;

    Button btn;

    void Start() {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        pCon = (PlayerController)player.GetComponent(typeof(PlayerController));
    }

    void TaskOnClick() {
        Text tempText = Instantiate(command, commandList);
        tempText.text = "Go " + dir + ", " + rep.ToString() + " times.";
        pCon.dir[amountAdded] = dir;
        pCon.rep[amountAdded] = rep;
        amountAdded++;
    }
}
