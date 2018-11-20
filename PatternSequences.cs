using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class PatternSequences : MonoBehaviour {
    public GameObject continueScreen;
    int randomIndex;


    [SerializeField]
    public PatternHolder[] allPatterns = new PatternHolder[0];
    PatternController.Directions[] correctPattern;
    bool[] setWrongArray;

    

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {

    }

    public PatternController.Directions[] GetRandomPattern() {
        randomIndex = Random.Range(0, allPatterns.Length);
        PatternController.Directions[] patternToReturn = allPatterns[randomIndex].array.Clone() as PatternController.Directions[];
        correctPattern = allPatterns[randomIndex].array;
        print("Executing pattern number: " + randomIndex);

        List<PatternHolder> tempPatterns = allPatterns.ToList();

        for (int i = 0; i < patternToReturn.Length; i++)
        {
            if (allPatterns[randomIndex].errors[i])
                patternToReturn[i] = PatternController.Directions.ERROR;
        }

        tempPatterns.RemoveAt(randomIndex);
        allPatterns = tempPatterns.ToArray();
        
                 
        print("There are " + allPatterns.Length + " patterns left.");
        if (allPatterns.Length == -1) { //This is supposed to run when the game is over.
            continueScreen.SetActive(true);
        }

        return patternToReturn;
    }

    public PatternController.Directions[] GetCorrectPattern() {
        return correctPattern;
    }

    public void AddPattern ()
    {
        PatternHolder newPatternHolder = new PatternHolder();
        PatternController.Directions[] newPattern = new PatternController.Directions[8];
        List<PatternHolder> tempPatterns = allPatterns.ToList();
        newPatternHolder.array = newPattern;
        newPatternHolder.errors = new bool[newPattern.Length];
        tempPatterns.Add(newPatternHolder);        
        allPatterns = tempPatterns.ToArray();             
    }

    void OnValidate()
    {
        if (allPatterns.Length > 0 && allPatterns[randomIndex].errors.All(x => x))
            allPatterns[randomIndex].errors = new bool[allPatterns[randomIndex].array.Length];
    }
}

[CustomEditor(typeof(PatternSequences))]
public class AddButtonEditor: Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        PatternSequences ps = (PatternSequences)target;
        if(GUILayout.Button("Add Pattern"))
        {
            ps.AddPattern();
        }
    }
}

[System.Serializable]
public class PatternHolder
{
    public PatternController.Directions[] array;
    public bool[] errors;
}

