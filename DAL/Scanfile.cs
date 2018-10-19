using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace DAL
{
 public   class Scanfile
    {
        private DCF19Entities db = new DCF19Entities();

        public int Save(List<DO_t_TemporaryScan> list)
        {
            try
            {
               

                foreach (DO_t_TemporaryScan item in list)
                {
                    db.DO_t_TemporaryScan.Add(item);
                }

                return db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            



        }
        public void del(string id)
        {
            List<DO_t_TemporaryScan> data = db.DO_t_TemporaryScan.Where(u => u.fStOutLogNo == id).ToList();
            if (data.Count > 0)
            {
                foreach (DO_t_TemporaryScan item in data)
                {
                    db.DO_t_TemporaryScan.Remove(item);
                }
                db.SaveChanges();
            }
           
           

        }

        public List<DO_t_TemporaryScan>  Load(string id)
        {
            List<DO_t_TemporaryScan> list = db.DO_t_TemporaryScan.Where(u => u.fStOutLogNo == id).ToList();
            return list;

        }



    }
}
