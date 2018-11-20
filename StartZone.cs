using UnityEngine;
using UnityEngine.EventSystems;

public class StartZone : MonoBehaviour, IDropHandler {

    public void OnDrop(PointerEventData eventData) {

        DraggableBeetle draggable = eventData.pointerDrag.GetComponent<DraggableBeetle>();
        if (draggable != null) {
            draggable.parentToReturnTo = transform;
        }
    }
}
