using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TournamentsMVC.Models;
using TournamentsMVC.Mapping;

namespace TournamentsMVC.ViewModels.Models
{
    public class TeamViewModel : IMapFrom<Team> //, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Rating { get; set; }


        //public string NameAndId { get; set; }

        //public void CreateMappings(IMapperConfigurationExpression config)
        //{
        //    config.CreateMap<Team, TeamViewModel>()
        //        .ForMember(x => x.NameAndId, opt => opt.MapFrom(x => x.Name + " " + x.Id));
        //}
    }
}