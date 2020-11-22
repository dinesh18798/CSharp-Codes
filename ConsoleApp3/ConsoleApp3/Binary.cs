using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp3
{
    class Binary
    {
        public string AddBinary(string a, string b)
        {
            string result = "";

            int sum = 0;
            int positionA = a.Length - 1;
            int positionB = b.Length - 1;

            while ((positionA >= 0 && positionB >= 0) || sum == 1)
            {
                sum += positionA >= 0 ? a[positionA--] - '0' : 0;
                sum += positionB >= 0 ? b[positionB--] - '0' : 0;

                result = (char)(sum % 2 + '0') + result;

                sum /= 2;
            }

            return result;
        }

    }
}
