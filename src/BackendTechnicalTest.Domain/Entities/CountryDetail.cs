using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTechnicalTest.Domain.Entities;

public class CountryDetail
{
    private CountryDetail()
    {
       
    }

    public CountryDetail(
        int id,
        int countryId,
        string operatorName,
        string operatorCode)
    {
        Id = id;
        CountryId = countryId;
        Operator = operatorName;
        OperatorCode = operatorCode;
    }

    public int Id { get; set; }
    public int CountryId { get; set; }
    public string Operator { get; set; }
    public string OperatorCode { get; set; }
    public Country? Country { get; set; }
}
