namespace Project_2_EMS {
  internal class NurseSqlHandler {
    protected string NameQuerier(string name) {
      var query = "SELECT * FROM PatientInfo WHERE FirstName LIKE " + name + "OR LastName LIKE " + name;

      return query;
    }
  }
}