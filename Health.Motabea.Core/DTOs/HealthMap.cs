using AutoMapper;
using Health.Motabea.Core.DTOs.Patients;
using Health.Motabea.Core.DTOs.Services;
using Health.Motabea.Core.Models.Patients;
using Health.Motabea.Core.Models.Services;
using UserManagement.Core.DTOs;
using UserManagement.Core.Models.Address;

namespace Health.Motabea.Core.DTOs
{
    public class HealthMap :Profile
    {
        public HealthMap()
        {
            CreateMap<PatientBaseData, PatientBaseDTO>();
            CreateMap<PatientBaseDTO, PatientBaseData>();
            CreateMap<PatientData, PatientDataDto>();
            CreateMap<PatientDataDto, PatientData>();
            CreateMap<Diagnostic, DiagnosticDTO>();
            CreateMap<DiagnosticDTO, Diagnostic>();
            CreateMap<Symptoms, SymptomsDTO>();
            CreateMap<SymptomsDTO, Symptoms>();
            CreateMap<Biometrics, BioDTO>();
            CreateMap<BioDTO, Biometrics>();
            CreateMap<CT_DTO, CT>();
            CreateMap<CT, CT_DTO>();
            CreateMap<Medicine, MedicinDTO>();
            CreateMap<MedicinDTO, Medicine>();
            CreateMap<Analysis, AnalysisDTO>();
            CreateMap<AnalysisDTO, Analysis>();
            CreateMap<Rays, RaysDTO>();
            CreateMap<RaysDTO, Rays>();

            CreateMap<Booking, BookDTO>();
            CreateMap<BookDTO, Booking>();
            CreateMap<PatCtDTO, PatCt>();
            CreateMap<PatCt, PatCtDTO>();
            CreateMap<PatAnalysis, PatAnalysDTO>();
            CreateMap<PatAnalysDTO, PatAnalysis>();
            CreateMap<PatMed, PatMedDTO>();
            CreateMap<PatMedDTO, PatMed>();
            CreateMap<PatientRays, PatRayDTO>();
            CreateMap<PatRayDTO, PatientRays>();
            CreateMap<PatientDiag, PatientDiagDTO>();
            CreateMap<PatientDiagDTO, PatientDiag>();
            CreateMap<PatientBio, PatBioDTO>();
            CreateMap<PatBioDTO, PatientBio>();
            CreateMap<PatientSymptom, PatSympDTO>();
            CreateMap<PatSympDTO, PatientSymptom>();

            //
            CreateMap<Work, WorkDTO>();
            CreateMap<WorkDTO, Work>();
        }
    }
}
