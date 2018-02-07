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

        [TestMethod]
        public void TestCreateUnitElement()
        {
            ///1st passing test
            string input = "1+11-3";
            //should return 1+2
            SOtests.Solution methodTOtest = new SOtests.Solution();
            var output = methodTOtest.CreateUnitElement(input);
            Assert.IsTrue(output == "9");
        }

        [TestMethod]
        public void TestCreateUnitElement2()
        {
            ///1st passing test
            string input = "8+1+3";
            //should return 1+2
            SOtests.Solution methodTOtest = new SOtests.Solution();
            var output = methodTOtest.CreateUnitElement(input);
            Assert.IsTrue(output == "12");
        }

        [TestMethod]
        public void TestCreateUnitElement3()
        {
            ///1st passing test
            string input = "2+18/2+1*3";
            //should return 1+2
            SOtests.Solution methodTOtest = new SOtests.Solution();
            var output = methodTOtest.CreateUnitElement(input);
            Assert.IsTrue(output == "14");
        }


        [TestMethod]
        public void TestCalculator()
        {
            ///1st passing test
            string input = "2+8/2+1*3";
            //should return 1+2
            SOtests.Solution methodTOtest = new SOtests.Solution();
            var output = methodTOtest.CalculatorParser(input);
            Assert.IsTrue(output == "9");
        }

        [TestMethod]
        public void TestCalculator2()
        {
            ///1st passing test
            string input = "(2+8)/2+1*3";
            //should return 1+2
            SOtests.Solution methodTOtest = new SOtests.Solution();
            var output = methodTOtest.CalculatorParser(input);
            Assert.IsTrue(output == "8");
        }

        [TestMethod]
        public void TestCalculator3()
        {
            ///1st passing test
            string input = "(2+7)/(2+1)*3";
            //should return 1+2
            SOtests.Solution methodTOtest = new SOtests.Solution();
            var output = methodTOtest.CalculatorParser(input);
            Assert.IsTrue(output == "1");
        }
    }
}
