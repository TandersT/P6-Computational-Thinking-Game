using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableAbs : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    [HideInInspector]
    public Transform placeholderParent = null; //Used in startZone. Maybe change to Getter/setter
    [HideInInspector]
    public Transform parentToReturnTo = null; //Used in startZone. Maybe change to Getter/setter

    public GameObject placeholder = null;
    private Transform originalParent = null;
    private StartZone startZone;

    public GameObject graphics;

    Transform startZoneTrans;

    void Start() {
        startZoneTrans = GameObject.FindGameObjectWithTag("StartZone").transform;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        CardBeginDrag();
    }

    public void OnDrag(PointerEventData eventData) {
        CardOnDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData) {
        CardEndDrag();
    }


    void CardBeginDrag() {
        placeholder = new GameObject();
        placeholder.name = "placeholder";
        placeholder.transform.SetParent(startZoneTrans);
        placeholderParent = startZoneTrans;
        LayoutElement le = placeholder.AddComponent<LayoutElement>();
        le.preferredHeight = GetComponent<LayoutElement>().preferredHeight;
        le.preferredWidth = GetComponent<LayoutElement>().preferredWidth;
        le.flexibleHeight = 0;
        le.flexibleWidth = 0;

        placeholder.transform.SetSiblingIndex(transform.GetSiblingIndex());

        parentToReturnTo = transform.parent;

        transform.SetParent(transform.parent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }


    void CardOnDrag(PointerEventData _eventData) {
        transform.position = _eventData.position;

        int newSiblingIndex = placeholderParent.childCount;

        for (int i = 0; i < placeholderParent.childCount; i++) {
            if (transform.position.x < placeholderParent.GetChild(i).position.x) {
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