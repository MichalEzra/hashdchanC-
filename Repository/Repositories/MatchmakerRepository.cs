using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class MatchmakerRepository : IRepository<Matchmaker>
    {
        private readonly IContext context;
        public MatchmakerRepository(IContext context)
        {
            this.context = context;
        }
        public Matchmaker AddItem(Matchmaker item)
        {
            this.context.Matchmakers.Add(item);
            this.context.Save();
            return item;
        }

        //public void DeleteItem(int id)
        //{
        //    this.context.Matchmakers.Remove(GetById(id));
        //    this.context.Save();
        //}
        public void DeleteItem(int id)
        {
            var matchmaker = GetById(id);
            if (matchmaker == null)
            {
                throw new Exception("Matchmaker not found");
            }
            this.context.Matchmakers.Remove(matchmaker);
            this.context.Save();
        }

        public List<Matchmaker> GetAll()
        {
            return context.Matchmakers.ToList();
        }

        public Matchmaker GetById(int id)
        {
            return context.Matchmakers.FirstOrDefault(x => x.Id == id);
        }

        public void UpdateItem(int id, Matchmaker item)
        {
            var matchmaker = GetById(id);
            if (matchmaker == null)
            {
                throw new Exception("Matchmaker not found");
            }

            matchmaker.UserId = item.UserId;
            matchmaker.FirstName = item.FirstName;
            matchmaker.LastName = item.LastName;
            matchmaker.BirthDate = item.BirthDate;
            matchmaker.MatchmakerGender = item.MatchmakerGender;
            matchmaker.IdentityNumber = item.IdentityNumber;
            matchmaker.MarriageDate = item.MarriageDate;
            matchmaker.Country = item.Country;
            matchmaker.City = item.City;
            matchmaker.MatchmakerSector = item.MatchmakerSector;
            matchmaker.SubSector = item.SubSector;
            matchmaker.YearsOfExperience = item.YearsOfExperience;
            matchmaker.MatchesClosed = item.MatchesClosed;
            matchmaker.Languages = item.Languages;
            matchmaker.ReligiousOpenness = item.ReligiousOpenness;

            // פרטי בנק
            matchmaker.BankName = item.BankName;
            matchmaker.BranchNumber = item.BranchNumber;
            matchmaker.AccountNumber = item.AccountNumber;
            matchmaker.AccountName = item.AccountName;

            // המלצות
            matchmaker.RecommendedMatchmaker1 = item.RecommendedMatchmaker1;
            matchmaker.RecommendedMatchmaker2 = item.RecommendedMatchmaker2;

            // דמי שידוך
            matchmaker.MatchFeeFirstMarriage = item.MatchFeeFirstMarriage;
            matchmaker.MatchFeeSecondMarriage = item.MatchFeeSecondMarriage;
            matchmaker.MatchFeeAbove30 = item.MatchFeeAbove30;

            matchmaker.PhoneNumber = item.PhoneNumber;

            context.Save();
        }
    }
}

