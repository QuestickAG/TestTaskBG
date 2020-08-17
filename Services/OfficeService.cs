using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestTaskBarsGroup.Dto;

namespace TestTaskBarsGroup.Services
{
    public class OfficeService

    {
        private readonly ApplicationContext _dbContext;

        public OfficeService(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddOffice(string name, string city)
        {
            var office = new Office()
            {
                OfficeName = name,
                OfficeCityName = city
            };

            _dbContext.Add(office);
            _dbContext.SaveChanges();
        }

        public bool RemoveOffice(int id)
        {
            var office = _dbContext.Office.Find(id);
            if (office != null)
            {
                _dbContext.Office.Remove(office);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<OfficeDto> GetOffices()
        {
            var offices = _dbContext.Office
                .Select(offices => new OfficeDto
                {
                    Id = offices.Id,
                    OfficeName =offices.OfficeName,
                    OfficeCityName = offices.OfficeCityName
                })
                .ToList();
            return offices;
        }

        public void ShowOffices(List<OfficeDto> offices)
        {
            foreach (var office in offices)
            {
                Console.WriteLine($"id - {office.Id}) {office.OfficeName} в городе {office.OfficeCityName}");
            }
        }
    }
}
