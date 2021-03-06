using System;
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

        public static string GetSpecialistData(string connectionString, int id)
        {
            var query = $"SELECT Должность,Фамилия,Имя,Отчество " +
                $"FROM dbo.Сотрудники LEFT JOIN dbo.ДолжностиСправ ON Сотрудники.Код_должности = ДолжностиСправ.Код_должности  " +
                $"WHERE ID_Сотрудника = '{id}';";
            var queryresult = Execute(connectionString, query);
            var row = queryresult.Rows[0];
            string result =
                ((string)row["Должность"]).Replace(" ", "") + ": " +
                ((string)row["Фамилия"]).Replace(" ", "") + " " +
                ((string)row["Имя"]).Replace(" ", "") + " " +
                ((string)row["Отчество"]).Replace(" ", "");
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
                $"LEFT JOIN dbo.Сотрудники AS сотр2 ON сотр2.ID_Сотрудника = з.ID_ЭпМ " +
                $"order by Номер_заявки desc";
            var result = Execute(connectionString, query);
            return result;
        }

        public static DataTable GetOrdersTableToMaster(string connectionString, int masterID, int checkState, DateTime minDatem, DateTime maxDate, bool isDateFilter)
        {
            var query = $"SELECT з.Номер_заявки AS '№', RTRIM(LTRIM(Статус_заявки)) AS Статус, Название_предприятия AS Предприятие, Задача, " +
                $"сотр2.Фамилия + ' ' + сотр2.Имя AS Мастер, Дата_составления AS Принята, Код_документа_описания AS КДО " +
                $"FROM dbo.Заявки AS з " +
                $"LEFT JOIN dbo.Статусы AS ст ON ст.Код_статуса_заявки = з.Код_статуса_заявки " +
                $"LEFT JOIN dbo.Предприятия AS пр ON пр.ID_предприятия = з.ID_предприятия " +
                $"LEFT JOIN dbo.ЗадачиСправ AS зс ON зс.Код_задачи = з.Код_задачи " +
                $"LEFT JOIN dbo.Сотрудники AS сотр2 ON сотр2.ID_Сотрудника = з.ID_ЭпМ ";
            var filters = new List<string>(2);
            if (checkState == 1)
                filters.Add("ID_ЭпМ is null");
            else if (checkState == 2)
                filters.Add($" ID_ЭпМ = {masterID} AND ст.Код_статуса_заявки = 1"); //TO DO: Заменить 1 на код статуса закрытой заявки
            else if (checkState == 3)
                filters.Add($" ID_ЭпМ = {masterID} AND ст.Код_статуса_заявки = 2"); //TO DO: Заменить 1 на код статуса закрытой заявки
            if (isDateFilter)
            {
                filters.Add($"з.Дата_составления BETWEEN '{minDatem:yyyy-MM-dd}' AND '{maxDate:yyyy-MM-dd}'");
                //{ info.Дата_выдачи?.ToString("\\'yyyy-MM-dd\\'") ?? "NULL"}); ";                filters.Add($"з.Дата_составления BETWEEN '{fromDate:yyyy-MM-dd}' AND '{toDate:yyyy-MM-dd}'");
            }
            string filter = "";
            if (filters.Count != 0)
                filter = " WHERE " + string.Join(" AND ", filters);
            filter += " Order by Номер_заявки desc";
            var result = Execute(connectionString, query + filter);
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
        {//, Номер_заявки
            var query = $"SELECT кл.ID_клиента,  кл.Фамилия + ' ' + кл.Имя + ' ' + кл.Отчество AS ФИО_клиента, Роль, Телефон " +
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

        public static DataTable GetCompanyDataToMaster(string connectionString, int orderNom)
        {
            var query = $"SELECT з.ID_предприятия, Название_предприятия, Адрес_предприятия, Тип_предприятия FROM dbo.Заявки AS з " +
                $"LEFT JOIN dbo.Предприятия AS пр ON пр.ID_предприятия = з.ID_предприятия " +
                $"LEFT JOIN dbo.ТипыПредприятий AS кт ON кт.Код_типа_предприятия = пр.Код_типа_предпрития " +
                $"where Номер_заявки = {orderNom}";
            var result = Execute(connectionString, query);
            return result;
        }

        public static DataTable GetFuzzySet_U(string connectionString, int orderNom)
        {
            var query = $"SELECT Код_М_U, Описание FROM dbo.Заявки AS з " +
                $"LEFT JOIN dbo.Хранилище_множеств_U AS хмU ON хмU.Номер_заявки = з.Номер_заявки " +
                $"where з.Номер_заявки = {orderNom}";
            var result = Execute(connectionString, query);
            return result;
        }

        public static DataTable GetFuzzySetsOfSet_A(string connectionString, int kodOfM_U)
        {
            var query = $"SELECT nmu.Код_М_U, Код_НМ, RTRIM(LTRIM(Имя_НМ)) AS Имя_НМ, m1, m2, a, b  FROM dbo.Хранилище_множеств_U AS nmU " +
                $"LEFT JOIN dbo.[Нечеткие_множества_A<u>] AS nmA ON nmA.Код_М_U = nmU.Код_М_U " +
                $"where nmu.Код_М_U = {kodOfM_U}";
            var result = Execute(connectionString, query);
            return result;
        }

        public static DataTable GetFuzzySetsOfLingVar_X(string connectionString, int kodOfM_U) //bool isnmA_notLP
        {
            var query = $"SELECT nmu.Код_М_U, лпX.Код_ЛП, лп.Код_НМ_ЛП, лп.Имя, лп.m1, лп.m2, лп.a, лп.b, лпX.Имя_набора_ЛП " +
                $"FROM dbo.Хранилище_множеств_U AS nmU " +
                $"LEFT JOIN dbo.Лингвистические_переменные_Х AS лпX ON лпX.Код_М_U = nmu.Код_М_U " +
                $"LEFT JOIN dbo.ЛП_Нечеткие_множества AS лп ON  лп.Код_ЛП = лпX.Код_ЛП " +
                $"where nmu.Код_М_U = { kodOfM_U}";
            var result = Execute(connectionString, query);
            return result;
        }
        public static DataTable GetFreeFuzzySets_U(string connectionString)
        {
            var query = $"SELECT * FROM [FuzzyCalc].dbo.Хранилище_множеств_U WHERE Номер_заявки is null";
            var result = Execute(connectionString, query);
            return result;
        }
        public static DataTable GetFreeFuzzySets_ofSet_X(string connectionString, int nmUID, bool showAll)
        {
            var query = $"SELECT * FROM [FuzzyCalc].dbo.Лингвистические_переменные_Х WHERE ";
            if (showAll)
                query += $" Код_М_U is null AND ";
            query += $" Код_М_U = {nmUID}";
            var result = Execute(connectionString, query);
            return result;
        }
        public static DataTable GetFreeFuzzySets_A(string connectionString, int nmUID, bool showAll)
        {
            var query = $"SELECT * FROM [FuzzyCalc].dbo.[Нечеткие_множества_A<u>] WHERE ";
            if (showAll)
                query += $" Код_М_U is null AND ";
            query += $" Код_М_U = {nmUID}";
            var result = Execute(connectionString, query);
            return result;
        }
        public static DataTable GetFreeFuzzySets_LPX(string connectionString, int nmXID, bool showAll)
        {
            var query = $"SELECT * FROM [FuzzyCalc].dbo.ЛП_Нечеткие_множества WHERE ";
            if (showAll)
                query += $" Код_ЛП is null AND ";
            query += $" Код_ЛП = {nmXID}";
            var result = Execute(connectionString, query);
            return result;
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
            }                                                                                        //По ID нового или старого предприятия добавляется новая заявка в БД
            query = $"INSERT INTO dbo.Заявки(ID_предприятия, Код_документа_описания, Код_задачи, ID_CСИ, ID_ЭпМ, Дата_составления, Дата_завершения, Код_статуса_заявки) " +
                $"OUTPUT Inserted.Номер_заявки " +
                $"VALUES({companyNewID}, {order.IDdocOfDescriptionOfTheSubjectArea}, {order.OrderTask}, {order.IdIC}, null, GETDATE(), null, {order.orderStatus}); ";
            dr = Execute(connectionString, query).Rows[0];
            int orderNum = (int)dr["Номер_заявки"];

            query = $"SELECT * FROM dbo.СписКлиентов where Номер_заявки = {orderNum}";// теперь, когда заявка уже добавлена в БД с ней можно связать клиентов по промежуточной (многое-ко-многим) таблице
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
            int clientdID = client.OldID;
            if (clientdID == -1)
            {
                query = $"INSERT INTO dbo.Клиенты( Фамилия, Имя, Отчество, Телефон, Код_роли)  " +
                    $"OUTPUT Inserted.ID_клиента " +
                    $"VALUES('{client.Family}', '{client.Name}', '{client.SecondName}', {client.PhoneNumber}, {client.ClientRole})";
                var dr = Execute(connectionString, query).Rows[0];
                clientdID = (int)dr["ID_клиента"];
            }
            if (true) // метод для client.orderNum = -1 (если 1++ то по ID создать связь в таб СписКлиентов)
            {

            }
            return clientdID;

        }

        private static string CustDoubCon(double val)
        {
            var str = val.ToString();
            str = str.Replace(",", ".");
            return str;
        }
        private static string CustDoubCon(string val)
        {
            var str = val.Replace(",", ".");
            return str;
        }
        public static void AddorUpdate_nmA(string connectionString, NmA_or_nmLP nmA_nmLP) /////////////////////
        {
            CultureInfo culInf;
            string query;
            if (nmA_nmLP.FromFree_nmulp)
            {
                if (nmA_nmLP.Is_nmA)
                    query = $"UPDATE dbo.[Нечеткие_множества_A<u>] SET [Код_М_U] = { nmA_nmLP.ID_nmU_or_nmX} WHERE Код_НМ = {nmA_nmLP.IDfree}; ";
                else
                    query = $"UPDATE dbo.ЛП_Нечеткие_множества SET Код_ЛП = {nmA_nmLP.ID_nmU_or_nmX}  WHERE Код_НМ_ЛП = {nmA_nmLP.IDfree};";
                _ = Execute(connectionString, query);
                return;
            }
            if (nmA_nmLP.Is_needToAdd_notUpdate)
            {
                if (nmA_nmLP.Is_nmA)
                {
                    query = $"INSERT INTO dbo.[Нечеткие_множества_A<u>] (Код_М_U, Имя_НМ, m1, m2, a, b) " +
                            $"OUTPUT Inserted.Код_НМ " +
                            $"VALUES({nmA_nmLP.ID_nmU_or_nmX}, '{nmA_nmLP.Name}', {CustDoubCon(nmA_nmLP.M1)}, {CustDoubCon(nmA_nmLP.M2)}, {CustDoubCon(nmA_nmLP.A)}, {CustDoubCon(nmA_nmLP.B)})";
                    //CultureInfo.InvariantCulture
                }
                else
                {
                    query = $"INSERT INTO dbo.ЛП_Нечеткие_множества (Код_ЛП, Имя, m1, m2, a, b) " +
                            $"OUTPUT Inserted.Код_НМ_ЛП " +
                            $"VALUES({nmA_nmLP.ID_nmU_or_nmX}, '{nmA_nmLP.Name}', {CustDoubCon(nmA_nmLP.M1)}, {CustDoubCon(nmA_nmLP.M2)}, {CustDoubCon(nmA_nmLP.A)}, {CustDoubCon(nmA_nmLP.B)})";
                }
            }
            else
            {//query = $"";

                if (nmA_nmLP.Is_nmA)
                {
                    query = $"UPDATE dbo.[Нечеткие_множества_A<u>] SET " +
                            $"[Код_М_U] = { nmA_nmLP.ID_nmU_or_nmX}, " +
                            $"[Имя_НМ] = '{nmA_nmLP.Name}', " +
                            $"[m1] = {CustDoubCon(nmA_nmLP.M1)}, " +
                            $"[m2] = {CustDoubCon(nmA_nmLP.M2)}, " +
                            $"[a]  = {CustDoubCon(nmA_nmLP.A)}, " +
                            $"[b]  = {CustDoubCon(nmA_nmLP.B)} " +
                            $"WHERE Код_НМ = { nmA_nmLP.ID_nmA_nmLP };";
                }
                else
                {
                    query = $"UPDATE dbo.ЛП_Нечеткие_множества SET " +
                            $"Код_ЛП = { nmA_nmLP.ID_nmU_or_nmX}, " +
                            $"[Имя] = '{nmA_nmLP.Name}',  " +
                            $"[m1] = {CustDoubCon(nmA_nmLP.M1)}, " +
                            $"[m2] = {CustDoubCon(nmA_nmLP.M2)}, " +
                            $"[a]  = {CustDoubCon(nmA_nmLP.A)}, " +
                            $"[b]  = {CustDoubCon(nmA_nmLP.B)} " +
                            $"WHERE Код_НМ_ЛП = { nmA_nmLP.ID_nmA_nmLP };";
                }
            }
            _ = Execute(connectionString, query);
        }

        public static void GetnmToShowToApdate(string connectionString, int orderNum, int clientID)
        {
            var query = $"DELETE FROM dbo.СписКлиентов WHERE Номер_заявки = {orderNum} AND ID_клиента = {clientID}";
            _ = Execute(connectionString, query);
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

        public static void OrderOwnerToDungeonMaster(string connectionString, int idFuzExp, int orderNum)
        {
            var query = $"UPDATE dbo.Заявки SET ID_ЭпМ = {idFuzExp} WHERE Номер_заявки = {orderNum}";
            _ = Execute(connectionString, query);
        }
        public static void OrderClose(string connectionString, int orderNum)
        {
            var query = $"UPDATE dbo.Заявки SET Код_статуса_заявки = 2 WHERE Номер_заявки = {orderNum}";
            _ = Execute(connectionString, query);
        }
        public static void InsertOrUpdate_nmU(string connectionString, int orderNum, int nmU, string description_nmU, bool U_btnStatChange, bool addLikeFree, bool fromFree_nmUs, int freeIdnmU)
        {
            string query;
            if (!U_btnStatChange) //изменить
                query = $"UPDATE dbo.Хранилище_множеств_U SET Описание = '{description_nmU}' WHERE Код_М_U = {nmU}";
            else
            { //добавить
                if (fromFree_nmUs)
                {//из свободных множеств
                    query = $"UPDATE dbo.Хранилище_множеств_U SET [Номер_заявки] = {orderNum} WHERE Код_М_U = {freeIdnmU}"; //oldIdnmU
                }
                else
                { //новое множество
                    if (addLikeFree)
                    {//добавить без привязки к заявке
                        query = $"INSERT INTO dbo.Хранилище_множеств_U( Номер_заявки, Описание) " +
                                $"OUTPUT Inserted.Код_М_U " +
                                $"VALUES(null, '{description_nmU}')";
                    }
                    else
                    { //добваить к заявке
                        query = $"INSERT INTO dbo.Хранилище_множеств_U( Номер_заявки, Описание) " +
                                $"OUTPUT Inserted.Код_М_U " +
                                $"VALUES({orderNum}, '{description_nmU}')";
                    }
                }
            }
            _ = Execute(connectionString, query);
        }


        public static void InsertOrUpdate_nmX(string connectionString, int orderNum, int nmU, string description_nmU, bool U_btnStatChange, bool addLikeFree, bool fromFree_nmUs, int freeIdnmU)
        {
            string query;
            if (!U_btnStatChange) //изменить
                query = $"UPDATE dbo.[Лингвистические_переменные_Х] SET [Имя_набора_ЛП] = {description_nmU} WHERE Код_ЛП = {freeIdnmU}";
            else
            { //добавить
                if (fromFree_nmUs)
                {//из свободных наборов ЛП
                    query = $"UPDATE dbo.Лингвистические_переменные_Х SET [Код_М_U] = {nmU} WHERE [Код_ЛП] = {freeIdnmU}"; //oldIdnmU
                }
                else
                { //новое множество
                    if (addLikeFree)
                    {//добавить без привязки к nmU
                        query = $"INSERT INTO dbo.Лингвистические_переменные_Х( Код_М_U, Имя_набора_ЛП) " +
                                $"OUTPUT Inserted.Код_ЛП " +
                                $"VALUES(null, '{description_nmU}')";
                    }
                    else
                    { //добваить к nmU
                        query = $"INSERT INTO dbo.Лингвистические_переменные_Х( Код_М_U, Имя_набора_ЛП) " +
                                $"OUTPUT Inserted.Код_ЛП " +
                                $"VALUES({nmU}, '{description_nmU}')";
                    }
                }
            }
            _ = Execute(connectionString, query);
        }


        public static void DeleteSetsU_U(string connectionString, int orderNum, int ID_mU, SetsParams setsParams)
        {
            var query = $"SELECT Код_ЛП FROM dbo.Хранилище_множеств_U AS хму " +
                $"LEFT JOIN dbo.Лингвистические_переменные_Х AS лпх ON хму.Код_М_U = лпх.Код_М_U " +
                $"WHERE хму.Код_М_U = {ID_mU}";
            var drSelectRes = Execute(connectionString, query).Rows;
            for (int i = 0; drSelectRes.Count != 0 && i < drSelectRes.Count; i++)
            {
                int.TryParse(drSelectRes[i]["Код_ЛП"].ToString(), out int LP_ID);
                query = "";
                switch (setsParams.dResult_X)
                {
                    case 2:
                        //query = $"";
                        query = $"DELETE FROM dbo.ЛП_Нечеткие_множества WHERE Код_ЛП = {LP_ID} ; ";                        // _ = Execute(connectionString, query);
                        query += $"DELETE FROM dbo.Лингвистические_переменные_Х WHERE Код_ЛП = {LP_ID} ;";
                        break;
                    case 1:
                        query = $"UPDATE dbo.ЛП_Нечеткие_множества SET Код_ЛП = null WHERE Код_ЛП = {LP_ID} ; ";
                        query += $"DELETE FROM dbo.Лингвистические_переменные_Х WHERE Код_ЛП = {LP_ID}";
                        break;
                    case 0:
                        query = $"UPDATE dbo.ЛП_Нечеткие_множества SET Код_ЛП = null WHERE Код_ЛП = {LP_ID} ; ";
                        query += $"UPDATE dbo.Лингвистические_переменные_Х SET Код_М_U = null WHERE Код_М_U = {ID_mU}";
                        break;
                    default:
                        break;
                }
                _ = Execute(connectionString, query);
            }

            if (setsParams.dRresults_onA)
                query = $"DELETE FROM dbo.[Нечеткие_множества_A<u>] WHERE Код_М_U = {ID_mU} ;";
            else
                query = $"UPDATE dbo.[Нечеткие_множества_A<u>] SET Код_М_U = null WHERE Код_М_U = {ID_mU};";
            _ = Execute(connectionString, query);
            query = $"DELETE FROM dbo.Хранилище_множеств_U WHERE Код_М_U = {ID_mU} AND Номер_заявки = {orderNum};";
            _ = Execute(connectionString, query);
        }

        public static void Delete_nmAorLP (string connectionString, bool isnmA, int nmID, bool deleteAll)
        {
            string query;
            if (deleteAll)
            {
                int nmUID = nmID;
                if (isnmA)
                    query = $"DELETE FROM dbo.[Нечеткие_множества_A<u>] WHERE Код_М_U = {nmUID} ;";
                else
                {
                    query = $"DELETE FROM [dbo].[ЛП_Нечеткие_множества] WHERE [Код_НМ_ЛП] = {nmUID} ;";
                }

            }
            else
            {
                if (isnmA)
                    query = $"DELETE FROM dbo.[Нечеткие_множества_A<u>] WHERE Код_НМ= {nmID} ;";
                else
                    query = $"DELETE FROM [dbo].[ЛП_Нечеткие_множества] WHERE [Код_НМ_ЛП] = {nmID} ;";
            }
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

        public static DataTable GetnmuONorderNum(string connectionString, int orderNum)
        {
            var query = $"SELECT * FROM dbo.Хранилище_множеств_U WHERE Номер_заявки = {orderNum}";
            var result = Execute(connectionString, query);
            return result;
        }
        public static DataTable GetNmXonNmUID(string connectionString, int nmUID)
        {
            var query = $"SELECT * FROM dbo.Лингвистические_переменные_Х AS x WHERE x.Код_М_U = {nmUID}";
            var result = Execute(connectionString, query);
            return result;
        }
        public static DataTable GetNmLPonNmX(string connectionString, int nmXID)
        {
            var query = $"SELECT * FROM [dbo].[ЛП_Нечеткие_множества] AS x WHERE x.[Код_ЛП] = {nmXID}";
            var result = Execute(connectionString, query);
            return result;
        }

        public static DataTable GetNmAonMnU(string connectionString, int ID_nmU)
        {
            var query = $"SELECT * FROM dbo.[Нечеткие_множества_A<u>] WHERE Код_М_U = {ID_nmU}";
            var result = Execute(connectionString, query);
            return result;
        }
        public static void InsertCountedNmA(string connectionString, int nmUID, string[] vallOfNewNma, string nameNewNma)
        {
            var query = $"INSERT INTO dbo.[Нечеткие_множества_A<u>] (Код_М_U, Имя_НМ, m1, m2, a, b) " +
                    $"OUTPUT Inserted.Код_НМ " +
                    $"VALUES({nmUID}, '{nameNewNma}', {CustDoubCon((vallOfNewNma[0]))}, {CustDoubCon((vallOfNewNma[1]))}, {CustDoubCon((vallOfNewNma[2]))}, {CustDoubCon((vallOfNewNma[3]))})";
            _ = Execute(connectionString, query);
        }

        public static string[] GetValuesNM(string connectionString, int nmuID, string nmName) 
        {
            string[] nm = new string[6];
            if (nmName != "")
                nmName = $" AND Имя_НМ = '{nmName}'";

            var query = $"SELECT Код_НМ, Имя_НМ, m1, m2, a, b FROM dbo.[Нечеткие_множества_A<u>] WHERE Код_М_U = {nmuID} {nmName}";
            var dt = Execute(connectionString, query);
            if (dt.Rows.Count != 0)
            {
                var dr = dt.Rows[0];
                for (int i = 0; i < nm.Length; i++)
                    nm[i] = (dr[i]).ToString();
            }

            return nm;
        }
    }
}                                                                                                                   ////{info.Дата_выдачи?.ToString("\\'yyyy-MM-dd\\'") ?? "NULL"});";
