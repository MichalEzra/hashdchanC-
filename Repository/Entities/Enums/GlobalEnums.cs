using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//namespace Repository.Entities.Enums
//{
//    public enum Gender // מגדר
//    {
//        MALE, // גבר
//        FEMALE, // אישה
//    }
//    public enum CandidateStatus //מצב אישי
//    {
//        SINGLE, // רווק
//        DIVORCED, // גרוש
//        WIDOWED, // אלמן
//        DIVORCED_WITH_KIDS, // גרוש עם ילדים
//        WIDOWED_WITH_KIDS // אלמן עם ילדים
//    }
//    public enum Sector //מגזר 
//    {
//        HASIDI, // חסידי
//        LITAI, // ליטאי
//        SEFARDI, // ספרדי
//        TEIMANI, // תימני
//        CHABAD, // חבד
//        HALF_HALF, // חצי חצי
//        OTHER // אחר
//    }
//    public enum SubSector
//    {
//        YESHIVISH, // ישיבתי
//        BNEI_TORAH_ETZ, // בני תורה עץ
//        BALEI_TSHUVA, // בעלי תשובה
//        YERUSHALMI, // ירושלמי
//        MODERN_HAREDI, // חרדי מודרני
//        CHUTZNIKIM, // חוצניקים
//        CHAZON_ISH, // חזונאישניקים
//        ZILBERMAN, // זילברמן
//        CHASIDIC_BACKGROUND, // רקע חסידי
//        OTHER // אחר
//    }
//    public enum TorahStudy //לימוד תורה 
//    {
//        FULL_TIME, // תורתו אומנתו
//        HALF_HALF, // חצי עובד חצי לומד
//        PART_TIME // קובע עיתים לתורה
//    }
//    public enum EducationInstitution //מוסד לימודים
//    {
//        YESHIVA_KTANA, // ישיבה קטנה
//        YESHIVA_GDOLA, // ישיבה גדולה
//        KIBBUTZ, // קיבוץ
//        HIGH_SCHOOL, // תיכון
//        SHMINAR, //סמינר
//        COLLEGE, // מכללה
//        UNIVERSITY, // אוניברסיטה
//        KOLLEL // כולל
//    }
//    public enum Occupation // עיסוק
//    {
//        STUDENT, // לימודים
//        WORKING // עבודה
//    }
//    public enum Language //שפה
//    {
//        ENGLISH, // אנגלית
//        HEBREW, // עברית
//        YIDDISH, // אידיש
//        FRENCH, // צרפתית
//        SPANISH, // ספרדית
//        RUSSIAN // רוסית
//    }
//    public enum Openness //פתיחות לשנות שמות למשתנים
//    {
//        VERY_STRICT, // שמור מאד
//        CONSERVATIVE, // שמרן
//        TRADITIONAL, // שמור
//        OPEN_TRADITIONAL, // שמור וראש פתוח
//        OPEN, // פתוח
//        MODERN, // מודרני
//        VERY_MODERN // מודרני מאד
//    }
//    public enum HeadCovering // כיסוי ראש
//    {
//        WIG_ONLY, // עקרוני - פאה
//        SCARF_ONLY, // עקרוני - מטפחת
//        WIG_WITH_COVER, // פאה + כיסוי מעל
//        TOP_LACE_WIG, // פאה טופ לייס
//        FLEXIBLE // גמיש - מטפחת או פאה
//    }
//    public enum PhoneType // סוג טלפון
//    {
//        KOSHER, // כשר
//        SUPPORTS_KOSHER, // תומך כשר
//        SECURE_DEVICE, // מכשיר מוגן (הדרן וכדומה)
//        SMARTPHONE, // מכשיר חכם
//        BUTTON_PHONE_SMS, // פלאפון מקשים עם SMS בלבד
//        WORK_PHONE, // פלאפון מוגן לצרכי עבודה
//        TWO_PHONES // שני טלפונים
//    }
//    public enum ParentsStatus // מצב משפחתי
//    {
//        MARRIED, // נשואים
//        DIVORCED, // גרושים
//        FATHER_DECEASED, // אב נפטר
//        MOTHER_DECEASED, // אם נפטרה
//        BOTH_DECEASED // אינם בין החיים
//    }
//    public enum Smoking // עישון
//    {
//        SMOKER, // מעשן
//        OCCASIONAL_SMOKER, // מעשן רק באירועים מיוחדים - תדירות נמוכה
//        NON_SMOKER, // לא מעשן בכלל
//        ELECTRONIC_CIGARETTE // מעשן סיגריה אלקטרונית בלבד
//    }
//    public enum Physique
//    {
//        VERY_THIN, // רזה מאד
//        THIN, // רזה
//        AVERAGE, // ממוצעת
//        FULL // מלאה
//    }

