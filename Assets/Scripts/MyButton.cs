using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class MyButton : MonoBehaviour

{
    public UnityEvent signalOnClick = new UnityEvent();


    public void _onClick()
    {
        this.signalOnClick.Invoke();
    }
}
