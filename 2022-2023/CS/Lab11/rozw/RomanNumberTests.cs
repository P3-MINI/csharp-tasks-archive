using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{

[TestClass]
public class RomanNumberTests
    {

    [DataRow(1)]
    [DataRow(3999)]
    [TestMethod]
    public void RomanNumberFromIntegerTestCorrect(int i)
        {
        RomanNumber r = new RomanNumber(i);
        int v = (int)r;
        Assert.AreEqual(v,i);
        }

    [DataRow(4000)]
    [DataRow(0)]
    [DataRow(-1)]
    [TestMethod]
    public void RomanNumberFromIntegerTestThrowsException(int i)
        {
        Assert.ThrowsException<System.OverflowException>( ()=>new RomanNumber(i) );
        }

    [DataRow(4000)]
    [DataRow(0)]
    [DataRow(-1)]
    [TestMethod]
    [ExpectedException(typeof(System.OverflowException),AllowDerivedTypes=false)]
    public void RomanNumberFromIntegerTestThrowsExceptionOldWay(int i)
        {
        new RomanNumber(i);
        }

    [DataRow(1,"I")][DataRow(2,"II")][DataRow(3,"III")][DataRow(4,"IV")][DataRow(5,"V")][DataRow(6,"VI")][DataRow(7,"VII")][DataRow(8,"VIII")][DataRow(9,"IX")]
    [DataRow(10,"X")][DataRow(20,"XX")][DataRow(30,"XXX")][DataRow(40,"XL")][DataRow(50,"L")][DataRow(60,"LX")][DataRow(70,"LXX")][DataRow(80,"LXXX")][DataRow(90,"XC")]
    [DataRow(100,"C")][DataRow(200,"CC")][DataRow(300,"CCC")][DataRow(400,"CD")][DataRow(500,"D")][DataRow(600,"DC")][DataRow(700,"DCC")][DataRow(800,"DCCC")][DataRow(900,"CM")]
    [DataRow(1000,"M")][DataRow(2000,"MM")][DataRow(3000,"MMM")]
    [DataRow(3999,"MMMCMXCIX")][DataRow(1111,"MCXI")][DataRow(2345,"MMCCCXLV")][DataRow(3888,"MMMDCCCLXXXVIII")][DataRow(3333,"MMMCCCXXXIII")]
    [TestMethod]
    public void RomanNumberTestFromStringCorrect(int i, string s)
        {
        RomanNumber r = new RomanNumber(s);
        int v = (int)r;
        Assert.AreEqual(v,i);
        }

    [DataRow("")]
    [DataRow("MMMM")][DataRow("IIII")][DataRow("VV")][DataRow("CCM")][DataRow("VL")][DataRow("I I")]
    [DataRow("A")][DataRow("1")][DataRow("I1")]
    [DataRow(null)]
    [TestMethod]
    public void RomanNumberFromStringTestThrowsException(string s)
        {
        Assert.ThrowsException<System.ArgumentException>( ()=>new RomanNumber(s) );
        }

    [DataRow("")]
    [DataRow("MMMM")][DataRow("IIII")][DataRow("VV")][DataRow("CCM")][DataRow("VL")][DataRow("I I")]
    [DataRow("A")][DataRow("1")][DataRow("I1")]
    [DataRow(null)]
    [TestMethod]
    [ExpectedException(typeof(System.ArgumentException),AllowDerivedTypes=false)]
    public void RomanNumberFromStringTestThrowsExceptionOldWay(string s)
        {
        new RomanNumber(s);
        }

    [Ignore]
    [TestMethod]
    public void EqualsTest()
        {
        Assert.Fail();
        }

    [Ignore]
    [TestMethod]
    public void GetHashCodeTest()
        {
        Assert.Fail();
        }

    [DataRow(1,"I")][DataRow(2,"II")][DataRow(3,"III")][DataRow(4,"IV")][DataRow(5,"V")][DataRow(6,"VI")][DataRow(7,"VII")][DataRow(8,"VIII")][DataRow(9,"IX")]
    [DataRow(10,"X")][DataRow(20,"XX")][DataRow(30,"XXX")][DataRow(40,"XL")][DataRow(50,"L")][DataRow(60,"LX")][DataRow(70,"LXX")][DataRow(80,"LXXX")][DataRow(90,"XC")]
    [DataRow(100,"C")][DataRow(200,"CC")][DataRow(300,"CCC")][DataRow(400,"CD")][DataRow(500,"D")][DataRow(600,"DC")][DataRow(700,"DCC")][DataRow(800,"DCCC")][DataRow(900,"CM")]
    [DataRow(1000,"M")][DataRow(2000,"MM")][DataRow(3000,"MMM")]
    [DataRow(3999,"MMMCMXCIX")][DataRow(1111,"MCXI")][DataRow(2345,"MMCCCXLV")][DataRow(3888,"MMMDCCCLXXXVIII")][DataRow(3333,"MMMCCCXXXIII")]
    [TestMethod]
    public void ToStringTest(int i, string s)
        {
        RomanNumber rn = new RomanNumber(i);
        string rns =  rn.ToString();
        Assert.AreEqual(rns,s);
        }

    [TestMethod]
    public void AdditionTestCorrect()
        {
        RomanNumber a = new RomanNumber(234);
        RomanNumber b = new RomanNumber(345);
        RomanNumber e = new RomanNumber(234+345);
        RomanNumber r;
        r = a + b;
        Assert.AreEqual(e,r);
        }

    [TestMethod]
    public void AdditionTestThrowsException()
        {
        RomanNumber a = new RomanNumber(2345);
        RomanNumber b = new RomanNumber(3456);
        Assert.ThrowsException<System.OverflowException>( () => a + b );
        }

    [TestMethod]
    public void DivisionTestCorrect()
        {
        RomanNumber a = new RomanNumber(468);
        RomanNumber b = new RomanNumber(234);
        RomanNumber e = new RomanNumber(2);
        RomanNumber r;
        r = a / b;
        Assert.AreEqual(e,r);
        }

    [TestMethod]
    public void DivisionTestThrowsException()
        {
        RomanNumber a = new RomanNumber(3);
        RomanNumber b = new RomanNumber(2);
        Assert.ThrowsException<System.ArithmeticException>( () => a / b );
        }

    }

}