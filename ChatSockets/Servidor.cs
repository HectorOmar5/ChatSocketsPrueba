using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using Serialization; //serializar informacion
using Auth; //recibir 

namespace ChatSockets
{
    class Servidor
    {
        IPHostEntry Host; //Nos ayuda a crear el host
        IPAddress IPAdr; //Obtener la direccion IP por la cual vamos a escuchar
        IPEndPoint Point;

        Socket s_Servidor;
        Socket s_Cliente;

        public Servidor(string ip, int Puerto)
        {
            Host = Dns.GetHostEntry(ip);//Nos devuelve un HostEntry
            IPAdr = Host.AddressList[0]; //Obtener la ip de nuestro host
            Point = new IPEndPoint(IPAdr, Puerto); //IPAdr por la cual va a escuchar.....Puerto, obtenemos del constructor
            //Listo para que nuestro socket pueda escuchar 


            s_Servidor = new Socket(IPAdr.AddressFamily, SocketType.Stream, ProtocolType.Tcp); //iniciar el servidor por medio del contructor socket
            s_Servidor.Bind(Point); //Bind, Recibe un EndPoint
            s_Servidor.Listen(10); //Pasar un parametro que es un entero, donde este entero significa los el numero maximo de conexiones que tendra en cola

        }
        //Inicializar el socket
        public void Start()
        {
            Thread t;
            while (true)
            {
                Console.Write("Esperando Conexiones... ");
                s_Cliente = s_Servidor.Accept(); //Acepta la conexion
                t = new Thread(ConexionCliente);
                t.Start(s_Cliente); //Iniciar el hilo
                Console.Write("Se Ha Conectado Un Cliente... ");
            }

        }
            
            public void ConexionCliente(object s)
            {

                Socket s_Cliente = (Socket)s; //convertir el objeto generico en un socket
                byte[] buffer; //array de tipo byte
                User user;

            //Intenta ejecutar el bloque de codigo que esta dentro del try
            try
            {
                //simulando un login
                while (true)
                {
                    buffer = new byte[1024];
                    s_Cliente.Receive(buffer); //buffer de bytes que recibiremos de nuestro cliente
                    user = (User)BinarySerialization.Deserializacion(buffer);

                    if (user.user == "admin" && user.password == "admin") //ver si las credenciales son correctas
                    {
                        byte[] toSend = Encoding.ASCII.GetBytes("Exito Al Entrar");
                        s_Cliente.Send(toSend); //enviar la respuesta por medio del socket

                    }
                    else
                    {
                        byte[] toSend = Encoding.ASCII.GetBytes("Fallo Al Entrar");
                        s_Cliente.Send(toSend); //enviar la respuesta por medio del socket
                    }

                }
            }
            catch (SocketException se) //el bloque catch puede recibir un argumento que es tipo exepcion
            {
                Console.WriteLine("Se Ha Desconectado Un Cliente: {0}", se.Message); //Almacena el mensaje por el cual se desconecto
            }

            }
        //convertir un array de bytes en un string 
        public string byte2string(byte[] buffer)
        {
            string message;
            int endInd;

            message = Encoding.ASCII.GetString(buffer);
            endInd = message.IndexOf('\0');
            if (endInd > 0)
                message = message.Substring(0, endInd);

            return message;
        }

        
    }
}
