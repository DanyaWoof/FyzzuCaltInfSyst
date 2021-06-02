using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace isFCAwf
{
    class FuzzyCounter
    {
        private string nmu = "3";
        private string lsimbol = "(";
        private string rsimbol = ")";
        private char[] chArray = { '*', '/', '-', '+' };
        public List<int[,]> variableSets = new List<int[,]>(10);//$#A1#$
        public List<string[]> listVariableValuesNM = new List<string[]>(10);

        public double[] GetRezult(string s) // TODO :передавать еще и код nmu | добавить обновление nmu
        {
            string res = "";
            if (ParenthesesEquality(s, "$", "$", out int p))
                res = AnalizatorStroki(s);
            else
                MessageBox.Show("Проверьте закрытость операндов символами $$");
            return SearcherInPrivateCollection(res.Substring(1, res.Length - 2));

        }

        private string AnalizatorStroki(string formStr)
        {
            //formStr = formStr.Replace(" ", "");  //удаляем все пробелы
            string leftHalfStr, rightHalfStr, centerStr;
            int numofPairedBrackets; // кол-во пар скобок

            if (formStr.Contains(lsimbol) && formStr.Contains(rsimbol) && ParenthesesEquality(formStr, lsimbol, rsimbol, out numofPairedBrackets))  // Если в строке есть скобки, то строка разделяется на 3 подстроки таким образом: 
            {                                                    // -Основная строка разделяется на две части, где все что до "(" - левая, а все что после ")" - правая 
                                                                 //  и записываются во временные переменнык leftHalfStr и rightHalfStr.
                                                                 // -Все что между () записывается в центральную строку centerStr без скобок передается в метод для расчета.
                                                                 // -После проведения расчета метод возвращает измененную центральную строку и они сшиваются воедино. 
                                                                 // -Цикл повторяется до тех пор, пока есть "()"

                for (int i = 0; i < numofPairedBrackets; i++)

                {
                    int rCount = formStr.IndexOf(rsimbol); //определяем индекс первого вхождения в закрывающую скобку и 

                    string subString = formStr.Substring(0, rCount); // обрезаем строку до этого 
                    int lCount = subString.LastIndexOf(lsimbol); // находим индекс последнего символа открывающей скобки
                    centerStr = subString.Substring(lCount + 1); //обрезаем все + открывающую парную скобку. Получаем подстроку внутри ближайшей пары скобок без скобок т.е. centerStr

                    if (lCount == 0)
                        leftHalfStr = "";
                    else
                        leftHalfStr = formStr.Substring(0, lCount);

                    if (rCount == formStr.Length)
                        rightHalfStr = "";
                    else
                        rightHalfStr = formStr.Substring(rCount + 1, formStr.Length - rCount - 1);
                    //со скобками все работает

                    centerStr = PoiskOperandov(centerStr);
                    formStr = leftHalfStr + centerStr + rightHalfStr;
                }
            }
            return PoiskOperandov(formStr);
        }

        /// <summary>
        /// Метод определения парности элементов строки p1=p2. Если элементы одинаковы - проверяется кратность двум(2).
        /// </summary>
        /// <param name="str">Строка для поиска</param>
        /// <returns>true/false</returns>
        private bool ParenthesesEquality(string str, string p1, string p2, out int count) // определение парности скобок в строке
        {
            count = 0;

            if (p1 == p2)
            {
                int oneParamDevb2 = str.ToCharArray().Where(c => c == p1[0]).Count();
                if (oneParamDevb2 % 2 == 0)
                    return true;
                return false;

            }
            //int ls = 0, rs = 0;

            //int lsimbcount = str.ToCharArray().Where(c => c == "("[0]).Count();
            //int rsimbcount = str.ToCharArray().Where(c => c == ")"[0]).Count();
            int lsimbcount = str.ToCharArray().Where(c => c == p1[0]).Count();
            int rsimbcount = str.ToCharArray().Where(c => c == p2[0]).Count();
            if (lsimbcount == rsimbcount)
            {
                count = lsimbcount;
                return true;
            }

            else
            {
                MessageBox.Show("Найдены лишние скобки. Проверьте совпадение скобок и попробуйте еще раз");
                return false;
            }
        }

        private string PoiskOperandov(string str)
        {

            int[] rptr = new int[4]; // количество операторов по типу, где 0(1) - chArray[0] = * и т.д.
            string bufferStr = str;
            //string bufferStr = "$Инвестор1$+$Инвестор2$/$Инвестор3$";
            //string bufferStr = "$Инвестор1$*$Инвестор2$/$Инвестор3$+$DDDd$";
            //string bufferStr = "$Инвестор1$+$Инвестор2$*$Инвестор3$+$DDDd$";
            string returnedValue = str;
            int ls = 0;
            int rs = 0;
            string leftStr = "";
            string middleStr = "";
            string rightStr = "";

            for (int i = 0; i < rptr.Length; i++)
            {
                rptr[i] = bufferStr.ToCharArray().Where(c => c == chArray[i]).Count(); //??????

                for (int j = 0; rptr[i] > 0 & j < rptr[i]; j++)
                {
                    if (!bufferStr.Contains("$" + chArray[i] + "$"))
                    {
                        MessageBox.Show("Между операндами и операторами не дролжно быть лишних знаков кроме $. В начале формулы не должны быть отрицательные операнды");
                        break;
                    }

                    else
                    {
                        int index = bufferStr.IndexOf("$" + chArray[i] + "$");
                        ls = bufferStr.Substring(0, index).LastIndexOf("$");
                        rs = (bufferStr.Substring(index + 3, bufferStr.Length - index - 3).IndexOf("$")) + index + 4; //?? index +2?

                        leftStr = bufferStr.Substring(0, ls);
                        middleStr = bufferStr.Substring(ls, rs - ls);
                        rightStr = bufferStr.Substring(rs, bufferStr.Length - rs);
                        bufferStr = leftStr + TwoOperandsCounter(middleStr, chArray[i]) + rightStr;
                    }
                }
            }
            return bufferStr;
        }

        public string TwoOperandsCounter(string str, char ch) //str передается в подготовленном формаате $имя_подмножества$+-*/$имя_подмножества$
        {
            int kodName; //promej
            double m1 = 0;
            double m2 = 0;
            double a = 0;
            double b = 0;

            double[] fOp;
            double[] sOp;
            int index = str.IndexOf("$" + ch + "$");
            string firstOp = str.Substring(1, index - 1);
            if (firstOp.Contains("#"))
                fOp = SearcherInPrivateCollection(firstOp);
            else
                fOp = GetValuesNM(nmu, firstOp);

            string secondOp = str.Substring(index + 3, str.Length - index - 4);
            if (secondOp.Contains("#"))
                sOp = SearcherInPrivateCollection(secondOp);
            else
                sOp = GetValuesNM(nmu, secondOp);

            switch (ch)
            {                               // fOp/sOp [m1,m2,a,b] = [0,1,2,3]
                case '*':
                    m1 = fOp[0] * sOp[0];
                    m2 = fOp[1] * sOp[1];
                    a = fOp[0] * sOp[0] - (fOp[0] - fOp[2]) * (sOp[0] - sOp[2]);
                    b = (fOp[1] + fOp[3]) * (sOp[1] + sOp[3]) - fOp[1] * sOp[1];
                    break;
                case '/':
                    m1 = fOp[0] / sOp[1];
                    m2 = fOp[1] / sOp[0];
                    a = (fOp[0] * sOp[3] + sOp[1] * fOp[2]) / (sOp[1] * sOp[1] + sOp[1] * sOp[3]);
                    b = (sOp[0] * fOp[3] + fOp[1] * sOp[2]) / (sOp[0] * sOp[0] - sOp[0] * sOp[2]);
                    break;
                case '-':
                    m1 = fOp[0] - sOp[1];
                    m2 = fOp[1] - sOp[0];
                    a = fOp[2] + sOp[3];
                    b = fOp[3] + sOp[2];
                    break;
                case '+':
                    m1 = fOp[0] + sOp[0];
                    m2 = fOp[1] + sOp[1];
                    a = fOp[2] + sOp[2];
                    b = fOp[3] + sOp[3];
                    break;
                default:
                    break;
            }

            kodName = listVariableValuesNM.Count() + 1; // ?? +0 ?
            string[] rez = { "A" + kodName.ToString(), m1.ToString(), m2.ToString(), a.ToString(), b.ToString() };
            listVariableValuesNM.Add(rez);
            return "$#A" + kodName.ToString() + "#$";
        }
        private double[] SearcherInPrivateCollection(string s)
        {
            double[] mas = new double[4];
            s = s.Substring(1, s.Length - 2);
            for (int i = 0; i < listVariableValuesNM.Count; i++)
            {
                if (listVariableValuesNM[i][0] == s)
                {
                    for (int j = 0; j < 4; j++)
                        mas[j] = Double.Parse(listVariableValuesNM[i][j + 1]);
                }
            }
            return mas;
        }



        private double[] GetValuesNM(string nmuPK, string nmName) //код множества и имя подмножества // Подключение MS SQL
        {
            //AND [Имя_НМ] =  'Инвестор1'
            string connectionString = @"Data Source=DESKTOP-EI0AJ7L\SQLEXPRESS;Initial Catalog=FuzzyCalc;Integrated Security=True";
            string n_m_u = nmuPK; // вместо 2 нечеткие множества M_U
            if (nmName != "")
                nmName = " AND Имя_НМ = '" + nmName + "'";

            string query = $"SELECT Имя_НМ, m1, m2, a, b FROM dbo.[Нечеткие_множества_A<u>] WHERE Код_М_U = {n_m_u} {nmName}";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                //int c = reader.FieldCount; //получение кол-ва строк записай
                double[] nm = new double[4];

                if (reader.HasRows) // если есть данные
                {
                    reader.Read();
                    for (int i = 1; i < nm.Length + 1; i++)
                        nm[i - 1] = Double.Parse(reader.GetValue(i).ToString());
                }
                reader.Close();
                return nm;
            }
        }
    }
}
