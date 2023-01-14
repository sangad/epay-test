using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(String[] args)
        {
            int[] euroDenominations = new int[] { 100, 50, 10 };
            int euro = 100, sum = 0;
            string strSum = "";
            CalcDenomination(euroDenominations, euro, euro, ref sum, ref strSum);
            Console.ReadLine();
        }

        public static void CalcDenomination(int[] euroDenominations, int orgEuro,int remaingEuro, ref int sum, ref string strSum)
        {
            for (int i = 0; i < 3; i++)
            {
                if (remaingEuro >= euroDenominations[i])
                {
                    int count = remaingEuro / euroDenominations[i];
                    sum += count * euroDenominations[i];
                    if (sum.Equals(orgEuro))
                    {
                        strSum += "+"+count + "x" + euroDenominations[i]+ " EURO";
                        Console.WriteLine(strSum.TrimStart('+'));
                        strSum = "";
                        sum = 0;
                    }
                    else
                    {
                        strSum += "+" + count + "x" + euroDenominations[i] + " EURO";
                        CalcDenomination(euroDenominations, orgEuro, remaingEuro % euroDenominations[i], ref sum, ref strSum);
                    }
                }
            }
        }
    }
}
