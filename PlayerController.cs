using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public bool _GameRunning = false;

    public List<string> dir = new List<string>();
    public List<int> rep = new List<int>();

    public bool start;
    bool startMove;

    Vector2 currentPos;
    Vector2 destPos;

    float startTime;
    float endTime;

    public float xScalar;
    public float yScalar;

    float movementTimeScalar = 2f;

    Animator anim;

    int walkHash = Animator.StringToHash("Walk");
    int jumpHash = Animator.StringToHash("Jump");

    public GameObject winScreen;
    public GameObject restartScreen;

    bool endReached;
    void Start() {
        _GameRunning = false;
        anim = GetComponentInChildren<Animator>();
    }

    void Update() {
        if (_GameRunning) {
            if (start) {
                StartCoroutine(playerMove());
                start = false;
            }
            if (startMove) {

                float timeSinceStarted = Time.time - startTime;
                float percentageComplete = timeSinceStarted / endTime;
                transform.position = Vector3.Lerp(currentPos, destPos, percentageComplete);
                if (percentageComplete >= 1.0f) {
                    if (endReached) {
                        endReached = false;
                        HaltGame();
                        restartScreen.SetActive(true);
                    }
                    startMove = false;
                    Debug.Log("Reached");
                    anim.SetTrigger("Idle");
                } else {
                    anim.SetTrigger("Walk");
                }
            } else {
                anim.SetTrigger("Idle");
            }
        }
    }
    IEnumerator playerMove() {
        for (int i = 0; i < dir.Count; i++) {
            if (i == dir.Count - 1) {
                endReached = true;
            }
            float[] dirXInt = new float[dir.Count];
            float[] dirYInt = new float[dir.Count];
            float movementTime = rep[i] * movementTimeScalar;
            if (dir[i] == "up") {
                dirYInt[i] = 1 * yScalar;
            } else if (dir[i] == "right") {
                dirXInt[i] = 1 * xScalar;
            } else if (dir[i] == "down") {
                dirYInt[i] = -1 * yScalar;
            } else if (dir[i] == "left") {
                dirXInt[i] = -1 * xScalar;
            }
            currentPos = transform.position;

            destPos = currentPos + new Vector2(dirXInt[i] * rep[i], dirYInt[i] * rep[i]);
            print(destPos);
            startTime = Time.time;
            endTime = movementTime;
            startMove = true;
            Debug.Log("Ran once");
            yield return new WaitForSeconds(movementTime);
        }
        yield break;
    }
    void OnTriggerEnter2D(Collider2D _col) {
        if (_col.transform.CompareTag("End")) {
            Debug.Log("End Reached");
            HaltGame();
            winScreen.SetActive(true);
        }
        if (_col.transform.CompareTag("Blockage")) {
            Debug.Log("You lose!");
            HaltGame();
            restartScreen.SetActive(true);
        }
    }

    void HaltGame() {
        dir.Clear();
        rep.Clear();
        StopCoroutine(playerMove());
        startMove = false;
        _GameRunning = false;
    }
}

