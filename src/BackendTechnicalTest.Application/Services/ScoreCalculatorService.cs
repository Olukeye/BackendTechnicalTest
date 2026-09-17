using BackendTechnicalTest.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTechnicalTest.Application.Services;

public sealed class ScoreCalculatorService : IScoreCalculatorService
{
    private const int EvenPoints = 1;
    private const int OddPoints = 3;
    private const int EightBonusPoints = 5;
    private const int EightValue = 8;

    public int CalculateScore(IEnumerable<int> numbers)
    {
        ArgumentNullException.ThrowIfNull(numbers);

        var score = 0;

        foreach(var number in numbers)
        {
            score += number % 2 == 0 ? EvenPoints : OddPoints;


            if (number == EightValue)
            {
                score += EightBonusPoints;
            }
        }

        return score;
    }
}