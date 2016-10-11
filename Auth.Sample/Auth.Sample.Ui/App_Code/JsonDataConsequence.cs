using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auth.Sample.Ui
{
    public class JsonDataConsequence : JsonConsequence
    {
        public object data { get; set; }

        public JsonDataConsequence(bool isSuccess, object data)
            : base(isSuccess)
        {
            this.data = data;
        }

        public JsonDataConsequence(bool isSuccess)
            : base(isSuccess)
        {

        }

    }
}