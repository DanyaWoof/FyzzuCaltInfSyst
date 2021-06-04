using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isFCAwf
{
    public class Order
    {
        public string CompanyName { get; set; }
        public string CompanyAdress { get; set; }
        public int CompanyType { get; set; }
        public int CompanyOldID { get; set; }
        public int CompanyNewID { get; set; }
        public int OrderTask { get; set; }
        public string DescriptionOfTheSubjectArea { get; set; }
        public int IDdocOfDescriptionOfTheSubjectArea { get; set; }
        public int IdIC { get; set; }
        public int orderStatus { get; set; }
        public bool ComIsNeedToReplase { get; set; }
        public bool ComIsNeedToAdd { get; set; }

    }
    public class Client
    {
        public int OldID { get; set; }
        public string Family { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string PhoneNumber { get; set; }
        public int ClientRole { get; set; }
        public int OrderNum { get; set; }
        public bool OrderExist { get; set; }


    }
    public class SetsParams
    {
        public int dResult_X { get; set; } //0 1 2
        public bool dRresults_onA { get; set; }

    }

    public class NmA_or_nmLP
    {
        public bool Is_nmA { get; set; }
        public bool Is_needToAdd_notUpdate { get; set; }
        public bool FromFree_nmulp { get; set; }
        public int IDfree { get; set; }
        public int ID_nmU_or_nmX { get; set; }
        public string Name { get; set; }
        public double M1 { get; set; }
        public double M2 { get; set; }
        public double A { get; set; }
        public double B { get; set; }

        public int ID_nmA_nmLP { get; set; }

    }

}
