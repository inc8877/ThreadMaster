using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ThreadMaster
{
    public static partial class Multithreading
    {
        public static async UniTask WaitForCondition(Action init, Action exit, Func<bool> condition, int pollingDelay)
        {
            init.Invoke();
            while (!condition.Invoke())
            {
                await UniTask.Delay(pollingDelay);
                await UniTask.Yield();
            }
            exit.Invoke();
        }
    }
}
