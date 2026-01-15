using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;



namespace StayHereCamera
{
    public class ControlCamera : MonoBehaviour
    {
        [Header("Time Transition")]
        public float duration = 1.0f;

        [Header("Vision")]
        public Camera camPlayer;
        [SerializeField] string screenLayer = "CamScreen";
        [SerializeField] GameObject camRecorder;

        // Ponto A
        static readonly Vector3 posA = new Vector3(0.291999996f, -0.46f, 0.273000002f);
        static readonly Quaternion rotA = new Quaternion(
            0.06711684f, 0.00471378f, -0.03138495f, 0.99724025f
        );

        // Ponto B
        static readonly Vector3 posB = new Vector3(0.283f, -0.115f, 0.221f);
        static readonly Quaternion rotB = new Quaternion(
            -0.71685761f, -0.02060881f, -0.01992277f, 0.69663012f
        );

       
        [Header("Initial State")]
        public bool startAtA = true;

        bool isAtA;
        Coroutine moveRoutine;

        void Awake()
        {
            isAtA = startAtA;

            // opcional: já “encaixa” no ponto inicial
            if (isAtA)
            {
                transform.localPosition = posA;
                transform.localRotation = rotA;
            }
            else
            {
                transform.localPosition = posB;
                transform.localRotation = rotB;
            }
        }

       
        public void ToggleCamera()
        {
            if (moveRoutine != null) return; // evita apertar durante a transição

            if (isAtA) StartMove(posB, rotB);
            else StartMove(posA, rotA);

            

            isAtA = !isAtA;
        }

        void StartMove(Vector3 targetPos, Quaternion targetRot)
        {
            moveRoutine = StartCoroutine(LerpTo(targetPos, targetRot));
        }

        IEnumerator LerpTo(Vector3 targetPos, Quaternion targetRot)
        {
            Vector3 startPos = transform.localPosition;
            Quaternion startRot = transform.localRotation;

            float t = 0f;
            float inv = 1f / Mathf.Max(0.0001f, duration);

            while (t < 1f)
            {
                t += Time.deltaTime * inv;

                float smooth = t * t * (3f - 2f * t); // SmoothStep
                transform.localPosition = Vector3.Lerp(startPos, targetPos, smooth);
                transform.localRotation = Quaternion.Slerp(startRot, targetRot, smooth);

                yield return null;
            }

            transform.localPosition = targetPos;
            transform.localRotation = targetRot;

            moveRoutine = null;
        }
    }

}


