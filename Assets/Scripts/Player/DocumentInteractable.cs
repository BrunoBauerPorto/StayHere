using UnityEngine;
using StayHere.Items;

public class DocumentInteractable : Interactable
{
    [Header("Data")]
    public DocumentData documentData;

    protected override void interact()
    {
        UIManager.Instance.CollectAndReadDocument(documentData);
        Destroy(this);
    }
}