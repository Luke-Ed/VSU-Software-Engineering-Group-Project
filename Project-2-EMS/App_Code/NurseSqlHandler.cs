using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_2_EMS {
  class NurseSqlHandler {
    protected string NameQuerier(String name) {
      String query = "SELECT * FROM PatientInfo WHERE FirstName LIKE " + name + "OR LastName LIKE " + name;

      return query;
    }
  }
}
