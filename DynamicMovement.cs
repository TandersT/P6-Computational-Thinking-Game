using UnityEngine;
using UnityEngine.UI;

public class DynamicMovement : MonoBehaviour {
    [Header("Changable values")]
    [SerializeField]
    private string nameExtendToFind;
    [SerializeField]
    private float followSpeed;
    [SerializeField]
    private float rotationDegree;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private GameObject cardContainer;

    private Transform transformToFollow;
    private Transform currentParent;
    private Transform originalParent;

    public int number;

    Image image;
    [SerializeField]
    Sprite[] numbersSprite;

    void Start() {
        image = GetComponent<Image>();
        image.sprite = numbersSprite[number - 1];
        originalParent = transform.parent;
        currentParent = transform.parent;
        if (transformToFollow == null) {
            GameObject tempCardContainer = Instantiate(cardContainer, transform.position, transform.rotation);
            tempCardContainer.GetComponent<SetNumber>().parent = gameObject.transform;
            tempCardContainer.GetComponent<SetNumber>().number = number;
            tempCardContainer.transform.SetParent(gameObject.transform.parent.parent.Find(transform.parent.name + nameExtendToFind));
            tempCardContainer.name = gameObject.name + nameExtendToFind;
            transformToFollow = tempCardContainer.transform;
        }
    }

    void Update() {
        if (transformToFollow == null) {
            Destroy(gameObject);
            return;
        }

        if (transform.parent != currentParent) {
            Invoke("MovingToHand", 0);

            transformToFollow.SetParent(gameObject.transform.parent.parent.Find(transform.parent.name + nameExtendToFind));
            transformToFollow.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (originalParent == transform.parent) {
            //return;
        }

        if (transformToFollow.name == gameObject.name + nameExtendToFind) {
            transform.position = Vector3.Lerp(transform.position, transformToFollow.position, followSpeed);
            Vector3 rotationVector = transform.rotation.eulerAngles;
            rotationVector = transform.position - transformToFollow.position;
            rotationVector.y = Mathf.Clamp(rotationVector.y, -rotationDegree, rotationDegree);
            rotationVector.x = Mathf.Clamp(rotationVector.x, -rotationDegree, rotationDegree);
            float tempFloat = rotationVector.y;
            rotationVector.y = rotationVector.x;
            rotationVector.x = tempFloat;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotationVector), rotationSpeed);
        }
    }
}
