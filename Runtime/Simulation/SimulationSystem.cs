using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shun_Utilities
{
    public class SimulationSystem : MonoBehaviour
    {
        private bool _isEnable = true;

        
        

        public void SetEnable(bool isEnable)
        {
            _isEnable = isEnable;
        }
        
        public void StopAllSimulation()
        {
            StopAllCoroutines();
        }


        public void Execute(SimulationPackage simulationPackage, Action finishCallback = null)
        {
            if (!_isEnable) return;

            StartCoroutine(ExecuteOneCoroutine(simulationPackage, finishCallback));
        }

        
        
        public void ExecuteSequential(Queue<SimulationPackage> simulationQueue, Action finishCallback = null)
        {
            if (!_isEnable) return;

            StartCoroutine(ExecuteAllCoroutineSequential(simulationQueue, finishCallback));
        }
        
        public void ExecuteParallel(List<SimulationPackage> simulations, Action finishCallback = null)
        {
            if (!_isEnable) return;

            StartCoroutine(ExecuteAllCoroutineParallel(simulations, finishCallback));
        }
        
        public void ExecuteSequenceParallel(Queue<List<SimulationPackage>> simulationQueue, Action finishCallback = null)
        {
            if (!_isEnable) return;

            StartCoroutine(ExecuteSequenceParallelCoroutine(simulationQueue, finishCallback));
        }

        public void ExecuteParallelSequence(List<Queue<SimulationPackage>> simulationQueue, Action finishCallback = null)
        {
            if (!_isEnable) return;

            StartCoroutine(ExecuteParallelSequenceCoroutine(simulationQueue, finishCallback));
        }

        private IEnumerator ExecuteAllCoroutineSequential(Queue<SimulationPackage> simulationQueue, Action finishCallback = null)
        {
            // if (_isExecuting)
            // {
            //     yield break;
            // }
            //
            // _isExecuting = true;
            while (simulationQueue.Count > 0 && _isEnable)
            {
                var simulationObject = simulationQueue.Dequeue();
                
                yield return StartCoroutine(ExecuteCoroutineSimulation(simulationObject));
            }
            
            finishCallback?.Invoke();
            // _isExecuting = false;
        }
        
        private IEnumerator ExecuteAllCoroutineParallel(List<SimulationPackage> simulations, Action finishCallback = null)
        {
            // if (_isExecuting)
            // {
            //     yield break;
            // }
            //
            // _isExecuting = true;
            List<IEnumerator> runningCoroutines = new();
            for (var index = 0; index < simulations.Count; index++)
            {
                var simulation = simulations[index];
                
                runningCoroutines.Add(ExecuteCoroutineSimulation(simulation));
            }

            // WaitPhase for all running coroutines to complete
            yield return StartCoroutine(WaitForAll(runningCoroutines));

            finishCallback?.Invoke();
            // _isExecuting = false;
        }
        
        
        private IEnumerator ExecuteSequenceParallelCoroutine(Queue<List<SimulationPackage>> simulationQueue, Action finishCallback)
        {
            // if (_isExecuting)
            // {
            //     yield break;
            // }
            // _isExecuting = true;

            while (simulationQueue.Count > 0)
            {
                var simulationList = simulationQueue.Dequeue();
                yield return StartCoroutine(ExecuteAllCoroutineParallel(simulationList));
            }

            finishCallback?.Invoke();
            //_isExecuting = false;
        }
        
        private IEnumerator ExecuteParallelSequenceCoroutine(List<Queue<SimulationPackage>> simulationQueue, Action finishCallback)
        {
            // if (_isExecuting)
            // {
            //     yield break;
            // }
            // _isExecuting = true;

            List<IEnumerator> runningCoroutines = new();
            for (var index = 0; index < simulationQueue.Count; index++)
            {
                var simulationList = simulationQueue[index];
                runningCoroutines.Add(ExecuteAllCoroutineSequential(simulationList));
            }

            // WaitPhase for all running coroutines to complete
            yield return StartCoroutine(WaitForAll(runningCoroutines));

            finishCallback?.Invoke();
            //_isExecuting = false;
        }

        private IEnumerator ExecuteOneCoroutine(SimulationPackage simulationPackage, Action finishCallback = null)
        {
            // if (_isExecuting)
            // {
            //     yield break;
            // }
            // _isExecuting = true;

            yield return StartCoroutine(ExecuteCoroutineSimulation(simulationPackage));

            finishCallback?.Invoke();
            //_isExecuting = false;
        }
        
        
        
        
        IEnumerator ExecuteCoroutineSimulation(SimulationPackage simulationPackage)
        {   
            simulationPackage.InvokeStartCallback();
            
            List<Func<IEnumerator>> runningCoroutines = new ();

            foreach (var execution in simulationPackage.ExecuteEvents)
            {
                if (execution == null) continue;
                
                if (!execution.IsParallel && runningCoroutines.Count > 0)
                {
                    yield return StartCoroutine(WaitForAll(runningCoroutines));
                    runningCoroutines.Clear();
                }

                var executionFunc = execution.ExecutionFunc;

                if (execution.IsParallel)
                {
                    runningCoroutines.Add(executionFunc);
                }
                else
                {
                    yield return StartCoroutine(executionFunc.Invoke());
                }
            }
            
            if (runningCoroutines.Count > 0)
                yield return StartCoroutine(WaitForAll(runningCoroutines));

            

            simulationPackage.InvokeCompleteCallback();
        }

        
        public IEnumerator WaitForAll(List<Func<IEnumerator>> coroutines)
        {
            int tally = 0;

            foreach(Func<IEnumerator> c in coroutines)
            {
                StartCoroutine(RunCoroutine(c.Invoke()));
            }

            while (tally > 0)
            {
                yield return null;
            }

            IEnumerator RunCoroutine(IEnumerator c)
            {
                tally++;
                yield return StartCoroutine(c);
                tally--;
            }
        }
        
        public IEnumerator WaitForAll(List<IEnumerator> coroutines)
        {
            int tally = 0;

            foreach(IEnumerator c in coroutines)
            {
                StartCoroutine(RunCoroutine(c));
            }

            while (tally > 0)
            {
                yield return null;
            }

            IEnumerator RunCoroutine(IEnumerator c)
            {
                tally++;
                yield return StartCoroutine(c);
                tally--;
            }
        }
        
    }
}

