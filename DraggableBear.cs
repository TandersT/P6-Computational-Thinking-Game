using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableBear : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    public string dir;
    public int rep;

    [HideInInspector]
    public Transform placeholderParent = null; //Used in commandZone. Maybe change to Getter/setter
    [HideInInspector]
    public Transform parentToReturnTo = null; //Used in commandZone. Maybe change to Getter/setter

    public bool hoveringCard;

    public GameObject placeholder = null;
    private Transform originalParent = null;
    private CommandZone commandZone;

    void Start() {
    }

    public void OnBeginDrag(PointerEventData eventData) {
        CardBeginDrag();
    }

    public void OnDrag(PointerEventData eventData) {
        CardOnDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData) {
        commandZone = parentToReturnTo.GetComponent<CommandZone>();
        CardEndDrag();
    }


    void CardBeginDrag() {
        placeholder = new GameObject();
        placeholder.transform.SetParent(transform.parent);
        LayoutElement le = placeholder.AddComponent<LayoutElement>();
        le.preferredHeight = GetComponent<LayoutElement>().preferredHeight;
        le.preferredWidth = GetComponent<LayoutElement>().preferredWidth;
        le.flexibleHeight = 0;
        le.flexibleWidth = 0;

        placeholder.transform.SetSiblingIndex(transform.GetSiblingIndex());

        parentToReturnTo = transform.parent;
        placeholderParent = parentToReturnTo;

        transform.SetParent(transform.parent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }


    void CardOnDrag(PointerEventData _eventData) {
        transform.position = _eventData.position;

        if (placeholderParent.transform.parent != placeholderParent) {
            placeholder.transform.SetParent(placeholderParent);

        }

        int newSiblingIndex = placeholderParent.childCount;

        for (int i = 0; i < placeholderParent.childCount; i++) {
            if (transform.position.y > placeholderParent.GetChild(i).position.y) {
                newSiblingIndex = i;
                if (placeholder.transform.GetSiblingIndex() < newSiblingIndex) {
                    newSiblingIndex--;
                }
                break;
            }
        }

        placeholder.transform.SetSiblingIndex(newSiblingIndex);

    }

    void CardEndDrag() {
        transform.SetParent(parentToReturnTo);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
        Destroy(placeholder);
    }
}

