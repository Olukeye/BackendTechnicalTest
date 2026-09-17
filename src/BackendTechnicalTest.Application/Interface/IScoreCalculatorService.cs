using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTechnicalTest.Application.Interface;

public interface IScoreCalculatorService
{
    int CalculateScore(IEnumerable<int> numbers);
}
