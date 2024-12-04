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

        public static string _sampleInputEdgeCases = @"48 46 47 49 51 54 56
1 1 2 3 4 5
1 2 3 4 5 5
5 1 2 3 4 5
1 4 3 2 1
1 6 7 8 9
1 2 3 4 3
9 8 7 6 7
7 10 8 10 11
29 28 27 25 26 25 22 20
7 10 8 10 11
90 89 86 84 83 79
97 96 93 91 85
29 26 24 25 21
36 37 40 43 47
43 44 47 48 49 54
35 33 31 29 27 25 22 18
77 76 73 70 64
68 65 69 72 74 77 80 83
37 40 42 43 44 47 51
70 73 76 79 86
1 2 3 4 5 5";

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
                    return RunPart2(_sampleInputEdgeCases);
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
                var levels = FileInputUtils.SplitLineIntoIntList(line, " ");

                if (!IsSafe(levels))
                {
                    @unsafe++;
                    continue;
                }
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
            int safe = 0;

            foreach (var line in lines)
            {
                var ascending = true;
                var isAscendingSet = false;
                var levels = FileInputUtils.SplitLineIntoIntList(line, " ");

                if (IsSafe(levels))
                {
                    safe++;
                    continue;
                }

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
                            @unsafe++;
                            break;
                        }
                        safe++;
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
                        var isSafe = false;
                        for (int i = 0; i <= secondTracker; i++)
                        {
                            if (IsSafe(RemoveBadLevel(levels, i))){
                                isSafe = true;
                                break;
                            }
                        }
                        if (isSafe)
                        {
                            safe++;
                            break;
                        }
                        else
                        {
                            @unsafe++;
                            break;
                        }
                    }
                    if (Math.Abs(firstNum - secondNum) > 3)
                    {
                        var isSafe = false;
                        for (int i = 0; i <= secondTracker; i++)
                        {
                            if (IsSafe(RemoveBadLevel(levels, i)))
                            {
                                isSafe = true;
                                break;
                            }
                        }
                        if (isSafe)
                        {
                            safe++;
                            break;
                        }
                        else
                        {
                            @unsafe++;
                            break;
                        }
                    }
                    firstTracker++;
                    secondTracker++;
                }
                if (secondTracker == levels.Count)
                {
                    safe++;
                }
            }
            var total = lines.Length;
            Console.WriteLine("safeTotal: " + safe);
            Console.WriteLine("unsafe: " + @unsafe);
            Console.WriteLine("safe plus unsafe: " + (safe + @unsafe));
            Console.WriteLine("total by lines: " + total);
            var minus = total - @unsafe;

            return minus.ToString();
        }
    }
}
