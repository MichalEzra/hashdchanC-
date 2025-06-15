using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities.Enums
{
    public enum Gender // מגדר
    {
        MALE, // גבר
        FEMALE, // אישה
    }
    public enum CandidateStatus //מצב אישי
    {
        SINGLE, // רווק
        DIVORCED, // גרוש
        WIDOWED, // אלמן
        DIVORCED_WITH_KIDS, // גרוש עם ילדים
        WIDOWED_WITH_KIDS // אלמן עם ילדים
    }
    public enum Sector //מגזר 
    {
        HASIDI, // חסידי
        LITAI, // ליטאי
        SEFARDI, // ספרדי
        TEIMANI, // תימני
        CHABAD, // חבד
        HALF_HALF, // חצי חצי
        OTHER // אחר
    }
    public enum SubSector
    {
        YESHIVISH, // ישיבתי
        BNEI_TORAH_ETZ, // בני תורה עץ
        BALEI_TSHUVA, // בעלי תשובה
        YERUSHALMI, // ירושלמי
        MODERN_HAREDI, // חרדי מודרני
        CHUTZNIKIM, // חוצניקים
        CHAZON_ISH, // חזונאישניקים
        ZILBERMAN, // זילברמן
        CHASIDIC_BACKGROUND, // רקע חסידי
        OTHER // אחר
    }
    public enum TorahStudy //לימוד תורה 
    {
        FULL_TIME, // תורתו אומנתו
        HALF_HALF, // חצי עובד חצי לומד
        PART_TIME // קובע עיתים לתורה
    }
    public enum EducationInstitution //מוסד לימודים
    {
        YESHIVA_KTANA, // ישיבה קטנה
        YESHIVA_GDOLA, // ישיבה גדולה
        KIBBUTZ, // קיבוץ
        HIGH_SCHOOL, // תיכון
        COLLEGE, // מכללה
        UNIVERSITY, // אוניברסיטה
        KOLLEL // כולל
    }
    public enum Occupation // עיסוק
    {
        STUDENT, // לימודים
        WORKING // עבודה
    }
    public enum Language //שפה
    {
        ENGLISH, // אנגלית
        HEBREW, // עברית
        YIDDISH, // אידיש
        FRENCH, // צרפתית
        SPANISH, // ספרדית
        RUSSIAN // רוסית
    }
    public enum Openness //פתיחות לשנות שמות למשתנים
    {
        VERY_STRICT, // שמור מאד
        CONSERVATIVE, // שמרן
        TRADITIONAL, // שמור
        OPEN_TRADITIONAL, // שמור וראש פתוח
        OPEN, // פתוח
        MODERN, // מודרני
        VERY_MODERN // מודרני מאד
    }
    public enum HeadCovering // כיסוי ראש
    {
        WIG_ONLY, // עקרוני - פאה
        SCARF_ONLY, // עקרוני - מטפחת
        WIG_WITH_COVER, // פאה + כיסוי מעל
        TOP_LACE_WIG, // פאה טופ לייס
        FLEXIBLE // גמיש - מטפחת או פאה
    }
    public enum PhoneType // סוג טלפון
    {
        KOSHER, // כשר
        SUPPORTS_KOSHER, // תומך כשר
        SECURE_DEVICE, // מכשיר מוגן (הדרן וכדומה)
        SMARTPHONE, // מכשיר חכם
        BUTTON_PHONE_SMS, // פלאפון מקשים עם SMS בלבד
        WORK_PHONE, // פלאפון מוגן לצרכי עבודה
        TWO_PHONES // שני טלפונים
    }
    public enum ParentsStatus // מצב משפחתי
    {
        MARRIED, // נשואים
        DIVORCED, // גרושים
        FATHER_DECEASED, // אב נפטר
        MOTHER_DECEASED, // אם נפטרה
        BOTH_DECEASED // אינם בין החיים
    }
    public enum Smoking // עישון
    {
        SMOKER, // מעשן
        OCCASIONAL_SMOKER, // מעשן רק באירועים מיוחדים - תדירות נמוכה
        NON_SMOKER, // לא מעשן בכלל
        ELECTRONIC_CIGARETTE // מעשן סיגריה אלקטרונית בלבד
    }
    public enum Physique
    {
        VERY_THIN, // רזה מאד
        THIN, // רזה
        AVERAGE, // ממוצעת
        FULL // מלאה
    }

    public enum SkinTone
    {
        FAIR, // בהיר
        FAIR_TO_MEDIUM, // נוטה לבהיר
        TAN, // שזוף
        MEDIUM_TO_DARK, // נוטה לכהה
        DARK // כהה
    }

    public enum HairColor
    {
        BROWN, // חום
        BLACK, // שחור
        DIRTY_BLONDE, // שטני
        BLONDE, // בלונדי
        REDHEAD // ג'ינג'י
    }

    public enum ClothingStyle
    {
        MODERN, // מודרני
        TRENDY, // עדכני
        ELEGANT, // מכובד
        CLASSIC, // קלאסי
        SIMPLE, // פשוט
        VERY_SIMPLE // פשוט מאד
    }

}
