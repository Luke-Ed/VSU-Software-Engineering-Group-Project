using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_2_EMS {
  class Patient {
    private int PatientId { get; }
    private string FirstName { get; }
    private string LastName { get; }
    private string Address { get; }
    private double Balance { get; }

    public Patient(int patientId, String firstName, String lastName, String address, double balance) {
      PatientId = patientId;
      FirstName = firstName;
      LastName = lastName;
      Address = address;
      Balance = balance;
    }
  }
}
