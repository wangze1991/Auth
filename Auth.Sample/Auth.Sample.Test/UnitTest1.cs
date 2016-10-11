using System;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using Auth.Sample.Domain;
using Auth.Sample.Repository;
namespace Auth.Sample.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var str = "{\"@event\":\"invoke\"}";
            //var obj= JsonConvert.DeserializeObject<EventJson>(str);
            //Assert.AreEqual(obj.@event, "invoke");
            var jObject = JsonConvert.DeserializeObject(str);
           // Assert.IsNotNull(jObject);
           Assert.AreEqual((jObject as JObject)["@event"].Value<string>(),"invoke");
        }
        [TestMethod]
        public void TestInsert() {
            var user = new T_User()
            {

                UserName = "wangze",
                Pwd = "123556",
                CreateTime=DateTime.Now
            };
         var id=new UserRepository().Insert(user);
         Assert.AreEqual(id, 1);
        }
    }
    public class EventJson {
        public string @event;
    }
}
