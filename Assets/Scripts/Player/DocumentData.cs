using UnityEngine;

namespace StayHere.Items
{
    [CreateAssetMenu(fileName = "NewDocument", menuName = "Stay Here/Items/Document")]
    public class DocumentData : ScriptableObject
    {
        [Header("Document Info")]
        public string documentTitle;

        [Header("UI Visuals")]
        [Tooltip("A imagem do documento que aparecerá em um lado da tela")]
        public Sprite documentImage;

        [Header("Content")]
        [Tooltip("O texto puro e legível que aparecerá do outro lado da tela")]
        [TextArea(10, 20)] // Dá um espaço bom no Inspector para digitar textos longos
        public string documentContent;
    }
}