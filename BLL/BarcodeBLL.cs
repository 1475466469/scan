using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
  public   class BarcodeBLL
    {
        private Barcode bc = new Barcode();
        public BarcodeDetial GetBarlist(string Barcode)
        {
            return bc.GetBarlist(Barcode);
        }
    }
}
