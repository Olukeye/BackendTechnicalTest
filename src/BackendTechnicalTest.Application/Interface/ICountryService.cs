using BackendTechnicalTest.Application.Common.Results;
using BackendTechnicalTest.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTechnicalTest.Application.Interface;

public interface ICountryService
{
    Task<Result<PhoneCountryLookupResponse?>> GetCountryByPhoneNumber(string phoneNumber, CancellationToken cancellationToken = default);
}
