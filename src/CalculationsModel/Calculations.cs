using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CalculationsModel
{
    public class Calculations
    {
        private double result;
        private int proc = 1;
        public string FirstOperand { get; set; } = "";
        public string SecondOperand { get; set; } = "";
        public string Operation { get; set; } = "";
        public string Result { get; private set; } = "";

        public bool IsAtomar { get; private set; } 

        public Calculations() {}
        public Calculations(string firstOperand, string secondOperand, string operation) 
        {
            CheckOperator(operation);
            CheckOperand(firstOperand);
            if (!IsAtomar) CheckOperand(secondOperand);

            FirstOperand = firstOperand;
            SecondOperand = secondOperand;
            Operation = operation;
        }

        public virtual void Calculate()
        {
            CheckOperator(Operation);
            CheckOperand(FirstOperand);
            if (!IsAtomar) CheckOperand(SecondOperand);

            try
            {
                checked
                {
                    switch (Operation)
                    {
                        case "+":
                            Result = (Convert.ToDouble(FirstOperand) + Convert.ToDouble(SecondOperand)).ToString();
                            break;
                        case "-":
                            Result = (Convert.ToDouble(FirstOperand) - Convert.ToDouble(SecondOperand)).ToString();
                            break;
                        case "*":
                            Result = (Convert.ToDouble(FirstOperand) * Convert.ToDouble(SecondOperand)).ToString();
                            break;
                        case "/":
                            if (SecondOperand == "0")
                            {
                                Result = "Zero devision error";
                                throw new DivideByZeroException("Zero devision error");
                            }
                            Result = (Convert.ToDouble(FirstOperand) / Convert.ToDouble(SecondOperand)).ToString();
                            break;
                        case "√":
                            var firstOperand = Convert.ToDouble(FirstOperand);
                            if (firstOperand < 0)
                            {
                                Result = "Operand is negative";
                                throw new ArgumentException("Operand is negative");
                            }
                            Result = Math.Sqrt(firstOperand).ToString();
                            break;
                        case "!":
                            var n = Convert.ToDouble(FirstOperand);
                            if (n <= 1) Result = BigInteger.One.ToString();
                            if (proc == 0) proc = Environment.ProcessorCount;
                            if (proc <= 1 || n <= proc)
                            {
                                result = (double)BigInteger.One;
                                for (double i = 2; i <= n; i++) 
                                    result *= i;
                                Result = result.ToString();
                            }

                            Task[] tasks = new Task[proc];

                            var results = new ConcurrentBag<BigInteger>();
                            TaskFactory factory = new TaskFactory();
                            var running = n / proc > 10000 ? TaskCreationOptions.LongRunning : TaskCreationOptions.None;
                            for (int i = 0; i < tasks.Length; i++)
                            {
                                var k = i;
                                tasks[i] = factory.StartNew(() =>
                                {
                                    var res = BigInteger.One;
                                    for (int j = k + 1; j <= n; j += proc) res *= j;
                                    while (results.TryTake(out var take)) res *= take;
                                    results.Add(res);
                                }, running);
                            }
                            Task.WaitAll(tasks);
                            break;
                    }
                }
                
            }
            catch
            {
                Result = "Unknown error";
                throw;
            }
        }


        protected virtual void CheckOperator(string operation)
        {
            switch (operation)
            {
                case "+":
                case "-":
                case "*":
                case "/":
                    IsAtomar = false;
                    break;
                case "!":
                case "√":
                    IsAtomar = true;
                    break;
                default:
                    Result = "Operation error";
                    throw new ArgumentException("Operation error");
            }
        }



        private void CheckOperand(string operand)
        {
            try
            {
                Convert.ToDouble(operand);
            }
            catch
            {
                Result = "Operand error";
                throw;
            }
        }
    }

}
