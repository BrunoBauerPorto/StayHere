using System;
using UnityEngine;
using System.Collections;

namespace StayHereCamera
{
    public class ControlCamera : MonoBehaviour
    {
        public float duration = 1.0f;

        public bool startAtA = true;
        public static bool isAtA { get; private set; }

        public static event Action<bool> OnActiveCameraChanged;

        static readonly Vector3 posA = new Vector3(0.291999996f, -0.46f, 0.273000002f);
        static readonly Quaternion rotA = new Quaternion(0.06711684f, 0.00471378f, -0.03138495f, 0.99724025f);

        static readonly Vector3 posB = new Vector3(0.283f, -0.115f, 0.221f);
        static readonly Quaternion rotB = new Quaternion(-0.71685761f, -0.02060881f, -0.01992277f, 0.69663012f);

        Coroutine moveRoutine;

        void Awake()
        {
            isAtA = startAtA;

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

            // notifica o estado inicial
            OnActiveCameraChanged?.Invoke(isAtA);
        }

        public void ToggleCamera()
        {
            if (moveRoutine != null) return;

            if (isAtA) moveRoutine = StartCoroutine(LerpTo(posB, rotB, willBeAtA: false));
            else moveRoutine = StartCoroutine(LerpTo(posA, rotA, willBeAtA: true));
        }

        IEnumerator LerpTo(Vector3 targetPos, Quaternion targetRot, bool willBeAtA)
        {
            Vector3 startPos = transform.localPosition;
            Quaternion startRot = transform.localRotation;

            float t = 0f;
            float inv = 1f / Mathf.Max(0.0001f, duration);

            while (t < 1f)
            {
                t += Time.deltaTime * inv;
                float smooth = t * t * (3f - 2f * t);
                transform.localPosition = Vector3.Lerp(startPos, targetPos, smooth);
                transform.localRotation = Quaternion.Slerp(startRot, targetRot, smooth);
                yield return null;
            }

            transform.localPosition = targetPos;
            transform.localRotation = targetRot;

            // confirma a troca de câmera
            isAtA = willBeAtA;
            OnActiveCameraChanged?.Invoke(isAtA);

            moveRoutine = null;
        }
    }
}
