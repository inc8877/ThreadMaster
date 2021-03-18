using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ThreadMaster
{
    public static partial class Multithreading
    {
        /// <summary>
        /// Perform the '<code>init</code>' action immediately after invocation.
        /// Invokes 'condition' while it returns false and performs the 'exit' action as soon as 'condition' returns true.
        /// <remarks>Be careful! This method polls the 'condition' func as often as it can,
        /// it can be bad for the network if you are working with a server.
        /// If traffic is important to you, use <see cref="WaitForCondition(Action, Action, Func{bool}, int)"/>
        /// overload with a polling delay</remarks>
        /// </summary>
        /// <param name="init"></param>
        /// <param name="exit"></param>
        /// <param name="condition">The condition under which the 'exit' action would perform</param>
        /// <returns></returns>
        public static async UniTask WaitForCondition(Action init, Action exit, Func<bool> condition)
        {
            init.Invoke();
            while (!condition.Invoke()) await UniTask.Yield();
            exit.Invoke();
        }

        /// <summary>
        /// Perform the 'init' action immediately after invocation.
        /// Invokes 'condition' with a polling delay and performs the 'exit' action as soon as 'condition' returns true
        /// </summary>
        /// <param name="init"></param>
        /// <param name="exit"></param>
        /// <param name="condition">The condition under which the 'exit' action would perform</param>
        /// <param name="pollingDelay">The time between 'condition' invocations. Specified in milliseconds</param>
        /// <returns></returns>
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
