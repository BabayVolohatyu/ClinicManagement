using ClinicManagement.Models.Health;

namespace ClinicManagement.Data.Health
{
    public static class DiagnosisSeedData
    {
        public static List<Diagnosis> GetSeedData()
        {
            var data = new List<Diagnosis>();
            int id = 1;
            
            // Generate diagnoses for completed appointments
            // Only appointments with DidItHappen = true should have diagnoses
            // We'll create diagnoses for approximately 70% of completed appointments
            
            var prescriptions = new[]
            {
                "Rest for 3-5 days. Take acetaminophen 500mg every 6 hours if fever above 100.4°F. Drink plenty of fluids. Return if symptoms worsen.",
                "Hypertension stage 1. Prescribed: Lisinopril 10mg once daily. Monitor blood pressure twice daily. Reduce salt intake. Follow-up in 2 weeks.",
                "Common cold in child. Symptomatic treatment: nasal saline drops, acetaminophen syrup if fever. Ensure adequate hydration. Monitor temperature.",
                "Acute bronchitis. Prescribed: Amoxicillin 500mg three times daily for 7 days. Expectorant syrup. Rest and avoid cold air. Follow-up if no improvement in 5 days.",
                "Tension headache. Prescribed: Ibuprofen 400mg as needed. Stress management techniques. Adequate sleep. Return if headaches persist or worsen.",
                "Type 2 Diabetes diagnosed. Prescribed: Metformin 500mg twice daily. Monitor blood glucose levels. Follow diabetic diet. Schedule follow-up in 1 month.",
                "Asthma exacerbation. Prescribed: Albuterol inhaler as needed. Prednisone 20mg daily for 5 days. Avoid triggers. Follow-up in 1 week.",
                "Urinary tract infection. Prescribed: Ciprofloxacin 250mg twice daily for 7 days. Increase fluid intake. Return if symptoms persist.",
                "Seasonal allergies. Prescribed: Loratadine 10mg daily. Nasal corticosteroid spray. Avoid allergens. Follow-up as needed.",
                "Osteoarthritis of knee. Prescribed: Ibuprofen 600mg three times daily. Physical therapy referral. Weight management. Follow-up in 6 weeks.",
                "Gastroenteritis. Prescribed: Rest and clear liquids. Loperamide as needed for diarrhea. Return if dehydration symptoms occur.",
                "Sinusitis. Prescribed: Amoxicillin-clavulanate 875mg twice daily for 10 days. Nasal decongestant. Steam inhalation. Follow-up in 1 week.",
                "Hypertension stage 2. Prescribed: Lisinopril 20mg and Amlodipine 5mg daily. Monitor blood pressure. Low sodium diet. Follow-up in 2 weeks.",
                "Migraine. Prescribed: Sumatriptan 50mg as needed for acute attacks. Propranolol 40mg twice daily for prevention. Avoid triggers.",
                "Pneumonia. Prescribed: Azithromycin 500mg daily for 5 days. Rest and hydration. Chest X-ray follow-up in 2 weeks.",
                "Bronchitis. Prescribed: Doxycycline 100mg twice daily for 7 days. Expectorant. Rest. Follow-up if no improvement.",
                "Gastritis. Prescribed: Omeprazole 20mg daily. Avoid NSAIDs. Small frequent meals. Follow-up in 2 weeks.",
                "Conjunctivitis. Prescribed: Tobramycin eye drops 4 times daily for 7 days. Warm compresses. Avoid contact lenses.",
                "Otitis media. Prescribed: Amoxicillin 500mg three times daily for 10 days. Pain medication as needed. Follow-up in 2 weeks.",
                "Dermatitis. Prescribed: Hydrocortisone cream twice daily. Avoid irritants. Moisturize regularly. Follow-up if no improvement.",
                "High cholesterol. Prescribed: Atorvastatin 20mg daily. Low-fat diet. Exercise. Recheck lipid panel in 3 months.",
                "Anxiety disorder. Prescribed: Sertraline 50mg daily. Cognitive behavioral therapy referral. Follow-up in 2 weeks.",
                "Depression. Prescribed: Escitalopram 10mg daily. Therapy referral. Regular follow-ups. Emergency contact information provided.",
                "Sleep apnea. Prescribed: CPAP machine. Weight loss program. Sleep hygiene education. Follow-up in 1 month.",
                "Rheumatoid arthritis. Prescribed: Methotrexate 15mg weekly. Folic acid 1mg daily. Regular monitoring. Follow-up in 1 month.",
                "Type 2 Diabetes with complications. Prescribed: Metformin 1000mg twice daily, Glipizide 5mg daily. Diabetic eye exam. Podiatry referral.",
                "COPD exacerbation. Prescribed: Prednisone 40mg daily for 5 days. Albuterol and Ipratropium inhalers. Oxygen therapy. Follow-up in 1 week.",
                "Kidney stones. Prescribed: Pain medication. Increase fluid intake to 3 liters daily. Strain urine. Follow-up with urology.",
                "Gout. Prescribed: Colchicine 0.6mg twice daily. Allopurinol 100mg daily. Low purine diet. Follow-up in 2 weeks.",
                "Hypothyroidism. Prescribed: Levothyroxine 75mcg daily. Recheck TSH in 6 weeks. Follow-up as scheduled.",
                "Irritable Bowel Syndrome. Prescribed: Dicyclomine 20mg as needed. Fiber supplements. Low FODMAP diet. Follow-up in 1 month.",
                "GERD. Prescribed: Omeprazole 20mg twice daily. Elevate head of bed. Avoid late meals. Follow-up in 4 weeks.",
                "Fibromyalgia. Prescribed: Duloxetine 30mg daily. Physical therapy referral. Sleep hygiene. Follow-up in 1 month.",
                "Chronic fatigue syndrome. Prescribed: Graded exercise therapy. Sleep management. Cognitive behavioral therapy. Follow-up in 2 months.",
                "Osteoporosis. Prescribed: Alendronate 70mg weekly. Calcium 1200mg and Vitamin D 800IU daily. Fall prevention. Follow-up in 1 year.",
                "Hyperthyroidism. Prescribed: Methimazole 10mg three times daily. Beta blocker for symptoms. Recheck thyroid function in 4 weeks.",
                "Crohn's disease. Prescribed: Prednisone 40mg daily, Mesalamine 800mg three times daily. Low residue diet. Gastroenterology follow-up.",
                "Ulcerative colitis. Prescribed: Mesalamine 800mg three times daily. Prednisone taper. Monitor symptoms. Follow-up in 2 weeks.",
                "Peptic ulcer disease. Prescribed: Omeprazole 20mg twice daily, Amoxicillin 1000mg and Clarithromycin 500mg twice daily for 14 days.",
                "Hepatitis. Prescribed: Rest and hydration. Avoid alcohol. Monitor liver function. Follow-up in 2 weeks.",
                "Gallstones. Prescribed: Pain management. Low-fat diet. Surgical consultation scheduled. Follow-up as needed.",
                "Pancreatitis. Prescribed: NPO (nothing by mouth). IV fluids. Pain management. Hospital admission if severe.",
                "Appendicitis. Prescribed: Surgical consultation. NPO. IV antibiotics. Emergency appendectomy scheduled.",
                "Diverticulitis. Prescribed: Ciprofloxacin and Metronidazole. Clear liquid diet. Follow-up in 1 week.",
                "Hemorrhoids. Prescribed: Hydrocortisone suppositories. Sitz baths. Increase fiber. Follow-up if no improvement.",
                "Eczema. Prescribed: Triamcinolone cream twice daily. Moisturize frequently. Avoid triggers. Follow-up in 2 weeks.",
                "Psoriasis. Prescribed: Clobetasol cream. Phototherapy referral. Avoid stress. Follow-up in 1 month.",
                "Acne vulgaris. Prescribed: Tretinoin cream nightly. Benzoyl peroxide wash. Follow-up in 6 weeks.",
                "Rosacea. Prescribed: Metronidazole gel. Avoid triggers. Sun protection. Follow-up in 2 months.",
                "Shingles. Prescribed: Valacyclovir 1000mg three times daily for 7 days. Pain management. Follow-up if complications.",
                "Herpes simplex. Prescribed: Acyclovir 400mg three times daily for 7 days. Avoid contact during outbreaks. Follow-up as needed."
            };
            
            // Create diagnoses for approximately 70% of completed appointments
            // Assuming we have appointments with IDs 1-350 that are completed
            var random = new Random(42);
            var completedAppointmentIds = Enumerable.Range(1, 350).Where(i => random.Next(100) < 70).ToList();
            
            foreach (var appointmentId in completedAppointmentIds)
            {
                var prescription = prescriptions[random.Next(prescriptions.Length)];
                
                data.Add(new Diagnosis 
                { 
                    Id = id++, 
                    AppointmentId = appointmentId, 
                    Prescription = prescription 
                });
            }
            
            return data;
        }
    }
}
