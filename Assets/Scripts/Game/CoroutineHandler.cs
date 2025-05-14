using System.Collections;
using UnityEngine;

namespace Game
{
    public class CoroutineHandler : MonoBehaviour
    {
        public Coroutine StartNewCoroutine(IEnumerator coroutine)
        {
            return StartCoroutine(coroutine);
        }
    }
}
