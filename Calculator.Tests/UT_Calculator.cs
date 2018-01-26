using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Calculator.Tests
{
    [TestClass]
    public class UT_Calculator
    {
        [TestMethod]
        public void testInnerElementMethod1()
        {
            ///1st passing test
            string input = "(1+2)";
            //should return 1+2
            SOtests.Solution methodTOtest = new SOtests.Solution();
            var output = methodTOtest.InnerElement(input);
            Assert.IsTrue(output == "1+2");
        }


        [TestMethod]
        public void testInnerElementMethod2()
        {
            ///1st passing test
            string input = "((1+2)+3)/5+(5+6)";
            //should return 1+2
            SOtests.Solution methodTOtest = new SOtests.Solution();
            var output = methodTOtest.InnerElement(input);
            Assert.IsTrue(output == "5+6");
        }
    }
}
