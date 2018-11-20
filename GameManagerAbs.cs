using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerAbs : MonoBehaviour {


	public List<string> abstractedWords = new List<string>();
	List<string> allWords = new List<string>();

	string[] randomWords = {"Skæl", "Røg", "Kødæder", "Pigge"};
	string[] absWords = {"Hale", "Tænder", "2 Øjne", "2 Bagben", "2 Forben", "Kløer" };

	[Header("Changable values")]
	[Range(0,4)]
	[SerializeField]
	int extraRandomWords;
	[Range(0, 4)]
	public int wordsToGuess;
	[SerializeField]
	GameObject wordPrefab;
	[SerializeField]
	Transform startPanel;


	// Use this for initialization
	void Start () {
		Random.InitState(System.DateTime.Now.Millisecond);

		RandomizeArray(randomWords);
		RandomizeArray(absWords);

		for (int i = 0; i < extraRandomWords; i++) {
			allWords.Add(randomWords[i]);
		}

		for (int i = 0; i < wordsToGuess; i++) {
			abstractedWords.Add(absWords[i]);
			allWords.Add(absWords[i]);
		}

		for (int i = 0; i < allWords.Count; i++) {
			string temp = allWords[i];
			int randomIndex = Random.Range(i, allWords.Count);
			allWords[i] = allWords[randomIndex];
			allWords[randomIndex] = temp;
		}

		for (int i = 0; i < allWords.Count; i++) {
			GameObject tempWord = Instantiate(wordPrefab, startPanel);
			tempWord.GetComponent<DynamicMovementAbs>().word = allWords[i];
		}
	}


	void RandomizeArray(string[] arr) { //This is a poor way of shuffeling it seems. But no time for better implementation now.
		for (int i = arr.Length - 1; i > 0; i--) {
			int r = Random.Range(0, i);
			string tmp = arr[i];
			arr[i] = arr[r];
			arr[r] = tmp;
		}
	}
}
