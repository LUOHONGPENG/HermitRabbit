using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CommonHoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isEnabled = true;
    public bool isHavor = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHavor = true;
    }

    
    public void OnPointerExit(PointerEventData eventData)
    {
        isHavor = false;
    }

    public void ResetOne()
    {

    }
}