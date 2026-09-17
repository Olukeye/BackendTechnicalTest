using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTechnicalTest.Application.Common.Results;

public sealed record ResultError(
    string Code,
    string Message);
