namespace Project_2_EMS {
  internal class Patient {
    public Patient(int patientId, string firstName, string lastName, string address, decimal balance) {
      PatientId = patientId;
      FirstName = firstName;
      LastName = lastName;
      Address = address;
      Balance = balance;
    }

    private int PatientId { get; }
    private string FirstName { get; }
    private string LastName { get; }
    private string Address { get; }
    private decimal Balance { get; }
  }
}