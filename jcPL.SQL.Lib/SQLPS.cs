using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

using jcPL.PCL;

namespace jcPL.SQL.Lib {
    public class SQLPS : BasePS {
        private readonly string _dbConnection;

        public SQLPS(string dbConnection) {
            _dbConnection = dbConnection;
        }

        public override async Task<T> GetAsync<T>(string dataKey) {
            using (var sqlConn = new SqlConnection(_dbConnection)) {
                await sqlConn.OpenAsync();

                var sqlCommand = new SqlCommand($"SELECT TOP 1 dataValue FROM dbo.jcPL WHERE dbo.jcPL.dataKey = '{dataKey}' AND dbo.jcPL.Active = 1", sqlConn);

                var executeReader = await sqlCommand.ExecuteReaderAsync();

                var obj = default(T);

                while (executeReader.Read()) {
                    obj = (T)GetObjectFromJSONString<T>(executeReader["dataValue"].ToString());
                }

                sqlConn.Close();

                return obj;
            }
        }

        public override T Get<T>(string dataKey) {
            throw new NotImplementedException();
        }

        public override Task<bool> PutAsync<T>(string dataKey, T fileData, bool replaceExisting = true) {
            throw new NotImplementedException();
        }

        public override bool Put<T>(string dataKey, T fileData, bool replaceExisting = true) {
            throw new NotImplementedException();
        }
    }
}