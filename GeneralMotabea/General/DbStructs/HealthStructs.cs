namespace GeneralMotabea.Core.General.DbStructs
{
    public struct HealthSchema
    {
        public const string Patient = "Patient";
        public const string Serv = "Service";
    }

    public struct PatientTab
    {
        public const string BaseData = "PatientBaseTBL";
        public const string Patient = "ParientDataTBL";
        public const string Booking = "BookingTBL";
        public const string PatientDiag = "PatDiagTBL";
        public const string PatientSymp = "PatSympTBL";
        public const string PatBio = "PatBioTBL";
        public const string PatRays = "PatRaysTBL";
        public const string PatAnalysis = "PatAnalysisTBL";
        public const string PatMed = "PatMedicineTBL";
        public const string PatCT = "PatCt_TBL";
    }

    public struct ServTab
    {
        public const string Symptoms = "SymptomsTBL";
        public const string Diagnostic = "DiagnosticTBL";
        public const string Biometirc = "BiometricTBL";
        public const string Rays = "RaysTBL";
        public const string Analysis = "AnalysisTBL";
        public const string Medicine = "MedicineTBL";
        public const string CT = "CT_TBL";
    }
}
