namespace CandidateHubAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CandidateDto, Candidate>().ReverseMap();
        }
    }
}
