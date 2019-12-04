using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace MathOperations
{
    public class CalculatorAssemblyLoadContext : AssemblyLoadContext
    {
        public CalculatorAssemblyLoadContext(/*string mainAssemblyToLoadPath*/) : base(isCollectible: true) // Позваоляет удалить сборку которая не используется
        {
            //_resolver = new AssemblyDependencyResolver(mainAssemblyToLoadPath); // Где находится основная сборка, к которой догружаются контексты
        }

        protected override Assembly Load(AssemblyName name)
        {
            //string assemblyPath = _resolver.ResolveAssemblyToPath(name);
            //if (assemblyPath != null) 
            //{
            //    return LoadFromAssemblyPath(assemblyPath);
            //}

            return null;
        }
    }
}
