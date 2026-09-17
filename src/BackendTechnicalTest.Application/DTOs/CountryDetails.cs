using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTechnicalTest.Application.DTOs;

public sealed record CountryDetails(
    string Operator,
    string OperatorCode
    );
