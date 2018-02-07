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
            //int numOFEndrow = 20;
            //int numOfStartRow = 31;
            //if (table.Length> numOfStartRow+ numOFEndrow)
            //{
            //    var dtNew = table.Select(x => x).Take(numOfStartRow).Skip(numOFEndrow);
            //}
            
            var calculator = new Solution();
            Console.WriteLine("Write your mathematical expressin and press Enter");
            
            var input = Console.ReadLine();
            while (input != "x")
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
               Console.WriteLine($"result is: {calculator.CalculatorParser(input)}");
                sw.Stop();
                var time = sw.Elapsed.TotalMilliseconds;
                Console.WriteLine($"time spent:{time} ms");
               input = Console.ReadLine();
            }
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

        #region Basic Operations
        private string Add(string a, string b)
        {
            ///a and b are evaluated to valid int
            return (int.Parse(a) + int.Parse(b)).ToString();
        }

        private string Sub(string a, string b)
        {
            ///a and b are evaluated to valid int
            return (int.Parse(a) - int.Parse(b)).ToString();
        }

        private string Mult(string a, string b)
        {
            ///a and b are evaluated to valid int
            return (int.Parse(a) * int.Parse(b)).ToString();
        }

        private string Div(string a, string b)
        {
            ///a and b are evaluated to valid int
            return (int.Parse(a) / int.Parse(b)).ToString();
        }
        #endregion

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
            while ((res.Contains("+")) || (res.Contains("-")) && (res.IndexOf('-') != 0) || (res.Contains("*")) || (res.Contains("/")) || (res.Contains("(")))
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
                // return input.Substring(startIndex + 1, endIndex -1); 
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
        //public void RemoveSpaces(out string input)
        //{
        //    // input = input.Replace(" ", "");
        //}
        public void EvaluateUnitElement(string unitElement)
        {
            ///we define Unit Element as a string that has no left or right parenthesis, 
            ///contains only 1 basic operator and it evaluates to valid int
        }

        public string CreateUnitElement(string noParenthesisElement)
        {
            //int sumOfOperators = 0;
             string res = noParenthesisElement;
            //Dictionary<string, int> _operators = new Dict0ionary<string, int>();
            while ((res.Contains("+")) || (res.Contains("-")) && (res.IndexOf('-') != 0) || (res.Contains("*")) || (res.Contains("/")))
            {
                foreach (var s in res)
                {

                    if ((s == '*')||(s== '/'))
                    {
                        int index = res.IndexOf(s);
                        //res = GetComponents(res, index);
                        var r = GetComponents(res, index);
                        res = r;
                        //res = res.Substring(0, index - 1) + Context.Operation(s.ToString(), res.Substring(index - 1, 1), res.Substring(index + 1, 1)) + res.Substring(index + 2);
                        //Debug.WriteLine(res);
                    }
                    else if (((s == '-')&&(res.IndexOf(s)!=0)||(s=='+'))&&(!res.Contains("*"))&&(!res.Contains("/")))
                    {
                        int index = res.IndexOf(s);
                        var r =  GetComponents(res, index);
                        res = r;
                       // res = res.Substring(0, index - 1) + Context.Operation(s.ToString(), res.Substring(index - 1, 1), res.Substring(index + 1, 1)) + res.Substring(index + 2);
                       // Debug.WriteLine(res);
                    }
                }
            }         
            return res;
        }

        
       
    }

}
