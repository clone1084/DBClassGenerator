using System.Data;

namespace DBDataLibrary.Utils
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
