using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WSAPI.Models
{
    public class ResponseWorker
    {
        public ResponseWorker(worker entyty)
        {
            this.Id = entyty.Id;
            this.FIO = entyty.FIO;
            this.positionID = entyty.positionID;

        }

        public int Id { get; set; }
        public string FIO { get; set; }
        public int positionID { get; set; }
    }
}