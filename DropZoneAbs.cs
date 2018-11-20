using UnityEngine;
using UnityEngine.EventSystems;

public class DropZoneAbs : MonoBehaviour, IDropHandler {

    public void OnDrop(PointerEventData eventData) {

        DraggableAbs draggable = eventData.pointerDrag.GetComponent<DraggableAbs>();
        if (draggable != null) {
            draggable.parentToReturnTo = transform;
        }
    }
}
