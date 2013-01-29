
using Adell.ItCan.Interop.Entities;
using NUnit.Framework;
using System.IO;
using System.Xml.Serialization;

namespace Adell.ItCan.Interop.Tests
{
    [TestFixture]
    public class XmlTests
    {
        [Test]
        public void TestDeserialize()
        {
            TextReader reader = new StreamReader("xmldump.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(config));
            var config = (config)serializer.Deserialize(reader);
            

            
            /*
            TextWriter writer = new StreamWriter("c:\\Users\\hsorbo\\test.xml");
            var v = new config();
            //var desktops = new Desktop [2];
            //desktops
            v.desktops = new desktop[1];
            v.desktops[0] = new desktop() 
            { 
                ip = "10.0.1.1", 
                name = "navn", 
                port = 22, 
                type = protocol.rdp
                
            };

            serializer.Serialize(writer, v);
            writer.Close();
            var config = serializer.Deserialize(reader);
            //Console.WriteLine(config);
            reader.Close();
             * */
        }
    }
}
