using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BarricaGames.Permutations
{
    public static class Permutations
    {
        public static int Factorial(int number)
        {
            int factorial = 1;
            for (int i = 1; i <= number; i++)
            {
                factorial *= i;
            }

            return factorial;
        }

        public static int FromChoose(int from, int choose, bool isOrderImportant)
        {
            if (isOrderImportant)
            {
                return Factorial(from) / Factorial(from - choose);
            }

            return Factorial(from) / (Factorial(from - choose) * Factorial(choose));
        }

        public static int EncodePermutation(int[] indicesToEncode, int from, bool isOrderImportant)
        {
            int code = 0;

            List<int> availableIndices = new List<int>();

            for (int i = 0; i < from; i++)
            {
                int x = i;
                availableIndices.Add(x);
            }

            for (int i = 0; i < indicesToEncode.Length; i++)
            {
                int indexOfValue = availableIndices.IndexOf(indicesToEncode[i]);
                int value = FromChoose(from - 1 - i, indicesToEncode.Length - 1 - i, isOrderImportant) 
                    * indexOfValue;

                availableIndices.RemoveAt(indexOfValue);
                code += value;
            }

            return code;
        }

        public static int[] DecodePermutationsIsOrderImportant(int code, int from, int choose)
        {
            int[] indexs = new int[choose];

            List<int> availableIndices = new List<int>();

            for (int i = 0; i < from; i++)
            {
                int x = i;
                availableIndices.Add(x);
            }

            for (int i = 0; i < choose; i++)
            {
                int index = Mathf.FloorToInt(code / FromChoose(from - (i+1), choose -(i+1), true)) % availableIndices.Count;
                indexs[i] = availableIndices[index];
                availableIndices.RemoveAt(index);
            }

            return indexs;
        }

        public static List<int> DecodePermutationsIsOrderImportantAsList(int code, int from, int choose)
        {
            return DecodePermutationsIsOrderImportant(code, from, choose).ToList();
        }
    }
}
