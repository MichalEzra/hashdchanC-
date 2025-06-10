using Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfasces
{
    public interface ICandidatesDetails<T>
    {
        // ממשק עבור מועמדים
        Task<T[]> GetFemaleCandidatesAsync();
        Task<T[]> GetMaleCandidatesAsync();
    }
}
