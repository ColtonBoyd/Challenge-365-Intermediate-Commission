using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge_365_Intermediate_Commission
{
    class Program
    {
        static void Main(string[] args)
        {
            string FileLocation = "SalesSheet.txt";
            var RevenueAndCostListString = File.ReadAllLines(FileLocation);
            var Names = RevenueAndCostListString[2].Split(' ').Where(i => i.Length > 0).ToList();
            var CutDownRevenueAndCostListString = RevenueAndCostListString.Skip(1).Where(Line => !string.IsNullOrWhiteSpace(Line)).Where(Line => !Line.Substring(0, 2).Contains(" ")).Select(F => F);
            List<double> TotalsList = GetRevenueAndCost(CutDownRevenueAndCostListString,Names.Count());
            GetCommission(TotalsList , Names);
            Console.ReadKey();
        }

        private static void GetCommission(List<double> totalsList, List<string> Names)
        {
            for (int i = 0; i < totalsList.Count(); i++)
            {
                Console.WriteLine("{0} made ${1} in commission this month.",Names[i],(totalsList[i]*0.062));
            }
        }

        private static List<double> GetRevenueAndCost(IEnumerable<string> revenueAndCostListString, int PersonCount)
        {
            int split = 0;
            var NumberList = revenueAndCostListString.ToList();
            List<double> CostList = new List<double>();
            List<double> RevenueList = new List<double>();
            List<double> TotalsList= new List<double>();

            for (int i = 0; i < NumberList.Count(); i++)
            {
                if (NumberList[i].Equals("Expenses"))
                {
                    split = i;
                    break;
                }

            }

            for (int i = 0; i < split; i++)
            {
                var LineArray = NumberList[i].Substring(NumberList[i].IndexOf(' ') + 1).Split(' ').Where(Line => !string.IsNullOrEmpty(Line)).ToList();
                for (int o = 0; o < LineArray.Count(); o++)
                {

                        Double.TryParse(LineArray[o], out double number);
                        RevenueList.Add(number);

                }

            }
            for (int i = split+1; i < NumberList.Count(); i++)
            {
                var LineArray = NumberList[i].Substring(NumberList[i].IndexOf(' ') + 1).Split(' ').Where(Line => !string.IsNullOrEmpty(Line)).ToList();
                for (int o = 0; o < LineArray.Count(); o++)
                {
                        Double.TryParse(LineArray[o], out double number);
                        CostList.Add(number);
                }

            }

            for (int i = 0; i < PersonCount; i++)
            {
                TotalsList.Add(-1);
            }

            for (int i = 0; i < RevenueList.Count(); i++)
            {
                if(RevenueList[i]>CostList[i])
                {
                    TotalsList[i%TotalsList.Count()]+=(RevenueList[i]-CostList[i]);
                }

            }

            return TotalsList;

        }

    }
}
