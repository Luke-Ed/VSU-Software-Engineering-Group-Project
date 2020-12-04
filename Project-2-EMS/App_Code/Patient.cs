namespace Project_2_EMS {
  internal class Patient {
    public Patient(int patientId, string firstName, string lastName, string address, decimal balance) {
      PatientId = patientId;
      FirstName = firstName;
      LastName = lastName;
      Address = address;
      Balance = balance;
    }

    public int PatientId { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string Address { get; }
    public decimal Balance { get; }
  }
}