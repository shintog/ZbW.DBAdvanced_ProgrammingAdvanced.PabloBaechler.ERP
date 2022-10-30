using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZbW.DBAdvanced_ProgrammingAdvanced.PabloBaechler.ERP.Model.Interface
{
    public interface IRepository
    {
        void LoadData();
        void WriteData(Object dataObject);
        KeyValuePair<String, String> DeleteData(Object dataObject);

    }
}
