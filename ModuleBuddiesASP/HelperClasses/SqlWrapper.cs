using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ModuleBuddiesASP.HelperClasses
{
    public class SqlWrapper
    {
        private SqlCommand _SqlCommand;
        private SqlConnection _SqlConnection;
        private SqlDataAdapter _SqlDataAdapter;
        private DataTable _DataTable;

        public SqlWrapper(String strConnectionString)
        {
            _SqlConnection = new SqlConnection(strConnectionString);
            try
            {
                _SqlConnection.Open();
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }

        }


        public bool isConnected()
        {
            if (_SqlConnection == null || _SqlConnection.State != ConnectionState.Open)
                return false;
            return true;
        }

        public DataTable executeQuery(String command)
        {
            if (!isConnected())
                throw new Exception("Database connection has not been established.");

            _SqlCommand = new SqlCommand(command, _SqlConnection);
            _SqlDataAdapter = new SqlDataAdapter(_SqlCommand);
            _SqlDataAdapter.Fill(_DataTable = new DataTable());

            return _DataTable;
        }

        public int executeNonQuery(String command)
        {
            if (!isConnected())
                throw new Exception("Database connection has not been established.");
            _SqlCommand = new SqlCommand(command, _SqlConnection);

            return _SqlCommand.ExecuteNonQuery();
        }

        public void closeConnection() { _SqlConnection.Close(); }
    }
}