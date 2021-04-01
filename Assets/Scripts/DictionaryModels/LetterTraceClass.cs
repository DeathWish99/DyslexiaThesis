using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LetterTraceClass : MonoBehaviour
{
    [System.Serializable]
    public struct LetterTrace
    {
        public GameObject letterObj;
        public char letter;
    };
}
