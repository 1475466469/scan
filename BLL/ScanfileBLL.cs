using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Model;

namespace BLL
{
  public  class ScanfileBLL
    {
        private Scanfile sf = new Scanfile();

        public int Save(List<DO_t_TemporaryScan> list)
        {
            return sf.Save(list);
        }
         public void  del(string id)
        {
            sf.del(id);
        }
        public List<DO_t_TemporaryScan> Load(string id)
        {
            return sf.Load(id);
        }
    }
}
