using System;
using System.Collections;
using System.Collections.Generic;
using Game.GameStateMachine;
using UnityEngine;

public class CoroutineStarter : MonoBehaviour
{
   public static  CoroutineStarter Instance;

   private void Awake()
   {
      Instance = this;
   }

   public void StartCoroutine2(BootstrapGameState coroutine)
   {
      StartCoroutine(coroutine.GetData());
   }
}
