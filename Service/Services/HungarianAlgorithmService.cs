//using Repository.Entities;
//using Repository.Entities.Enums;
//using Service.Interfaces;
////using Common.Dto;
//using AutoMapper;
//using HungarianAlgorithm;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Service.Interfasces;



//public class HungarianAlgorithmService : IHungarianAlgorithm
//{
//    private readonly IMyDetails<Candidate> _candidateService;
//    private readonly IService<MatchDto> _matchService;
//    private readonly IMapper _mapper;

//    private Candidate[] femaleCandidates;
//    private Candidate[] maleCandidates;
//    private int maleCount, femaleCount;

//    public int[,] CostMatrix { get; set; }
//    public int[,] CostMatrixMale { get; set; }
//    public int[,] CostMatrixFemale { get; set; }

//    private int[] assignments;
//    private static Random _random ;

//    public HungarianAlgorithmService(IMyDetails<Candidate> candidateService, IService<MatchDto> matchService, IMapper mapper)
//    {
//        _candidateService = candidateService;
//        _matchService = matchService;
//        _mapper = mapper;
//        _random = new Random();
//    }

//    public async Task InitializeCandidatesAsync()
//    {
//        femaleCandidates = (await _candidateService.GetFemaleCandidatesAsync());
//        maleCandidates = (await _candidateService.GetMaleCandidatesAsync());
//        Console.WriteLine($"מספר מועמדות: {femaleCandidates.Length}");
//        Console.WriteLine($"מספר מועמדים: {maleCandidates.Length}");
//        femaleCount = femaleCandidates.Length;
//        maleCount = maleCandidates.Length;
//        CostMatrix = new int[maleCount, femaleCount];
//        CostMatrixMale = new int[Math.Min(10, maleCount), femaleCount];
//        CostMatrixFemale = new int[maleCount, Math.Min(10, femaleCount)];
//    }

//    public int CalculateMatchScore(Candidate c1, Candidate c2)
//    {
//        int score = 0;

//        if (c1.SubSector == c2.SubSector) score += 10; // // אותו מגזר (חסידי / ליטאי / ספרדי וכו')

//        if (c1.SubSector == c2.SubSector) score += 5; // // אותו תת-מגזר (לדוגמה: גור / ויז'ניץ וכו')

//        int opennessDiff = Math.Abs((int)c1.ReligiousOpenness - (int)c2.ReligiousOpenness);
//        score += (6 - opennessDiff) * 3;

//        int clothingStyleDiff = Math.Abs((int)c1.ClothingStyle - (int)c2.ClothingStyle);
//        score += (5 - clothingStyleDiff) * 2;// סגנון לבוש זהה – מקבלים ניקוד מלא

//        int PhysiqueDiff = Math.Abs((int)c1.Physique - (int)c2.Physique);//מבנה גוף 


//        int SkinToneDiff = Math.Abs((int)c1.SkinTone - (int)c2.SkinTone);
//        score += (4 - PhysiqueDiff);// גוון עור – = ניקוד גבוה


//        double heightDifference = Math.Abs((double)c1.Height - (double)c2.Height);
//        score += (int)(5 / (1 + Math.Log(1 + heightDifference))); // // פער בגובה – ניקוד יורד ככל שההפרש גדול יותר


//        if (c1.FamilyStatus == c2.FamilyStatus) score += 10; // // מצב משפחתי של ההורים (נשואים / גרושים וכו')

//        if (c1.CandidatePhoneType == c2.CandidatePhoneType) score += 4; // // אם יש או אין פלאפון – זהה = ניקוד

//        if (c1.License == c2.License) score += 3; // // רישיון נהיגה – אם יש לשניהם או לאף אחד, ניקוד

//        if (c1.JobOrStudies == c2.JobOrStudies) score += 8; // // מקצוע זהה – ניקוד גבוה

//        if (c1.SmokingStatus == c2.SmokingStatus) score += 3; // // שניהם מעשנים או לא מעשנים – ניקוד

//        score += 5; // // תוספת בסיסית על כך שזהו חיבור חדש (אין בדיקה על Match קודם כרגע)

//        return Math.Min(score, 100); // // החזרת ניקוד מקסימלי עד 100
//    }


//    public static void ShuffleCandidates(Candidate[] candidates)
//    {
//        for (int i = candidates.Length - 1; i > 0; i--)
//        {
//            int j = _random.Next(0, i + 1);
//            (candidates[i], candidates[j]) = (candidates[j], candidates[i]);
//        }
//    }

//    public void MatrixFilling(int[,] costMatrix)
//    {
//        for (int i = 0; i < costMatrix.GetLength(0); i++)
//        {
//            for (int j = 0; j < costMatrix.GetLength(1); j++)
//            {
//                double score = CalculateMatchScore(maleCandidates[i], femaleCandidates[j]);
//                costMatrix[i, j] = 100 - (int)score;
//            }
//        }

