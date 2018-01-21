using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiDemoLite.Models;

namespace WebApiDemoLite.DataRepository
{
    public interface IRepository<TEnt, in Tpk> where TEnt : class
    {
        IEnumerable<TEnt> GetAll();
        TEnt GetById(Tpk id);
        Response Save(TEnt entity);        
        Response Remove(Tpk id);

    }
}
