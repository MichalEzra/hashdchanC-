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
        // מטריצות עלות (CostMatrix) שמשמשות לחישוב ההתאמות בין מועמדים:
        public int[,] CostMatrix { get; set; }
        public int[,] CostMatrixMale { get; set; }
        public int[,] CostMatrixFemale { get; set; }

        // פונקציה למילוי מטריצת העלות עם ערכים המחושבים לפי ניקוד ההתאמה בין מועמדים
        public void MatrixFilling(int[,] costMatrix);

        // פונקציה שמריצה את האלגוריתם ההונגרי על מטריצת העלות ומחזירה:
        // - מערך דו-ממדי של התאמות (זכר-נקבה)
        // - מערך של ניקודי ההתאמה (costMatch)
        public (Candidate[,], int[]) RunHungarianAlgorithm(int[,] costMatrix);
    }
}
