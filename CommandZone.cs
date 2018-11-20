using UnityEngine;
using UnityEngine.EventSystems;

public class CommandZone : MonoBehaviour, IDropHandler {

    public void OnDrop(PointerEventData eventData) {
        DraggableBear draggable = eventData.pointerDrag.GetComponent<DraggableBear>();
        if (draggable != null) {
            draggable.parentToReturnTo = transform;
        }
    }
}
