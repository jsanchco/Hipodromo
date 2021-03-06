﻿namespace Codere.SBGOnline.WebApi.Helpers
{
    public class InfrastructureAppSettings
    {
        public string ConnectionString { get; set; }
    }

    public class JwtAppSettings
    {
        public string SecretKey { get; set; }
        public int ExpirationTokenMinutes { get; set; }
    }
}
