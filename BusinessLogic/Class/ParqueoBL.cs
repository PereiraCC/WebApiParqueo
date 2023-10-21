using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using Models.Parqueos;
using Models.Enums;
using Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Class
{
    public class ParqueoBL : IParqueoBL
    {
        private readonly IParqueoDA _Parqueo;
        
        public ParqueoBL(IParqueoDA Parqueo)
        {
            _Parqueo = Parqueo;
        }

        public ResponseGeneric<List<Parqueo>> addValue(Parqueo parqueo)
        {
            return _Parqueo.addValue(parqueo);
        }

        public ResponseGeneric<List<Parqueo>> deleteValue(int idParqueo)
        {
            return _Parqueo.deleteValue(idParqueo);
        }

        public ResponseGeneric<List<Parqueo>> editValue(Parqueo parqueo, int idParqueo)
        {
            return _Parqueo.editValue(parqueo, idParqueo);
        }

        public ResponseGeneric<List<Parqueo>> searchValue(string valor, EnumSearchParqueo tipo)
        {
            return _Parqueo.searchValue(valor, tipo);
        }
    }
}
