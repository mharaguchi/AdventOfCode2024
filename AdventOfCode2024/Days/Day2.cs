using AdventOfCode2024.Utils;
using System.Runtime.CompilerServices;
using System.Text;

namespace AdventOfCode2024.Days
{
    public static class Day2
    {
        public static string _sampleInput = @"7 6 4 2 1
1 2 7 8 9
9 7 6 2 1
1 3 2 4 5
8 6 4 4 1
1 3 6 7 9";

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
            int @unsafe = 0;

            foreach(var line in lines)
            {
                var ascending = true;
                var isAscendingSet = false;
                var levels = FileInputUtils.SplitLineIntoIntList(line, " ");
                for(int i = 1; i < levels.Count; i++)
                {
                    var firstNum = levels[i - 1];
                    var secondNum = levels[i];
                    if (firstNum == secondNum)
                    {
                        @unsafe++;
                        break;
                    }
                    if (!isAscendingSet)
                    {
                        if (firstNum > secondNum)
                        {
                            ascending = false;
                        }
                        isAscendingSet = true;
                    }
                    if ((firstNum > secondNum && ascending) || (firstNum < secondNum && !ascending))
                    {
                        @unsafe++;
                        break;
                    }
                    if (Math.Abs(firstNum - secondNum) > 3)
                    {
                        @unsafe++;
                        break;
                    }
                }
                isAscendingSet = false;
            }
            var total = lines.Length;
            var safe = total - @unsafe;

            return safe.ToString();
        }

        internal static bool IsSafe(List<int> levels)
        {
            var ascending = true;
            var isAscendingSet = false;
            for (int i = 1; i < levels.Count; i++)
            {
                var firstNum = levels[i - 1];
                var secondNum = levels[i];
                if (firstNum == secondNum)
                {
                    return false;
                }
                if (!isAscendingSet)
                {
                    if (firstNum > secondNum)
                    {
                        ascending = false;
                    }
                    isAscendingSet = true;
                }
                if ((firstNum > secondNum && ascending) || (firstNum < secondNum && !ascending))
                {
                    return false;
                }
                if (Math.Abs(firstNum - secondNum) > 3)
                {
                    return false;
                }
            }

            return true;
        }

        internal static List<int> RemoveBadLevel(List<int> levels, int badLevel)
        {
            var levelsCopy = new List<int>(levels);
            levelsCopy.RemoveAt(badLevel);

            return levelsCopy;
        }

        internal static string RunPart2(string input)
        {
            var lines = FileInputUtils.SplitLinesIntoStringArray(input);
            int @unsafe = 0;

            foreach (var line in lines)
            {
                var ascending = true;
                var isAscendingSet = false;
                var levels = FileInputUtils.SplitLineIntoIntList(line, " ");

                var firstTracker = 0;
                var secondTracker = 1;

                while (secondTracker < levels.Count) { 
                    var firstNum = levels[firstTracker];
                    var secondNum = levels[secondTracker];
                    if (firstNum == secondNum)
                    {
                        var removeFirst = RemoveBadLevel(levels, firstTracker);
                        if (!IsSafe(removeFirst))
                        {
                            Console.WriteLine(line + ": Unsafe");
                            @unsafe++;
                            break;
                        }
                        break;
                    }
                    if (!isAscendingSet)
                    {
                        if (firstNum > secondNum)
                        {
                            ascending = false;
                        }
                        isAscendingSet = true;
                    }
                    if ((firstNum > secondNum && ascending) || (firstNum < secondNum && !ascending))
                    {
                        var broken = false;
                        for (int i = 0; i <= secondTracker; i++)
                        {
                            if (IsSafe(RemoveBadLevel(levels, i))){
                                broken = true;
                                break;
                            }
                        }
                        if (broken)
                        {
                            break;
                        }
                        Console.WriteLine(line + ": Unsafe");
                        @unsafe++;
                        break;
                    }
                    if (Math.Abs(firstNum - secondNum) > 3)
                    {
                        var broken = false;
                        for (int i = 0; i <= secondTracker; i++)
                        {
                            if (IsSafe(RemoveBadLevel(levels, i)))
                            {
                                broken = true;
                                break;
                            }
                        }
                        if (broken)
                        {
                            break;
                        }
                    }
                    firstTracker++;
                    secondTracker++;
                }
            }
            var total = lines.Length;
            var safe = total - @unsafe;

            return safe.ToString();
        }
    }
}
