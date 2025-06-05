using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using BLL.Dto;
using DAL.Entities;
using InnoClinic.Messaging.Contracts.Events.Doctor;
using InnoClinic.Messaging.Contracts.Events.Office;
using InnoClinic.Messaging.Contracts.Events.Patient;
using InnoClinic.Messaging.Contracts.Events.Service;

namespace BLL.Mappers;

[ExcludeFromCodeCoverage]
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateAppointmentDto, Appointment>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.Patient, opt => opt.Ignore())
            .ForMember(dest => dest.Doctor, opt => opt.Ignore())
            .ForMember(dest => dest.Office, opt => opt.Ignore())
            .ForMember(dest => dest.Service, opt => opt.Ignore())
            .ForMember(dest => dest.Result, opt => opt.Ignore());

        CreateMap<UpdateAppointmentDto, Appointment>()
            .ForMember(dest => dest.Patient, opt => opt.Ignore())
            .ForMember(dest => dest.Doctor, opt => opt.Ignore())
            .ForMember(dest => dest.Office, opt => opt.Ignore())
            .ForMember(dest => dest.Service, opt => opt.Ignore())
            .ForMember(dest => dest.Result, opt => opt.Ignore());

        CreateMap<Appointment, AppointmentDto>()
            .ForMember(dest => dest.Patient, opt => opt.MapFrom(src => src.Patient))
            .ForMember(dest => dest.Doctor, opt => opt.MapFrom(src => src.Doctor))
            .ForMember(dest => dest.Office, opt => opt.MapFrom(src => src.Office))
            .ForMember(dest => dest.Service, opt => opt.MapFrom(src => src.Service))
            .ForMember(dest => dest.Result, opt => opt.MapFrom(src => src.Result));

        CreateMap<Patient, PatientDto>();
        CreateMap<Doctor, DoctorDto>();
        CreateMap<Service, ServiceDto>();
        CreateMap<Office, OfficeDto>();

        CreateMap<CreateAppointmentResultDto, AppointmentResult>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.Appointment, opt => opt.Ignore());

        CreateMap<UpdateAppointmentResultDto, AppointmentResult>()
            .ForMember(dest => dest.Appointment, opt => opt.Ignore());

        CreateMap<AppointmentResult, AppointmentResultDto>();

        CreateMap<ServiceCreatedIntegrationEvent, Service>()
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description ?? string.Empty)); 
        CreateMap<ServiceUpdatedIntegrationEvent, Service>()
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description ?? string.Empty));
        CreateMap<ServiceDeletedIntegrationEvent, Service>()
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description ?? string.Empty));

        CreateMap<OfficeCreatedIntegrationEvent, Office>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));
        CreateMap<OfficeUpdatedIntegrationEvent, Office>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));
        CreateMap<OfficeDeletedIntegrationEvent, Office>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));

        CreateMap<PatientCreatedIntegrationEvent, Patient>();
        CreateMap<PatientUpdatedIntegrationEvent, Patient>();
        CreateMap<PatientDeletedIntegrationEvent, Patient>();

        CreateMap<DoctorCreatedIntegrationEvent, Doctor>();
        CreateMap<DoctorUpdatedIntegrationEvent, Doctor>();
        CreateMap<DoctorDeletedIntegrationEvent, Doctor>();
    }
}