using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Reflection;

namespace Serialization
{
    //La serializacion consiste en convertir los datos en un “documento estándar” en el cual se incluirá además de los datos a serializar, una serie de 
    //información adicional que será utilizada por el sistemas destino para construir el objeto original.
    //La serializacion es la tecnica que podemos utilizar para poder enviar objetos como tales en lugar de tipos de datos primitivos
    //asi ya no se limita a enviar tipos de datos "string", "int", "Float"
    //Podemos enviar objetos que tengan sus propios atributos y sus propios metodos
    //La serializacion no es un tema como tal sobre sockets, si no que se puede utilizar de otras formas
    class BinarySerialization
    {
        //Serializar Informacion
        public static byte[] Serializate(object toSerializate)
        {
            MemoryStream memory = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();

            formatter.Serialize(memory, toSerializate);

            return memory.ToArray(); //Se convierte en un array
        }

        //Deserializar Informacion
        public static object Deserializacion(byte[] data)
        {
            MemoryStream memory = new MemoryStream(data);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Binder = new CurrentAssemblyDeserializationBinder();

            return formatter.Deserialize(memory);
        }
    }

    //Binder, nos ayudan a poder comvertir la informacion para poder recibirla de manera correcta 
    public class CurrentAssemblyDeserializationBinder : SerializationBinder 
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            return Type.GetType(String.Format("{0}, {1}", typeName, Assembly.GetExecutingAssembly().FullName));
        }
    }
}
