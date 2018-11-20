using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField]
    GameObject water;
    [SerializeField]
    GameObject blockage;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject end;
    [SerializeField]
    Transform _canvas;

    public GameObject winScreen;
    public GameObject restartScreen;

    public Vector2 startPos;

    float indentFromCornerX = 1.4f;
    float indentFromCornerY = 0.3f;

    float xScalar = 1.8f;
    float yScalar = 1f;

    int[,] map = {
        {1,1,1,1,1,1,1,1 },
        {1,2,0,1,1,0,1,1 },
        {1,0,1,0,0,0,0,1 },
        {1,0,0,1,1,0,0,1 },
        {1,1,0,0,1,1,0,1 },
        {1,0,0,0,1,0,0,1 },
        {1,1,1,0,0,0,3,1 },
        {1,1,1,1,1,1,1,1 }
    };

    void Awake() {
        Vector3 topLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, Camera.main.nearClipPlane));
        for (int x = 0; x < map.GetLength(0); x++) {
            for (int y = 0; y < map.GetLength(1); y++) {
                float posX = xScalar * x + topLeft.x + indentFromCornerX;
                float posY = topLeft.y - yScalar * y - indentFromCornerY;
                if (map[x, y] == 0) {
                    GameObject tempWater = Instantiate(water, new Vector2(posX, posY), Quaternion.identity, _canvas);
                } else if (map[x, y] == 1) {
                    GameObject tempBlockage = Instantiate(blockage, new Vector2(posX, posY), Quaternion.identity, _canvas);
                } else if (map[x, y] == 2) {
                    Debug.Log("Start Pos");
                    startPos = new Vector2(posX, posY);
                    GameObject tempPlayer = Instantiate(player, startPos, Quaternion.identity, _canvas);
                    tempPlayer.GetComponent<PlayerController>().xScalar = xScalar;
                    tempPlayer.GetComponent<PlayerController>().yScalar = yScalar;
                    tempPlayer.GetComponent<PlayerController>().winScreen = winScreen;
                    tempPlayer.GetComponent<PlayerController>().restartScreen = restartScreen;
                } else if (map[x, y] == 3) {
                    Debug.Log("End Pos");
                    GameObject tempEnd = Instantiate(end, new Vector2(posX, posY), Quaternion.identity, _canvas);
                }
            }
        }
    }

    void Update() {

    }
}
