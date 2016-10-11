using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auth.Sample.Ui
{
    public class JsonConsequence
    {
        public bool isSuccess { get; set; }

        public string msg { get; set; }


        public string extraData { get; set; }


        public JsonConsequence(bool isSuccess, string msg, string extraData)
        {
            this.isSuccess = isSuccess;
            this.msg = msg;
            this.extraData = extraData;
        }

        public JsonConsequence(bool isSuccess)
            : this(isSuccess, null, null)
        {

        }
    }
   
}