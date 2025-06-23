using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization; // יש להוסיף את זה!

namespace Common.Dto
{
    public enum Gender // מגדר
    {
        [EnumMember(Value = "זכר")] // הערך הזה ישלח ויתקבל כ-"זכר"
        זכר,
        [EnumMember(Value = "נקבה")] // הערך הזה ישלח ויתקבל כ-"נקבה"
        נקבה
    }

    public enum CandidateStatus // מצב אישי
    {
        [EnumMember(Value = "רווק/ה")]
        רווק_ה,
        [EnumMember(Value = "גרוש/ה")]
        גרוש_ה,
        [EnumMember(Value = "אלמן/ה")]
        אלמן_ה,
        [EnumMember(Value = "גרוש/ה עם ילדים")]
        גרוש_ה_עם_ילדים,
        [EnumMember(Value = "אלמן/ה עם ילדים")]
        אלמן_ה_עם_ילדים
    }

    public enum Sector // מגזר
    {
        [EnumMember(Value = "חסידי")]
        חסידי,
        [EnumMember(Value = "ליטאי")]
        ליטאי,
        [EnumMember(Value = "ספרדי")]
        ספרדי,
        [EnumMember(Value = "תימני")]
        תימני,
        [EnumMember(Value = "חבד")]
        חבד,
        [EnumMember(Value = "חצי חצי")] // ודא שזה הערך המצופה (עם קו תחתון)
        חצי_חצי,
        [EnumMember(Value = "אחר")]
        אחר
    }

    public enum SubSector // תת מגזר
    {
        [EnumMember(Value = "ישיבתי")]
        ישיבתי,
        [EnumMember(Value = "בני תורה עץ")] // ודא שזה הערך המצופה
        בני_תורה_עץ,
        [EnumMember(Value = "בעלי תשובה")] // ודא שזה הערך המצופה
        בעלי_תשובה,
        [EnumMember(Value = "ירושלמי")]
        ירושלמי,
        [EnumMember(Value = "חרדי מודרני")]
        חרדי_מודרני,
        [EnumMember(Value = "חוצניקים")]
        חוצניקים,
        [EnumMember(Value = "חזונאישניק")]
        חזונאישניק,
        [EnumMember(Value = "זילברמן")]
        זילברמן,
        [EnumMember(Value = "רקע חסידי")]
        רקע_חסידי,
        [EnumMember(Value = "אחר")]
        אחר
    }

    public enum TorahStudy // לימוד תורה
    {
        [EnumMember(Value = "תורתו אומנותו")]
        תורתו_אומנותו,
        [EnumMember(Value = "חצי עובד חצי לומד")]
        חצי_עובד_חצי_לומד,
        [EnumMember(Value = "קובע עיתים")]
        קובע_עיתים
    }

    public enum EducationInstitution // מוסד לימודים
    {
        [EnumMember(Value = "ישיבה קטנה")]
        ישיבה_קטנה,
        [EnumMember(Value = "ישיבה גדולה")]
        ישיבה_גדולה,
        [EnumMember(Value = "קיבוץ")]
        קיבוץ,
        [EnumMember(Value = "תיכון")]
        תיכון,
        [EnumMember(Value = "סמינר")]
        סמינר,
        [EnumMember(Value = "מכללה")]
        מכללה,
        [EnumMember(Value = "אוניברסיטה")]
        אוניברסיטה,
        [EnumMember(Value = "כולל")]
        כולל
    }

    public enum Occupation // עיסוק
    {
        [EnumMember(Value = "לומד")]
        לומד,
        [EnumMember(Value = "עובד")]
        עובד
    }

    public enum Language // שפה
    {
        [EnumMember(Value = "אנגלית")]
        אנגלית,
        [EnumMember(Value = "עברית")]
        עברית,
        [EnumMember(Value = "אידיש")]
        אידיש,
        [EnumMember(Value = "צרפתית")]
        צרפתית,
        [EnumMember(Value = "ספרדית")]
        ספרדית,
        [EnumMember(Value = "רוסית")]
        רוסית
    }

