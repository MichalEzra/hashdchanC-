using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IHungarianAlgorithm
    {
        // ממשק לאלגוריתם ההונגרי
        Task InitializeCandidatesAsync();
        public int[,] CostMatrix { get; set; }
        public int[,] CostMatrixMale { get; set; }
        public int[,] CostMatrixFemale { get; set; }
        public void MatrixFilling(int[,] costMatrix);
        public (Candidate[,], int[]) RunHungarianAlgorithm(int[,] costMatrix);


    }
}
