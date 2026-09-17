using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTechnicalTest.Domain.Entities;

public class Country
{
    private Country()
    {
        // Required by EF Core
    }

    public Country(
        int id,
        string countryCode,
        string name,
        string countryIso)
    {
        Id = id;
        CountryCode = countryCode;
        Name = name;
        CountryIso = countryIso;
    }

    public int Id { get; set; }
    public string CountryCode { get; set; }
    public string Name { get; set; }
    public string CountryIso {  get; set; }
    public ICollection<CountryDetail> CountryDetails { get; set; } = new LinkedList<CountryDetail>();
}
