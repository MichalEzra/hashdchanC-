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

    private int[] assignments;
    private static Random _random = new Random();

    public HungarianAlgorithmService(IMyDetails<Candidate> candidateService, IService<MatchDto> matchService, IMapper mapper)
    {
        _candidateService = candidateService;
        _matchService = matchService;
        _mapper = mapper;
    }

    public async Task InitializeCandidatesAsync()
    {
        femaleCandidates = (await _candidateService.GetFemaleCandidatesAsync())
            .Where(c => c.CandidateGender == Gender.FEMALE)
            .ToArray();

        maleCandidates = (await _candidateService.GetMaleCandidatesAsync())
            .Where(c => c.CandidateGender == Gender.MALE)
            .ToArray();

        femaleCount = femaleCandidates.Length;
        maleCount = maleCandidates.Length;

        CostMatrix = new int[maleCount, femaleCount];
        CostMatrixMale = new int[Math.Min(10, maleCount), femaleCount];
        CostMatrixFemale = new int[maleCount, Math.Min(10, femaleCount)];
    }

    public int CalculateMatchScore(Candidate c1, Candidate c2)
    {
        int score = 0;

        if (c1.SubSector == c2.SubSector) score += 10; // // אותו מגזר (חסידי / ליטאי / ספרדי וכו')
        if (c1.SubSector == c2.SubSector) score += 5; // // אותו תת-מגזר (לדוגמה: גור / ויז'ניץ וכו')

        if (c1.ReligiousOpenness == c2.ReligiousOpenness)
            score += 15; // פתיחות זהה – ניקוד מקסימלי

        if (c1.ClothingStyle == c2.ClothingStyle)
            score += 10; // סגנון לבוש זהה – מקבלים ניקוד מלא

        if (c1.SkinTone == c2.SkinTone) // גוון עור – = ניקוד גבוה
            score += 4;
        double heightDifference = Math.Abs((double)c1.Height - (double)c2.Height);
        score += (int)(5 / (1 + Math.Log(1 + heightDifference))); // // פער בגובה – ניקוד יורד ככל שההפרש גדול יותר




        if (c1.FamilyStatus == c2.FamilyStatus) score += 10; // // מצב משפחתי של ההורים (נשואים / גרושים וכו')

        
        if (c1.CandidatePhoneType == c2.CandidatePhoneType) score += 4; // // אם יש או אין פלאפון – זהה = ניקוד
        if (c1.License == c2.License) score += 3; // // רישיון נהיגה – אם יש לשניהם או לאף אחד, ניקוד
        if (c1.JobOrStudies == c2.JobOrStudies) score += 8; // // מקצוע זהה – ניקוד גבוה
        if (c1.SmokingStatus == c2.SmokingStatus) score += 3; // // שניהם מעשנים או לא מעשנים – ניקוד

        score += 5; // // תוספת בסיסית על כך שזהו חיבור חדש (אין בדיקה על Match קודם כרגע)

        return Math.Min(score, 100); // // החזרת ניקוד מקסימלי עד 100
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
        for (int i = 0; i < costMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < costMatrix.GetLength(1); j++)
            {
                double score = CalculateMatchScore(maleCandidates[i], femaleCandidates[j]);
                costMatrix[i, j] = 100 - (int)score;
            }
        }

        ShuffleCandidates(femaleCandidates);
        ShuffleCandidates(maleCandidates);
    }

    public (Candidate[,], int[]) RunHungarianAlgorithm(int[,] costMatrix)
    {
        assignments = costMatrix.FindAssignments();
        Candidate[,] idAssignments = new Candidate[assignments.Length, 2];
        int[] costMatch = new int[assignments.Length];

        for (int i = 0; i < assignments.Length; i++)
        {
            idAssignments[i, 0] = maleCandidates[i];

            if (assignments[i] != -1)
            {
                idAssignments[i, 1] = femaleCandidates[assignments[i]];
                costMatch[i] = 100 - costMatrix[i, assignments[i]];
            }
            else
            {
                idAssignments[i, 1] = null;
                costMatch[i] = 0;
            }
        }

        return (idAssignments, costMatch);
    }
}

