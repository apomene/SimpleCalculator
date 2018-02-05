using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SOtests
{
    public abstract class BasicOperators<T>
    {
        public abstract T Operation(T a, T b);    
    }


    #region Basic Operations Implementation
    public class  Addition:BasicOperators<string>
    {
        ///a and b are evaluated to valid int
       public override string Operation(string a, string b)
        {
            ///a and b are evaluated to valid int
            return (int.Parse(a) + int.Parse(b)).ToString();
        }
    }

    public class Substraction:BasicOperators<string>
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


    #region Context of Use in Calculator Implementation

    public class Context
    {
        private static Dictionary<string, BasicOperators<string>> _operations = new Dictionary<string, SOtests.BasicOperators<string>>();

        static Context()
        {
            _operations.Add("+", new Addition());
            _operations.Add("-", new Substraction());
            _operations.Add("*", new Multiplication());
            _operations.Add("/", new Division());
        }

        static string Operation(string operationSymbol,string inputA,string inputB)
        {
            return _operations[operationSymbol].Operation(inputA, inputB);
        }
    }

    #endregion



}
