const hebrewRegex = /^[א-ת׳״\-\s]+$/;
const englishRegex = /^[a-zA-Z' -]+$/;
const digitsRegex = /^\d{9}$/;
const accountRegex = /^\d{1,10}$/;

export const rules = {
  user: {
    fullName: (value: string) => {
      if (value.length > 20) return "שם מלא חייב להכיל עד 20 תווים";
      if (!hebrewRegex.test(value))
        return "שם מלא חייב להכיל אותיות בעברית בלבד";
    },
    englishFullName: (value: string) => {
      if (value.length > 15) return "שם מלא באנגלית חייב להכיל עד 15 תווים";
      if (!englishRegex.test(value))
        return "שם מלא באנגלית חייב להכיל אותיות באנגלית בלבד";
    },
    tz: (value: string) => { 
      if(!digitsRegex.test(value)) return "תעודת זהות חייבת להכיל 9 ספרות";
    },
    account: (value: string) => {
      if(accountRegex.test(value)) return "מספר חשבון חייב להכיל עד 10 ספרות"
    },
    amount: (value: string) => {
      if(accountRegex.test(value)) return "סכום חייב להכיל עד 10 ספרות"
    },
    password: (value: string, confirmationPassword: string) => {
      if(value !== confirmationPassword) return "הסיסמאות אינן תואמות"
      if(value.length < 8) return "הסיסמה חייבת להכיל לפחות 8 תווים"
      if(value.length > 12) return "הסיסמה יכולה להכיל עד 12 תווים"
    }
  },
};
