using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventCode2020
{
    public class Day25
    {
        readonly int doorPublicKey = 0;
        readonly int cardPublicKey = 0;

        public Day25()
        {
            string[] publicKeys = File.ReadAllLines(@"E:\GitHub\CSharp-Codes\AdventCode2020\keys.txt");

            cardPublicKey = int.Parse(publicKeys[0]);
            doorPublicKey = int.Parse(publicKeys[1]);

            int cardLoopSize = GetLoopSize(cardPublicKey);
            int doorLoopSize = GetLoopSize(doorPublicKey);

            long cardEncryptionKey = GetEncryptionKey(cardLoopSize, doorPublicKey);
            long doorEncryptionKey = GetEncryptionKey(doorLoopSize, cardPublicKey);
            if (cardEncryptionKey == doorEncryptionKey)
            {
                Console.WriteLine($"Part 1: {cardEncryptionKey}");
            }
            else
            {
                Console.WriteLine($"No Encryption Key in Part 1");
            }
        }

        private long GetEncryptionKey(int loopSize, int subjectNumber)
        {
            long encryptionKey = 1;
            for (int i = 0; i < loopSize; i++)
            {
                encryptionKey = (encryptionKey * subjectNumber) % 20201227;
            }
            return encryptionKey;
        }

        private int GetLoopSize(int publicKey)
        {
            int loop = 1;
            int resultKey = 1;

            while (true)
            {
                resultKey = (resultKey * 7) % 20201227;
                if (resultKey == publicKey)
                    break;
                ++loop;
            }
            return loop;
        }
    }
}
