using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MathOperations
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CountFactorial(object sender, RoutedEventArgs e)
        {
            var currentDomain = AppDomain.CurrentDomain;
            var assemblyPath = @"C:\Users\ТажибаевД\source\repos\Factorial\Factorial\bin\Debug\netcoreapp3.0\Factorial.dll";
            WeakReference weakReference;

            ProcessFactorial(assemblyPath, out weakReference);
           
            for (int i = 0; weakReference.IsAlive && (i < 10); i++)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            currentDomain = AppDomain.CurrentDomain;
            Console.WriteLine("*******************************");
            foreach (var assembly in currentDomain.GetAssemblies())
            {
                Console.WriteLine($"{assembly.GetName()}");
            }

            Console.ReadKey();
        }

        static void ProcessFactorial(string assemblyPath, out WeakReference weakReference)
        {
            CalculatorAssemblyLoadContext context = new CalculatorAssemblyLoadContext();
            var calculatorAssembly = context.LoadFromAssemblyPath(assemblyPath);

            weakReference = new WeakReference(context, true);

            var currentDomain = AppDomain.CurrentDomain;
            Console.WriteLine("*******************************");
            foreach (var assembly in currentDomain.GetAssemblies())
            {
                Console.WriteLine($"{assembly.GetName()}");
            }

            var args = new object[] { new string[] { "1", "6" } };
            calculatorAssembly.EntryPoint.Invoke(null, args);

            context.Unload();
        }

        private void Changing(object sender, DependencyPropertyChangedEventArgs e)
        {
      
        }
    }
}
