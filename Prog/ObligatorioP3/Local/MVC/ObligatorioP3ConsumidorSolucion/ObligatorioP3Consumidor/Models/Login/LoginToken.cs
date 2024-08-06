namespace ObligatorioP3Consumidor.Models.Login
{
    public record LoginToken
    {
        public string Email { get; set; }
        public string Rol { get; set; }

        public string Token { get; set; }
    }
}
