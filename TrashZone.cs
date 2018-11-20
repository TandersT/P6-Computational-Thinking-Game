using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TrashZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

    Image _image;
    [SerializeField]
    Sprite[] trashcans = new Sprite[2];

    void Start() {
        _image = GetComponent<Image>();
    }

    public void OnDrop(PointerEventData eventData) {
        Destroy(eventData.pointerDrag);
        Destroy(eventData.pointerDrag.GetComponent<DraggableBear>().placeholder);
        _image.sprite = trashcans[0];
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (eventData.pointerDrag == null) {
            return;
        }
        _image.sprite = trashcans[1];
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (eventData.pointerDrag == null) {
            return;
        }
        _image.sprite = trashcans[0];
    }
}
