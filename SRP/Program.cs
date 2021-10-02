using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Console;

namespace SRP
{

    //Solution SRP
    class Program
    {
        static void Main(string[] args)
        {
            var appt = new Appointment
            {

                Patient = new Patient
                {
                    Name = "Johan Sanchez",
                    Email = "Johanhotmail.com",
                    PhonoNumber = 123456789
                },
                Time = DateTime.Now
            };

            WriteLine(new AppointmentService().Create(appt));

            ReadLine();
        }

        public class Patient
        {
            public string Name { get; set; }
            public string Email { get; set; } = "";
            public int PhonoNumber { get; set; }
        }

        public class Appointment
        {
            public DateTime Time { get; set; }
            public Patient Patient { get; set; }
        }

        public class ValidationResult
        {
            public List<string> ErrorMessage { get; set; } = new List<string>();
            public bool IsValid { get { return !ErrorMessage.Any(); } }
        }

        public static class AppointmentServiceValidation
        {
            public static ValidationResult Validate(Appointment appointment)
            {
                ValidationResult validation = new ValidationResult();

                if (string.IsNullOrEmpty(appointment.Patient.Name))
                    validation.ErrorMessage.Add("La cita no puede ser agendada, debido a que debe proporcionar un nombre de paciente.");

                if (appointment.Time.Equals(DateTime.MinValue))
                    validation.ErrorMessage.Add("La cita no puede ser agendada, debido a que debe proporcionar la hora de la cita.");

                if (!appointment.Patient.Email.Contains("@") || string.IsNullOrEmpty(appointment.Patient.Email))
                    validation.ErrorMessage.Add("La cita no puede ser agendada, debido a que debe proporcionar un email valido.");

                if (appointment.Patient.PhonoNumber.ToString().Length < 9)
                    validation.ErrorMessage.Add("La cita no puede ser agendada, debido a que debe proporcionar un telefono valido.");

                return validation;
            }
        }

        public class AppointmentService
        {
            public string Create(Appointment appointment)
            {
                ValidationResult valdation = AppointmentServiceValidation.Validate(appointment);

                return valdation.IsValid ?
                    $"La cita quedo agendada para el paciente {appointment.Patient.Name}."
                    : string.Join(Environment.NewLine, valdation.ErrorMessage);
            }
        }
    }
}
//PROBLEM SRP
/**
 *  class Program
    {
        static void Main(string[] args)
        {
            WriteLine(new AppointmentService().Create("Johan Sanchez", "johan@hotmail.com", Convert.ToDateTime("2021-09-21"), 315489654));
            ReadLine();
        }

        public class AppointmentService
        {
            public string Create(string name, string email, DateTime time, int phonoNumber)
            {
                StringBuilder messsage = new StringBuilder();
                bool isValid = true;
                messsage.Append($"Iniciando cita{DateTime.Now.ToLongTimeString()}...");

                if (string.IsNullOrEmpty(name))
                {
                    messsage.Append("La cita no puede ser agendada, debido a que debe proporcionar un nombre de paciente.");
                    isValid = false;
                }

                if (time.Equals(DateTime.MinValue))
                {
                    messsage.Append("La cita no puede ser agendada, debido a que debe proporcionar la hora de la cita.");
                    isValid = false;
                }

                if (!email.Contains("@") || string.IsNullOrEmpty(name))
                {
                    messsage.Append("La cita no puede ser agendada, debido a que debe proporcionar un email valido.");
                    isValid = false;
                }

                if (phonoNumber.ToString().Length < 9)
                {
                    messsage.Append("La cita no puede ser agendada, debido a que debe proporcionar un telefono valido.");
                    isValid = false;
                }

                if (isValid)
                    messsage.Append($"La cita quedo agendada para el paciente {name}");

                return messsage.ToString().Replace(".", Environment.NewLine);
            }
        }
    }
 * */