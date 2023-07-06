namespace ModsenOnlineStore.EmailAuthentication.Domain
{
    public static class Constants
    {
        public const string Theme = "Online store verification code";

        public const string TextTitle = $"<h2>Here is your verification code</h2>";

        public const string TextBody = "<p>copy it and paste it into " +
            "the field for entering the verification code</p>";

        public const string EmailConfirmationTheme = "Online store email confirmation";

        public const string EmailConfirmationText = """
            <form action="{0}" method="post">
                <button type="submit">Approve your email</button>
            </form>
            """;
    }
}
