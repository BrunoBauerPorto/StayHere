using StayHere.Move;
using StayHere.Player;
using UnityEngine;

public class LeatherTrunk : Interactable
{
    public GameObject walletUi;
    public Animator walletUiAnim;
    public float rotationSpeed = 6.0f;
    public GameObject player;

    bool isInteract = false;
    public float t;
    protected override void interact()
    {
        if(isInteract == false)
        {

            walletUi.SetActive(true);
            walletUi.GetComponent<Animator>().enabled = true;
            player.GetComponent<MovePlayer>().enabled = false;
            isInteract = true;
            

        }

        else if (isInteract == true)
        {
            walletUiAnim.SetTrigger("Out");
            walletUi.SetActive(false);
            player.GetComponent<MovePlayer>().enabled = true;
            isInteract = false;

        }

    }
}
