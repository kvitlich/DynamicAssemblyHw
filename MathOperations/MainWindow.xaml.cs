using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
            var assemblyPath = @"C:\Users\ww\source\repos\Factorial\Factorial\bin\Debug\netcoreapp3.0\Factorial.dll";
            bool tryParseFlag = false;
            WeakReference weakReference;
            string[] args = enterTextBox.Text.Split(',');
            for (int i = 0; i < args.Length; i++)
            { int num;
                if (!Int32.TryParse(args[i],out num))
                {
                    tryParseFlag = true;
                }
            }
            if (args.Length > 10 || args.Length < 10 || tryParseFlag)
            { 
                MessageBox.Show("Введите 10 чисел через запятую!");
                weakReference = null;
                return;
            }
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

        }

        private void ProcessFactorial(string assemblyPath, out WeakReference weakReference)
        {
            var args = enterTextBox.Text.Split(',');
            MessageBox.Show(args.FirstOrDefault());
            CalculatorAssemblyLoadContext context = new CalculatorAssemblyLoadContext();
            var calculatorAssembly = Assembly.LoadFile(assemblyPath);
           
            weakReference = new WeakReference(context, true);

            Object mainObject = calculatorAssembly.CreateInstance("Factorial.MathOperations", false);
            MethodInfo executedMethod = calculatorAssembly.GetType("Factorial.MathOperations").GetMethod("Factorial");
            Object returnedResult = executedMethod.Invoke(mainObject, new Object[] { args });


            var resultFactorialCalc = calculatorAssembly.EntryPoint.Invoke(null, new object[] { args });
            List<NumberResult> results = new List<NumberResult>();            
            context.Unload();
            long[] returnedResultAsLong = returnedResult as long[];
            for (int i = 0; i < returnedResultAsLong.Length; i++)
            {
                results.Add(new NumberResult { Result = returnedResultAsLong[i].ToString()});
            }

            dataGrid.ItemsSource = results;

        }
    }
}
