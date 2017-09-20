﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTimeCounter
{
    static class NameActivity_db
    {
        public static Dictionary<string, string> GetNameActivityList()
        {
            string contentCommand = "SELECT Id, NameActivity FROM nameactivity WHERE ID > 1";
            Dictionary<string, string> nameActivity = DataBase.GetDictionaryFromExecuteReader(contentCommand, "NameActivity", "Id");
            return nameActivity;
        }
    }
}
