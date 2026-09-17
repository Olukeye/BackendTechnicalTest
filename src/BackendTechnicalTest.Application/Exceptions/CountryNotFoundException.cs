using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTechnicalTest.Application.Exceptions;

public class CountryNotFoundException : Exception
{
    public CountryNotFoundException(string phoneNumber)
      : base($"No country dialing code could be matched for phone number '{phoneNumber}'.")
    {
    }
}
