using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class LockControl : MonoBehaviour
{
    public GameObject[] wheels;
    

    public GameObject openLock;
    public bool isOpened1, isOpened2;
    public GameObject wallet, walletOpen, secondLock;
    

    // Índice do objeto atualmente selecionado
    public int wheelsIndice = 0;

    public int[] result, correctCombination;

    private void Awake()
    {
        openLock.GetComponent<Animator>().enabled = false;
        secondLock.GetComponent<Animator>().enabled = false;

    }
    private void Start()
    {
        SelectWheel(wheelsIndice);
        LockRotation.Rotated += CheckResults;
    }

    void CheckResults(string wheelsName, int number)
    {
        switch(wheelsName)
        {
            case "ClockFace1":
                result[0] = number;
                break;
            case "ClockFace2":
                result[1] = number; 
                break;
            case "ClockFace3":
                result[2] = number;
                break;
            case "ClockFace4":
                result[3] = number; 
                break;
            case "ClockFace5":
                result[4] = number; 
                break;
            case "ClockFace6":
                result[5] = number;
                break;
            case "ClockFace7":
                result[6] = number;
                break;
            case "ClockFace8":
                result[7] = number;
                break;
            case "ClockFace9":
                result[8] = number;
                break;
            case "ClockFace10":
                result[9] = number;
                break;
        }

        
         if (result[0] == correctCombination[0] && result[1] == correctCombination[1] && result[2] == correctCombination[2] && result[3] == correctCombination[3] && result[4] == correctCombination[4])
         {
            Debug.Log("Opened1");
            openLock.GetComponent<Animator>().enabled = true;
            isOpened1 = true;   
         }
        
                   
         if (result[5] == correctCombination[5] && result[6] == correctCombination[6] && result[7] == correctCombination[7] && result[8] == correctCombination[8] && result[9] == correctCombination[9])
         {
            Debug.Log("Opened2");
            secondLock.GetComponent<Animator>().enabled = true;
            isOpened2 = true;   
         }

         if(isOpened1 == true && isOpened2 == true)
        {
            wallet.GetComponent<Animator>().SetTrigger("Open");
            walletOpen.GetComponent<Animator>().enabled = true;
            //walletOpen.GetComponent<BoxCollider>().enabled = false;
            //walletOpen.GetComponent<LeatherTrunk>().enabled = false;

        }
        

        

    }

    private void OnDestroy()
    {
        LockRotation.Rotated -= CheckResults;
    }

    void OnMoveRight(InputValue button)
    {
       if (!isActiveAndEnabled) return;          // <- bloqueia se estiver desativado
        if (!button.isPressed) return;            // <- evita disparo em release/cancel

        NextWheel();
    }

    void OnMoveLeft(InputValue button)
    {
        if (!isActiveAndEnabled) return;          // <- bloqueia se estiver desativado
        if (!button.isPressed) return;            // <- evita disparo em release/cancel

        BackWheel();
    }
    public void SelectWheel(int index)
    {
        // Desativa em todos
        for (int i = 0; i < wheels.Length; i++)
        {
            var lr = wheels[i].GetComponent<LockRotation>();
            if (lr != null) lr.enabled = false;
        }

        // Valida índice
        if (index < 0 || index >= wheels.Length) return;

        // Ativa só no selecionado
        var selected = wheels[index].GetComponent<LockRotation>();
        if (selected != null) selected.enabled = true;

        Debug.Log("Selecionado: " + wheels[index].name);
    }

    void NextWheel()
    {
        wheelsIndice = (wheelsIndice + 1) % wheels.Length;
        SelectWheel(wheelsIndice);
    }

    void BackWheel()
    {
        wheelsIndice = (wheelsIndice - 1 + wheels.Length) % wheels.Length;
        SelectWheel(wheelsIndice);
    }
}
