﻿using System;
using System.Collections.Generic;
using ApplicationTimeCounter.Other;

namespace ApplicationTimeCounter
{
    static class NameActivity_db
    {
        public static Dictionary<string, string> GetNameActivityDictionary()
        {
            string contentCommand = "SELECT Id, NameActivity FROM nameactivity WHERE ID > 1";
            Dictionary<string, string> nameActivity = DataBase.GetDictionaryFromExecuteReader(contentCommand, "Id", "NameActivity");
            return nameActivity;
        }

        public static Dictionary<string, string> GetAllNameActivityDictionary()
        {
            string contentCommand = "SELECT Id, NameActivity FROM nameactivity ";
            Dictionary<string, string> nameActivity = DataBase.GetDictionaryFromExecuteReader(contentCommand, "Id", "NameActivity");
            return nameActivity;
        }

        public static string GetAllNameActivity()
        {
            string contentCommand = "SELECT COUNT(*) AS numberNameActivity FROM nameactivity WHERE ID != 1";
            return DataBase.GetListStringFromExecuteReader(contentCommand, "numberNameActivity")[0];
        }

        public static int GetIDForNameActivity(string nameActivity)
        {
            string contentCommand = "SELECT Id FROM nameactivity WHERE NameActivity = " + SqlValidator.Validate(nameActivity);
            return Convert.ToInt32(DataBase.GetListStringFromExecuteReader(contentCommand, "Id")[0]);
        }

        public static string GetNameActivityForID(int idActivity)
        {
            string contentCommand = "SELECT NameActivity FROM nameactivity WHERE Id = " + idActivity;
            return DataBase.GetListStringFromExecuteReader(contentCommand, "NameActivity")[0];
        }

        public static bool CheckIfExistName(string name)
        {
            string contentCommand = "SELECT COUNT(*) AS ifExistName FROM nameactivity WHERE NameActivity = " + SqlValidator.Validate(name);
            if (Convert.ToInt32(DataBase.GetListStringFromExecuteReader(contentCommand, "ifExistName")[0]) > 0)
                return true;
            else
                return false;
        }

        public static void ChangeNameActivity(string oldNameActivity, string newNameActivity)
        {
            string contentCommand = "UPDATE nameactivity SET NameActivity = " + SqlValidator.Validate(newNameActivity)
                + " WHERE NameActivity = " + SqlValidator.Validate(oldNameActivity);

            if (DataBase.ExecuteNonQuery(contentCommand))
                ApplicationLog.LogService.AddRaportInformation("Nazwa aktywności " + SqlValidator.Validate(oldNameActivity) + " została zmieniona na " + SqlValidator.Validate(newNameActivity));
            else
                ApplicationLog.LogService.AddRaportWarning("Nie udało się zamienić nazwy aktywności " + SqlValidator.Validate(oldNameActivity) + " na " + SqlValidator.Validate(newNameActivity));
        }

        public static bool AddNewActivity(string nameActivity)
        {
            string contentCommand = "INSERT INTO nameactivity (NameActivity) VALUES (" + SqlValidator.Validate(nameActivity) + ")";

            if (DataBase.ExecuteNonQuery(contentCommand))
            {
                ApplicationLog.LogService.AddRaportInformation("Została dodana nowa aktywność " + SqlValidator.Validate(nameActivity));
                return true;
            }
            else
            {
                ApplicationLog.LogService.AddRaportWarning("Nie udało się dodać nowej aktywności " + SqlValidator.Validate(nameActivity));
                return false;
            }
                
        }

        public static bool DeleteActivity(string nameActivity)
        {
            string contentCommand = "DELETE FROM nameactivity WHERE NameActivity = " + SqlValidator.Validate(nameActivity);

            if (DataBase.ExecuteNonQuery(contentCommand))
            {
                ApplicationLog.LogService.AddRaportInformation("Została usunięta aktywność " + SqlValidator.Validate(nameActivity));
                return true;
            }
            else
            {
                ApplicationLog.LogService.AddRaportWarning("Nie udało się usunąć aktywności " + SqlValidator.Validate(nameActivity));
                return false;
            }
                
        }

    }
}