//        ShuffleCandidates(femaleCandidates);
//        ShuffleCandidates(maleCandidates);
//    }

//    public (Candidate[,], int[]) RunHungarianAlgorithm(int[,] costMatrix)
//    {
//        assignments = costMatrix.FindAssignments();
//        Candidate[,] idAssignments = new Candidate[assignments.Length, 2];
//        int[] costMatch = new int[assignments.Length];

//        for (int i = 0; i < assignments.Length; i++)
//        {
//            idAssignments[i, 0] = maleCandidates[i];

//            if (assignments[i] != -1)
//            {
//                idAssignments[i, 1] = femaleCandidates[assignments[i]];
//                costMatch[i] = 100 - costMatrix[i, assignments[i]];
//            }
//            else
//            {
//                idAssignments[i, 1] = null;
//                costMatch[i] = 0;
//            }
//        }

//        return (idAssignments, costMatch);
//    }
//}

using Repository.Entities;
using Repository.Entities.Enums;
using Service.Interfaces;
//using Common.Dto;
using AutoMapper;
using HungarianAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Service.Interfasces;



public class HungarianAlgorithmService : IHungarianAlgorithm
{
    private readonly IMyDetails<Candidate> _candidateService;
    private readonly IService<MatchDto> _matchService;
    private readonly IMapper _mapper;

    private Candidate[] femaleCandidates;
    private Candidate[] maleCandidates;
    private int maleCount, femaleCount;

    public int[,] CostMatrix { get; set; }
    public int[,] CostMatrixMale { get; set; }
    public int[,] CostMatrixFemale { get; set; }
    // ✅ הוסף מטריצה חדשה לאחסון הציונים הגולמיים
    public int[,] RawMatchScoresMatrix { get; set; }

    private int[] assignments;
    private static Random _random;

    public HungarianAlgorithmService(IMyDetails<Candidate> candidateService, IService<MatchDto> matchService, IMapper mapper)
    {
        _candidateService = candidateService;
        _matchService = matchService;
        _mapper = mapper;
        _random = new Random();
    }

    public async Task InitializeCandidatesAsync()
    {
        femaleCandidates = (await _candidateService.GetFemaleCandidatesAsync());
        maleCandidates = (await _candidateService.GetMaleCandidatesAsync());
        Console.WriteLine($"מספר מועמדות: {femaleCandidates.Length}");
        Console.WriteLine($"מספר מועמדים: {maleCandidates.Length}");
        femaleCount = femaleCandidates.Length;
        maleCount = maleCandidates.Length;
        CostMatrix = new int[maleCount, femaleCount];
        CostMatrixMale = new int[Math.Min(10, maleCount), femaleCount];
        CostMatrixFemale = new int[maleCount, Math.Min(10, femaleCount)];
        RawMatchScoresMatrix = new int[maleCount, femaleCount];
    }



    //public int CalculateMatchScore(Candidate c1, Candidate c2)
    //{
    //    int score = 0;

    //    if (c1.CandidateSector == c2.CandidateSector) score += 20;
    //    else score -= 40;
    //    if (c1.SubSector == c2.SubSector) score += 5;
    //    int ageDiff = Math.Abs(c1.Age - c2.Age);
    //    if (ageDiff <= 2) score += 25; // בונוס חזק להתאמה מושלמת
    //    else if (ageDiff <= 5) score += 10; // בונוס קטן על הפרש סביר
    //    else if (ageDiff <= 8) score -= 50; // קנס חזק מאוד
    //    else score -= 70; // קנס דרסטי כמעט פוסל לחלוטין
    //    int opennessDiff = Math.Abs((int)c1.ReligiousOpenness - (int)c2.ReligiousOpenness);
    //    score += (6 - opennessDiff) * 3;
    //    int clothingStyleDiff = Math.Abs((int)c1.ClothingStyle - (int)c2.ClothingStyle);
    //    score += (5 - clothingStyleDiff) * 2;
    //    // שימוש בלוגריתם כדי לא לתת לגובה הרבה ניקוד
    //    double heightDifference = Math.Abs((double)c1.Height - (double)c2.Height);
    //    score += (int)(5 / (1 + Math.Log(1 + heightDifference)));
    //    int PhysiqueDiff = Math.Abs((int)c1.Physique - (int)c2.Physique);
    //    score += (3 - PhysiqueDiff) * 2;
    //    int SkinToneDiff = Math.Abs((int)c1.SkinTone - (int)c2.SkinTone);
    //    score += (4 - PhysiqueDiff);

    //    if (c1.Status == c2.Status) score += 10;

