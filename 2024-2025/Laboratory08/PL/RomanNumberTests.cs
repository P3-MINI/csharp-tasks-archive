using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{

[TestClass]
public class RomanNumberTests
    {

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
    [DataRow("Q")][DataRow("1")][DataRow("I1")]
    [DataRow(null)]
    [TestMethod]
    public void RomanNumberFromStringTestThrowsException(string s)
        {
        Assert.ThrowsException<System.ArgumentException>( ()=>new RomanNumber(s) );
        }

    [DataRow("")]
    [DataRow("MMMM")][DataRow("IIII")][DataRow("VV")][DataRow("CCM")][DataRow("VL")][DataRow("I I")]
    [DataRow("Q")][DataRow("1")][DataRow("I1")]
    [DataRow(null)]
    [TestMethod]
    [ExpectedException(typeof(System.ArgumentException),AllowDerivedTypes=false)]
    public void RomanNumberFromStringTestThrowsExceptionOldWay(string s)
        {
        new RomanNumber(s);
        }

    [Ignore]
    [TestMethod]
    public void GetHashCodeTest()
        {
        Assert.Fail();
        }

    [Ignore]
    [TestMethod]
    public void ToStringTest(int i, string s)
        {
        Assert.Fail();
        }

    }

}