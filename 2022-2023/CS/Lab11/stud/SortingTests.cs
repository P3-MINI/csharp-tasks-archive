using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests
    {

    [TestClass]
    public class ArrayExtensionTests
        {

        private static List<int[]> testCases;
        private static List<int[]> sortedTestCases;

        [ClassInitialize]
        public static void Initialize(TestContext _)
            {
            testCases = new List<int[]>();
            sortedTestCases = new List<int[]>();

            // test 0
            testCases.Add(new int[]{3});
            sortedTestCases.Add(new int[]{3});

            // test 1
            Random rnd = new Random(12345);
            int[] tab = new int[800];
            for ( int i=0 ; i<tab.Length ; ++i )
                tab[i] = rnd.Next(-100,100);
            testCases.Add(tab);
            var tl = tab.ToList<int>();
            tl.Sort();
            sortedTestCases.Add(tl.ToArray());

            }

        [DataRow(0)][DataRow(1)]
        [TestMethod]
        public void InsertionSortTest(int i)
            {
            int[] tab = (int[])testCases[i].Clone();
            tab.InsertionSort();
            CollectionAssert.AreEqual(sortedTestCases[i],tab);
            }

        [DataRow(0)][DataRow(1)]
        [TestMethod]
        public void selectionSortTest(int i)
            {
            int[] tab = (int[])testCases[i].Clone();
            tab.SelectionSort();
            CollectionAssert.AreEqual(sortedTestCases[i],tab);
            }

        }

    }