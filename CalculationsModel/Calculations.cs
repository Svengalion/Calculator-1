using System;
using System.Collections.Generic;
using System.Text;

namespace CalculationsModel
{
    public class Calculations
    {
        public string FirstOperand { get; set; } = "";
        public string SecondOperand { get; set; } = "";
        public string Operation { get; set; } = "";
        public string Result { get; private set; } = "";

        private bool isAtomar;

        public Calculations() {}
        public Calculations(string firstOperand, string secondOperand, string operation) 
        {
            CheckOperator(operation);
            CheckOperand(firstOperand);
            if (!isAtomar) CheckOperand(secondOperand);

            FirstOperand = firstOperand;
            SecondOperand = secondOperand;
            Operation = operation;
        }

        protected virtual void Calculate()
        {
            CheckOperator(Operation);
            CheckOperand(FirstOperand);
            if (!isAtomar) CheckOperand(SecondOperand);

            try
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
                            throw new ArgumentException("Zero devision error");
                        }
                        Result = (Convert.ToDouble(FirstOperand) + Convert.ToDouble(SecondOperand)).ToString();
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
                    isAtomar = false;
                    break;
                case "sqrt":
                    isAtomar = true;
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
