using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    
    public void Deselect()
    {
        GetComponent<Button>().OnPointerUp(new PointerEventData(EventSystem.current));
    }

    public void Select()
    {
        GetComponent<Button>().OnPointerDown(new PointerEventData(EventSystem.current));
    }
}
