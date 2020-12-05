namespace Project_2_EMS {
  public class Patient {
    public int PatientId { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string Address { get; }
    public decimal Balance { get; }

    public Patient(int patientId, string firstName, string lastName, string address, decimal balance) {
      PatientId = patientId;
      FirstName = firstName;
      LastName = lastName;
      Address = address;
      Balance = balance;
    }

  }
}