using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBDataLibrary.DbUtils
{    public static class DbCommandHelper
    {
        public static void AddParameters(IDbCommand command, Dictionary<string, object?> parameters)
        {
            foreach (var kvp in parameters)
            {
                var parameter = command.CreateParameter();
                parameter.ParameterName = kvp.Key;
                parameter.Value = kvp.Value ?? DBNull.Value;
                command.Parameters.Add(parameter);
            }
        }
    }

}
