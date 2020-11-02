using System.Collections.Generic;

namespace LogiticsWebApp.Models
{
    public class DtParameters
    {
        public int Draw { get; set; }
        public DtColumn[] Columns { get; set; }
        public DtOrder[] Order { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public DtSearch Search { get; set; }
        public string SortOrder => Columns != null && Order != null && Order.Length > 0 ? (Columns[Order[0].Column].Data + (Order[0].Dir == DtOrderDir.Desc ? " " + Order[0].Dir : string.Empty)) : null;
        public IEnumerable<string> AdditionalValues { get; set; }

    }
    public class DtColumn
    {
        public string Data { get; set; }
        public string Name { get; set; }
        public bool Searchable { get; set; }
        public bool Orderable { get; set; }
        public DtSearch Search { get; set; }
    }
    public class DtOrder
    {
        public int Column { get; set; }
        public DtOrderDir Dir { get; set; }
    }

    public enum DtOrderDir
    {
        Asc,
        Desc
    }

    public class DtSearch
    {
        public string Value { get; set; }
        public bool Regex { get; set; }
    }

    //---------------------------------------

    public class Awesomeobject
    {
        public int SomeProps1 { get; set; }
        public string SomeProps2 { get; set; }

    }

    public class User
    {
        public string id { get; set; }
        public string name { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string email { get; set; }
        public string testanadditionalfield { get; set; }

    }

    public class Class1
    {
        public int id { get; set; }
        public string user_id { get; set; }
        public Awesomeobject awesomeobject { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public List<User> users { get; set; }

    }

    public class Class2
    {
        public string SomePropertyOfClass2 { get; set; }

    }

    public class DatatableParameters
    {
        public Class1 Class1 { get; set; }
        public Class2 Class2 { get; set; }

    }

}
