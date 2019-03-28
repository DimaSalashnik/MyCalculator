using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        calculation calculator;
        bool isSecondNumber = false;
        bool percentOperation = false;
        bool infoOperationAct = false;
        bool chekOperationAct = false;
        string operationLabel;
        string infoOperationNum2;

        public Form1()
        {
            InitializeComponent();
            calculator = new calculation();
            isSecondNumber = false;
        }

        //метот для вывода числа на дисплей
        public void AddDigit(string Digit)
        {
            if (textBoxDisplay.Text.Equals("0"))
                textBoxDisplay.Text = Digit;
            else
                textBoxDisplay.Text += Digit;
        }

        //вывод на экран нажатую кнопку
        private void ButtonDigit(object sender, EventArgs e)
        {
            if (chekOperationAct == false)
                AddDigit(((Button)sender).Text);
            else
            {
                
            } 
                
            //вывод информации о придыдущих действиях
            if (isSecondNumber == true)
                infoOperationNum2 += ((Button)sender).Text;
            InfoOperation_Label();
        }

        //полная очиста калькулятора
        private void buttonClear_Click(object sender, EventArgs e)
        {
            calculator.num1 = 0;
            calculator.num2 = 0;
            calculator.operationCode = 0;
            textBoxDisplay.Text = "0";
            InfoOperation.Text = "";
            infoOperationNum2 = "0";
            isSecondNumber = false;
            infoOperationAct = false;
            operationLabel = "";
        }

        //изменение знака числа
        private void buttonPlusMinus_Click(object sender, EventArgs e)
        {
            textBoxDisplay.Text = (Convert.ToDouble(textBoxDisplay.Text) * -1).ToString();
        }

        //запятая
        private void buttonComma_Click(object sender, EventArgs e)
        {
            if (!textBoxDisplay.Text.Contains(','))
                textBoxDisplay.Text += ",";
        }

        //возведение в квадрат
        private void buttonDouble_Click(object sender, EventArgs e)
        {
            textBoxDisplay.Text = (Math.Pow(Convert.ToDouble(textBoxDisplay.Text), 2)).ToString();
        }
        
        //корень числа
        private void buttonSQRT_Click(object sender, EventArgs e)
        {
            double NegativeNumberChek = Convert.ToDouble(textBoxDisplay.Text);
            NegativeNumberChek /= Convert.ToDouble(textBoxDisplay.Text);
            if (NegativeNumberChek == 1) //проверка на отрицательное число
                textBoxDisplay.Text = (Math.Sqrt(Convert.ToDouble(textBoxDisplay.Text))).ToString();
            else
                textBoxDisplay.Text = "Eror";
        }

        //квадратный корень числа
        private void buttonDoubleSQRT_Click(object sender, EventArgs e)
        {
            double NegativeNumberChek = Convert.ToDouble(textBoxDisplay.Text);
            NegativeNumberChek /= Convert.ToDouble(textBoxDisplay.Text);
            if (NegativeNumberChek == 1) //проверка на отрицательное число
                textBoxDisplay.Text = (Math.Sqrt(Math.Sqrt(Convert.ToDouble(textBoxDisplay.Text)))).ToString();
            else
                textBoxDisplay.Text = "Eror";
        }

        //операции над числами
        public void ChooseOperation_Click(object sender, EventArgs e)
        {
            switch (((Button)sender).Text)
            {
                case "+":
                    calculator.OperationCode = 1;
                    break;
                case "-":
                    calculator.OperationCode = 2;
                    break;
                case "x":
                    calculator.OperationCode = 3;
                    break;
                case "÷":
                    calculator.OperationCode = 4;
                    break;
            }
            operationLabel = ((Button)sender).Text; //знак с кнопки
            calculator.Num1 = Convert.ToDouble(textBoxDisplay.Text);
            textBoxDisplay.Text = "0"; //очистка дисплея
            this.isSecondNumber = true;
            //проверка: содержит ли переменная значение
            if (infoOperationNum2 != null)
                infoOperationAct = true;
            else
                infoOperationAct = false;
            //проверка: было ли второе значение
            if (infoOperationAct == false)
            {
                InfoOperation.Visible = true; //появления lable-а
                InfoOperation_Label();
                chekOperationAct = true;
            }
            else
            {
                InfoOperation.Text = "";
                infoOperationNum2 = "";
            }
        }
        
        //вывод первого числа, тип операции и второе число
        public void InfoOperation_Label()
        {
            double n1 = calculator.num1;
            InfoOperation.Text = $"{n1} {operationLabel} {infoOperationNum2}";
        }

        //вывод результата на дисплей
        public void buttonEquals_Click(object sender, EventArgs e)
        {
            //исправление бага (ошибка, нет ответа)
            if (textBoxDisplay.Text == "0")
                textBoxDisplay.Text = "0";
            else if (textBoxDisplay.Text != "0" && isSecondNumber == false)
                return;
            else
            {
                if (percentOperation == true)
                    textBoxDisplay.Text = ((calculator.num1 * calculator.num2) / 100).ToString();
                else
                {
                    calculator.Num2 = Convert.ToDouble(textBoxDisplay.Text);
                    calculator.Calculation();
                    textBoxDisplay.Text = calculator.Result;
                }
            }
            calculator.num1 = 0;
            InfoOperation.Text = "0";
            InfoOperation.Visible = false;
            //исправление бага (нету очистки второго числа)
            calculator.num2 = 0;
            infoOperationNum2 = "";
        }

        //удаление последнего символа
        public void buttonDelete_Click(object sender, EventArgs e)
        {
            int lastNumber = textBoxDisplay.TextLength - 1;
            //проверка: последний ли символ в строке
            if (lastNumber == 0) //если да, то ставим ноль
                textBoxDisplay.Text = "0";
            else if (lastNumber > 0 && lastNumber != 0) //если нет, то удаляем последнее число
                textBoxDisplay.Text = textBoxDisplay.Text.Remove(lastNumber);
        }

        //очистка случайно введенного второго числа
        public void buttonClearE_Click(object sender, EventArgs e)
        {
            //проверка: было ли введено второе число
            if (isSecondNumber == false)
                textBoxDisplay.Text = "0";
        }
        
        //процент от числа
        public void buttonPercent_Click(object sender, EventArgs e)
        {
            //запоминание первого числа
            calculator.num1 = Convert.ToDouble(textBoxDisplay.Text);
            //вывод информации в дисплей и label
            textBoxDisplay.Text = "0";
            InfoOperation.Text = $"{calculator.num1} %";
            //кнопка '%' была нажата
            percentOperation = true;
        }

    }
}