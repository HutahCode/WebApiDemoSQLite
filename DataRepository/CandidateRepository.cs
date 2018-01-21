using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Dapper;
using WebApiDemoLite.Models;

namespace WebApiDemoLite.DataRepository
{
    public class CandidateRepository : AbstractDatabase, IRepository<CandidateVM, int>
    {       

        public IEnumerable<CandidateVM> GetAll()
        {
            IEnumerable<CandidateVM> candidateList = null;            
            using (IDbConnection _db = DBConnection)
            {
                candidateList = _db.Query<CandidateVM>("SELECT id, name, date(substr(dob,1,4) ||  '-' || substr(dob,5,2) || '-' || substr(dob,7,2)) as dob, gender FROM Candidate Order by id").ToList();
                foreach(var candidate in candidateList)
                {                    
                    var favouriteList = _db.Query<Favourite>("SELECT * FROM Favourite WHERE candidateId="+ candidate.id.ToString() +" Order by id").ToList();
                    candidate.favouriteCollection = favouriteList;
                }
            }
            return candidateList;
        }

        public CandidateVM GetById(int id)
        {
            using (IDbConnection _db = DBConnection)
            {
                var candidate = _db.Query<CandidateVM>("SELECT id, name, date(substr(dob,1,4) ||  '-' || substr(dob,5,2) || '-' || substr(dob,7,2)) as dob, gender  FROM Candidate WHERE id=" + id.ToString()).SingleOrDefault();
                if(candidate != null)
                {
                    var favouriteList = _db.Query<Favourite>("SELECT * FROM Favourite WHERE candidateId=" + candidate.id.ToString() + " Order by id").ToList();
                    candidate.favouriteCollection = favouriteList;
                }
                return candidate;
            }
        }

        public Response Save(CandidateVM entity)
        {
            if (entity.id > 0)            
                return Update(entity);            
            else
                return Insert(entity);                
        }

        private Response Insert(CandidateVM entity)
        {
            try
            {
                int lastId = 0;
                var SQLQuery = "INSERT INTO Candidate (name, dob, gender) VALUES (";
                SQLQuery += "'" + entity.name + "', '" + entity.dob.ToString("yyyyMMdd") + "','" + entity.gender + "')";
                using (IDbConnection _db = DBConnection)
                {
                    _db.Execute(SQLQuery, null, null, 0, CommandType.Text);
                    SQLQuery = "SELECT Max(id) FROM Candidate";
                    lastId = _db.ExecuteScalar<Int32>(SQLQuery, null, null, 0, CommandType.Text);

                    foreach (var fav in entity.favouriteCollection)
                    {
                        SQLQuery = "INSERT INTO Favourite (candidateId, favKey, favValue) VALUES (";
                        SQLQuery += lastId.ToString() + ", '" + fav.favKey + "', '" + fav.favValue + "')";
                        _db.Execute(SQLQuery, null, null, 0, CommandType.Text);
                    }
                }
                return new Models.Response { success = true, id = lastId };
            }
            catch (Exception ex)
            {
                return new Models.Response { success = false, errorMessage = ex.Message };
            }
        }

        private Response Update(CandidateVM entity)
        {
            try
            {
                var SQLQuery = "UPDATE Candidate SET name='" + entity.name + "'";
                SQLQuery += ", dob='" + entity.dob.ToString("yyyyMMdd") + "'";
                SQLQuery += ", gender='" + entity.gender + "'";
                SQLQuery += " WHERE id=" + entity.id.ToString();
                using (IDbConnection _db = DBConnection)
                {
                    _db.Execute(SQLQuery);

                    SQLQuery = "DELETE FROM Favourite WHERE candidateId=" + entity.id.ToString();
                    _db.Execute(SQLQuery);

                    foreach (var fav in entity.favouriteCollection)
                    {
                        SQLQuery = "INSERT INTO Favourite (candidateId, favKey, favValue) VALUES (";
                        SQLQuery += entity.id.ToString() + ", '" + fav.favKey + "', '" + fav.favValue + "')";
                        _db.Execute(SQLQuery, null, null, 0, CommandType.Text);
                    }
                }
                return new Response { success = true, id=entity.id };
            }
            catch(Exception ex)
            {
                return new Models.Response { success = false, errorMessage = ex.Message, id = entity.id };
            }
        }

        public Response Remove(int id)
        {
            try
            {
                var SQLQuery = "DELETE FROM Candidate ";
                SQLQuery += " WHERE id=" + id.ToString();
                using (IDbConnection _db = DBConnection)
                {
                    _db.Execute(SQLQuery);

                    SQLQuery = "DELETE FROM Favourite WHERE candidateId=" + id.ToString();
                    _db.Execute(SQLQuery);                    
                }
                return new Response { success = true, id= id };
            }
            catch (Exception ex)
            {
                return new Models.Response { success = false, errorMessage = ex.Message, id =  id };
            }
        }

       
    }
}