using FunctionAppDoc2Data.DataContext;
using FunctionAppDoc2Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionAppDoc2Data.Mappers;
public static class UnitOfMeasuresMapper
{
    public static List<UnitOfMeasuresDTO> MapToUnitOfMeasures(this List<UnitOfMeasure> unitOfMeasures)
    {
        if (unitOfMeasures == null)
            throw new ArgumentNullException(nameof(unitOfMeasures));

        var unitOfMeasureDTOs = unitOfMeasures.Select(m => new UnitOfMeasuresDTO()
        {
            IsActive = m.IsActive,
            Name = m.Name,
            UnitOfMeasureId = m.UnitOfMeasureId
        }).ToList();

        return unitOfMeasureDTOs;
    }
}
