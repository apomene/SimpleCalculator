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
            int numOFEndrow = 20;
            int numOfStartRow = 31;
            if (table.Length> numOfStartRow+ numOFEndrow)
            {
                var dtNew = table.Select(x => x).Take(numOfStartRow).Skip(numOFEndrow);
            }
            Console.ReadLine();
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
       
        public  Solution()
        {
            //although mult has the smae priority with div as well as add with sub, 
            //we assign 4 different presendce values for matter of simplicity
            Operators.Add("*", 4);
            Operators.Add("/", 3);
            Operators.Add("+", 2);
            Operators.Add("-", 1);
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

        public void CalculatorParser(string input)
        {
            //resebmle mathematical process pf evaluating an expression 
            ///1st we evaluate parenthesis
        }


        public string InnerElement(string input)
        {
            if (input.IndexOf('(')>-1) //it contains Left parenthesis
            {
                int startIndex = input.LastIndexOf('(');
                int endIndex = input.Substring(startIndex).IndexOf(')');
                return input.Substring(startIndex + 1, endIndex -1);
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
            int sumOfOperators = 0;
            string res = noParenthesisElement;
            foreach (var s in noParenthesisElement)
            {
                if (Operators.ContainsKey(s.ToString()))
                {
                    sumOfOperators++;
                }
            }

            while(sumOfOperators>0)
            {
                //TO DO: -----
                sumOfOperators--;
            }

            return res;
        }

        
       
    }

}
