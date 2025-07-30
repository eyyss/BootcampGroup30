using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonBase : MonoBehaviour, IPointerEnterHandler
{
    private Button button;
    public AudioData hoverAudio, clickAudio;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate
        {
            clickAudio.Play2D(this);
        });
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverAudio.Play2D(this);
    }
}
