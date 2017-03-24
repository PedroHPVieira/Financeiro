using AutoMapper;
using Financeiro.Dtos;
using Financeiro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Financeiro.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //From Domain to Dto
            Mapper.CreateMap<Empresa, EmpresaDto>();


            //From Dto to Domain
            Mapper.CreateMap<EmpresaDto, Empresa>().ForMember(c => c.Id, opt => opt.Ignore());
        }        
    }
}