using System;
using System.Collections;

namespace Shun_Utilities
{
    public class SimulationExecution
    {
        public readonly Func<IEnumerator> ExecutionFunc;
        public readonly bool IsParallel;
        
        public SimulationExecution(Func<IEnumerator> executionFunc, bool isParallel = false)
        {
            ExecutionFunc = executionFunc;
            IsParallel = isParallel;
        }
    }
}