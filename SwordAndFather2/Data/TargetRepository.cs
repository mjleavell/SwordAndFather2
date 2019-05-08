using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using SwordAndFather2.Controllers;
using SwordAndFather2.Models;

namespace SwordAndFather2.Data
{
    public class TargetRepository
    {

        //readonly DbConfiguration _dbConfiguration; //can only set readonly fields in the constructor
        string _connectionString;


        public TargetRepository(DbConfiguration dbConfig)
        {
            _connectionString = dbConfig.Value.ConnectionString;
        }

        //const string ConnectionString = "Server=localhost;Database=Sword&Father;Trusted_Connection=True"; //field

        public Target AddTarget(string name, string location, FitnessLevel fitnessLevel, int userId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var insertQuery = @"
                    INSERT INTO [dbo].[Targets]
                                ([Location]
                                ,[Name]
                                ,[FitnessLevel]
                                ,[UserId])
                    OUTPUT inserted.*
                        VALUES
                            (@location
                            ,@name
                            ,@fitnessLevel
                            ,@userId)";

                var parameters = new
                {
                    Name = name,
                    Location = location,
                    FitnessLevel = fitnessLevel,
                    UserId = userId
                };

                var newTarget = db.QueryFirstOrDefault<Target>(insertQuery, parameters); // want generic bc we want it to automattically map

                if (newTarget != null)
                {
                    return newTarget;
                }

                throw new Exception("Could not create a new target");
            }
        }

        public IEnumerable<Target> GetAll()
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var targets = db.Query<Target>("SELECT * FROM Targets").ToList();

                return targets;
            }
        }

    }
}
