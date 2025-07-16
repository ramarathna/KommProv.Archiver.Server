namespace KommProv.Archiver.Server.Data
{
    public class UserSessionService
    {
        public string Username { get; set; }
        public bool IsAuthenticated => !string.IsNullOrWhiteSpace(Username);

    }
}
