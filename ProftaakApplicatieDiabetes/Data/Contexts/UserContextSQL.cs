﻿using System.Data.SqlClient;
using Data.Interfaces;
using Models;

namespace Data.Contexts
{
    public class UserContextSQL : IUserContext
    {
        private readonly SqlConnection _conn = Connection.GetConnection();



    }

}