    public enum Openness // פתיחות
    {
        [EnumMember(Value = "שמור מאוד")]
        שמור_מאוד,
        [EnumMember(Value = "שמרן")]
        שמרן,
        [EnumMember(Value = "מסורתי")] // ודא שזה שם ה-Enum ב-C#
        מסורתי,
        [EnumMember(Value = "שמור וראש פתוח")]
        שמור_וראש_פתוח,
        [EnumMember(Value = "פתוח")]
        פתוח,
        [EnumMember(Value = "מודרני")]
        מודרני,
        [EnumMember(Value = "מודרני מאוד")]
        מודרני_מאוד
    }

    public enum HeadCovering // כיסוי ראש
    {
        [EnumMember(Value = "פאה בלבד")]
        פאה_בלבד,
        [EnumMember(Value = "מטפחת בלבד")]
        מטפחת_בלבד,
        [EnumMember(Value = "פאה עם כיסוי")]
        פאה_עם_כיסוי,
        [EnumMember(Value = "טופ לייס")]
        טופ_לייס,
        [EnumMember(Value = "גמיש")]
        גמיש
    }

    public enum PhoneType // סוג טלפון
    {
        [EnumMember(Value = "כשר")]
        כשר,
        [EnumMember(Value = "תומך כשר")]
        תומך_כשר,
        [EnumMember(Value = "מכשיר מוגן")]
        מכשיר_מוגן,
        [EnumMember(Value = "סמארטפון")]
        סמארטפון,
        [EnumMember(Value = "פלאפון מקשים עם SMS")]
        פלאפון_מקשים_עם_SMS,
        [EnumMember(Value = "טלפון מוגן לעבודה")]
        טלפון_מוגן_לעבודה,
        [EnumMember(Value = "שני טלפונים")]
        שני_טלפונים
    }

    public enum ParentsStatus // מצב משפחתי של ההורים
    {
        [EnumMember(Value = "נשואים")]
        נשואים,
        [EnumMember(Value = "גרושים")]
        גרושים,
        [EnumMember(Value = "אב נפטר")]
        אב_נפטר,
        [EnumMember(Value = "אם נפטרה")]
        אם_נפטרה,
        [EnumMember(Value = "שניהם נפטרו")]
        שניהם_נפטרו
    }

    public enum Smoking // עישון
    {
        [EnumMember(Value = "מעשן")]
        מעשן,
        [EnumMember(Value = "מעשן לעיתים רחוקות")]
        מעשן_לעיתים_רחוקות,
        [EnumMember(Value = "לא מעשן")]
        לא_מעשן,
        [EnumMember(Value = "סיגריה אלקטרונית")]
        סיגריה_אלקטרונית
    }

    public enum Physique // מבנה גוף
    {
        [EnumMember(Value = "רזה מאוד")]
        רזה_מאוד,
        [EnumMember(Value = "רזה")]
        רזה,
        [EnumMember(Value = "ממוצע/ת")]
        ממוצע_ת,
        [EnumMember(Value = "מלא/ה")]
        מלא_ה
    }

    public enum SkinTone // גוון עור
    {
        [EnumMember(Value = "בהיר")]
        בהיר,
        [EnumMember(Value = "נוטה לבהיר")]
        נוטה_לבהיר,
        [EnumMember(Value = "שזוף")]
        שזוף,
        [EnumMember(Value = "נוטה לכהה")]
        נוטה_לכהה,
        [EnumMember(Value = "כהה")]
        כהה
    }

    public enum HairColor // צבע שיער
    {
        [EnumMember(Value = "חום")]
        חום,
        [EnumMember(Value = "שחור")]
        שחור,
        [EnumMember(Value = "שטני")]
        שטני,
        [EnumMember(Value = "בלונדי")]
        בלונדי,
        [EnumMember(Value = "גינגי")]
        גינגי
    }

    public enum ClothingStyle // סגנון לבוש
    {
        [EnumMember(Value = "מודרני")]
        מודרני,
        [EnumMember(Value = "עדכני")]
        עדכני,
        [EnumMember(Value = "מכובד")]
        מכובד,
        [EnumMember(Value = "קלאסי")]
        קלאסי,
        [EnumMember(Value = "פשוט")]
        פשוט,
        [EnumMember(Value = "פשוט_מאוד")]
        פשוט_מאוד
    }
}
