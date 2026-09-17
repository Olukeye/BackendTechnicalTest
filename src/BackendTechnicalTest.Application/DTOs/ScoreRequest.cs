using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BackendTechnicalTest.Application.DTOs;

public class ScoreRequest
{
    public int[] Numbers { get; set; } = [];
}