//    public enum SkinTone
//    {
//        FAIR, // בהיר
//        FAIR_TO_MEDIUM, // נוטה לבהיר
//        TAN, // שזוף
//        MEDIUM_TO_DARK, // נוטה לכהה
//        DARK // כהה
//    }

//    public enum HairColor
//    {
//        BROWN, // חום
//        BLACK, // שחור
//        DIRTY_BLONDE, // שטני
//        BLONDE, // בלונדי
//        REDHEAD // ג'ינג'י
//    }

//    public enum ClothingStyle
//    {
//        MODERN, // מודרני
//        TRENDY, // עדכני
//        ELEGANT, // מכובד
//        CLASSIC, // קלאסי
//        SIMPLE, // פשוט
//        VERY_SIMPLE // פשוט מאד
//    }

//}
namespace Repository.Entities.Enums
{
    public enum Gender // מגדר
    {
        זכר,
        נקבה
    }

    public enum CandidateStatus // מצב אישי
    {
        רווק_ה,
        גרוש_ה,
        אלמן_ה,
        גרוש_ה_עם_ילדים,
        אלמן_ה_עם_ילדים
    }

    public enum Sector // מגזר
    {
        חסידי,
        ליטאי,
        ספרדי,
        תימני,
        חבד,
        חצי_חצי,
        אחר
    }

    public enum SubSector // תת מגזר
    {
        ישיבתי,
        בני_תורה_עץ,
        בעלי_תשובה,
        ירושלמי,
        חרדי_מודרני,
        חוצניקים,
        חזונאישניק,
        זילברמן,
        רקע_חסידי,
        אחר
    }

    public enum TorahStudy // לימוד תורה
    {
        תורתו_אומנותו,
        חצי_עובד_חצי_לומד,
        קובע_עיתים
    }

    public enum EducationInstitution // מוסד לימודים
    {
        ישיבה_קטנה,
        ישיבה_גדולה,
        קיבוץ,
        תיכון,
        סמינר,
        מכללה,
        אוניברסיטה,
        כולל
    }

    public enum Occupation // עיסוק
    {
        לומד,
        עובד
    }

    public enum Language // שפה
    {
        אנגלית,
        עברית,
        אידיש,
        צרפתית,
        ספרדית,
        רוסית
    }

    public enum Openness // פתיחות
    {
        שמור_מאוד,
        שמרן,
        מסורתי,
        שמור_וראש_פתוח,
        פתוח,
        מודרני,
        מודרני_מאוד
    }

    public enum HeadCovering // כיסוי ראש
    {
        פאה_בלבד,
        מטפחת_בלבד,
        פאה_עם_כיסוי,
        טופ_לייס,
        גמיש
    }

    public enum PhoneType // סוג טלפון
    {
        כשר,
        תומך_כשר,
        מכשיר_מוגן,
        סמארטפון,
        פלאפון_מקשים_עם_SMS,
        טלפון_מוגן_לעבודה,
        שני_טלפונים
    }

    public enum ParentsStatus // מצב משפחתי של ההורים
    {
        נשואים,
        גרושים,
        אב_נפטר,
        אם_נפטרה,
        שניהם_נפטרו
    }

    public enum Smoking // עישון
    {
        מעשן,
        מעשן_לעיתים_רחוקות,
        לא_מעשן,
        סיגריה_אלקטרונית
    }

    public enum Physique // מבנה גוף
    {
        רזה_מאוד,
        רזה,
        ממוצע_ת,
        מלא_ה
    }

    public enum SkinTone // גוון עור
    {
        בהיר,
        נוטה_לבהיר,
        שזוף,
        נוטה_לכהה,
        כהה
    }

    public enum HairColor // צבע שיער
    {
        חום,
        שחור,
        שטני,
        בלונדי,
        גינגי
    }

    public enum ClothingStyle // סגנון לבוש
    {
        מודרני,
        עדכני,
        מכובד,
        קלאסי,
        פשוט,
        פשוט_מאוד
    }
}

