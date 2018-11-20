using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculateOutput : MonoBehaviour {

    [SerializeField]
    GameObject number;
    [SerializeField]
    Transform numberParent;

    [SerializeField]
    GameObject[] inputPanels;
    List<SetNumber> numbersToCalculate = new List<SetNumber>();
    List<int> usedNumbers = new List<int>();
    [SerializeField]
    int goalValue;
    int totalValue;
    Text goalText;
    bool calculate;

    SetNumber[] inputChildren;

    [Header("Change equation")]
    [SerializeField]
    int maxAmountOfNumbers;
    [SerializeField]
    int minAmountOfNumbers;

    Image[] images;
    [SerializeField]
    Sprite[] numbersSprite;

    [SerializeField]
    AudioClip correct;
    [SerializeField]
    AudioClip wrong;

    public GameObject winScreen;
    public GameObject restartScreen;

    AudioSource aSource;
    void Start() {
        aSource = GetComponent<AudioSource>();
        int amountOfNumbers = Random.Range(minAmountOfNumbers, maxAmountOfNumbers + 1); //5
        int[,] numberMultipliers = new int[inputPanels.Length, amountOfNumbers]; //3,5
        for (int y = 0; y < numberMultipliers.GetLength(0); y++) { //0-2
            int amountOfNumbersToAdd = Random.Range(0, amountOfNumbers); //2, 2, 2
            if (y == numberMultipliers.GetLength(0) - 1 && amountOfNumbers > 0) {
                amountOfNumbersToAdd = amountOfNumbers;
            }
            amountOfNumbers -= amountOfNumbersToAdd;
            if (amountOfNumbers >= 0) {
                for (int x = 0; x < amountOfNumbersToAdd; x++) {
                    Debug.Log("I ran");
                    numberMultipliers[y, x] = Random.Range(1, 5);
                    GameObject tempNumber = Instantiate(number, numberParent);
                    tempNumber.GetComponent<DynamicMovement>().number = numberMultipliers[y, x];
                    numberMultipliers[y, x] *= (y + 1);
                    goalValue += numberMultipliers[y, x];
                }
            }
        }
        char[] numbersToIns = new char[goalValue.ToString().Length];
        for (int i = 0; i < goalValue.ToString().Length; i++) {
            numbersToIns[i] = goalValue.ToString()[i];
            Debug.Log(numbersToIns[i]);
            GameObject tempNumber = new GameObject(numbersToIns[i].ToString());
            tempNumber.transform.SetParent(gameObject.transform);
            tempNumber.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            tempNumber.AddComponent<Image>().sprite = numbersSprite[int.Parse(numbersToIns[i].ToString())];
            tempNumber.GetComponent<Image>().SetNativeSize();
        }
    }

    void Update() {
        if (calculate) {
            for (int i = 0; i < inputPanels.Length; i++) {
                if (inputPanels[i].transform.childCount != 0) {
                    inputChildren = inputPanels[i].GetComponentsInChildren<SetNumber>();
                    for (int j = 0; j < inputPanels[i].transform.childCount; j++) {
                        numbersToCalculate.Add(inputChildren[j].GetComponentInChildren<SetNumber>());
                        usedNumbers.Add(i + 1);
                    }
                }
            }
            for (int i = 0; i < numbersToCalculate.Count; i++) {
                totalValue += numbersToCalculate[i].number * usedNumbers[i];
            }
            if (totalValue == goalValue) {
                Debug.Log("Correct!: " + totalValue);
                aSource.clip = correct;
                winScreen.SetActive(true);
            } else {
                Debug.Log("Wrong! " + totalValue);
                aSource.clip = wrong;
            }
            aSource.Play();
            totalValue = 0;
            usedNumbers.Clear();
            numbersToCalculate.Clear();
            calculate = false;
        }

    }

    public void CalculateButton() {
        calculate = true;
    }

}
