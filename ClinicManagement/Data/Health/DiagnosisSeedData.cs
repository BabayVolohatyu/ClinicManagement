using ClinicManagement.Models.Health;

namespace ClinicManagement.Data.Health
{
    public static class DiagnosisSeedData
    {
        public static List<Diagnosis> GetSeedData()
        {
            return new List<Diagnosis>
            {
                // Diagnoses for completed appointments
                new Diagnosis 
                { 
                    Id = 1, 
                    AppointmentId = 1, 
                    Prescription = "Rest for 3-5 days. Take paracetamol 500mg every 6 hours if fever above 38°C. Drink plenty of fluids. Return if symptoms worsen." 
                },
                new Diagnosis 
                { 
                    Id = 2, 
                    AppointmentId = 2, 
                    Prescription = "Hypertension stage 1. Prescribed: Lisinopril 10mg once daily. Monitor blood pressure twice daily. Reduce salt intake. Follow-up in 2 weeks." 
                },
                new Diagnosis 
                { 
                    Id = 3, 
                    AppointmentId = 3, 
                    Prescription = "Common cold in child. Symptomatic treatment: nasal saline drops, paracetamol syrup if fever. Ensure adequate hydration. Monitor temperature." 
                },
                new Diagnosis 
                { 
                    Id = 4, 
                    AppointmentId = 4, 
                    Prescription = "Acute bronchitis. Prescribed: Amoxicillin 500mg three times daily for 7 days. Expectorant syrup. Rest and avoid cold air. Follow-up if no improvement in 5 days." 
                },
                new Diagnosis 
                { 
                    Id = 5, 
                    AppointmentId = 5, 
                    Prescription = "Tension headache. Prescribed: Ibuprofen 400mg as needed. Stress management techniques. Adequate sleep. Return if headaches persist or worsen." 
                }
            };
        }
    }
}

