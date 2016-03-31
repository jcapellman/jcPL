using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

using jcPL.PCL;
using jcPL.PCL.Transports;

namespace jcPL.SQL.Lib {
    public class SQLPS : BasePS {
        private readonly string _dbConnection;
        private static bool _tableVerified = false;

        public SQLPS(string dbConnection) {
            _dbConnection = dbConnection;

            if (!_tableVerified) {
                checkTableSchema();
            }
        }

        private async void checkTableSchema() {
            using (var sqlConn = new SqlConnection(_dbConnection)) {
                await sqlConn.OpenAsync();
                
                var tableCheck = sqlConn.GetSchema("TABLES", new[] { null, null, "jcPL" });

                if (tableCheck.Rows.Count == 0) {
                    var sqlCommand = new SqlCommand("CREATE TABLE dbo.jcPL");

                    await sqlCommand.ExecuteNonQueryAsync();
                }

                _tableVerified = true;
            }
        }

        public override async Task<ReturnSet<T>> GetAsync<T>(string dataKey) {
            using (var sqlConn = new SqlConnection(_dbConnection)) {
                await sqlConn.OpenAsync();

                var sqlCommand = new SqlCommand($"SELECT TOP 1 dataValue FROM dbo.jcPL WHERE dbo.jcPL.dataKey = '{dataKey}' AND dbo.jcPL.Active = 1", sqlConn);

                var executeReader = await sqlCommand.ExecuteReaderAsync();

                var obj = default(T);

                while (executeReader.Read()) {
                    obj = (T)GetObjectFromJSONString<T>(executeReader["dataValue"].ToString());
                }

                sqlConn.Close();

                return new ReturnSet<T>(obj);
            }
        }

        public override ReturnSet<T> Get<T>(string dataKey) {
            throw new NotImplementedException();
        }

        public override Task<bool> PutAsync<T>(string dataKey, T fileData, bool replaceExisting = true) {
            throw new NotImplementedException();
        }

        public override bool Put<T>(string dataKey, T fileData, bool replaceExisting = true) {
            throw new NotImplementedException();
        }

        public override Guid Put<T>(T fileData) { throw new NotImplementedException(); }

        public override Task<Guid> PutAsync<T>(T fileData) { throw new NotImplementedException(); }
    }
}