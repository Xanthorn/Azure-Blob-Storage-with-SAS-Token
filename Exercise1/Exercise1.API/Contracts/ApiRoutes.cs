namespace Exercise1.API.Contracts
{
    public static class ApiRoutes
    {
        private const string Version = "v1";
        private const string Root = "api";
        private const string Base = Root + "/" + Version;

        public static class User
        {
            private const string UserBase = Base + "/users";

            public const string Add = UserBase;
            public const string Update = UserBase + "/{id}";
            public const string GetById = UserBase + "/{id}";
            public const string GetAll = UserBase;
        }
    }
}
