using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine.InputSystem;

public class LockRotation : MonoBehaviour
{
    public static event Action<string, int> Rotated = delegate { };

    private bool coroutineAllowed;
    private int numberShow;

    private void Start()
    {
        coroutineAllowed = true;
        numberShow = 0;
    }

    void OnMoveUp(InputValue button)
    {
        if (!isActiveAndEnabled) return;          // <- bloqueia se estiver desativado
        if (!button.isPressed) return;            // <- evita disparo em release/cancel
        if (coroutineAllowed)
            StartCoroutine(RotateWheelUp());
    }

    void OnMoveDown(InputValue button)
    {
        if (!isActiveAndEnabled) return;          // <- bloqueia se estiver desativado
        if (!button.isPressed) return;            // <- evita disparo em release/cancel
        if (coroutineAllowed)
            StartCoroutine(RotateWheelDown());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        coroutineAllowed = true;
    }

    IEnumerator RotateWheelUp()
    {
        coroutineAllowed = false;
        for (int i = 0; i <= 11; i++)
        {
            transform.Rotate(0f, 0f, 3f);
            yield return new WaitForSeconds(0.01f);
        }

        coroutineAllowed= true;
        numberShow++;

        if(numberShow > 9)
        {
            numberShow = 0;
        }

        Rotated(name, numberShow);
    }

    IEnumerator RotateWheelDown()
    {
        coroutineAllowed = false;
        for (int i = 0; i <= 11; i++)
        {
            transform.Rotate(0f, 0f, -3f);
            yield return new WaitForSeconds(0.01f);
        }

        coroutineAllowed = true;
        numberShow--;

        if (numberShow < 0)
        {
            numberShow = 9;
        }

        Rotated(name, numberShow);
    }
}
