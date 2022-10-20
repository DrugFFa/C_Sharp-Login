using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPP_SIBKMNET.Models;

namespace WebApplicationMVC_SIBKM.Repositories.Interface
{
    public interface IProvinceRepository
    {
        List<Province> Get();
        Province Get(int id);
        int Post(Province province);
        int Put(int id, Province province );
        int Delete(Province province);
    }
}
