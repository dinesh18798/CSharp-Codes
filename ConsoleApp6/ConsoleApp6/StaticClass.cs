using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp6
{
    public static class StaticClass
    {
        readonly int i = 0;
        public static int MyProperty { get; set; }

        public static void TestStaticMethod()
        {

        }
    }

    public class NonStaticClass
    {
        static int i = 0;
        public static int MyProperty { get; set; }

        public static void TestStaticMethod()
        {

        }

        public void TestNonStaticMethod()
        {

        }
    }
}
