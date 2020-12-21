using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PointerEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
     
     public AudioSource audio;
     Transform transform;
     Vector3 initialscale;
     public Vector3 scaletobe;
     
    [SerializeField] private UnityEvent OnClick = new UnityEvent();

    
    private Color color;

    private void Awake()
    {
        transform = GetComponent<Transform>();
        initialscale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = scaletobe;
        audio.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = initialscale;
      
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick.Invoke();
        print("Click");
    }
}
