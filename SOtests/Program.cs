using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Threading;
using System.Net.Http;
using System.Data;

namespace SOtests
{
    class Program
    {
        static int[] table = new int[80];

        
        static void Main(string[] args)
        {                  
            var calculator = new Solution();
            Console.WriteLine("Write your mathematical expressin and press Enter");
            
            var input = Console.ReadLine();
            while (input != "x")
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
               //TO DO--- Thread myTimer = new Thread(Action()=>myTiming()).Start;
                Console.WriteLine($"result is: {calculator.CalculatorParser(input)}");
                sw.Stop();
                var time = sw.Elapsed.TotalMilliseconds;
                Console.WriteLine($"time spent:{time} ms");
                input = Console.ReadLine();
            }
        }

        private static bool myTiming()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while(sw.ElapsedMilliseconds<10*1000)
            {

            }
            sw.Stop();
            return true;
        }
    }


    public class Solution
    {
        List<string> ValidChars = new List<string>()
        {
            "+","-", "*", "/"," ","(",")",
            "0","1","2","3","4","5","6","7","8","9"          
        };

        Dictionary<string, int> Operators = new Dictionary<string, int>();
        char[] _operators;
        public  Solution()
        {
            //although mult has the smae priority with div as well as add with sub, 
            //we assign 4 different presendce values for matter of simplicity
            Operators.Add("*", 4);
            Operators.Add("/", 3);
            Operators.Add("+", 2);
            Operators.Add("-", 1);
            _operators = "+-*/".ToCharArray();
        }

        public int Calculate(string s)
        {
            //TO DO 
            return 0;
        }

       public bool WhileEvaluation(string expression)
        {
            var res1 = NumOfOperants(expression) > 0;
            var res2 = expression.Contains("(");
            var res3 = ((NumOfOperants(expression)== 1) && (expression.IndexOf('-') == 0));
            return (res1 && !res3) || res2;
        }

        public string GetComponents(string UnitElement,int index)
        {
            var index1 = UnitElement.Substring(0,index).LastIndexOfAny(_operators);
            index1 = index1 < index ? index1+1 : 0;
            var index2 = UnitElement.IndexOfAny(_operators, index+1);
            index2 =index2>-1 ? index2 : UnitElement.Length;

            var res = UnitElement.Replace(UnitElement.Substring(index1, index2 - index1), Context.Operation(UnitElement[index].ToString(), UnitElement.Substring(index1, index - index1), UnitElement.Substring(index + 1, index2 - index - 1)));
            //return Context.Operation(UnitElement[index].ToString(), UnitElement.Substring(index1, index - index1), UnitElement.Substring(index + 1, index2 - index-1));
            return res;
        }


        public string CalculatorParser(string input)
        {
            //resebmle mathematical process of evaluating an expression 
            ///1st we remove spaces

            string res = RemoveSpaces(input);
            while(WhileEvaluation(res))
            {
                var inner = InnerElement(res);
                //res = res.Replace(innner, "placeholder");
                var unit = CreateUnitElement(inner);
                if (res.Contains("("))
                    res = res.Replace($"({inner})", unit);
                else
                    res = res.Replace(inner, unit); 
            }
            return res;
        }


        public string InnerElement(string input)
        {
            if (input.IndexOf('(')>-1) //it contains Left parenthesis
            {
                int startIndex = input.LastIndexOf('(');
                int endIndex = input.Substring(startIndex).IndexOf(')');
                var res = input.Substring(startIndex + 1, endIndex - 1);
                return res;
            }
            ///else no parenthesis contained
            return input;
        }

        public string RemoveSpaces(string input)
        {
            string res = input.Replace(" ", "");
            return res;
        }

        public bool IsValidInputBaSe(string input)
        {
            foreach (var s in input)
            {
                if (!ValidChars.Contains(s.ToString()))
                {
                    return false;
                }
            }
            return true;
        }
            
        public string CreateUnitElement(string noParenthesisElement)
        {
            //int sumOfOperators = 0;
             string res = noParenthesisElement;
            //Dictionary<string, int> _operators = new Dict0ionary<string, int>();
            while (WhileEvaluation(res))
            {
                foreach (var s in res)
                {

                    if ((s == '*')||(s== '/'))
                    {
                        int index = res.IndexOf(s);
                        var r = GetComponents(res, index);
                        res = r;
                        //res = res.Substring(0, index - 1) + Context.Operation(s.ToString(), res.Substring(index - 1, 1), res.Substring(index + 1, 1)) + res.Substring(index + 2);
                        //Debug.WriteLine(res);
                    }
                    else if (WhileEvaluation2(s,res))
                    {
                        int index = res.IndexOf(s);
                        if (res[index+1]=='-'|| res[index + 1] == '+')
                        {
                            res = res.Replace(res.Substring(index, 2), ReplaceOperator(res.Substring(index, 2)));
                            //break;
                        }
                        var r =  GetComponents(res, index);
                        res = r;
                       // res = res.Substring(0, index - 1) + Context.Operation(s.ToString(), res.Substring(index - 1, 1), res.Substring(index + 1, 1)) + res.Substring(index + 2);
                       // Debug.WriteLine(res);
                    }
                }
            }         
            return res;
        }

        private bool WhileEvaluation2(char s, string expression)
        {
            var res1 = (s == '-') && (expression.IndexOf(s) != 0); //it contains substraction as an operation not as a negative int
            var res2 = (s == '+'); //it contains addition
            var res3 = (!expression.Contains("*")) && (!expression.Contains("/")); //it does not contain multiplication or division
            return (res1 || res2) && (res3);
        }

        public string ReplaceOperator(string multiOperators)
        {
            if (multiOperators == "-+")
                return "-";
            else if (multiOperators == "+-")
                return "-";
            else
                return "+";
        }

        public int NumOfOperants(string input)
        {
            var res = 0;
            foreach(var s in input)
            {
                if (_operators.Contains(s))
                {
                    res++;
                }
            }
            return res;
        }


    }

}
