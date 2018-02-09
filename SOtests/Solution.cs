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
        char[] _operators;
        public Solution()
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
            string res = RemoveSpaces(s);
            while (WhileEvaluation(res))
            {
                if (NumOfParenthesis(res) == 5)
                {
                    string r = res;
                }
                var inner = InnerElement(res);
                //res = res.Replace(innner, "placeholder");
                var unit = CreateUnitElement(inner);
                if (res.Contains("("))
                    res = res.Replace($"({inner})", unit);
                else
                    res = res.Replace(inner, unit);
            }
            return int.Parse(res);
        }

        public bool WhileEvaluation(string expression)
        {
            var res1 = NumOfOperants(expression) > 0;
            var res2 = expression.Contains("(");
            var res3 = ((NumOfOperants(expression) == 1) && (expression.IndexOf('-') == 0));
            return (res1 && !res3) || res2;
        }

        public string GetComponents(string UnitElement, int index)
        {
            try
            {
               
                var index1 = UnitElement.Substring(0, index).LastIndexOfAny(_operators);
                index1 = index1 < index ? index1 + 1 : 0;
                //we must also take the case where exists negative sign - before
                index1 = UnitElement[0] == '-' ? index1 - 1 : index1;
                var index2 = UnitElement.IndexOfAny(_operators, index + 1);
                index2 = index2 > -1 ? index2 : UnitElement.Length;

                var res = UnitElement.Replace(UnitElement.Substring(index1, index2 - index1), Context.Operation(UnitElement[index].ToString(), UnitElement.Substring(index1, index - index1), UnitElement.Substring(index + 1, index2 - index - 1)));
                //return Context.Operation(UnitElement[index].ToString(), UnitElement.Substring(index1, index - index1), UnitElement.Substring(index + 1, index2 - index-1));
                return res;
            }
            catch (Exception ex)
            {
                throw new Exception($"GetComponents Error...UnitElemetn={UnitElement} index={index}...{ex.Message} at {ex.TargetSite}");
            }
        }


        public string CalculatorParser(string input)
        {
            //resebmle mathematical process of evaluating an expression 
            ///1st we remove spaces

            string res = RemoveSpaces(input);
            while (WhileEvaluation(res))
            {
                if (NumOfParenthesis(res) == 10)
                {
                    string r = res;
                }
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
                

                int afterIndex = 0;
                if (res[0] == '+')
                {
                    res = res.Substring(1); //in case it contains positive sign on start;  
                }
                //if (res[0]=='0')
                //{
                //    res = res.Substring(1);
                //}
                //if((res[0]=='-')&&(res[1]=='0'))
                //{
                //    res = res.Remove(1, 1);
                //}
                foreach (var s in res)
                {
                    var resTemp = ReplaceOperator(res);
                    if (res!=resTemp)
                    {
                        res = resTemp;
                        break;
                    }
                    if ((s == '*') || (s == '/'))
                    {
                        int index = afterIndex;// res.IndexOf(s);
                        
                        var r = GetComponents(res, index);
                        res = r;
                        afterIndex = 0;
                        break;
                        //res = res.Substring(0, index - 1) + Context.Operation(s.ToString(), res.Substring(index - 1, 1), res.Substring(index + 1, 1)) + res.Substring(index + 2);
                        //Debug.WriteLine(res);
                    }
                    else if (WhileEvaluation2(s, res, afterIndex))
                    {
                        int index = afterIndex;// res.IndexOf(s);
                        //if (res[index + 1] == '-' || res[index + 1] == '+')
                        //{
                        //    res = res.Replace(res.Substring(index, 2), ReplaceOperator(res.Substring(index, 2)));
                        //}
                        //else if (res[index - 1] == '-' || res[index - 1] == '+')
                        //{
                        //    res = res.Replace(res.Substring(index-1, 2), ReplaceOperator(res.Substring(index-1, 2)));
                        //}
                        var r = GetComponents(res, index);
                        res = r;
                        afterIndex = 0;
                        break;
                        // res = res.Substring(0, index - 1) + Context.Operation(s.ToString(), res.Substring(index - 1, 1), res.Substring(index + 1, 1)) + res.Substring(index + 2);
                        // Debug.WriteLine(res);
                    }
                    afterIndex++;
                }
            }
            return res;
        }

        private bool WhileEvaluation2(char s, string expression, int index)
        {
            var res1 = (s == '-') && (index != 0); //it contains substraction as an operation not as a negative int
            var res2 = (s == '+') && (index != 0); //it contains addition but it is not a positive sign
            var res3 = (!expression.Contains("*")) && (!expression.Contains("/")); //it does not contain multiplication or division
            return (res1 || res2) && (res3);
        }


        public string ReplaceOperator(string expression)
        {
            var res = expression;
            res = res.Replace("--", "+");
            res = res.Replace("-+", "-");
            res = res.Replace("+-", "-");
            res = res.Replace("++", "+");
            return res;
        }

        public string ReplaceOperatorObsolete(string multiOperators)
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
            foreach (var s in input)
            {
                if (_operators.Contains(s))
                {
                    res++;
                }
            }
            return res;
        }

        public int NumOfParenthesis(string input)
        {
            var res = 0;
            foreach (var s in input)
            {
                if (s=='(')
                {
                    res++;
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
