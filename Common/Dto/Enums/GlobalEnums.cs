using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
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

