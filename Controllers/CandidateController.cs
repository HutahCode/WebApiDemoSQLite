using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiDemoLite.DataRepository;
using WebApiDemoLite.Models;

namespace WebApiDemoLite.Controllers
{
    public class CandidateController : ApiController
    {
        private IRepository<CandidateVM, int> _repository;

        public CandidateController(IRepository<CandidateVM, int> repo)
        {
            _repository = repo;
        }
        
        // GET: api/Candidate
        public IEnumerable<CandidateVM> Get()
        {
            return _repository.GetAll();
        }

        // GET: api/Candidate/5
        public Candidate Get(int id)
        {
            return _repository.GetById(id);
        }

        // POST: api/Candidate
        public Response Post([FromBody]CandidateVM candidate)
        {
            return _repository.Save(candidate);
        }

        // PUT: api/Candidate/5
        public Response Put([FromBody]CandidateVM candidate)
        {
            return _repository.Save(candidate);
        }

        // DELETE: api/Candidate/5
        public Response Delete(int id)
        {
            return _repository.Remove(id);
        }
    }
}