    //    //int FamilyStyleDiff = Math.Abs((int)c1.FamilyStyle - (int)c2.FamilyStyle);
    //    //score += (3 - FamilyStyleDiff) * 3;
    //    //if (c1.FamilyOpenness == c2.FamilyOpenness) score += 6;
    //    if (c1.CandidatePhoneType == c2.CandidatePhoneType) score += 4;
    //    if (c1.License == c2.License) score += 3;

    //    if (c1.PreferredHeadCovering == c2.PreferredHeadCovering) score += 8;
    //    else score -= 15; // קנס משמעותי על אי-התאמה

    //    if (c1.SmokingStatus == c2.SmokingStatus) score += 3;
    //    if (c1.Beard == c2.Beard) score += 3;


    //    return Math.Min(score, 100);
    //}
    public int CalculateMatchScore(Candidate c1, Candidate c2)
    {
        // הגדרת משקלים (הכי חשובים מקבלים משקל גבוה יותר)
        const int MAX_SCORE = 100; // ציון מקסימלי אפשרי
        int currentScore = 0;

        // 1. סטטוס (חובה להתאים) - משקל גבוה מאוד
        if (c1.Status == c2.Status)
        {
            currentScore += 25; // לדוגמה, 25 נקודות על התאמה מושלמת
        }
        else
        {
            return 0; // אם הסטטוס לא תואם, ההתאמה היא 0% (או קנס כבד שיוריד את הציון לאפס או פחות).
        }
        // 2. מגזר (חובה להתאים) - משקל גבוה מאוד
        if (c1.CandidateSector == c2.CandidateSector)
        {
            currentScore += 20; // לדוגמה, 20 נקודות
        }
        else
        {
            return 0; // אם המגזר לא תואם, ההתאמה היא 0%.
        }

        // 3. גיל - קנסות מדורגים
        int ageDiff = Math.Abs(c1.Age - c2.Age);
        if (ageDiff <= 2)
        {
            currentScore += 15; // בונוס חזק
        }
        else if (ageDiff <= 5)
        {
            currentScore += 8; // בונוס סביר
        }
        else if (ageDiff <= 8)
        {
            currentScore -= 10; // קנס בינוני
        }
        else
        {
            currentScore -= 20; // קנס חזק מאוד
        }

        // 4. תת-מגזר (SubSector) - חשוב שיתאים
        if (c1.SubSector == c2.SubSector)
        {
            currentScore += 10;
        }
        else
        {
            currentScore -= 5; // קנס קטן אם לא תואם
        }

        // 5. כיסוי ראש מועדף (PreferredHeadCovering) - חשוב
        if (c1.PreferredHeadCovering == c2.PreferredHeadCovering)
        {
            currentScore += 8;
        }
        else
        {
            currentScore -= 7; // קנס משמעותי
        }

        // --- קריטריונים נוספים (פחות משמעותיים, תוספת נקודות אם מתאים) ---

        // לימוד תורה
        if (c1.TorahLearning == c2.TorahLearning)
        {
            currentScore += 4;
        }

        // עיסוק
        if (c1.JobOrStudies == c2.JobOrStudies)
        {
            currentScore += 3;
        }

        // מוצא
        if (c1.Origin == c2.Origin)
        {
            currentScore += 3;
        }

        // פתיחות דתית
        int opennessDiff = Math.Abs((int)c1.ReligiousOpenness - (int)c2.ReligiousOpenness);
        currentScore += Math.Max(0, (6 - opennessDiff) * 2); // ככל שההבדל קטן יותר, הציון גבוה יותר. ציון מינימלי 0.

        // סגנון לבוש
        int clothingStyleDiff = Math.Abs((int)c1.ClothingStyle - (int)c2.ClothingStyle);
        currentScore += Math.Max(0, (5 - clothingStyleDiff) * 1);

        // גובה - שימוש בלוגריתם עם ניקוד קטן יותר
        double heightDifference = Math.Abs(c1.Height - c2.Height);
        currentScore += (int)(3 / (1 + Math.Log(1 + heightDifference))); // ניקוד יורד במהירות עם הפרש גובה

        // כמה נותנים / כמה מבקשים - כאן תצטרכי להחליט איך את רוצה לחשב.
        // לדוגמה, אם קרוב:
        double givingExpectingDiff = Math.Abs(c1.Giving - c2.Giving) + Math.Abs(c1.Expecting - c2.Expecting);
        if (givingExpectingDiff <= 0.5) currentScore += 2;
        else if (givingExpectingDiff <= 1.5) currentScore += 1;


        // מצב משפחתי הורים
        if (c1.FamilyStatus == c2.FamilyStatus)
        {
            currentScore += 2;
        }

        // סוג טלפון
        if (c1.CandidatePhoneType == c2.CandidatePhoneType)
        {
            currentScore += 1;
        }

        // זקן
        if (c1.Beard == c2.Beard)
        {
            currentScore += 1;
        }

        // עישון
        if (c1.SmokingStatus == c2.SmokingStatus)
        {
            currentScore += 1;
        }

        // רישיון
        if (c1.License == c2.License)
        {
            currentScore += 1;
        }

        // מבנה גוף (Physique) - הוספתי (לא היה בפונקציה המקורית ששלחת אבל היה בקריטריונים המלאים)
        int PhysiqueDiff = Math.Abs((int)c1.Physique - (int)c2.Physique);
        currentScore += Math.Max(0, (3 - PhysiqueDiff) * 1);

        // צבע עור (SkinTone) - הוספתי (לא היה בפונקציה המקורית ששלחת אבל היה בקריטריונים המלאים)
        int SkinToneDiff = Math.Abs((int)c1.SkinTone - (int)c2.SkinTone);
        currentScore += Math.Max(0, (4 - SkinToneDiff) * 1);


        // --- נרמול הציון לטווח 0-100 ---
        // קודם כל נדאג שהציון לא יהיה שלילי
        currentScore = Math.Max(0, currentScore);

        // מכיוון שהגדרנו נקודות מקסימליות לכל קריטריון, נסכם אותן
        // נניח שסיכום הנקודות המקסימלי האפשרי הוא כ-100-120 (תלוי במשקלים שהוגדרו).
        // אם היית רוצה להיות מדויקת, היית צריכה לסכם את כל הבונוסים המקסימליים.
        // לדוגמה: 25 (סטטוס) + 20 (מגזר) + 15 (גיל) + 10 (תת-מגזר) + 8 (כיסוי ראש) + 4 (לימוד תורה) + 3 (עיסוק) + 3 (מוצא) + 12 (פתיחות) + 5 (לבוש) + 3 (גובה) + 2 (נתינה/בקשה) + 2 (הורים) + 1 (טלפון) + 1 (זקן) + 1 (עישון) + 1 (רישיון) + 6 (מבנה גוף) + 4 (צבע עור) = סה"כ 122 נקודות (בערך)

        // לכן, כדי לנרמל ל-100, נחלק את הציון בפועל בציון המקסימלי האפשרי ונכפיל ב-100.
        // כדי לפשט, נניח שהציון המקסימלי שניתן להגיע אליו הוא 100.
        // אם הציון המקסימלי הפוטנציאלי גבוה מ-100, נרמל אותו בהתאם.
        // לצורך הדוגמה, נניח ציון מקסימלי פוטנציאלי של 120 (צריך לחשב אותו במדויק מכל הבונוסים).
        //double normalizedScore = (double)currentScore / 120.0 * 100.0;
        //return (int)Math.Min(Math.Round(normalizedScore), 100);

        // גישה פשוטה יותר: לוודא שהציון לא עולה על 100.
        return Math.Min(currentScore, 100);
    }

