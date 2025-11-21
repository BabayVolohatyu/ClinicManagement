using ClinicManagement.Models.Health;

namespace ClinicManagement.Data.Health
{
    public static class SicknessSymptomSeedData
    {
        public static List<SicknessSymptom> GetSeedData()
        {
            var data = new List<SicknessSymptom>();
            
            // Common Cold (1)
            data.AddRange(new[] {
                new SicknessSymptom { SicknessId = 1, SymptomId = 1 }, // Fever
                new SicknessSymptom { SicknessId = 1, SymptomId = 2 }, // Headache
                new SicknessSymptom { SicknessId = 1, SymptomId = 3 }, // Cough
                new SicknessSymptom { SicknessId = 1, SymptomId = 4 }, // Sore Throat
                new SicknessSymptom { SicknessId = 1, SymptomId = 5 }, // Runny Nose
                new SicknessSymptom { SicknessId = 1, SymptomId = 24 }, // Sneezing
                new SicknessSymptom { SicknessId = 1, SymptomId = 25 } // Congestion
            });
            
            // Influenza (2)
            data.AddRange(new[] {
                new SicknessSymptom { SicknessId = 2, SymptomId = 1 }, // Fever
                new SicknessSymptom { SicknessId = 2, SymptomId = 2 }, // Headache
                new SicknessSymptom { SicknessId = 2, SymptomId = 3 }, // Cough
                new SicknessSymptom { SicknessId = 2, SymptomId = 13 }, // Fatigue
                new SicknessSymptom { SicknessId = 2, SymptomId = 15 }, // Muscle Pain
                new SicknessSymptom { SicknessId = 2, SymptomId = 21 }, // Chills
                new SicknessSymptom { SicknessId = 2, SymptomId = 23 } // Body Aches
            });
            
            // Hypertension (3)
            data.AddRange(new[] {
                new SicknessSymptom { SicknessId = 3, SymptomId = 2 }, // Headache
                new SicknessSymptom { SicknessId = 3, SymptomId = 12 }, // Dizziness
                new SicknessSymptom { SicknessId = 3, SymptomId = 29 } // High Blood Pressure
            });
            
            // Bronchitis (4)
            data.AddRange(new[] {
                new SicknessSymptom { SicknessId = 4, SymptomId = 3 }, // Cough
                new SicknessSymptom { SicknessId = 4, SymptomId = 7 }, // Shortness of Breath
                new SicknessSymptom { SicknessId = 4, SymptomId = 1 }, // Fever
                new SicknessSymptom { SicknessId = 4, SymptomId = 26 } // Wheezing
            });
            
            // Pneumonia (5)
            data.AddRange(new[] {
                new SicknessSymptom { SicknessId = 5, SymptomId = 1 }, // Fever
                new SicknessSymptom { SicknessId = 5, SymptomId = 3 }, // Cough
                new SicknessSymptom { SicknessId = 5, SymptomId = 6 }, // Chest Pain
                new SicknessSymptom { SicknessId = 5, SymptomId = 7 }, // Shortness of Breath
                new SicknessSymptom { SicknessId = 5, SymptomId = 13 } // Fatigue
            });
            
            // Gastritis (6)
            data.AddRange(new[] {
                new SicknessSymptom { SicknessId = 6, SymptomId = 8 }, // Nausea
                new SicknessSymptom { SicknessId = 6, SymptomId = 11 }, // Abdominal Pain
                new SicknessSymptom { SicknessId = 6, SymptomId = 20 } // Loss of Appetite
            });
            
            // Migraine (7)
            data.AddRange(new[] {
                new SicknessSymptom { SicknessId = 7, SymptomId = 2 }, // Headache
                new SicknessSymptom { SicknessId = 7, SymptomId = 8 }, // Nausea
                new SicknessSymptom { SicknessId = 7, SymptomId = 12 }, // Dizziness
                new SicknessSymptom { SicknessId = 7, SymptomId = 31 } // Blurred Vision
            });
            
            // Osteoarthritis (8)
            data.AddRange(new[] {
                new SicknessSymptom { SicknessId = 8, SymptomId = 14 }, // Joint Pain
                new SicknessSymptom { SicknessId = 8, SymptomId = 15 }, // Muscle Pain
                new SicknessSymptom { SicknessId = 8, SymptomId = 65 }, // Swollen Joints
                new SicknessSymptom { SicknessId = 8, SymptomId = 66 } // Stiff Joints
            });
            
            // Dermatitis (9)
            data.AddRange(new[] {
                new SicknessSymptom { SicknessId = 9, SymptomId = 16 }, // Rash
                new SicknessSymptom { SicknessId = 9, SymptomId = 17 }, // Itching
                new SicknessSymptom { SicknessId = 9, SymptomId = 52 }, // Swelling
                new SicknessSymptom { SicknessId = 9, SymptomId = 53 } // Redness
            });
            
            // Type 2 Diabetes (10)
            data.AddRange(new[] {
                new SicknessSymptom { SicknessId = 10, SymptomId = 13 }, // Fatigue
                new SicknessSymptom { SicknessId = 10, SymptomId = 56 }, // Weight Loss
                new SicknessSymptom { SicknessId = 10, SymptomId = 58 }, // Excessive Thirst
                new SicknessSymptom { SicknessId = 10, SymptomId = 59 }, // Excessive Hunger
                new SicknessSymptom { SicknessId = 10, SymptomId = 48 } // Frequent Urination
            });
            
            // Asthma (11)
            data.AddRange(new[] {
                new SicknessSymptom { SicknessId = 11, SymptomId = 3 }, // Cough
                new SicknessSymptom { SicknessId = 11, SymptomId = 7 }, // Shortness of Breath
                new SicknessSymptom { SicknessId = 11, SymptomId = 26 }, // Wheezing
                new SicknessSymptom { SicknessId = 11, SymptomId = 73 } // Chest Tightness
            });
            
            // Sinusitis (12)
            data.AddRange(new[] {
                new SicknessSymptom { SicknessId = 12, SymptomId = 2 }, // Headache
                new SicknessSymptom { SicknessId = 12, SymptomId = 5 }, // Runny Nose
                new SicknessSymptom { SicknessId = 12, SymptomId = 4 }, // Sore Throat
                new SicknessSymptom { SicknessId = 12, SymptomId = 25 } // Congestion
            });
            
            // Otitis Media (13)
            data.AddRange(new[] {
                new SicknessSymptom { SicknessId = 13, SymptomId = 33 }, // Ear Pain
                new SicknessSymptom { SicknessId = 13, SymptomId = 34 }, // Hearing Loss
                new SicknessSymptom { SicknessId = 13, SymptomId = 1 } // Fever
            });
            
            // Conjunctivitis (14)
            data.AddRange(new[] {
                new SicknessSymptom { SicknessId = 14, SymptomId = 32 }, // Eye Pain
                new SicknessSymptom { SicknessId = 14, SymptomId = 53 }, // Redness
                new SicknessSymptom { SicknessId = 14, SymptomId = 17 } // Itching
            });
            
            // UTI (15)
            data.AddRange(new[] {
                new SicknessSymptom { SicknessId = 15, SymptomId = 49 }, // Painful Urination
                new SicknessSymptom { SicknessId = 15, SymptomId = 50 }, // Frequent Urination
                new SicknessSymptom { SicknessId = 15, SymptomId = 48 }, // Blood in Urine
                new SicknessSymptom { SicknessId = 15, SymptomId = 11 } // Abdominal Pain
            });
            
            // Add more relationships for other sicknesses (16-100)
            // Gastroenteritis (16)
            data.AddRange(new[] {
                new SicknessSymptom { SicknessId = 16, SymptomId = 8 }, // Nausea
                new SicknessSymptom { SicknessId = 16, SymptomId = 9 }, // Vomiting
                new SicknessSymptom { SicknessId = 16, SymptomId = 10 }, // Diarrhea
                new SicknessSymptom { SicknessId = 16, SymptomId = 11 }, // Abdominal Pain
                new SicknessSymptom { SicknessId = 16, SymptomId = 1 } // Fever
            });
            
            // Seasonal Allergies (17)
            data.AddRange(new[] {
                new SicknessSymptom { SicknessId = 17, SymptomId = 5 }, // Runny Nose
                new SicknessSymptom { SicknessId = 17, SymptomId = 24 }, // Sneezing
                new SicknessSymptom { SicknessId = 17, SymptomId = 4 }, // Sore Throat
                new SicknessSymptom { SicknessId = 17, SymptomId = 17 } // Itching
            });
            
            // Rheumatoid Arthritis (18)
            data.AddRange(new[] {
                new SicknessSymptom { SicknessId = 18, SymptomId = 14 }, // Joint Pain
                new SicknessSymptom { SicknessId = 18, SymptomId = 65 }, // Swollen Joints
                new SicknessSymptom { SicknessId = 18, SymptomId = 66 }, // Stiff Joints
                new SicknessSymptom { SicknessId = 18, SymptomId = 13 } // Fatigue
            });
            
            // Fibromyalgia (19)
            data.AddRange(new[] {
                new SicknessSymptom { SicknessId = 19, SymptomId = 15 }, // Muscle Pain
                new SicknessSymptom { SicknessId = 19, SymptomId = 13 }, // Fatigue
                new SicknessSymptom { SicknessId = 19, SymptomId = 19 }, // Insomnia
                new SicknessSymptom { SicknessId = 19, SymptomId = 14 } // Joint Pain
            });
            
            // Chronic Fatigue Syndrome (20)
            data.AddRange(new[] {
                new SicknessSymptom { SicknessId = 20, SymptomId = 13 }, // Fatigue
                new SicknessSymptom { SicknessId = 20, SymptomId = 2 }, // Headache
                new SicknessSymptom { SicknessId = 20, SymptomId = 19 }, // Insomnia
                new SicknessSymptom { SicknessId = 20, SymptomId = 12 } // Dizziness
            });
            
            // Sleep Apnea (21)
            data.AddRange(new[] {
                new SicknessSymptom { SicknessId = 21, SymptomId = 13 }, // Fatigue
                new SicknessSymptom { SicknessId = 21, SymptomId = 19 }, // Insomnia
                new SicknessSymptom { SicknessId = 21, SymptomId = 2 } // Headache
            });
            
            // Depression (22)
            data.AddRange(new[] {
                new SicknessSymptom { SicknessId = 22, SymptomId = 13 }, // Fatigue
                new SicknessSymptom { SicknessId = 22, SymptomId = 19 }, // Insomnia
                new SicknessSymptom { SicknessId = 22, SymptomId = 20 }, // Loss of Appetite
                new SicknessSymptom { SicknessId = 22, SymptomId = 44 } // Depression
            });
            
            // Anxiety Disorder (23)
            data.AddRange(new[] {
                new SicknessSymptom { SicknessId = 23, SymptomId = 27 }, // Rapid Heartbeat
                new SicknessSymptom { SicknessId = 23, SymptomId = 12 }, // Dizziness
                new SicknessSymptom { SicknessId = 23, SymptomId = 7 }, // Shortness of Breath
                new SicknessSymptom { SicknessId = 23, SymptomId = 43 } // Anxiety
            });
            
            // High Cholesterol (24)
            data.AddRange(new[] {
                new SicknessSymptom { SicknessId = 24, SymptomId = 6 } // Chest Pain (can be asymptomatic)
            });
            
            // Coronary Artery Disease (25)
            data.AddRange(new[] {
                new SicknessSymptom { SicknessId = 25, SymptomId = 6 }, // Chest Pain
                new SicknessSymptom { SicknessId = 25, SymptomId = 7 }, // Shortness of Breath
                new SicknessSymptom { SicknessId = 25, SymptomId = 27 }, // Rapid Heartbeat
                new SicknessSymptom { SicknessId = 25, SymptomId = 74 } // Heart Palpitations
            });
            
            // Heart Failure (26)
            data.AddRange(new[] {
                new SicknessSymptom { SicknessId = 26, SymptomId = 7 }, // Shortness of Breath
                new SicknessSymptom { SicknessId = 26, SymptomId = 13 }, // Fatigue
                new SicknessSymptom { SicknessId = 26, SymptomId = 52 }, // Swelling
                new SicknessSymptom { SicknessId = 26, SymptomId = 27 } // Rapid Heartbeat
            });
            
            // Atrial Fibrillation (27)
            data.AddRange(new[] {
                new SicknessSymptom { SicknessId = 27, SymptomId = 28 }, // Irregular Heartbeat
                new SicknessSymptom { SicknessId = 27, SymptomId = 74 }, // Heart Palpitations
                new SicknessSymptom { SicknessId = 27, SymptomId = 7 }, // Shortness of Breath
                new SicknessSymptom { SicknessId = 27, SymptomId = 12 } // Dizziness
            });
            
            // COPD (28)
            data.AddRange(new[] {
                new SicknessSymptom { SicknessId = 28, SymptomId = 3 }, // Cough
                new SicknessSymptom { SicknessId = 28, SymptomId = 7 }, // Shortness of Breath
                new SicknessSymptom { SicknessId = 28, SymptomId = 26 } // Wheezing
            });
            
            // Emphysema (29)
            data.AddRange(new[] {
                new SicknessSymptom { SicknessId = 29, SymptomId = 7 }, // Shortness of Breath
                new SicknessSymptom { SicknessId = 29, SymptomId = 3 }, // Cough
                new SicknessSymptom { SicknessId = 29, SymptomId = 13 } // Fatigue
            });
            
            // Chronic Kidney Disease (30)
            data.AddRange(new[] {
                new SicknessSymptom { SicknessId = 30, SymptomId = 13 }, // Fatigue
                new SicknessSymptom { SicknessId = 30, SymptomId = 52 }, // Swelling
                new SicknessSymptom { SicknessId = 30, SymptomId = 12 } // Dizziness
            });
            
            // Kidney Stones (31)
            data.AddRange(new[] {
                new SicknessSymptom { SicknessId = 31, SymptomId = 11 }, // Abdominal Pain
                new SicknessSymptom { SicknessId = 31, SymptomId = 49 }, // Painful Urination
                new SicknessSymptom { SicknessId = 31, SymptomId = 48 }, // Blood in Urine
                new SicknessSymptom { SicknessId = 31, SymptomId = 8 } // Nausea
            });
            
            // Gout (32)
            data.AddRange(new[] {
                new SicknessSymptom { SicknessId = 32, SymptomId = 14 }, // Joint Pain
                new SicknessSymptom { SicknessId = 32, SymptomId = 65 }, // Swollen Joints
                new SicknessSymptom { SicknessId = 32, SymptomId = 52 }, // Swelling
                new SicknessSymptom { SicknessId = 32, SymptomId = 53 } // Redness
            });
            
            // Osteoporosis (33)
            data.AddRange(new[] {
                new SicknessSymptom { SicknessId = 33, SymptomId = 18 }, // Back Pain
                new SicknessSymptom { SicknessId = 33, SymptomId = 14 } // Joint Pain
            });
            
            // Hypothyroidism (34)
            data.AddRange(new[] {
                new SicknessSymptom { SicknessId = 34, SymptomId = 13 }, // Fatigue
                new SicknessSymptom { SicknessId = 34, SymptomId = 57 }, // Weight Gain
                new SicknessSymptom { SicknessId = 34, SymptomId = 92 }, // Cold Intolerance
                new SicknessSymptom { SicknessId = 34, SymptomId = 19 } // Insomnia
            });
            
            // Hyperthyroidism (35)
            data.AddRange(new[] {
                new SicknessSymptom { SicknessId = 35, SymptomId = 27 }, // Rapid Heartbeat
                new SicknessSymptom { SicknessId = 35, SymptomId = 56 }, // Weight Loss
                new SicknessSymptom { SicknessId = 35, SymptomId = 94 }, // Excessive Sweating
                new SicknessSymptom { SicknessId = 35, SymptomId = 93 } // Heat Intolerance
            });
            
            // IBS (36)
            data.AddRange(new[] {
                new SicknessSymptom { SicknessId = 36, SymptomId = 11 }, // Abdominal Pain
                new SicknessSymptom { SicknessId = 36, SymptomId = 10 }, // Diarrhea
                new SicknessSymptom { SicknessId = 36, SymptomId = 46 }, // Constipation
                new SicknessSymptom { SicknessId = 36, SymptomId = 8 } // Nausea
            });
            
            // Crohn's Disease (37)
            data.AddRange(new[] {
                new SicknessSymptom { SicknessId = 37, SymptomId = 11 }, // Abdominal Pain
                new SicknessSymptom { SicknessId = 37, SymptomId = 10 }, // Diarrhea
                new SicknessSymptom { SicknessId = 37, SymptomId = 1 }, // Fever
                new SicknessSymptom { SicknessId = 37, SymptomId = 56 } // Weight Loss
            });
            
            // Ulcerative Colitis (38)
            data.AddRange(new[] {
                new SicknessSymptom { SicknessId = 38, SymptomId = 10 }, // Diarrhea
                new SicknessSymptom { SicknessId = 38, SymptomId = 47 }, // Blood in Stool
                new SicknessSymptom { SicknessId = 38, SymptomId = 11 }, // Abdominal Pain
                new SicknessSymptom { SicknessId = 38, SymptomId = 1 } // Fever
            });
            
            // Peptic Ulcer Disease (39)
            data.AddRange(new[] {
                new SicknessSymptom { SicknessId = 39, SymptomId = 11 }, // Abdominal Pain
                new SicknessSymptom { SicknessId = 39, SymptomId = 8 }, // Nausea
                new SicknessSymptom { SicknessId = 39, SymptomId = 20 } // Loss of Appetite
            });
            
            // GERD (40)
            data.AddRange(new[] {
                new SicknessSymptom { SicknessId = 40, SymptomId = 4 }, // Sore Throat
                new SicknessSymptom { SicknessId = 40, SymptomId = 8 }, // Nausea
                new SicknessSymptom { SicknessId = 40, SymptomId = 11 } // Abdominal Pain
            });
            
            // Add more relationships for remaining sicknesses (41-100) with appropriate symptoms
            // This is a sample - you can expand with more realistic medical relationships
            
            return data;
        }
    }
}
