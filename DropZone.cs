using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler {

    public void OnDrop(PointerEventData eventData) {

        DraggableBeetle draggable = eventData.pointerDrag.GetComponent<DraggableBeetle>();
        if (draggable != null && gameObject.transform.childCount < 3) {
            draggable.parentToReturnTo = transform;
        }
    }
}