    public static void ShuffleCandidates(Candidate[] candidates)
    {
        for (int i = candidates.Length - 1; i > 0; i--)
        {
            int j = _random.Next(0, i + 1);
            (candidates[i], candidates[j]) = (candidates[j], candidates[i]);
        }
    }

    public void MatrixFilling(int[,] costMatrix)
    {
        // ✅ חשוב מאוד: ערבב את המועמדים לפני מילוי המטריצה!
        ShuffleCandidates(maleCandidates);
        ShuffleCandidates(femaleCandidates);

        for (int i = 0; i < costMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < costMatrix.GetLength(1); j++)
            {
                double score = CalculateMatchScore(maleCandidates[i], femaleCandidates[j]);
                RawMatchScoresMatrix[i, j] = (int)score; // שמירת הניקוד הגולמי
                costMatrix[i, j] = (int)(100 - score); // הפיכת הניקוד לעלות
            }
        }
        
    }


    public (Candidate[,], int[]) RunHungarianAlgorithm(int[,] costMatrix)
    {
        assignments = costMatrix.FindAssignments();
        Candidate[,] idAssignments = new Candidate[assignments.Length, 2];
        int[] actualMatchScores = new int[assignments.Length];

        for (int i = 0; i < assignments.Length; i++)
        {
            if (assignments[i] != -1)
            {
                idAssignments[i, 0] = maleCandidates[i];
                idAssignments[i, 1] = femaleCandidates[assignments[i]];
                actualMatchScores[i] = RawMatchScoresMatrix[i, assignments[i]];
            }
            else
            {
                idAssignments[i, 0] = maleCandidates[i];
                idAssignments[i, 1] = null;
                actualMatchScores[i] = 0;
            }
        }
        return (idAssignments, actualMatchScores);
    }
}

