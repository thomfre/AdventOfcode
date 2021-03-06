﻿using System;
using System.Collections.Generic;
using System.Linq;
using Thomfre.AdventOfCode2018.Tools;

namespace Thomfre.AdventOfCode2018.Solvers
{
    internal class Day2Solver : SolverBase
    {
        public Day2Solver(IInputLoader inputLoader) : base(inputLoader)
        {
        }

        public override int DayNumber => 2;

        public override string Solve(ProblemPart part)
        {
            StartExecutionTimer();
            string input = GetInput();
            string[] boxIds = input.Split('\n');

            switch (part)
            {
                case ProblemPart.Part1:
                    int containsTwo = 0;
                    int containsThree = 0;

                    foreach (string boxId in boxIds)
                    {
                        var charCounts = boxId.ToCharArray().GroupBy(c => c).Select(c => new {Char = c.Key, Count = c.Count()}).ToArray();
                        if (charCounts.Any(c => c.Count == 2))
                        {
                            containsTwo++;
                        }

                        if (charCounts.Any(c => c.Count == 3))
                        {
                            containsThree++;
                        }
                    }

                    int checksum = containsTwo * containsThree;

                    AnswerSolution1 = checksum;

                    StopExecutionTimer();

                    return FormatSolution($"The checksum for the box IDs are [{ConsoleColor.Green}!{checksum}]");
                case ProblemPart.Part2:

                    Dictionary<string, char[]> boxIdDictionary = boxIds.ToDictionary(b => b.Trim(), c => c.Trim().ToCharArray());

                    int similaritiesNeeded = boxIdDictionary.First().Value.Length - 1;
                    foreach (KeyValuePair<string, char[]> boxId in boxIdDictionary)
                    {
                        foreach (KeyValuePair<string, char[]> boxIdOther in boxIdDictionary)
                        {
                            int matching = 0;
                            string matchingChars = string.Empty;
                            for (int i = 0; i < boxId.Value.Length; i++)
                            {
                                if (boxId.Value[i] != boxIdOther.Value[i])
                                {
                                    continue;
                                }

                                matching++;
                                matchingChars += boxId.Value[i];
                            }

                            if (matching != similaritiesNeeded)
                            {
                                continue;
                            }

                            AnswerSolution2 = matchingChars;

                            StopExecutionTimer();

                            return
                                FormatSolution($"The common letters between [{ConsoleColor.Yellow}!{boxId.Key}] and [{ConsoleColor.Yellow}!{boxIdOther.Key}] are [{ConsoleColor.Green}!{matchingChars}]");
                        }
                    }

                    StopExecutionTimer();
                    return "Unable to find solution";
                default:
                    throw new ArgumentOutOfRangeException(nameof(part), part, null);
            }
        }
    }
}
