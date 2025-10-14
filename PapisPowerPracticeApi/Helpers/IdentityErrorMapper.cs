using Microsoft.AspNetCore.Identity;

namespace PapisPowerPracticeApi.Helpers
{
    public static class IdentityErrorMapper
    {
        public static string MapErrors(IEnumerable<IdentityError>errors)
        {
            var message = new List<string>();

            foreach (var error in errors)
            {
                switch (error.Code)
                {
                    case "DuplicateEmail":
                        message.Add("Emailen används redan på ett konto");
                        break;

                    case "PasswordTooShort":
                        message.Add("Lösenordet måste innehålla minst 6 tecken");
                        break;

                    case "PasswordRequiresUniqueChars":
                        message.Add("Lösenordet behöver innehålla flera olika tecken");
                        break;

                    case "PasswordRequiresNonAlphanumeric":
                        message.Add("Lösenordet behöver innehålla minst ett specialtecken");
                        break;

                    case "PasswordRequiresDigit":
                        message.Add("Lösenordet behöver innehålla minst en siffra");
                        break;

                    case "PasswordRequiresUpper":
                        message.Add("Lösenordet behöver innehålla minst en stor bokstav");
                        break;

                    default:
                        message.Add(error.Description);
                        break;
                }
            }
            return string.Join("; ", message);
        }
    }
}
