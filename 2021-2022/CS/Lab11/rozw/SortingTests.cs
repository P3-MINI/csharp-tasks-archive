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

            // test 2
            testCases.Add(new int[0]);
            sortedTestCases.Add(new int[0]);

            // test 3
            testCases.Add(new int[]{-1});
            sortedTestCases.Add(new int[]{-1});

            // test 4
            testCases.Add(new int[]{3,-2});
            sortedTestCases.Add(new int[]{-2,3});

            // test 5
            testCases.Add(new int[]{5,5,5,5,5,5,5,5,5,5});
            sortedTestCases.Add(new int[]{5,5,5,5,5,5,5,5,5,5});

            // test 6
            tab = new int[1000];
            for ( int i=0 ; i<tab.Length ; ++i )
                tab[i] = i-500;
            testCases.Add(tab);
            sortedTestCases.Add((int[])tab.Clone());

            // test 7
            tab = new int[2000];
            for ( int i=0 ; i<tab.Length ; ++i )
                tab[i] = 1000-i;
            testCases.Add(tab);
            tl = tab.ToList<int>();
            tl.Sort();
            sortedTestCases.Add(tl.ToArray());

            }

        [DataRow(0)][DataRow(1)][DataRow(2)][DataRow(3)][DataRow(4)][DataRow(5)][DataRow(6)][DataRow(7)]
        [TestMethod]
        public void InsertionSortTest(int i)
            {
            int[] tab = (int[])testCases[i].Clone();
            tab.InsertionSort();
            CollectionAssert.AreEqual(sortedTestCases[i],tab);
            }

        [DataRow(0)][DataRow(1)][DataRow(2)][DataRow(3)][DataRow(4)][DataRow(5)][DataRow(6)]
        [TestMethod]
        public void selectionSortTest(int i)
            {
            int[] tab = (int[])testCases[i].Clone();
            tab.SelectionSort();
            CollectionAssert.AreEqual(sortedTestCases[i],tab);
            }

        }

    }