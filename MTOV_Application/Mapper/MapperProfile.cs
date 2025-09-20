namespace MTOV_Application.Mapper
{
    using AutoMapper;
    using MTOV_Domain.Models;
    using MTOV_DTO.Account;
    using MTOV_DTO.Trades;

    /// <summary>
    /// Auto mapper
    /// </summary>
    public class MapperProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MapperProfile"/> class.
        /// </summary>
        public MapperProfile()
        {
            CreateMap<AccountModel, AccountDetailResDto>();
            CreateMap<TradesModel, OpenTradesResDto>();
        }
    }
}