using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Auth
{
    //SERIALIZAR LA INFORMACION
    //La clase tiene que ser igual por lado del servidor que por lado del cliente
    //Antes de declaarr la clase se pone una linea "[Serializable]" porque asi le estamos diciendo al framework.NET
    //que nuestra clase se va a apoder serializar.

    [Serializable]
    class User
    {
        public string user;
        public string password;

        public User(string _user, string _password)
        {
            this.user = _user;
            this.password = _password;
        }
    }
}
