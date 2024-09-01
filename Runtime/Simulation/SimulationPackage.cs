using System;
using System.Collections;
using System.Collections.Generic;

#if DOTWEEN
using DG.Tweening;
#endif

using UnityEngine;

namespace Shun_Utilities
{
    public class SimulationPackage
    {
        public float Priority { get; set; }
        public readonly List<SimulationExecution> ExecuteEvents = new();
        public SimulationCallback OnCompleteCallback;
        public SimulationCallback OnStartCallback;

        public SimulationPackage(float priority = 0)
        {
            Priority = priority;
        }
        
        public void SetPriority(float priority)
        {
            Priority = priority;
        }
        
        public void MergeWith(SimulationPackage other)
        {
            // Merge ExecuteEvents
            this.ExecuteEvents.AddRange(other.ExecuteEvents);

            // Merge OnCompleteCallback
            if (this.OnCompleteCallback == null)
            {
                this.OnCompleteCallback = other.OnCompleteCallback;
            }
            else if (other.OnCompleteCallback != null)
            {
                var originalCallback = this.OnCompleteCallback;
                this.OnCompleteCallback = new SimulationCallback();
                this.OnCompleteCallback.Callbacks += originalCallback.Callbacks;
                this.OnCompleteCallback.Callbacks += other.OnCompleteCallback.Callbacks;
            }

            // Merge OnStartCallback
            if (this.OnStartCallback == null)
            {
                this.OnStartCallback = other.OnStartCallback;
            }
            else if (other.OnStartCallback != null)
            {
                var originalCallback = this.OnStartCallback;
                this.OnStartCallback = new SimulationCallback();
                this.OnStartCallback.Callbacks += originalCallback.Callbacks;
                this.OnStartCallback.Callbacks += other.OnStartCallback.Callbacks;
            }

            // Merge Priority
            this.Priority = Math.Max(this.Priority, other.Priority);
        }

        public void AddToPackage(float waitTime, bool isParallelWithPrevious = false)
        {
            IEnumerator CoroutineWrapper()
            {
                yield return new WaitForSeconds(waitTime);
            }
            
            ExecuteEvents.Add(new SimulationExecution(CoroutineWrapper, isParallelWithPrevious));
        }
        
        public void AddToPackage(Action action, bool isParallelWithPrevious = false)
        {
            IEnumerator CoroutineWrapper()
            {
                action();
                yield return null; // Yielding once to make it a valid coroutine
            }
            
            ExecuteEvents.Add(new SimulationExecution(CoroutineWrapper, isParallelWithPrevious));
        }

        public void AddToPackage(Func<IEnumerator> coroutine, bool isParallelWithPrevious = false)
        {
            ExecuteEvents.Add(new SimulationExecution(coroutine, isParallelWithPrevious));
        }
        
        public void AddToPackage(IEnumerator coroutine, bool isParallelWithPrevious = false)
        {
            IEnumerator CoroutineWrapper() => coroutine;
            ExecuteEvents.Add(new SimulationExecution(CoroutineWrapper, isParallelWithPrevious));
        }
        
        public void AddCompleteCallback(SimulationCallback simulationCallback)
        {
            OnCompleteCallback = simulationCallback;
        }
        
        public void AddStartCallback(SimulationCallback simulationCallback)
        {
            OnStartCallback = simulationCallback;
        }
        
        public void RemoveCompleteCallback()
        {
            OnCompleteCallback = null;
        }
        
        public void RemoveStartCallback()
        {
            OnStartCallback = null;
        }
        
        public void InvokeCompleteCallback()
        {
            OnCompleteCallback?.Callbacks.Invoke(this);
        }
        
        public void InvokeStartCallback()
        {
            OnStartCallback?.Callbacks.Invoke(this);
        }
            
            


#if DOTWEEN
        
        public void AddToPackage(Tween tween, bool isParallelWithPrevious = false)
        {
            if (tween == null || !tween.IsActive()) return;
            
            tween.Pause();
            //tween.SetAutoKill(false);

            //Debug.Log("TWEEN ADDED TO PACKAGE");
            IEnumerator CoroutineWrapper()
            {
                //tween.SetAutoKill(false);
                tween.Play();
                while (tween.IsActive() && !tween.IsComplete() )
                {
                    //Debug.Log("TWEEN IS ACTIVE");
                    yield return null;
                }
                //Debug.Log("TWEEN IS NOT ACTIVE");
                //tween.Kill();
            }
            
            ExecuteEvents.Add(new SimulationExecution(CoroutineWrapper, isParallelWithPrevious));
        }
        
        
#endif
        
        
    }
}