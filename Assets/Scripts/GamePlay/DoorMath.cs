using UnityEngine;
using System.Collections; 
public class DoorMath : MonoBehaviour
{
    [Header("Configurações da Porta")]
    public float openAngle = 90f; 
    public float openSpeed = 5f;  

    private bool isOpen = false;
    private Quaternion closedRotation;
    private Coroutine animationCoroutine;
    public AudioSource openDoor, closedDoor;

    void Start()
    {
        closedRotation = transform.rotation;
    }

    public void InteractWithDoor(Transform playerTransform)
    {
        
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }

        if (!isOpen)
        {
            
           
            Vector3 directionToPlayer = playerTransform.position - transform.position;
            float dotProduct = Vector3.Dot(transform.forward, directionToPlayer.normalized);

            
            float targetAngle = dotProduct > 0 ? -openAngle : openAngle;

          
            Quaternion targetRotation = closedRotation * Quaternion.Euler(0, targetAngle, 0);

           
            animationCoroutine = StartCoroutine(MoveDoor(targetRotation));
            if (openDoor != null) openDoor.Play();
            isOpen = true;
        }
        else
        {
            
            animationCoroutine = StartCoroutine(MoveDoor(closedRotation));
            
            if(closedDoor != null) closedDoor.Play();

            isOpen = false;
        }
    }

   
    private IEnumerator MoveDoor(Quaternion target)
    {
       
        while (Quaternion.Angle(transform.rotation, target) > 0.01f)
        {
           
            transform.rotation = Quaternion.Lerp(transform.rotation, target, Time.deltaTime * openSpeed);

            
            yield return null;
        }

       
        transform.rotation = target;
    }
}