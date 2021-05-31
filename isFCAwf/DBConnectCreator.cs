﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isFCAwf
{
    public static class DBConnectCreator
    {
        private static DataTable Execute(string connectionString, string command)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(command, connectionString);
            var datatable = new DataTable { Locale = CultureInfo.InvariantCulture };
            adapter.Fill(datatable);
            return datatable;
        }

        public static DataTable Authorize(string connectionString, string login, string password)
        {
            var query = $"SELECT Код_должности, ID_Сотрудника FROM dbo.Сотрудники WHERE Логин = '{login}' AND Пароль = '{password}';";
            var result = Execute(connectionString, query);
            return result;
        }

        public static DataTable GetSpecialistData(string connectionString, int id)
        {
            var query = $"SELECT Должность,Фамилия,Имя,Отчество " +
                $"FROM dbo.Сотрудники LEFT JOIN dbo.ДолжностиСправ ON Сотрудники.Код_должности = ДолжностиСправ.Код_должности  " +
                $"WHERE ID_Сотрудника = '{id}';";
            var result = Execute(connectionString, query);
            return result;
        }

        public static DataTable GetOrdersTable(string connectionString)
        {

            var query = $"SELECT з.Номер_заявки, Статус_заявки, Название_предприятия, Задача, " +
                $"сотр.Фамилия + ' ' + сотр.Имя AS ССИ, сотр2.Фамилия + ' ' + сотр2.Имя AS ЭпМ, Дата_составления, Дата_завершения, Код_документа_описания AS КДО " +
                $"FROM dbo.Заявки AS з " +
                $"LEFT JOIN dbo.Статусы AS ст ON ст.Код_статуса_заявки = з.Код_статуса_заявки " +
                $"LEFT JOIN dbo.Предприятия AS пр ON пр.ID_предприятия = з.ID_предприятия " +
                $"LEFT JOIN dbo.ЗадачиСправ AS зс ON зс.Код_задачи = з.Код_задачи " +
                $"LEFT JOIN dbo.Сотрудники AS сотр ON сотр.ID_Сотрудника = з.ID_CСИ " +
                $"LEFT JOIN dbo.Сотрудники AS сотр2 ON сотр2.ID_Сотрудника = з.ID_ЭпМ";
            var result = Execute(connectionString, query);
            return result;
        }

        public static DataTable GetOrdersValuesONNum(string connectionString, int orderNum)
        {

            var query = $"SELECT з.Номер_заявки, з.Код_статуса_заявки, Статус_заявки, Название_предприятия, пр.Код_типа_предпрития,  Адрес_предприятия, Тип_предприятия, з.ID_предприятия, Код_задачи, з.Код_документа_описания, " +
                $"сотр.Фамилия + ' ' + сотр.Имя AS ССИ, сотр2.Фамилия + ' ' + сотр2.Имя AS ЭпМ, Дата_составления, Дата_завершения, Код_документа_описания AS КДО " +
                $"FROM dbo.Заявки AS з " +
                $"LEFT JOIN dbo.Статусы AS ст ON ст.Код_статуса_заявки = з.Код_статуса_заявки " +
                $"LEFT JOIN dbo.Предприятия AS пр ON пр.ID_предприятия = з.ID_предприятия " +
                $"LEFT JOIN dbo.ТипыПредприятий AS тп ON пр.Код_типа_предпрития = тп.Код_типа_предприятия " +
                $"LEFT JOIN dbo.Сотрудники AS сотр ON сотр.ID_Сотрудника = з.ID_CСИ " +
                $"LEFT JOIN dbo.Сотрудники AS сотр2 ON сотр2.ID_Сотрудника = з.ID_ЭпМ " +
                $"where Номер_заявки = '{orderNum}'";
            var result = Execute(connectionString, query);
            return result;
        }

        public static DataTable GetCompanyTypes(string connectionString)
        {
            var query = $"SELECT * From dbo.ТипыПредприятий";
            var result = Execute(connectionString, query);
            return result;
        }
        public static DataTable GetClientRoles(string connectionString)
        {
            var query = $"SELECT * From dbo.СпрРолей";
            var result = Execute(connectionString, query);
            return result;
        }
        public static DataTable GetOrdersStatuses(string connectionString)
        {
            var query = $"SELECT * From dbo.Статусы";
            var result = Execute(connectionString, query);
            return result;
        }

        public static DataTable GetFIOsTable(string connectionString, int orderNum)
        {
            var query = $"SELECT кл.ID_клиента, Номер_заявки,  кл.Фамилия + ' ' + кл.Имя + ' ' + кл.Отчество AS ФИО_клиента, Роль, Телефон " +
                $"FROM dbo.СписКлиентов AS ск LEFT JOIN dbo.Клиенты AS кл ON кл.ID_клиента = ск.ID_клиента " +
                $"LEFT JOIN dbo.СпрРолей AS ср ON ср.Код_роли = кл.Код_роли WHERE Номер_заявки = '{orderNum}'";
            var result = Execute(connectionString, query);
            return result;
        }
        public static DataTable GetSearchResultCompanyOnName(string connectionString, string nameCom)
        {
            var query = $"SELECT * FROM dbo.Предприятия WHERE Название_предприятия LIKE '%{nameCom}%'";
            var result = Execute(connectionString, query);
            return result;
        }
        public static DataTable GetClientsOnFamil(string connectionString, string famil)
        {
            var query = $"SELECT * FROM dbo.Клиенты WHERE Фамилия LIKE '%{famil}%'";
            var result = Execute(connectionString, query);
            return result;
        }
        public static DataTable GetClientsID(string connectionString)
        {
            var query = $"SELECT ID_клиента FROM dbo.Клиенты";
            var result = Execute(connectionString, query);
            return result;
        }

        public static DataTable GetClientsOnOrders(string connectionString, int orderNum)
        {
            var query = $"SELECT * FROM dbo.СписКлиентов WHERE Номер_заявки = {orderNum}";
            return Execute(connectionString, query);
        }

        public static DataTable GetClientsOnOrders(string connectionString, List<int> clients)
        {
            if (clients.Count == 0)
                return new DataTable();
            string clientsShowQuery = "";
            for (int i = 0; i < clients.Count; i++)
            {
                if (clientsShowQuery != "")
                    clientsShowQuery += ",";
                clientsShowQuery += " " + clients[i];
            }
            var query = $"SELECT ID_клиента, Фамилия, Имя, Отчество, Телефон, Роль FROM dbo.Клиенты AS к " +
                $"LEFT JOIN СпрРолей as ср ON к.Код_роли = ср.Код_роли " +
                $"WHERE ID_клиента IN ({clientsShowQuery});";
            //var dt = Execute(connectionString, query);
            return Execute(connectionString, query);
        }


        public static DataRow CommAdder(string connectionString, Order order)
        {
            var query = $"INSERT INTO dbo.Предприятия( Название_предприятия, Адрес_предприятия, Код_типа_предпрития) " +
                   $"OUTPUT Inserted.ID_предприятия " +
                   $"VALUES('{order.CompanyName}', '{order.CompanyAdress}', {order.CompanyType}); ";
            return Execute(connectionString, query).Rows[0];
        }

        public static void OrderAndClientsRelationCreator(string connectionString, List<int> clientsIDlist, int orderNum)
        {
            string query;
            for (int i = 0; i < clientsIDlist.Count; i++)
            {
                query = $"INSERT INTO dbo.СписКлиентов(Номер_заявки, ID_клиента) " + //если нет, то доавляем т.с. связывая
                    $"OUTPUT Inserted.Номер_заявки " +
                    $"VALUES({orderNum}, {clientsIDlist[i]}) ";
                _ = Execute(connectionString, query);
            }
        }
        public static void OrderInsertInBase(string connectionString, Order order, List<int> clientsIDlist, out List<int> abortedClients)
        {
            abortedClients = new List<int>(2);
            string query;
            DataRow dr;
            int companyNewID = order.CompanyNewID;
            if (companyNewID == -1) // Если новое предприятие - то оно заноится в бд
            {
                dr = CommAdder(connectionString, order);
                companyNewID = (int)dr["ID_предприятия"];
            }            //По ID нового или старого предприятия добавляется новая заявка в БД
            query = $"INSERT INTO dbo.Заявки(ID_предприятия, Код_документа_описания, Код_задачи, ID_CСИ, ID_ЭпМ, Дата_составления, Дата_завершения, Код_статуса_заявки) " +
                $"OUTPUT Inserted.Номер_заявки " +
                $"VALUES({companyNewID}, {order.IDdocOfDescriptionOfTheSubjectArea}, {order.OrderTask}, {order.IdIC}, null, GETDATE(), null, {order.orderStatus}); ";
            dr = Execute(connectionString, query).Rows[0];
            int orderNum = (int)dr["Номер_заявки"];
                        // теперь, когда заявка уже добавлена в БД с ней можно связать клиентов по промежуточной (многое-ко-многим) таблице
            query = $"SELECT * FROM dbo.СписКлиентов where Номер_заявки = {orderNum}";
            var existDBRelations = Execute(connectionString, query).Rows;
            query = $"SELECT ID_клиента FROM dbo.Клиенты";
            for (int i = 0; i < clientsIDlist.Count; i++)
            {
                if (existDBRelations.Count > 0)
                for (int j = 0; j < existDBRelations.Count; j++)
                {
                        if (clientsIDlist[i] == (int)existDBRelations[0][j]) //если к данной заявке уже был ранее прикреплен один из клиентов из List-а, то он отправляется назад как недобавленный 
                        {                                                           //а из этой колекции удаляется
                            abortedClients.Add(clientsIDlist[i]);
                            clientsIDlist.RemoveAt(i);
                        }
                }
            }
            OrderAndClientsRelationCreator(connectionString, clientsIDlist, orderNum);
        }

        public static int ClientInsertInBase(string connectionString, Client client)
        {
            string query;
            //DataRow dr;
            int clientdID = client.OldID ;
            if (clientdID == -1)
            {
                query = $"INSERT INTO dbo.Клиенты( Фамилия, Имя, Отчество, Телефон, Код_роли)  " +
                    $"OUTPUT Inserted.ID_клиента " +
                    $"VALUES('{client.Family}', '{client.Name}', '{client.SecondName}', {client.PhoneNumber}, {client.ClientRole})";
                var dr = Execute(connectionString, query).Rows[0];
                clientdID = (int)dr["ID_клиента"];
            }
            if (true) // метод для client.orderNum = -1 (если 1++ то по ID создать спязо в таб СписКлиентов)
            {

            }
            return clientdID;

        }


        public static void DeleteClientFromOrder(string connectionString, int orderNum, int clientID)
        {
            var query = $"DELETE FROM dbo.СписКлиентов WHERE Номер_заявки = {orderNum} AND ID_клиента = {clientID}";
            _ = Execute(connectionString, query);
        }
        public static void UpdateOrder(string connectionString, Order order, List<int> clientsIDlist, int orderNum)
        {
            if (clientsIDlist.Count != 0)
                OrderAndClientsRelationCreator(connectionString, clientsIDlist, orderNum);

            if (order.ComIsNeedToReplase)
            {
                if (order.ComIsNeedToAdd)
                {
                    var dr = CommAdder(connectionString, order);
                    order.CompanyOldID = (int)dr["ID_предприятия"];
                }
                else
                    order.CompanyOldID = order.CompanyNewID;
            }

            var query = $"UPDATE dbo.Заявки " +
                $"SET ID_предприятия = {order.CompanyOldID}, Код_документа_описания = {order.IDdocOfDescriptionOfTheSubjectArea}, Код_задачи = {order.OrderTask}, Код_статуса_заявки = {order.orderStatus} " +
                $"WHERE Номер_заявки = {orderNum}";
            _ = Execute(connectionString, query);
        }



        /*public static DataTable GetOrdersTable(string connectionString)
        {
            var query = $"SELECT з.Номер_заявки, Статус_заявки, кл.Фамилия + ' ' + кл.Имя + ' ' + кл.Отчество AS ФИО_клиента," +
                $"Название_предприятия, Задача, сотр.Фамилия + ' ' + сотр.Имя AS ССИ, сотр2.Фамилия + ' ' + сотр2.Имя AS ЭпМ, Дата_составления, Дата_завершения, Код_документа_описания AS КДО " +
                $"FROM dbo.Заявки AS з " +
                $"LEFT JOIN dbo.Статусы AS ст ON ст.Код_статуса_заявки = з.Код_статуса_заявки " +
                $"LEFT JOIN dbo.СписКлиентов AS ск ON ск.Номер_заявки = з.Номер_заявки " +
                $"LEFT JOIN dbo.Клиенты AS кл ON кл.ID_клиента = ск.ID_клиента " +
                $"LEFT JOIN dbo.Предприятия AS пр ON пр.ID_предприятия = з.ID_предприятия " +
                $"LEFT JOIN dbo.ЗадачиСправ AS зс ON зс.Код_задачи = з.Код_задачи " +
                $"LEFT JOIN dbo.Сотрудники AS сотр ON сотр.ID_Сотрудника = з.ID_CСИ " +
                $"LEFT JOIN dbo.Сотрудники AS сотр2 ON сотр2.ID_Сотрудника = з.ID_ЭпМ";
            var result = Execute(connectionString, query);
            return result;
        }*/


    }
}
