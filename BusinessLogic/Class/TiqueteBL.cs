using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using Models.Tiquetes;
using Models.Enums;
using Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Class
{
    public class TiqueteBL : ITiqueteBL
    {
        private readonly ITiqueteDA _Tiquete;
        
        public TiqueteBL(ITiqueteDA Tiquete)
        {
            _Tiquete = Tiquete;
        }

        public ResponseGeneric<List<Tiquete>> addValue(Tiquete tiquete)
        {
            return _Tiquete.addValue(tiquete);
        }

        public ResponseGeneric<List<Tiquete>> deleteValue(int idTiquete)
        {
            return _Tiquete.deleteValue(idTiquete);
        }

        public ResponseGeneric<List<Tiquete>> editValue(Tiquete tiquete, int idTiquete)
        {
            return _Tiquete.editValue(tiquete, idTiquete);
        }

        public ResponseGeneric<List<Tiquete>> searchValue(string valor, EnumSearchTiquete tipo)
        {
            return _Tiquete.searchValue(valor, tipo);
        }
    }
}
