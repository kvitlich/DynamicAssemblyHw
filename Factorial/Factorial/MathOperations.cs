using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Factorial
{
    public class MathOperations
    {
        public long[] Factorial(string[] args)
        {
            int[] argsInt32 = new int[args.Length];
            for (int i = 0; i < args.Length; i++)
            {
                argsInt32[i] = Int32.Parse(args[i]);
            }
            long[] results = new long[argsInt32.Length];
            for (int j = 0; j < argsInt32.Length; j++)
            {
                long result = 1;
                for (int i = argsInt32[j]-1; i >= 1; i--)
                {
                    result += i * result;
                }
                results[j] = result;
            }


            return results;
        }
    }
}
