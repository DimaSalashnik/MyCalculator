using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class calculation
    {
        public double num1;
        public double num2;
        public string result;
        public byte operationCode; // 1 - plus, 2 - minus, 3 - mult, 4 - division
        

        public double Num1 { get => num1; set => num1 = value; }
        public double Num2 { get => num2; set => num2 = value; }
        public string Result { get => result; set => result = value; }
        public byte OperationCode { get => operationCode; set => operationCode = value; }

        public void Calculation()
        {
            switch (this.operationCode)
            {
                case 1:
                    this.Result = (Num1 + Num2).ToString();
                    break;
                case 2:
                    this.Result = (Num1 - Num2).ToString();
                    break;
                case 3:
                    this.Result = (Num1 * Num2).ToString();
                    break;
                case 4:
                    this.Result = (Num2 != 0 ? (Num1 + Num2).ToString() : "EROR");
                    break;
            }
        }
    }
}
