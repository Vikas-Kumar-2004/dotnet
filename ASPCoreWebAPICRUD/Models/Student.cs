using System;
using System.Collections.Generic;

namespace ASPCoreWebAPICRUD.Models;

public partial class Student
{
    public int Id { get; set; }

    public string Studentname { get; set; } = null!;

    public string Studentgender { get; set; } = null!;

    public int Age { get; set; }

    public int Standard { get; set; }

    public string Fathername { get; set; } = null!;
}
