using DBDataLibrary.Tables;
using log4net;
using Oracle.ManagedDataAccess.Client;
using System;

namespace CRUDTestApp
{
    /// <summary>
    /// Test class to verify ReadOnlyOnUpdate functionality
    /// </summary>
    public class ReadOnlyOnUpdateTest
    {
        private readonly string _connectionString;
        private readonly ILog _log;

        public ReadOnlyOnUpdateTest(string connectionString)
        {
            _connectionString = connectionString;
            _log = LogManager.GetLogger(typeof(ReadOnlyOnUpdateTest));
        }

        public void RunTest()
        {
            _log.Info("========================================");
            _log.Info("Starting ReadOnlyOnUpdate Test");
            _log.Info("========================================");

            using var conn = new OracleConnection(_connectionString);
            conn.Open();

            try
            {
                TestUpdatableTable(conn);
                _log.Info("ReadOnlyOnUpdate Test PASSED");
            }
            catch (Exception ex)
            {
                _log.Error("ReadOnlyOnUpdate Test FAILED", ex);
            }
        }

        private void TestUpdatableTable(OracleConnection conn)
        {
            string baseMsg = "ReadOnlyOnUpdateTest: ";
            
            _log.Info($"{baseMsg}Testing UpdatableTable with ReadOnlyOnUpdate properties");

            // Step 1: Insert a new record
            var newRecord = new UpdatableTable
            {
                CdItem = 99999,
                Constrain = "TEST_READONLY",
                StEnabled = 1,
                ReturnDefaultPosition = 100
            };

            _log.Info($"{baseMsg}Inserting record with CdItem={newRecord.CdItem}, Constrain={newRecord.Constrain}");
            
            bool inserted = newRecord.Insert(conn, _log, baseMsg);
            if (!inserted)
            {
                throw new Exception("Failed to insert test record");
            }
            _log.Info($"{baseMsg}Record inserted successfully");

            // Step 2: Load the record
            var loadedRecord = UpdatableTable.Get(conn, _log, baseMsg, x => x.CdItem == 99999);
            if (loadedRecord == null)
            {
                throw new Exception("Failed to load inserted record");
            }
            _log.Info($"{baseMsg}Record loaded: CdItem={loadedRecord.CdItem}, Constrain={loadedRecord.Constrain}, StEnabled={loadedRecord.StEnabled}");

            // Step 3: Try to update all fields (including read-only ones)
            _log.Info($"{baseMsg}Attempting to modify all fields including read-only ones");
            
            loadedRecord.CdItem = 88888;          // Read-only - should NOT update in DB
            loadedRecord.Constrain = "MODIFIED";  // Read-only - should NOT update in DB
            loadedRecord.StEnabled = 0;           // Updatable - SHOULD update in DB
            loadedRecord.ReturnDefaultPosition = 200; // Updatable - SHOULD update in DB

            _log.Info($"{baseMsg}Modified values: CdItem={loadedRecord.CdItem}, Constrain={loadedRecord.Constrain}, StEnabled={loadedRecord.StEnabled}, ReturnDefaultPosition={loadedRecord.ReturnDefaultPosition}");

            bool updated = loadedRecord.Update(conn, _log, baseMsg);
            if (!updated)
            {
                throw new Exception("Update operation failed");
            }
            _log.Info($"{baseMsg}Update executed");

            // Step 4: Reload and verify
            var reloadedRecord = UpdatableTable.Get(conn, _log, baseMsg, x => x.CdItem == 99999);
            if (reloadedRecord == null)
            {
                throw new Exception("Failed to reload record after update");
            }

            _log.Info($"{baseMsg}Reloaded record: CdItem={reloadedRecord.CdItem}, Constrain={reloadedRecord.Constrain}, StEnabled={reloadedRecord.StEnabled}, ReturnDefaultPosition={reloadedRecord.ReturnDefaultPosition}");

            // Verify read-only fields were NOT updated
            if (reloadedRecord.CdItem != 99999)
            {
                throw new Exception($"CdItem was updated! Expected 99999, got {reloadedRecord.CdItem}. ReadOnlyOnUpdate failed!");
            }
            if (reloadedRecord.Constrain != "TEST_READONLY")
            {
                throw new Exception($"Constrain was updated! Expected 'TEST_READONLY', got '{reloadedRecord.Constrain}'. ReadOnlyOnUpdate failed!");
            }

            // Verify updatable fields WERE updated
            if (reloadedRecord.StEnabled != 0)
            {
                throw new Exception($"StEnabled was not updated! Expected 0, got {reloadedRecord.StEnabled}");
            }
            if (reloadedRecord.ReturnDefaultPosition != 200)
            {
                throw new Exception($"ReturnDefaultPosition was not updated! Expected 200, got {reloadedRecord.ReturnDefaultPosition}");
            }

            _log.Info($"{baseMsg}? CdItem remained unchanged (99999) - ReadOnly constraint worked!");
            _log.Info($"{baseMsg}? Constrain remained unchanged (TEST_READONLY) - ReadOnly constraint worked!");
            _log.Info($"{baseMsg}? StEnabled was updated to 0 - Update worked!");
            _log.Info($"{baseMsg}? ReturnDefaultPosition was updated to 200 - Update worked!");

            // Cleanup
            _log.Info($"{baseMsg}Cleaning up test record");
            reloadedRecord.Delete(conn, _log, baseMsg);
            _log.Info($"{baseMsg}Test record deleted");
        }
    }
}
