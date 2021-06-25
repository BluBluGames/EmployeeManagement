using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";

        public const string Version = "v1";

        public const string Base = Root + "/" + Version;

        public static class Employees
        {
            public const string GetAll = Base + "/employees";

            public const string Update = Base + "/employees";

            public const string Delete = Base + "/employees/{employeeId}";

            public const string Get = Base + "/employees/{employeeId}";

            public const string Create = Base + "/employees";
        }
    }

}
