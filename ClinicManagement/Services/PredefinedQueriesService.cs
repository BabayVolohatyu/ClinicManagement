namespace ClinicManagement.Services
{
    public class PredefinedQueriesService
    {
        public Dictionary<string, QueryDefinition> GetQueries()
        {
            return new Dictionary<string, QueryDefinition>
            {
                ["reception-schedule"] = new QueryDefinition
                {
                    Name = "Reception Schedule",
                    Description = "Reception days and hours of doctors + corresponding cabinets",
                    Query = @"SELECT 
                p.""FirstName"" || ' ' || p.""LastName"" AS ""Doctor"",
                s.""StartTime"",
                s.""EndTime"",
                c.""Building"",
                c.""Floor"",
                c.""Number"" AS ""CabinetNumber""
            FROM ""Schedules"" s
            JOIN ""Doctors"" d ON s.""DoctorId"" = d.""Id""
            JOIN ""People"" p ON d.""PersonId"" = p.""Id""
            JOIN ""Cabinets"" c ON s.""CabinetId"" = c.""Id""
            ORDER BY p.""LastName"", s.""StartTime"";",
                    RequiresParameters = false
                },
                
                ["doctor-details"] = new QueryDefinition
                {
                    Name = "Doctor Details",
                    Description = "Detailed information about doctors and issued certificates",
                    Query = @"SELECT 
                p.""FirstName"" || ' ' || p.""LastName"" AS ""Doctor"",
                sp.""Name"" AS ""Specialty"",
                COUNT(DISTINCT a.""Id"") AS ""TotalAppointments"",
                COUNT(DISTINCT dg.""Id"") AS ""TotalDiagnoses""
            FROM ""Doctors"" d
            JOIN ""People"" p ON d.""PersonId"" = p.""Id""
            JOIN ""Specialties"" sp ON d.""SpecialtyId"" = sp.""Id""
            LEFT JOIN ""DoctorProcedures"" dp ON d.""Id"" = dp.""DoctorId""
            LEFT JOIN ""Appointments"" a ON dp.""Id"" = a.""DoctorProcedureId""
            LEFT JOIN ""Diagnoses"" dg ON a.""Id"" = dg.""AppointmentId""
            GROUP BY p.""Id"", p.""FirstName"", p.""LastName"", sp.""Name""
            ORDER BY p.""LastName"";",
                    RequiresParameters = false
                },
                
                ["patients-per-week"] = new QueryDefinition
                {
                    Name = "Weekly Patient Count",
                    Description = "Number of patients examined by a doctor per week",
                    Query = @"SELECT COUNT(DISTINCT a.""PatientId"") AS ""PatientCount""
                FROM ""Appointments"" a
                JOIN ""DoctorProcedures"" dp ON a.""DoctorProcedureId"" = dp.""Id""
                WHERE dp.""DoctorId"" = {0}
                AND a.""StartTime"" >= CURRENT_DATE - INTERVAL '7 days';",
                    RequiresParameters = true,
                    ParameterLabels = new[] { "Doctor ID" }
                },
                
                ["search-patients-lastname"] = new QueryDefinition
                {
                    Name = "Search by Last Name",
                    Description = "Search patients by last name",
                    Query = @"SELECT 
                    p.""FirstName"",
                    p.""LastName"",
                    p.""Patronymic"",
                    pt.""Id"" AS ""PatientId"",
                    a.""Country"" || ', ' || a.""State"" || ', ' || a.""Locality"" AS ""Address""
                FROM ""Patients"" pt
                JOIN ""People"" p ON pt.""PersonId"" = p.""Id""
                JOIN ""Addresses"" a ON pt.""AddressId"" = a.""Id""
                WHERE p.""LastName"" ILIKE '%{0}%'
                ORDER BY p.""LastName"";",
                    RequiresParameters = true,
                    ParameterLabels = new[] { "Last Name" }
                },
                
                ["search-patients-record"] = new QueryDefinition
                {
                    Name = "Search by Record Number",
                    Description = "Search patients by medical record number (Patient ID) - shows patient history",
                    Query = @"SELECT 
                    p.""FirstName"",
                    p.""LastName"",
                    p.""Patronymic"",
                    pt.""Id"" AS ""PatientId"",
                    addr.""Country"" || ', ' || addr.""State"" || ', ' || addr.""Locality"" AS ""Address"",
                    apt.""StartTime"" AS ""AppointmentDate"",
                    apt.""EndTime"" AS ""AppointmentEndTime"",
                    apt.""DidItHappen"" AS ""Completed"",
                    doc_p.""FirstName"" || ' ' || doc_p.""LastName"" AS ""Doctor"",
                    sp.""Name"" AS ""Specialty"",
                    pr.""Name"" AS ""Procedure"",
                    dg.""Prescription"" AS ""Diagnosis"",
                    c.""Building"" || ', Floor ' || c.""Floor"" || ', Cabinet ' || c.""Number"" AS ""Cabinet""
                FROM ""Patients"" pt
                JOIN ""People"" p ON pt.""PersonId"" = p.""Id""
                JOIN ""Addresses"" addr ON pt.""AddressId"" = addr.""Id""
                LEFT JOIN ""Appointments"" apt ON pt.""Id"" = apt.""PatientId""
                LEFT JOIN ""DoctorProcedures"" dp ON apt.""DoctorProcedureId"" = dp.""Id""
                LEFT JOIN ""Doctors"" d ON dp.""DoctorId"" = d.""Id""
                LEFT JOIN ""People"" doc_p ON d.""PersonId"" = doc_p.""Id""
                LEFT JOIN ""Specialties"" sp ON d.""SpecialtyId"" = sp.""Id""
                LEFT JOIN ""Procedures"" pr ON dp.""ProcedureId"" = pr.""Id""
                LEFT JOIN ""Diagnoses"" dg ON apt.""Id"" = dg.""AppointmentId""
                LEFT JOIN ""Cabinets"" c ON apt.""CabinetId"" = c.""Id""
                WHERE pt.""Id"" = {0}
                ORDER BY apt.""StartTime"" DESC NULLS LAST;",
                    RequiresParameters = true,
                    ParameterLabels = new[] { "Patient ID" }
                },
                
                ["search-patients-condition"] = new QueryDefinition
                {
                    Name = "Search by Condition",
                    Description = "Search patients by health condition (sickness name)",
                    Query = @"SELECT DISTINCT
                    p.""FirstName"",
                    p.""LastName"",
                    p.""Patronymic"",
                    pt.""Id"" AS ""PatientId"",
                    dg.""Prescription"" AS ""Diagnosis""
                FROM ""Patients"" pt
                JOIN ""People"" p ON pt.""PersonId"" = p.""Id""
                JOIN ""Appointments"" a ON pt.""Id"" = a.""PatientId""
                JOIN ""Diagnoses"" dg ON a.""Id"" = dg.""AppointmentId""
                WHERE dg.""Prescription"" ILIKE '%{0}%'
                ORDER BY p.""LastName"";",
                    RequiresParameters = true,
                    ParameterLabels = new[] { "Condition/Sickness Name" }
                },
                
                ["search-patients-doctor"] = new QueryDefinition
                {
                    Name = "Search by Doctor",
                    Description = "Search patients by assigned doctor",
                    Query = @"SELECT DISTINCT
                    p.""FirstName"",
                    p.""LastName"",
                    p.""Patronymic"",
                    pt.""Id"" AS ""PatientId""
                FROM ""Patients"" pt
                JOIN ""People"" p ON pt.""PersonId"" = p.""Id""
                JOIN ""Appointments"" a ON pt.""Id"" = a.""PatientId""
                JOIN ""DoctorProcedures"" dp ON a.""DoctorProcedureId"" = dp.""Id""
                WHERE dp.""DoctorId"" = {0}
                ORDER BY p.""LastName"";",
                    RequiresParameters = true,
                    ParameterLabels = new[] { "Doctor ID" }
                },
                
                ["patients-multiple-doctors"] = new QueryDefinition
                {
                    Name = "Multiple Doctors",
                    Description = "Patients examined by more than 2 doctors per week",
                    Query = @"SELECT 
                p.""FirstName"" || ' ' || p.""LastName"" AS ""Patient"",
                COUNT(DISTINCT dp.""DoctorId"") AS ""DoctorCount""
            FROM ""Patients"" pt
            JOIN ""People"" p ON pt.""PersonId"" = p.""Id""
            JOIN ""Appointments"" a ON pt.""Id"" = a.""PatientId""
            JOIN ""DoctorProcedures"" dp ON a.""DoctorProcedureId"" = dp.""Id""
            WHERE a.""StartTime"" >= CURRENT_DATE - INTERVAL '7 days'
            GROUP BY pt.""Id"", p.""FirstName"", p.""LastName""
            HAVING COUNT(DISTINCT dp.""DoctorId"") > 2
            ORDER BY ""DoctorCount"" DESC;",
                    RequiresParameters = false
                },
                
                ["angina-count"] = new QueryDefinition
                {
                    Name = "Angina Statistics",
                    Description = "Number of \"angina\" diagnosis cases per month",
                    Query = @"SELECT COUNT(*) AS ""AnginaCount""
                FROM ""Diagnoses"" dg
                JOIN ""Appointments"" a ON dg.""AppointmentId"" = a.""Id""
                WHERE dg.""Prescription"" ILIKE '%angina%'
                AND a.""StartTime"" >= DATE_TRUNC('month', CURRENT_DATE)
                AND a.""StartTime"" < DATE_TRUNC('month', CURRENT_DATE) + INTERVAL '1 month';",
                    RequiresParameters = false
                },
                
                ["doctor-schedule"] = new QueryDefinition
                {
                    Name = "Doctor Schedule",
                    Description = "Full work schedule of a doctor for week/month",
                    Query = @"SELECT 
                    s.""StartTime"",
                    s.""EndTime"",
                    c.""Building"",
                    c.""Floor"",
                    c.""Number"" AS ""CabinetNumber""
                FROM ""Schedules"" s
                JOIN ""Cabinets"" c ON s.""CabinetId"" = c.""Id""
                WHERE s.""DoctorId"" = {0}
                AND s.""StartTime"" >= '{1}'::date
                AND s.""StartTime"" < '{2}'::date
                ORDER BY s.""StartTime"";",
                    RequiresParameters = true,
                    ParameterLabels = new[] { "Doctor ID", "Start Date (YYYY-MM-DD)", "End Date (YYYY-MM-DD)" }
                },
                
                ["doctors-by-specialty"] = new QueryDefinition
                {
                    Name = "Doctors by Specialty",
                    Description = "List and count of doctors of specified specialty",
                    Query = @"SELECT 
                    p.""FirstName"" || ' ' || p.""LastName"" AS ""Doctor"",
                    sp.""Name"" AS ""Specialty"",
                    (SELECT COUNT(*) FROM ""Doctors"" d2 WHERE d2.""SpecialtyId"" = sp.""Id"") AS ""TotalDoctorsInSpecialty""
                FROM ""Doctors"" d
                JOIN ""People"" p ON d.""PersonId"" = p.""Id""
                JOIN ""Specialties"" sp ON d.""SpecialtyId"" = sp.""Id""
                WHERE sp.""Name"" ILIKE '%{0}%'
                ORDER BY p.""LastName"";",
                    RequiresParameters = true,
                    ParameterLabels = new[] { "Specialty Name" }
                },
                
                ["home-call-patients"] = new QueryDefinition
                {
                    Name = "Home Call Patients",
                    Description = "Last names and addresses of patients who called a doctor home",
                    Query = @"SELECT 
                p.""LastName"",
                a.""Country"" || ', ' || a.""State"" || ', ' || a.""Locality"" || ', ' || a.""StreetName"" || ' ' || a.""StreetNumber"" AS ""Address""
            FROM ""HomeCallLogs"" hcl
            JOIN ""Doctors"" d ON hcl.""DoctorId"" = d.""Id""
            JOIN ""Patients"" pt ON hcl.""AddressId"" = pt.""AddressId""
            JOIN ""People"" p ON pt.""PersonId"" = p.""Id""
            JOIN ""Addresses"" a ON hcl.""AddressId"" = a.""Id""
            ORDER BY p.""LastName"";",
                    RequiresParameters = false
                },
                
                ["home-calls-count"] = new QueryDefinition
                {
                    Name = "Home Calls Count",
                    Description = "Number of home calls per doctor",
                    Query = @"SELECT 
                p.""FirstName"" || ' ' || p.""LastName"" AS ""Doctor"",
                COUNT(*) AS ""HomeCallsCount""
            FROM ""HomeCallLogs"" hcl
            JOIN ""Doctors"" d ON hcl.""DoctorId"" = d.""Id""
            JOIN ""People"" p ON d.""PersonId"" = p.""Id""
            GROUP BY d.""Id"", p.""FirstName"", p.""LastName""
            ORDER BY ""HomeCallsCount"" DESC;",
                    RequiresParameters = false
                },
                
                ["procedures-total"] = new QueryDefinition
                {
                    Name = "Procedures Total",
                    Description = "Total number of treatment procedures per week",
                    Query = @"SELECT COUNT(*) AS ""TotalProcedures""
            FROM ""Appointments"" a
            WHERE a.""StartTime"" >= CURRENT_DATE - INTERVAL '7 days'
            AND a.""DidItHappen"" = true;",
                    RequiresParameters = false
                },
                
                ["patients-with-procedures"] = new QueryDefinition
                {
                    Name = "Patients with Procedures",
                    Description = "Patients who received treatment procedures",
                    Query = @"SELECT DISTINCT
                p.""FirstName"" || ' ' || p.""LastName"" AS ""Patient"",
                pr.""Name"" AS ""Procedure"",
                p.""LastName""
            FROM ""Patients"" pt
            JOIN ""People"" p ON pt.""PersonId"" = p.""Id""
            JOIN ""Appointments"" a ON pt.""Id"" = a.""PatientId""
            JOIN ""DoctorProcedures"" dp ON a.""DoctorProcedureId"" = dp.""Id""
            JOIN ""Procedures"" pr ON dp.""ProcedureId"" = pr.""Id""
            WHERE a.""DidItHappen"" = true
            ORDER BY p.""LastName"", pr.""Name"";",
                    RequiresParameters = false
                },
                
                ["fluorography-patients"] = new QueryDefinition
                {
                    Name = "Fluorography Patients",
                    Description = "Patients who underwent fluorography on a specific day",
                    Query = @"SELECT 
                    p.""FirstName"" || ' ' || p.""LastName"" AS ""Patient"",
                    a.""StartTime""
                FROM ""Patients"" pt
                JOIN ""People"" p ON pt.""PersonId"" = p.""Id""
                JOIN ""Appointments"" a ON pt.""Id"" = a.""PatientId""
                JOIN ""DoctorProcedures"" dp ON a.""DoctorProcedureId"" = dp.""Id""
                JOIN ""Procedures"" pr ON dp.""ProcedureId"" = pr.""Id""
                WHERE pr.""Name"" ILIKE '%fluorography%'
                AND a.""StartTime"" >= '{0}'::date
                AND a.""StartTime"" < '{0}'::date + INTERVAL '1 day'
                ORDER BY a.""StartTime"";",
                    RequiresParameters = true,
                    ParameterLabels = new[] { "Date (YYYY-MM-DD)" }
                },
                
                ["missed-vaccination"] = new QueryDefinition
                {
                    Name = "Missed Vaccination",
                    Description = "Patients who did not complete scheduled vaccination",
                    Query = @"SELECT 
                p.""FirstName"" || ' ' || p.""LastName"" AS ""Patient"",
                pt.""Id"" AS ""PatientId""
            FROM ""Patients"" pt
            JOIN ""People"" p ON pt.""PersonId"" = p.""Id""
            WHERE pt.""Id"" NOT IN (
                SELECT DISTINCT pt2.""Id""
                FROM ""Patients"" pt2
                JOIN ""Appointments"" a ON pt2.""Id"" = a.""PatientId""
                JOIN ""DoctorProcedures"" dp ON a.""DoctorProcedureId"" = dp.""Id""
                JOIN ""Procedures"" pr ON dp.""ProcedureId"" = pr.""Id""
                WHERE pr.""Name"" ILIKE '%vaccination%'
                AND a.""DidItHappen"" = true
            )
            ORDER BY p.""LastName"";",
                    RequiresParameters = false
                },
                
                ["physio-schedule"] = new QueryDefinition
                {
                    Name = "Physio Room Schedule",
                    Description = "Schedule by shifts (1, 2, 3) for physical therapy rooms",
                    Query = @"SELECT 
                    c.""Building"",
                    c.""Floor"",
                    c.""Number"" AS ""CabinetNumber"",
                    s.""StartTime"",
                    s.""EndTime"",
                    p.""FirstName"" || ' ' || p.""LastName"" AS ""Doctor""
                FROM ""Schedules"" s
                JOIN ""Cabinets"" c ON s.""CabinetId"" = c.""Id""
                JOIN ""CabinetTypes"" ct ON c.""TypeId"" = ct.""Id""
                JOIN ""Doctors"" d ON s.""DoctorId"" = d.""Id""
                JOIN ""People"" p ON d.""PersonId"" = p.""Id""
                WHERE (ct.""Type"" ILIKE '%physical%' OR ct.""Type"" ILIKE '%therapy%')
                AND (
                    '{0}' = '3' OR
                    ('{0}' = '1' AND EXTRACT(HOUR FROM s.""StartTime"") >= 8 AND EXTRACT(HOUR FROM s.""StartTime"") < 12) OR
                    ('{0}' = '2' AND EXTRACT(HOUR FROM s.""StartTime"") >= 13 AND EXTRACT(HOUR FROM s.""StartTime"") < 17)
                )
                ORDER BY c.""Building"", c.""Floor"", s.""StartTime"";",
                    RequiresParameters = true,
                    ParameterLabels = new[] { "Shift Filter (1=morning 8-12, 2=afternoon 13-17, 3=both)" }
                },
                
                ["physio-doctors-count"] = new QueryDefinition
                {
                    Name = "Physio Room Doctors",
                    Description = "Number of doctors in physical therapy rooms per week",
                    Query = @"SELECT COUNT(DISTINCT d.""Id"") AS ""DoctorCount""
            FROM ""Schedules"" s
            JOIN ""Cabinets"" c ON s.""CabinetId"" = c.""Id""
            JOIN ""CabinetTypes"" ct ON c.""TypeId"" = ct.""Id""
            JOIN ""Doctors"" d ON s.""DoctorId"" = d.""Id""
            WHERE (ct.""Type"" ILIKE '%physical%' OR ct.""Type"" ILIKE '%therapy%')
            AND s.""StartTime"" >= CURRENT_DATE - INTERVAL '7 days';",
                    RequiresParameters = false
                },
                
                ["visit-stats-total"] = new QueryDefinition
                {
                    Name = "Total Visits",
                    Description = "Total number of visits per month",
                    Query = @"SELECT COUNT(*) AS ""TotalVisits""
                FROM ""Appointments""
                WHERE ""StartTime"" >= DATE_TRUNC('month', CURRENT_DATE)
                AND ""StartTime"" < DATE_TRUNC('month', CURRENT_DATE) + INTERVAL '1 month';",
                    RequiresParameters = false
                },
                
                ["visit-stats-by-group"] = new QueryDefinition
                {
                    Name = "Visits by Specialty",
                    Description = "Visit statistics by each doctor group (specialty)",
                    Query = @"SELECT 
                    sp.""Name"" AS ""Specialty"",
                    COUNT(*) AS ""VisitCount""
                FROM ""Appointments"" a
                JOIN ""DoctorProcedures"" dp ON a.""DoctorProcedureId"" = dp.""Id""
                JOIN ""Doctors"" d ON dp.""DoctorId"" = d.""Id""
                JOIN ""Specialties"" sp ON d.""SpecialtyId"" = sp.""Id""
                WHERE a.""StartTime"" >= DATE_TRUNC('month', CURRENT_DATE)
                AND a.""StartTime"" < DATE_TRUNC('month', CURRENT_DATE) + INTERVAL '1 month'
                GROUP BY sp.""Name""
                ORDER BY ""VisitCount"" DESC;",
                    RequiresParameters = false
                }
            };
        }

        public string BuildQuery(string queryKey, params string[] parameters)
        {
            var queries = GetQueries();
            if (!queries.ContainsKey(queryKey))
            {
                throw new ArgumentException($"Query key '{queryKey}' not found");
            }

            var queryDef = queries[queryKey];
            var query = queryDef.Query;

            
            for (int i = 0; i < parameters.Length; i++)
            {
                var param = parameters[i];
                
                param = param.Replace("'", "''");
                query = query.Replace($"{{{i}}}", param);
            }

            
            if (!queryDef.RequiresParameters)
            {
                int placeholderIndex = 0;
                while (query.Contains($"{{{placeholderIndex}}}"))
                {
                    query = query.Replace($"{{{placeholderIndex}}}", "");
                    placeholderIndex++;
                }
            }

            return query;
        }
    }

    public class QueryDefinition
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Query { get; set; } = string.Empty;
        public bool RequiresParameters { get; set; }
        public string[]? ParameterLabels { get; set; }
    }
}

