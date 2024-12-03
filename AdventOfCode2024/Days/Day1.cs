using AdventOfCode2024.Utils;
using System.Text;

namespace AdventOfCode2024.Days
{
    public static class Day1
    {
        public static string _sampleInput = @"3   4
4   3
2   5
1   3
3   9
3   3";

        public static string Run(string input, int part, bool useSampleData)
        {
            if (part == 1)
            {
                if (useSampleData)
                {
                    return RunPart1(_sampleInput);
                }
                return RunPart1(input);
            }
            else
            {
                if (useSampleData)
                {
                    return RunPart2(_sampleInput);
                }
                return RunPart2(input);
            }
        }

        internal static string RunPart1(string input)
        {
            var lines = FileInputUtils.SplitLinesIntoStringArray(input);

            List<int> firstList = new List<int>();
            List<int> secondList = new List<int>();

            long total = 0;

            foreach(var line in lines)
            {
                if (line.Length == 0)
                {
                    continue;
                }
                var firstNumSB = new StringBuilder();
                var secondNumSB = new StringBuilder();
                    var tracker = 0;
                    while (line[tracker] != ' ')
                    {
                        firstNumSB.Append(line[tracker]);
                        tracker++;
                    }
                    while (line[tracker] == ' ')
                    {
                        tracker++;
                    }
                    while (tracker < line.Length)
                {
                    secondNumSB.Append(line[tracker]);
                    tracker++;
                }
                int firstNum = Int32.Parse(firstNumSB.ToString());
                firstList.Add(firstNum);

                int secondNum = Int32.Parse(secondNumSB.ToString());
                secondList.Add(secondNum);
            }

            firstList.Sort();
            secondList.Sort();
            for(int i = 0; i < firstList.Count; i++)
            {
                total += Math.Abs(secondList[i] - firstList[i]);
            }

            return total.ToString();
        }

        internal static string RunPart2(string input)
        {
            var lines = FileInputUtils.SplitLinesIntoStringArray(input);

            var firstList = new List<int>();
            var secondDict = new Dictionary<int, int>();

            long total = 0;

            foreach (var line in lines)
            {
                if (line.Length == 0)
                {
                    continue;
                }
                var firstNumSB = new StringBuilder();
                var secondNumSB = new StringBuilder();
                var tracker = 0;
                while (line[tracker] != ' ')
                {
                    firstNumSB.Append(line[tracker]);
                    tracker++;
                }
                while (line[tracker] == ' ')
                {
                    tracker++;
                }
                while (tracker < line.Length)
                {
                    secondNumSB.Append(line[tracker]);
                    tracker++;
                }
                int firstNum = Int32.Parse(firstNumSB.ToString());
                firstList.Add(firstNum);

                int secondNum = Int32.Parse(secondNumSB.ToString());
                if (secondDict.TryGetValue(secondNum, out int value))
                {
                    secondDict[secondNum] = ++value;
                }
                else
                {
                    secondDict[secondNum] = 1;
                }
            }

            for (int i = 0; i < firstList.Count; i++)
            {
                var firstNum = firstList[i];
                if (secondDict.TryGetValue(firstNum, out int value))
                {
                    total += firstList[i] * value;
                }
            }

            return total.ToString();
        }
    }
}
