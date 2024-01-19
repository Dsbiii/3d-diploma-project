using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public static class Utilites
    {
        public static T[] Shuffle<T>(this T[] array)
        {
            for (int i = array.Length - 1; i >= 1; i--)
            {
                Random random = new Random();
                int j = random.Next(i + 1);
                var temp = array[j];
                array[j] = array[i];
                array[i] = temp;
            }
            return array;
        }

    }
}
