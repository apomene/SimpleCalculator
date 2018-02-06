using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class Solution
    {
        List<string> ValidChars = new List<string>()
        {
            "+","-", "*", "/"," ","(",")",
            "0","1","2","3","4","5","6","7","8","9"
        };

        Dictionary<string, int> Operators = new Dictionary<string, int>();
        List<string> _operators = new List<string>();
        public Solution()
        {
            //although mult has the smae priority with div as well as add with sub, 
            //we assign 4 different presendce values for matter of simplicity
            Operators.Add("*", 4);
            Operators.Add("/", 3);
            Operators.Add("+", 2);
            Operators.Add("-", 1);
            _operators.Add("*");
            _operators.Add("/");
            _operators.Add("+");
            _operators.Add("-");
        }

        public int Calculate(string s)
        {
            return int.Parse(CalculatorParser(s));
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

        public string CalculatorParser(string input)
        {
            //resebmle mathematical process of evaluating an expression 
            ///1st we remove parenthesis
            string res = input;
            while (res.Length > 1)
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
            if (input.IndexOf('(') > -1) //it contains Left parenthesis
            {
                int startIndex = input.LastIndexOf('(');
                int endIndex = input.Substring(startIndex).IndexOf(')');
                return input.Substring(startIndex + 1, endIndex - 1);
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
            //Dictionary<string, int> _operators = new Dictionary<string, int>();
            while (res.Length > 1)
            {
                foreach (var s in res)
                {

                    if ((s == '*') || (s == '/'))
                    {
                        int index = res.IndexOf(s);
                        res = res.Substring(0, index - 1) + Context.Operation(s.ToString(), res.Substring(index - 1, 1), res.Substring(index + 1, 1)) + res.Substring(index + 2);
                        //Debug.WriteLine(res);
                    }
                    else if (((s == '-') || (s == '+')) && (!res.Contains("*")) && (!res.Contains("/")))
                    {
                        int index = res.IndexOf(s);
                        res = res.Substring(0, index - 1) + Context.Operation(s.ToString(), res.Substring(index - 1, 1), res.Substring(index + 1, 1)) + res.Substring(index + 2);
                        // Debug.WriteLine(res);
                    }
                }
            }
            return res;
        }



    }

    public abstract class BasicOperators<T>
    {
        public abstract T Operation(T a, T b);
    }


    #region Basic Operations Implementation
    public class Addition : BasicOperators<string>
    {
        ///a and b are evaluated to valid int
        public override string Operation(string a, string b)
        {
            ///a and b are evaluated to valid int
            return (int.Parse(a) + int.Parse(b)).ToString();
        }
    }

    public class Substraction : BasicOperators<string>
    {
        public override string Operation(string a, string b)
        {
            ///a and b are evaluated to valid int
            return (int.Parse(a) - int.Parse(b)).ToString();
        }
    }


    public class Multiplication : BasicOperators<string>
    {
        public override string Operation(string a, string b)
        {
            ///a and b are evaluated to valid int
            return (int.Parse(a) * int.Parse(b)).ToString();
        }
    }

    public class Division : BasicOperators<string>
    {
        public override string Operation(string a, string b)
        {
            ///a and b are evaluated to valid int
            return (int.Parse(a) / int.Parse(b)).ToString();
        }
    }


    #endregion


  
    public class Context
    {
        private static Dictionary<string, BasicOperators<string>> _operations = new Dictionary<string, BasicOperators<string>>();

        static Context()
        {
            _operations.Add("+", new Addition());
            _operations.Add("-", new Substraction());
            _operations.Add("*", new Multiplication());
            _operations.Add("/", new Division());
        }

        public static string Operation(string operationSymbol, string inputA, string inputB)
        {
            return _operations[operationSymbol].Operation(inputA, inputB);
        }
    }


}
