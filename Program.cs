using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    static class Program
    {
        static void Main(String[] args)
        {  //input
            List<int> euroDenomination = new List<int>() { 10, 50, 100 };
            int amount = 980;

            Dictionary<int, int> comboDenomination = new Dictionary<int, int>();
            foreach (int ed in euroDenomination)
                comboDenomination.Add(ed, 0);

            CalcDenomination(amount, euroDenomination, comboDenomination);
        }

        public static void CalcDenomination(int money, List<int> euroDenomination, Dictionary<int, int> comboDenomination)
        {
            if (money < 0 || euroDenomination.Count == 0) return;
            if (money == 0)
            {
                string comboDenoStr = string.Empty;
                foreach (KeyValuePair<int, int> keyValues in comboDenomination)
                    if (keyValues.Value != 0)
                        comboDenoStr = comboDenoStr + (string.IsNullOrEmpty(comboDenoStr) ? "" : "+ ") + keyValues.Key + " X " + keyValues.Value + " EUR ";
                Console.WriteLine(comboDenoStr);
                return;
            }

            List<int> copy = new List<int>(euroDenomination);
            copy.RemoveAt(0);
            CalcDenomination(money, copy, comboDenomination);
            comboDenomination = CreateNewOrUpdateExisting(new Dictionary<int, int>(comboDenomination), euroDenomination[0], 1);
            CalcDenomination(money - euroDenomination[0], euroDenomination, comboDenomination);
        }


        public static Dictionary<int, int> CreateNewOrUpdateExisting(Dictionary<int, int> map, int key, int value)
        {
            if (map.ContainsKey(key))
                map[key] = value + map[key];
            else
                map.Add(key, value);
            return map;
        }
    }
}

