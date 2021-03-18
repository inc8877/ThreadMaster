/*Copyright 2021 Volodymyr Bozhko
 
Contact: inc8877@gmail.com

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.*/

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
        /// Invokes 'condition' while it returns false and performs the 'react' action as soon as 'condition' returns true.
        /// <remarks>If you work with a backend, then use <see cref="WaitForCondition(Action, Func{bool}, int)"/>
        /// overload with a polling delay, so as not to load the backend with requests </remarks>
        /// </summary>
        /// <param name="react">Invokes as soon as the 'condition' returns true</param>
        /// <param name="condition">The condition under which the 'exit' action would perform</param>
        /// <returns></returns>
        public static async UniTask WaitForCondition(Action react, Func<bool> condition)
        {
            while (!condition.Invoke()) await UniTask.Yield();
            react.Invoke();
        }
        
        /// <summary>
        /// Invokes 'condition' with a polling delay and performs the 'react' action as soon as 'condition' returns true
        /// </summary>
        /// <param name="react">Invokes as soon as the 'condition' returns true</param>
        /// <param name="condition">The condition under which the 'exit' action would perform</param>
        /// <param name="pollingDelay">The time between 'condition' invocations. Specified in milliseconds</param>
        /// <returns></returns>
        public static async UniTask WaitForCondition(Action react, Func<bool> condition, int pollingDelay)
        {
            while (!condition.Invoke()) await UniTask.Delay(pollingDelay);
            react.Invoke();
        }
        
        /// <summary>
        /// Perform the '<code>init</code>' action immediately after invocation.
        /// Invokes 'condition' while it returns false and performs the 'exit' action as soon as 'condition' returns true.
        /// <remarks>If you work with a backend, then use <see cref="WaitForCondition(Action, Action, Func{bool}, int)"/>
        /// overload with a polling delay, so as not to load the backend with requests </remarks>
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
            while (!condition.Invoke()) await UniTask.Delay(pollingDelay);
            exit.Invoke();
        }
    }
}
